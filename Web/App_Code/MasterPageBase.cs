using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


using Hangjing.Common;
using Hangjing.Model;

/// <summary>
/// 前台界面基类 页面基类，
/// </summary>
public class MasterPageBase : System.Web.UI.Page
{
    /// <summary>
    /// BasePage类构造函数
    /// </summary>
    public MasterPageBase()
    {
        this.PreRender += new EventHandler(Page_PreRender);

        string isclose = SectionProxyData.GetSetValue(14).Trim();

        if (isclose == "1")
        {
            GameOver();
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
        string body = SectionProxyData.GetSetValue(15);
        System.Web.HttpContext.Current.Response.Clear();
        System.Web.HttpContext.Current.Response.Write("<!DOCTYPE html >\r\n<html>\r\n<head><title>温馨提示</title>");
        System.Web.HttpContext.Current.Response.Write("<div>" + body + "</div></body></html>");
        System.Web.HttpContext.Current.Response.End();
    }
}

