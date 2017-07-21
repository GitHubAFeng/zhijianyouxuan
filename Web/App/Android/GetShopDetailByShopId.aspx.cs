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
using Hangjing.WebCommon;

public partial class AndroidAPI_GetShopDetailByShopId_2 : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        StringBuilder shoplistjson = new StringBuilder();
        //苹果测试 返回的坐标可能是负数，有- 符号 会被当成是sql注入被过滤，暂时取消安全检查
        string lat = HjNetHelper.GetQueryStringFix("lat", false).Replace('-', ' ').Trim();
        string lng = HjNetHelper.GetQueryStringFix("lng", false).Replace('-', ' ').Trim();

        //string lat = HjNetHelper.GetPostParam("lat");
        //string lng = HjNetHelper.GetPostParam("lng");

        Points daltogo = new Points();
        int shopid = HjNetHelper.GetPostParam("shopid", 0);
        int userid = HjNetHelper.GetPostParam("userid", 0);

        IList<PointsInfo> list = daltogo.GetDistanceListSuper(1, 1, "Points.unid =" + shopid, "sortnum", 1, lat, lng, "1=1");
        IList<shopdeliveryInfo> deliveryrecord = new shopdelivery().GetList(" tid = " + shopid);

        IList<ShopFoodPictureInfo> piclist = CacheHelper.GetShopPicTag();

        string newsid = "0";
        string newstiele = "";

        foreach (PointsInfo togo in list)
        {
            foreach (var record in deliveryrecord)
            {
                if (record.distancestart <= togo.Distance && record.distanceend > togo.Distance)
                {
                    togo.SendFee = record.sendmoney;
                    togo.SendLimit = record.minmoney;
                    break;
                }
            }



            string timestart2 = "";
            string timeend2 = "";
            if (togo.Opentimes1.ToString("HH:mm") == togo.Closetimes1.ToString("HH:mm"))
            {
                timestart2 = "";
                timeend2 = "";
            }
            else
            {
                timestart2 = togo.Closetimes1.ToString("HH:mm");
                timeend2 = togo.Closetimes2.ToString("HH:mm");
            }

            shoplistjson.Append("{");
            shoplistjson.Append("\"DataID\":\"" + togo.Unid.ToString() + "\",");
            shoplistjson.Append("\"TogoName\":\"" + togo.Name + "\",");
            shoplistjson.Append("\"Comm\":\"" + togo.Comm + "\",");
            shoplistjson.Append("\"address\":\"" + togo.Address + "\",");
            shoplistjson.Append("\"sendmoney\":\"" + togo.SendFee + "\",");
            shoplistjson.Append("\"Time1Start\":\"" + togo.Opentimes1.ToString("HH:mm") + "\",");
            shoplistjson.Append("\"Time1End\":\"" + togo.Opentimes2.ToString("HH:mm") + "\",");
            shoplistjson.Append("\"Time2Start\":\"" + timestart2 + "\",");
            shoplistjson.Append("\"Time2End\":\"" + timeend2 + "\",");
            shoplistjson.Append("\"shopdiscount\":\"" + 0 + "\",");
            shoplistjson.Append("\"status\":\"" + togo.isonline.ToString() + "\",");
            shoplistjson.Append("\"newsid\":\"" + newsid + "\",");

            string [] tagids = WebUtility.delBrackets(togo.OpenTime).Split(',');
            IList<ShopFoodPictureInfo> mylist = new List<ShopFoodPictureInfo>();
            foreach (var item in tagids)
            {
                foreach (var pic in piclist)
                {
                    if (item == pic.IID.ToString())
                    {
                        ShopFoodPictureInfo tag = new ShopFoodPictureInfo();
                        tag.Picture = pic.Picture.Replace("~", WebUtility.GetConfigsite());
                        tag.Title = pic.Title;

                        mylist.Add(tag);
                    }
                }
            }

            shoplistjson.Append("\"taglist\":" + WebUtility.NoHTML(JsonConvert.SerializeObject(mylist)) + ",");//图片标签

            shoplistjson.Append("\"promotions\":" + WebUtility.NoHTML(JsonConvert.SerializeObject(PromotionTool.getPromotionsFormPicTagList(togo.Unid,togo.PType,togo.PEnd))) + ",");//促销

            shoplistjson.Append("\"newstiele\":\"" + newstiele + "\",");
            shoplistjson.Append("\"startsendtime\":\"" + togo.senttime + "\",");
            shoplistjson.Append("\"ReveInt2\":\"" + 0 + "\",");
            shoplistjson.Append("\"Star\":\"" + togo.Star.ToString() + "\",");
            shoplistjson.Append("\"icon\":\"" + togo.Picture.Replace("~", WebUtility.GetConfigsite()) + "\",");
            shoplistjson.Append("\"desc\":\"" + WebUtility.FileterJson(WebUtility.NoHTML(togo.special)) + "\",");
            shoplistjson.Append("\"shopdistance\":\"" + togo.Inve1.ToString() + "\",");
            shoplistjson.Append("\"minmoney\":\"" + togo.SendLimit + "\",");
            shoplistjson.Append("\"ptimes\":\"" + togo.PTimes + "\",");
            shoplistjson.Append("\"sendfree\":\"" + 0 + "\",");
            shoplistjson.Append("\"lat\":\"" + togo.Lat + "\",");
            shoplistjson.Append("\"lng\":\"" + togo.Lng + "\",");
            shoplistjson.Append("\"basemoney\":\"" + 0 + "\",");

          
            if (userid >  0)
            {
                ETogoCollect dal_c = new ETogoCollect();
                string sql = " 1= 1 and togoid=" + togo.Unid + " and userid=" + userid;
                if (dal_c.GetTogoCount(sql) > 0)
                {
                    shoplistjson.Append("\"iscollected\":\"" + 1 + "\",");
                }
                else
                {
                    shoplistjson.Append("\"iscollected\":\"" + 0 + "\",");
                }
            }
            else
            {
                shoplistjson.Append("\"iscollected\":\"" + 0 + "\",");
            }


            shoplistjson.Append("\"PType\":\"" + togo.PType + "\",");
            shoplistjson.Append("\"RcvType\":\"" + togo.RcvType + "\",");
            shoplistjson.Append("\"IsCallCenter\":\"" + togo.IsCallCenter + "\",");


            shoplistjson.Append("\"basedistance\":\"" + 0 + "\",");
            shoplistjson.Append("\"everyfee\":\"" + 0 + "\",");
            shoplistjson.Append("\"distance\":\"" + togo.Distance.ToString("#0.0") + "\"");
            

            shoplistjson.Append("}");
        }


        Response.Write(shoplistjson.ToString());
        Response.End();

    }
}
