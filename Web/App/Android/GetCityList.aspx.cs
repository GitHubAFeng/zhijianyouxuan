using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.SQLServerDAL;
using System.Data;

/// <summary>
/// 获取城市列表
/// </summary>
public partial class APP_Android_GetCityList : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        StringBuilder listjson = new StringBuilder();
        listjson.Append("{\"page\":\"1\",\"total\":\"" + 0 + "\", \"datalist\":[");

        foreach (var item in SectionProxyData.GetCityList())
        {
            listjson.Append("{\"cid\":\"" + item.cid + "\",\"cname\":\"" + item.cname + "\"},");
        }

        listjson.Append("]}");

        Response.Write(listjson.ToString().Replace("},]}", "}]}"));
        Response.End();
    }
}
