/*********************************************************************
 * CopyRight (c) 2009-2014 HangJing Teconology. All Rights Reserved.
 * FileName : 
 * Function : 
 * Created by jijunjian at 2014-05-24 15:18:46.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Newtonsoft.Json;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 网页授权相关，本类用来要微网站中通过网页授权方式，获取用户openid,和信息(本类只实现了获取openid)
    /// </summary>
    public class WebOAuth
    {
        protected HttpContext context;
        public string code = "";//用于获取openid

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="callbackurl"></param>
        public WebOAuth(HttpContext _context)
        {
            context = _context;
            WeiXinAccountInfo config = CacheHelper.GetWeiXinAccount();
            string callbackurl = config.revevar2 + "/Account/weixincallbak.aspx";

            string OAuthurl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + config.AppId + "&redirect_uri=" + context.Server.UrlEncode(callbackurl) + "&response_type=code&scope=snsapi_base&state=hj#wechat_redirect";
            HJlog.toLog(OAuthurl);

            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;

            context.Response.Redirect(OAuthurl);
        }

        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>
        /// 授权后返回的地址
        /// </summary>
        /// <param name="callbackurl"></param>
        public WebOAuth(HttpContext _context, string _code)
        {
            context = _context;
            code = _code;
        }

        /// <summary>
        /// 获取Openid,也会获取到access_token,可用于获取用户信息。
        /// </summary>
        /// <returns></returns>
        public string getOpenid()
        {
            string openid = "";

            WeiXinAccountInfo config = CacheHelper.GetWeiXinAccount();

            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?grant_type=authorization_code&appid=" + config.AppId + "&secret=" + config.AppSecret + "&code=" + code;
            HttpHelper objhttp = new HttpHelper();
            objhttp.isToLower = false;
            HttpItem objHttpItem = new HttpItem()
            {
                URL = url,
                Encoding = "utf-8",
                Method = "GET",
                UserAgent = context.Request.UserAgent,
            };
            string returnmsg = objhttp.GetHtml(objHttpItem);

 
            JObject jo = JObject.Parse(returnmsg);

            openid = jo["openid"].ToString().Replace("\"", "");
            Hangjing.Common.HJlog.toLog("getOpenid:returnmsg =" + returnmsg + " openid="+openid);

            return openid;
        }

        /// <summary>
        /// 获取用户基本信息(UnionID机制)
        /// </summary>
        /// <returns></returns>
        public static weixinUserInfo GetUserInfoByUnionID(string openid)
        {
            string ACCESSTOKE = new ACCESSTOKEN(null).getAccessTokern();
            string url = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + ACCESSTOKE + "&openid=" + openid + "&lang=zh_CN";
            HttpHelper objhttp = new HttpHelper();
            objhttp.isToLower = false;
            HttpItem objHttpItem = new HttpItem()
            {
                URL = url,
                Encoding = "utf-8",
                Method = "GET"
            };
            string returnmsg = objhttp.GetHtml(objHttpItem);

            Hangjing.AppLog.AppLog.Info("GetUserInfoByUnionID:returnmsg =" + returnmsg + " openid=" + openid);

            weixinUserInfo user = JsonConvert.DeserializeObject<weixinUserInfo>(returnmsg);
            return user;

        }

    }
}
