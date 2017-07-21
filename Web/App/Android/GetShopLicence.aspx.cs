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
/// 商家从业资质照片
/// </summary>
public partial class App_Android_GetShopLicence : System.Web.UI.Page
{
    ShopData dal = new ShopData();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

      

        StringBuilder shoplistjson = new StringBuilder();
        shoplistjson.Append("{\"page\":\"" + 1 + "\",\"total\":\"" + 1 + "\",\"datalist\":[");

        int shopid = HjNetHelper.GetPostParam("shopid", 0);
        PointsInfo info = new Points().GetModel(shopid);

        if (info.isLicense == 1)
        {

            shoplistjson.Append("{\"SortID\":\"" + 0 + "\"");
            shoplistjson.Append(",\"SortName\":\"" + 0 + "\"");
            shoplistjson.Append(",\"SortPic\":\"" + info.licensePic.Replace("~", WebUtility.GetConfigsite()) + "\"");//后期添加
            shoplistjson.Append(",\"JOrder\":\"" + 0 + "\"},");


        }

        if (info.isCatering == 1)
        {
            shoplistjson.Append("{\"SortID\":\"" + 0 + "\"");
            shoplistjson.Append(",\"SortName\":\"" + 0 + "\"");
            shoplistjson.Append(",\"SortPic\":\"" + info.cateringPic.Replace("~", WebUtility.GetConfigsite()) + "\"");//后期添加
            shoplistjson.Append(",\"JOrder\":\"" + 0 + "\"},");
        }



        shoplistjson.Append("]}");

        Response.Write(shoplistjson.ToString().Replace(",]}", "]}"));
        Response.End();

    }
}

