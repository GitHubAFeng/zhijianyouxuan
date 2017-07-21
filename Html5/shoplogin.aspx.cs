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
    public partial class shoplogin : System.Web.UI.Page
    {
        Points dal = new Points();

        protected void Page_Load(object sender, EventArgs e)
        {
            string name = HjNetHelper.GetQueryString("name");
            if (name != "")
            {
                this.tbuserName.Value = name;
            }
            //退出登录，这里只是方便测试
            if (Request["out"] != null)
            {
                UserHelp.Logout_Togo();
            }

            string openid = Request["openid"];
            if (openid != null && openid != "")
            {
                WebUtility.FixsetCookie("openid", openid, 365);
            }

            if (Request.HttpMethod.ToUpper() == "POST")
            {
                add_Click();
            }
            else
            {
                PointsInfo shop = UserHelp.GetUser_Togo();
                if (shop != null && openid != null && openid != "")
                {
                    hfislogin.Value = "1";//已经登录

                    string sql = "UPDATE dbo.Points SET PosAddr = '" + openid + "' WHERE Unid = " + shop.Unid;
                    WebUtility.excutesql(sql);
                }
            }
        }

        /// <summary>
        /// 提交登录信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void add_Click()
        {
            string username = WebUtility.InputText(Request.Form["tbuserName"]);
            string password = WebUtility.GetMd5(WebUtility.InputText(Request.Form["tbpassword"]));

            PointsInfo model = dal.GetModel(username, password);

            if (model != null)
            {
                string uc = WebUtility.FixgetCookie("openid");
                model.PosAddr = uc;
                string sql = "UPDATE dbo.Points SET PosAddr = '" + uc + "' WHERE Unid = " + model.Unid;
                WebUtility.excutesql(sql);

                UserHelp.SetLogin_Togo(model);
                Response.Redirect("shoplogin.aspx?openid=" + model.PosAddr);
            }
            else
            {
                divError.InnerHtml = "帐号或密码错误。";
                return;
            }
        }
    }
}
