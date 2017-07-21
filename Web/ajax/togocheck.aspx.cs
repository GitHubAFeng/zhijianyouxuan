using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;

public partial class ajax_togocheck : System.Web.UI.Page
{
    Points dal = new Points();
    protected void Page_Load(object sender, EventArgs e)
    {
        string name = WebUtility.InputText(Request["name"]);
        string sql = " LoginName = '" + name + "'";
        int count = dal.GetCount(sql);
        string rs = "0";
        if (count == 0)
        {
            rs = "1";//可以
        }
        Response.Write(rs);
        Response.End();
    }
}
