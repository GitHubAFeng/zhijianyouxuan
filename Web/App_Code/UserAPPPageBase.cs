using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Hangjing.Common;
using Hangjing.Model;
using Newtonsoft.Json;

/// <summary>
/// app 页面基类，
/// </summary>
public class UserAPPPageBase : System.Web.UI.Page
{
    public apiResultInfo res = new apiResultInfo()
    {
        msg = "",
        state = 1
    };

    /// <summary>
    /// BasePage类构造函数
    /// </summary>
    public UserAPPPageBase()
    {
        this.PreRender += new EventHandler(Page_PreRender);
    }

    void Page_PreRender(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 返回数据
    /// </summary>
    /// <param name="hint"></param>
    /// <param name="mode"></param>
    public void ShowJsonData()
    {
        System.Web.HttpContext.Current.Response.Clear();
        System.Web.HttpContext.Current.Response.Write(JsonConvert.SerializeObject(res));
        System.Web.HttpContext.Current.Response.End();
    }
}

