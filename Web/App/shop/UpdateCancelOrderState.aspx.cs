using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class App_shop_UpdateCancelOrderState : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string orderid = WebUtility.InputText(Request["orderid"]);
        string state = WebUtility.InputText(Request["state"]);// 0同意，1拒绝
        string msg = WebUtility.InputText(Request["msg"]);//拒绝理由

        Custorder dal = new Custorder();
        int rs = 0;

        switch (state)
        {
            case "0":
                string sql = " UPDATE dbo.Custorder SET shopCancel=1  where OrderStatus=8 and orderid = " + orderid;
                rs = WebUtility.excutesql(sql);
                if (rs > 0)
                {
                    dal.AddOrderRecord(orderid, 8, "商家", "商家同意订单取消申请");
                    new OrderStep().Add(new OrderStepInfo()
                    {
                        stepcode = 105,
                        title = "商家同意订单取消申请",
                        subtitle = "",
                        addtime = DateTime.Now,
                        orderid = orderid,
                        revevar1 = "",
                        revevar2 = "",
                        revevar3 = ""
                    });
                }
                break;
            case "1":
                string sqlwhere = " UPDATE dbo.Custorder SET shopCancel=2,Cancelreason='" + msg + "' where OrderStatus=8 and orderid = " + orderid;
                rs = WebUtility.excutesql(sqlwhere);
                if (rs > 0)
                {
                    dal.AddOrderRecord(orderid, 8, "商家", "商家拒绝订单取消申请");
                    new OrderStep().Add(new OrderStepInfo()
                    {
                        stepcode = 108,
                        title = "商家拒绝订单取消申请",
                        subtitle = msg,
                        addtime = DateTime.Now,
                        orderid = orderid,
                        revevar1 = "",
                        revevar2 = "",
                        revevar3 = ""
                    });
                }
                break;
        }

        if (rs == 1)
        {
            Response.Write("{\"orderid\":\"" + orderid + "\",\"state\":\"1\",\"msg\":\"处理成功\"}");
        }
        else
        {
            Response.Write("{\"orderid\":\"" + orderid + "\",\"state\":\"0\",\"msg\":\"处理失败\"}");
        }

        Response.End();
    }
}