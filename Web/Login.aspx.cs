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

public partial class Login : System.Web.UI.Page
{
    ECustomer dal = new ECustomer();
    //protected string return_content;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (UserHelp.IsLogin() && UserHelp.GetUser() != null)
        {
            Response.Redirect("user/myindex.aspx");
        }
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        string username = WebUtility.InputText(tbemail.Value);
        string password = WebUtility.GetMd5(WebUtility.InputText(tbpassword.Value));
        ECustomerInfo model = new ECustomerInfo();

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
                    divError.InnerHtml = "手机号或密码错误";
                    divError.Style["display"] = "block";
                    return;
                }
                model = list[0];
            }
        }
        else//用户名
        {
            model = dal.GetModelByNameAPassword(username, password);
        }
       
        if (model != null)
        {
            UserHelp.SetLogin(model);
            //同步
            SYNlogin(model.Name, WebUtility.InputText(tbpassword.Value));

            string url = "index.aspx";
            if (string.IsNullOrEmpty(Request.QueryString["returnurl"]))
            {
                url = "index.aspx";
            }
            else
            {
                url = Request.QueryString["returnurl"];
            }

            if (WebUtility.isSyn())
            {
                AlertScript.RegScript(Page, "timeoutlogin('" + url + "');");
            }
            else
            {
                Response.Redirect(url);
            }
        }
        else
        {
            divError.InnerHtml = "用户名或密码错误";
            divError.Style["display"] = "block";
        }
    }

    /// <summary>
    /// 同步登录
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pwd"></param>
    protected void SYNlogin(string name, string pwd)
    {
        //同步登录
        if (WebUtility.isSyn())
        {
            var uc = new UcClient();
            var user = uc.UserLogin(name, pwd, LoginMethod.UserName, true, 0, "");
            Response.Write(uc.UserSynlogin(user.Uid));
        }
    }
}
