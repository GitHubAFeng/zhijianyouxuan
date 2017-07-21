using System;
using System.Collections.Generic;
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

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.SQLServerDAL;

public partial class OrderSuccess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        int isreg = HjNetHelper.GetQueryInt("isreg", 0);
        if (isreg == 1)
        {
            string msg = "系统已经自动为您注册了帐号（手机号）：<span class=\"f24 orange\">" + HjNetHelper.GetQueryString("tel") + "</span>，默认密码：<span class=\"f24 orange\">123456。</span>，请登录后及时修改";
            regnotice.InnerHtml = msg;
        }


        IList<ROrderinfo> list = (IList<ROrderinfo>)Session["orderinfo"];
        if (list != null && list.Count > 0)
        {
            string orders = "";
            decimal p = 0;
            decimal accountpay = 0;
            decimal allprice = 0;
            decimal cardpay = 0;
            for (int i = 0; i < list.Count; i++)
            {
                orders += "  " + list[i].Orderid;
                p += list[i].Currentprice;
                allprice += list[i].allprice;
                accountpay += list[i].accountpay;
                cardpay += list[i].cardpay; ;

                lbpromotion.InnerText = list[i].promotionmoney.ToString();
            }

            //只提交一家的订单
            orderid.InnerHtml = orders;
            Custorder dalorder = new Custorder();
            CustorderInfo infoorder = dalorder.GetModel(list[0].Orderid);
            payway.InnerHtml = WebUtility.TurnPayModel(infoorder.paymode);
            paymoney.InnerHtml = (allprice).ToString();
            lbcardpay.InnerHtml = (cardpay).ToString();



            Points dal = new Points();
            PointsInfo info = dal.GetModel(Convert.ToInt32(list[0].togoid));
            sendtime.InnerHtml = info.senttime.ToString();

            if (UserHelp.GetUser() == null)
            {
                orderlink.HRef = "/myorder.aspx";
            }

            if (Request["m"] != null && Request["m"].ToString() == "1" && p > 0)
            {

                PayOrderLog dalpaylog = new PayOrderLog();
                string alipaypayorderid = dalpaylog.GetPayBatch(infoorder.orderid); ;

                /*********************准备去支付 添加支付日志********************************/
                PayOrderLogInfo apyinfo = new PayOrderLogInfo();
                apyinfo.OrderId = infoorder.orderid;
                apyinfo.AddTime = DateTime.Now;
                apyinfo.Batch = alipaypayorderid;
                apyinfo.Price = p;
                apyinfo.PayType = 0;
                apyinfo.PayTime = Convert.ToDateTime("1900-1-1");
                apyinfo.State = 0;
                apyinfo.PayCallTime = Convert.ToDateTime("1900-1-1");
                apyinfo.Remark = "";
                apyinfo.Reve1 = "1";
                apyinfo.Reve2 = "";
                dalpaylog.Add(apyinfo);
                /*********************添加支付日志 over********************************/


                string show_url = WebUtility.GetConfigsite() + "";
                AliPayInfo alipa = new AliPayInfo(alipaypayorderid, "orderpay", "hj", p.ToString(), show_url, "", "");
                AliPay.Pay(alipa);
            }
        }



    }

}
