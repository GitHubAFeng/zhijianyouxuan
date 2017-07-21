using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
/// 商家app提交订单
/// </summary>
public partial class APP_Android_shop_buildorder : System.Web.UI.Page
{
    Custorder orderdal = new Custorder();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        string ret = " {\"orderstate\":\"2\" } ";
        //ordermodel={ "OrderRcver": "测试", "OrderComm": "18758580307", "AddressText": "杭州市江干区学正街547号金沙居1239", "OrderAttach": "[东][1份餐]", "OrderSums": 100, "SendTime": "2015-06-17T09:50:00", "P2Sign": "东" }
        string jsonstring = Request["ordermodel"];
        string ulat = WebUtility.InputText(Request["ulat"]);
        string ulng = WebUtility.InputText(Request["ulng"]);
        int shopid = HjNetHelper.GetPostParam("shopid", 0);


        PointsInfo shop = new Points().GetModel(shopid);
        ETogoLocalInfo info = new ETogoLocalInfo();
        ETogoLocal bll = new ETogoLocal();

        info = bll.GetInfoById(shop.Unid.ToString());
        CustorderInfo infoaddress = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<CustorderInfo>(jsonstring);
        infoaddress.tempcode = shopid.ToString();
        infoaddress.CustomerName = "";
        infoaddress.UserId = 0;
        infoaddress.fromweb = ((int)OrderSource.ShopCenter).ToString();
        infoaddress.ReveVar1 = shop.sentorg;
        infoaddress.ReveVar2 = "{'ulat':'" + ulat + "','ulng':'" + ulng + "','slat':'" + info.Lat + "','slng':'" + info.Lng + "'}";
        infoaddress.shopdiscountmoney = infoaddress.OrderSums - Convert.ToDecimal(shop.SN2);
        infoaddress.OldPrice = infoaddress.OrderSums;
        infoaddress.cityid = shop.cityid;
        infoaddress.TogoId = shop.Unid;
        infoaddress.paytime = Convert.ToDateTime("1970-1-1");
        infoaddress.IsShopSet = 1;
        infoaddress.ReveDate1 = DateTime.Now;

        if (orderdal.AddTBOrder(infoaddress) > 0)
        {
            ret = "{\"state\":\"1\"}";
        }
        else
        {
            ret = "{\"state\":\"0\"}";
        }

        Response.Write(ret);
        Response.End();

    }
}
