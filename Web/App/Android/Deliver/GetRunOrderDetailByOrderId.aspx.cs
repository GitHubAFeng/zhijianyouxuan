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
public partial class AndroidAPI_Deliver_GetRunOrderDetailByOrderId : APIPageBase
{
    /*
    {"totalmoney":"125.0","phone":"12345678910","SentTime":"2012-4-1 12:00","state":"1","userid":"207","foodinorderString":"",
     * "list":["{\"count\":2,\"id\":\"73278\",\"price\":30.0,\"name\":\"清蒸牛肉饭\"}",
     * "{\"count\":3,\"id\":\"73277\",\"price\":20.0,\"name\":\"香菇滑鸡\"}"],
     * "shopname":"食养林","people":1,"addtime":"2012-04-01 10:57:29","username":"test123","shopid":"1400",
     * "address":"保利21世家 - XX网络科技有限公司","Packagefree":"0.0","sentmoney":"5","orderid":"20120401105729260","realname":"admin","note":"测试"}
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string orderid = "";

        if (!string.IsNullOrEmpty(Request["orderid"]))
        {
            orderid = Request["orderid"];
        }

        ExpressOrder dal = new ExpressOrder();


        //IList<ExpressOrderInfo> list = new List<ExpressOrderInfo>();

        //list = dal.GetList(1, 1, " DataID = '" + orderid + "'", "DataID", 1);

        ExpressOrderInfo info = new ExpressOrderInfo();
        if (orderid.Length == 18)
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
            //orderlistjson.Append(",\"paystate\":\"" + info.paystate.ToString() + "\"");
            orderjson.Append("}");
        }



        Response.Write(orderjson.ToString().Replace(",],", "],"));
        Response.End();
    }
}
