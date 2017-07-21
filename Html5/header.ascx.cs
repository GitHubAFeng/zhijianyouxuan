using Hangjing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Html5
{
    public partial class header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ECustomerInfo user = UserHelp.GetUser();
            if (user != null)
            {
                IList<ECustomerInfo> userlist = new List<ECustomerInfo>();
                userlist.Add(user);

                WebUtility.BindRepeater(rptppt, userlist);
            }


        }
    }
}