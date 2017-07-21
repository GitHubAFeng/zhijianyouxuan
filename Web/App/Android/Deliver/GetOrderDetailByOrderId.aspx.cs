using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Newtonsoft.Json;
using Hangjing.Common;
using Hangjing.WebCommon;

/// <summary>
/// 
/// </summary>
public partial class AndroidAPI_Deliver_GetOrderDetailByOrderId : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();


        string orderid = HjNetHelper.GetQueryString("orderid");
        int ordertype = HjNetHelper.GetPostParam("ordertype", 1);//1表示外卖订单，2表示跑腿订单


        if (ordertype == 1)
        {
            #region 外卖订单
            IList<CustorderInfo> list = new Custorder().DeliverGetOrderList4GetModel(1, 1, " orderid='" + orderid + "'", "Unid", 1);

            CustorderInfo info = null;
            if (list != null && list.Count > 0)
            {
                info = list[0];
            }

            StringBuilder orderjson = new StringBuilder();
            if (info != null)
            {
                PointsInfo togoinfo = new Points().GetModel(info.TogoId);
                DateTime ReachTime = info.SendTime.AddMinutes(togoinfo.senttime);//抵达时间
                orderjson.Append("{\"totalmoney\":\"" + info.OrderSums.ToString("0.0") + "\",\"phone\":\"" + info.OrderComm + "\",\"SentTime\":\"" + info.SendTime.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"ReachTime\":\"" + ReachTime.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"state\":\"" + info.OrderStatus.ToString() + "\",\"userid\":\"" + info.UserId.ToString() + "\",\"foodinorderString\":\"\",\"list\":[");
            }

            IList<FoodlistInfo> food_list = new Foodlist().GetAllByOrderID(info.orderid);
            foreach (FoodlistInfo finfo in food_list)
            {
                orderjson.Append("{\"count\":" + finfo.FCounts.ToString() + ",\"id\":\"" + finfo.FoodUnid.ToString() + "\",\"price\":" + finfo.FoodPrice.ToString("0.0") + ",\"name\":\"" + finfo.FoodName + "\"},");
            }

            if (info != null)
            {
                orderjson.Append("],");
                orderjson.Append("\"shopname\":\"" + info.TogoName + "\",\"people\":1,\"addtime\":\"" + info.OrderDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"username\":\"" + info.CustomerName + "\",");
                orderjson.Append("\"shopid\":\"" + info.TogoId.ToString() + "\",\"address\":\"" + info.AddressText + "\",\"Packagefree\":\"" + info.Packagefee + "\",\"sentmoney\":\"" + info.SendFee + "\",\"orderid\":\"" + info.orderid + "\",\"realname\":\"" + info.OrderRcver + "\",\"note\":\"" + info.OrderAttach + "\",\"shopaddress\":\"" + info.TogoAddress + "\",\"shoptel\":\"" + info.TogoTel + "\"");
                orderjson.Append(",\"PayMode\":\"" + info.paymode + "\"");
                orderjson.Append(",\"paymoney\":\"" + info.paymoney + "\"");
                orderjson.Append(",\"OrderTotal\":\"" + info.shopdiscountmoney.ToString() + "\"");
                orderjson.Append(",\"oldprice\":\"" + info.OldPrice.ToString() + "\"");
                orderjson.Append(",\"sendstate\":\"" + info.sendstate + "\"");
                orderjson.Append(",\"sitename\":\"" + info.SentTime + "\"");
                orderjson.Append(",\"picktime\":\"" + info.picktime.ToString("yyyy-MM-dd HH:mm:ss") + "\"");
                orderjson.Append(",\"comtime\":\"" + info.comtime.ToString("yyyy-MM-dd HH:mm:ss") + "\"");

                orderjson.Append(",\"promotionmoney\":\"" + info.promotionmoney + "\"");

                orderjson.Append(","+PromotionTool.getOrderPromotionsStr(info.orderid));

                orderjson.Append(",\"cardpay\":\"" + info.cardpay + "\"");

                orderjson.Append(",\"IsShopSet\":\"" + info.IsShopSet + "\"");
                orderjson.Append(",\"paystate\":\"" + info.paystate.ToString() + "\"");
                orderjson.Append(",\"shopCancel\":\"" + info.shopCancel + "\"");
                orderjson.Append(",\"Cancelreason\":\"" + info.Cancelreason.ToString() + "\"");
                orderjson.Append("," + info.ReveVar2.Replace("{", "").Replace("}", "").Replace("'", "\"") + "");  //ReveVar2的格式:{'ulat':'30.260804','ulng':'120.180593','slat':'30.270684','slng':'120.210628'}
                orderjson.Append("}");
            }

            Response.Write(orderjson.ToString().Replace(",],", "],"));
            #endregion

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
                sb.AppendFormat("\"State\":\"{0}\",",info.State);
                sb.AppendFormat("\"UserName\":\"{0}\",",info.UserName);
                sb.AppendFormat("\"Tel\":\"{0}\",",info.Tel);
                sb.AppendFormat("\"SentTime\":\"{0}\",",info.SentTime);
                sb.AppendFormat("\"Address\":\"{0}\",",info.Address);
                sb.AppendFormat("\"orderTime\":\"{0}\",",info.orderTime);
                sb.AppendFormat("\"Remark\":\"{0}\",",info.Remark);
                sb.AppendFormat("\"Oorderid\":\"{0}\",",info.Oorderid);
                sb.AppendFormat("\"Inve1\":\"{0}\",",info.Inve1);
                sb.AppendFormat("\"sendmoney\":\"{0}\",",info.sendmoney);
                sb.AppendFormat("\"Inve2\":\"{0}\",",info.Inve2);
                sb.AppendFormat("\"callmsg\":\"{0}\",",info.callmsg);
                sb.AppendFormat("\"ReveVar\":\"{0}\",",info.ReveVar);
                sb.AppendFormat("\"OrderID\":\"{0}\",",info.OrderID);

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
