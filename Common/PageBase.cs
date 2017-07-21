// PageBase.cs:页面基类
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-03-02
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Hangjing.Common
{
    /// <summary>
    /// EasyEat页面基类
    /// </summary>
    public class PageBase : System.Web.UI.Page
    {
        void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetActionStamp();
            }

            //将时间戳保存在hiddenfield中
            ClientScript.RegisterHiddenField("actionStamp", Session["actionStamp"].ToString());
        }

        /// <summary>
        /// 设置时间戳到session中 每次提交都会导致session变成新的时间戳
        /// </summary>
        private void SetActionStamp()
        {
            Session["actionStamp"] = Server.UrlEncode(DateTime.Now.ToString());
        }

        /// <summary>
        /// 系统配置信息
        /// </summary>
        protected internal SiteInfo config;

        public string pagename = "basic.aspx";// HjNetHelper.GetPageName();

        /// <summary>
        /// BasePage类构造函数
        /// </summary>
        public PageBase()
        {

            //校验用户是否可以访问
            if (!ValidateUserPermission())
            {
                return;
            }

            this.PreRender += new EventHandler(Page_PreRender);
        }

        /// <summary>    
        /// 取得值，指出网页是否经由重新整理动作回传 (PostBack)   
        /// </summary>   
        protected bool IsRefresh
        {
            get
            {
                //如果hiddenfield中的时间戳和session中的时间戳是一样的则表示是第一次提交页面
                if (HttpContext.Current.Request["actionStamp"] == null || (HttpContext.Current.Request["actionStamp"] as string == Session["actionStamp"] as string))
                {
                    SetActionStamp();
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// 校验用户是否可以访问此页面
        /// </summary>
        /// <returns></returns>
        private bool ValidateUserPermission()
        {
            //权限进行判断

            return true;
        }

        /// <summary>
        /// 显示无法访问页面
        /// </summary>
        /// <param name="hint"></param>
        /// <param name="mode"></param>
        public void GameOver(string hint, byte mode)
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head><title>");
            string title;
            string body;
            switch (mode)
            {
                case 1:
                    title = "网站已关闭";
                    body = "";
                    break;
                case 2:
                    title = "禁止访问";
                    body = hint;
                    break;
                default:
                    title = "提示";
                    body = hint;
                    break;
            }
            System.Web.HttpContext.Current.Response.Write(title);
            System.Web.HttpContext.Current.Response.Write(" - ");
            System.Web.HttpContext.Current.Response.Write(config.SiteName);
            System.Web.HttpContext.Current.Response.Write(" - Powered by IHangjing</title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            //System.Web.HttpContext.Current.Response.Write(config.KeyWords);
            System.Web.HttpContext.Current.Response.Write("<style type=\"text/css\"><!-- body { margin: 20px; font-family: Tahoma, Verdana; font-size: 14px; color: #333333; background-color: #FFFFFF; }a {color: #1F4881;text-decoration: none;}--></style></head><body><div style=\"border: #cccccc solid 1px; padding: 20px; width: 500px; margin:auto\" align=\"center\">");
            System.Web.HttpContext.Current.Response.Write(body);
            System.Web.HttpContext.Current.Response.Write("</div><br /><br /><br /><div style=\"border: 0px; padding: 0px; width: 500px; margin:auto\"><strong>当前服务器时间:</strong> ");
            System.Web.HttpContext.Current.Response.Write(Utils.GetDateTime());
            System.Web.HttpContext.Current.Response.Write("<br /><strong>当前页面</strong> ");
            System.Web.HttpContext.Current.Response.Write(pagename);

            System.Web.HttpContext.Current.Response.Write("</div></body></html>");
            System.Web.HttpContext.Current.Response.End();
        }
    }
}
