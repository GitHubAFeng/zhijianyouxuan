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
using Newtonsoft.Json;

/// <summary>
/// 商品列表
/// </summary>
public partial class AndroidAPI_GetFoodListByShopId_2 : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        int shopid = HjNetHelper.GetPostParam("shopid", 0); //商家编号
        int shopsortid = HjNetHelper.GetPostParam("shopsortid", 0);//商品分类

        Foodinfo dal = new Foodinfo();

        int pagesize = HjNetHelper.GetPostParam("pagesize", 8);
        int pageindex = HjNetHelper.GetPostParam("pageindex", 1);
        int pagecount = 1;

        string sql = " InUse = 'y' ";

        if (shopid > 0)
        {
            sql += " and FPMaster = " + shopid;
        }

        if (shopsortid > 0)
        {
            sql += " and FoodType = " + shopsortid;
        }
        int dataid = HjNetHelper.GetPostParam("dataid", 0);//商品编号
        if (dataid > 0)
        {
            sql += " and unid = " + dataid;
        }


        string shopname = HjNetHelper.GetQueryString("keyword");
        if (shopname != null && shopname != "" && shopname != "0")
        {
            sql += " and FoodName like '%" + shopname + "%' ";
        }
        IList<FoodinfoInfo> list = dal.GetList(pagesize, pageindex, sql, "OrderNum", 0);

        StyleAndAttr foodext = dal.getAllByShopid(shopid);

        int count = dal.GetCount(sql);

        if (count % pagesize == 0)
        {
            pagecount = count / pagesize;
        }
        else
        {
            pagecount = count / pagesize + 1;
        }
        StringBuilder shoplistjson = new StringBuilder();
        shoplistjson.Append("{\"page\":\"" + pageindex + "\",\"total\":\"" + pagecount + "\", \"foodlist\":[");


        foreach (var info in list)
        {
            shoplistjson.Append("{\"FoodID\":\"" + info.Unid.ToString() + "\",\"Name\":\"" + info.FoodName + "\",\"togoid\":\"" + info.FPMaster + "\",\"publicgood\":\"" + 0 + "\",\"intro\":\"" + WebUtility.FileterJson(WebUtility.NoHTML(info.Taste)) + "\",\"note\":\"" + "" + "\",\"PackageFree\":\"" + info.FullPrice + "\",\"icon\":\"" + info.Picture.Replace("~", WebUtility.GetConfigsite()) + "\"");
            shoplistjson.Append(",\"sale\":\"" + info.Remains + "\"");
            shoplistjson.Append(",\"MaxPerDay\":\"" + info.MaxPerDay + "\"");
          
            shoplistjson.Append(",\"Stylelist\":" + JsonConvert.SerializeObject( foodext.styles.Where(a=>a.FoodtId == info.Unid).ToList()));
            shoplistjson.Append(",\"attrlist\":" + JsonConvert.SerializeObject(foodext.attrs.Where(a => a.FoodtId == info.Unid).ToList()));
            shoplistjson.Append("},");
        }
        shoplistjson.Append("]}");

        Response.Write(shoplistjson.ToString().Replace(",]}", "]}"));
        Response.End();


    }
}
