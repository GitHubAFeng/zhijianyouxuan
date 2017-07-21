using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Text;

// UrlTools.cs
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-04-01
// 伪静态处理类
   
// public class HttpModule : System.Web.IHttpModule
// <httpModules>
//     <add type="Hangjing.Common.HttpModule,Hangjing.Common" name="HttpModule" />
// </httpModules>

namespace Hangjing.Common
{
    public static class UrlTools
    {
        /// <summary>
        /// 重定向请求
        /// 如果传入的是ashx后缀的请求页面则需要重定向到正确的页面中
        /// 一般格式 
        /// 1.点餐商家：http://www.dianyifen.com/togo.show-1.ashx -> http://www.dianyifen.com/togo.aspx?id=1
        /// 2.订餐商家：http://www.dianyifen.com/order.show-1.ashx -> http://www.dianyifen.com/order/showpub.aspx?id=1
        /// 3.在线超市：http://www.dianyifen.com/market.show-1.ashx -> http://www.dianyifen.com/market/showmarket.aspx?id=1
        /// </summary>
        public static void RouteCurrentRequest()
        {
            //提取物理地址 togo.show-1
            //string pageName = Path.GetFileNameWithoutExtension(HttpContext.Current.Request.PhysicalPath);
            
            //获取后缀
            string ext = Path.GetExtension(HttpContext.Current.Request.PhysicalPath).ToLowerInvariant();

            if (ext.Length > 0)
            {
                ext = ext.Substring(1);
            }

            //非ashx页面不予处理 如 .aspx .jpg .css .js .html
            if (ext != "ashx")
            {
                return;
            }

            //提取物理地址 90
            string pageName = Path.GetFileNameWithoutExtension(HttpContext.Current.Request.PhysicalPath);

            // Extract the current namespace, if any
            //提取当前请求的板块(在线点餐、在线订餐、在线超市、积分对换) 定义为命名空间
            string queryString = HttpContext.Current.Request.Url.Query.Replace("?", "&");
            string name_space = GetCurrentNamespace();

            //有命名空间
            if (!string.IsNullOrEmpty(name_space))
            {
                if (name_space.Equals("shop"))
                {
                    //获取Id show-1
                    pageName = pageName.Substring(name_space.Length + 1);
                    string id = pageName.Replace("show-", "");//show-1
                    //-> show-1->BenefitDetail.aspx?id=1
                    HttpContext.Current.RewritePath("shop.aspx?id=" + id + "" + queryString);
                }
                if (name_space.Equals("Benefit2"))
                {
                    //获取Id show-1
                    pageName = pageName.Substring(name_space.Length + 1);
                    string id = pageName.Replace("show-", "");//show-1
                    //-> show-1->BenefitDetail.aspx?id=1
                    HttpContext.Current.RewritePath("ShowBenefitDetail.aspx?id=" + id + "" + queryString);
                }
                else if (name_space.Equals("BenefitClass"))
                {
                    //获取Id show-1
                    pageName = pageName.Substring(name_space.Length + 1);
                    string id = pageName.Replace("show-", "");//show-1
                    //-> show-1->BenefitClassDetail.aspx?id=1
                    HttpContext.Current.RewritePath("BenefitClassDetail.aspx?id=" + id + "");
                }
                else if (name_space.Equals("Coupons"))
                {
                    //获取Id show-1
                    pageName = pageName.Substring(name_space.Length + 1);
                    string id = pageName.Replace("show-", "");//show-1
                    //-> show-1->togo.aspx?id=1
                    HttpContext.Current.RewritePath("CouponsDetail.aspx?id=" + id + "");
                }
            }
            //else nothing todo  now
        }

        /// <summary>
        /// 提取请求的页面的命名空间 如 <i>Namespace.togo.aspx</i>.
        /// </summary>
        /// <returns>返回命名空间或者null</returns>
        public static string GetCurrentNamespace()
        {
            string filename = Path.GetFileNameWithoutExtension(HttpContext.Current.Request.Path); //如togo.show-1 order.show-1

            string[] fields = filename.Split('.');

            if (fields.Length != 1 && fields.Length != 2)
            {
                return null;
            }
            if (fields.Length == 1)
            {
                return ""; // 只有文件名 无命名空间
            }
            else
            {
                return fields[0];
            }
        }

        /// <summary>
        /// Executes URL-encoding, avoiding to use '+' for spaces.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The encoded string.</returns>
        public static string UrlEncode(string input)
        {
            return HttpContext.Current.Server.UrlEncode(input).Replace("+", "%20");
        }

        /// <summary>
        /// Executes URL-decoding, replacing spaces as processed by UrlEncode.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The decoded string.</returns>
        public static string UrlDecode(string input)
        {
            return HttpContext.Current.Server.UrlDecode(input.Replace("%20", " "));
        }
    }
}
