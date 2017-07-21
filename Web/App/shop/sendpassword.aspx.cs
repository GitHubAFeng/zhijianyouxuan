using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
/// 商家找收密码，发送新密码到邮箱
/// </summary>
public partial class App_Android_shop_sendpassword : System.Web.UI.Page
{
    Points dal = new Points();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string rs = "{\"state\":\"0\"}";

        string email = WebUtility.InputText(Request["email"]);
        string sql = " EMail = '" + email + "'";
        if (dal.GetCount(sql) == 0)
        {
            rs = "{\"state\":\"-1\",\"msg\":\"邮箱错误\"}";
            Response.Write(rs);
            Response.End();
            return;
        }

        string NewPassword = WebUtility.RandStr(6);
        string Title = "新密码";
        string body = "您的新密码是为：" + NewPassword;
        SysMailMessage ema = new SysMailMessage(email, Title, body);

        bool res = ema.Send();
        if (res == true)
        {
            rs = "{\"state\":\"1\",\"msg\":\"新密码已经发到邮箱，请注意查收\"}";    
            string Password = WebUtility.GetMd5(NewPassword);
            dal.ResetPassword(email, Password);
        }
        else
        {
            rs = "{\"state\":\"-2\",\"msg\":\"邮件发送失败，请联系管理员\"}";
        }

        Response.Write(rs);
        Response.End();
        return;
    }
}
