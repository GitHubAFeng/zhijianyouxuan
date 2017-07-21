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

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.SQLServerDAL;

public partial class HomeDemo_Banner : System.Web.UI.UserControl
{
    Custorder bll = new Custorder();
    //protected void Page_Load(object sender, EventArgs e)
    protected void Page_Init(object sender, EventArgs e)//为了先执行判断是否登录 这里为init 2015-12-7 
    {
        if (!Page.IsPostBack)
        {
            Hangjing.Model.EAdminInfo model = UserHelp.GetAdmin();
            if (model != null)
            {
                if (model.root == 1)
                {
                    Response.Redirect("~/AreaAdmin/basic.aspx");
                    this.snUsername.InnerHtml = model.AdminName;
                    string[] Weeks = { "日", "一", "二", "三", "四", "五", "六" };
                    this.snDate.InnerHtml = DateTime.Now.ToLongDateString() + " 星期" + Weeks[(int)DateTime.Now.DayOfWeek];
                }
                else
                {
                    this.snUsername.InnerHtml = model.AdminName;
                    string[] Weeks = { "日", "一", "二", "三", "四", "五", "六" };
                    this.snDate.InnerHtml = DateTime.Now.ToLongDateString() + " 星期" + Weeks[(int)DateTime.Now.DayOfWeek];
                }             
            }
            else
            {
                Response.Redirect("~/Admin/login.aspx");
            }



            string sql = "OrderStatus = 1 and (paymode = 4 or paystate = 1)  ";
            int count = bll.GetCount(sql);
            tcount.InnerHtml = "<font color='red'>" + count + "</font>";

            string path = Request.Url.PathAndQuery.ToLower();

            //主要用來判斷商家
            if (path.IndexOf("/basic") > 0)
            {

            }
            else
            {
                //WebUtility.checkAccess(model, path);
                //访问权限
                WebUtility.checkOperator(1);
            }
        }

    }
}
