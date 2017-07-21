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

public partial class shop_EmailGsmRecordList : System.Web.UI.Page
{
    EmailGsmRecord EmailbLL = new EmailGsmRecord();
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

    private string togoid
    {
        get
        {
            object o = ViewState["togoid"];
            return (o == null) ? "" : Convert.ToString(o);
        }
        set
        {
            ViewState["togoid"] = value;
        }
    }

    public string showState(object s)
    {
        int states = Convert.ToInt32(s);
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

    public string SentType(object st)
    {
        int SentTypes = Convert.ToInt32(st);
        switch (SentTypes)
        {
            case 1:
                return "短信";
                break;
            case 2:
                return "邮件";
                break;
            default:
                return "失败";
                break;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
        if (!IsPostBack)
        {
            togoid = UserHelp.GetUser_Togo().Unid.ToString();
            SqlWhere = "togoid = "+togoid;
            
            selEmailRec();
        }
    }

    private void selEmailRec()
    {
        this.AspNetPager1.RecordCount = EmailbLL.GetCount(SqlWhere);
        this.rtpEmailRec.DataSource = EmailbLL.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "DataId", 1);
        this.rtpEmailRec.DataBind();
    }

    protected void rtpEmailRec_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            if (EmailbLL.DelEmailGsmRecord(Convert.ToInt32(e.CommandArgument)) > 0)
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
        SqlWhere = "togoid = " + togoid;
        if (this.ddl_States.SelectedValue != "-1")
        {
            SqlWhere += " And Status =" + ddl_States.SelectedValue + "";
        }
        if (this.ddl_Sendtype.SelectedValue != "-1")
        {
            SqlWhere += " And SentType =" + ddl_Sendtype.SelectedValue + "";
        }
        this.AspNetPager1.RecordCount = EmailbLL.GetCount(SqlWhere);
        selEmailRec();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        selEmailRec();
    }
}
