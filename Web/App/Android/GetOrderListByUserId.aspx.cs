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
/// 获取用户的订单列表
/// </summary>
public partial class AndroidAPI_GetOrderListByUserId_2 : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        Custorder dal = new Custorder();
        Foodlist orderfood_bll = new Foodlist();

        int pagesize = HjNetHelper.GetPostParam("pagesize", 4);
        int pageindex = HjNetHelper.GetPostParam("pageindex", 1);
        int pagecount = 1;

        int userid = HjNetHelper.GetPostParam("userid", 0);
        string phone = HjNetHelper.GetQueryString("phone");
        string today = Request["today"];

        StringBuilder shoplistjson = new StringBuilder();

        IList<CustorderInfo> list = new List<CustorderInfo>();

        string SqlWhere = " 1=1 ";
        if (userid > 0)
        {
            SqlWhere += " and  UserId = " + userid;
        }
        if (phone != null && phone != "" && phone != "0")
        {
            SqlWhere += " and  OrderComm = '" + phone + "'";
        }

        //今日订单 不分状态
        if (today == "1")
        {
            SqlWhere += " and  DATEDIFF(day,ordertime,GETDATE()) =0 ";
        }

        int count = dal.GetCount(SqlWhere);
        list = dal.GetList(pagesize, pageindex, SqlWhere, "unid", 1);

        if (count % pagesize == 0)//整数倍
        {
            pagecount = count / pagesize;
        }
        else
        {
            pagecount = count / pagesize + 1;
        }

        shoplistjson.Append("{\"page\":\"" + pageindex.ToString() + "\",\"total\":\"" + pagecount + "\", \"orderlist\":[");


        foreach (var info in list)
        {
            shoplistjson.Append("{\"OrderID\":\"" + info.orderid + "\"");
            shoplistjson.Append(",\"TogoName\":\"" + info.TogoName + "\"");
            shoplistjson.Append(",\"orderTime\":\"" + info.OrderDateTime.ToString("yyyy-MM-dd HH:mm:dd") + "\"");
            shoplistjson.Append(",\"TotalPrice\":\"" + info.OrderSums.ToString("0.0") + "\"");
            shoplistjson.Append(",\"State\":\"" + info.OrderStatus.ToString() + "\"");
            shoplistjson.Append(",\"sendmoney\":\"" + info.SendFee.ToString() + "\"");

            shoplistjson.Append(",\"sendstate\":\"" + info.sendstate + "\"");
            shoplistjson.Append(",\"IsShopSet\":\"" + info.IsShopSet + "\"");
            shoplistjson.Append(",\"TogoId\":\"" + info.TogoId + "\"");
            shoplistjson.Append(",\"sitename\":\"" + info.SentTime + "\"");
            shoplistjson.Append(",\"shopCancel\":\"" + info.shopCancel + "\"");
            shoplistjson.Append(",\"Cancelreason\":\"" + info.Cancelreason + "\"");

            //添加继续支付时添加
            shoplistjson.Append(",\"PayMode\":\"" + info.paymode + "\"");
            shoplistjson.Append(",\"paystate\":\"" + info.paystate + "\"");
            shoplistjson.Append(",\"cardpay\":\"" + info.cardpay + "\"");
            shoplistjson.Append(",\"SendTime\":\"" + info.SendTime + "\"");
            shoplistjson.Append(",\"eattype\":\"" + info.TogoPic.Replace("~", WebUtility.GetConfigsite()) + "\"");
            shoplistjson.Append(",\"Packagefree\":\"\"");
            shoplistjson.Append("," + info.ReveVar2.Replace("{", "").Replace("}", "").Replace("'", "\"") + "");
            //ReveVar2的格式:{'ulat':'30.260804','ulng':'120.180593','slat':'30.270684','slng':'120.210628'}

            IList<FoodlistInfo> food_list = orderfood_bll.GetList(1000, 1, " orderid='" + info.orderid + "'", "Unid", 1);

            if (food_list.Count > 0)
            {
                shoplistjson.Append(",\"list\":[");
                foreach (FoodlistInfo finfo in food_list)
                {
                    shoplistjson.Append("{");
                    shoplistjson.Append("\"count\":\"" + finfo.FCounts.ToString() + "\",");
                    shoplistjson.Append("\"id\":\"" + finfo.FoodUnid.ToString() + "\",");
                    shoplistjson.Append("\"price\":\"" + finfo.FoodPrice.ToString("0.0") + "\",");
                    shoplistjson.Append("\"name\":\"" + finfo.FoodName + "\"");
                    shoplistjson.Append("},");
                }
                shoplistjson.Append("]");
            }
            else
            {
                ExpressOrder bll = new Hangjing.SQLServerDAL.ExpressOrder();
                ExpressOrderInfo infos = bll.GetModel(info.orderid);
                if (infos != null)
                {
                    shoplistjson.Append(",\"list\":[");
                    shoplistjson.Append("{");
                    shoplistjson.AppendFormat("\"State\":\"{0}\",", infos.State);
                    shoplistjson.AppendFormat("\"UserName\":\"{0}\",", infos.UserName);
                    shoplistjson.AppendFormat("\"Tel\":\"{0}\",", infos.Tel);
                    shoplistjson.AppendFormat("\"SentTime\":\"{0}\",", infos.SentTime);
                    shoplistjson.AppendFormat("\"Address\":\"{0}\",", infos.Address);
                    shoplistjson.AppendFormat("\"orderTime\":\"{0}\",", infos.orderTime);
                    shoplistjson.AppendFormat("\"Remark\":\"{0}\",", infos.Remark);
                    shoplistjson.AppendFormat("\"Oorderid\":\"{0}\",", infos.Oorderid);
                    shoplistjson.AppendFormat("\"Inve1\":\"{0}\",", infos.Inve1);
                    shoplistjson.AppendFormat("\"sendmoney\":\"{0}\",", infos.sendmoney);
                    shoplistjson.AppendFormat("\"Inve2\":\"{0}\",", infos.Inve2);
                    shoplistjson.AppendFormat("\"callmsg\":\"{0}\",", infos.callmsg);
                    shoplistjson.AppendFormat("\"ReveVar\":\"{0}\",", infos.ReveVar);
                    shoplistjson.AppendFormat("\"OrderID\":\"{0}\",", infos.OrderID);

                    shoplistjson.AppendFormat("\"ulat\":\"{0}\",", infos.ulat);
                    shoplistjson.AppendFormat("\"ulng\":\"{0}\",", infos.ulng);
                    shoplistjson.AppendFormat("\"shoplat\":\"{0}\",", infos.shoplat);
                    shoplistjson.AppendFormat("\"shoplng\":\"{0}\"", infos.shoplng);
                    shoplistjson.Append("}");
                    shoplistjson.Append("]");
                }
                else
                {
                    shoplistjson.Append(",\"list\":[");
                    shoplistjson.Append("{");
                    shoplistjson.Append("}");
                    shoplistjson.Append("]");
                }
            }
            shoplistjson.Append("},");
        }

        shoplistjson.Append("]}");

        Response.Write(shoplistjson.ToString().Replace("},]}", "}]}").Replace("]},]", "]}]"));
        Response.End();
    }
}
