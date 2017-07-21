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

/// <summary>
/// 获取订单[外卖订单和跑腿订单]
/// </summary>
public partial class AndroidAPI_Deliver_GetOrderListByUserId : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        Custorder dal = new Custorder();//外卖订单

        int pagesize = HjNetHelper.GetPostParam("pagesize", 8);//页容量
        int pageindex = HjNetHelper.GetPostParam("pageindex", 1);//当前页
        int pagecount = 1;

        string order_state = WebUtility.InputText(Request["state"]);//订单状态
        string order_sendstate = WebUtility.InputText(Request["sendstate"]);//配送状态
        int did = HjNetHelper.GetPostParam("did", 0); ;//配送员编号
        int cityid = HjNetHelper.GetPostParam("cityid", 0); ;//城市编号（发群时要判断）

        StringBuilder orderlistjson = new StringBuilder();

        string waimaiWhere = " (paymode = 4 OR paystate = 1) and IsShopSet = 1  ";
        string paotuiWhere = " 1=1 ";

        //群编号
        int gid = HjNetHelper.GetPostParam("gid", 0);
        if (gid > 0)
        {
            waimaiWhere += " and  (deliverheaderid=" + gid + " or deliverheaderid = " + Constant.biggid + " ) and OrderStatus=7 and sendstate=0  ";//并且配送状态为未配送 2015-12-14

            paotuiWhere += " and (sid=" + gid + ") and state =1";//群组编号
            if (cityid > 0)
            {
                waimaiWhere += " and  custorder.cityid =" + cityid;
                paotuiWhere += " and CityId=" + cityid;
            }
        }

        if (did != 0)
        {
            waimaiWhere += " and  custorder.deliverid=" + did;
            paotuiWhere += " and  ExpressOrder.Inve1=" + did;//配送员编号
        }

        //订单状态
        if (order_state != null && order_state != "" && order_state != "-1")
        {
            waimaiWhere += " and  OrderStatus=" + order_state;
            //paotuiWhere += " and  State=" + order_state;
        }

        //配送状态 后期添加 2015-12-11 
        //sendstate=0 对应跑腿订单的状态 1
        //1对应跑腿订单的状态 2
        //2 对应跑腿订单的状态 4
        //3 对应跑腿订单的状态 3
        //4 对应跑腿订单的状态 6
        //不传返回所有
        if (!string.IsNullOrEmpty(order_sendstate))
        {
            waimaiWhere += " and sendstate = " + order_sendstate;
            switch (order_sendstate)
            {
                case "0": paotuiWhere += " and State =1 "; break;
                case "1": paotuiWhere += " and State =2 "; break;
                case "2": paotuiWhere += " and State =4 "; break;
                case "3": paotuiWhere += " and State =3 "; break;
                case "4": paotuiWhere += " and State =6 "; break;
            }
        }

        int count = 0;

        //得到外卖订单和跑腿订单，都按调度时间排序
        IList<CustorderInfo> list = dal.getOrderListWithWMAndPT(pagesize, pageindex, waimaiWhere, paotuiWhere, " DispatchTime  DESC ");
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


        orderlistjson.Append("{\"page\":\"" + pageindex.ToString() + "\",\"total\":\"" + pagecount + "\",\"record\":\"" + count + "\", \"orderlist\":[");

        CustorderInfo info = new CustorderInfo();
        for (int i = 0; i < list.Count; i++)
        {
            info = new CustorderInfo();
            info = list[i];

            orderlistjson.Append("{\"orderid\":\"" + info.orderid + "\"");
            orderlistjson.Append(",\"shopname\":\"" + info.TogoName + "\"");
            orderlistjson.Append(",\"addtime\":\"" + info.OrderDateTime.ToString("yyyy-MM-dd HH:mm:dd") + "\"");
            orderlistjson.Append(",\"DispatchTime\":\"" + info.tempcode + "\"");

            orderlistjson.Append(",\"PayMode\":\"" + info.paymode + "\"");
            orderlistjson.Append(",\"shopaddress\":\"" + info.TogoAddress + "\"");
            orderlistjson.Append(",\"paymoney\":\"" + info.paymoney + "\"");
            orderlistjson.Append(",\"sitename\":\"" + info.SentTime + "\"");
            orderlistjson.Append(",\"picktime\":\"" + info.picktime.ToString("yyyy-MM-dd HH:mm:ss") + "\"");
            orderlistjson.Append(",\"comtime\":\"" + info.comtime.ToString("yyyy-MM-dd HH:mm:ss") + "\"");

            orderlistjson.Append(",\"totalmoney\":\"" + info.OrderSums.ToString("0.0") + "\"");
            orderlistjson.Append(",\"state\":\"" + info.OrderStatus.ToString() + "\"");
            orderlistjson.Append(",\"address\":\"" + info.AddressText.ToString() + "\"");
            orderlistjson.Append(",\"sendstate\":\"" + info.sendstate + "\"");
            orderlistjson.Append(",\"ordertype\":\"" + info.Unid + "\"");
            orderlistjson.Append(",\"togoaddress\":\"" + info.TogoAddress + "\"");
            orderlistjson.Append(",\"shopCancel\":\"" + info.shopCancel + "\"");
            orderlistjson.Append(",\"Cancelreason\":\"" + info.Cancelreason + "\"");
            orderlistjson.Append("," + info.ReveVar2.Replace("{", "").Replace("}", "").Replace("'", "\"") + "");  
            //ReveVar2的格式:{'ulat':'30.260804','ulng':'120.180593','slat':'30.270684','slng':'120.210628'}
            orderlistjson.Append("},");

        }

        orderlistjson.Append("]}");
        Response.Write(orderlistjson.ToString().Replace("},]}", "}]}").Replace(",},", "},").Replace(",}]", "}]"));


        Response.End();
    }
}
