using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

namespace Html5
{
    public partial class submitexpressorder : System.Web.UI.Page
    {
        ExpressOrder dal = new ExpressOrder();
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取post信息，提交订单
            if (Request.HttpMethod.ToUpper() == "POST")
            {
                add_Click();
            }

            //判断是否 登录
            ECustomerInfo model = UserHelp.GetUser();

            if (model == null)
            {
                Response.Redirect("login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            else
            {
                model = new ECustomer().GetModel(model.DataID);
                hfuid.Value = model.DataID.ToString();
                //lbmymoney.InnerText = model.Usermoney + "元";
            }



        }

        /// <summary>
        /// submit
        /// </summary>
        public void add_Click()
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

            expressinfo expressmodel = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<expressinfo>(Request.Form["tbexpressinfo"]);

            info.UserName = WebUtility.InputText(expressmodel.tbUserName);
            info.Tel = WebUtility.InputText(expressmodel.tbTel);
            info.SentTime = WebUtility.InputText(expressmodel.tbSentTime);
            info.Address = WebUtility.InputText(expressmodel.tbAddress+","+ expressmodel.tbAddressdetail);
            info.State = 0;
            info.TogoID = 0;
            info.orderTime = DateTime.Now;

            string orderid = DateTime.Now.ToString("yMMddHHmmss") + info.Tel.Substring(info.Tel.Length - 4, 4);
            info.OrderID = orderid;
            info.SetStateTime = Convert.ToDateTime("1970-1-1");

            info.Remark = WebUtility.InputText(expressmodel.tbRemark);
            info.Oorderid = WebUtility.InputText(expressmodel.tbOorderid+","+ expressmodel.tbOorderiddetail);
            info.callcount = 0;
            info.callmsg = WebUtility.InputText(expressmodel.tbcallmsg);
            info.ReveVar = WebUtility.InputText(expressmodel.tbReveVar);
            info.writer = "";
            info.PayMode = Convert.ToInt32(WebUtility.InputText(Request.Form["ddlpaymode"]));
            info.paytime = Convert.ToDateTime("1970-1-1");
            info.paystate = 0;
            info.paymoney = 0;

            //生成支付订单编号
            info.PayOrderId = orderid;
            info.Inve1 = 0;
            info.Inve2 = WebUtility.InputText(Request.Form["tbInve2"]);
            info.sid = 0;
            info.bid = 0;
            info.tempcode = usercode;
            info.sendmoney = Convert.ToDecimal(expressmodel.hidsendfee);
            info.TotalPrice = Convert.ToDecimal(expressmodel.tbTotalPrice);
            info.Currentprice = Convert.ToDecimal(expressmodel.hiddistance);
            info.Cityid = Convert.ToInt32(expressmodel.cityid);
            info.ordersource = 7;
            info.isaddpoint = 0;
            info.sendtype = 0;

            info.ulat = WebUtility.InputText(expressmodel.hidflat);
            info.ulng = WebUtility.InputText(expressmodel.hidflng);
            info.shoplat = WebUtility.InputText(expressmodel.hidtlat);
            info.shoplng = WebUtility.InputText(expressmodel.hidtlng);
            info.sitelat = "{'ulat':'" + info.ulat + "','ulng':'" + info.ulng + "','slat':'" + info.shoplat + "','slng':'" + info.shoplng + "'}";
            info.sitelng = "";
            info.callcount = Convert.ToInt32(expressmodel.tbcallcount);
            info.ordertype = 0;
            info.noaccess = 0;
            info.validateCode = 0;
            info.iscancel = 0;
            info.ReveInt2 = 0;
            info.ReveDate1 = Convert.ToDateTime("1970-1-1");
            info.ReveDate2 = Convert.ToDateTime("1970-1-1");
            info.IsTimeLimit = 0;
            info.ReveInt1 = 0;
            info.servename = expressmodel.tbcallcount;

            ECustomerInfo einfo = new ECustomer().GetModel(user.DataID);
            lbmymoney.InnerText = einfo.Usermoney + "元";
            decimal useroney = einfo.Usermoney;
            if (info.PayMode == 3)
            {
                string PayPassword = WebUtility.InputText(Request.Form["tbpaypwd"]);
                if (useroney == 0)
                {
                    Response.Redirect("submitexpressorder.aspx?id=" + 0 + "&msg=2");//您的账户余额为0，不能选择账户余额支付
                    return;
                }
                //可以支付部分金额
                if (useroney < info.sendmoney)
                {
                    Response.Redirect("submitexpressorder.aspx?id=" + 0 + "&msg=6");//余额不足，请选择其他支付方式
                    return;
                }

                if (WebUtility.GetMd5(PayPassword) != user.PayPassword)
                {
                    Response.Redirect("expressOrderSuccess.aspx?id=" + 0 + "&msg=3");//支付密码错误，请重新输入
                    return;
                }
            }

            int oid = dal.Add(info);
            if (oid > 0)
            {
             
                NoticeHelper notice = new NoticeHelper(HttpContext.Current);
                notice.send2All(info.Cityid);
                Response.Redirect("expressOrderSuccess.aspx?orderid=" + info.OrderID);
            }
            else
            {
                Response.Redirect("expressOrderSuccess.aspx?id=" + 0 + "&msg=1");//失败
            }


        }

    }
}