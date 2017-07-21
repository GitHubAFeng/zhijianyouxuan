#region license
/*****************************************
*CopyRight (c) 2009-2013 HangJing Teconology. All Rights Reserved.
*Function :
*Created by jijunjian at 2013/9/16 22:21:13.
*E-Mail: jijunjian@ihangjing.com
*****************************************/
#endregion
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 主要发送消息
    /// </summary>
    public class SendMsg
    {
        protected HttpContext context;
        protected string accesstoken = "";

        public SendMsg(HttpContext _context)
        {
            context = _context;

            accesstoken = new ACCESSTOKEN(context).getAccessTokern();
        }

        /// <summary>
        /// 给用户发送文本消息
        /// </summary>
        public void sendText(string openid, string textmsg)
        {
            string postdata = "{";
            postdata += "\"touser\":\"" + openid + "\",";
            postdata += "\"msgtype\":\"text\",";
            postdata += "\"text\":{\"content\":\"" + textmsg + "\"}";

            postdata += "}";

            send2User(postdata);
        }

        /// <summary>
        /// 给用户发送模版消息(新订单通知模版)
        /// </summary>
        public void sendTemplateMsg(string openid, string delivermsg)
        {
            string domain = CacheHelper.GetWeiXinAccount().revevar2;
            string url = domain + "/myorderlist.aspx?openid=" + openid;

            string postdata = "{";
            postdata += "\"touser\":\"" + openid + "\",";
            postdata += "\"template_id\":\"eP9F2TbOPaCdtpYXbKGeeNcOcLQkEXFa5ws0UU4dtCc\",";
            postdata += "\"url\":\"" + url + "\",";
            postdata += "\"topcolor\":\"#FF0000\",";

            postdata += "\"data\":{";
            postdata += "\"first\":{\"value\":\"您的订单已被外卖郎骑士接收,正在火速的为您进行配送\",\"color\":\"#173177\"},";
            postdata += "\"keyword1\":{\"value\":\"点击查看详情\",\"color\":\"#173177\"},";
            postdata += "\"keyword2\":{\"value\":\"" + delivermsg + "\",\"color\":\"#173177\"},";
            postdata += "\"remark\":{\"value\":\"请耐心等待商品送达，祝您用餐愉快\",\"color\":\"#173177\"}";

            postdata += "}}";

            sendTemplate2User(postdata);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="jsonmsg"></param>
        private void send2User(string jsonmsg)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + accesstoken;
            HttpHelper objhttp = new HttpHelper();
            objhttp.isToLower = false;
            HttpItem objHttpItem = new HttpItem()
            {
                URL = url,
                Encoding = "utf-8",
                Method = "POST",
                UserAgent = context.Request.UserAgent,
                Postdata = jsonmsg,
            };
            string returnmsg = objhttp.GetHtml(objHttpItem);

            Hangjing.Common.HJlog.toLog("returnmsg=" + returnmsg + "  Postdata=" + jsonmsg + " url=" + url);
        }

        /// <summary>
        /// 发送模版消息
        /// </summary>
        /// <param name="jsonmsg"></param>
        private void sendTemplate2User(string jsonmsg)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + accesstoken;
            HttpHelper objhttp = new HttpHelper();
            objhttp.isToLower = false;
            HttpItem objHttpItem = new HttpItem()
            {
                URL = url,
                Encoding = "utf-8",
                Method = "POST",
                UserAgent = context.Request.UserAgent,
                Postdata = jsonmsg,
            };
            string returnmsg = objhttp.GetHtml(objHttpItem);

            Hangjing.Common.HJlog.toLog("sendTemplate2User：returnmsg=" + returnmsg + "  Postdata=" + jsonmsg + " url=" + url);
        }


        /// <summary>
        /// 给用户发送订单配送的消息(配送模版消息)
        /// textmsg = 小李（138xxxxxx）
        /// </summary>
        public void sendTemplateMsg(string orderid)
        {
            CustorderInfo model = new Custorder().GetOpenidAndDeliver(orderid);
            if (model != null && model.P2Sign.Length > 10)
            {
                sendTemplateMsg(model.P2Sign, model.Sender + "(" + model.CallPhoneNo + ")");
            }

        }
    }
}
