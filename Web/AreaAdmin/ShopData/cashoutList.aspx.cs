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
/// 提现申请
/// </summary>
public partial class Admin_shop_cashoutLisaddt : AdminPageBase
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


    TogoAddMoneyLog bll = new TogoAddMoneyLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlWhere = "1=1 and PayType =1 ";


            EAdminInfo model = UserHelp.GetAdmin();
            if (model.CityID != 0)
            {
                SqlWhere += " and userid in (select unid from points where cityid in(" + model.CityID + ")" + ')';
            }


            BindData();
        }
    }

    /// <summary>
    /// 绑定指定的数据
    /// </summary>
    protected void BindData()
    {
        this.AspNetPager1.RecordCount = bll.GetCount(SqlWhere);
        this.rtpOrderlist.DataSource = bll.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "dataid", 1);
        this.rtpOrderlist.DataBind();

        AlertScript.AjaxRegisterScript("init();", UpdatePanel1);

    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        AlertScript.RegScript(this.Page, UpdatePanel1, "loading();");
        SqlWhere = "1=1 and PayType =1 ";


        EAdminInfo model = UserHelp.GetAdmin();
        if (model.CityID != 0)
        {
            SqlWhere += " and userid in (select unid from points where cityid in(" + model.CityID + ")" + ')';
        }


        if (tbTogoID.Text.Trim() != "")
        {
            SqlWhere += " and UserId = " + tbTogoID.Text;
        }
      
        if (tbTogoName.Text != "")
        {
            SqlWhere += " and UserId in (select Unid from Points where Name like '%" + Utils.RegEsc(WebUtility.InputText(this.tbTogoName.Text.Trim())) + "%')";
        }
        if (tbStartTime.Text != "")
        {
            SqlWhere = SqlWhere + " and  AddDate > '" + WebUtility.InputText(this.tbStartTime.Text) + "' ";
        }
        if (tbEndTime.Text != "")
        {
            SqlWhere = SqlWhere + " and  AddDate <  '" + WebUtility.InputText(this.tbEndTime.Text) + " 23:59:59'";
        }
        if (ddlstate.SelectedValue != "-1")
        {
            SqlWhere += " and PayState = " + ddlstate.SelectedValue;
        }
       
  
        BindData();

    }


    protected void DelList_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hdDels.Value;
        if (bll.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
        }
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
