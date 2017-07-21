/*********************************************************************
 * CopyRight (c) 2009-2014 HangJing Teconology. All Rights Reserved.
 * FileName : 
 * Function : 
 * Created by jijunjian at 2015-03-19 10:29:28.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Cache;
using System.Web;
using Newtonsoft.Json.Linq;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 获取 jsapi_ticket（通过access_token）
    /// </summary>
    public class jsapi_ticket
    {
        /// <summary>
        /// 获取Ticket,调用次数有限，服务器要缓存
        /// </summary>
        /// <returns></returns>
        public string getTicket(HttpContext context)
        {
            string shopid = context.Request["id"];
            if (shopid == null || shopid.Length == 0)
            {
                shopid = "0";
            }

            string Ticket = getcacheTicket(shopid);
            if (Ticket != "")
            {
                return Ticket;
            }

            string accesston = new ACCESSTOKEN(context).getAccessTokern();

            string url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + accesston + "&type=jsapi";
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

            Hangjing.Common.HJlog.toLog(" getTicket=>returnmsg =" + returnmsg+ "\r\nshopid="+shopid);


            JObject jo = JObject.Parse(returnmsg);
            Ticket = jo["ticket"].ToString().Replace("\"", "");

            cacheTicket(Ticket, shopid);

            return Ticket;
        }

        /// <summary>
        /// 缓存 Ticket 1小时
        /// </summary>
        /// <param name="accesstoken"></param>
        private void cacheTicket(string accesstoken,string shopid)
        {
            EasyEatCache.GetCacheService().AddObject("/jsapi_ticket"+ shopid, accesstoken, 3600);
        }

        /// <summary>
        /// 获取缓存 Ticket
        /// </summary>
        /// <param name="accesstoken"></param>
        private string getcacheTicket(string shopid)
        {
            string cacheAccessTokern = (string)EasyEatCache.GetCacheService().RetrieveObject("/jsapi_ticket"+ shopid);
            if (cacheAccessTokern == null)
            {
                cacheAccessTokern = "";
            }

            return cacheAccessTokern;
        }
    }
}
