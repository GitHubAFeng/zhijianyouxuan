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

/// <summary>
/// 
/// </summary>
public partial class AndroidAPI_GetRunOrderDetailByOrderId : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string orderid = "";

        if (!string.IsNullOrEmpty(Request["orderid"]))
        {
            orderid = Request["orderid"];
        }

        ExpressOrder dal = new ExpressOrder();

        ExpressOrderInfo info = new ExpressOrderInfo();
        if (orderid.Length > 10)
        {
            info = dal.GetModelByOrderid(orderid);
        }
        else
        {
            info = dal.GetModel(Convert.ToInt32(orderid));
        }


        StringBuilder orderjson = new StringBuilder();


        if (info != null)
        {
            orderjson.Append("{\"orderid\":\"" + info.OrderID.ToString() + "\",\"addtime\":\"" + info.orderTime.ToString("yyyy-MM-dd HH:mm:dd") + "\"");
            orderjson.Append(",\"sendmoney\":\"" + info.sendmoney.ToString("0.0") + "\",\"senddistance\":\"" + info.Currentprice.ToString("0.0") + "\"");
            orderjson.Append(",\"state\":\"" + info.State.ToString() + "\"");
            orderjson.Append(",\"sendtime\":\"" + info.SentTime + "\"");
            orderjson.Append(",\"goods\":\"" + info.Inve2 + "\"");
            orderjson.Append(",\"fromname\":\"" + info.UserName + "\",\"Fromtel\":\"" + info.Tel + "\",\"fromaddress\":\"" + info.Address + "\"");//发件人信息
            orderjson.Append(",\"toname\":\"" + info.callmsg + "\",\"totel\":\"" + info.ReveVar + "\",\"toaddress\":\"" + info.Oorderid + "\"");//收件人信息
            orderjson.Append(",\"PayMode\":\"" + info.PayMode.ToString() + "\"");
            orderjson.Append(",\"fromlat\":\"" + info.ulat + "\",\"fromlng\":\"" + info.ulng + "\"");
            orderjson.Append(",\"tolat\":\"" + info.shoplat + "\",\"tolng\":\"" + info.shoplng + "\"");
            orderjson.Append(",\"Remark\":\"" + WebUtility.NoHTML(info.Remark) + "\"");
            orderjson.Append("}");
        }



        Response.Write(orderjson.ToString().Replace(",],", "],"));
        Response.End();
    }
}
