using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;

using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// 登录接口
/// </summary>
public partial class AndroidAPI_Loginv : APIPageBase
{
    ECustomer dal = new ECustomer();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string userid = "0";
        string state = "-1";

        //输入参数：用户名、密码
        string username = WebUtility.InputText(Request["username"]);
        string password = WebUtility.GetMd5(WebUtility.InputText(Request["password"]));
        ECustomerInfo model = new ECustomerInfo();
        model = dal.GetModelByNameAPassword(username, password);
        if (model != null)
        {
            userid = model.DataID.ToString();
            state = "1";
        }

        Response.Write("{\"userid\":\"" + userid + "\",\"state\":\"" + state + "\"}");

        Response.End();
    }
}
