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
/// 编辑和添加分类
/// </summary>
public partial class App_shop_addfoodsort : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        EFoodSort tdal = new EFoodSort();
        StringBuilder shoplistjson = new StringBuilder();
        EFoodSortInfo model = new EFoodSortInfo();

        string sortname = HjNetHelper.GetPostParam("sortname");
        int jorder = HjNetHelper.GetPostParam("jorder", 0);
        model.SortID = HjNetHelper.GetPostParam("foodid", 0);
        model.SortName = sortname;
        model.TogoNum = HjNetHelper.GetPostParam("shopid", 0);
        model.Jorder = jorder;

        if (model.TogoNum == 0 || model.SortName == "")
        {
            Response.Write("{\"SortID\":\"" + 0 + "\",\"state\":\"-1\",\"msg\":\"缺少参数\"}");
            Response.End();
            return;
        }

        if (model.SortID == 0)
        {
            int foodtype = tdal.Add(model);
            if (foodtype > 0)
            {
                Response.Write("{\"SortID\":\"" + foodtype + "\",\"state\":\"1\",\"msg\":\"添加成功\"}");
            }
            else
            {
                Response.Write("{\"SortID\":\"" + foodtype + "\",\"state\":\"-1\",\"msg\":\"服务器错误，添加失败\"}");
            }
        }
        else
        {
            if (tdal.Update(model) > 0)
            {
                Response.Write("{\"SortID\":\"" + model.SortID + "\",\"state\":\"1\",\"msg\":\"修改成功\"}");
            }
            else
            {
                Response.Write("{\"SortID\":\"" + model.SortID + "\",\"state\":\"-1\",\"msg\":\"服务器错误，修改失败\"}");
            }
        }

        Response.End();
    }
}