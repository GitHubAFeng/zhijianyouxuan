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
using Hangjing.Model;

public partial class AreaAdmin_AdList : AdminPageBase
{
    AdTable bll = new AdTable();

    protected string SqlWhere
    {
        set
        {
            ViewState["sqlwhere"] = value;
        }
        get
        {
            return ViewState["sqlwhere"] == null ? "" : ViewState["sqlwhere"].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            SelAdList();
        }
    }

    protected void SelAdList()
    {
        SqlWhere = "1=1";
        this.AspNetPager1.RecordCount = bll.GetCount(SqlWhere);
        this.rptADList.DataSource = bll.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "TID", 0);
        this.rptADList.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        SelAdList();
    }

    protected void rptADList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Del":
                {
                    //判断权限
                    int _rs = WebUtility.AreaAdmin_checkOperator(4);
                    if (_rs == 0)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                        return;
                    }
                    if (bll.DelAdTable(Convert.ToInt32(e.CommandArgument)) > 0)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
                        SelAdList();
                    }
                    else
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败!','error','true',5);init();");
                    }
                }
                break;
        }
    }

    //protected void DelList_Click(object sender, EventArgs e)
    //{
    //    string IdList = this.hdDels.Value;

    //    if (bll.DelList(IdList) > 0)
    //    {
    //        AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
    //        SelAdList();
    //    }
    //    else
    //    {
    //        AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
    //        SelAdList();
    //    }
    //}
}
