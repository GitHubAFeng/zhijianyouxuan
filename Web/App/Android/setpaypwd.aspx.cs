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
/// 设置支付密码
/// </summary>
public partial class APP_Android_SaveUserInfo_androisetpaypwdd : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string rs = "{\"userid\":\"" + 0 + "\",\"state\":\"" + 0 + "\"}";

        ECustomer dal = new ECustomer();
        //会员编号
        string userid = HjNetHelper.GetPostParam("userid");
        //新密码
        string newpwd = HjNetHelper.GetPostParam("newpassword");
        //旧密码
        string oldpwd = HjNetHelper.GetPostParam("oldpassword");
        string sql = " ";

        ECustomerInfo info = new ECustomerInfo();
        info = dal.GetModel(Convert.ToInt32(userid));
        if (oldpwd == "")
        {
            if (info.PayPassword == "")
            {
                sql = "update ecustomer set PayPassword = '" + WebUtility.GetMd5(newpwd) + "' where dataid = " + userid;
                WebUtility.excutesql(sql);
                rs = "{\"userid\":\"" + userid + "\",\"state\":\"" + 1 + "\"}";
            }
            else
            {
                rs = "{\"userid\":\"" + userid + "\",\"state\":\"" + -1 + "\"}";
            }
        }
        else
        {
            if (WebUtility.GetMd5(oldpwd) == info.PayPassword)
            {
                sql = "update ecustomer set PayPassword = '" + WebUtility.GetMd5(newpwd) + "' where dataid = " + userid;
                WebUtility.excutesql(sql);
                rs = "{\"userid\":\"" + userid + "\",\"state\":\"" + 1 + "\"}";
            }
            else
            {
                rs = "{\"userid\":\"" + userid + "\",\"state\":\"" + -1 + "\"}";
            }
        }
        Response.Write(rs);
        Response.End();
    }
}
