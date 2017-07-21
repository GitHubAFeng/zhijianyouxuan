using System;
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

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class Admin_AdList_other : System.Web.UI.Page
{
    SortAd bll = new SortAd();
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
        if (!Page.IsPostBack)
        {
            SqlWhere = " 1=1";
            GetAdList();
        }
    }

    protected void GetAdList()
    {
        AspNetPager1.RecordCount = bll.GetCount(SqlWhere);
        IList<SortAdInfo> list = bll.GetList(AspNetPager1.PageSize , AspNetPager1.CurrentPageIndex , SqlWhere , "state" , 1);
        rptADList.DataSource = list;
        rptADList.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
        SectionProxyData.ClearWordAdlist();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetAdList();
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        string IdList = this.hdDels.Value;
        //判断权限
        int _rs = WebUtility.checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        if (bll.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
            GetAdList();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败!','error','true',5);init();");
            GetAdList();
        }
    }

    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = " 1=1 ";
        if (this.tbtitle.Text.Trim() != "")
        {
            SqlWhere += " AND title LIKE '%" + WebUtility.InputText(this.tbtitle.Text.Trim()) + "%' ";
        }
        GetAdList();
    }
}
