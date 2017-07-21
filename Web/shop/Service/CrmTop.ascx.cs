using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class qshopy_54tss_Admin_Service_CrmTop : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (UserHelp.GetUser_Togo() == null)
            {
                Response.Redirect("~/tLogin.aspx");
                return;
            }

            string[] Weeks = { "日", "一", "二", "三", "四", "五", "六" };
            this.snDate.InnerHtml = DateTime.Now.ToLongDateString() + " 星期" + Weeks[(int)DateTime.Now.DayOfWeek];
           
        }
    }
}
