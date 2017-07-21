using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using System.Reflection;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 处理用户关注事件，取消等事件
    /// </summary>
    public class EventHandler : BaseHandler
    {
        protected Event model = null;
        public EventHandler(BaseNotice _notice)
            : base(_notice)
        { }

        /// <summary>
        /// 处理用户关注事件,如关注后发配置信息
        /// </summary>
        /// <returns></returns>
        public override string HandleNotice(HttpContext context)
        {
            string backmsg = "";
            model = (Event)base.notice;



            switch (model.EventType)
            {
                case "LOCATION":
                    {
                        LOCATION();
                        backmsg = "";
                    }
                    break;

                case "SCAN"://已经关注的，扫描代参数的二维码 ，EventKey 为上线用户编号
                    {
                        subscribeByUserQRcode(Convert.ToInt32(model.EventKey), model.FromUserName);
                        backmsg = defautset();

                    }
                    break;

                default:
                    {

                        if (model.EventKey.StartsWith("qrscene_")) //用户扫描代参数的二维码关注
                        {
                            subscribeByUserQRcode(Convert.ToInt32(model.EventKey.Replace("qrscene_", "")), model.FromUserName);
                            backmsg = defautset();
                        }
                        else
                        {
                            MethodInfo methodInfo = this.GetType().GetMethod(model.EventKey, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                            if (methodInfo != null)
                            {
                                //调用方法
                                backmsg = methodInfo.Invoke(this, null).ToString(); 
                            }
                            else
                            {
                                backmsg = defautset();
                            }
                        }

                    }

                    break;
            }

            return backmsg.ToString();
        }

        /// <summary>
        /// 用户通过扫描带参数的二维码。
        /// </summary>
        /// <param name="puserid"></param>
        /// <param name="openid"></param>
        public void subscribeByUserQRcode(int puserid, string openid)
        {
            weixinUserInfo user = WebOAuth.GetUserInfoByUnionID(openid);

            subscribeByUserQRcodeRecordInfo model = new subscribeByUserQRcodeRecordInfo();
            model.openid = openid;
            model.nickname = user.nickname;
            model.puserid = puserid;
            model.addtime = DateTime.Now;
            model.reveint1 = 0;
            model.reveint2 = 0;
            model.revevar1 = "";
            model.revevar2 = "";
            new subscribeByUserQRcodeRecord().Add(model);

            DistributorNotice.Notice2superior(puserid, user.nickname);

        }

        /// <summary>
        /// 返回今日订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string cjfan_4()
        {
            StringBuilder backmsg = new StringBuilder();

            backmsg.Append("<xml>");
            backmsg.Append("<ToUserName><![CDATA[" + model.FromUserName + "]]></ToUserName>");
            backmsg.Append("<FromUserName><![CDATA[" + model.ToUserName + "]]></FromUserName>");
            backmsg.Append("<CreateTime>" + DateTime.Now.Ticks + "</CreateTime>");

            string Content = "";
            Custorder dal = new Custorder();

            StringBuilder ordermsg = new StringBuilder("");

            IList<CustorderInfo> orderlist = dal.GetList(5, 1, " tempcode='" + model.FromUserName + "' and OrderDateTime > '" + DateTime.Now.ToShortDateString() + "' ", "Unid", 1);

            if (orderlist.Count > 0)
            {
                ordermsg.Append("今日订单");

                foreach (var item in orderlist)
                {
                    ordermsg.Append("\r\n订单号：");
                    ordermsg.Append("\r\n" + item.orderid);
                    ordermsg.Append("\r\n商家名称：" + item.TogoName);
                    ordermsg.Append("\r\n订单金额：" + item.OrderSums.ToString());
                    ordermsg.Append("\r\n配送费：" + item.SendFee.ToString());
                    ordermsg.Append("\r\n下单时间：" + item.OrderDateTime.ToString());
                    ordermsg.Append("\r\n订单状态：" + ConfigHelper.TurnOrderState(item.OrderStatus));

                    IList<FoodlistInfo> foodlist = new Foodlist().GetAllByOrderID(item.orderid);
                    foreach (var food in foodlist)
                    {
                        ordermsg.Append("\r\n" + food.FoodName + "(" + food.FoodPrice + "x" + food.FCounts + ")");
                    }

                    ordermsg.Append("\r\n==================");
                }

            }
            else
            {
                ordermsg.Append("您今天还没有订餐点哦");
                ordermsg.Append("\r\n==================");
            }
            Content = ordermsg.ToString();
            backmsg.Append("<Content><![CDATA[" + Content.ToString() + "]]></Content>");
            backmsg.Append(" <MsgType><![CDATA[text]]></MsgType>");
            backmsg.Append(" </xml> ");

            return backmsg.ToString();

        }

        /// <summary>
        /// 自定定位(进入html5界面定位)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string autolocate()
        {
            StringBuilder backmsg = new StringBuilder();

            string url = domain + "/index.aspx?openid=" + model.FromUserName;

            backmsg.Append("<xml>");
            backmsg.Append("<ToUserName><![CDATA[" + model.FromUserName + "]]></ToUserName>");
            backmsg.Append("<FromUserName><![CDATA[" + model.ToUserName + "]]></FromUserName>");
            backmsg.Append("<CreateTime>" + DateTime.Now.Ticks + "</CreateTime>");
            backmsg.Append(" <MsgType><![CDATA[news]]></MsgType>");
            backmsg.Append(" <ArticleCount>" + (1) + "</ArticleCount>");
            backmsg.Append(" <Articles>");
            backmsg.Append(" <item>");
            backmsg.Append(" <Title><![CDATA[自定定位]]></Title> ");
            backmsg.Append(" <Description><![CDATA[]]></Description>");
            backmsg.Append(" <PicUrl><![CDATA[" + domain + "/images/wm_img.png?v=11]]></PicUrl>");
            backmsg.Append(" <Url><![CDATA[" + url + "]]></Url>");
            backmsg.Append(" </item>");
            backmsg.Append(" </Articles>");
            backmsg.Append(" </xml> ");

            return backmsg.ToString();
        }

        /// <summary>
        /// 获取已登陆用户余额
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string cjfan_5()
        {
            

            StringBuilder ordermsg = new StringBuilder("");

            ECustomer dal = new ECustomer();
            IList<ECustomerInfo> users = dal.GetList(1, 1, "PayPWDQuestion = '" + model.FromUserName + "'", "dataid", 1);
            if (users.Count == 0)
            {
                ordermsg.Append("尚未登陆，请先登录！");
            }
            else
            {
                ordermsg.Append(users[0].Name);
                ordermsg.Append("\r\n当前余额：" + users[0].Usermoney + "元");
            }

            StringBuilder backmsg = new StringBuilder();
            backmsg.Append("<xml>");
            backmsg.Append("<ToUserName><![CDATA[" + model.FromUserName + "]]></ToUserName>");
            backmsg.Append("<FromUserName><![CDATA[" + model.ToUserName + "]]></FromUserName>");
            backmsg.Append("<CreateTime>" + DateTime.Now.Ticks + "</CreateTime>");

            backmsg.Append("<Content><![CDATA[" + ordermsg.ToString() + "]]></Content>");
            backmsg.Append(" <MsgType><![CDATA[text]]></MsgType>");
            backmsg.Append(" </xml> ");

            Hangjing.Common.HJlog.toLog(backmsg);
            return backmsg.ToString();
        }

        /// <summary>
        /// 获取积分
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string cjfan_6()
        {
            

            StringBuilder ordermsg = new StringBuilder("");

            ECustomer dal = new ECustomer();
            IList<ECustomerInfo> users = dal.GetList(1, 1, "PayPWDQuestion = '" + model.FromUserName + "'", "dataid", 1);

            if (users.Count == 0)
            {
                ordermsg.Append("尚未登陆，请先登录！");
            }
            else
            {
                ordermsg.Append(users[0].Name);
                ordermsg.Append("\r\n当前可用积分：" + users[0].Point + "分");
            }

            StringBuilder backmsg = new StringBuilder();
            backmsg.Append("<xml>");
            backmsg.Append("<ToUserName><![CDATA[" + model.FromUserName + "]]></ToUserName>");
            backmsg.Append("<FromUserName><![CDATA[" + model.ToUserName + "]]></FromUserName>");
            backmsg.Append("<CreateTime>" + DateTime.Now.Ticks + "</CreateTime>");

            backmsg.Append("<Content><![CDATA[" + ordermsg.ToString() + "]]></Content>");
            backmsg.Append(" <MsgType><![CDATA[text]]></MsgType>");
            backmsg.Append(" </xml> ");

            Hangjing.Common.HJlog.toLog(backmsg);
            return backmsg.ToString();
        }

        /// <summary>
        /// 默认处理方式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string defautset()
        {
           

            StringBuilder backmsg = new StringBuilder();
            backmsg.Append("<xml>");
            backmsg.Append("<ToUserName><![CDATA[" + model.FromUserName + "]]></ToUserName>");
            backmsg.Append("<FromUserName><![CDATA[" + model.ToUserName + "]]></FromUserName>");
            backmsg.Append("<CreateTime>" + DateTime.Now.Ticks + "</CreateTime>");
            string Content = ConfigHelper.GetConfigBackMsg();

            backmsg.Append("<Content><![CDATA[" + Content.ToString() + "]]></Content>");
            backmsg.Append(" <MsgType><![CDATA[text]]></MsgType>");
            backmsg.Append(" </xml> ");

            return backmsg.ToString();
        }

        /// <summary>
        /// 获取用户地理位置   http://mp.weixin.qq.com/wiki/index.php?title=%E8%8E%B7%E5%8F%96%E7%94%A8%E6%88%B7%E5%9C%B0%E7%90%86%E4%BD%8D%E7%BD%AE
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void LOCATION()
        {
            WeixinUserLocation dat = new WeixinUserLocation();

            string[] bdlatlng = Hangjing.Common.HjNetHelper.bd_encrypt(Convert.ToDouble(model.Latitude), Convert.ToDouble(model.Longitude));
            model.Latitude = bdlatlng[0];
            model.Longitude = bdlatlng[1];

            dat.Add(model.FromUserName, model.Latitude, model.Longitude);
        }

        /// <summary>
        /// user center
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string userCenter()
        {
            WeiXinAccountInfo config = CacheHelper.GetWeiXinAccount();
            string domain = config.revevar2;

            //这里的图片用广告位的
            AdTableInfo model_ad = new AdTable().GetModel(3);
            string imgurl = model_ad.AdImageAdrees.Replace("~", CacheHelper.GetSetValue(1));

            StringBuilder backmsgx = new StringBuilder();
            backmsgx.Append("<xml>");
            backmsgx.Append("<ToUserName><![CDATA[" + model.FromUserName + "]]></ToUserName>");
            backmsgx.Append("<FromUserName><![CDATA[" + model.ToUserName + "]]></FromUserName>");
            backmsgx.Append("<CreateTime>" + DateTime.Now.Ticks + "</CreateTime>");
            backmsgx.Append(" <MsgType><![CDATA[news]]></MsgType>");
            backmsgx.Append(" <ArticleCount>" + 1 + "</ArticleCount>");
            backmsgx.Append(" <Articles>");

            backmsgx.Append(" <item>");
            backmsgx.Append("<Title><![CDATA[点击进入个人中心]]></Title> ");
            backmsgx.Append(" <Description><![CDATA[]]></Description>");
            backmsgx.Append(" <PicUrl><![CDATA[" + imgurl + "]]></PicUrl>");
            backmsgx.Append(" <Url><![CDATA[" + domain + "/myinfolist.aspx?openid=" + model.FromUserName + "]]></Url>");
            backmsgx.Append(" </item>");


            backmsgx.Append(" </Articles>");
            backmsgx.Append(" </xml> ");

            return backmsgx.ToString();
        }


    }

}
