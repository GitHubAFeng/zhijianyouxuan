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
using System.Text;

using Hangjing.Common;

public partial class Admin_Email : System.Web.UI.Page
{
    EmailConfig dal = new EmailConfig();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetEmailData();
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        EmailConfigInfo model = new EmailConfigInfo();
        model.SysEmail = tbEmail.Text;
        model.Smtp = tbSMTP.Text;
        model.Port = tbPort.Text;
        model.PassWord = tbPassword.Text;

        model.RegContent = "";
        model.ErrorContent = "";
        model.UserName = tbName.Text;
        if (dal.SetEmailConfig(model))
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:邮件配置成功!','250','150','true','1000','true','text');hideload_super();");
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:邮件配置失败!','250','150','true','1000','true','text');hideload_super();");
        }
    }

    protected void GetEmailData()
    {
        EmailConfigInfo model = dal.GetEmailConfigModel();
        this.tbSMTP.Text = model.Smtp;
        this.tbPort.Text = model.Port;
        this.tbName.Text = model.UserName;
        this.tbPassword.Text = model.PassWord;
        this.tbEmail.Text = model.SysEmail;
       this.ttContent.Value = model.RegContent;
        this.ttError.Value = model.ErrorContent;
    }

    protected void test_Click(object sender, EventArgs e)
    {
        string Title = "测试邮件";
        string Body = "这是一封测试邮件";
        SysMailMessage ema = new SysMailMessage(WebUtility.InputText(tbEmail.Text), Title, Body);

        bool res = ema.Send();
        if (res == true)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:发送成功，配置正确!','250','150','true','2000','true','text');hideload_super();");
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:发送失败!','250','150','true','1000','true','text');hideload_super();");
        }
    }
}
