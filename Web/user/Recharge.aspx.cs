using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class UserHome_Recharge : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin(Request.Url.PathAndQuery);
        if (!this.Page.IsPostBack)
        {
            lbMoney.Text = UserHelp.GetUser().Usermoney.ToString("0.0");
            lbUserName.Text = UserHelp.GetUser().Name;

        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {

        ECustomerInfo user = UserHelp.GetUser();     
        UserAddMoneyLogInfo info = new UserAddMoneyLogInfo();
        UserAddMoneyLog dal = new UserAddMoneyLog();
        string orderid = "";
        string tnum = user.DataID.ToString("00000");
        orderid = "r" + tnum + DateTime.Now.ToString("yyMMddHHmmss");

        info.AddDate = DateTime.Now;
        info.AddMoney = Convert.ToDecimal(WebUtility.InputText(tbRealName.Text));
        info.Inve1 = 0;
        info.Inve2 = orderid;
        info.PayDate = Convert.ToDateTime("1900-01-01 00:00:00");
        info.PayState = 0;
        info.PayType = 1;
        info.State = 0;
        info.TogoName = "";
        info.UserId = user.DataID;



        dal.Add(info);

        string show_url = WebUtility.GetConfigsite() + "";


        PayOrderLog dalpaylog = new PayOrderLog();
        string alipaypayorderid = dalpaylog.GetPayBatch(orderid); ;

        /*********************准备去支付 添加支付日志********************************/
        PayOrderLogInfo apyinfo = new PayOrderLogInfo();
        apyinfo.OrderId = orderid;
        apyinfo.AddTime = DateTime.Now;
        apyinfo.Batch = alipaypayorderid;
        apyinfo.Price = info.AddMoney;
        apyinfo.PayType = 0;
        apyinfo.PayTime = Convert.ToDateTime("1900-1-1");
        apyinfo.State = 0;
        apyinfo.PayCallTime = Convert.ToDateTime("1900-1-1");
        apyinfo.Remark = "";
        apyinfo.Reve1 = "1";
        apyinfo.Reve2 = "";
        dalpaylog.Add(apyinfo);
        /*********************添加支付日志 over********************************/


        AliPayInfo alipa = new AliPayInfo(alipaypayorderid, "Recharge", "pay", info.AddMoney.ToString(), show_url, "", "");
        AliPay.Pay(alipa);
    }
}
