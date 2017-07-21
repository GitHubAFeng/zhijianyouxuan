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
/// 1、操作员进行订单分配之后，骑士在5分钟之内未进行订单确定的，请弹窗提醒，调度员好电话进行提醒；
///2、订单配送时间超过XX分钟未送达，系统自动提醒。
///3、订单超过xxx分钟未被任何处理，那么系统应当自动提醒。
/// 三个提醒都一起返回.
/// </summary>
public partial class qy_54tss_AreaAdmin_Se1rvice_ajax_mymsg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Custorder dalorder = new Custorder();
        OrderDeliver dalod = new OrderDeliver();

        int cid = HjNetHelper.GetQueryInt("cid", 0);

        //1,操作员进行订单分配之后，骑士在5分钟之内未进行订单确定的，请弹窗提醒，调度员好电话进行提醒；
        int GPSDelay = 5;
        string sql = " orderid in (select orderid from Custorder where OrderStatus = 7 and sendstate = 0 and cityid = " + cid + "  and ReveVar1 = '点必得配送' and ReveInt2 = 0  ) and Inve1 = 0 and DispatchTime < '" + DateTime.Now.AddMinutes(-GPSDelay) + "' ";

        IList<OrderDeliverInfo> GPSDelaylist = dalod.GetList(10, 1, sql, "DispatchTime", 0);
        StringBuilder sb = new StringBuilder();
        if (GPSDelaylist.Count > 0)
        {
            sb.Append("<div class='clear'>");
            sb.Append("<span>" + GPSDelay + "分内骑士未进行订单确定的订单</span>");
            sb.Append("<div  style=' width:337px' class='mymsg_notice'><ul>");
            foreach (OrderDeliverInfo item in GPSDelaylist)
            {
                sb.Append("<li><a href=\"OrderDelive.aspx?oid=" + item.Orderid + "\" style='margin-left:5px;'>" + item.Orderid + "</a></li>");
            }
            sb.Append("</ul></div>");
            sb.Append("</div>");
        }
        //2、订单配送时间超过XX分钟未送达，系统自动提醒。
        int DeliverDelay = 10;
        sql = " orderid in (select orderid from Custorder where OrderStatus = 7 and cityid = " + cid + "  and ReveVar1 = '点必得配送' and ReveInt2 = 0 ) and   DATEADD ( minute  ,inve1, DeliveryTime ) < '" + DateTime.Now.AddMinutes(-DeliverDelay) + "' ";

        IList<OrderDeliverInfo> DeliverDelaylist = dalod.GetList(10, 1, sql, "DeliveryTime", 0);
        if (DeliverDelaylist.Count > 0)
        {
            sb.Append("<div class='clear'>");
            sb.Append("<span>订单配送时间超过" + DeliverDelay + "分钟未送达的订单</span>");
            sb.Append("<div  style=' width:337px' class='mymsg_notice'><ul>");
            foreach (OrderDeliverInfo item in DeliverDelaylist)
            {
                sb.Append("<li><a href=\"OrderDelive.aspx?oid=" + item.Orderid + "\" style='margin-left:5px;'>" + item.Orderid + "</a></li>");
            }
            sb.Append("</ul></div>");
            sb.Append("</div>");
        }

        //3、订单超过xxx分钟未被任何处理，那么系统应当自动提醒。(状态为1的)
        int HandleDelay = 3;
        sql = " OrderStatus = 2 and  OrderDateTime < '" + DateTime.Now.AddMinutes(-HandleDelay) + "'  and ReveVar1 = '点必得配送' and ReveInt2 = 0 and cityid = " + cid + "";
        IList<CustorderInfo> HandleDelaylist = dalorder.GetList(10, 1, sql, "OrderDateTime", 0);

        if (HandleDelaylist.Count > 0)
        {
            sb.Append("<div class='clear'>");
            sb.Append("<span>超过" + HandleDelay + "分钟未被任何处理</span>");
            sb.Append("<div  style=' width:337px' class='mymsg_notice'><ul>");
            foreach (var item in HandleDelaylist)
            {
                sb.Append("<li><a href=\"OrderDelive.aspx?oid=" + item.orderid + "\" style='margin-left:5px;'>" + item.orderid + "</a></li>");
            }
            sb.Append("</ul></div>");
            sb.Append("</div>");
        }

        string msg = sb.ToString();
        if (msg == "")
        {
            msg = "0";
        }

        Response.Clear();
        Response.Write(msg);
        Response.End();
    }
}
