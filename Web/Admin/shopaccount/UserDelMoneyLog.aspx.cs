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
using Hangjing.Model;
using Hangjing.Common;

public partial class Admin_shopaccount_UserDelMoneyLog_ : AdminPageBase
{
    UserDelMoneyLog dal = new UserDelMoneyLog();

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

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckRights("D");
        if (!Page.IsPostBack)
        {
            BindData();
        }
        
    }

    private void BindData() 
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rtpUserDelMoneyLog.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "UserId", 1);
        this.rtpUserDelMoneyLog.DataBind();
    }

    protected void rtpUserDelMoneyLog_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            if (dal.DelUserDelMoneyLog(Convert.ToInt32(e.CommandArgument)) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
                BindData();
            }
            else 
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败','error','true',5);init();");
            } 
        }
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        string date1 = WebUtility.InputText(tb_Date1.Text);
        string date2 = WebUtility.InputText(tb_Date2.Text);

        SqlWhere = " 1=1 ";
        if (date1 != "")
        {
            SqlWhere += "And AddDate > " + "'" + tb_Date1.Text + "'";
        }
        else if (date2 != "")
        {
            SqlWhere += "And AddDate < " + "'" + tb_Date1.Text + "'";
        }
        BindData();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        string IdList = this.hidDels.Value;

        if (dal.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
            BindData();
        }
    }

}
