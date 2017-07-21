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
    /// <summary>
    /// 收藏的商家
    /// </summary>
    public partial class myshops : System.Web.UI.Page
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
                string temp = "distance";//距离

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
                string pageurl = "myshops.aspx?a=1";

                int pageindex = HjNetHelper.GetQueryInt("PageNo", 1);
                sqlWhere = "  1=1 and IsDelete = 0 and Star = 1 and InUse = 'Y'";//已经审核的
                string lat = WebUtility.FixgetCookie("mylat");
                string lng = WebUtility.FixgetCookie("mylng");


                if (string.IsNullOrEmpty(lat) || string.IsNullOrEmpty(lng))
                {
                    lat = "0";
                    lng = "0";
                }


                ECustomerInfo user = UserHelp.GetUser();
                if (user == null)
                {
                    Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
                }
                sqlWhere += " and EXISTS (SELECT dataid FROM dbo.ETogoCollect WHERE userid = "+user.DataID+ " AND togoid = points.unid)";
                string strDistance = " distance <= Inve1 ";


                string sortword = Constant.shopsortname;

                int count = daltogo.GetCountWidthDistance(sqlWhere, sortword, 1, lat, lng, strDistance);


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
                    shopids = " tid in (" + shopids + ") ";
                    deliveryrecord = new shopdelivery().GetList(shopids);
                }

                IList<ShopFoodPictureInfo> alltags = CacheHelper.GetShopPicTag();
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
    }
}
