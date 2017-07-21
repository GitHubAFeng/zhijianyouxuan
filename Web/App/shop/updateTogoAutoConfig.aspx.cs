using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;


/// <summary>
/// 自动接单设置
/// </summary>
public partial class App_Android_shop_updateTogoAutoConfig : System.Web.UI.Page
{

    Points dal = new Points();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string shopid = WebUtility.InputText(Request["shopid"]);
        string state = WebUtility.InputText(Request["state"]);//0 表示关闭，1：开启

        int back = dal.UpdateValue("RcvType", state, " where unid = " + shopid.Trim());

        if (back > 0)
        {
            Response.Write("{\"togoid\":\"" + shopid.Trim() + "\",\"state\":\"1\"}");
        }
        else
        {
            Response.Write("{\"togoid\":\"" + shopid.Trim() + "\",\"state\":\"0\"}");
        }

        Response.End();
    }
}
