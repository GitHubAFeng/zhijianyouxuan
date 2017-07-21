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

public partial class Admin_shopaccount_EmailGsmRecordList : System.Web.UI.Page
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

    public  string showState(object  s)
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
        if (!IsPostBack)
        {
            selEmailRec();
        }
    }

    private void selEmailRec()
    {
        this.AspNetPager1.RecordCount = EmailbLL.GetCount(SqlWhere);
        this.rtpEmailRec.DataSource = EmailbLL.GetList(AspNetPager1.PageSize,AspNetPager1.CurrentPageIndex,SqlWhere,"DataId",1);
        this.rtpEmailRec.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init()");
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        selEmailRec();
    }

    protected void rtpEmailRec_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            if (EmailbLL.DelEmailGsmRecord(Convert.ToInt32(e.CommandArgument)) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
                selEmailRec();
            }
            else
            {
               AlertScript.RegScript(this.Page ,this.UpdatePanel1,"showMessage('删除失败','error','true',5);init()");
            }
        }
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = "1=1";
        string tbName = tbTogoName.Text.Trim();
        string date1 = WebUtility.InputText(tbDate1.Text);
        string date2 = WebUtility.InputText(tbDate2.Text);
        if (date1 != "" && date2 != "")
        {
            SqlWhere += "AddDate between " + "'" + Convert.ToDateTime(date1) + "'" + " and " + "'" + Convert.ToDateTime(date2) + "'";
        }
        if (date1 != "")
        {
            SqlWhere += "AddDate > " + "'" + Convert.ToDateTime(date1) + "'";
        }
        if (date2 != "")
        {
            SqlWhere += "AddDate < " + "'" + Convert.ToDateTime(date2) + "'";
        }
        if (ddlstatus.SelectedValue != "-1")
        {
            SqlWhere += " And Status=" + ddlstatus.SelectedValue + "";
        }
        if (ddlSendtype.SelectedValue != "0")
        {
            SqlWhere += " And Status=" + ddlSendtype.SelectedValue + "";
        }
        if (tbName != "")
        {
            SqlWhere += " AND togoid in (select dataid from etogo where  TogoName LiKE '%" + WebUtility.InputText(tbName) + "%')";
        }
        this.AspNetPager1.RecordCount = EmailbLL.GetCount(SqlWhere);
        if (AspNetPager1.RecordCount == 0)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('搜索结果为0','success','true',5)");
        }
        selEmailRec();
    }
}
