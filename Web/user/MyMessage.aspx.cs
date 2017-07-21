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

public partial class UserHome_MyMessage : PageBase
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
                SqlWhere = "userid ='" + UserHelp.GetUser().DataID + "'";
                BindData();
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }
    }

    EUserWord dal = new EUserWord();

    void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptComment.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "time", 1);
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

    protected string StateToStr(object x)
    {
        int d = Convert.ToInt32(x);
        if (d == 0)
        {
            return "正在审核";
        }
        else
        {
            return "通过审核";
        }
    }

    protected void test(object sender , EventArgs e)
    {
        //if (0==1)
        //{
        //    this.comment.Style.Add(HtmlTextWriterStyle.Display, "block");
        //    Response.Write("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //}
        //else
        //{
        //    this.comment.Style.Add(HtmlTextWriterStyle.Display, "none");
        //}
    }
}
