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

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.SQLServerDAL;

public partial class Admin_WebMessageLogList : AdminPageBase
{
    WebMessageLog bll = new WebMessageLog();

    protected string SqlWhere
    {
        get
        {
            return (ViewState["sqlWhere"] == null) ? "" : ViewState["sqlWhere"].ToString();
        }
        set
        {
            ViewState["sqlWhere"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            GetWebMessageLog();
        }
    }

  

    protected void GetWebMessageLog()
    {     
        this.AspNetPager1.RecordCount = bll.GetCount(SqlWhere);
        this.rptpWebMessageLog.DataSource = bll.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "DataId", 1);
        this.rptpWebMessageLog.DataBind();
    }

    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        GetWebMessageLog();
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        string IdList = this.hidDels.Value;

        if (bll.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            GetWebMessageLog();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
           GetWebMessageLog();
        }
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = "1=1";
        if (this.tbKeyName.Text.Trim() != "")
        {
            SqlWhere += " AND MessageId in (select dataid from WebMessage where binary Title LiKE '%" + WebUtility.InputText(this.tbKeyName.Text) + "%')";
        }     
        GetWebMessageLog();
    }

    protected void rptpWebMessageLog_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Del":
                {
                    if (bll.DelWebMessageLog(Convert.ToInt32(e.CommandArgument)) > 0)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
                        GetWebMessageLog();
                    }
                    else
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败!','error','true',5);init();");
                    }
                }
                break;
        }
    }
}
