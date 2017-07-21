using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class showfood : System.Web.UI.Page
{
    Points daltogo = new Points();
    ETogoOpinion daloption = new ETogoOpinion();
    Custorder dalorder = new Custorder();
    /// <summary>
    /// 商家编号
    /// </summary>
    protected string togoID
    {
        set
        {
            ViewState["togoID"] = value;
        }
        get
        {
            return ViewState["togoID"] == null ? "0" : ViewState["togoID"].ToString();
        }
    }

    /// <summary>
    /// 条件查询条件
    /// </summary>
    protected string SqlWhere
    {
        set
        {
            ViewState["SqlWhere"] = value;
        }
        get
        {
            return ViewState["SqlWhere"] == null ? "0" : ViewState["SqlWhere"].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            string usercode = WebUtility.FixgetCookie("uc");
            if (usercode == null || usercode == "")
            {
                usercode = Guid.NewGuid().ToString();
                WebUtility.FixsetCookie("uc", usercode, 365);
            }
            hfcode.Value = usercode;

            GetTogoInfo();
        }
    }


    PointsInfo togomodel = null;
    public decimal distance = 0;
    int id = HjNetHelper.GetQueryInt("id", 0);

    /// <summary>
    /// 获取商家信息
    /// </summary>
    protected void GetTogoInfo()
    {
        int id = HjNetHelper.GetQueryInt("id", 0);
        togoID = id.ToString();
        SqlWhere = String.Format("Unid={0}", id);

        if (id == 1)//超市
        {
            Response.Redirect("market.aspx?id=1");
            return;
        }
        int iscollect = 0;//是否收藏，0表示没有，1表示已经收藏
        ECustomerInfo user = UserHelp.GetUser();
        if (user != null)
        {
            hidUid.Value = user.DataID.ToString();
            ETogoCollect dal_c = new ETogoCollect();
            string sql = " 1= 1 and togoid=" + id + " and userid=" + user.DataID;
            if (dal_c.GetTogoCount(sql) > 0)
            {
                iscollect = 1;
            }

        }

        string strDistance = " 1=1 ";
        string lat = WebUtility.FixgetCookie("mylat");
        string lng = WebUtility.FixgetCookie("mylng");
        if (lat == null || lat == "0" || lat == "")
        {
            AlertScript.RegScript(this.Page, "alert('您还没有定位，点击确定开始定位！');gourl('shoplist.aspx');");
            return;
        }

        IList<shopdeliveryInfo> deliveryrecord = new shopdelivery().GetList(" tid = " + id);
        IList<webPromotionConfigInfo> webPromotions = CacheHelper.GetWebPromotionConfig();
        IList<PointsInfo> shoplist = daltogo.GetDistanceListSuper(1, 1, SqlWhere, "unid", 1, lat, lng, strDistance);
        IList<ShopFoodPictureInfo> alltags = CacheHelper.GetShopPicTag();


        foreach (var item in shoplist)
        {
            item.opentimestr = Datetimes(item.Opentimes1.ToShortTimeString(), item.Opentimes2.ToShortTimeString(), item.Closetimes1.ToShortTimeString(), item.Closetimes2.ToShortTimeString());
            if (item.Inve1 < item.Distance)
            {
                AlertScript.RegScript(this.Page,  "alert('超过" + item.Name + "配送距离" + item.Inve1 + "公里，点击确定，重新选择商家！');gourl('shoplist.aspx');");
                return;
            }
            foreach (var record in deliveryrecord)
            {
                if (record.distancestart <= item.Distance && record.distanceend > item.Distance)
                {
                    item.SendFee = record.sendmoney;
                    item.SendLimit = record.minmoney;
                    break;
                }
            }

            item.pictags = new List<ShopFoodPictureInfo>();
            string[] tagids = WebUtility.delBrackets(item.OpenTime).Split(',');
            foreach (var tagid in tagids)
            {
                foreach (var tag in alltags)
                {
                    if (tagid.Trim() == tag.IID.ToString())
                    {
                        item.pictags.Add(tag);
                    }
                }
            }

        }
        togomodel = shoplist[0];
        togomodel.iscollect = iscollect;

        IList<webPromotionConfigInfo> shoppromotions = Hangjing.WebCommon.WebHelper.getShopPromotions(togomodel.Unid, togomodel.PType, togomodel.PEnd);
        foreach (var item in shoppromotions)
        {
            ShopFoodPictureInfo tag = new ShopFoodPictureInfo();
            tag.Title = item.revevar1;
            tag.Picture = "/images/jian_02.png";
            togomodel.pictags.Add(tag);
        }


        hfsendmoney.Value = Convert.ToInt32(togomodel.SendFee) + "";
        hfminmoney.Value = togomodel.SendLimit + "";

        //餐车的起送价
        limit.InnerHtml = togomodel.SendLimit.ToString();
        daltogo.UpdateValue("ViewTimes", togomodel.ViewTimes + 1, " where unid = " + id);//更改浏览量

        if (togomodel == null)
        {
            Response.Redirect("error.html");
        }

        //隐藏控件赋值
        this.hidTogoName.Value = togomodel.Name;
        this.hidTogoId.Value = id + "";
        hftogoGrade.Value = togomodel.Grade + "";
        hnTogoBusiness.Value = togomodel.isbisness + ""; //时间
        hnTogoStatus.Value = togomodel.Status + "";     //状态
        IList<PointsInfo> tlist = new List<PointsInfo>();
        tlist.Add(togomodel);
        if (togomodel.licensePic != "" && togomodel.isLicense == 1)
        {
            hfisLicense.Value = "1";
        }

        if (togomodel.cateringPic != "" && togomodel.isCatering == 1)
        {
            hfisCatering.Value = "1";
        }

        this.Page.Title = togomodel.Name + "-" + SectionProxyData.GetSetValue(3);

        IList<PointsInfo> lists = new List<PointsInfo>();

        //绑定商家信息
        WebUtility.BindRepeater(rpshop, tlist);

        IList<FoodinfoInfo> foodlist = new Foodinfo().GetList(1, 1, "unid=" + HjNetHelper.GetQueryInt("fid", 0), "unid", 1);

        WebUtility.BindRepeater(rptSortList2, foodlist);

    }

    /// <summary>
    /// 生成时间串
    /// </summary>
    /// <param name="Time1Start"></param>
    /// <param name="Time1End"></param>
    /// <param name="Time2Start"></param>
    /// <param name="Time2End"></param>
    /// <returns></returns>
    public string Datetimes(string Time1Start, string Time1End, string Time2Start, string Time2End)
    {
        string srt = string.Empty;
        if (Time1Start == Time2Start)
        {
            srt = Time1Start.ToString() + "-" + Time1End.ToString();
        }
        else
        {

            srt = Time1Start.ToString() + "-" + Time1End.ToString() + " | " + Time2Start.ToString() + "-" + Time2End.ToString();
        }
        return srt;
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
        string rs = "<span class=\"state\">" + sr + "</span>";


        if (status.ToString() == "0" || isbiseniss.ToString() == "0")
        {
            rs = "<span class=\"off\">" + sr + "</span>";
        }

        return rs;
    }
}
