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

public partial class App_shop_GetFoodDetailByFoodId : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        StringBuilder shoplistjson = new StringBuilder();

        Foodinfo daltogo = new Foodinfo();
        int foodid = HjNetHelper.GetPostParam("foodid", 0);

        IList<FoodStyleInfo> Stylelist = new FoodStyle().GetList(20, 1, "FoodtId = " + foodid, "dataid", 1);
        IList<FoodAttributesInfo> attrlist = new FoodAttributes().GetList(100, 1, "foodtid = " + foodid, "dataid", 1);

        FoodinfoInfo Togomodel = daltogo.GetModel(foodid);
        if (Togomodel != null)
        {
            shoplistjson.Append("{");
            shoplistjson.Append("\"Name\":\"" + Togomodel.FoodName.ToString() + "\",");
            shoplistjson.Append("\"Picture\":\"" + Togomodel.Picture.Replace("~", WebUtility.GetConfigsite()) + "\",");
            shoplistjson.Append("\"FoodType\":\"" + Togomodel.FoodType + "\",");
            shoplistjson.Append("\"Funit\":\"" + 0 + "\",");   //现未用
            shoplistjson.Append("\"IsDelete\":\"" + Togomodel.InUse + "\",");
            shoplistjson.Append("\"MaxPerDay\":\"" + 0 + "\",");//现未用
            shoplistjson.Append("\"Remains\":\"" + 0 + "\",");//现未用
            shoplistjson.Append("\"SortNum\":\"" + Togomodel.OrderNum + "\",");
            shoplistjson.Append("\"Weekday\":\"" + 0 + "\",");//现未用
            shoplistjson.Append("\"sale\":\"" + Togomodel.Remains + "\",");

            shoplistjson.Append("\"FullPrice\":\"" + Togomodel.FullPrice + "\",");

            shoplistjson.Append("\"nprice\":\"" + Togomodel.FPrice.ToString() + "\",");
            shoplistjson.Append("\"Stylelist\":" + JsonConvert.SerializeObject(Stylelist) + ",");
            shoplistjson.Append("\"attrlist\":" + JsonConvert.SerializeObject(attrlist) + ",");


            shoplistjson.Append("\"istuan\":\"" + 0 + "\",");
            shoplistjson.Append("\"Price\":\"" + 0 + "\"");//现未用
            shoplistjson.Append("}");
            Response.Write(shoplistjson.ToString());
        }
        else
        {
            Response.Write("{\"foodid\":\"" + foodid + "\",\"state\":\"0\"}");
        }
    }
}