using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using System.Text;

/// <summary>
/// 获取网站设置
/// </summary>
public partial class App_Android_GetWebBasicList : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        int id = Convert.ToInt32(Request["id"]);

        string backvalue = SectionProxyData.GetSetValue(id);

        string ret = "";

        if (backvalue!="")
        {
            ret = "{\"id\":\"" + id + "\",\"value\":\"" + WebUtility.NoHTML(backvalue) + "\"}";
        }

        Response.Write(ret);
        Response.End();

    }
}

