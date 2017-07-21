using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System;

namespace Html5
{
    public partial class drawcash : System.Web.UI.Page
    {
        public int userid = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            ECustomerInfo user = UserHelp.GetUser();
            if (user == null)
            {
                Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }

            user = new ECustomer().GetModel(user.DataID);
            lbhavemoney.InnerText = user.Usermoney.ToString();
            hfhavemoney.Value = user.Usermoney.ToString();
            userid = user.DataID;

            userCashAcountInfo account = new userCashAcount().GetModelByUser(user.DataID);
            if (account != null)
            {
                hfjsonddata.Value = Newtonsoft.Json.JsonConvert.SerializeObject(account);
            }

        }

    }
}