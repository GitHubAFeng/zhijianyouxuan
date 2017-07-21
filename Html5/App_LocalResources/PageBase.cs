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
public class PageBase : System.Web.UI.Page, System.Web.SessionState.IRequiresSessionState
{
    /// <summary>
    /// 用户微信对应公众平台唯一编号
    /// </summary>
    public string openid = "";

    /// <summary>
    /// BasePage类构造函数
    /// </summary>
    public PageBase()
    {
        this.PreRender += new EventHandler(Page_PreRender);
    }

    void Page_PreRender(object sender, EventArgs e)
    {
        //将时间戳保存在hiddenfield中
    }
    /// <summary>
    /// 初始化，这个时间生成session对像
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        openid = WebUtility.FixgetCookie("openid");

        //return;

        if (openid == null || openid == "")
        {
            WebUtility.WebOauth(Context);
            return;
        }

        //HJlog.toLog(" PageBase->openid=" + openid + " uid=" + userid);
    }

}

