using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.Model;
using Hangjing.SQLServerDAL;

/// <summary>
/// 返回app广告列表
/// </summary>
public partial class AndroidAPI_specialad : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        StringBuilder shoplistjson = new StringBuilder();

        string size = Request["size"];

        IList<PPTInfo> list = SectionProxyData.GetPPTList().Where(a => a.Reve2 == "3").ToList();
        shoplistjson.Append("{\"foodtypelist\":[");

        for (int i = 0; i < list.Count; i++)
        {
            PPTInfo info = new PPTInfo();
            info = list[i];
            shoplistjson.Append("{\"SortID\":\"" + info.PUrl + "\",\"SortName\":\"" + info.picture.Replace("~", WebUtility.GetConfigsite()) + "\",\"priority\":\"" + 0 + "\",\"state\":\"" + 0 + "\"},");
        }

        shoplistjson.Append("]}");

        Response.Write(shoplistjson.ToString().Replace(",]}", "]}"));
        Response.End();

    }
}
