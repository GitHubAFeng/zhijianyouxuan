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

// 订单信息管理
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com 
// 2011-06-10

public partial class AreaAdmin_shop_OrderList : AdminPageBase
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
            SqlWhere = "1=1  and (paymode = 4 or paystate = 1) ";
            SqlWhere = WebUtility.GetOrderSql(SqlWhere);

            string cityid = WebUtility.FixgetCookie("admin_cityid");
            if (cityid != null && cityid != "" && cityid != "0")
            {
                SqlWhere += " and custorder.CityID = " + cityid + "  ";
            }
            if (Request["tid"] != null && Request["tid"] != "0")
            {
                SqlWhere += " and TogoId = " + HjNetHelper.GetQueryString("tid") + "";
            }
            if (!string.IsNullOrEmpty(HjNetHelper.GetQueryString("day")))
            {
                SqlWhere += " and custorder.OrderDateTime > '" + DateTime.Now.ToShortDateString() + "' and custorder.OrderDateTime < '" + (DateTime.Now.ToShortDateString() + " 23:59:59") + "'";
            }
            if (Request["type"] == "1")
            {
                SqlWhere += " and OrderStatus in (1) ";
            }
            if (Request["dates"] != null)
            {
                DateTime date = Convert.ToDateTime(Request["dates"]);
                SqlWhere += " and custorder.OrderDateTime between '" + date + "' and '" + date.AddDays(1) + "'";
            }

            new OrderState().BindOrderState(ddlOrderState);

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

        string sql = SqlWhere + " and  OrderStatus = 3 ";
        CustorderInfo count = bll.GetCountAndTotal1(sql);

        lborder.InnerText = count.OrderCount.ToString();
        lbcount.InnerText = count.OrderTotal.ToString();

        //保存此状态
        WebUtility.FixdelCookie("SearchOrderSqlWhere");

        AlertScript.AjaxRegisterScript("init();", UpdatePanel1);
        AlertScript.RegScript(this.Page, UpdatePanel1, " $('#loading-mask').hide();");
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        AlertScript.RegScript(this.Page, UpdatePanel1, "loading();");

        SqlWhere = "1=1  and (paymode = 4 or paystate = 1) ";
        SqlWhere = WebUtility.GetOrderSql(SqlWhere);
        string cityid = WebUtility.FixgetCookie("admin_cityid");
        if (cityid != null && cityid != "" && cityid != "0")
        {
            SqlWhere += " and custorder.CityID = " + cityid + "  ";
        }
        if (!string.IsNullOrEmpty(HjNetHelper.GetQueryString("day")))
        {
            SqlWhere += " and  custorder.OrderDateTime between '" + DateTime.Now.ToShortDateString() + "' and  '" + DateTime.Now.ToString() + "'";
        }
        if (tbTogoID.Text.Trim() != "")
        {
            SqlWhere += " and TogoId = " + tbTogoID.Text;
        }
        if (tborderid.Text != "")
        {
            SqlWhere += " and orderid like '%" + WebUtility.InputText(tborderid.Text) + "%'";
        }
        if (tbTogoName.Text != "")
        {
            SqlWhere += " and TogoId in (select Unid from Points where Name like '%" + Utils.RegEsc(WebUtility.InputText(this.tbTogoName.Text.Trim())) + "%')";
        }
        if (tbphone.Text != "")
        {
            SqlWhere += " and  CallPhoneNo like '%" + Utils.RegEsc(WebUtility.InputText(this.tbphone.Text.Trim())) + "%' ";
        }
        if (tbStartTime.Text != "")
        {
            SqlWhere = SqlWhere + " and  custorder.OrderDateTime > '" + WebUtility.InputText(this.tbStartTime.Text) + "' ";
        }
        if (tbEndTime.Text != "")
        {
            SqlWhere = SqlWhere + " and  custorder.OrderDateTime <  '" + WebUtility.InputText(this.tbEndTime.Text) + " 23:59:59'";
        }
        if (ddlOrderState.SelectedValue != "-1")
        {
            SqlWhere += " and OrderStatus = " + ddlOrderState.SelectedValue;
        }
        //if (DDLArea.SelectedValue != "0")
        //{
        //    SqlWhere += " and custorder.cityid = " + DDLArea.SelectedValue + "";
        //}
        if (ddleatytpe.SelectedValue != "-1")
        {
            SqlWhere += " and ReveInt2 = " + ddleatytpe.SelectedValue;
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
        if (this.ddlIsShopSet.SelectedValue != "-1")
        {
            SqlWhere = SqlWhere + " and IsShopSet=" + ddlIsShopSet.SelectedValue + "";
        }
        

        BindData(ordersort);

        WebUtility.FixsetCookie("SearchOrderSqlWhere", SqlWhere, 1);
    }


    protected void rtpOrderlist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string type = e.CommandName;
        switch (type)
        {
            
            case "print":
                {
                    FeYinPrinter p = new FeYinPrinter(e.CommandArgument.ToString());
                    p.SendCustomMessage();
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:操作成功!','250','150','true','2000','true','text');init();");
                }
                break;
        }
    }

    
    protected void dyList_Click(object sender, CommandEventArgs e)
    {
        //判断权限
        int _rs = WebUtility.AreaAdmin_checkOperator(5);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hdDels.Value;
        if (IdList == "")
        {

            IdList = ((LinkButton)sender).CommandArgument;
        }
        WebUtility.FixsetCookie("dy", IdList, 1);
        Response.Redirect("printorder.aspx");
    }


    public string gethurry(string oid)
    {
        hurryorderInfo model = new Hangjing.SQLServerDAL.hurryorder().GetModel(oid);
        string rs = "";
        if (model.oid != null)
        {
            rs = "<font color='red'>" + model.Ccount + "(" + model.addtime + ")</font>";
        }
        else
        {
            rs = "无";
        }
        return rs;
    }

    protected void ddlFunction_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drop = sender as DropDownList;

        Repeater rep = drop.Parent.Parent as Repeater;
        int n = ((RepeaterItem)drop.Parent).ItemIndex;
        string dataid = ((HiddenField)(rep.Items[n].FindControl("hidOrderId"))).Value;
        CustorderInfo model = bll.GetModel(Convert.ToInt32(dataid));

        if (model.OrderStatus == 70)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:已经完成的订单不能修改了。','250','150','true','1000','true','text');init();");
            return;
        }
        if (model.OrderStatus == 50)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:此订单已经锁定，不能操作。','250','150','true','1000','true','text');init();");
            return;
        }

        if (bll.UpdataState(Convert.ToInt32(dataid), Convert.ToInt32(drop.SelectedValue)) > 0)
        {
            bll.UpdataState(Convert.ToInt32(dataid), UserHelp.GetAdmin().AdminName);
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:更新状态成功!','250','150','true','1000','true','text');init();");

            //加积分 TODO：是否在此处加积分 
            if (drop.SelectedValue == "70" && model.OrderStatus != 70)//避免多次加分
            {
                bll.AddPoint(model.Unid.ToString());
            }

            BindData(ordersort);
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:更新状态失败!','250','150','true','1000','true','text');init();");
        }
    }

    protected void ot(object sender, EventArgs e)
    {
        ordersort = "OrderDateTime";//custorder.OrderDateTime
        BindData(ordersort);
    }

    protected void os(object sender, EventArgs e)
    {
        ordersort = "OrderStatus";
        BindData(ordersort);
    }

    /// <summary>
    /// timeer event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        string sql = "1=1  and (paymode = 4 or paystate = 1)  OrderStatus  in (1,2)";
        sql = WebUtility.GetOrderSql(sql);

        int count = bll.GetCount(sql);
        if (count != 0)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "play(" + count + ");");
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "play(0);");
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
        int _rs = WebUtility.AreaAdmin_checkOperator(5);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, "alert('无操作权限','success','true',5);init();");
            return;
        }
        Response.Redirect("SearchExcelOrder.aspx?type=1");
    }
}
