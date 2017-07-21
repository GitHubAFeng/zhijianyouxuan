using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System.Web;
using System.Reflection;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 文本信息处理类
    /// </summary>
    public class TextHandler : BaseHandler
    {
        HttpContext context = null;
        text model = null;
        public TextHandler(BaseNotice _notice)
            : base(_notice)
        {

        }

        /// <summary>
        /// 文本信息处理方法,如果文本信息 = d,返回今天订单
        /// </summary>
        /// <returns></returns>
        public override string HandleNotice(HttpContext _context)
        {
            StringBuilder backmsg = new StringBuilder();
            model = (text)base.notice;
            context = _context;


            /// <summary>
            /// 保存要处理的文字，及对应函数
            /// </summary>
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("订单", "showOrder");
            data.Add("d", "showOrder");

            data.Add("商家", "shopLoing");
            data.Add("s", "shopLoing");

            data.Add("zd", "autolocate");
            data.Add("自动", "autolocate");

            string posttext = model.Content.ToLower().Trim();
            string action = "defaultReback";
            foreach (var item in data)
            {
                if (item.Key == posttext)
                {
                    action = item.Value;
                    break;
                }
            }

            //反射调用方法
            MethodInfo methodInfo = this.GetType().GetMethod(action, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);

            string rebackmsg = methodInfo.Invoke(this, null).ToString();
            HJlog.toLog(rebackmsg + " " + action);
            return rebackmsg;
        }

        /// <summary>
        /// 显示订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string showOrder()
        {
            string Content = "";
            Custorder dal = new Custorder();
            StringBuilder ordermsg = new StringBuilder("");

            IList<CustorderInfo> orderlist = dal.GetList(2, 1, " tempcode='" + model.FromUserName + "' and OrderDateTime > '" + DateTime.Now.ToShortDateString() + "' ", "Unid", 1);

            if (orderlist.Count > 0)
            {
                ordermsg.Append("今日订单");

                foreach (var item in orderlist)
                {
                    ordermsg.Append("\r\n订单号：");
                    ordermsg.Append("\r\n" + item.orderid);
                    ordermsg.Append("\r\n商家名称：" + item.TogoName);
                    ordermsg.Append("\r\n订单金额：" + item.OrderSums.ToString());
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


            StringBuilder backmsg = new StringBuilder();
            backmsg.Append("<xml>");
            backmsg.Append("<ToUserName><![CDATA[" + model.FromUserName + "]]></ToUserName>");
            backmsg.Append("<FromUserName><![CDATA[" + model.ToUserName + "]]></FromUserName>");
            backmsg.Append("<CreateTime>" + DateTime.Now.Ticks + "</CreateTime>");
            backmsg.Append("<Content><![CDATA[" + Content.ToString() + "]]></Content>");
            backmsg.Append(" <MsgType><![CDATA[text]]></MsgType>");
            backmsg.Append(" </xml> ");

            return backmsg.ToString();
        }

        /// <summary>
        /// 商家登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string shopLoing()
        {
            string domain = CacheHelper.GetWeiXinAccount().revevar2;

            StringBuilder backmsgx = new StringBuilder();
            backmsgx.Append("<xml>");
            backmsgx.Append("<ToUserName><![CDATA[" + model.FromUserName + "]]></ToUserName>");
            backmsgx.Append("<FromUserName><![CDATA[" + model.ToUserName + "]]></FromUserName>");
            backmsgx.Append("<CreateTime>" + DateTime.Now.Ticks + "</CreateTime>");
            backmsgx.Append(" <MsgType><![CDATA[news]]></MsgType>");
            backmsgx.Append(" <ArticleCount>" + 1 + "</ArticleCount>");
            backmsgx.Append(" <Articles>");

            backmsgx.Append(" <item>");
            backmsgx.Append("<Title><![CDATA[点击登录商家帐号]]></Title> ");
            backmsgx.Append(" <Description><![CDATA[]]></Description>");
            backmsgx.Append(" <PicUrl><![CDATA[" + domain + "/images/wm_img.png?v=1.1]]></PicUrl>");
            backmsgx.Append(" <Url><![CDATA[" + domain + "/shoplogin.aspx?openid=" + model.FromUserName + "]]></Url>");
            backmsgx.Append(" </item>");

            backmsgx.Append(" </Articles>");
            backmsgx.Append(" </xml> ");

            return backmsgx.ToString();
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
        /// 默认回复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string defaultReback()
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
    }
}
