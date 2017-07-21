using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 分销商相关微信消息通知
    /// </summary>
    public class DistributorNotice
    {
        /// <summary>
        /// 会员成为分销商
        /// </summary>
        /// <param name="userid"></param>
        public void becomeToVIP(int userid,decimal paymoney)
        {
            
            ECustomerInfo user = new ECustomer().GetModel(userid);
            if (user.PayPWDAnswer == "1")
            {
                return;
            }

            Hangjing.DBUtility.SQLHelper.excutesql("UPDATE dbo.ECustomer SET PayPWDAnswer = 1 WHERE dataid = " + userid);


            Hangjing.Weixin.SendMsg weixinsender = new Weixin.SendMsg(null);

            //给用户发文字消息
            if (user.PayPWDQuestion.Length > 0)
            {
                string body = CacheHelper.GetSetValue(75);
                body = body.Replace("{username}", user.Name);
                weixinsender.sendText(user.PayPWDQuestion, body);

                distributorInfo superiors = new distributor().GetSuperiors(user.PayPWDQuestion);

                distributorInfo mymodel = new distributorInfo();
                mymodel.userid = user.DataID;
                mymodel.onegradeID = 0;
                mymodel.twogradeID = 0;
                mymodel.thressgradeID = 0;
                mymodel.reveint1 = 0;
                mymodel.reveint2 = 0;
                mymodel.reveint3 = 0;
                mymodel.reveint4 = 0;
                mymodel.revevar1 = "";
                mymodel.revevar2 = "";
                mymodel.revevar3 = "";
                mymodel.revevar4 = "";

                //，给上级，上上级，上上上级发消息
                if (superiors.dId > 0)
                {
                    mymodel.onegradeID = superiors.onegradeID;
                    mymodel.twogradeID = superiors.twogradeID;
                    mymodel.thressgradeID = superiors.thressgradeID;
                }
                new distributor().Add(mymodel);

                //各上线分佣金
                shareCommission(user.DataID, user.Name, 2, paymoney);
            }
          
        }

        /// <summary>
        /// 佣金支付到各级上线
        /// </summary>
        /// <param name="userid">会员编号</param>
        /// <param name="type">1，分销提成比例 ；2，成为分销商提成比例</param>
        /// <param name="sharemoney">分销的总金额</param>
        public void shareCommission(int userid,string username,int type,decimal sharemoney)
        {

            Hangjing.AppLog.AppLog.Info("sharemoney="+ sharemoney);

            string buinessmsg = "完成订单";
            if (type == 2)
            {
                buinessmsg = "付款成功,成为分销商";
            }

            Hangjing.Weixin.SendMsg weixinsender = new Weixin.SendMsg(null);
            UserDistributionLog dal = new UserDistributionLog();
            distributorInfo model = new distributor().GetSuperiors(userid);
            if (model != null)
            {
                distributeRatioInfo config = CacheHelper.GetDistributeRatConfigs().Where(a => a.drId == type).FirstOrDefault();

                if (model.onegradeID > 0 && model.onestate == 0)
                {
                    UserDistributionLogInfo info = new UserDistributionLogInfo();
                    info.AddMoney = sharemoney * config.onegraderatio/100;
                    info.Inve1 = 0;
                    info.UserId = model.onegradeID;
                    info.State = 0;
                    info.PayType = 0;
                    info.PayDate = DateTime.Now;
                    info.PayState = 1;
                    info.AddDate = DateTime.Now;
                    info.Inve2 = "您的一级会员【"+username+"】"+ buinessmsg + "，获得佣金:"+info.AddMoney+"元";

                    dal.AddMoney(info);

                    weixinsender.sendText(model.oneopenid, info.Inve2);

                }

                if (model.twogradeID > 0 && model.twostate == 0)
                {
                    UserDistributionLogInfo info = new UserDistributionLogInfo();
                    info.AddMoney = sharemoney * config.twograderatio / 100;
                    info.Inve1 = 0;
                    info.UserId = model.twogradeID;
                    info.State = 0;
                    info.PayType = 0;
                    info.PayDate = DateTime.Now;
                    info.PayState = 1;
                    info.AddDate = DateTime.Now;
                    info.Inve2 = "您的二级会员【" + username + "】" + buinessmsg + "，获得佣金:" + info.AddMoney + "元";

                    dal.AddMoney(info);

                    weixinsender.sendText(model.twoopenid, info.Inve2);
                }

                if (model.thressgradeID > 0 && model.thressstate == 0 )
                {
                    UserDistributionLogInfo info = new UserDistributionLogInfo();
                    info.AddMoney = sharemoney * config.threegraderatio / 100;
                    info.Inve1 = 0;
                    info.UserId = model.thressgradeID;
                    info.State = 0;
                    info.PayType = 0;
                    info.PayDate = DateTime.Now;
                    info.PayState = 1;
                    info.AddDate = DateTime.Now;
                    info.Inve2 = "您的三级会员【" + username + "】" + buinessmsg + "，获得佣金:" + info.AddMoney + "元";

                    dal.AddMoney(info);

                    weixinsender.sendText(model.thressopenid, info.Inve2);
                }

            }
        }

        /// <summary>
        /// 用户扫描二维码时给商家发消息
        /// </summary>
        /// <param name="pusrid"></param>
        /// <param name="username"></param>
        public static void Notice2superior(int pusrid,string username)
        {
            ECustomerInfo user = new ECustomer().GetModel(pusrid);
            Hangjing.Weixin.SendMsg weixinsender = new Weixin.SendMsg(null);

            //给用户发文字消息
            if (user.PayPWDQuestion.Length > 0)
            {
                string body = CacheHelper.GetSetValue(76);
                body = body.Replace("{username}", username);
                weixinsender.sendText(user.PayPWDQuestion, body);
            }
        }
    }
}
