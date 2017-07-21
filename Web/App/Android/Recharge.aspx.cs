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

public partial class App_Android_Recharge : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        UserAddMoneyLogInfo info = new UserAddMoneyLogInfo();
        UserAddMoneyLog dal = new UserAddMoneyLog();


        Response.Clear();

        StringBuilder json = new StringBuilder("");

        int userid = HjNetHelper.GetPostParam("userid", 0);//用户编号
        string AddMoney = HjNetHelper.GetPostParam("AddMoney");//充值金额
        string paymodel = HjNetHelper.GetPostParam("paymodel"); //支付方式

        string orderid = "";
        string tnum = userid.ToString("00000");
        orderid = "r" + tnum + DateTime.Now.ToString("yyMMddHHmmss");

        info.AddDate = DateTime.Now;
        info.AddMoney = Convert.ToDecimal(WebUtility.InputText(AddMoney));
        info.Inve1 = 0;
        info.Inve2 = orderid;
        info.PayDate = Convert.ToDateTime("1900-01-01 00:00:00");
        info.PayState = 0;
        info.PayType = 1;
        info.State = 0;
        info.TogoName = "";
        info.UserId = userid;

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


        Response.Write("{\"userid\":\"" + userid + "\",\"state\":\"" + 1 + "\",\"payorderid\":\"" + alipaypayorderid + "\",\"msg\":\"0\"}");
        Response.End();

    }
}