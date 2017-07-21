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
    public partial class moneylist : System.Web.UI.Page
    {
        msgpacketInfo model = new msgpacketInfo();
        msgpacket dal = new msgpacket();
        protected void Page_Load(object sender, EventArgs e)
        {
            ECustomerInfo user = UserHelp.GetUser();
            if (user == null)
            {
                Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            else
            {
                string sqlwhere = "1=1 and ReveVar='" + user.Tell + "' and num=0 and validitytime>'" + DateTime.Now + "'";
                IList<msgpacketInfo> list = dal.GetList(999, 1, sqlwhere, "id", 1);

                WebUtility.BindRepeater(rptpromotion, list);


            }
        }
        protected int getdate(DateTime day)
        {
            int d = day.Subtract(DateTime.Now).Days; 
            return d;
        }
    }
}