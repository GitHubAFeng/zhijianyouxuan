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
using Hangjing.Common;
using Hangjing.Model;


public partial class UserHome_CommentList : PageBase
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
        if (!this.Page.IsPostBack)
        {
            if (UserHelp.IsLogin())
            {
                SqlWhere = "userid ='" + UserHelp.GetUser().DataID + "'";
                BindData();
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }
    }

    ETogoOpinion dal = new ETogoOpinion(); 
    void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptComment.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "PostTime", 1);
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

}
