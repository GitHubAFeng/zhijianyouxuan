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

public partial class Admin_User_UserActivityStatis : System.Web.UI.Page
{
    ECustomer dal_ecustomer = new ECustomer();

    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? string.Empty : o.ToString();
        }
        set
        {
            ViewState["SqlWhere"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
    }

    private void BindData(int day)
    {
        rptUserActivityStatis.DataSource = dal_ecustomer.getuser_ActivityStatis(day);
        rptUserActivityStatis.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        BindData(3);
    }

    protected void btSearchWork_Click(object sender, EventArgs e)
    {
        BindData(7);
    }

    protected void btSearchMonth_Click(object sender, EventArgs e)
    {
        BindData(30);
    }

    protected void btday_Click(object sender, EventArgs e)
    {
        BindData(0);
    }
}
