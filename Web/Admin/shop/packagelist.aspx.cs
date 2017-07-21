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

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

/// <summary>
/// 套餐管理
/// </summary>
public partial class qy_54tss_Admin_Shop_packagelist : System.Web.UI.Page
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

    FoodPackag bll = new FoodPackag();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlWhere = "shopid =" + HjNetHelper.GetQueryString("tid") + "";// 
            hidTogoId.Value = HjNetHelper.GetQueryString("tid");
            BindData();
        }
    }

    /// <summary>
    /// 数据
    /// </summary>
    protected void BindData()
    {
        this.AspNetPager1.RecordCount = bll.GetCount(SqlWhere);
        this.rptFoodlist.DataSource = bll.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "pid", 1);
        this.rptFoodlist.DataBind();

        AlertScript.AjaxRegisterScript("init();", UpdatePanel1);
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = " 1=1 and ShopId=" + HjNetHelper.GetQueryString("tid") + " ";

        if (!string.IsNullOrEmpty(tbKeyword.Text))
        {
            SqlWhere += " and title like '%" + WebUtility.InputText(tbKeyword.Text) + "%'";
        }
        if (ddlallnum.SelectedValue != "-1")
        {
            SqlWhere += " and inve1 = " + ddlallnum.SelectedValue;
        }

        BindData();
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DelList_Click(object sender, EventArgs e)
    {
        string IdList = this.hdDels.Value;
        if (bll.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','','true','text');init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','','true','text');init();");
        }
    }

    /// <summary>
    /// 事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptUserList_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            if (bll.DelList(e.CommandArgument.ToString()) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','','true','text');init();");
                BindData();
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','','true','text');init();");
            }
        }
        if (e.CommandName == "update")
        {
            string dataid = e.CommandArgument.ToString();
            TextBox tbNum = e.Item.FindControl("tbNum") as TextBox;
            TextBox tbcnum = e.Item.FindControl("tbcnum") as TextBox;
            TextBox tbsortnum = e.Item.FindControl("tbsortnum") as TextBox;


            string sql = " update FoodPackag set Num=" + tbNum.Text + ", cnum =" + tbcnum.Text + ", sortnum =" + tbsortnum.Text + " where PID =" + dataid;

            if (WebUtility.excutesql(sql) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:操作成功!','250','150','true','3000','true','text');init();");
                BindData();
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:操作失败!','250','150','true','3000','true','text');init();");
            }

        }
    }

    protected void lbDownFood_Click(object sender, EventArgs e)
    {
        string IdList = this.hdDels.Value;
        if (bll.UpdateValue("state", 1, "where pid in (" + IdList + ")") > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:批量下架成功!','250','150','true','1000','true','text');init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:批量下架失败!','250','150','true','1000','true','text');init();");
        }
    }

    protected void lbUpFood_Click(object sender, EventArgs e)
    {
        string IdList = this.hdDels.Value;
        if (bll.UpdateValue("state", 0, "where pid in (" + IdList + ")") > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:批量上架成功!','250','150','true','1000','true','text');init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:批量上架失败!','250','150','true','1000','true','text');init();");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void add_Click(object sender, EventArgs e)
    {
        string url = "packageDetail.aspx?tid=" + HjNetHelper.GetQueryString("tid");
        Response.Redirect(url);
    }
}
