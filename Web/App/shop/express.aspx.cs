using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web.Script.Serialization;
using Hangjing.Common;
using Hangjing.WebCommon;

/// <summary>
/// 商户APP提交跑腿订单
/// </summary>
public partial class App_Android_shop_express : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        string ret = " {\"orderstate\":\"2\" } ";
        //ordermodel={ "Inve2": "外卖", "callmsg": "测试姓名", "ReveVar": "15268170323", "Oorderid": "杭州市江干区学正街547号金沙居1239",  "SentTime": "2015-06-17 09:50:00","PayMode": 1, "Remark": "备注","ulat":"30.314395","ulng":"120.390334","shopid":"889" }
        string jsonstring = Request["ordermodel"];

        if (string.IsNullOrEmpty(jsonstring))
        {
            Response.Write("{\"state\":\"0\"}");
            Response.End();
            return;
        }


        ETogoLocal bll = new ETogoLocal();
        ExpressOrder dal = new ExpressOrder();

        ExpressOrderInfo infoaddress = new JavaScriptSerializer().Deserialize<ExpressOrderInfo>(jsonstring);
        PointsInfo shop = new Points().GetModel(infoaddress.shopid);

        ETogoLocalInfo infos = bll.GetInfoById(shop.Unid.ToString());

        SendfeeInfo snfo = new SendfeeInfo();
        snfo.ShopID = shop.cityid;//menunum

        CityInfo city = new City().GetModel(snfo.ShopID);
        snfo.cityname = city.cname;

        snfo.sendtime = Convert.ToDateTime(infoaddress.SentTime);

        snfo.latlng = new latlnginfo();
        snfo.latlng.ulat = infoaddress.ulat;
        snfo.latlng.ulng = infoaddress.ulng;
        snfo.latlng.slat = infos.Lat;
        snfo.latlng.slng = infos.Lng;

        SendFee fee = new SendFee(snfo);
        SendInfo model = fee.getSendFee();


        ExpressOrderInfo info = new ExpressOrderInfo();
        info.UserID = 0;
        info.CustomerName = "";
        info.Inve2 = infoaddress.Inve2;
        info.UserName = shop.CommPerson;
        info.Tel = shop.Comm;
        info.Address = shop.Address;
        info.callmsg = infoaddress.callmsg;
        info.ReveVar = infoaddress.ReveVar;
        info.Oorderid = infoaddress.Oorderid;
        info.SentTime = infoaddress.SentTime;
        info.Remark = infoaddress.Remark;
        info.PayMode = infoaddress.PayMode;
        info.paytime = Convert.ToDateTime("1970-1-1");
        info.paystate = 0;
        info.Currentprice = model.Distance;
        info.sendmoney = model.sendmoney;
        info.ulat = infoaddress.ulat;
        info.ulng = infoaddress.ulng;
        info.shoplat = infos.Lat;
        info.shoplng = infos.Lng;
        info.tempcode = "";
        string orderid = DateTime.Now.ToString("yMMddHHmmss") + WebUtility.GetRandomOnlyNum(4) + info.Tel.Substring(info.Tel.Length - 2, 2);
        info.OrderID = orderid;
        info.State = 0;
        info.orderTime = DateTime.Now;
        info.SetStateTime = Convert.ToDateTime("1970-1-1");
        info.Inve1 = 0;
        info.bid = 0;
        info.writer = "";
        //info.isaddDeliver = 0;
        info.TotalPrice = info.sendmoney;
        info.Cityid = shop.cityid;//menunum
        info.sid = 0;  // Convert.ToInt32(WebUtility.FixgetCookie("mybid"));
        info.ordersource = 0;
        info.callcount = 0;
        info.paymoney = 0;
        info.PayOrderId = "";
        info.TogoID = infoaddress.shopid;
        info.isaddpoint = 0;
        info.sendtype = 0;
        info.sitelat = "{'ulat':'" + info.ulat + "','ulng':'" + info.ulng + "','slat':'" + info.shoplat + "','slng':'" + info.shoplng + "'}";
        info.sitelng = "";
        info.ordertype = 0;
        info.noaccess = 0;
        info.validateCode = 0;
        info.iscancel = 0;
        info.ReveInt2 = 0;
        info.ReveDate1 = Convert.ToDateTime("1970-1-1");
        info.ReveDate2 = Convert.ToDateTime("1970-1-1");
        info.IsTimeLimit = 0;
        info.ReveInt1 = 0;
        info.servename = "";

        int oid = dal.Add(info);

        if (oid > 0)
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