using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Model;

namespace Html5
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        ECustomer dal = new ECustomer();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.HttpMethod.ToLower() == "post")
            {

                ECustomerInfo model = new ECustomerInfo();
                string Tell = WebUtility.InputText(Request["tbmobile"]);

                string url = "";
                string cookie_gsmcode = WebUtility.FixgetCookie("gsmcode");
                string phonevalid = Request.Form["tbphonevalid"];
                if (cookie_gsmcode == null || cookie_gsmcode.ToString() != phonevalid.Trim())
                {
                    url = "~/ForgotPassword.aspx?tel=" + Tell + "&tip=1";
                    Response.Redirect(url);
                    return;
                }

                string sql = "Tell = '" + Tell + "'";
                int count = dal.GetCount(sql);
                if (count == 0)
                {
                    url = "~/ForgotPassword.aspx?tel=" + Tell + "&tip=2";
                    Response.Redirect(url);

                    return;

                }
                WebUtility.FixsetCookie("phonevalid", "1", 1);

                url = "~/resetpwd.aspx?tell=" + Tell;

                Response.Redirect(url);


            }

        }



    }
}