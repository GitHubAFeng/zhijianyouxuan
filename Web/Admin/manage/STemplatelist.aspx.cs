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

public partial class qy_54tss_Admin_shop_STemplatelist : System.Web.UI.Page
{
    STemplate dal = new STemplate();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            getItem();
        }
    }

    protected void getItem()
    {
        int type = HjNetHelper.GetQueryInt("type", 0);
        IList<STemplateInfo> list = dal.GetsubList(0, type);
        rptsubItem.DataSource = list;
        rptsubItem.DataBind();
        Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/STemplate");
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
        string pic = "";
        if (dal.updateName(id, classname, pri, pic) > 0)
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

    protected void btSave_Click(object sender, EventArgs e)
    {
        STemplateInfo mm = new STemplateInfo();
        mm.Parentid = HjNetHelper.GetQueryInt("id", 0);
        mm.Status = 0;
        mm.isDel = 0;
        mm.classname = WebUtility.InputText(tbclassname.Text);
        mm.Depth = HjNetHelper.GetQueryInt("type", 0); //1:跑腿服务类型 2:备注选项管理;
        mm.Priority = Convert.ToInt32(tbpri.Text);
        mm.pic = "";
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
}

