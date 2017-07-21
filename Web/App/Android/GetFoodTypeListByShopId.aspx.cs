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

public partial class AndroidAPI_GetFoodTypeListByShopId_2 : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        StringBuilder shoplistjson = new StringBuilder();

        int id = HjNetHelper.GetPostParam("shopid", 0);
        EFoodSort tdal = new EFoodSort();
        IList<EFoodSortInfo> list = tdal.GetListByTogoNum(id);

        shoplistjson.Append("{\"foodtypelist\":[");
        EFoodSortInfo info = null;

        for (int i = 0; i < list.Count; i++)
        {
            info = new EFoodSortInfo();
            info = list[i];
            shoplistjson.Append("{\"SortID\":\"" + info.SortID + "\",\"SortName\":\"" + info.SortName + "\",\"JOrder\":\"" + 0 + "\",\"icon\":\"" + info.pic.Replace("~", WebUtility.GetConfigsite()) + "\"},");
        }

        shoplistjson.Append("]}");

        Response.Write(shoplistjson.ToString().Replace(",]}", "]}"));
        Response.End();

    }
}
