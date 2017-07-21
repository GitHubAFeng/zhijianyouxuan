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

/// <summary>
/// 订单详细（外卖订单和跑腿订单 2015-12-11 22:23:11）
/// </summary>
public partial class AndroidAPI_shop_GetOrderDetailByOrderId : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();


        string orderid = HjNetHelper.GetPostParam("orderid");
        int ordertype = HjNetHelper.GetPostParam("ordertype", 1);//1表示外卖订单，2表示跑腿订单


        if (ordertype == 1)
        {
            Custorder dal = new Custorder();
            Foodlist orderfood_bll = new Foodlist();

            CustorderInfo info = new CustorderInfo();
            IList<CustorderInfo> list = dal.GetListFix(1, 1, " custorder.orderid='" + orderid + "'", "OrderDateTime", 1);

            StringBuilder orderjson = new StringBuilder();

            if (list != null && list.Count > 0)
            {
                info = list[0];
            }

            if (info != null)
            {
                orderjson.Append("{\"totalmoney\":\"" + info.OrderSums.ToString("0.0") + "\",\"phone\":\"" + info.OrderComm + "\",\"SentTime\":\"" + info.OrderDateTime + "\",\"state\":\"" + info.OrderStatus.ToString() + "\",\"userid\":\"" + info.UserId.ToString() + "\",\"foodinorderString\":\"\",\"list\":[");
            }

            IList<FoodlistInfo> food_list = orderfood_bll.GetList(1000, 1, " orderid='" + orderid + "'", "Unid", 1);

            foreach (FoodlistInfo finfo in food_list)
            {
                orderjson.Append("{\"count\":" + finfo.FCounts.ToString() + ",\"id\":\"" + finfo.FoodUnid.ToString() + "\",\"price\":" + finfo.FoodPrice.ToString("0.0") + ",\"name\":\"" + finfo.FoodName + "\"},");
            }
            orderjson.Append("],");

            orderjson.Append("\"shopname\":\"" + info.TogoName + "\"");
            orderjson.Append(",\"people\":\"" + 1 + "\"");
            orderjson.Append(",\"addtime\":\"" + info.OrderDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "\"");
            orderjson.Append(",\"username\":\"" + info.OrderRcver + "\"");
            orderjson.Append(",\"shopid\":\"" + info.TogoId + "\"");
            orderjson.Append(",\"address\":\"" + info.AddressText + "\"");
            orderjson.Append(",\"Packagefree\":\"" + info.Packagefee + "\"");
            orderjson.Append(",\"sentmoney\":\"" + info.SendFee + "\"");
            orderjson.Append(",\"orderid\":\"" + info.orderid + "\"");
            orderjson.Append(",\"realname\":\"" + info.OrderRcver + "\"");
            orderjson.Append(",\"note\":\"" + info.OrderAttach + "\"");
            orderjson.Append(",\"shopaddress\":\"" + info.TogoAddress + "\"");
            orderjson.Append(",\"shoptel\":\"" + info.TogoTel + "\"");
            orderjson.Append(",\"PayMode\":\"" + info.paymode + "\"");
            orderjson.Append(",\"OrderTotal\":\"" + info.shopdiscountmoney.ToString() + "\"");
            orderjson.Append(",\"oldprice\":\"" + info.OldPrice.ToString() + "\"");
            orderjson.Append(",\"eattype\":\"" + info.ReveInt2 + "\"");
            orderjson.Append(",\"people\":\"" + info.ReveInt1 + "\"");
            orderjson.Append(",\"promotionmoney\":\"" + info.promotionmoney + "\"");
            orderjson.Append("," + PromotionTool.getOrderPromotionsStr(info.orderid));
            orderjson.Append(",\"cardpay\":\"" + info.cardpay + "\"");
            orderjson.Append(",\"sendstate\":\"" + info.sendstate + "\"");
            orderjson.Append(",\"paystate\":\"" + info.paystate + "\"");
            orderjson.Append(",\"paymoney\":\"" + info.paymoney + "\"");
            orderjson.Append(",\"IsShopSet\":\"" + info.IsShopSet + "\"");
            orderjson.Append(",\"SentTime\":\"" + info.SendTime + "\"");
            orderjson.Append(",\"shopCancel\":\"" + info.shopCancel + "\"");
            orderjson.Append(",\"Cancelreason\":\"" + info.Cancelreason + "\"");

            orderjson.Append("}");

            Response.Write(orderjson.ToString().Replace(",],", "],"));

        }
        else//跑腿订单 2015-12-11
        {
            ExpressOrder bll = new Hangjing.SQLServerDAL.ExpressOrder();
            ExpressOrderInfo info = bll.GetModel(orderid);

            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            if (info != null)
            {
                //sb.Append(DataHelper.Obj2Json(info));
                sb.AppendFormat("\"State\":\"{0}\",", info.State);
                sb.AppendFormat("\"UserName\":\"{0}\",", info.UserName);
                sb.AppendFormat("\"Tel\":\"{0}\",", info.Tel);
                sb.AppendFormat("\"SentTime\":\"{0}\",", info.SentTime);
                sb.AppendFormat("\"Address\":\"{0}\",", info.Address);
                sb.AppendFormat("\"orderTime\":\"{0}\",", info.orderTime);
                sb.AppendFormat("\"Remark\":\"{0}\",", info.Remark);
                sb.AppendFormat("\"Oorderid\":\"{0}\",", info.Oorderid);
                sb.AppendFormat("\"Inve1\":\"{0}\",", info.Inve1);
                sb.AppendFormat("\"sendmoney\":\"{0}\",", info.sendmoney);
                sb.AppendFormat("\"Inve2\":\"{0}\",", info.Inve2);
                sb.AppendFormat("\"callmsg\":\"{0}\",", info.callmsg);
                sb.AppendFormat("\"ReveVar\":\"{0}\",", info.ReveVar);
                sb.AppendFormat("\"OrderID\":\"{0}\",", info.OrderID);

                sb.AppendFormat("\"ulat\":\"{0}\",", info.ulat);
                sb.AppendFormat("\"ulng\":\"{0}\",", info.ulng);
                sb.AppendFormat("\"shoplat\":\"{0}\",", info.shoplat);
                sb.AppendFormat("\"shoplng\":\"{0}\"", info.shoplng);

            }

            sb.Append("}");

            Response.Write(sb.ToString());
        }




        Response.End();
    }
}
