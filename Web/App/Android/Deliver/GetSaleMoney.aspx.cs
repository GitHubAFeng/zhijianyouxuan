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
/// 骑士营业额，及订单量统计
/// </summary>
public partial class Android_Deliver_GetSaleMoney : System.Web.UI.Page
{
    Custorder dalorder = new Custorder();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        StringBuilder json = new StringBuilder("");
        string SqlWhere = "  ";
        int togoid = HjNetHelper.GetPostParam("shopid", 0);
        if (togoid > 0)
        {
            SqlWhere = "1=1 AND deliverid=" + togoid;
        }

        string starttime = WebUtility.InputText(Request["starttime"]);//开始日期
        string endtime = WebUtility.InputText(Request["endtime"]);//结束日期


        string order_state = WebUtility.InputText(Request["state"]);//订单状态
        int paymode =  HjNetHelper.GetPostParam("paymode", -1);//支付方式

        //分页参数
        int pagesize = HjNetHelper.GetPostParam("pagesize", 10); //分页大小
        int pageindex = HjNetHelper.GetPostParam("pageindex", 1);//分页标签
        int pagecount = 1;//分页总数


        StringBuilder orderlistjson = new StringBuilder();
        IList<CustorderInfo> list = new List<CustorderInfo>();

        if (starttime != "")
        {
            SqlWhere += " and OrderDateTime > '" + starttime + "' ";
        }
        if (endtime != "")
        {
            SqlWhere = SqlWhere + " and OrderDateTime <  '" + endtime + " 23:59:59'";
        }

        if (starttime == "" && endtime == "")
        {
            SqlWhere += " and DATEDIFF(day,OrderDateTime,GETDATE()) = 0 ";
        }

        if (order_state != "" && order_state != "-1")
        {
            SqlWhere += " and OrderStatus=" + order_state;
        }

        if (paymode >= 0)
        {
            SqlWhere += " and paymode=" + paymode;
        }

        //Response.Write(SqlWhere);

        CustorderInfo count = dalorder.GetCountAndTotal1(SqlWhere);
        orderlistjson.Append("{\"OrderCount\":\"" + count.OrderCount + "\",\"OrderTotal\":\"" + count.OrderTotal + "\"");

        int ordercount = dalorder.GetCount(SqlWhere);
        list = dalorder.GetListFix(pagesize, pageindex, SqlWhere, "OrderDateTime", 1);

        if (ordercount % pagesize == 0)//整数倍
        {
            pagecount = ordercount / pagesize;
        }
        else
        {
            pagecount = ordercount / pagesize + 1;
        }


        orderlistjson.Append(",\"page\":\"" + pageindex.ToString() + "\",\"total\":\"" + pagecount + "\", \"orderlist\":[");

        CustorderInfo info = new CustorderInfo();

        for (int i = 0; i < list.Count; i++)
        {
            info = new CustorderInfo();
            info = list[i];
            orderlistjson.Append("{\"orderid\":\"" + info.orderid + "\",\"shopname\":\"" + info.TogoName + "\",\"addtime\":\"" + info.OrderDateTime.ToString("yyyy-MM-dd HH:mm:dd") + "\",\"totalmoney\":\"" + info.OrderSums.ToString("0.0") + "\",\"state\":\"" + info.OrderStatus.ToString() + "\",\"address\":\"" + WebUtility.NoHTML(info.AddressText.Trim()) + "\"");
            orderlistjson.Append(",\"PayMode\":\"" + info.paymode.ToString() + "\"");//支付方式
            orderlistjson.Append(",\"paystate\":\"" + info.paystate.ToString() + "\"");//支付状态
            orderlistjson.Append(",\"paystate\":\"" + info.paystate.ToString() + "\"");
            orderlistjson.Append(",\"eattype\":\"" + 0 + "\"");
            orderlistjson.Append(",\"sendmoney\":\"" + 0 + "\"");
            orderlistjson.Append("},");
        }

        orderlistjson.Append("]}");

        Response.Write(orderlistjson.ToString().Replace("},]}", "}]}"));

        Response.End();

    }
}
