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
using Hangjing.WebCommon;

public partial class App_Android_GetAgainOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Custorder dal = new Custorder();
        Foodlist orderfood_bll = new Foodlist();
        Foodinfo fal = new Foodinfo();
        FoodinfoInfo foodinfo = new FoodinfoInfo();
        IList<FoodlistInfo> foodlist = new List<FoodlistInfo>();

        string orderid = HjNetHelper.GetPostParam("orderid");
        CustorderInfo info = dal.GetModel(orderid);

        //手机类型：0表示android,1表示ios
        int isios = 0;

        string mobileversion = Request["isios"];
        if (mobileversion != null && mobileversion == "1")
        {
            isios = 1;
        }

        string sqlwhere = " 1=1 and orderid=" + orderid;

        StringBuilder shoplistjson = new StringBuilder();

        if (info != null)
        {
            shoplistjson.Append("{\"foodlist\":[");

            IList<FoodlistInfo> food_list = orderfood_bll.GetAllByOrderID(orderid);
            int styleindex = 0;
            foreach (var finfo in food_list)
            {
                foodinfo = fal.GetModel(finfo.FoodUnid);
                if (foodinfo != null)
                {
                    styleindex++;
                    int styleint = 0;
                    decimal foodprice = foodinfo.FPrice;
                    if (foodinfo.InUse == "y")
                    {
                        StyleAndAttr foodext = fal.getAllByShopid(info.TogoId);

                        IList<FoodStyleInfo> styles = foodext.styles.Where(a => a.FoodtId == foodinfo.Unid).ToList();

                        if (finfo.sid > 0)
                        {
                            foreach (FoodStyleInfo item in styles)
                            {
                                if (item.DataId == finfo.sid)
                                {
                                    foodprice = item.Price;
                                    break;
                                }
                                styleint += 1;
                            }
                        }
                        shoplistjson.Append("{\"Num\":\"" + finfo.FCounts.ToString() + "\",\"FoodID\":\"" + finfo.FoodUnid.ToString() + "\",\"FoodPrice\":" + foodprice.ToString("0.0") + ",\"foodname\":\"" + finfo.FoodName + "\",\"activeid\":\"" + styleint + "\",\"package\":\"" + foodinfo.FullPrice + "\",\"isreview\":\"" + finfo.sid + "\",\"MaxPerDay\":\"" + foodinfo.MaxPerDay + "\"}");

                    }
                    else
                    {
                        shoplistjson.Append("{\"Num\":\"" + 0 + "\",\"FoodID\":\"" + finfo.FoodUnid.ToString() + "\",\"FoodPrice\":" + foodprice.ToString("0.0") + ",\"foodname\":\"" + finfo.FoodName + "\",\"activeid\":\"" + styleint + "\",\"package\":\"" + foodinfo.FullPrice + "\",\"isreview\":\"" + finfo.sid + "\",\"MaxPerDay\":\"" + foodinfo.MaxPerDay + "\"}");
                    }
                    if (styleindex != food_list.Count)
                    {
                        //最后一个
                        shoplistjson.Append(",");
                    }
                }
            }
            shoplistjson.Append("],");

            shoplistjson.Append("\"TogoId\":\"" + info.TogoId.ToString() + "\",\"sendmoney\":\"" + info.SendFee + "\"}");

            Response.Write(shoplistjson.ToString().Replace(",],", "],"));
            Response.End();
        }
    }
}