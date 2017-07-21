
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

/// <summary>
/// 配送员分组管理
/// </summary>
public partial class qy_54tss_Admin_delivergroup : System.Web.UI.Page
{
    DeliverGroup dal = new DeliverGroup();

    protected string SqlWhere
    {
        get
        {
            return (ViewState["sqlWhere"] == null) ? "" : ViewState["sqlWhere"].ToString();
        }
        set
        {
            ViewState["sqlWhere"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {
            if (Request["name"] != null)
            {
                mycityname.InnerText = "区域管理("+Server.UrlDecode(Request["name"].ToString())+")";
            }
            SqlWhere = "1=1 and parentid = 0";
            if (Request["id"] != null)
            {
                SqlWhere += " and Status = " + Request["id"];
            }
            WebUtility.SetDDLCity(DDLArea);
            WebUtility.SetDDLCity(ddlcity);
            BindData();
        }
    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    private void BindData()
    {
        AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        rptsubItem.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "Status desc,Priority", 1);
        rptsubItem.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
        SectionProxyData.ClearEdelivergroupList();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(4);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (dal.DelList(e.CommandArgument.ToString()) > 0)
            {
                AlertScript.RegScript(this, UpdatePanel1, "jtip('删除成功！');init();");
                BindData();
            }
            else
            {
                AlertScript.RegScript(this, UpdatePanel1, "jtip('删除失败，请重试！');init();");
            }
        }
    }

    protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        rptsubItem.EditIndex = e.NewEditIndex;
        BindData();
    }

    protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        rptsubItem.EditIndex = -1;
        BindData();
    }

    protected void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        
        string classname = ((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_classname")).Text.Trim();
        int pri = Convert.ToInt32(((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_priority")).Text.Trim());
        int id = Convert.ToInt32(((Label)rptsubItem.Rows[e.RowIndex].FindControl("lb_id")).Text.Trim());
        DeliverGroupInfo mm = dal.GetModel(id);
        mm.classname = classname;
        mm.Priority = pri;
        mm.ID = id;
        mm.Parentid = 0;
        //判断权限
        int _rs = WebUtility.checkOperator(3);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        if (dal.Update(mm) > 0)
        {
            AlertScript.RegScript(Page, UpdatePanel1, "jtip('操作成功！');init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(Page, UpdatePanel1, "jtip('操作失败，请重试！');init();");
        }
        rptsubItem.EditIndex = -1;
        BindData();
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        DeliverGroupInfo mm = new DeliverGroupInfo();
        string classname = WebUtility.InputText(tbclassname.Text);
        if (classname != "")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            mm.classname = classname;
            mm.Priority = Convert.ToInt32(tbpri.Text);
            mm.Status = Convert.ToInt32(DDLArea.SelectedValue);
            mm.Parentid = 0;
            if (dal.Add(mm) > 0)
            {
                AlertScript.RegScript(Page, UpdatePanel1, "jtip('添加成功！');init();");
                tbclassname.Text = "";
                BindData();
            }
            else
            {
                AlertScript.RegScript(Page, UpdatePanel1, "jtip('添加失败，请重试！');init();");
            }
        }
        else
        {
            AlertScript.RegScript(Page, UpdatePanel1, "jtip('区域名不能为空！');init();");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = "1=1 and parentid = 0";
        if (tbKeyword.Text != "")
        {
            SqlWhere += " and  className like '%" + WebUtility.InputText(this.tbKeyword.Text) + "%' ";
        }
        if (ddlcity.SelectedValue != "0")
        {
            SqlWhere += " and Status = " + ddlcity.SelectedValue + "";
        }
        BindData();
    }
}
