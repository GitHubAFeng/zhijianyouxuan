using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Weixin;
using System.Text.RegularExpressions;

namespace Html5
{
    public partial class TogoList : MasterPageBase
    {

        Points daltogo = new Points();
        string SqlWhere = "  1=1 ";
        int pagesize = 300;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlWhere = "  1=1 and IsDelete = 0 and Star = 1 and InUse = 'Y'";//已经审核的
                WebUtility.BindRepeater(rptppt, SectionProxyData.GetPPTList().Where(a => a.Reve2 == "2").ToList());

                string pageurl = "waimaijie.aspx?a=1";

                int pageindex = HjNetHelper.GetQueryInt("PageNo", 1);

                string lat = HjNetHelper.GetQueryString("lat");
                string lng = HjNetHelper.GetQueryString("lng");

                if (lat == "" || lng == "" || lat == null || lng == null)
                {
                    lat = "0";
                    lng = "0";
                    return;
                }
                myordermsgk.Style["display"] = "none";
                noaddresstip.Style["display"] = "none";

                WebUtility.FixsetCookie("mylat", lat, 30);
                WebUtility.FixsetCookie("mylng", lng, 30);

                string openid = Request["openid"];
                if (openid != null && openid != "")
                {
                    WebUtility.FixsetCookie("openid", openid, 365);
                }

                int id = HjNetHelper.GetQueryInt("id", 0); //分类的id
                if (id > 0)
                {
                    pageurl += "&id=" + id;
                    SqlWhere += " and category like  '%{" + id + "}%'";
                }

                pageurl += "&lat=" + lat;
                pageurl += "&lng=" + lng;

                string strDistance = " distance <= Inve1 ";


                if (lat == "" || lng == "" || lat == null || lng == null)
                {
                    lat = "0";
                    lng = "0";
                    return;
                }

                string sortword = "Status desc, havenew desc, sortnum desc,distance ";

                int count = daltogo.GetCountWidthDistance(SqlWhere, sortword, 1, lat, lng, strDistance);
                IList<webPromotionConfigInfo> shoppromotions = new List<webPromotionConfigInfo>();
                IList<PointsInfo> shoplisthot = daltogo.GetDistanceListSuper(pagesize, pageindex, SqlWhere, sortword, 1, lat, lng, strDistance);
                string shopidhot = "";
                foreach (var item in shoplisthot)
                {
                    shopidhot += item.Unid + ",";
                }
                shopidhot = WebUtility.dellast(shopidhot);
                IList<shopdeliveryInfo> deliveryrecordhot = new List<shopdeliveryInfo>();
                if (shopidhot != "")
                {
                    deliveryrecordhot = new shopdelivery().GetList(" tid in (" + shopidhot + ") ");
                    shoppromotions = new webPromotionConfig().GetList(999, 1, " shopid in (" + shopidhot + ") and isopen=1 and shopid > 0 and startdate <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND enddate >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ", "pId", 1, 0);
                }

                IList<ShopFoodPictureInfo> alltags = CacheHelper.GetShopPicTag();

                IList<webPromotionConfigInfo> webPromotions = CacheHelper.GetWebPromotionConfig();
                foreach (var item in shoplisthot)
                {
                    item.pictags = new List<ShopFoodPictureInfo>();
                    string[] tagids = WebUtility.delBrackets(item.OpenTime).Split(',');
                    foreach (var ids in tagids)
                    {
                        foreach (var tag in alltags)
                        {
                            if (ids.Trim() == tag.IID.ToString())
                            {
                                item.pictags.Add(tag);
                            }
                        }
                    }
                    foreach (var record in deliveryrecordhot.Where(a => a.tid == item.Unid))
                    {
                        if (record.distancestart <= item.Distance && record.distanceend > item.Distance)
                        {
                            item.SendFee = record.sendmoney;
                            item.SendLimit = record.minmoney;
                            break;
                        }
                    }
                    item.promotions = new List<ShopFoodPictureInfo>();

                    switch (item.PType)
                    {
                        case 10:
                            {
                                foreach (var promotion in shoppromotions.Where(a => a.shopid == item.Unid).ToList())
                                {
                                    ShopFoodPictureInfo tag = new ShopFoodPictureInfo();
                                    tag.Title = promotion.revevar1;
                                    tag.Picture = "/images/jian_02.png";
                                    item.promotions.Add(tag);
                                }
                            }
                            break;
                        case 20:
                            {
                                string[] promotionids = WebUtility.delBrackets(item.PEnd).Split(',');
                                foreach (var pid in promotionids)
                                {
                                    foreach (var promotion in webPromotions)
                                    {
                                        if (pid.Trim() == promotion.pId.ToString())
                                        {
                                            ShopFoodPictureInfo tag = new ShopFoodPictureInfo();
                                            tag.Title = promotion.revevar1;
                                            tag.Picture = "/images/jian_02.png";
                                            item.promotions.Add(tag);
                                        }

                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }

                }
                this.rptJoinTogolist.DataSource = shoplisthot.Where(a => a.ID == "1").ToList();
                this.rptJoinTogolist.DataBind();

                //根据地址，确定城市编号（注：地址中含省市等信息）
                string address = Server.UrlDecode(Request["address"]);
                WebUtility.FixsetCookie("address", Server.UrlEncode(address), 30);
                addresstext.InnerText = WebUtility.Left(address, 16)+ " 》";

                InitSort();

                //查询本城市超市
                string marketsql = " IsCallCenter=1 and cityid=" + WebUtility.get_userCityid();
                PointsInfo shop = daltogo.getshopList(marketsql).FirstOrDefault();
                if (shop == null)
                {
                    marketlink.HRef = "javascript:alert('此城市未开通超市模块');";
                }
                else
                {
                    marketlink.HRef = "ShowTogo.aspx?id=" + shop.Unid;
                }

            }
        }
        /// <summary>
        /// 判断状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="isbiseniss"></param>
        /// <returns></returns>
        protected string ParseBisness(object status, object isbiseniss)
        {
            string sr = WebUtility.GetShopStatus(status, isbiseniss);
            string rs = "<i class=\"shop-open\"></i>";

            if (status.ToString() == "0" || isbiseniss.ToString() == "0")
            {
                rs = "<i class=\"shop-close\"></i>";
            }

            return rs;
        }

        public void InitSort()
        {
            IList<sortgroup> groups = new List<sortgroup>();
            IList<ShopDataInfo> sorts = WebUtility.Clone(SectionProxyData.GetSortList());

            ShopDataInfo allitem = new ShopDataInfo();
            allitem.ID = 0;
            allitem.classname = "全部";
            allitem.Pic = "images/index_class_icon01.png";
            sorts.Insert(0, allitem);

            int pagesize = 8;
            int pagecount = sorts.Count / pagesize;
            if (sorts.Count % pagesize > 0)
            {
                pagecount++;
            }

            for (int i = 0; i < pagecount; i++)
            {
                sortgroup groupitem = new sortgroup();
                groupitem.sortlist = new List<ShopDataInfo>();
                groups.Add(groupitem);
            }

            for (int i = 0; i < sorts.Count; i++)
            {
                groups[i / 8].sortlist.Add(sorts[i]);
            }

            WebUtility.BindRepeater(rptgroup, groups);

        }
    }
}
