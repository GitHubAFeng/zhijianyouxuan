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
using System.Collections;
using Hangjing.WebCommon;

/// <summary>
/// 获取订单[外卖订单 和 跑腿订单]
/// </summary>
public partial class AndroidAPI_shop_GetOrderListByUserId : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        Custorder dal = new Custorder();

        int pagesize = HjNetHelper.GetPostParam("pagesize", 8);//页容量
        int pageindex = HjNetHelper.GetPostParam("pageindex", 1);//当前页
        int pagecount = 1;

        string did = WebUtility.InputText(Request["did"]);//商家编号
        string order_state = WebUtility.InputText(Request["state"]);//订单状态
        string order_sendstate = WebUtility.InputText(Request["sendstate"]);//配送状态
        string shopState = WebUtility.InputText(Request["shopState"]);//商家状态
        string shopHurry = WebUtility.InputText(Request["shopHurry"]);//催单  0  未处理 1 已处理
        string shopCancel = WebUtility.InputText(Request["shopCancel"]);//退单  0 未处理  3 已处理（1 同意 2 拒绝 ）


        string waimaiWhere = "togoid=" + did + " and (paymode = 4 OR paystate = 1)  ";
        string paotuiWhere = " 1=1 ";

        if (!string.IsNullOrEmpty(did))
        {
            paotuiWhere += " and  TogoId=" + did;//配送员编号
        }
        else
        {
            Response.End();
            return;
        }

        //订单状态
        if (!string.IsNullOrEmpty(order_state) && order_state != "-1")
        {
            waimaiWhere += " and  OrderStatus=" + order_state;
            paotuiWhere += " and  State=" + order_state;
        }


        //商家接收状态
        if (!string.IsNullOrEmpty(shopState))
        {
            waimaiWhere += " and IsShopSet=" + shopState;
            //不传或者等于1 就返回跑腿订单
            if (shopState.Trim() != "1")
            {
                paotuiWhere += " and 1=2 ";
            }
        }

        switch (shopHurry)
        {
            case "0":
                waimaiWhere += " and Custorder.orderid in(SELECT oid FROM hurryorder where ReveInt=0)";
                break;
            case "1":
                waimaiWhere += " and Custorder.orderid in(SELECT oid FROM hurryorder where ReveInt=1)";
                break;
        }

        switch (shopCancel)
        {
            case "0":
                waimaiWhere += " and (Custorder.shopCancel  IS NULL or Custorder.shopCancel=0) ";
                break;
            case "1":
                waimaiWhere += " and Custorder.shopCancel=1 ";
                break;
            case "2":
                waimaiWhere += " and Custorder.shopCancel=2 ";
                break;
            case "3":
                waimaiWhere += " and Custorder.shopCancel in(1,2)";
                break;
        }


        //支付类型 （1支付宝/2银联/3账户余额/4货到付款/5微信支付）
        //如果订单为支付宝支付、银联支付、微信支付，订单必须支付成功才返回 2015-12-23 
        waimaiWhere += "  AND (((paymode=1 OR paymode=2 OR paymode=5) AND paystate=1) OR paymode=3 OR paymode=4)";


        int count = 0;
        //得到外卖订单和跑腿订单，都按调度时间排序
        IList<CustorderInfo> list = dal.getOrderListWithWMAndPT(pagesize, pageindex, waimaiWhere, paotuiWhere, " OrderDateTime  DESC");
        if (list.Count > 0)
        {
            count = list[0].OrderCount;
        }
        if (count % pagesize == 0)//整数倍
        {
            pagecount = count / pagesize;
        }
        else
        {
            pagecount = count / pagesize + 1;
        }


        StringBuilder orderlistjson = new StringBuilder();
        orderlistjson.Append("{\"page\":\"" + pageindex.ToString() + "\",\"total\":\"" + pagecount + "\",\"record\":\"" + count + "\", \"orderlist\":[");

        CustorderInfo info = new CustorderInfo();
        for (int i = 0; i < list.Count; i++)
        {
            info = new CustorderInfo();
            Foodlist orderfood_bll = new Foodlist();
            info = list[i];

            IList<FoodlistInfo> food_list = orderfood_bll.GetList(1000, 1, " orderid='" + info.orderid + "'", "Unid", 1);

            if (food_list.Count > 0)
            {
                orderlistjson.Append("{\"orderid\":\"" + info.orderid + "\"");
                orderlistjson.Append(",\"shopname\":\"" + info.TogoName + "\"");
                orderlistjson.Append(",\"addtime\":\"" + info.OrderDateTime.ToString("yyyy-MM-dd HH:mm:dd") + "\"");
                orderlistjson.Append(",\"DispatchTime\":\"" + Convert.ToDateTime(info.tempcode).ToString("yyyy-MM-dd HH:mm:dd") + "\"");//骑士配送完成时间
                orderlistjson.Append(",\"SendFee\":\"" + info.SendFee + "\"");
                orderlistjson.Append(",\"Sender\":\"" + info.Sender + "\"");
                orderlistjson.Append(",\"delivertel\":\"" + info.delivertel + "\"");
                orderlistjson.Append(",\"totalmoney\":\"" + info.OrderSums.ToString("0.0") + "\"");
                orderlistjson.Append(",\"state\":\"" + info.OrderStatus.ToString() + "\"");
                orderlistjson.Append(",\"address\":\"" + info.AddressText.ToString() + "\"");
                orderlistjson.Append(",\"sendstate\":\"" + info.sendstate + "\"");
                orderlistjson.Append(",\"ordertype\":\"" + info.Unid + "\"");
                orderlistjson.Append(",\"togoid\":\"" + info.TogoId + "\"");
                orderlistjson.Append(",\"OrderComm\":\"" + info.OrderComm + "\"");
                orderlistjson.Append(",\"OrderRcver\":\"" + info.OrderRcver + "\"");
                orderlistjson.Append(",\"IsShopSet\":\"" + info.IsShopSet + "\"");
                orderlistjson.Append(",\"SentTime\":\"" + info.SendTime.ToString("yyyy-MM-dd HH:mm:dd") + "\"");
                orderlistjson.Append(",\"Packagefree\":\"" + info.Packagefee + "\"");
                orderlistjson.Append(",\"promotionmoney\":\"" + info.promotionmoney + "\"");
                orderlistjson.Append(",\"cardpay\":\"" + info.cardpay + "\"");
                orderlistjson.Append(",\"PayMode\":\"" + info.paymode + "\"");
                orderlistjson.Append(",\"paystate\":\"" + info.paystate + "\"");
                orderlistjson.Append(",\"shopCancel\":\"" + info.shopCancel + "\"");
                orderlistjson.Append(",\"note\":\"" + info.OrderAttach + "\"");
                orderlistjson.Append(",\"OrderTotal\":\"" + info.shopdiscountmoney.ToString() + "\"");
                orderlistjson.Append(",\"oldprice\":\"" + info.OldPrice.ToString() + "\"");
                orderlistjson.Append(",\"shopCancel\":\"" + info.shopCancel + "\"");
                orderlistjson.Append(",\"Cancelreason\":\"" + info.Cancelreason + "\"");


                orderlistjson.Append(",\"list\":[");
                foreach (FoodlistInfo finfo in food_list)
                {
                    orderlistjson.Append("{");
                    orderlistjson.Append("\"count\":\"" + finfo.FCounts.ToString() + "\",");
                    orderlistjson.Append("\"id\":\"" + finfo.FoodUnid.ToString() + "\",");
                    orderlistjson.Append("\"price\":\"" + finfo.FoodPrice.ToString("0.0") + "\",");
                    orderlistjson.Append("\"name\":\"" + finfo.FoodName + "\"");
                    orderlistjson.Append("},");
                }
                orderlistjson.Append("]},");
            }
            else
            {
                //是否催单
                if (string.IsNullOrEmpty(shopHurry))
                {
                    ExpressOrder bll = new Hangjing.SQLServerDAL.ExpressOrder();
                    ExpressOrderInfo infos = bll.GetModel(info.orderid);
                    if (infos != null)
                    {
                        orderlistjson.Append("{");
                        orderlistjson.AppendFormat("\"State\":\"{0}\",", infos.State);
                        orderlistjson.AppendFormat("\"UserName\":\"{0}\",", infos.UserName);
                        orderlistjson.AppendFormat("\"Tel\":\"{0}\",", infos.Tel);
                        orderlistjson.AppendFormat("\"SentTime\":\"{0}\",", infos.SentTime);
                        orderlistjson.AppendFormat("\"Address\":\"{0}\",", infos.Address);
                        orderlistjson.AppendFormat("\"orderTime\":\"{0}\",", infos.orderTime.ToString("yyyy-MM-dd HH:mm:dd"));
                        orderlistjson.AppendFormat("\"Remark\":\"{0}\",", infos.Remark);
                        orderlistjson.AppendFormat("\"Oorderid\":\"{0}\",", infos.Oorderid);
                        orderlistjson.AppendFormat("\"Inve1\":\"{0}\",", infos.Inve1);
                        orderlistjson.AppendFormat("\"delivername\":\"{0}\",", infos.delivername);
                        orderlistjson.AppendFormat("\"delivertel\":\"{0}\",", infos.delivertel);
                        orderlistjson.AppendFormat("\"ordertype\":\"{0}\",", 2);

                        orderlistjson.AppendFormat("\"sendmoney\":\"{0}\",", infos.sendmoney);
                        orderlistjson.AppendFormat("\"Inve2\":\"{0}\",", infos.Inve2);
                        orderlistjson.AppendFormat("\"callmsg\":\"{0}\",", infos.callmsg);
                        orderlistjson.AppendFormat("\"ReveVar\":\"{0}\",", infos.ReveVar);
                        orderlistjson.AppendFormat("\"OrderID\":\"{0}\",", infos.OrderID);

                        orderlistjson.AppendFormat("\"ulat\":\"{0}\",", infos.ulat);
                        orderlistjson.AppendFormat("\"ulng\":\"{0}\",", infos.ulng);
                        orderlistjson.AppendFormat("\"shoplat\":\"{0}\",", infos.shoplat);
                        orderlistjson.AppendFormat("\"shoplng\":\"{0}\"", infos.shoplng);
                        orderlistjson.Append("},");
                    }
                }
            }

        }

        orderlistjson.Append("]}");
        Response.Write(orderlistjson.ToString().Replace("},]}", "}]}").Replace("]},]", "]}]"));


        Response.End();
    }
}
