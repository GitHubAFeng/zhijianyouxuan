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
using Hangjing.Model;
using Hangjing.Common;

public partial class Admin_shopaccount_a : System.Web.UI.Page
{
    EmailGsmRecord EmailbLL = new EmailGsmRecord();
    EmailGsmRecordInfo info = new EmailGsmRecordInfo();

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
        if (HjNetHelper.GetQueryString("id") == "")
        {
            pageType.Text = "添加商家短信邮件营销记录";
        }
        else
        {
            //控件绑定内容 
            int id = Convert.ToInt32(HjNetHelper.GetQueryInt("id", 0));
                pageType.Text = "编辑商家短信邮件营销记录";
                info = EmailbLL.GetModel(id);
                this.tbDataid.Text = info.DataId.ToString();
                this.tbTogoName.Text = info.TogoName.ToString();
                this.tbCdate.Text = info.AddDate.ToString();
                this.tbDelMoney.Text = info.DelMoney.ToString();
                this.tbUserIdList.Text = info.UserIdList.ToString();
                this.tbSum.Text = info.Sum.ToString();
                this.ddlState.SelectedValue = info.Status.ToString();
                this.ddlType.SelectedValue = info.SentType.ToString();
                this.fcContent.Value = info.Content.ToString();           
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        EmailGsmRecordInfo info = new EmailGsmRecordInfo();
        info.DataId = Convert.ToInt32(WebUtility.InputText(this.tbDataid.Text.Trim()));
        //info.TogoId = Convert.ToInt32(WebUtility.InputText(this.hidTogoId.Value));
        info.AddDate = Convert.ToDateTime(WebUtility.InputText(this.tbCdate.Text.Trim()));
        info.DelMoney = Convert.ToDecimal(WebUtility.InputText(this.tbDelMoney.Text.Trim()));
        info.UserIdList = WebUtility.InputText(this.tbUserIdList.Text.Trim());
        info.Sum = Convert.ToInt32(WebUtility.InputText(this.tbSum.Text.Trim()));
        info.Status = Convert.ToInt32(WebUtility.InputText(this.ddlState.SelectedValue));
        info.SentType = Convert.ToInt32(WebUtility.InputText(this.ddlType.SelectedValue));
        info.Content = WebUtility.InputText(this.fcContent.Value);

        if (pageType.Text == "添加商家短信邮件营销记录")
        {
            this.tbTogoName.Enabled = true;
           // info.TogoId = Convert.ToInt32(WebUtility.InputText(this.tbTogoName.Text));
            if (EmailbLL.Add(info) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('新增成功','success','true',5);init();");
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('新增失败','error','true',5);init();");
            }
        }
        else
        {
            info.DataId = Convert.ToInt32(HjNetHelper.GetQueryString("id"));
            if (EmailbLL.Update(info) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('编辑成功','success','true',5);init();");
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('编辑失败','error','true',5);init();");
            }
        }
    }
}
