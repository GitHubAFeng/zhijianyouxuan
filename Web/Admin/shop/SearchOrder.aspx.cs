using System;
using System.Collections;
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
using System.Net;
using org.in2bits.MyXls; //生成excel时需要使用的引用

// 订单检索管理
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-07-15
// 返回此页面时保存原有的数据 使用Session

public partial class Admin_shop_SearchOrder:AdminPageBase
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
    Custorder bll = new Custorder();

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckRights("C");
        //WebUtility.FixdelCookie("SearchOrderSqlWhere");

        // 此处保存的cookies使用获取数据
        // TODO: 是否存在问题
        if (!IsPostBack)
        {
            new OrderState().BindOrderState(ddlOrderState);//绑定订单状态

            BindData();
        }
    }

    /// <summary>
    /// 根据条件
    /// </summary>
    protected void GetSqlWhere()
    {
        SqlWhere = " 1=1 ";

        if (!string.IsNullOrEmpty(WebUtility.InputText(tbTel.Text)))
        {
            SqlWhere += " and OrderComm like '%" + WebUtility.InputText(tbTel.Text) + "%'";
        }

        if (!string.IsNullOrEmpty(WebUtility.InputText(tbTogoName.Text)))
        {
            SqlWhere += " and TogoId in (select unid from points where   Name like '%" + WebUtility.InputText(tbTogoName.Text) + "%')";
        }

        if (this.tbStartTime.Text != "" && this.tbEndTime.Text != "")
        {
            SqlWhere = SqlWhere + " and  OrderDateTime between '" + WebUtility.InputText(this.tbStartTime.Text) + "' and '" + WebUtility.InputText(this.tbEndTime.Text) + "'";
        }
        else if (this.tbStartTime.Text != "" && this.tbEndTime.Text == "")//无结束时间
        {
            SqlWhere = SqlWhere + " and OrderDateTime between '" + WebUtility.InputText(this.tbStartTime.Text) + "' and  '" + DateTime.Now.ToString() + "'";
        }
        else if (this.tbStartTime.Text == "" && this.tbEndTime.Text != "")//有结束时间无开始时间
        {
            SqlWhere = SqlWhere + " and OrderDateTime between '2010-01-01 12:00:00' and '" + WebUtility.InputText(this.tbEndTime.Text) + "'";
        }

        if (ddlOrderState.SelectedValue != "-1")
        {
            SqlWhere += " and OrderStatus=" + ddlOrderState.SelectedValue + "";
        }

        //保存此状态
        WebUtility.FixdelCookie("SearchOrderSqlWhere");
    }

    protected void BindData()
    {
        this.AspNetPager1.RecordCount = bll.GetCount(SqlWhere);//TODO:修改成链接查询的记录条数获取方法
      
        this.rtpOrderlist.DataSource = bll.GetListFix(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "OrderId", 1);
        this.rtpOrderlist.DataBind();
        NoRecord();
    }

    protected void rtpOrderlist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        
    }

    private void NoRecord()
    {
        if (rtpOrderlist.Items.Count == 0)
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "");
        }
        else
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }

    protected void btExport_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(5);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, "alert('无操作权限','success','true',5);init();");
            return;
        }
        GetSqlWhere();
        Response.Redirect("SearchExcelOrder.aspx?type=1");
    }

    protected void btShowRecord_Click1(object sender, EventArgs e)
    {
        GetSqlWhere();
        BindData();
    }

    /// <summary>
    /// 翻页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
