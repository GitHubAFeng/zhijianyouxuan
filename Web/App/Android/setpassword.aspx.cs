using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Hangjing.Model;
using Hangjing.Common;
using Hangjing.SQLServerDAL;

/// <summary>
/// 根据手机号重置密码
/// </summary>
public partial class APP_Android_setpassword : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string msg = "";

        string tel = HjNetHelper.GetPostParam("tel");
        string newpassword = HjNetHelper.GetPostParam("newpassword");
        if (tel.Length == 0 || newpassword.Length == 0)
        {
            msg = "请输入完整参数";
            Response.Write("{\"state\":\"" + -1 + "\",\"msg\":\""+ msg + "\"}");
            Response.End();
            return;
        }
        string state = "0";

        string sql = "UPDATE dbo.ECustomer SET Password = '"+ WebUtility.GetMd5(newpassword.Trim()) +"' WHERE Tell = '" + tel + "'";

        if (WebUtility.excutesql(sql) > 0)
        {
            state = "1";
        }
        else
        {
            msg = "用户不存在";
            state = "-1";
        }

        Response.Write("{\"state\":\"" + state + "\",\"msg\":\""+ msg + "\"}");

        Response.End();
    }
}
