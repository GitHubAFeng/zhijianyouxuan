using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using System.Text;

/// <summary>
/// 获取商家统一分类 修改于2015-12-18
/// </summary>
public partial class App_Android_GetShopTypeList : System.Web.UI.Page
{
    ShopData dal = new ShopData();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        int pageindex = HjNetHelper.GetPostParam("pageindex", 1);
        int pagesize = HjNetHelper.GetPostParam("pagesize", 9);//“全部”和“其他”在APP端实现
        int pagecount = 1;


        int count = dal.GetCount("isDel=0");
        pagecount = count % pagesize == 0 ? count / pagesize : count / pagesize + 1;


        StringBuilder shoplistjson = new StringBuilder();
        shoplistjson.Append("{\"page\":\"" + pageindex + "\",\"total\":\"" + pagecount + "\",\"datalist\":[");


        IList<ShopDataInfo> list = dal.GetList(pagesize, pageindex, "isDel=0", "Priority", 1);
        ShopDataInfo info = null;
        for (int i = 0; i < list.Count; i++)
        {
            info = new ShopDataInfo();
            info = list[i];

            shoplistjson.Append("{\"SortID\":\"" + info.ID + "\"");
            shoplistjson.Append(",\"SortName\":\"" + info.classname + "\"");
            shoplistjson.Append(",\"SortPic\":\"" + info.Pic.Replace("~", WebUtility.GetConfigsite()) + "\"");//后期添加
            string sqlwhere = " 1= 1 and IsDelete = 0 and Star = 1 and InUse = 'Y' and  charindex('{" + info.ID + "}',category)>=1 ";
            count = new Points().GetCount(sqlwhere);
            shoplistjson.Append(",\"ShopCount\":\"" + count + "\"");
            shoplistjson.Append(",\"JOrder\":\"" + info.Priority + "\"},");
        }

        shoplistjson.Append("]}");

        Response.Write(shoplistjson.ToString().Replace(",]}", "]}"));
        Response.End();

    }
}

