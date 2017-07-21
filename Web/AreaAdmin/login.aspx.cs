using System;
using System.Collections;
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
using Hangjing.Model;
using System.Collections.Generic;

public partial class qy_54tss_AreaAdmin_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["out"] != null)
            {
                UserHelp.AdminLogout();
            }
        }
    }

    protected void btLogin_Click(object sender, EventArgs e)
    {
        Hangjing.SQLServerDAL.EAdmin dal = new Hangjing.SQLServerDAL.EAdmin();

        string adminname = WebUtility.InputText(this.tbname.Value);
        string adminpwd = WebUtility.GetMd5(this.tbpwd.Value);

        if (Session["CheckCode"] != null && Session["CheckCode"].ToString().ToLower() != this.tbvcode.Value.ToLower())
        {
            WebUtility.RegScript(this, "alert('验证码错误!','error');");
        }
        else
        {
            Hangjing.Model.EAdminInfo model = new Hangjing.Model.EAdminInfo();

            model = dal.GetModelByRoot(adminname, adminpwd);
            if (model != null)
            {

                string sql = "update eadmin set LastAccess = '" + DateTime.Now + "' where id = " + model.ID;
                WebUtility.excutesql(sql);

                HttpContext.Current.Session["admin"] = model;
                UserHelp.AdminLogin(model);
                FormsAuthentication.SetAuthCookie(adminname, false);

                Response.Redirect("~/AreaAdmin/basic.aspx");
                Response.End();
            }
            else
            {
                WebUtility.RegScript(this, "alert('帐号不存在或密码错误!','error');");
            }
        }
    }
}
