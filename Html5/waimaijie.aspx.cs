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
    public partial class waimaijie : System.Web.UI.Page
    {
        Points daltogo = new Points();
        string sqlWhere = " IsDelete=0 ";
        int pagesize = 300;
        public string address = "";
        public string TogoUrl = "";
        /// <summary>
        /// 本页面参数集合.每个参数以下字母开头(参数个数是定)
        /// </summary>
        Dictionary<string, string> mypara
        {
            get { object o = ViewState["mypara"]; return (Dictionary<string, string>)o; }
            set { ViewState["mypara"] = value; }
        }

        /// <summary>
        /// 列表排序,默认排序
        /// </summary>
        private string sortword
        {
            get
            {
                object o = ViewState["sortword"];
                string temp = "SortNum";//距离

                return (o == null) ? temp : o.ToString();
            }
            set
            {
                ViewState["sortword"] = value;
            }
        }

        /// <summary>
        /// 列表排序,1表示降序，0表示升序
        /// </summary>
        private int sortflag
        {
            get
            {
                object o = ViewState["sortflag"];
                return Convert.ToInt32(o);
            }
            set
            {
                ViewState["sortflag"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TogoUrl = "";
                string pageurl = "waimaijie.aspx?a=1";

                int pageindex = HjNetHelper.GetQueryInt("PageNo", 1);
                sqlWhere = "  1=1 and IsDelete = 0 and Star = 1 and InUse = 'Y'";//已经审核的
                string lat = WebUtility.FixgetCookie("mylat");
                string lng = WebUtility.FixgetCookie("mylng");

                string openid = Request["openid"];
                if (openid != null && openid != "")
                {
                    WebUtility.FixsetCookie("openid", openid, 365);
                }

                int id = HjNetHelper.GetQueryInt("id", 0); //分类的id
                if (id > 0)
                {
                    pageurl += "&id=" + id;
                    sqlWhere += " and category like  '%{" + id + "}%'";
                }

                sortflag = 1;

                 setCurrentSort();

                WebUtility.BindRepeater(rpttogosortlist, SectionProxyData.GetSortList());

                pageurl += "&lat=" + lat;
                pageurl += "&lng=" + lng;

                string strDistance = " distance <= Inve1 ";



                string keyword = WebUtility.InputText(Request["keyword"]);
                if (keyword != null && keyword.Length > 0)
                {
                    sqlWhere += " and (Points.unid in (select FPMaster from foodinfo where FoodName like '%" + keyword + "%') ";
                    sqlWhere += " or Points.Name like '%" + keyword + "%')";//商家
                }


                if (lat == "" || lng == "" || lat == null || lng == null)
                {
                    lat = "0";
                    lng = "0";
                    return;
                }

                //string sortword = Constant.shopsortname;//使用方法里面的 2015-12-2 

                int count = daltogo.GetCountWidthDistance(sqlWhere, sortword, sortflag, lat, lng, strDistance);

                IList<webPromotionConfigInfo> shoppromotions = new List<webPromotionConfigInfo>();
                IList<PointsInfo> shoplist = daltogo.GetDistanceListSuper(pagesize, pageindex, sqlWhere, sortword, sortflag, lat, lng, strDistance); ;
                //查询本页所有商家配送段记录，再根据此及距离，得到配送费，起送价


                string shopids = "";
                foreach (var item in shoplist)
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

                IList<ShopFoodPictureInfo> alltags = CacheHelper.GetShopPicTag();

                IList<webPromotionConfigInfo> webPromotions = CacheHelper.GetWebPromotionConfig();

                foreach (var item in shoplist)
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

                    foreach (var record in deliveryrecord.Where(a => a.tid == item.Unid))
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
                this.rptJoinTogolist.DataSource = shoplist;
                this.rptJoinTogolist.DataBind();

                int pagecount = (count % pagesize == 0 ? count / pagesize : count / pagesize + 1);
                hfpage.Value = pagecount.ToString();

                if (pagecount > 1)
                {
                    pages.InnerHtml = WebUtility.GetPageString(pageindex, pagecount, pageurl);
                }
                TogoUrl = pageurl;
                address = Server.UrlDecode(WebUtility.FixgetCookie("address"));
                back.HRef = "Togolist.aspx?islocal=1&address=" + WebUtility.FixgetCookie("address") + "&lat=" + lat + "&lng=" + lng;



            }
        }

        public void setCurrentSort()
        {
            mypara = new Dictionary<string, string>();
            string para1 = Request["para1"];
            //保存每个条件的标签如:s,sp,l,a...
            string mykey = "";
            //保存每个条件的标值如:223,2,11_22
            string myvalue = "";
            //数字和'_'
            Regex rxnumber = new Regex(@"[0-9_]+");
            //字母
            Regex rxLetter = new Regex(@"[a-zA-Z]+");
            if (para1 != null && para1 != "")
            {
                para1 = WebUtility.dellast(para1, "/");
                string[] paraitems = para1.Split('/');
                foreach (var item in paraitems)
                {
                    mykey = rxnumber.Replace(item, "");
                    myvalue = rxLetter.Replace(item, "");
                    if (myvalue != "")
                    {
                        mypara.Add(mykey, myvalue);
                    }

                }
            }

            foreach (var item in mypara)
            {
                switch (item.Key)
                {
                    case "od"://排序
                        {
                            switch (item.Value)
                            {
                                case "0"://默认
                                    sortword = "SortNum";
                                    sortflag = 1;
                                    break;
                                case "1"://销量
                                    sortword = "pop";
                                    sortflag = 1;
                                    break;
                                case "2"://最新
                                    sortword = "InTime";
                                    sortflag = 1;
                                    break;
                                case "3"://距离
                                    sortword = "Distance";
                                    sortflag = 0;
                                    break;
                            }
                            hfod.Value = item.Key + item.Value;
                        }
                        break;
                    case "s"://分类
                        {
                            sqlWhere += " and  charindex('{" + item.Value + "}',category)>=1 ";
                            //hfcursortid.Value = item.Value;
                        }
                        break;
                    case "a"://起送价
                        {
                            //hfcursendmoney.Value = item.Value;
                            sqlWhere += " and EXISTS (SELECT tid FROM dbo.shopdelivery WHERE tid = Points.Unid AND minmoney <= " + item.Value + ") ";
                        }
                        break;
                    case "l"://配送类型
                        {
                            sqlWhere += " and sentorg= 0 ";
                            //hfcursaleid.Value = item.Value;
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// 各个排序Url
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected string getSortUrl(string tag, object id)
        {
            string url = "waimaijie.aspx?c=1";
            url += "&para1=";

            foreach (var item in mypara)
            {
                if (item.Key != tag)
                {
                    url += item.Key + item.Value + "/";
                }
            }
            if (id.ToString() != "")
            {
                url += tag + id + "/";
            }

            return url;
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
    }
}
