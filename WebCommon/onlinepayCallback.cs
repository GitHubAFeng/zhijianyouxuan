using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Cache;
using System.Web.UI.WebControls;
using Hangjing.Common;
using Hangjing.Weixin;

namespace Hangjing.WebCommon
{
    /// <summary>
    /// 在线支付（充值）回调处理逻辑
    /// </summary>
    public class onlinepayCallback
    {
        protected string payorderid;
        protected decimal money;
        protected int paymodel;

        public onlinepayCallback(string _payorderid, decimal _money, int _paymodel)
        {
            payorderid = _payorderid;
            money = _money;
            paymodel = Convert.ToInt32(_paymodel);

            string sql = " update PayOrderLog set state = 2,PayTime='" + DateTime.Now + "' where  Batch = '" + payorderid + "'";
            Hangjing.DBUtility.SQLHelper.excutesql(sql);
        }

        /// <summary>
        /// 根据订单编号规则，支付方式，调用处理逻辑
        /// </summary>
        /// <returns></returns>
        public int Handle()
        {
            string firststr = payorderid.Substring(0, 1);
            switch (firststr)
            {
                case "r":
                    rechargeHandle();
                    break;
                case "d": //分销商
                    {

                        string orderid = payorderid.Substring(0, payorderid.Length - 3);
                        string[] data = orderid.Split('_');

                        DistributorNotice dnotice = new DistributorNotice();
                        dnotice.becomeToVIP(Convert.ToInt32(data[1]), money);

                        break;
                    }
                case "e": //跑腿
                    {
                        //订单支付
                        //获取订单列表(存在多个订单同时提交的情况)
                        IList<ExpressOrderInfo> list = new List<ExpressOrderInfo>();
                        ExpressOrder dal = new ExpressOrder();
                        string orderid = payorderid.Substring(0, payorderid.Length - 3);

                        list = dal.GetList(100, 1, "Orderid='" + orderid + "'", "dataid", 1);

                        ///更新订单状态
                        foreach (ExpressOrderInfo info in list)
                        {
                            info.OrderID = info.OrderID;
                            info.PayMode = paymodel;
                            info.paymoney = money;
                            info.paystate = 1;
                            info.paytime = DateTime.Now;
                            info.Currentprice = 0;
                            dal.UpdatePayState(info);
                        }
                    }
                    break;

                default:
                    payorderHandle();
                    break;
            }


            return 1;
        }

        /// <summary>
        /// 充值逻辑处理。
        /// </summary>
        /// <returns></returns>
        protected int rechargeHandle()
        {
            string orderid = payorderid.Substring(0, payorderid.Length - 3);
            UserAddMoneyLog udal = new UserAddMoneyLog();
            UserAddMoneyLogInfo uinfo = udal.GetModel(orderid);
            uinfo.Inve2 = orderid;
            uinfo.AddMoney = money;
            if (udal.CallBlackByAli(uinfo) > 0)
            {            //发短信
                //SendMsg.recharegeSuccess(uinfo.UserTell, money.ToString());

            }
            return 1;
        }

        /// <summary>
        /// 支付宝或者网银支付逻辑处理。
        /// </summary>
        /// <returns></returns>
        protected int payorderHandle()
        {
            string orderid = payorderid.Substring(0, payorderid.Length - 3);

            HJlog.toLog("orderid=" + orderid);

            //订单支付
            //获取订单列表(存在多个订单同时提交的情况)
            IList<CustorderInfo> list = new List<CustorderInfo>();
            Custorder dal = new Custorder();
            ETogoLocal etdal = new ETogoLocal();

            Points pdal = new Points();
            PointsInfo pinfo = new PointsInfo();

            ETogoLocalInfo etinfo = new ETogoLocalInfo();

            list = dal.GetList(100, 1, "Orderid='" + orderid + "'", "Unid", 1);

            ///更新订单状态
            foreach (CustorderInfo info in list)
            {
                //避免多次通知，需要判断一下可能之前已经更新订单状态
                if (info.paystate == 0)
                {
                    info.orderid = info.orderid;
                    info.paymode = paymodel;
                    info.paymoney = money;
                    info.paystate = 1;
                    info.paytime = DateTime.Now;

                    string updatefoodsql = "";
                    if (dal.UpdatePayState(info) > 0)
                    {
                        HJlog.toLog("info.orderid=" + info.orderid);
                        FeieYunPrinter p = new FeieYunPrinter(info.orderid);
                        apiResultInfo rs = p.PrintOrder();

                        Foodlist fal = new Foodlist();
                        IList<FoodlistInfo> foodlist = fal.GetAllByOrderID(info.orderid);
                        foreach (FoodlistInfo item in foodlist)
                        {
                            int pnum = item.FCounts;
                            int pid = item.FoodUnid;

                            updatefoodsql += "update Foodinfo set Remains =(SELECT COUNT(1) FROM dbo.Foodlist WHERE  FoodUnid=" + pid + " AND datediff(month,adddate,getdate())=0)+" + pnum + " where unid =" + pid + ";";
                        }
                        Hangjing.DBUtility.SQLHelper.ExecuteNonQuery(System.Data.CommandType.Text, updatefoodsql, null);

                    }

                    pinfo = pdal.GetModel(info.TogoId);
                    etinfo = etdal.GetInfoById(info.TogoId.ToString());

                    new OrderStep().Add(new OrderStepInfo()
                    {
                        stepcode = 10,
                        title = "支付成功",
                        subtitle = "",
                        addtime = DateTime.Now,
                        orderid = orderid,
                        revevar1 = "",
                        revevar2 = "",
                        revevar3 = ""
                    });


                    //通知商家
                    NoticeHelper notice = new NoticeHelper(null, info.TogoId.ToString());
                    if (pinfo.RcvType == 1)
                    {
                        new OrderStep().Add(new OrderStepInfo()
                        {
                            stepcode = 20,
                            title = "商家已经接单",
                            subtitle = "",
                            addtime = DateTime.Now,
                            orderid = orderid,
                            revevar1 = "",
                            revevar2 = "",
                            revevar3 = ""
                        });

                        Hangjing.DBUtility.SQLHelper.excutesql("UPDATE dbo.Custorder SET IsShopSet = 0 ,ReveDate1 = GETDATE() WHERE orderid = '"+orderid+"'");
                        info.IsShopSet = 1;

                        //自动调度订单 通知骑士
                        if (pinfo.sentorg == "0" && info.IsShopSet == 1)
                        {
                            IList<ROrderinfo> my_list = new List<ROrderinfo>();
                            ROrderinfo mm = new ROrderinfo();
                            mm.latlng = info.ReveVar2;
                            mm.Orderid = info.orderid;
                            mm.cityid = info.cityid;
                            mm.sentorg = pinfo.sentorg;
                            mm.paystate = 1;
                            mm.paymode = 1;
                            my_list.Add(mm);
                            dal.AutoDispatch(my_list);
                        }

                        notice.sendNotice2Shop(pinfo.Unid, "您有新订单，已经自动接单", orderid);
                    }
                    else
                    {
                        notice.send2ShopByShopid();
                    }


                  


                }
            }


            return 1;
        }



    }
}