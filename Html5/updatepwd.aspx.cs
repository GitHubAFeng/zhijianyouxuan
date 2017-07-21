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
    public partial class updatepwd : System.Web.UI.Page
    {

        ECustomer dal = new ECustomer();

        protected void Page_Load(object sender, EventArgs e)
        {
         

            if (Request.HttpMethod.ToUpper() == "POST")
            {
                add_Click();
            }

            //错误信息
            int err = HjNetHelper.GetQueryInt("err", 0);
            if (err == 1)
            {
                hferrmsg.Value = Session["order_errinfo"].ToString();
            }
        }

        /// <summary>
        /// 提交修改密码信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void add_Click()
        {

            string username = WebUtility.GetMd5(Request.Form["tbOldPwd"]);
            string password = WebUtility.GetMd5(WebUtility.InputText(Request.Form["tbNewPwd"]).Trim());

            if (username == UserHelp.GetUser().Password)//get?test.html
            {
                if (dal.ChangePassword(UserHelp.GetUser().DataID, password) == 1)
                {
                    Session["order_errinfo"] = "修改密码成功";
                    UserHelp.Logout();
                    Response.Redirect("updatepwd.aspx?err=1");
                }
                else
                {
                    divError.InnerHtml = "修改密码失败!";
                    return;
                   
                }

            }
            else
            {
                divError.InnerHtml = "原密码输入错误。";
                return;
            }

        }
    }
}
