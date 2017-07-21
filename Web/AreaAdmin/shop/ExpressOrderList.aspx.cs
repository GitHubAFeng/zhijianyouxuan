using System;
using System.Collections.Generic;
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

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.DBUtility;

/// <summary>
/// 跑腿订单管理
/// </summary>
public partial class qy_54tss_AreaAdmin_Sale_OrderListExpressOrderList : System.Web.UI.Page
{
    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? "" : Convert.ToString(o);
        }
        set
        {
            ViewState["SqlWhere"] = value;
        }
    }

    protected string ordersort
    {
        get
        {
            object o = ViewState["ordersort"];
            return (o == null) ? "orderTime" : Convert.ToString(o);
        }
        set
        {
            ViewState["ordersort"] = value;
        }
    }

    ExpressOrder bll = new ExpressOrder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            SqlWhere = "1=1 ";
            SqlWhere = WebUtility.GetExpressOrderSql(SqlWhere);
            
            BindData(ordersort);
        }
    }

    /// <summary>
    /// 绑定指定的数据
    /// </summary>
    protected void BindData(string oo)
    {
        this.rtpOrderlist.DataSource = bll.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, oo, 1);
        this.AspNetPager1.RecordCount = bll.GetCount(SqlWhere);
        this.rtpOrderlist.DataBind();

        AlertScript.AjaxRegisterScript("init();", UpdatePanel1);
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = " 1=1";
        SqlWhere = WebUtility.GetExpressOrderSql(SqlWhere);
        if (WebUtility.InputText(tbKeyword.Text.Trim()) != "")
        {
            SqlWhere += " and  CustomerName  like '%" + Utils.RegEsc(WebUtility.InputText(this.tbKeyword.Text.Trim())) + "%' ";
        }
        if (WebUtility.InputText(tborder.Text) != "")
        {
            SqlWhere += " and dataid like '%" + Utils.RegEsc(WebUtility.InputText(this.tborder.Text.Trim())) + "%' ";
        }
        if (this.tbStartTime.Text != "")
        {
            SqlWhere = SqlWhere + " and  [OrderTime] > '" + WebUtility.InputText(this.tbStartTime.Text) + "' ";
        }
        if (this.tbEndTime.Text != "")
        {
            SqlWhere = SqlWhere + " and [OrderTime] < '" + WebUtility.InputText(this.tbEndTime.Text) + " 23:59:59'";
        }
        if (ddlOrderState.SelectedValue != "-2")
        {
            SqlWhere = SqlWhere + " and State=" + ddlOrderState.SelectedValue + "";
        }
        if (tbtel.Text.Trim() != "")
        {
            SqlWhere += " and Tel like '%" + tbtel.Text.Trim() + "%'";
        }
        //if (hfcid.Value != "0")
        //{
        //    SqlWhere += " and cityid = " + this.hfcid.Value + "";
        //}

        BindData(ordersort);
    }

    protected void rtpOrderlist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        
        
    }

    

    protected void ot(object sender, EventArgs e)
    {
        ordersort = "orderTime";
        BindData(ordersort);
    }

    protected void os(object sender, EventArgs e)
    {
        ordersort = "State";
        BindData(ordersort);
    }


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData(ordersort);
    }

}
