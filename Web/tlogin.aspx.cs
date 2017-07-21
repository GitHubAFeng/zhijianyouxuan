/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * Function : 用户登录
 * Created by jijunjian at 2009-7-24 15:49:47.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
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
using System.Text;
using System.Net;
using System.IO;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class tlogin : System.Web.UI.Page
{
    Points dal = new Points();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["out"] != null)
        {
            UserHelp.Logout_Togo();
            Response.Redirect("index.aspx");
        }
       
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        string name = tbemail.Value;
        string p = WebUtility.GetMd5(tbpassword.Value);
        string url = "~/shop/myindex.aspx";
        PointsInfo model = dal.GetModel(name, p);
        if (model != null)
        {
            if (Request["returnurl"] != null)
            {
                string myUrl = Server.UrlDecode(HttpContext.Current.Request.Url.Query);
                int location = myUrl.IndexOf('=');
                myUrl = myUrl.Substring(location + 1);
                if (myUrl.IndexOf('?') < 0)
                {
                    myUrl = myUrl.Replace('&', '?');
                }
                url = myUrl;
            }
           UserHelp.SetLogin_Togo(model);
            Response.Redirect(url);
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:用户名或密码错误!','250','150','true','3000','true','text'); ");//init();
        }
    }
}
