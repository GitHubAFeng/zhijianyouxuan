/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : Admin_shop_shopdatalist
 * Function : 
 * Created by jijunjian at 2011-1-10 15:02:03.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class qy_54tss_AreaAdmin_active_DeliverGroupList : System.Web.UI.Page
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

        if (!Page.IsPostBack)
        {
            SqlWhere = "1=1 and parentid = 0";
            WebUtility.SetDDLCity(DDLArea);
            WebUtility.SetDDLCity(ddlcity);
            getItem();
        }
    }

    protected void getItem()
    {
        AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        rptsubItem.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "status desc,priority", 1);
        rptsubItem.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
        SectionProxyData.ClearEdelivergroupList();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            if (dal.DelList(e.CommandArgument.ToString()) > 0)
            {
                AlertScript.RegScript(this, UpdatePanel1, "jtip('删除成功！');init();");
                getItem();
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
        getItem();
    }

    protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        rptsubItem.EditIndex = -1;
        getItem();
    }

    protected void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string classname = ((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_classname")).Text.Trim();
        int pri = Convert.ToInt32(((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_priority")).Text.Trim());
        int id = Convert.ToInt32(((Label)rptsubItem.Rows[e.RowIndex].FindControl("lb_id")).Text.Trim());
        int isDel = Convert.ToInt32(((DropDownList)(rptsubItem.Rows[e.RowIndex].FindControl("drpNum"))).Text.Trim());
        if (dal.updateName(id, classname, pri, isDel) > 0)
        {
            SectionProxyData.ClearEdelivergroupList();
            AlertScript.RegScript(Page, UpdatePanel1, "jtip('编辑成功！');init();");
            getItem();
        }
        else
        {
            AlertScript.RegScript(Page, UpdatePanel1, "jtip('编辑失败，请重试！');init();");
        }
        rptsubItem.EditIndex = -1;
        getItem();
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        DeliverGroupInfo mm = new DeliverGroupInfo();
        mm.Parentid = HjNetHelper.GetQueryInt("id", 0);
        mm.Status = Convert.ToInt32(ddlcity.SelectedValue);
        mm.isDel = 0;
        mm.classname = WebUtility.InputText(tbclassname.Text);
        mm.Depth = 2;
        mm.Priority = Convert.ToInt32(tbpri.Text);
        if (dal.Add(mm) > 0)
        {
            AlertScript.RegScript(Page, UpdatePanel1, "jtip('添加成功！');init();");
            tbclassname.Text = "";
            getItem();
        }
        else
        {
            AlertScript.RegScript(Page, UpdatePanel1, "jtip('添加失败，请重试！');init();");
        }

    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        getItem();
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = "1=1 and parentid = 0";
        if (tbKeyword.Text != "")
        {
            SqlWhere += " and  Classname like '%" + WebUtility.InputText(this.tbKeyword.Text) + "%' ";
        }
        if (DDLArea.SelectedValue != "0")
        {
            SqlWhere += " and Status = " + DDLArea.SelectedValue + "";
        }
        getItem();
    }
}

