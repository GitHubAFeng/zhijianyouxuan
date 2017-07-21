using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class Admin_callcenter_ajax_getstyle : System.Web.UI.Page
{
    FoodStyle dal = new FoodStyle();
    protected void Page_Load(object sender, EventArgs e)
    {
        string fid = Request["id"];
        IList<FoodStyleInfo> list = dal.GetList(20, 1, "FoodtId = " + fid, "dataid", 1);
        Response.Write(WebUtility.ObjectToJson("myattr", list));
        Response.End();
    }
}
