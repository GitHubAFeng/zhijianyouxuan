using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class shop_Left : System.Web.UI.UserControl
{
    Custorder dal = new Custorder();
    public string isorder = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        PointsInfo info = UserHelp.GetUser_Togo();
        if (!this.Page.IsPostBack)
        {
            if (info == null)
            {
                Response.Redirect("/tlogin.aspx");
            }
            tbshopid.Value = info.Unid.ToString();
            retime.Value = info.RefreshTime;
            isorder = HttpContext.Current.Request.Url.PathAndQuery.ToLower().IndexOf("orderlist.aspx").ToString();
        }


    }
}
