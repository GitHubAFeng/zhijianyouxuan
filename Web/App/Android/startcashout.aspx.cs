using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class App_Android_startcashout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        string wantmoney = WebUtility.InputText(Request["wantmoney"]);
        int shopid = HjNetHelper.GetPostParam("shopid", 0);

        UserAddMoneyLog dal = new UserAddMoneyLog();
        UserAddMoneyLogInfo info = new UserAddMoneyLogInfo();

        info.UserId = shopid;
        info.AddMoney = Convert.ToDecimal(WebUtility.InputText(wantmoney));
        info.State=0;
        info.PayType = 8;
        info.PayDate = Convert.ToDateTime("1970-1-1"); 
        info.PayState=1;
        info.AddDate=DateTime.Now;
        info.Inve1=0;
        info.Inve2="";

        CanCashOutInfo model = dal.GetCanCashOutmoney(info.UserId);

        if (info.AddMoney > model.usemoney)
        {
            Response.Write("{\"msg\":\"当前最多可提现" + model.usemoney + "元\",\"state\":\"0\"}");
            Response.End();
            return;
        }

        if (dal.Add(info) > 0)
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