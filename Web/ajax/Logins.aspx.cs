using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;


public partial class ajax_Logins : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ECustomer dal = new ECustomer();
        ECustomerInfo model = new ECustomerInfo();
      
            string password = WebUtility.GetMd5(HjNetHelper.GetQueryString("pass"));
            model = dal.GetModelByNameAPassword(HjNetHelper.GetQueryString("name"),password);
            if (model != null)
            {
                UserHelp.SetLogin(model);
                Response.Write(model.Name+","+model.Point);
                Response.End();

            }
            else
            {
                Response.Write("-1");
                Response.End();
            }
      
    }
}
