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
/// 添加、更新商品
/// </summary>
public partial class App_shop_EditorFoodDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ordermodel={"FoodType":"524","MaxPerDay":"0","Weekday":"","FoodNamePy":"ZF3","Funit":"份","FPMaster":"889","Taste":"sdfwefwe","FoodName":"支付3","Remains":"0","FullPrice":"0.0","FPrice":"2.0","OrderNum":"0","istuan":"0","Price":"2.0","Unid":6706}
        Response.Clear();

        StringBuilder shoplistjson = new StringBuilder();

        Foodinfo daltogo = new Foodinfo();
        string jsonstring = Request["ordermodel"];
        FoodinfoInfo Togomodel = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<FoodinfoInfo>(jsonstring);

        FoodinfoInfo info = new Hangjing.Model.FoodinfoInfo();
        if (Togomodel.Unid > 0)
        {
            info = daltogo.GetModel(Togomodel.Unid);
        }
        else
        {
            info.Picture = "";
            info.Remains = 0;
            info.OpenTime = "";
            info.IsRecommend = 0;
            info.IsSpecial = 1;
            info.FoodNo = "1";
            info.FPInDate = DateTime.Now;
            info.FPActiveDate = DateTime.Now;
            info.isauth = 0;
            info.InUse = "y";
            info.Special = "0";
        }
        info.MaxPerDay = Togomodel.MaxPerDay;
        if (info.MaxPerDay == 100000)
        {
            info.DpPerDay = 0;
        }
        else
        {
            info.DpPerDay = 1;
        }
        info.FoodName = Togomodel.FoodName;
        info.FPrice = Togomodel.FPrice;
        info.FullPrice = Togomodel.FullPrice;
        info.FPMaster = Togomodel.FPMaster;
        info.FoodNamePy = Togomodel.FoodNamePy;
        info.FoodType = Togomodel.FoodType;
        info.OrderNum = Togomodel.OrderNum;
        info.Taste = Togomodel.Taste;
        if (Togomodel.Unid == 0)
        {
            int foodtype = daltogo.Add(info);
            if (foodtype > 0)
            {
                Response.Write("{\"state\":\"1\",\"msg\":\"添加成功\",\"foodid\":\""+foodtype+"\"}");
            }
            else
            {
                Response.Write("{\"state\":\"-1\",\"msg\":\"服务器错误，添加失败\"}");
            }
        }
        else
        {
            if (daltogo.Update(info) > 0)
            {
                //修改规格的价格
                if (Convert.ToInt32(SectionProxyData.GetSetValue(69)) != 1)
                {
                    string sql = " UPDATE dbo.ProductStyle SET Price = "+ info.FPrice+ " WHERE FoodtId = "+info.Unid+" ";
                    WebUtility.excutesql(sql);
                }

                Response.Write("{\"state\":\"1\",\"msg\":\"修改成功\",\"foodid\":\"" + Togomodel.Unid + "\"}");

            }
            else
            {
                Response.Write("{\"state\":\"-1\",\"msg\":\"服务器错误，修改失败\"}");
            }
        }

        Response.End();

    }
}