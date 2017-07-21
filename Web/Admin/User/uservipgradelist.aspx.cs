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
using Hangjing.Cache;

public partial class Admin_shop_uservipgradelist : System.Web.UI.Page
{
    VipGrade dal = new VipGrade();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            getItem();
        }
    }

    protected void getItem()
    {
        IList<VipGradeInfo> list = dal.GetList(100, 1, "1=1", "Reve1", 1);
        rptsubItem.DataSource = list;
        rptsubItem.DataBind();
        SectionProxyData.ClearUserGradeList();
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
        VipGradeInfo info = new VipGradeInfo();
        info.GradeName = ((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_classname")).Text.Trim();
        info.Reve1 = Convert.ToInt32(((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_priority")).Text.Trim());
        info.DataID = Convert.ToInt32(((Label)rptsubItem.Rows[e.RowIndex].FindControl("lb_id")).Text.Trim());
        info.GaiPoint = Convert.ToInt32(((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_GaiPoint")).Text.Trim());
        //info.MinPoint = Convert.ToInt32(((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_MinPoint")).Text.Trim());
        //info.MaxPoint = Convert.ToInt32(((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_MaxPoint")).Text.Trim());
        info.MinPoint = 0;
        info.MaxPoint = 0;
        //判断权限
        int _rs = WebUtility.checkOperator(3);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        if (dal.UpdateName(info) > 0)
        {
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(2);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        VipGradeInfo info = new VipGradeInfo();
        info.GradeName = WebUtility.InputText(tbclassname.Text);
        //info.MinPoint = WebUtility.InputText(tbMinPoint.Text ,true);
        //info.MaxPoint = WebUtility.InputText(tbMaxPoint.Text, true);
        info.MinPoint = 0;
        info.MaxPoint = 0;
        info.vRat = 0;
        info.GaiPoint = 0;
        info.Reve1 = Convert.ToInt32(tbpri.Text);
        info.Reve2 = "";
        //判断权限
        if (dal.Add(info) > 0)
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
}

