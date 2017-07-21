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

public partial class Admin_EMailGsm_SystemgsmlistList :AdminPageBase
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
        this.rtpSetEmailRec.DataSource = EmailbLL.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "DataId", 1);
        this.rtpSetEmailRec.DataBind();
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

    protected void DelList_Click(object sender, EventArgs e)
    {
        string IdList = this.hidDels.Value;

        if (EmailbLL.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            selEmailRec();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
            selEmailRec();
        }
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = "1=1";
        string tbName = tbTogoName.Text.Trim();
        if (this.tbDate1.Text != "")
        {
            SqlWhere += " And AddDate >= " + "'" + this.tbDate1.Text + "'";
        }
        if (this.tbDate2.Text != "")
        {
            SqlWhere += " And AddDate <= " + "'" + this.tbDate2.Text + "'";
        }
        if (ddlstatus.SelectedValue != "-1")
        {
            SqlWhere += " And Status =" + ddlstatus.SelectedValue + "";
        }
        if (ddlSendtype.SelectedValue != "0")
        {
            SqlWhere += " And SentType =" + ddlSendtype.SelectedValue + "";
        }
        if (tbName != "")
        {
            SqlWhere += " And togoid in (select dataid from etogo where binary TogoName LiKE '%" + WebUtility.InputText(tbName) + "%')";
        }
        selEmailRec();
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        selEmailRec();
    }
}
