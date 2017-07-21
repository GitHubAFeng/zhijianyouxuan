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
    public partial class registerdetail :  PageBase
    {
        ECustomer userBLL = new ECustomer();
        protected void Page_Load(object sender, EventArgs e)
        {
            gocart.HRef = "login.aspx";

            string state = SectionProxyData.GetSetValue(39);
            if (state == "0")
            {
                this.div_gsmCode.Style["display"] = "none";
            }
        }

    }
}
