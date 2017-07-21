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

public partial class App_shop_GetFoodListByShopId : System.Web.UI.Page
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

        string sql = " 1=1 and InUse != 'd'  ";

        if (shopid > 0)
        {
            sql += " and FPMaster = " + shopid;
        }

        if (shopsortid > 0)
        {
            sql += " and FoodType = " + shopsortid;
        }
        string shopname = HjNetHelper.GetQueryString("keyword");   //商品搜索
        if (shopname != null && shopname != "" && shopname != "0")
        {
            sql += " and FoodName like '%" + shopname + "%' ";
        }
        IList<FoodinfoInfo> list = dal.GetList(pagesize, pageindex, sql, "OrderNum", 0);

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
            shoplistjson.Append("{\"FoodID\":\"" + info.Unid.ToString() + "\",");
            shoplistjson.Append("\"Name\":\"" + info.FoodName + "\",");
            shoplistjson.Append("\"FoodNamePy\":\"" + info.FoodNamePy + "\",");
            shoplistjson.Append("\"MaxPerDay\":\""+info.MaxPerDay+"\",");
            shoplistjson.Append("\"isDelete\":\"" + info.InUse + "\",");
            shoplistjson.Append("\"publicgood\":\"" + 0 + "\",");
            shoplistjson.Append("\"intro\":\"" + info.SortName + "\",");
            shoplistjson.Append("\"note\":\"" + "" + "\",");
            shoplistjson.Append("\"FullPrice\":\"" + info.FullPrice + "\",");
            shoplistjson.Append("\"icon\":\"" + info.Picture.Replace("~", WebUtility.GetConfigsite()) + "\"");
            shoplistjson.Append(",\"sale\":\"" + info.Remains + "\"");
            shoplistjson.Append(",\"istuan\":\"" + 0 + "\"");
            shoplistjson.Append(",\"Weekday\":\"" + 0 + "\"");
            shoplistjson.Append(",\"SortNum\":\"" + info.OrderNum + "\"");
            shoplistjson.Append(",\"Remains\":\"" + info.Taste + "\"");
            shoplistjson.Append(",\"FoodType\":\"" + info.FoodType.ToString() + "\"");

            //规格--只有一个价格，返回规格
            shoplistjson.Append(",\"foodstylelist\":[");
            shoplistjson.Append("{\"DataId\":\"" + 0 + "\",");
            shoplistjson.Append("\"nprice\":\"" + 0 + "\",");
            shoplistjson.Append("\"Price\":\"" + info.FPrice.ToString() + "\",");
            shoplistjson.Append("\"Foodcurrentprice\":\"" + 0 + "\"}");
            shoplistjson.Append("]");
            shoplistjson.Append("},");
        }
        shoplistjson.Append("]}");

        Response.Write(shoplistjson.ToString().Replace(",]}", "]}"));
        Response.End();


    }
}
