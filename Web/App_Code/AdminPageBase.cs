using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

//zjf@ihangjing.com
//2011.6.15
//后台管理页面基类  进行登录判断 权限判断
using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;

/// <summary>
/// AdminPageBase 后台页面基类
/// </summary>
public class AdminPageBase : System.Web.UI.Page
{
    /// <summary>
    /// 系统配置信息
    /// </summary>
    protected internal SiteInfo config;

    public string pagename = "basic.aspx";// HjNetHelper.GetPageName();

    /// <summary>
    /// BasePage类构造函数
    /// </summary>
    public AdminPageBase()
    {

    }

    void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetActionStamp();
        }

    }

    /// <summary>
    /// 设置时间戳到session中 每次提交都会导致session变成新的时间戳
    /// </summary>
    private void SetActionStamp()
    {
        Session["actionStamp"] = Server.UrlEncode(DateTime.Now.ToString());
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
        // 如果IP访问列表有设置则进行判断
        if (config.Ipaccess.Trim() != "")
        {
            string[] regctrl = StringHelper.SplitString(config.Ipaccess, "\n");
            if (!Utils.InIPArray(HjNetHelper.GetIP(), regctrl))
            {
                GameOver("抱歉, 系统设置了IP访问列表限制, 您无法访问本网站", 2);
                return false;
            }
        }

        // 如果IP访问列表有设置则进行判断
        if (config.Ipdenyaccess.Trim() != "")
        {
            string[] regctrl = StringHelper.SplitString(config.Ipdenyaccess, "\n");
            if (Utils.InIPArray(HjNetHelper.GetIP(), regctrl))
            {
                GameOver("由于您严重违反了网站的相关规定, 已被禁止访问", 2);
                return false;
            }
        }

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

    /// <summary>
    /// 检测管理员是否拥有权限
    /// </summary>
    /// <param name="strType">大板块划分</param>
    /// <param name="strOperate"></param>
    //public bool CheckRights(string pageRole, string userRole)
    //{
    //    //获取角色对应的操作权限字符串

    //    string roles = "";

    //    IList<RoleInfo> rolelist = new List<RoleInfo>();

    //    Role bll = new Role();

    //    rolelist = bll.GetList(1000, 1, "Id in ('" + userRole + "')", "Id", 1);

    //    foreach (RoleInfo role in rolelist)
    //    {
    //        roles += role.Basic;
    //    }


    //    if (roles.IndexOf(pageRole) > -1)//存在改权限
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        GameOver("您无权限",2);
    //        return false;
    //    }
    //}
    public static void CheckRights(string A)
    {

    }
    /// <summary>
    /// 检测管理员是否拥有权限
    /// </summary>
    /// <param name="strType">页面所需的权限</param>
    /// <param name="strOperate"></param>
    //public bool CheckRights(string pageRole)
    //{
    //    //获取角色对应的操作权限字符串
    //    //测试时 设置值为：1,2,3,4,5,6,7,8
    //    //UserHelp.IsAdminLogin(Request.Url.PathAndQuery);

    //    //string userRole = UserHelp.GetAdmin().RoleId;

    //  //  userRole = "1,2,3,4,5,6,7,8";//测试使用

    //    string roles = "";

    //    IList<RoleInfo> rolelist = new List<RoleInfo>();

    //    Role bll = new Role();

    //    rolelist = bll.GetList(1000, 1, "Id in (" + userRole + ")", "Id", 1);

    //    foreach (RoleInfo role in rolelist)
    //    {
    //        roles += role.Basic;
    //    }

    //    if (roles.IndexOf(pageRole) > -1)//存在改权限
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        GameOver("您无权限 " + roles + "; " + rolelist .Count.ToString()+ "", 2);
    //        return false;
    //    }
    //}
        /*
        商家信息	A   1
        会员信息	B   2
        订单信息	C   3
        财务系统	D   4
        积分兑换	E   5
        抽奖管理	F   6
        数据统计	G   7
        网站基本信息管理	H 8
        管理员权限管理	J    9
        */
}

