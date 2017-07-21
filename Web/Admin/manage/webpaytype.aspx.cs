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

/// <summary>
/// 网站支付管理
/// </summary>
public partial class Admin_shop_webpaytype : System.Web.UI.Page
{
    StateConfig dal = new StateConfig();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            getItem();
        }
    }

    protected void getItem()
    {
        IList<StateConfigInfo> list = dal.GetList(999, 1, "status  >= 0 and  Parentid=" + Constant.PaymentMethodPrrentID, "Priority", 1);
        rptsubItem.DataSource = list;
        rptsubItem.DataBind();
        SectionProxyData.ClearPayModel();
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
        int id = Convert.ToInt32(((Label)rptsubItem.Rows[e.RowIndex].FindControl("lb_id")).Text.Trim());
        int pri = Convert.ToInt32(((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_priority")).Text.Trim());
        //0表示启用，1表示未起用
        int Isopen = ((CheckBox)rptsubItem.Rows[e.RowIndex].FindControl("cb_Isopen")).Checked ? 0 : 1;

        string sql = "UPDATE dbo.StateConfig SET isDel = " + Isopen + ",Priority = " + pri + " WHERE ID =" + id;

        if (WebUtility.excutesql(sql) > 0)
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

}

