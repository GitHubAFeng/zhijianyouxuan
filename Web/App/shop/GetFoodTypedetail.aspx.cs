using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
/// 查询某个分类
/// </summary>
public partial class App_shop_GetFoodTypedetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Clear();

        StringBuilder shoplistjson = new StringBuilder();

        int id = HjNetHelper.GetPostParam("shopid", 0);
        int sortid = HjNetHelper.GetPostParam("sortid", 0);
        EFoodSort tdal = new EFoodSort();

        EFoodSortInfo info = tdal.GetModel(sortid);

        shoplistjson.Append("{\"SortID\":\"" + info.SortID + "\",\"SortName\":\"" + info.SortName + "\",\"JOrder\":\"" + info.Jorder + "\",\"icon\":\"" + "" + "\"}");

        Response.Write(shoplistjson.ToString());
        Response.End();

    }
}