using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
/// 商家app幻灯片
/// </summary>
public partial class AndroidAPI_shoppics : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        StringBuilder shoplistjson = new StringBuilder();

        int id = HjNetHelper.GetPostParam("shopid", 0);
        TogoPicture tdal = new TogoPicture();
        IList<TogoPictureInfo> list = tdal.GetList(10, 1, " TogoId =" + id, "Pri", 1);
        shoplistjson.Append("{\"datalist\":[");

        foreach (var info in list)
        {
            shoplistjson.Append("{\"dataid\":\"" + info.DataId + "\",\"icon\":\"" + info.Picture.Replace("~", WebUtility.GetConfigsite()) + "\"},");
        }

        shoplistjson.Append("]}");

        Response.Write(shoplistjson.ToString().Replace(",]}", "]}"));
        Response.End();

    }
}
