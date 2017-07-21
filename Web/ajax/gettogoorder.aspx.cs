#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-6-7 21:31:43.
* E-Mail: jijunjian@ihangjing.com
* 查询商家新订单
*********************************************** */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

/// <summary>
/// 异步查询商家新订单
/// </summary>
public partial class aEnglish_Ajax_TogoHome_gettogoorder : System.Web.UI.Page
{
    Custorder dal = new Custorder();
    hurryorder hal = new hurryorder();
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = WebUtility.InputText(Request["tid"]);
        string SqlWhere = "   OrderStatus in (1,2,7) and   TogoId = " + id + " and IsShopSet =0  and (paymode = 4 OR paystate = 1) ";

        IList<CustorderInfo> list = dal.GetList(10, 1, SqlWhere, "Unid", 1);
        Response.Clear();


        string msg = "";
        int ordercount = list.Count;

        SqlWhere = " ReveVar = " + id + " and ReveInt=0";
        int hurcount = hal.GetCount(SqlWhere);
        SqlWhere = " TogoId = " + id + " and OrderStatus=8 and (shopCancel is null or shopCancel=0)";
        int cancount = dal.GetCount(SqlWhere);

        msg = ordercount + "&&" + hurcount + "&&" + cancount;

        if (list.Count == 0)
        {
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class='ordermsgtab'>");
            foreach (var item in list)
            {
                sb.Append("<tr><td><a class='orange' href=\"OrderDetail.aspx?id=" + item.Unid + "\">" + item.orderid + "</a> " + item.OrderDateTime.ToString("HH:mm") + " " + item.OrderRcver + " " + item.OrderComm + "</td></tr>");
            }
            sb.Append("</table>");
            
            msg += "&&" + sb.ToString();
        }
        Response.Write(msg);
        Response.End();
    }
}
