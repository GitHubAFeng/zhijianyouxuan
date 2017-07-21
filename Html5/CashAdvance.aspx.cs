using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Html5
{
    public partial class CashAdvance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ECustomerInfo user = UserHelp.GetUser();
            if (user == null)
            {
                Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }

            user = new ECustomer().GetModel(user.DataID);
            hfuserid.Value = user.DataID.ToString();

            lbhavemoney.InnerText = user.GroupID.ToString();
        }
    }
}