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
/// 商家信息
/// </summary>
public partial class App_shop_getShopInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        StringBuilder shoplistjson = new StringBuilder();

        Points daltogo = new Points();
        int shopid = HjNetHelper.GetPostParam("shopid", 0);

        PointsInfo Togomodel = daltogo.GetModel(shopid);

        shoplistjson.Append("{");
        shoplistjson.Append("\"togoname\":\"" + Togomodel.Name + "\",");
        shoplistjson.Append("\"picture\":\"" + Togomodel.Picture.Replace("~", WebUtility.GetConfigsite()) + "\",");
        shoplistjson.Append("\"PosSrvAd\":\"" + Togomodel.licensePic.Replace("~", WebUtility.GetConfigsite()) + "\",");
        shoplistjson.Append("\"PosRoom\":\"" + Togomodel.cateringPic.Replace("~", WebUtility.GetConfigsite()) + "\",");
        shoplistjson.Append("\"togoaccount\":\"" + Togomodel.LoginName + "\",");
        shoplistjson.Append("\"togopassword\":\"" + Togomodel.Password + "\",");


        shoplistjson.Append("\"RcvType\":\"" + Togomodel.RcvType + "\",");


        shoplistjson.Append("\"address\":\"" + Togomodel.Address + "\",");
        shoplistjson.Append("\"PTimes\":\"" + Togomodel.PTimes + "\",");//满就免配送费
        shoplistjson.Append("\"status\":\"" + Togomodel.Status + "\",");//营业状态
        shoplistjson.Append("\"CommPerson\":\"" + Togomodel.CommPerson + "\",");//联系人
        shoplistjson.Append("\"special\":\"" + WebUtility.FileterJson(Togomodel.special) + "\",");//店铺活动
        shoplistjson.Append("\"Opentimes1\":\"" + Togomodel.Opentimes1.ToString("HH:mm") + "\",");
        shoplistjson.Append("\"Closetimes1\":\"" + Togomodel.Opentimes2.ToString("HH:mm") + "\",");
        shoplistjson.Append("\"Opentimes2\":\"" + Togomodel.Closetimes1.ToString("HH:mm") + "\",");
        shoplistjson.Append("\"Closetimes2\":\"" + Togomodel.Closetimes2.ToString("HH:mm") + "\",");
        shoplistjson.Append("\"senttime\":\"" + Togomodel.senttime + "\",");
        shoplistjson.Append("\"tbsend\":\"" + Togomodel.EBuilding + "\",");
        shoplistjson.Append("\"email\":\"" + Togomodel.email + "\",");
        shoplistjson.Append("\"Comm\":\"" + Togomodel.Comm + "\"");
        shoplistjson.Append("}");
        Response.Write(shoplistjson.ToString());

    }
}