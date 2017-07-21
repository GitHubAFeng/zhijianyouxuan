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

public partial class qy_54tss_AreaAdmin_aboutus_makelist : System.Web.UI.Page
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

    Practice dal = new Practice();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlWhere = "1=1";
            if (Request["tid"] != null)
            {
                SqlWhere += " and inve1 = "+Request["tid"];
            }
            BindData();
        }
    }

    private void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rtpGifts.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "pid", 1);
        this.rtpGifts.DataBind();
        AlertScript.RegScript(this.Page, UpdatePanel1, "init();");
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = " 1=1 ";
        if (Request["tid"] != null)
        {
            SqlWhere += " and inve1 = " + Request["tid"];
        }
        if (tbKeyword.Text.Trim() != "")
        {
            SqlWhere += " and  (pnum like '%" + WebUtility.InputText(tbKeyword.Text) + "%' or pname like '%" + WebUtility.InputText(tbKeyword.Text) + "%' or namepy like '%" + WebUtility.InputText(tbKeyword.Text) + "%')";
        }
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        BindData();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        string IdList = this.hidDels.Value;
        if (dal.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败','error','true',5);init();");
        }
    }

    protected void rtpGifts_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            if (dal.DelPractice(Convert.ToInt32(e.CommandArgument)) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
                BindData();
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败','error','true',5);init();");
            }
        }
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        Response.Redirect("makedetail.aspx?tid=" + HjNetHelper.GetQueryInt("tid", 0));
    }
}
