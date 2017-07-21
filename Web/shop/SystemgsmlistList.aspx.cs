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

public partial class shop_SystemgsmlistList : System.Web.UI.Page
{
    Hangjing.SQLServerDAL.SystemGsmList EmailbLL = new SystemGsmList();
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

    public string showState(object st)
    {
        int states = Convert.ToInt32(st);
        switch (states)
        {
            case 0:
                return "新增";
                break;
            case 1:
                return "进行中";
                break;
            case 2:
                return "完成";
                break;
            default:
                return "失败";
                break;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
            SqlWhere = " TogoId= '" + UserHelp.GetUser_Togo().Unid + "'";
            selEmailRec();
        }
    }

    private void selEmailRec()
    {
        this.AspNetPager1.RecordCount = EmailbLL.GetCount(SqlWhere);
        this.rtpSetEmailRec.DataSource = EmailbLL.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "DataId", 1);
        this.rtpSetEmailRec.DataBind();
    }

    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        selEmailRec();
    }

    protected void rtpSetEmailRec_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            if (EmailbLL.DelSystemGsmList(Convert.ToInt32(e.CommandArgument)) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
                selEmailRec();
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败','error','true',5);init()");
            }
        }
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = " TogoId= '" + UserHelp.GetUser_Togo().Unid + "'";
        if (this.tbDate1.Text != "")
        {
            SqlWhere += " And AddDate >= " + "'" + this.tbDate1.Text + "'";
        }
        if (this.tbDate2.Text != "")
        {
            SqlWhere += " And AddDate <= " + "'" + this.tbDate2.Text + "'";
        }
        if (this.ddl_States.SelectedValue != "-1")
        {
            SqlWhere += " And Status =" + ddl_States.SelectedValue + "";
        }
        if (this.ddl_Sendtype.SelectedValue != "0")
        {
            SqlWhere += " And SentType =" + ddl_Sendtype.SelectedValue + "";
        }
        selEmailRec();
    }
}

