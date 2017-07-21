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
/// 商家收支记录
/// </summary>
public partial class qy_54tss_Admin_User_shopAddMoneyLog : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlWhere = "1=1";

            int TogoId = HjNetHelper.GetQueryInt("tid", 0);
            if (TogoId > 0)
            {
                hidTogoId.Value = TogoId.ToString();
                SqlWhere += " and userid=" + TogoId;

                Points e_dal = new Points();
                PointsInfo e_info = e_dal.GetModel(TogoId);
                username_legend.InnerHtml = e_info.Name;
                lbmoney.InnerHtml = e_info.money.ToString() + "元";
            }
            else
            {
                btadd.Visible = false;
                userinfofieldset.Visible = false;
            }
            BindData();
        }
    }

    TogoAddMoneyLog dal = new TogoAddMoneyLog();

    protected void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rtpGifts.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "dataid", 1);
        this.rtpGifts.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
    }

    protected void rtpGifts_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        string type = e.CommandName;
        switch (type)
        {
            case "del":
                if (dal.DelTogoAddMoneyLog(Convert.ToInt32(e.CommandArgument)) > 0)
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
                    //TODO:同时删除对应类别下面的餐品

                    BindData();
                }
                else
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
                }
                break;
        }
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        if (dal.DelList(this.hidDels.Value) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            //TODO:同时删除对应类别下面的餐品

            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
        }
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = " 1=1 ";
        if (!string.IsNullOrEmpty(HjNetHelper.GetQueryString("tid")))
        {
            string userid = Request["tid"];
            SqlWhere += " and userid=" + userid;
        }
        if (tbKeyword.Text.Trim() != "")
        {
            SqlWhere += " and userid in (select unid from points where Name like '%" + WebUtility.InputText(tbKeyword.Text.Trim()) + "%')";
        }
        if (this.tbStartTime.Text != "")
        {
            SqlWhere += " and AddDate > '" + WebUtility.InputText(tbStartTime.Text) + "'";
        }
        if (this.tbEndTime.Text != "")
        {
            SqlWhere += " and AddDate < '" + WebUtility.InputText(tbEndTime.Text) + " 23:59:59'";
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

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
