using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;
using System.Text.RegularExpressions;

public partial class shoplist : MasterPageBase
{
    Points daltogo = new Points();
    EAddress daladdress = new EAddress();
    protected string titles = string.Empty;

    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? string.Empty : o.ToString();
        }
        set
        {
            ViewState["SqlWhere"] = value;
        }
    }

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
            string temp = "SortNum";//Status营业状态

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

    /// <summary>
    /// 纬度
    /// </summary>
    protected string blat
    {
        set
        {
            ViewState["blat"] = value;
        }
        get
        {
            return ViewState["blat"] == null ? "" : ViewState["blat"].ToString();
        }
    }

    /// <summary>
    /// 经度
    /// </summary>
    protected string blng
    {
        set
        {
            ViewState["blng"] = value;
        }
        get
        {
            return ViewState["blng"] == null ? "" : ViewState["blng"].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlMeta desc = new HtmlMeta();
        desc.Name = "Description";
        desc.Content = WebUtility.GetDescription();
        Page.Header.Controls.Add(desc);

        HtmlMeta keywords = new HtmlMeta();
        keywords.Name = "keywords";
        keywords.Content = WebUtility.GetKeywords();
        Page.Header.Controls.Add(keywords);

        if (!IsPostBack)
        {
            sortflag = 1;
            hfcityname.Value = WebUtility.get_userCityName();


            hidLat.Value = Map.DefautLocal.getlocalInfo().Lat;
            hidLng.Value = Map.DefautLocal.getlocalInfo().Lng;

            string cityid = WebUtility.get_userCityid();
            SqlWhere = "  1=1 and IsDelete = 0 and Star = 1 and InUse = 'Y'";//已经审核的
            if (cityid != "0")
            {
                SqlWhere += " and cityid =" + cityid;
            }

            //设置当前排序
            setCurrentSort();

            int islocal = 0;//是否定位，从地图上过来，或者地址簿中过，及有地图的都叫定位
            string myaddress = "";

            //更换地址或者地图选址
            if (Request["addr"] != null)
            {
                myaddress = Server.UrlDecode(Request["addr"]);
                WebUtility.FixsetCookie("mylat", Request["lat"], 30);
                WebUtility.FixsetCookie("mylng", Request["lng"], 30);

                hfaddresskey.Value = myaddress;

                blat = Request["lat"];
                blng = Request["lng"];
                islocal = 1;
            }

            //绑定更换地址
            ECustomerInfo user = UserHelp.GetUser();
            if (user != null)
            {
                string sql = string.Format("UserId={0}", user.DataID);
                IList<EAddressInfo> list = daladdress.GetList(10, 1, sql, "pri", 1);
                WebUtility.BindRepeater(rptaddress, list);//绑定更换地址

                int addressid = HjNetHelper.GetQueryInt("addressid", 0);
                if (addressid > 0)
                {
                    WebUtility.FixsetCookie("useraddr_id", addressid.ToString(), 30);
                    daladdress.UpdateDefaut(addressid, user.DataID);
                }

                if (list.Count > 0 && Request["addr"] == null)
                {
                    WebUtility.FixsetCookie("mylat", list[0].Lat, 30);
                    WebUtility.FixsetCookie("mylng", list[0].Lng, 30);
                    blat = list[0].Lat;
                    blng = list[0].Lng;
                    islocal = 1;
                    myaddress = list[0].Address;
                    //转到此界面。保证有参数
                    string url = "shoplist.aspx?lat=" + blat + "&lng=" + blng + "&addr=" + Server.UrlEncode(Server.UrlEncode(myaddress)) + "&from=m&addressid=" + list[0].DataID;
                    Response.Redirect(url);
                    return;
                }
                if (list.Count == 0)
                {
                    this.myAddress.InnerHtml = "";
                }
            }

            basedata();

            if (islocal == 0 || blat == null || blat == "")
            {
                return;
            }
            loaderbox.Style["display"] = "none";
            this.myAddress.InnerHtml = myaddress;

            WebUtility.FixsetCookie("myaddress", Server.UrlEncode(myaddress), 30);

            Getshoplist();

        }
    }

    /// <summary>
    /// 绑定基础数据
    /// </summary>
    protected void basedata()
    {
        string cityid = WebUtility.get_userCityid();
        WebUtility.BindRepeater(rptppt, SectionProxyData.GetPPTList().Where(a => a.Reve2 == "1").ToList());
        WebUtility.BindRepeater(rpttogosortlist, SectionProxyData.GetSortList());
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

        //当前已经选择的条件的列表,前台用repeater绑定
        IList<string> selectedcondition = new List<string>();
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
                case "r"://区域
                    {
                        hfcursid.Value = item.Value;
                        SqlWhere += " and  charindex('{" + item.Value + "}',MgrCell)>=1 ";
                    }
                    break;
                case "s"://分类
                    {
                        SqlWhere += " and  charindex('{" + item.Value + "}',category)>=1 ";
                        hfcursortid.Value = item.Value;
                    }
                    break;
                case "a"://起送价
                    {
                        hfcursendmoney.Value = item.Value;
                        SqlWhere += " and EXISTS (SELECT tid FROM dbo.shopdelivery WHERE tid = Points.Unid AND minmoney <= " + item.Value + ") ";
                    }
                    break;
                case "l"://配送类型
                    {
                        SqlWhere += " and sentorg= 0 ";
                        hfcursaleid.Value = item.Value;
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
        string url = "shoplist.aspx?c=1";
        if (Request["lat"] != null)
        {
            url += "&lat=" + HjNetHelper.GetQueryString("lat");
        }
        if (Request["lng"] != null)
        {
            url += "&lng=" + HjNetHelper.GetQueryString("lng");
        }
        if (Request["addressid"] != null)
        {
            url += "&addressid=" + HjNetHelper.GetQueryString("addressid");
        }
        if (Request["from"] != null)
        {
            url += "&from=m";
        }
        if (Request["addr"] != null)
        {
            url += "&addr=" + Server.UrlEncode(Request["addr"].ToString());
        }

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
    /// 获取商家列表
    /// </summary>
    protected void Getshoplist()
    {
        IList<ShopFoodPictureInfo> alltags = CacheHelper.GetShopPicTag();
        //热门
        string strDistance = " distance <= Inve1 and Status=1  ";
        IList<shopdeliveryInfo> deliveryrecord = new List<shopdeliveryInfo>();
        IList<webPromotionConfigInfo> shoppromotions = new List<webPromotionConfigInfo>();

        IList<ShopDataInfo> sortlist = SectionProxyData.GetSortList();

        IList<PointsInfo> shoplist = daltogo.GetDistanceListSuper(100, 1, SqlWhere, sortword, sortflag, blat, blng, strDistance);
        //查询本页所有商家配送段记录，再根据此及距离，得到配送费，起送价

        string shopidhot = "";
        foreach (var item in shoplist)
        {
            shopidhot += item.Unid + ",";
        }
        shopidhot = WebUtility.dellast(shopidhot);

        if (shopidhot != "")
        {
            deliveryrecord = new shopdelivery().GetList(" tid in (" + shopidhot + ") ");
            shoppromotions = new webPromotionConfig().GetList(999, 1, " shopid in (" + shopidhot + ") and isopen=1 and shopid > 0 and startdate <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND enddate >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ", "pId", 1, 0);
        }

        IList<webPromotionConfigInfo> webPromotions = CacheHelper.GetWebPromotionConfig();
        foreach (var item in shoplist)
        {
            item.pictags = new List<ShopFoodPictureInfo>();
            string[] tagids = WebUtility.delBrackets(item.OpenTime).Split(',');
            foreach (var id in tagids)
            {
                foreach (var tag in alltags)
                {
                    if (id.Trim() == tag.IID.ToString())
                    {
                        item.pictags.Add(tag);
                    }
                }
            }


            switch (item.PType)
            {
                case 10:
                    {
                        foreach (var promotion in shoppromotions.Where(a => a.shopid == item.Unid).ToList())
                        {
                            ShopFoodPictureInfo tag = new ShopFoodPictureInfo();
                            tag.Title = promotion.revevar1;
                            tag.Picture = "/images/jian_02.png";
                            item.pictags.Add(tag);
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
                                    tag.Picture = "/images/jian_02.png";
                                    item.pictags.Add(tag);
                                }

                            }
                        }
                    }
                    break;
                default:
                    break;
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

        IList<shopgroup> groups = new List<shopgroup>();
        {
            shopgroup group = new shopgroup();
            group.headhtml = "<div class=\"hot-restaurant\">热门餐厅</div>";
            group.shops = shoplist.Where(a => a.ID == "1").ToList();

            groups.Add(group);
        }
        {
            shopgroup group = new shopgroup();
            group.headhtml = "<div class=\"open-restaurant\">营业中</div>";
            group.shops = shoplist.Where(a => a.ID == "0" && a.isonline == 1).ToList();
            groups.Add(group);
        }
        {
            shopgroup group = new shopgroup();
            group.headhtml = "<div class=\"close-restaurant\">休息中</div>";
            group.shops = shoplist.Where(a => a.ID == "0" && a.isonline == 0).ToList();
            groups.Add(group);
        }

        rptgroup.DataSource = groups;
        rptgroup.DataBind();

        if (shoplist.Count == 0)
        {
            this.divnocord.Style["display"] = "block";
        }




    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Getshoplist();
    }

    //搜索
    protected void search_Click(object sender, EventArgs e)
    {
        string cityid = WebUtility.get_userCityid();
        SqlWhere = " 1=1 and IsDelete = 0 and Star = 1 and InUse = 'Y'";
        if (cityid != "0")
        {
            SqlWhere += " and cityid =" + cityid;
        }

        if (tbshopkeyword.Value != "")
        {
            SqlWhere += " and (Points.unid in (select FPMaster from foodinfo where FoodName like '%" + WebUtility.InputText(tbshopkeyword.Value) + "%') ";
            SqlWhere += " or Points.Name like '%" + WebUtility.InputText(tbshopkeyword.Value.Trim()) + "%')";//商家
        }
        setCurrentSort();

        Getshoplist();
    }


}




