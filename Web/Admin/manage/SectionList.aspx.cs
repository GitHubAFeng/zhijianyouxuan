//----------------------------------------------------------------------
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :
// Created by zhangxiaoliang at 2010-10-22 14:58:38.
// E-Mail: zhangxiaoliang@Ihangjing.com
//----------------------------------------------------------------------
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

public partial class qy_54tss_Admin_SectionList : System.Web.UI.Page
{
    ESection dal = new ESection();

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
                SqlWhere += " and cityid = " + Request["id"];
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
        rptsubItem.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "cityid desc,pri", 1);
        rptsubItem.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
        Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/section");
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
        SectionInfo mm = new SectionInfo();
        string classname = ((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_classname")).Text.Trim();
        int pri = Convert.ToInt32(((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_priority")).Text.Trim());
        int id = Convert.ToInt32(((Label)rptsubItem.Rows[e.RowIndex].FindControl("lb_id")).Text.Trim());
        mm.SectionName = classname;
        mm.pri = pri;
        mm.SectionID = id;
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
        SectionInfo mm = new SectionInfo();
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
            mm.SectionName = classname;
            mm.pri = Convert.ToInt32(tbpri.Text);
            mm.cityid = Convert.ToInt32(DDLArea.SelectedValue);
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
            SqlWhere += " and  SectionName like '%" + WebUtility.InputText(this.tbKeyword.Text) + "%' ";
        }
        if (ddlcity.SelectedValue != "0")
        {
            SqlWhere += " and cityid = " + ddlcity.SelectedValue + "";
        }
        BindData();
    }
}
