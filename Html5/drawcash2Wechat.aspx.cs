using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System;

namespace Html5
{
    /// <summary>
    /// 提现到微信账户
    /// </summary>
    public partial class drawcash2Wechat : System.Web.UI.Page
    {
        public int userid = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            ECustomerInfo user = UserHelp.GetUser();
            if (user == null)
            {
                Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }

            string isopen = SectionProxyData.GetSetValue(77).Trim();
            if (isopen != "1")
            {
                Response.Redirect("drawcash.aspx");
            }

            user = new ECustomer().GetModel(user.DataID);
            lbhavemoney.InnerText = user.Usermoney.ToString();
            hfhavemoney.Value = user.Usermoney.ToString();
            userid = user.DataID;


        }

    }
}