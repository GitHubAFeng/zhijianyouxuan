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
using Hangjing.Common;
using Hangjing.WebCommon;
using Hangjing.Model;

/// <summary>
/// 收支记录
/// </summary>
public partial class shop_OrderList_myaccountlist : System.Web.UI.Page
{
    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? string.Empty : o.ToString();
        }
        set
        {
            ViewState["SqlWhere"] = value;
        }
    }

    /// <summary>
    /// 商家编号
    /// </summary>
    private int tid
    {
        get
        {
            object o = ViewState["tid"];
            return (o == null) ? 0 : Convert.ToInt32(o);
        }
        set
        {
            ViewState["tid"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);

        if (!this.Page.IsPostBack)
        {
            PointsInfo model = new Points().GetModel(UserHelp.GetUser_Togo().Unid);
            tid = model.Unid;

            lbMoney.Text = model.money.ToString();
            SqlWhere = " userid = " + model.Unid + "";
            BindData();
        }
    }

    TogoAddMoneyLog dal = new TogoAddMoneyLog();
    protected void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptOrderList.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "dataid", 1);
        this.rptOrderList.DataBind();
        NoRecord();
    }

    private void NoRecord()
    {
        if (rptOrderList.Items.Count == 0)
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "block");
        }
        else
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void rptOrderList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "cancel")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            string sql = "UPDATE TogoAddMoneyLog SET PayState = 3 WHERE dataid = " + id;

            if (WebUtility.excutesql(sql) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:更新成功!','250','150','true','1000','true','text');init();");
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:更新失败!','250','150','true','1000','true','text');init();");
            }
        }
        BindData();
    }

    protected void btSearch_Click(object sender, EventArgs s)
    {
        SqlWhere = "userid = " + tid + "";
        if (this.starttime.Value != "")
        {
            SqlWhere += " and AddDate > '" + WebUtility.InputText(starttime.Value) + "'";
        }
        if (this.enttime.Value != "")
        {
            SqlWhere += " and AddDate < '" + WebUtility.InputText(enttime.Value) + " 23:59:59'";
        }
        if (this.ddlpaymodel.SelectedValue != "-1")
        {
            if (this.ddlpaymodel.SelectedValue == "1")
            {
                SqlWhere += " and AddMoney > 0";
            }
            else
            {
                SqlWhere += " and AddMoney < 0";
            }

        }
        if (this.ddlPayType.SelectedValue != "-1")
        {
            SqlWhere += " and PayType = " + this.ddlPayType.SelectedValue;
        }

        BindData();
    }

}
