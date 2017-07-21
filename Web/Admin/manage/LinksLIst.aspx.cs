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

public partial class Admin_Links :AdminPageBase
{
    Links linkBLL = new Links();

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

    private string OrderByWhich
    {
        get
        {
            object ob = ViewState["OrderByWhich"];
            return (ob == null ? "Introduce" : ob.ToString());
        }

        set
        {
            ViewState["OrderByWhich"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            SelRptLinks();
        }

    }

    protected void SelRptLinks()
    {
        AspNetPager1.RecordCount = linkBLL.GetCount(SqlWhere);
        this.rptLinksList.DataSource = linkBLL.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, OrderByWhich, 1);
        this.rptLinksList.DataBind();
        AlertScript.RegScript(Page, this.UpdatePanel1, "init();");
    }

    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        SelRptLinks();
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = "1=1";
        if (this.tbTitle.Text.Trim() != "")
        {
            SqlWhere += " And  binary title LIKE  '%" + WebUtility.InputText(tbTitle.Text) + "%' ";
        }
        SelRptLinks();
    }

    protected void lbDelsom_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string idlist = this.hidDels.Value;
        if (linkBLL.DelList(idlist) > 0)
        {
            AlertScript.RegScript(Page, this.UpdatePanel1, "alert(' 批量删除数据成功!' );init();");
            SelRptLinks();
        }
        else
        {
            AlertScript.RegScript(Page, this.UpdatePanel1, "alert(' 批量删除数据失败!' );init();");
            SelRptLinks();
        }
        SectionProxyData.ClearLinkList();
    }

    protected void rptLinksList_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(4);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (linkBLL.DelLinks(Convert.ToInt32(e.CommandArgument)) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
                SelRptLinks();
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败','error','true',5);init()");
            }
        }
        SectionProxyData.ClearLinkList();
    }
}
