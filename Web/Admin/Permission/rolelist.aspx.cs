using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;

public partial class Admin_Permission_rolelist : System.Web.UI.Page
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

    sys_Roles dal = new sys_Roles();

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {
            BindData();
            if (Request["id"] != null)
            {
                lbadd.InnerHtml = "编辑角色";
            }
            else
            {
                lbadd.InnerHtml = "添加角色";
            }
        }
    }

    private void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptsubItem.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "RoleID", 1);
        this.rptsubItem.DataBind();
    }


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
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
                //删除sys_RolePermission记录
                new sys_RolePermission().DelRolePermissionByRid(Convert.ToInt32(e.CommandArgument.ToString()));
                //更新缓存(所有角色的)
                Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/Permissions");
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
        //判断权限
        int _rs = WebUtility.checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        sys_RolesInfo mm = new sys_RolesInfo();
        string classname = ((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_classname")).Text.Trim();
        string des = ((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_priority")).Text.Trim();
        int id = Convert.ToInt32(((Label)rptsubItem.Rows[e.RowIndex].FindControl("lb_id")).Text.Trim());
        mm.R_RoleName = classname;
        mm.R_Description = des;
        mm.RoleID = id;

        if (dal.Update(mm) > 0)
        {
            AlertScript.RegScript(Page, UpdatePanel1, "jtip('刪除成功！');init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(Page, UpdatePanel1, "jtip('刪除失敗，请重试！');init();");
        }
        rptsubItem.EditIndex = -1;
        BindData();
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(2);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        sys_RolesInfo mm = new sys_RolesInfo();
        mm.R_RoleName = WebUtility.InputText(tbclassname.Text);
        mm.R_Description = tbpri.Text.Trim();
        if (dal.Add(mm) > 0)
        {
            AlertScript.RegScript(Page, UpdatePanel1, "jtip('添加成功！');init();");
            tbclassname.Text = "";
            tbpri.Text = "";
            BindData();
        }
        else
        {
            AlertScript.RegScript(Page, UpdatePanel1, "jtip('添加失敗，请重试！');init();");
        }
    }
}
