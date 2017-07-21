using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.SQLServerDAL;

public partial class App_Android_UpdateOrderStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        StringBuilder json = new StringBuilder("");

        Custorder dal = new Custorder();
        CustorderInfo info = new CustorderInfo();

        string orderid = HjNetHelper.GetPostParam("orderid");
        string status = HjNetHelper.GetPostParam("status");

        info = dal.GetModel(orderid);

        int state = 0;
        string msg = "操作失败";

        if (status == "5" || status == "8")
        {
            if (info.IsShopSet == 1)
            {
                status = "8";
            }
            if (info.shopCancel == 1 && info.iscount > 0)
            {
                state = -1;
                msg = "商家已同意不允许重复退款,操作失败";

                Response.Write("{\"orderid\":\"" + orderid + "\",\"state\":\"" + state + "\",\"msg\":\"" + msg + "\"}");

                Response.End();
                return;
            }
            if (info.shopCancel == 0 && info.iscount > 0)
            {
                state = -1;
                msg = "用户申请退款期间不允许重复退款，请稍后再试";
                Response.Write("{\"orderid\":\"" + orderid + "\",\"state\":\"" + state + "\",\"msg\":\"" + msg + "\"}");

                Response.End();
                return;
            }
            if (info.iscount >= 2)
            {
                state = -1;
                msg = "申请退款次数过多，操作失败";
                Response.Write("{\"orderid\":\"" + orderid + "\",\"state\":\"" + state + "\",\"msg\":\"" + msg + "\"}");

                Response.End();
                return;
            }
        }

        if (dal.UpdataState(orderid, Convert.ToInt32(status)) > 0)
        {
            switch (status)
            {
                case "3":
                    new OrderStep().Add(new OrderStepInfo()
                    {
                        stepcode = 90,
                        title = "处理成功",
                        subtitle = "",
                        addtime = DateTime.Now,
                        orderid = orderid,
                        revevar1 = "",
                        revevar2 = "0",
                        revevar3 = "0"
                    });
                    break;
                case "5":
                    new OrderStep().Add(new OrderStepInfo()
                    {
                        stepcode = 100,
                        title = "用户取消订单",
                        subtitle = "",
                        addtime = DateTime.Now,
                        orderid = orderid,
                        revevar1 = "",
                        revevar2 = "0",
                        revevar3 = "0"

                    });
                    NoticeHelper notice = new NoticeHelper(Context);
                    notice.sendNotice2Shop(info.TogoId, "您有个订单被取消", orderid);
                    break;
                case "8":
                    new OrderStep().Add(new OrderStepInfo()
                    {
                        stepcode = 100,
                        title = "用户取消订单",
                        subtitle = "",
                        addtime = DateTime.Now,
                        orderid = orderid,
                        revevar1 = "",
                        revevar2 = "0",
                        revevar3 = "0"

                    });
                    NoticeHelper notice1 = new NoticeHelper(Context);
                    notice1.sendNotice2Shop(info.TogoId, "您有个取消订单申请", orderid);
                    string sql = " update custorder set iscount=iscount+1,shopCancel=0 where orderid=" + orderid;
                    WebUtility.excutesql(sql);

                    break;
            }
            state = 1;
            msg = "操作成功";
        }
        Response.Write("{\"orderid\":\"" + orderid + "\",\"state\":\"" + state + "\",\"msg\":\"" + msg + "\"}");

        Response.End();
    }
}