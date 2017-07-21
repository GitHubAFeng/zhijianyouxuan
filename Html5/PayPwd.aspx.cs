using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using System.Text.RegularExpressions;

namespace Html5
{
    public partial class PayPwd : System.Web.UI.Page
    {

        ECustomer dal = new ECustomer();

        protected void Page_Load(object sender, EventArgs e)
        {
            ECustomerInfo user = UserHelp.GetUser();
            if (user == null)
            {
                Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            if (Request.HttpMethod.ToUpper() == "POST")
            {
                add_Click();
            }

        }

        /// <summary>
        /// 提交修改密码信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void add_Click()
        {
            string loginpwd = WebUtility.GetMd5(WebUtility.InputText(Request.Form["tbOldPwd"]));
            string paypassword = WebUtility.GetMd5(WebUtility.InputText(Request.Form["tbNewPwd"]).Trim());

            ECustomerInfo user = UserHelp.GetUser();
            if (loginpwd == user.Password)
            {
                string sql = "update ecustomer set PayPassword = '" + paypassword + "' where dataid = " + user.DataID;
                if (WebUtility.excutesql(sql) > 0)
                {
                    user = new ECustomer().GetModel(user.DataID);
                    UserHelp.SetLogin(user);
                    Response.Redirect("PayPwd.aspx?msg=1");//设置成功
                }
                else
                {
                    Response.Redirect("PayPwd.aspx?msg=2");//设置失败
                    return;
                }
            }
            else
            {
                Response.Redirect("PayPwd.aspx?msg=3");//原密码输入错误
                return;
            }

        }
    }
}
