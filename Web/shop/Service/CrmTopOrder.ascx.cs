using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class qshopy_54tss_Admin_Service_CrmTopOrder : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            EAdminInfo info = UserHelp.GetAdmin();
            lbadminname.InnerText = info.AdminName;

            string[] Weeks = { "日", "一", "二", "三", "四", "五", "六" };
            this.snDate.InnerHtml = DateTime.Now.ToLongDateString() + " 星期" + Weeks[(int)DateTime.Now.DayOfWeek];
            if (info.Permission.ToString().Trim()=="2")
            {
                this.callback.Style["display"] = "none";
                string url=ResolveClientUrl("~/Admin/login.aspx?out=1");
                this.lithref.Text = "<a href='" + url + "'><strong>退出</strong></a>";
                string orderurl = ResolveClientUrl("~/Admin//Service/OrderCrm.aspx");
                this.Litorderlist.Text = "<a href='" + orderurl + "'><strong>返回呼叫中心</strong></a>";
            } 
        }
    }
}
