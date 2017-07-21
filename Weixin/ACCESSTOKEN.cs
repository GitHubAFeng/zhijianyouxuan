#region license
/*****************************************
*CopyRight (c) 2009-2013 HangJing Teconology. All Rights Reserved.
*Function :
*Created by jijunjian at 2013/9/16 21:58:13.
*E-Mail: jijunjian@ihangjing.com
*****************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Cache;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 获取凭证
    /// </summary>
    public class ACCESSTOKEN
    {
        protected HttpContext context;
        public ACCESSTOKEN(HttpContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// 获取token,调用次数有限，服务器要缓存
        /// </summary>
        /// <returns></returns>
        public string getAccessTokern()
        {
            string token = getcacheAccessToken();
            if (token != "")
            {
                return token;
            }


            WeiXinAccountInfo config = CacheHelper.GetWeiXinAccount();

            string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid="+config.AppId+"&secret="+config.AppSecret;
            HttpHelper objhttp = new HttpHelper();
            objhttp.isToLower = false;
            HttpItem objHttpItem = new HttpItem()
            {
                URL = url,
                Encoding = "utf-8",
                Method = "GET"
            };
            string returnmsg = objhttp.GetHtml(objHttpItem);

            Hangjing.Common.HJlog.toLog(" getAccessTokern=>returnmsg =" + returnmsg);
            JObject jo = JObject.Parse(returnmsg);
            token = jo["access_token"].ToString().Replace("\"","");

            cacheAccessToken(token);

            return token;
        }

        /// <summary>
        /// 缓存 accesstoken 1小时
        /// </summary>
        /// <param name="accesstoken"></param>
        private void cacheAccessToken(string accesstoken)
        {
            EasyEatCache.GetCacheService().AddObject("/accesstoken", accesstoken,600);
        }

        /// <summary>
        /// 获取缓存 accesstoken
        /// </summary>
        /// <param name="accesstoken"></param>
        private string getcacheAccessToken()
        {
            string cacheAccessTokern = (string)EasyEatCache.GetCacheService().RetrieveObject("/accesstoken");
            if (cacheAccessTokern == null)
            {
                cacheAccessTokern = "";
            }

            return cacheAccessTokern;
        }
    }
}
