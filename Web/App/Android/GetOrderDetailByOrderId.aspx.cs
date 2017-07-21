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
using Hangjing.WebCommon;

public partial class AndroidAPI_GetOrderDetailByOrderId_2 : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string orderid = HjNetHelper.GetPostParam("orderid");
        //手机类型：0表示android,1表示ios
        int isios = 0;

        string mobileversion = Request["isios"];
        if (mobileversion != null && mobileversion == "1")
        {
            isios = 1;
        }

        Custorder dal = new Custorder();
        Foodlist orderfood_bll = new Foodlist();

        CustorderInfo info = dal.GetModel(orderid);

        StringBuilder shoplistjson = new StringBuilder();

        if (info != null)
        {
            shoplistjson.Append("{\"TotalPrice\":\"" + info.OrderSums + "\",\"UserName\":\"" + info.OrderRcver + "\",\"Tel\":\"" + info.OrderComm + "\",\"State\":\"" + info.OrderStatus.ToString() + "\",\"UserID\":\"" + info.UserId.ToString() + "\",\"foodlist\":[");
        }
        IList<FoodlistInfo> food_list = orderfood_bll.GetAllByOrderID(orderid);
        int styleindex = 0;
        foreach (var finfo in food_list)
        {
            if (isios == 0)
            {
                styleindex++;
                shoplistjson.Append("\"{\\\"Num\\\":" + finfo.FCounts.ToString() + ",\\\"FoodID\\\":\\\"" + finfo.FoodUnid.ToString() + "\\\",\\\"FoodPrice\\\":" + finfo.FoodPrice.ToString("0.0") + ",\\\"shopname\\\":\\\"" + "" + "\\\",\\\"foodname\\\":\\\"" + finfo.FoodName + "\\\",\\\"activetype\\\":\\\"" + 0 + "\\\",\\\"activeid\\\":\\\"" + 0 + "\\\",\\\"package\\\":\\\"" + 0 + "\\\",\\\"isreview\\\":\\\"" + 0 + "\\\"}\"");
                if (styleindex != food_list.Count)
                {
                    //最后一个
                    shoplistjson.Append(",");
                }
                    
            }
            else
            {
                styleindex++;
                shoplistjson.Append("{\"Num\":" + finfo.FCounts.ToString() + ",\"FoodID\":\"" + finfo.FoodUnid.ToString() + "\",\"FoodPrice\":" + finfo.FoodPrice.ToString("0.0") + ",\"shopname\":\"" + "" + "\",\"foodname\":\"" + finfo.FoodName + "\",\"activetype\":\"" + 0 + "\",\"activeid\":\"" + 0 + "\",\"package\":\"" + 0 + "\",\"isreview\":\"" + 0 + "\"}");
                if (styleindex != food_list.Count)
                {
                    //最后一个
                    shoplistjson.Append(",");
                }
            }
        }
        shoplistjson.Append("],");

        shoplistjson.Append("\"TogoName\":\"" + info.TogoName + "\",\"orderTime\":\"" + info.OrderDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "\",");
        shoplistjson.Append("\"sendtype\":\"" + "0" + "\",");
        //支付部分
        shoplistjson.Append("\"PayMode\":\"" + info.paymode + "\",");
        shoplistjson.Append("\"paymoney\":\"" + info.paymoney + "\",");
        shoplistjson.Append("\"paytime\":\"" + info.paytime + "\",");
        shoplistjson.Append("\"paymodeint\":\"" + info.paymode + "\",");
        shoplistjson.Append("\"paystateint\":\"" + info.paystate + "\",");

        shoplistjson.Append("\"promotionmoney\":\"" + info.promotionmoney + "\"");
        shoplistjson.Append("," + PromotionTool.getOrderPromotionsStr(info.orderid));
        shoplistjson.Append(",\"cardpay\":\"" + info.cardpay + "\",");

        shoplistjson.Append("\"shopCancel\":\"" + info.shopCancel + "\",");
        shoplistjson.Append("\"Cancelreason\":\"" + info.Cancelreason + "\",");
        shoplistjson.Append("\"IsShopSet\":\"" + info.IsShopSet + "\",");
        shoplistjson.Append("\"sendstate\":\"" + info.sendstate + "\",");
        shoplistjson.Append("\"orderstateint\":\"" + info.OrderStatus + "\",");
        shoplistjson.Append("\"paystate\":\"" + info.paystate + "\",");
        shoplistjson.Append("\"sitename\":\"" + info.SentTime + "\",");
        shoplistjson.Append("\"discountmsy\":\"" + "" + "\",");
        shoplistjson.Append("\"discount\":\"" + 0 + "\",");
        shoplistjson.Append("\"deliverid\":\"" + info.deliverid + "\",");
        shoplistjson.Append("\"shoptel\":\"" + info.TogoTel + "\",");
        shoplistjson.Append("" + info.ReveVar2.Replace("{", "").Replace("}", "").Replace("'", "\"") + ",");
        //ReveVar2的格式:{'ulat':'30.260804','ulng':'120.180593','slat':'30.270684','slng':'120.210628'}
        shoplistjson.Append("\"TogoId\":\"" + info.TogoId.ToString() + "\",\"Address\":\"" + info.AddressText + "\",\"sendmoney\":\"" + info.SendFee + "\",\"Packagefree\":\"" + info.Packagefee + "\",\"OrderID\":\"" + info.orderid + "\",\"SentTime\":\"" + info.SendTime + "\",\"Remark\":\"" + info.OrderAttach + "\"}");

        Response.Write(shoplistjson.ToString().Replace(",],", "],"));
        Response.End();
    }
}
