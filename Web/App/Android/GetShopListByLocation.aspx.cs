using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Newtonsoft.Json;

/// <summary>
/// 返回商家
/// </summary>
public partial class App_Android_GetShopListByLocation : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        int pagesize = HjNetHelper.GetPostParam("pagesize", 8);
        int pageindex = HjNetHelper.GetPostParam("pageindex", 1);
        int pagecount = 1;

        Points dal = new Points();

        string sqlWhere = " 1=1 and IsDelete = 0 and Star = 1 and InUse = 'Y' ";

        int cityid = HjNetHelper.GetPostParam("cityid", 0);
        if (cityid > 0)
        {
            sqlWhere += " and cityid = " + cityid;
        }
        //外卖赠饮料，0表示不是，1表示是
        int shopstyle = HjNetHelper.GetPostParam("givedrink", 0);
        if (shopstyle > 0)
        {
            sqlWhere += " and showpicture=1";
        }
        //分类
        int sortid = HjNetHelper.GetPostParam("shoptype", 0);
        if (sortid > 0)
        {
            sqlWhere += " and  charindex('{" + sortid + "}',category)>=1 ";
        }
        //区域
        int sid = HjNetHelper.GetPostParam("sid", 0);
        if (sid > 0)
        {
            sqlWhere += " and  MgrCell = '" + sid + "'";
        }

        //配送方式
        int sentorg = HjNetHelper.GetPostParam("sentorg", 0);
        if (sentorg > 0)
        {
            sqlWhere += " and  sentorg = '" + 1 + "'";
        }

        //推荐商家（是否热门： 0表示不是，1表示是）
        int isrem = HjNetHelper.GetPostParam("isrem", 0);
        if (isrem > 0)
        {
            sqlWhere += " and ID = '1'";
        }

        int userid = HjNetHelper.GetPostParam("userid", 0);//会员编号，用于获取收藏商家
        //获取收藏的商家
        int iscollected = HjNetHelper.GetPostParam("iscollected", 0);
        if (iscollected == 1 && userid > 0)
        {
            sqlWhere += " and EXISTS(SELECT dataid FROM dbo.ETogoCollect WHERE userid = " + userid + " AND togoid = Points.Unid  ) ";
        }


        //起送价
        int sendlimit = HjNetHelper.GetPostParam("sendlimit", 0);
        if (sendlimit > 0)
        {
            sqlWhere += " and EXISTS (SELECT tid FROM dbo.shopdelivery WHERE tid = Points.Unid AND minmoney <= " + sendlimit + ")  ";
        }

        //起送价
        int ispromotion = HjNetHelper.GetPostParam("ispromotion", 0);
        if (ispromotion > 0)
        {
            sqlWhere += " and ptype > 0 and ( EXISTS (SELECT pid FROM dbo.webPromotionConfig WHERE  isopen=1 and shopid = points.unid and startdate <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND enddate >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' )  or ptype=20 )";
        }

        string keyWord = HjNetHelper.GetPostParam("shopname");
        if (keyWord != null && keyWord != "" && keyWord != "0")
        {
            sqlWhere += " and Name like '%" + keyWord + "%' ";
        }


        string strDistance = " distance <= Inve1  ";
        int isonline = HjNetHelper.GetPostParam("isonline", -1);
        if (isonline > -1)
        {
            strDistance += " and havenew =" + isonline;
        }

        string lat = HjNetHelper.GetPostParam("lat");
        string lng = HjNetHelper.GetPostParam("lng");

        StringBuilder shoplistjson = new StringBuilder();

        string sortname = HjNetHelper.GetPostParam("sortname");
        int sortflag = HjNetHelper.GetPostParam("sortflag", 1);
        if (sortname == null || sortname == "")
        {
            sortname = "SortNum";
            sortflag = 1;
        }

        sortname = "Status desc,havenew desc," + sortname;


        int count = dal.GetCountWidthDistance(sqlWhere, sortname, sortflag, lat, lng, strDistance);

        IList<PointsInfo> list = dal.GetDistanceListSuper(pagesize, pageindex, sqlWhere, sortname, sortflag, lat, lng, strDistance);
        IList<webPromotionConfigInfo> shoppromotions = new List<webPromotionConfigInfo>();

        pagecount = count % pagesize == 0 ? count / pagesize : count / pagesize + 1;



        //查询本页所有商家配送段记录，再根据此及距离，得到配送费，起送价
        string shopids = "";
        foreach (var item in list)
        {
            shopids += item.Unid + ",";
        }
        shopids = WebUtility.dellast(shopids);
        IList<shopdeliveryInfo> deliveryrecord = new List<shopdeliveryInfo>();
        if (shopids != "")
        {
            deliveryrecord = new shopdelivery().GetList(" tid in (" + shopids + ") ");
            shoppromotions = new webPromotionConfig().GetList(999, 1, " shopid in (" + shopids + ") and isopen=1 and shopid > 0 and startdate <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND enddate >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ", "pId", 1, 0);
        }


        shoplistjson.Append("{\"record\":\"" + count + "\",\"page\":\"" + pageindex + "\",\"total\":\"" + pagecount + "\", \"list\":[");

        IList<ShopFoodPictureInfo> piclist = CacheHelper.GetShopPicTag();
        IList<webPromotionConfigInfo> webPromotions = CacheHelper.GetWebPromotionConfig();


        foreach (PointsInfo item in list)
        {
            #region 配送费 起送价
            foreach (var record in deliveryrecord.Where(a => a.tid == item.Unid))
            {
                if (record.distancestart <= item.Distance && record.distanceend > item.Distance)
                {
                    item.SendFee = record.sendmoney;
                    item.SendLimit = record.minmoney;
                    break;
                }
            }
            #endregion

            item.promotions = new List<ShopFoodPictureInfo>();
            switch (item.PType)
            {
                case 10:
                    {
                        foreach (var promotion in shoppromotions.Where(a => a.shopid == item.Unid).ToList())
                        {
                            ShopFoodPictureInfo tag = new ShopFoodPictureInfo();
                            tag.Title = promotion.revevar1;
                            tag.togoname = promotion.overmoney.ToString(); ;
                            tag.Inve2 = promotion.minusmoney.ToString();
                            tag.Picture = WebUtility.GetConfigsite()+"/images/jian_02.png";
                            item.promotions.Add(tag);
                        }
                    }
                    break;
                case 20:
                    {
                        string[] promotionids = WebUtility.delBrackets(item.PEnd).Split(',');
                        foreach (var id in promotionids)
                        {
                            foreach (var promotion in webPromotions)
                            {
                                if (id.Trim() == promotion.pId.ToString())
                                {
                                    ShopFoodPictureInfo tag = new ShopFoodPictureInfo();
                                    tag.Title = promotion.revevar1;
                                    tag.togoname = promotion.overmoney.ToString(); ;
                                    tag.Inve2 = promotion.minusmoney.ToString();
                                    tag.Picture = WebUtility.GetConfigsite() + "/images/jian_02.png";
                                    item.promotions.Add(tag);
                                }

                            }
                        }
                    }
                    break;
                default:
                    break;
            }

         


            string timestart2 = "";
            string timeend2 = "";
            if (item.Opentimes1.ToString("HH:mm") == item.Closetimes1.ToString("HH:mm"))
            {
                timestart2 = "";
                timeend2 = "";
            }
            else
            {
                timestart2 = item.Closetimes1.ToString("HH:mm");
                timeend2 = item.Closetimes2.ToString("HH:mm");
            }

            shoplistjson.Append("{\"DataID\":\"" + item.Unid.ToString() + "\", \"TogoName\":\"" + item.Name + "\", \"Grade\":\"" + item.Grade + "\",\"sortname\":\"" + "" + "\",\"address\":\"" + item.Address + "\",");
            shoplistjson.Append("\"icon\":\"" + item.Picture.Replace("~", WebUtility.GetConfigsite()) + "\",");
            shoplistjson.Append("\"sales\":\"" + item.pop + "\",");
            shoplistjson.Append("\"reason\":\"" + item.SN1 + "\",");
            shoplistjson.Append("\"desc\":\"" + WebUtility.FileterJson(WebUtility.NoHTML(item.special)) + "\",");
            shoplistjson.Append("\"minmoney\":\"" + item.SendLimit + "\",");

            #region 商家标签
            string[] tagids = WebUtility.delBrackets(item.OpenTime).Split(',');
            IList<ShopFoodPictureInfo> mylist = new List<ShopFoodPictureInfo>();
            foreach (var id in tagids)
            {
                foreach (var pic in piclist)
                {
                    if (id == pic.IID.ToString())
                    {
                        ShopFoodPictureInfo tag = new ShopFoodPictureInfo();
                        tag.Picture = pic.Picture.Replace("~", WebUtility.GetConfigsite());
                        tag.Title = pic.Title;

                        mylist.Add(tag);
                    }
                }
            } 
            #endregion

            shoplistjson.Append("\"taglist\":" + WebUtility.NoHTML(JsonConvert.SerializeObject(mylist)) + ",");//图片标签


            shoplistjson.Append("\"promotions\":" + WebUtility.NoHTML(JsonConvert.SerializeObject(item.promotions)) + ",");//促销




            shoplistjson.Append("\"senttime\":\"" + item.senttime + "\",");
            shoplistjson.Append("\"shopdiscount\":\"" + 0 + "\",");
            shoplistjson.Append("\"ptimes\":\"" + item.PTimes + "\",");
            shoplistjson.Append("\"Time1Start\":\"" + item.Opentimes1.ToString("HH:mm") + "\",\"Time1End\":\"" + item.Opentimes2.ToString("HH:mm") + "\",\"Time2Start\":\"" + timestart2 + "\",\"Time2End\":\"" + timeend2 + "\",\"distance\":\"" + item.Distance.ToString("#0.0") + "\",\"Star\":\"" + item.Star + "\",\"lng\":\"" + item.Lng + "\",\"lat\":\"" + item.Lat + "\",\"sendmoney\":\"" + item.SendFee + "\",\"status\":\"" + item.isonline+"\"");

            //返回的格式里面增加参数标识是菜品的结果还是商家的结果，如果是菜品的结果最后还有参数显示菜品的名称
            shoplistjson.Append(",\"seekType\":" + item.seekType);
            shoplistjson.Append(",\"keyWord\":\"" + item.keyWord + "\"");
            shoplistjson.Append("},");
        }

        shoplistjson.Append("]}");


        Response.Write(shoplistjson.ToString().Replace(",}","}").Replace(",]", "]"));
        Response.End();
    }
}
