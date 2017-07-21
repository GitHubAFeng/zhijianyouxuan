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
using System.Collections.Generic;

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.DBUtility;

/// <summary>
/// 订单结算
/// </summary>
public partial class Admin_shop_settleOrderList : AdminPageBase
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
            return (o == null) ? "unid" : Convert.ToString(o);
        }
        set
        {
            ViewState["ordersort"] = value;
        }
    }

    Custorder bll = new Custorder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            InitSqlwhere();
            EnumHelper.OrderSourceToDropDownList(ddlsource);

            BindData(ordersort);
        }
    }

    /// <summary>
    /// 绑定指定的数据
    /// </summary>
    protected void BindData(string oo)
    {
        this.AspNetPager1.RecordCount = bll.GetCount(SqlWhere);
        this.rtpOrderlist.DataSource = bll.GetListFix(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "custorder.unid", 1);
        this.rtpOrderlist.DataBind();

        CustorderInfo count = bll.GetCountAndTotal1(SqlWhere);
        lborder.InnerText = count.OrderCount.ToString();
        lbcount.InnerText = count.OrderTotal.ToString();
        lbnopaymoney.InnerText = count.shopdiscountmoney.ToString();

        //保存此状态
        WebUtility.FixdelCookie("SearchOrderSqlWhere");

        AlertScript.AjaxRegisterScript("init();", UpdatePanel1);
        AlertScript.RegScript(this.Page, UpdatePanel1, " $('#loading-mask').hide();");
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        AlertScript.RegScript(this.Page, UpdatePanel1, "loading();");

        InitSqlwhere();
        if (tborderid.Text != "")
        {
            SqlWhere += " and orderid like '%" + WebUtility.InputText(tborderid.Text) + "%'";
        }

        if (ddlsource.SelectedValue != "-1")
        {
            SqlWhere += " and fromweb = " + ddlsource.SelectedValue;
        }

        if (ddlpaystate.SelectedValue != "-2")
        {
            SqlWhere += " and paystate=" + ddlpaystate.SelectedValue;
        }
        if (this.ddlpaymodel.SelectedValue != "-1")
        {
            SqlWhere = SqlWhere + " and paymode=" + ddlpaymodel.SelectedValue + "";
        }


        BindData(ordersort);

        WebUtility.FixsetCookie("SearchOrderSqlWhere", SqlWhere, 1);
    }


    protected void rtpOrderlist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string type = e.CommandName;
        switch (type)
        {
            case "settle":
                {
                    //判断权限
                    int _rs = WebUtility.checkOperator(3);
                    if (_rs == 0)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                        return;
                    }

                    string sql = "UPDATE dbo.Custorder SET deliversiteid = 1,settleuser = '" + UserHelp.GetAdmin().AdminName + "',settledate = getdate() WHERE Unid =" + e.CommandArgument;
                    WebUtility.excutesql(sql);

                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:操作成功!','250','150','true','2000','true','text');init();");

                }
                break;
        }
        BindData(ordersort);
    }


    /// <summary>
    /// 翻页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {

        BindData(ordersort);
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
        Response.Redirect("SearchExcelOrder.aspx?type=1");
    }

    /// <summary>
    /// 生成查询条件
    /// </summary>
    protected void InitSqlwhere()
    {
        SqlWhere = " 1=1 and OrderStatus = 3 and deliversiteid=0 ";
        int tid = HjNetHelper.GetQueryInt("tid", 0);
        if (tid > 0)
        {
            SqlWhere += " and TogoId = " + HjNetHelper.GetQueryString("tid") + "";
        }
        string starttime = WebUtility.InputText(Request.QueryString["starttime"]);
        if (starttime != null && starttime != "")
        {
            SqlWhere += " and custorder.OrderDateTime > '" + starttime + "'";
        }
        string endtime = WebUtility.InputText(Request.QueryString["endtime"]);
        if (endtime != null && endtime != "")
        {
            SqlWhere += " and custorder.OrderDateTime < '" + endtime + " 23:59:59'";
        }
    }

    /// <summary>
    /// 批量结算
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void settle_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(3);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hdDels.Value;
        string sql = "UPDATE dbo.Custorder SET deliversiteid = 1,settleuser = '" + UserHelp.GetAdmin().AdminName + "',settledate = getdate() WHERE Unid in (" + IdList + ")";
        WebUtility.excutesql(sql);

        AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:操作成功!','250','150','true','1000','true','text');init();");

        BindData(ordersort);
    }

    /// <summary>
    /// 全部结算
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void settleall_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(3);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hdDels.Value;
        string sql = "UPDATE dbo.Custorder SET deliversiteid = 1,settleuser = '" + UserHelp.GetAdmin().AdminName + "',settledate = getdate() WHERE "+SqlWhere;
        WebUtility.excutesql(sql);

        AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:操作成功!','250','150','true','1000','true','text');init();");

        BindData(ordersort);
    }
}

