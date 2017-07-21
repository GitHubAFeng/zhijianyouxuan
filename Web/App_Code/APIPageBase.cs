using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


using Hangjing.Common;
using Hangjing.Model;

/// <summary>
/// app 页面基类，
/// </summary>
public class APIPageBase : System.Web.UI.Page
{
    /// <summary>
    /// BasePage类构造函数
    /// </summary>
    public APIPageBase()
    {
        this.PreRender += new EventHandler(Page_PreRender);
        string key = "ihangjing.com";
        string apipageaccess = WebUtility.GetConfigKey("apipageaccess");
        if (apipageaccess == "1")
        {
            if (key != Context.Request.UserAgent)
            {
                GameOver();
            }
        }
    }

    void Page_PreRender(object sender, EventArgs e)
    {
        
    }

    /// <summary>
    /// 显示无法访问页面
    /// </summary>
    /// <param name="hint"></param>
    /// <param name="mode"></param>
    public void GameOver()
    {
        System.Web.HttpContext.Current.Response.Clear();
        System.Web.HttpContext.Current.Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head><title>访问地址出错啦</title>");
        System.Web.HttpContext.Current.Response.Write("<div>访问地址出错啦</div></body></html>");
        System.Web.HttpContext.Current.Response.End();
    }
}

