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
/// 提现
/// </summary>
public partial class App_Android_shop_startcashout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        string wantmoney = WebUtility.InputText(Request["wantmoney"]);
        int shopid = HjNetHelper.GetPostParam("shopid", 0);

        TogoAddMoneyLogInfo info = new TogoAddMoneyLogInfo();
        decimal point = -Convert.ToDecimal(wantmoney);
        info.AddMoney = point;
        info.Inve1 = 0;
        info.UserId = shopid;
        info.State = 0;
        info.PayType = 1;
        info.PayDate = DateTime.Now;
        info.PayState = 0;
        info.AddDate = DateTime.Now;
        info.Inve2 = "商户提现";

        TogoAddMoneyLog dal = new TogoAddMoneyLog();

        CanCashOutInfo model = dal.GetCanCashOutmoney(Convert.ToInt32(shopid));


        if (Math.Abs(info.AddMoney) > model.usemoney)
        {
            Response.Write("{\"msg\":\"当前最多可提现" + model.usemoney + "元\",\"state\":\"0\"}");
            Response.End();
            return;
        }

        if (dal.AddModel(info) > 0)
        {
            Response.Write("{\"msg\":\"提现成功，请等待管理员处理\",\"state\":\"1\"}");
            Response.End();
        }
        else
        {
            Response.Write("{\"msg\":\"服务器繁忙，请稍后\",\"state\":\"0\"}");
            Response.End();
        }


        Response.End();
    }
}
