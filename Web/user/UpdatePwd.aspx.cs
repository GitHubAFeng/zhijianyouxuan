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
using Hangjing.SQLServerDAL;
using Hangjing.Model;

public partial class UserHome_UpdatePwd : PageBase
{
    ECustomer dal = new ECustomer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            if (!UserHelp.IsLogin())
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                btUpdate.OnClientClick = string.Format("return checkNull('{0}','{1}','{2}','{3}','{4}','{5}')", tbOldPwd.ClientID, tbNewPwd.ClientID, tbPwdagin.ClientID, lbmsgOldpwd.ClientID, lbmsgNewpwd.ClientID, lbmsgPwdagin.ClientID);
            }
        }
    }

    protected void btUpdate_Click(object sender, EventArgs e)
    {
        if (WebUtility.GetMd5(this.tbOldPwd.Text) == UserHelp.GetUser().Password)//get?test.html
        {
            if (this.tbNewPwd.Text == this.tbPwdagin.Text)
            {
                if (dal.ChangePassword(UserHelp.GetUser().DataID, WebUtility.GetMd5(this.tbNewPwd.Text)) == 1)
                {
                    AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:修改密码成功，请重新登陆','250','150','false','3000','true','text');");
                    UserHelp.Logout();
                    Response.Redirect("../Login.aspx");
                }
                else
                {
                    AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:修改密码失败!','250','150','true','2000','true','text');");
                }
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:两次输入的密码不一致!','250','150','true','2000','true','text');");
            }
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:原始密码错误，请重新输入!','250','150','true','2000','true','text');");
        }
    }
}
