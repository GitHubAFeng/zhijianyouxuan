using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.WebCommon;

/// <summary>
/// 商户APP 根据经纬度和商家id 获取跑腿费
/// </summary>
public partial class App_Android_shop_expressfee : System.Web.UI.Page
{
    //senttime=2015-06-17 09:50:00&shopid=889&ulat=30.414395&ulng=120.490334
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        ETogoLocal bll = new ETogoLocal();
        ETogoLocalInfo infos = new ETogoLocalInfo();
        string ret = "";
        string msg = "获取成功";

        string ulat = WebUtility.InputText(Request["ulat"]);
        string ulng = WebUtility.InputText(Request["ulng"]);
        string shopId = WebUtility.InputText(Request["shopid"]);
        if (string.IsNullOrEmpty(shopId))
        {
            msg = "商家id不能为空";
            Response.Write("{\"state\":\"0\",\"sendmoney\":\"0\",\"distance\":\"0\",\"msg\":\"" + msg + "\"}");
            Response.End();
            return;
        }
        int shopid = Convert.ToInt32(shopId);
        DateTime senttime = Convert.ToDateTime(WebUtility.InputText(Request["senttime"]));//扩展 暂未用

        PointsInfo shop = new Points().GetModel(shopid);
        infos = bll.GetInfoById(shop.Unid.ToString());

        SendfeeInfo info = new SendfeeInfo();
        info.ShopID = shop.cityid;//menunum

        CityInfo city = new City().GetModel(info.ShopID);
        info.cityname = city.cname;
        info.sendtime = senttime;

        info.latlng = new latlnginfo();
        info.latlng.ulat = ulat;
        info.latlng.ulng = ulng;
        info.latlng.slat = infos.Lat;
        info.latlng.slng = infos.Lng;

        SendFee fee = new SendFee(info);
        SendInfo model = fee.getSendFee();

        if (model != null && model.sendmoney>0)
        {
            ret = "{\"state\":\"1\",\"sendmoney\":\"" + model.sendmoney + "\",\"distance\":\"" + model.Distance + "\",\"msg\":\"" + msg + "\"}";
        }
        else
        {
            if (model.sendmoney == -1)
            {
                msg = "城市暂未开通此功能";
            }
            else
            {
                msg = "获取失败";
            }
            ret = "{\"state\":\"0\",\"sendmoney\":\"0\",\"distance\":\"0\",\"msg\":\"" + msg + "\"}";
        }

        Response.Write(ret);
        Response.End();
    }
}