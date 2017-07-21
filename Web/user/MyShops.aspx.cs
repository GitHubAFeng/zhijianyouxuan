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

public partial class UserHome_MyShops : PageBase
{

    ETogoCollect dal = new ETogoCollect();
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
        UserHelp.IsLogin(Request.Url.PathAndQuery);
        if (!this.Page.IsPostBack)
        {
            SqlWhere = "UserId=" + UserHelp.GetUser().DataID.ToString() + " and togoid in (select unid from  points)";
            BindDate();
        }
    }

    void BindDate()
    {
        this.AspNetPager1.RecordCount = dal.GetTogoCount(SqlWhere);
        this.rptCollect.DataSource = dal.GetTogoList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "ctime", 1);
        this.rptCollect.DataBind();
        NoRecord();
    }

    private void NoRecord()
    {
        if (rptCollect.Items.Count == 0)
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
        BindDate();
    }

    protected void rptproduct_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string s = e.CommandArgument.ToString();
        int id = Convert.ToInt32(e.CommandArgument.ToString());
        string userid = UserHelp.GetUser().DataID.ToString();
        switch (e.CommandName)
        {
            case "del":
                if (dal.DelETogoCollect(id) == 1)
                {
                    AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
                    BindDate();

                }
                else
                {
                    AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
                }
                break;
        }
    }

    public string handlerdefaut(object x)
    {
        if (Convert.ToInt32(x) == 1)
        {
            return "默认地址";
        }
        else
        {
            if (Convert.ToInt32(x) == 0)
            {
                return "设置默认地址";
            }
            return "";
        }
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Mlb_Click(object sender, EventArgs e)
    {
        string IdList = this.hdDels.Value;
        if (dal.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            BindDate();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
        }
    }
}
