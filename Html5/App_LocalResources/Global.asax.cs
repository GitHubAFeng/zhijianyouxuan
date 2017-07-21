using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Hangjing.Common;
using tenpayApp;
using Hangjing.Weixin;

/// <summary>
///Global 的摘要说明
/// </summary>
public class Global: System.Web.HttpApplication
{
    void Application_Start(object sender, EventArgs e)
    {
        TenpayUtil wxpayconfig = new TenpayUtil();
    }

    void Application_End(object sender, EventArgs e)
    {


    }

    void Application_Error(object sender, EventArgs e)
    {
        //在出现未处理的错误时运行的代码
        Exception ex = Server.GetLastError();
        HttpException httpEx = ex as HttpException;
        if (httpEx != null)
        {
            //404错误
            if (httpEx.GetHttpCode() == 404)
            {
                HttpContext.Current.Response.Status = "404 Not Found";
                HttpContext.Current.Response.Redirect("~/404.aspx");
                return;
            }
        }

       // Hangjing.AppLog.AppLog.Error(ex);
        HJlog.toLog(ex.ToString());

        //string url = "";
        //try
        //{
        //    url = HttpContext.Current.Request.Url.ToString();
        //}
        //catch { }
        //Response.Redirect("~/Error.aspx");
    }

    void Session_Start(object sender, EventArgs e)
    {
        //在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e)
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }

    /// <summary>
    /// 重定向url地址
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        if (Application["StartupOK"] == null)
        {
            Application.Lock();
            if (Application["StartupOK"] == null)
            {
                //初始化
                StartupTools.Startup();
                Application["StartupOK"] = "OK";
            }
        }

        Hangjing.Common.UrlTools.RouteCurrentRequest();
    }
}
