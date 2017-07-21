using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;


namespace Html5
{
    public partial class m_recharge : System.Web.UI.Page
    {

        ECustomer dal = new ECustomer();
        protected void Page_Load(object sender, EventArgs e)
        {
            //错误信息
            int err = HjNetHelper.GetQueryInt("err", 0);
            ECustomerInfo user = UserHelp.GetUser();
            if (user == null)
            {
                Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }


            ECustomerInfo carduser = new ECustomer().GetModel(user.DataID);
            lbusermoney.InnerText = carduser.Usermoney.ToString() + "";


            if (Request.HttpMethod.ToUpper() == "POST")
            {
                add_Click();
            }

            WebUtility.BindRepeater(rptordersouce, CacheHelper.GetOrderSourceList());
        }

        /// <summary>
        /// 提交修改密码信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void add_Click()
        {
            string rid = WebUtility.InputText(Request.Form["ddlpaymode"]);

            ECustomerInfo user = UserHelp.GetUser();
            string orderid = "";
            string tnum = user.DataID.ToString("00000");
            orderid = "r" + tnum + DateTime.Now.ToString("yyMMddHHmmss");

   
            string url = "/wxpay.aspx?orderid=" + orderid + "&showwxpaytitle=1";
            url += "&price=0&rid=" + rid;
            url = "/weixinpay/" + url;

            Response.Redirect(url);

        }
    }
}

