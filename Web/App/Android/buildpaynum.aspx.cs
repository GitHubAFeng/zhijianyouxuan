using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.SQLServerDAL;

/// <summary>
/// 生成支付编号(订单编号+001)
/// </summary>
public partial class APP_AndriodV2_buildpaynum : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        PayOrderLog dalpaylog = new PayOrderLog();

        string orderid = HjNetHelper.GetPostParam("orderid");
        string price = Request["price"];

        string batchnum = dalpaylog.GetPayBatch(orderid);

        /*********************准备去支付 添加支付日志********************************/

        PayOrderLogInfo info = new PayOrderLogInfo();
        info.OrderId = orderid;
        info.AddTime = DateTime.Now;
        info.Batch = batchnum;
        info.Price = Convert.ToDecimal(price);
        info.PayType = 0;
        info.PayTime = Convert.ToDateTime("1900-1-1");
        info.State = 0;
        info.PayCallTime = Convert.ToDateTime("1900-1-1");
        info.Remark = "";
        info.Reve1 = "1";
        info.Reve2 = "";
        dalpaylog.Add(info);
        /*********************添加支付日志 over********************************/

        string ret = "{\"batch\":\""+batchnum+"\",\"state\":\"1\"}";

        Response.Write(ret);
        Response.End();
    }
}
