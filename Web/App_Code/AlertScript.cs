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
using System.Web.UI;
using System.Collections;
using System.Collections.Specialized;

/// <summary>
///AlertScript 的摘要说明
/// </summary>
public static class AlertScript
{
    /// <summary>
    /// JavaScript方法弹出消息对话框
    /// </summary>
    /// <param name="msg">要弹出的消息内容</param>
    /// <param name="page">当前页面，可以直接写this或者Page</param>
    public static void Alert(string msg, Page page)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), null, "alert('" + msg + "');", true);
    }

    /// <summary>
    /// 向页面注册JavaScript脚本，可以在页面上注册任何你想要的js脚本。
    /// </summary>
    /// <param name="scriptcontent">js脚本的内容或者js方法的名称。</param>
    /// <param name="page">前页面，可以直接写this或者Page</param>
    public static void RegisterScript(string scriptcontent, Page page)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), null, "alert('弹出框二')", true);
    }

    /// <summary>
    /// 弹出消息对话框，关闭对话框后跳到指定的URL链接页面
    /// </summary>
    /// <param name="msg">要弹出的消息内容</param>
    /// <param name="pageurl">要跳转的页面链接，站内链接写相对路径，站外的链接加上"http://"</param>
    /// <param name="page">当前页面，可以直接写this或者Page</param>
    public static void AlertAndGotoUrl(string msg, string pageurl, Page page)
    {
        string strScript = "alert('" + msg + "');window.location.href='" + pageurl + "'";
        page.ClientScript.RegisterStartupScript(page.GetType(), null, strScript, true);
    }

    /// <summary>
    /// ajax方式弹出消息对话框，主要在UpdatePanel控件和用户控件中使用，在页面中也可以使用
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="controlId"></param>
    public static void AjaxAlert(string msg, Control controlId)
    {
        ScriptManager.RegisterStartupScript(controlId, controlId.GetType(), null, "alert('" + msg + "');", true);
    }

    /// <summary>
    /// 利用Ajax的方式向控件或页面注册JavaScript脚本主要在UpdatePanel控件和用户控件中使用，在页面中也可以使用
    /// </summary>
    /// <param name="scriptcontent">js脚本的内容</param>
    /// <param name="controlId">控件的ID名称，用户控件和页面中参数为:this,UpdatePanel控件中参数为当前UpdatePanel的ID</param>
    public static void AjaxRegisterScript(string scriptcontent, Control controlId)
    {
        ScriptManager.RegisterStartupScript(controlId, controlId.GetType(), null, scriptcontent, true);
    }

    /// <summary>
    /// 利用Ajax的方式弹出消息对话框，跳转到指定的URL链接页面，主要在UpdatePanel控件和用户控件中使用，在页面可以使用。
    /// </summary>
    /// <param name="msg">要弹出的消息内容</param>
    /// <param name="pageurl">要跳转的URL</param>
    /// <param name="controlId">控件的ID名称</param>
    public static void AjaxAlertAndGotoUrl(string msg, string pageurl, Control controlId)
    {
        string strScript = "alert('" + msg + "');window.location.href='" + pageurl + "'";
        ScriptManager.RegisterStartupScript(controlId, controlId.GetType(), null, strScript, true);
    }

    /// <summary>
    /// 弹出确认对话框后，跳转到指定的url链接页面。
    /// </summary>
    /// <param name="msg">要弹出的内容</param>
    /// <param name="okGotoUrl">选择ok，跳转到指定的url页面</param>
    /// <param name="cancelGotoUrl">选择Cancel，跳到指定的url链接页面</param>
    /// <param name="page">当前页面，可以直接写this或者Page</param>
    public static void ConfirmAndGotoUrl(string msg, string okGotoUrl, string cancelGotoUrl, Page page)
    {
        string strScript = "";
        if (String.IsNullOrEmpty(cancelGotoUrl))
        {
            strScript = "if(confirm('" + msg + "')==true){window.location.href='" + okGotoUrl + "';}else{return false;}";
        }
        else
        {
            strScript = "if(confirm('" + msg + "')==true){window.location.href='" + okGotoUrl + "';}else{window.location.href='" + cancelGotoUrl + "';}";
        }

        page.ClientScript.RegisterStartupScript(page.GetType(), null, strScript, true);
    }

    /// <summary>
    /// JavaScript弹出确认对话框后，并执行指定的JavaScript脚本。
    /// </summary>
    /// <param name="msg">要弹出的消息内容</param>
    /// <param name="okToScript">选择确认按钮，要执行的脚本</param>
    /// <param name="cancelToScript">选择取消按钮，要执行的脚本，如果参数为NULL，则返回false</param>
    /// <param name="page">当前页面，可以直接写this或者Page</param>
    public static void ConfirmAndExecuteScript(string msg, string okToScript, string cancelToScript, Page page)
    {
        string strScript = "";
        if (String.IsNullOrEmpty(cancelToScript))
        {
            strScript = "if(confirm('" + msg + "')==true){" + okToScript + "} else{return false;}";
        }
        else
        {
            strScript = "if(confirm('" + msg + "')==true){" + okToScript + "} else{" + cancelToScript + "}";
        }

        page.ClientScript.RegisterStartupScript(page.GetType(), null, strScript, true);

    }

    /// <summary>
    /// 利亚Ajax方式弹出对话框，跳转到指定URL的页面，适合控件和页面。
    /// </summary>
    /// <param name="msg">弹出的消息内容</param>
    /// <param name="okGotoUrl">选择确认按钮跳转的URl</param>
    /// <param name="cancelGotoUrl">选择取消按钮跳转的URL</param>
    /// <param name="controlId">控件的ID</param>
    public static void AjaxConfrimAndGotoUrl(string msg,string okGotoUrl,string cancelGotoUrl,Control controlId)
    {
        string strScript = "";
        if (String.IsNullOrEmpty(cancelGotoUrl))
        {
            strScript = "if(confirm('" + msg + "')==true){window.location.href='" + okGotoUrl + "';}else{return false;}";
        }
        else
        {
            strScript = "if(confirm('" + msg + "')==true){window.location.href='" + okGotoUrl + "';}else{window.location.href='" + cancelGotoUrl + "';}";
        }

        ScriptManager.RegisterStartupScript(controlId, controlId.GetType(), null, strScript, true);
    }

    /// <summary>
    /// 利用Ajax弹出确认对话框，并执行指定的脚本
    /// </summary>
    /// <param name="msg">要弹出的内容</param>
    /// <param name="okToScript">选择确认要执行的脚本</param>
    /// <param name="cancelToScript">选择取消要执行的脚本</param>
    /// <param name="controlId">控件的ID属性</param>
    public static void AjaxConfirmAndExecuteScript(string msg, string okToScript, string cancelToScript, Control controlId)
    {
        string strScript = "";
        if (String.IsNullOrEmpty(cancelToScript))
        {
            strScript = "if(confirm('" + msg + "')==true){" + okToScript + "}else{return false;}";
        }
        else
        {
            strScript = "if(confirm('" + msg + "')==true){" + okToScript + "}else{" + cancelToScript + "}";
        }

        ScriptManager.RegisterStartupScript(controlId, controlId.GetType(), null, strScript, true);
    }

    /// <summary>
    /// 向Page对象注册脚本
    /// </summary>
    public static void RegScript(Page page, string script)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "ShowWindow", "<script type='text/javascript' defer>" + script + "</script>");
    }

    /// <summary>
    /// 向页面注册脚本
    /// </summary>
    public static void RegScript(Page page, Control control, string script)
    {
        ScriptManager.RegisterStartupScript(control, page.GetType(), "UpdatePanel", script, true);
    }

    /// <summary>
    /// 注册页面加载时的脚本
    /// </summary>
    /// <param name="page"></param>
    /// <param name="script"></param>
    public static void RegStartScript(Page page, string script)
    {
        page.RegisterStartupScript("scriptStart", "<script language='javascript'>"+script+"</script>");
    }

    public static void RegisterScript(Page page, UpdatePanel updatePanel, string p)
    {
        throw new NotImplementedException();
    }


    /*以下函数由zjf@ihangjing.com 于2011.5.27 添加，用途，用于一些页面的提示信息*/
    /*需要使用的页面需要在页面后台代码上增加
    if (!Page.IsPostBack)
    {
        this.RegisterAdminPageClientScriptBlock();
    } 
    */

    /// <summary>
    /// 注册提示信息JS脚本
    /// </summary>
    public static void RegisterAdminPageClientScriptBlock(Page page)
    {
        string script = "<div id=\"success\" style=\"position:absolute;z-index:300;height:120px;width:284px;left:50%;top:50%;margin-left:-150px;margin-top:-80px;\">\r\n" +
            "	<div id=\"Layer2\" style=\"position:absolute;z-index:300;width:270px;height:90px;background-color: #FFFFFF;border:solid #000000 1px;font-size:14px;\">\r\n" +
            "		<div id=\"Layer4\" style=\"height:26px;background:#f1f1f1;line-height:26px;padding:0px 3px 0px 3px;font-weight:bolder;\">操作提示</div>\r\n" +
            "		<div id=\"Layer5\" style=\"height:64px;line-height:150%;padding:0px 3px 0px 3px;\" align=\"center\"><BR /><table><tr><td valign=top><img border=\"0\" src=\"../images/ajax_loading.gif\"  /></td><td valign=middle style=\"font-size: 14px;\" >正在执行当前操作, 请稍等...<BR /></td></tr></table><BR /></div>\r\n" +
            "	</div>\r\n" +
            "	<div id=\"Layer3\" style=\"position:absolute;width:270px;height:90px;z-index:299;left:4px;top:5px;background-color: #E8E8E8;\"></div>\r\n" +
            "</div>\r\n" +
            "<script> \r\n" +
            "document.getElementById('success').style.display = \"none\"; \r\n" +
            "</script> \r\n" +
            "<script type=\"text/javascript\" src=\"../js/divcover.js\"></script>\r\n";

        #if NET1			
		    RegisterClientScriptBlock("Page", script);
        #else
        page.ClientScript.RegisterClientScriptBlock(page.GetType(), "Page", script);
        #endif
    }

    #if NET1
    public static override void RegisterStartupScript(string key, string scriptstr)
