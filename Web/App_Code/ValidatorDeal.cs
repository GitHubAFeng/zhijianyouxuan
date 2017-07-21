using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
//zheng_jianfeng 
//2009-08-29 
//zjf@ihangjing.com
//验证控件提示信息气泡显示执行脚本方法类

/// <summary>
///ValidatorDeal 的摘要说明
/// </summary>
public static class ValidatorDeal
{
    private static string applicationPath = HttpContext.Current.Request.ApplicationPath;

    /// <summary>
    /// 加载js文件
    /// </summary>
    /// <param name="jsFilePath"></param>
    /// <param name="isInHead"></param>
    public static void IncludeJsFile(string jsFilePath, bool isInHead)
    {
        if (isInHead)
        {
            Page currentHandler = HttpContext.Current.CurrentHandler as Page;
            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            if (applicationPath != "/")
            {
                applicationPath = applicationPath + "/";
            }
            HtmlGenericControl child = new HtmlGenericControl("script");
            child.Attributes.Add("type", "text/javascript");
            child.Attributes.Add("language", "javascript");
            child.Attributes.Add("src", applicationPath + jsFilePath);
            currentHandler.Header.Controls.Add(child);
            
        }
        else
        {
            string str = string.Format("\n<script  type=\"text/javascript\" src=\"{0}\">\n</script>\n", jsFilePath);
            ((Page)HttpContext.Current.Handler).ClientScript.RegisterStartupScript(Type.GetType("System.String"), Guid.NewGuid().ToString(), str);
    
        }
    }



    /// <summary>
    /// 加载css样式表
    /// </summary>
    /// <param name="cssFilePath"></param>
    public static void IncludeCssFile(string cssFilePath)
    {
        HtmlGenericControl child = new HtmlGenericControl("link");
        child.Attributes.Add("href", cssFilePath);
        child.Attributes.Add("rel", "stylesheet");
        child.Attributes.Add("type", "text/css");
        Page handler = (Page)HttpContext.Current.Handler;
        handler.Header.Controls.Add(child);
    }

    /// <summary>
    /// 执行Js语句
    /// </summary>
    /// <param name="script"></param>
    /// <param name="isInHead"></param>
    public static void ExecuteJs(string script, bool isInHead)
    {
        string str = string.Format("\n<script  type=\"text/javascript\">\n{0}\n</script>\n", script);
        if (!isInHead)
        {
            ((Page)HttpContext.Current.Handler).ClientScript.RegisterStartupScript(Type.GetType("System.String"), Guid.NewGuid().ToString(), str);
        }
        else
        {
            HtmlGenericControl child = new HtmlGenericControl("script");
            child.Attributes.Add("type", "text/javascript");
            child.InnerHtml = "\n" + script + "\n";
            Page handler = (Page)HttpContext.Current.Handler;
            handler.Header.Controls.Add(child);
        }
    }

    public static void ExecuteJs(string script)
    {
        ExecuteJs(script, true);
    }


    public static void JsAlert(string msg, bool isInHead)
    {
        string m = string.Format("alert(\"{0}\");", msg.Replace(@"\", @"\\").Replace("\"", "\\\"").Replace("\n", @"\n"));
        ExecuteJs(m, isInHead);
    }

    public static void JsAlert(string msg)
    {
        JsAlert(msg, true);
    }

}
