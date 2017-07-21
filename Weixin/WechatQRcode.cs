using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 微信二维码
    /// </summary>
    public class WechatQRcode
    {
        string ticket = "";
        /// <summary>
        /// 二维码地址
        /// </summary>
        public string qrcodeurl = "";
        public WechatQRcode()
        {

        }

        /// <summary>
        /// 创建二维码ticket
        /// </summary>
        public void getTicket(int userid)
        {
            string access_token = new ACCESSTOKEN(null).getAccessTokern();

            string url = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + access_token;
            HttpHelper objhttp = new HttpHelper();
            objhttp.isToLower = false;
            HttpItem objHttpItem = new HttpItem()
            {
                URL = url,
                Encoding = "utf-8",
                Method = "POST",
                Postdata = "{\"action_name\":\"QR_LIMIT_STR_SCENE\",\"action_info\":{\"scene\":{\"scene_str\":\"" + userid + "\"}}}",
            };
            string returnmsg = objhttp.GetHtml(objHttpItem);

            Hangjing.Common.HJlog.toLog(" getTicket=>returnmsg =" + returnmsg);

            JObject jo = JObject.Parse(returnmsg);
            ticket = jo["ticket"].ToString().Replace("\"", "");
            qrcodeurl = jo["url"].ToString().Replace("\"", "");
        }
    }
}
