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

public partial class Admin_EMailGsm_SystemgsmlistDeTail :AdminPageBase
{
    SystemGsmList EmailbLL = new SystemGsmList();
    SystemGsmListInfo info = new SystemGsmListInfo();

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
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            SelEmailGRpt();
        }
    }

    private void SelEmailGRpt()
    {
        if (!string.IsNullOrEmpty(HjNetHelper.GetQueryString("id")) != null)
        {
            //控件绑定内容 
            int id = HjNetHelper.GetQueryInt("id", 0);
            pageType.Text = "查看商家短信邮件营销记录";
            info = EmailbLL.GetModel(id);
            this.tbTogoName.Text = info.TogoName.ToString();
            this.tbCdate.Text = info.AddDate.ToString();
            this.tbSentTime.Text = info.SentTime.ToShortDateString();
            this.tbUserIdList.Text = info.UserIdList.ToString();
            this.tbSum.Text = info.Sum.ToString();
            this.ddlState.SelectedValue = info.Status.ToString();
            this.ddlType.SelectedValue = info.SentType.ToString();
            this.fcContent.Value = info.Content.ToString();
            this.hidTogoId.Value = info.TogoId.ToString();
        }
    }
}


