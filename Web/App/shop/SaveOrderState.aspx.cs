using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
/// 接受或者拒绝订单
//   订单编号 订单接收状态   商家是否确认(0未确认，1已经确认，2拒绝).,推送商家根据这个字段
/// </summary>
public partial class App_Android_shop_SaveOrderState : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string orderid = WebUtility.InputText(Request["orderid"]);
        string state = WebUtility.InputText(Request["state"]);// 1已经确认，2拒绝,3表示订单完成
        string msg = WebUtility.InputText(Request["msg"]);//拒绝理由
        int shopid = HjNetHelper.GetPostParam("shopid", 0);

        Custorder dal = new Custorder();
        int rs = 0;

        switch (state)
        {
            case "1":
            case "2":
                {
                    shopSetOrder set = new shopSetOrder(Context, shopid, orderid, Convert.ToInt32(state), msg);
                    rs = set.HandleOrder();
                }

                break;
            case "3":
                {
                    shopSetOrder set = new shopSetOrder(Context, shopid, orderid, 3, msg);
                    rs = set.CompleteOrder();
                }
                break;
        }

        if (rs == 1)
        {
            Response.Write("{\"orderid\":\"" + orderid + "\",\"state\":\"1\",\"msg\":\"\"}");
        }
        else if (rs == -1)
        {
            msg = "用户已取消订单,不能接受订单";
            Response.Write("{\"orderid\":\"" + orderid + "\",\"state\":\"0\",\"msg\":\"" + msg + "\"}");
        }
        else
        {
            if (state == "2")
            {
                msg = "配送员已经接单，不能取消";
            }

            Response.Write("{\"orderid\":\"" + orderid + "\",\"state\":\"0\",\"msg\":\"" + state + "\"}");
        }

        Response.End();
    }
}
