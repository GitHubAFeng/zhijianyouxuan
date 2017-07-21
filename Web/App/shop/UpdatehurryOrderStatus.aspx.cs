using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;

public partial class App_shop_UpdatehurryOrderStatus : System.Web.UI.Page
{

    hurryorder dal = new hurryorder();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string orderid = WebUtility.InputText(Request["orderid"]);

        int back = dal.UpdateValue("ReveInt", 1, " where oid = " + orderid.Trim());

        if (back > 0)
        {
            Response.Write("{\"orderid\":\"" + orderid.Trim() + "\",\"state\":\"1\",\"msg\":\"处理成功\"}");
        }
        else
        {
            Response.Write("{\"orderid\":\"" + orderid.Trim() + "\",\"state\":\"0\",\"msg\":\"处理失败\"}");
        }

        Response.End();
    }
}
