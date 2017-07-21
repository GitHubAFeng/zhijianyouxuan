/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : aboutus
 * Function : 
 * Created by jijunjian at 2010-10-21 20:02:53.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using System.Web.UI.HtmlControls;
using Hangjing.Control;
using Hangjing.DBUtility;
//using tenpayApp;

/// <summary>
/// 跑腿订单
/// </summary>
public partial class aboutus_express : System.Web.UI.Page
{
    ExpressOrder dal = new ExpressOrder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int cityid = Convert.ToInt32(WebUtility.get_userCityid());
            if (cityid == 0)
            {
                string url = "citys.aspx";
                Response.Redirect(url);
            }
            hfcityname.Value = WebUtility.get_userCityName();
         
            ECustomerInfo user = UserHelp.GetUser();
            if (user != null)
            {
                hiduid.Value = user.DataID.ToString();

                if (string.IsNullOrEmpty(user.PayPassword))
                {
                    pwdmsg.Style["display"] = "";
                    setpaypwd.HRef = "UserHome/PayPwd.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString());
                    tbpaypwd.Disabled = true;
                }
                else
                {
                    pwdmsg.Style["display"] = "none";
                }
            }

          
            List<string> times = new List<string>();
            DateTime now = DateTime.Now;
            now = now.AddHours(1);

            for (int hour = 0; hour <= 23; hour++)
            {
                times.Add(hour.ToString() + ":00");
                times.Add(hour.ToString() + ":30");
            }

            this.ddl_Time1.DataSource = times;
            this.ddl_Time1.DataBind();
            tbdate.Value = DateTime.Now.ToString("yyyy-MM-dd");


            IList<STemplateInfo> list = SectionProxyData.GetExpressServeList();
            WebUtility.BindList("ID", "classname", list, ddlexpressserve);

        }
    }

    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btsave_Click(object sender, EventArgs e)
    {
        ExpressOrderInfo info = new ExpressOrderInfo();
        info.DataID = 0;

        string usercode = WebUtility.FixgetCookie("uc");
        if (usercode == null || usercode == "")
        {
            usercode = WebUtility.GetRandom(10);
            WebUtility.FixsetCookie("uc", usercode, 1);
        }

        string url = "myorder.aspx?isexpress=1";

        ECustomerInfo user = UserHelp.GetUser();
        if (user != null)
        {
            info.UserID = user.DataID;
            info.CustomerName = user.Name;
            url = "User/MyexpressOrder.aspx";
        }
        else
        {
            info.UserID = 0;
            info.CustomerName = "";
        }

        info.UserName = WebUtility.InputText(tbUserName.Value);
        info.Tel = WebUtility.InputText(tbTel.Value);
        info.SentTime = WebUtility.InputText(tbdate.Value) + " " + ddl_Time1.SelectedValue;
        info.Address = WebUtility.InputText(tbAddress.Value);
        info.State = 0;
        info.TogoID = 0;
        info.orderTime = DateTime.Now;

        string orderid = DateTime.Now.ToString("yMMddHHmmss") + WebUtility.GetRandomOnlyNum(4) + info.Tel.Substring(info.Tel.Length - 2, 2);
        info.OrderID = orderid;
        info.SetStateTime = Convert.ToDateTime("1970-1-1");
     
        info.Remark = WebUtility.InputText(tbRemark.Value);
        info.Oorderid = WebUtility.InputText(tbOorderid.Value);
        info.callcount = 0;
        info.callmsg = WebUtility.InputText(tbcallmsg.Value);
        info.writer = "";
        info.PayMode = Convert.ToInt32(rbpaymode.SelectedValue);
        info.paytime = Convert.ToDateTime("1970-1-1");
        info.paystate = 0;
        info.paymoney = 0;

        //生成支付订单编号
        string payorderid = "e" + DateTime.Now.ToString("yyMMddHHmmssff") + HJConvert.GetRandom(4);
        info.PayOrderId = payorderid;
        info.Inve1 = 0;
        info.Inve2 = WebUtility.InputText(tbInve2.Value);
        info.sid = 0;
        info.bid = 0;
        info.tempcode = usercode;
        info.sendmoney = Convert.ToDecimal(WebUtility.InputText(hidsendfee.Value));
        info.TotalPrice = info.sendmoney;
        info.Currentprice = info.sendmoney; 
        info.Cityid = Convert.ToInt32(WebUtility.get_userCityid());
        info.ordersource = 0;
        info.isaddpoint = 0;
        info.sendtype = 0;

        info.ulat = hidtlat.Value;
        info.ulng = hidtlng.Value;
        info.shoplat = hidflat.Value;
        info.shoplng = hidflng.Value;
        info.sitelat = "{'ulat':'" + info.ulat + "','ulng':'" + info.ulng + "','slat':'" + info.shoplat + "','slng':'" + info.shoplng + "'}";
        info.sitelng = "";

        info.ordertype = 0;
        info.noaccess = 0;
        info.validateCode = 0;
        info.iscancel = 0;

        info.ReveInt2 = 0;
        info.ReveVar = WebUtility.InputText(tbReveVar.Value);
        info.ReveDate1 = Convert.ToDateTime("1970-1-1");
        info.ReveDate2 = Convert.ToDateTime("1970-1-1");
        info.IsTimeLimit = 0;
        info.ReveInt1 = Convert.ToInt32(ddlexpressserve.SelectedValue);
        info.servename = ddlexpressserve.SelectedItem.Text;


        ECustomerInfo einfo = new ECustomer().GetModel(user.DataID);
        decimal useroney = einfo.Usermoney;
        if (info.PayMode ==3)
        {
            if (useroney == 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "alert('您的账户余额为0，不能选择账户余额支付！');hideload_super();");
                return;
            }
            if (useroney < info.TotalPrice)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "alert('您的账户余额不足请及时充值，暂不能选择账户余额支付！');hideload_super();");
                return;
            }

            if (WebUtility.GetMd5(tbpaypwd.Value.Trim()) != einfo.PayPassword)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "alert('支付密码错误，请重新输入！');payusermoney(3);hideload_super();");
                //this.tbpaypwd.Style["display"] = "";
                return;
            }
        }

        IList<ROrderinfo> mylist = dal.submitorder(info);
        if (mylist != null)
        {
            Session["expressorderinfo"] = mylist;

            if (info.PayMode == 1)
            {
                string show_url = WebUtility.GetConfigsite() + "/index.aspx";
                AliPayInfo alipa = new AliPayInfo(payorderid, SectionProxyData.GetSetValue(7), SectionProxyData.GetSetValue(7), mylist[0].Currentprice.ToString(), show_url, "", "");
                AliPay.Pay(alipa);
            }
            else if (info.PayMode == 5)
            {
               // TenpayUtil tenpay = new TenpayUtil();
               // Response.Redirect(TenpayUtil.tenpay_show + "?porderid=" + mylist[0].PayOrderId + "&price=" + mylist[0].Currentprice.ToString() + "&productname=" + SectionProxyData.GetSetValue(7));      
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "$.jBox.prompt('订单提交成功，请耐心等待!', '提示', 'info', { closed: function () { gourl('" + url + "'); } });hideload_super();");
            }
        }
        else
        {

            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:对不起，服务器繁忙，请与我们客服联系!','250','150','true','2000','true','text');hideload_super();;");
        }


    }

}