#else
    public static new void RegisterStartupScript(Page page,string key, string scriptstr)
    #endif
    {
        key = key.ToLower();
        if ((key == "pagetemplate") || (key == "page"))
        {
            string script = "";

            if (key == "page")
            {
                script = "<script>\r\n" +
                    "var bar=0;\r\n" +
                    "document.getElementById('success').style.display = \"block\";  \r\n" +
                    "document.getElementById('Layer5').innerHTML ='<BR>操作成功执行<BR>';  \r\n" +
                    "count() ; \r\n" +
                    "function count(){ \r\n" +
                    "bar=bar+4; \r\n" +
                    "if (bar<99) \r\n" +
                    "{setTimeout(\"count()\",100);} \r\n" +
                    "else { \r\n" +
                    "document.getElementById('success').style.display = \"none\";HideOverSels('success'); \r\n" +  //HideOverSels 函数位于admin/javascript/common.js
                    scriptstr + "} \r\n" +
                    "} \r\n" +
                    "</script> \r\n" +
                    "<script> window.onload = function(){HideOverSels('success')};</script>\r\n";
            }

            if (key == "pagetemplate")
            {
                script = "<script> \r\n" +
                    "var bar=0;\r\n document.getElementById('success').style.display = \"block\";  \r\n" +
                    "document.getElementById('Layer5').innerHTML = '<BR>" + scriptstr + "<BR>';  \r\n" +
                    "count() ; \r\n" +
                    "function count(){ \r\n" +
                    "bar=bar+4; \r\n" +
                    "if (bar<99) \r\n" +
                    "{setTimeout(\"count()\",100);} \r\n" +
                    "else { \r\n" +
                    "document.getElementById('success').style.display = \"none\";HideOverSels('success'); \r\n" +
                    "}} \r\n" +
                    "</script> \r\n" +
                    "<script> window.onload = function(){HideOverSels('success')};</script>\r\n";
            }
            #if NET1
			    base.RegisterStartupScript(key, script);
            #else
            page.ClientScript.RegisterStartupScript(page.GetType(), key, script);
            #endif

        }
        else
        {
            #if NET1
			    base.RegisterStartupScript(key, scriptstr);
            #else
            page.ClientScript.RegisterStartupScript(page.GetType(), key, scriptstr);
            #endif
        }
    }

    public static void LoadRegisterStartupScript(Page page,string key, string scriptstr)
    {
        
        string message = "程序执行中... <BR /> 当前操作可能要运行一段时间.<BR />您可在此期间进行其它操作<BR /><BR />";

        string script = "<script>debugger\r\n" +
            "var bar=0;\r\n document.getElementById('success').style.display = \"block\";  \r\n" +
            "document.getElementById('Layer5').innerHTML ='" + message + "';  \r\n" +
            "count() ; \r\n" +
            "function count(){ \r\n" +
            "bar=bar+2; \r\n" +
            "if (bar<99) \r\n" +
            "{setTimeout(\"count()\",100);} \r\n" +
            "else { \r\n" +
            "	document.getElementById('success').style.display = \"none\";HideOverSels('success'); \r\n" +
            scriptstr + "} \r\n" +
            "} \r\n" +
            "</script> \r\n" +
            "<script> window.onload = function(){HideOverSels('success')};</script>\r\n";

        CallBaseRegisterStartupScript(page,key, script);

    }

    public static void CallBaseRegisterStartupScript(Page page, string key, string scriptstr)
    {
        #if NET1
		    base.RegisterStartupScript(key, scriptstr);
        #else
        page.ClientScript.RegisterStartupScript(page.GetType(), key, scriptstr);
        #endif
    }

    /*
     使用示例
    if (无权限操作时)
    {
        Response.Write(base.GetShowMessage());
        Response.End();
        return;
    }
    */
    /// <summary>
    /// 提示信息
    /// </summary>
    /// <returns></returns>
    public static string GetShowMessage()
    {
        string message = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";
        message += "<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><title>您没有权限运行当前程序!</title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">";
        message += "<link href=\"../styles/default.css\" type=\"text/css\" rel=\"stylesheet\"></head><body><br><br><div style=\"width:100%\" align=\"center\">";
        message += "<div align=\"center\" style=\"width:660px; border:1px dotted #925842; background-color:#FFFCEC; margin:auto; padding:20px;\"><img src=\"../images/hint.gif\" border=\"0\" alt=\"提示:\" align=\"absmiddle\" width=\"11\" height=\"13\" /> &nbsp;";
        message += "您没有权限运行当前程序,请您以适当权限的帐号用户登陆后台进行操作!</div></div></body></html>";
        return message;
    }
}
