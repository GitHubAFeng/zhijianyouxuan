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

/// <summary>
/// 订单来源管理
/// </summary>
public partial class Admin_shop_ordersourcelist : AdminPageBase
{
    ordersources dal = new ordersources();
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        CheckRights("A");
        if (!Page.IsPostBack)
        {
            getItem();
        }
    }

    protected void getItem()
    {
        IList<ordersourcesInfo> list = dal.GetsubList(0);
        rptsubItem.DataSource = list;
        rptsubItem.DataBind();
        //缓存
        CacheHelper.OrderSourceListClear();
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
                AlertScript.RegScript(this, UpdatePanel1, "jtip('删除成功！');");
                getItem();
            }
            else
            {
                AlertScript.RegScript(this, UpdatePanel1, "jtip('删除失败，请重试！');");
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
        //判断权限
        int _rs = WebUtility.checkOperator(3);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        int id = Convert.ToInt32(((Label)rptsubItem.Rows[e.RowIndex].FindControl("lb_id")).Text.Trim());

        string classname = ((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_classname")).Text.Trim();
        int pri = Convert.ToInt32(((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_priority")).Text.Trim());
        int status = Convert.ToInt32(((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("tbstatus")).Text.Trim());

        
        ordersourcesInfo model = dal.GetModel(id);
        model.classname = classname;
        model.Priority = pri;
        model.Status = status;

        if (dal.Update(model) > 0)
        {
            AlertScript.RegScript(Page, UpdatePanel1, "jtip('编辑成功！');");
            getItem();
        }
        else
        {
            AlertScript.RegScript(Page, UpdatePanel1, "jtip('编辑失败，请重试！');");
        }
        rptsubItem.EditIndex = -1;
        getItem();    
    }

    protected void btSave_Click(object sender, EventArgs e)
    { //判断权限
        int _rs = WebUtility.checkOperator(2);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        ordersourcesInfo mm = new ordersourcesInfo();
        mm.Parentid = HjNetHelper.GetQueryInt("id", 0);
        mm.Status = Convert.ToInt32(tbStatus.Text);
        mm.isDel = 0;
        mm.classname = WebUtility.InputText(tbclassname.Text);
        mm.Depth = 2;
        mm.Priority = Convert.ToInt32(tbpri.Text);


        if (dal.Add(mm) > 0)
        {
            getItem();
            AlertScript.RegScript(Page, UpdatePanel1, "jtip('添加成功！');");
            tbclassname.Text = "";
        }
        else
        {
            AlertScript.RegScript(Page, UpdatePanel1, "jtip('添加失败，请重试！');");
        }

    }
}

