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

public partial class UserHome_MyPointCount : System.Web.UI.Page
{
    EPointRecord dal = new EPointRecord();
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
        if (!this.Page.IsPostBack)
        {
            if (UserHelp.IsLogin())
            {
                SqlWhere = "UserId=" + UserHelp.GetUser().DataID.ToString() + "";
                BindDate();
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }
    }

    void BindDate()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptPointCount.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "DataID" ,1);
        this.rptPointCount.DataBind();
        NoRecord();
    }

    private void NoRecord()
    {
        if (rptPointCount.Items.Count == 0)
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "block");
        }
        else
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindDate();
    }
}
