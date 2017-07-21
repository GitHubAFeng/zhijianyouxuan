using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class UserHome_mychange : System.Web.UI.Page
{
    private string SqlWhere
    {
        get
        {
            return ViewState["where"] == null ? "" : ViewState["where"].ToString();
        }
        set
        {
            ViewState["where"] = (object)value;
        }
    }

    public string ImagePath = WebUtility.GetMasterPicturePath();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin(Request.Url.PathAndQuery);
        if (!this.Page.IsPostBack)
        {
            if (UserHelp.IsLogin())
            {
                SqlWhere = "custid ='" + UserHelp.GetUser().DataID + "'";
                BindData();
            }
        }
    }

    Integral dal = new Integral();

    void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptComment.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "IntegralId", 1);
        this.rptComment.DataBind();
        NoRecord();
    }

    private void NoRecord()
    {
        if (rptComment.Items.Count == 0)
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
        BindData();
    }

    protected string GetState(object o)
    {
        int state = Convert.ToInt32(o);
        string value = "";
        switch (state)
        {
            case 0: value = "<span style=\"color:Gray;\">未审核</span>"; break;
            case 1: value = "<span style=\"color:Green;\">审核通过</span>"; break;
            case 2: value = "<span style=\"color:Red;\">审核未通过</span>"; break;
        }

        return value;
    }
}
