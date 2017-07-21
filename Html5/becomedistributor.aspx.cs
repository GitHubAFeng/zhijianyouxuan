using Hangjing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Html5
{
    public partial class becomedistributor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ECustomerInfo user = UserHelp.GetUser();
            if (user == null)
            {
                Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            else
            {
                string orderid = "d" + WebUtility.GetTime()+"_"+user.DataID;
                spanorderid.InnerText = orderid;
                hforderid.Value = orderid;
            }
        }
    }
}