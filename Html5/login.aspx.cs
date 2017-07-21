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
    public partial class login : PageBase
    {

        ECustomer dal = new ECustomer();

        protected void Page_Load(object sender, EventArgs e)
        {
            string name = HjNetHelper.GetQueryString("name");
            if (name != "")
            {
                this.tbuserName.Value = name;
            }

            if (Request.HttpMethod.ToUpper() == "POST")
            {
                add_Click();
            }
        }

        /// <summary>
        /// 提交登录信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void add_Click()
        {

            ECustomerInfo model = new ECustomerInfo();

            string username = WebUtility.InputText(Request.Form["tbuserName"]);
            string password = WebUtility.GetMd5(WebUtility.InputText(Request.Form["tbpassword"]));

            Regex rx = new Regex(@"^[0-9]*$");
            if ((username.IndexOf('@') > 0) || (rx.IsMatch(username) && username.Length == 11))
            {
                if (username.IndexOf('@') > 0)
                {
                    //判断邮箱有没有激活
                    model = dal.GetModelByEmail(username, password);
                }

                if (rx.IsMatch(username) && username.Length == 11)
                {
                    //判断手机
                    string sql = "tell='" + username + "' and Password ='" + password + "'";
                    IList<ECustomerInfo> list = dal.GetList(1, 1, sql, "dataid", 1);
                    if (list.Count == 0)
                    {
                        divError.InnerHtml = "手机号或密码错误！";
                        return;
                    }
                    else
                    {
                        model = list[0];
                    }

                }
            }
            else//用户名
            {
                model = dal.GetModelByNameAPassword(username, password);
            }

            if (model != null)
            {
                UserHelp.SetLogin(model);

                //更新用户openid ecustomer.PayPWDQuestion
                string openid = WebUtility.FixgetCookie("openid");

                if (openid != null && openid != "")
                {
                    string sql = "UPDATE dbo.ECustomer SET PayPWDQuestion = '" + openid + "' WHERE dataid = " + model.DataID;
                    WebUtility.excutesql(sql);
                }

                if (string.IsNullOrEmpty(Request.QueryString["returnurl"]))
                {
                    Response.Redirect("~/myinfolist.aspx");

                }
                else
                {
                    string jrs = Request.QueryString["returnurl"];
                    Response.Redirect(jrs);
                }
            }

            else
            {
                divError.InnerHtml = "帐号或密码错误！";
                return;
            }

        }
    }
}
