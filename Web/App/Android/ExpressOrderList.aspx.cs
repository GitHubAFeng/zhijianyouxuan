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
public partial class App_Android_ExpressOrderList : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        ExpressOrder dal = new ExpressOrder();

        int pagesize = HjNetHelper.GetPostParam("pagesize", 4);
        int pageindex = HjNetHelper.GetPostParam("pageindex", 1);
        int pagecount = 1;

        int userid = HjNetHelper.GetPostParam("userid", 0);
        string phone = HjNetHelper.GetQueryString("phone");
        string today = Request["today"];

        StringBuilder shoplistjson = new StringBuilder();

        string SqlWhere = " 1=1 ";
        if (userid > 0)
        {
            SqlWhere += " and  UserId = " + userid;
        }
        if (phone != null && phone != "" && phone != "0")
        {
            SqlWhere += " and  Tel = '" + phone + "'";
        }

        //今日订单 不分状态
        if (today == "1")
        {
            SqlWhere += " and  DATEDIFF(day,ordertime,GETDATE()) =0 ";
        }

        int count = dal.GetCount(SqlWhere);
        IList<ExpressOrderInfo> list = dal.GetList(pagesize, pageindex, SqlWhere, "dataid", 1);

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
            shoplistjson.Append("{\"OrderID\":\"" + info.OrderID + "\"");
            shoplistjson.Append(",\"TogoName\":\"" + info.Address + "\"");
            shoplistjson.Append(",\"orderTime\":\"" + info.orderTime.ToString("yyyy-MM-dd HH:mm:dd") + "\"");
            shoplistjson.Append(",\"TotalPrice\":\"" + info.TotalPrice.ToString("0.0") + "\"");
            shoplistjson.Append(",\"State\":\"" + info.State.ToString() + "\"");
            shoplistjson.Append(",\"sendmoney\":\"" + info.sendmoney.ToString() + "\"");

            shoplistjson.Append(",\"sendstate\":\"" + 0 + "\"");
            shoplistjson.Append(",\"IsShopSet\":\"" + 0 + "\"");

            //添加继续支付时添加
            shoplistjson.Append(",\"PayMode\":\"" + info.PayMode + "\"");
            shoplistjson.Append(",\"paystate\":\"" + info.paystate + "\"");
            shoplistjson.Append(",\"cardpay\":\"" + 0 + "\"");
            shoplistjson.Append(",\"SendTime\":\"" + info.SentTime + "\"");

            shoplistjson.Append(",\"eattype\":\"\",\"Packagefree\":\"\"},");
        }

        shoplistjson.Append("]}");

        Response.Write(shoplistjson.ToString().Replace("},]}", "}]}"));
        Response.End();
    }
}
