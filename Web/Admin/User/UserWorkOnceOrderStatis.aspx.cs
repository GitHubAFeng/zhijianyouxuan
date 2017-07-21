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

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;


public partial class Admin_User_UserWorkOnceOrderStatis : System.Web.UI.Page
{
    ECustomer dal_ecustomer = new ECustomer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        rptUserWorkOnceOrderStatis.DataSource = dal_ecustomer.getuser_workOnceOrderStatis();
        rptUserWorkOnceOrderStatis.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
    }
}