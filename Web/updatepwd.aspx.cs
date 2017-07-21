using System;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;
using DS.Web.UCenter.Client;
using DS.Web.UCenter;

/// <summary>
/// 从邮箱中链接，修改密码
/// </summary>
public partial class updatepwd : System.Web.UI.Page
{
    ECustomer dal = new ECustomer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string phonevalid = WebUtility.FixgetCookie("phonevalid");
            if (phonevalid == null || phonevalid != "1")
            {
                string js = "alert('验证已经过期，请重新输入验证码');gourl('forgetpassword.aspx');";
                AlertScript.RegScript(Page, UpdatePanel1, js);
            }
        }
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        string pwd = WebUtility.InputText(tbnewpwd.Value);
        string code = WebUtility.InputText(Request["tell"]);
        string sql = " update ECustomer set Password = '" + WebUtility.GetMd5(pwd) + "' where tell = '" + code + "'";

        if (WebUtility.excutesql(sql) > 0)
        {
            AlertScript.RegScript(Page, UpdatePanel1, "alert('修改密码成功！ ');gourl('index.aspx');hideloadfix();");
        }
        else
        {
            AlertScript.RegScript(Page, UpdatePanel1, "alert('服务器繁忙，请稍后再试！ ');hideloadfix();");
        }
    }

}
