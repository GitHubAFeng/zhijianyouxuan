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
using Hangjing.SQLServerDAL;

public partial class shop_TogoCommentList : System.Web.UI.Page
{
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
            UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
            SqlWhere = "togoid= '" + UserHelp.GetUser_Togo().Unid + "'";
            BindData();
        }
    }

    Hangjing.SQLServerDAL.ETogoOpinion dal = new Hangjing.SQLServerDAL.ETogoOpinion();

    void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptTogoOpinionList.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "PostTime", 1);
        this.rptTogoOpinionList.DataBind();
        NoRecord();
    }

    private void NoRecord()
    {
        if (rptTogoOpinionList.Items.Count == 0)
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

    protected void rptTogoOpinionList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            if (dal.DelETogoOpinion(Convert.ToInt32(e.CommandArgument)) > 0)
            {
                AlertScript.RegScript(this, "tipsWindown('提示信息','text:删除成功!','250','150','true','','true','text');location.href=location.href;");
                BindData();
            }
            else
            {
                AlertScript.RegScript(this, "tipsWindown('提示信息','text:删除失败!','250','150','true','','true','text');");
            }
        }
        else
        {
            Response.Redirect("CommentDetail.aspx?id=" + Convert.ToInt32(e.CommandArgument));
        }
    }

    protected string isSee(object time)
    {
        string rs = "否";
        if (time == "")
        {
            //todo;
        }
        DateTime d = Convert.ToDateTime(time);
        if (d > Convert.ToDateTime("1900-01-01"))
        {
            rs = "是";
        }
        return rs;
    }
}
