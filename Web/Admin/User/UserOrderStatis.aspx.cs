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

public partial class Admin_User_UserOrderStatis : System.Web.UI.Page
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

    private void BindData(string order)
    {
        SqlWhere = "";
        if (this.tb_Start.Text != "")
        {
            SqlWhere += " and Custorder.OrderDateTime >= '" + this.tb_Start.Text + "' ";
        }
        if (this.tb_End.Text != "")
        {
            SqlWhere += " and Custorder.OrderDateTime <= '" + this.tb_End.Text + " 23:59:59' ";
        }

        rptUserOrderStatis.DataSource = dal_ecustomer.getuser_orderStatis(SqlWhere, order);
        rptUserOrderStatis.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
    }


    protected void btnum_Click(object sender, EventArgs e)
    {
        BindData("num");
    }

    protected void btorderSums_Click(object sender, EventArgs e)
    {
        BindData("orderSums");
    }
}