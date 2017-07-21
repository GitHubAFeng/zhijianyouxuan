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
/// 保存用户信息
/// </summary>
public partial class APP_Android_SaveUserInfo_androidv : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        StringBuilder json = new StringBuilder("");
        string state = "0";

        ECustomer dal = new ECustomer();

        int userid = HjNetHelper.GetPostParam("userid", 0);
        
        string username = HjNetHelper.GetPostParam("username");
        string truename = HjNetHelper.GetPostParam("truename");
        string sex = HjNetHelper.GetPostParam("sex");
        string email = WebUtility.InputText(Request["email"].ToString());
        string phone = HjNetHelper.GetPostParam("phone");


        ECustomerInfo info = new ECustomerInfo();
        info = dal.GetModel(Convert.ToInt32(userid));

        //检查昵称是否有重复的
        if (dal.GetCount("[Name] ='" + username + "'") > 1)
        {
            state = "0";
        }
        else
        {
            info.TrueName = truename;
            info.Name = username;
            info.Tell = phone;
            info.Sex = sex;
            info.EMAIL = email;

            if (dal.Update(info) > 0)
            {
                state = "1";
            }
            else
            {
                state = "-1";
            }
        }

        Response.Write("{\"userid\":\"" + userid + "\",\"state\":\"" + state + "\"}");

        Response.End();
    }
}
