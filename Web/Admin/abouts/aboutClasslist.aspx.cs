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

public partial class Admin_abouts_aboutClasslist :AdminPageBase
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

    aboutClass dal = new aboutClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindData();
            if (Request["id"] != null)
            {
                lbadd.InnerHtml = "编辑分类";
            }
            else
            {
                lbadd.InnerHtml = "添加分类";
            }
        }
    }

    private void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptsubItem.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "FullId", 1);
        this.rptsubItem.DataBind();
    }


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
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
            int id = Convert.ToInt32(e.CommandArgument);
            if (id <= 2)
            {
                AlertScript.RegScript(this, UpdatePanel1, "alert('此数据是系统默认数据，不能删除！');init();");
                return;
            }

            if (dal.DelList(e.CommandArgument.ToString()) > 0)
            {
                AlertScript.RegScript(this, UpdatePanel1, "jtip(''删除成功！');init();");
                BindData();
            }
            else
            {
                AlertScript.RegScript(this, UpdatePanel1, "jtip(''删除失败，请重试！');init();");
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
        aboutClassInfo mm = new aboutClassInfo();
        string classname = ((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_classname")).Text.Trim();
        int pri = Convert.ToInt32(((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_priority")).Text.Trim());
        int id = Convert.ToInt32(((Label)rptsubItem.Rows[e.RowIndex].FindControl("lb_id")).Text.Trim());
        mm.Name = classname;
        mm.FullId = pri;
        mm.Id = id;
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
        //判断权限
        int _rs = WebUtility.checkOperator(2);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        aboutClassInfo mm = new aboutClassInfo();
        mm.ParentId = 0;
        mm.Name = WebUtility.InputText(tbclassname.Text);
        mm.FullId = Convert.ToInt32(tbpri.Text);
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
}
