using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;

/// <summary>
/// 删除商品
/// </summary>
public partial class App_Android_shop_deletefood : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string ids = WebUtility.InputText(Request["id"]); //多个用逗号分分开
        string shopid = WebUtility.InputText(Request["shopid"]);

        Foodinfo dal = new Foodinfo();

        string rs = "{\"state\":\"1\",\"msg\":\"删除成功\"}";
        if (ids == "")
        {
            rs = "{\"state\":\"-2\",\"msg\":\"参数不完整\"}";
            Response.Write(rs);
            Response.End();
            return;
        }

        if (dal.UpdateValue("InUse", "d", "where Unid in (" + WebUtility.dellast(ids) + ")") > 0)
        {
            rs = "{\"state\":\"1\",\"msg\":\"删除成功\"}";
        }
        else
        {
            rs = "{\"state\":\"-1\",\"msg\":\"删除失败\"}";
        }
        Response.Write(rs);
        Response.End();
    }
}
