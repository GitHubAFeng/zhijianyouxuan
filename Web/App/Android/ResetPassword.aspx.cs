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
/// 重置密码
/// </summary>
public partial class APP_Android_ResetPasswordv : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        ECustomer dal = new ECustomer();
        StringBuilder json = new StringBuilder("");

        int userid = HjNetHelper.GetPostParam("userid", 0);

        string oldpassword = HjNetHelper.GetPostParam("oldpassword");
        string newpassword = HjNetHelper.GetPostParam("newpassword");
        string state = "0";

        ECustomerInfo info = new ECustomerInfo();
        info = dal.GetModel(Convert.ToInt32(userid));
        if (info != null)
        {
            if (WebUtility.GetMd5(oldpassword.Trim()) == info.Password)
            {
                if (dal.ChangePassword(Convert.ToInt32(userid), WebUtility.GetMd5(newpassword.Trim())) == 1)
                {
                    state = "1";
                }
                else
                {
                    state = "-1";
                }
            }
            else
            {
                state = "0";
            }
        }
        else
        {
            state = "-2";
        }


        Response.Write("{\"userid\":\"" + userid + "\",\"state\":\"" + state + "\"}");

        Response.End();
    }
}
