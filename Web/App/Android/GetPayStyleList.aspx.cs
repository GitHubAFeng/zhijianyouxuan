using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.SQLServerDAL;

public partial class App_Android_GetPayStyle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        IList<StateConfigInfo> list = new StateConfig().GetList(4, 1, "isdel=0 and Parentid =" + Constant.PaymentMethodPrrentID, "Priority", 1);

        StringBuilder listjson = new StringBuilder();
        listjson.Append("{\"datalist\":[");

        foreach (var item in list)
        {
            listjson.Append("{\"status\":\"" + item.Status + "\",\"classname\":\"" + item.classname + "\"},");
        }

        listjson.Append("]}");

        Response.Write(listjson.ToString().Replace("},]}", "}]}"));
        Response.End();
    }
}
