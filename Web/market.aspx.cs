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

/// <summary>
/// 超市界面
/// </summary>
public partial class shop_market : System.Web.UI.Page
{

    Points daltogo = new Points();
    EFoodSort dalsort = new EFoodSort();
    Foodinfo dalfood = new Foodinfo();
    ETogoOpinion daloption = new ETogoOpinion();
    EAddress addressdal = new EAddress();
    Custorder dalorder = new Custorder();
    /// <summary>
    /// 商家编号
    /// </summary>
    protected int togoID
    {
        set
        {
            ViewState["togoID"] = value;
        }
        get
        {
            return ViewState["togoID"] == null ? 1 : Convert.ToInt32(ViewState["togoID"]);
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
            ECustomerInfo user = UserHelp.GetUser();
            if (user != null)
            {
                hidUid.Value = user.DataID.ToString();
            }

            string usercode = WebUtility.FixgetCookie("uc");
            if (usercode == null || usercode == "")
            {
                usercode = Guid.NewGuid().ToString();
                WebUtility.FixsetCookie("uc", usercode, 365);
            }
            hfcode.Value = usercode;

            AdTableInfo adinfo = new AdTable().GetModel(4);
            market_bgbox.Style["background-image"] = WebUtility.ShowPic(adinfo.AdImageAdrees);

            GetTogoInfo();
        }
    }


    PointsInfo togomodel = null;
    public decimal distance = 0;

    /// <summary>
    /// 获取商家信息
    /// </summary>
    protected void GetTogoInfo()
    {
      
        string strDistance = " 1=1 ";
        string lat = WebUtility.FixgetCookie("mylat");
        string lng = WebUtility.FixgetCookie("mylng");
        if (lat == null || lat == "0" || lat == "")
        {
            AlertScript.RegScript(this.Page,  "alert('您还没有定位，点击确定开始定位！');gourl('shoplist.aspx');");
            return;
        }

        togoID = HjNetHelper.GetQueryInt("id", 0);
        SqlWhere = String.Format("Unid={0}", togoID);
        if (togoID == 0)
        {
            string marketsql = " IsCallCenter=1 and cityid=" + WebUtility.get_userCityid();
            PointsInfo shop = daltogo.getshopList(marketsql).FirstOrDefault();
            if (shop == null)
            {
                AlertScript.RegScript(this.Page, "alert('本城市未开通超市模块');gourl('shoplist.aspx');");
                return;
            }
            else {
                Response.Redirect("market.aspx?id="+shop.Unid);
            }
        }


        IList<shopdeliveryInfo> deliveryrecord = new shopdelivery().GetList(" tid = " + togoID);

        IList<PointsInfo> shoplist = daltogo.GetDistanceListSuper(1, 1, SqlWhere, "unid", 1, lat, lng, strDistance);
        foreach (var item in shoplist)
        {
            item.opentimestr = Datetimes(item.Opentimes1.ToShortTimeString(), item.Opentimes2.ToShortTimeString(), item.Closetimes1.ToShortTimeString(), item.Closetimes2.ToShortTimeString());
            if (item.Inve1 < item.Distance)
            {
                AlertScript.RegScript(this.Page,"alert('超过" + item.Name + "配送距离" + item.Inve1 + "公里，点击确定，重新选择商家！');gourl('shoplist.aspx');");
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
        }
        togomodel = shoplist[0];

        hfsendmoney.Value = Convert.ToInt32(togomodel.SendFee) + "";
        hfminmoney.Value = togomodel.SendLimit + "";

        daltogo.UpdateValue("ViewTimes", togomodel.ViewTimes + 1, " where unid = " + togoID);//更改浏览量

        if (togomodel == null)
        {
            Response.Redirect("error.html");
        }

        //隐藏控件赋值
        this.hidTogoName.Value = togomodel.Name;
        this.hidTogoId.Value = togoID + "";
        hftogoGrade.Value = togomodel.Grade + "";
        hnTogoBusiness.Value = togomodel.isbisness + ""; //时间
        hnTogoStatus.Value = togomodel.Status + "";     //状态
        IList<PointsInfo> tlist = new List<PointsInfo>();
        tlist.Add(togomodel);

        this.Page.Title = togomodel.Name + "-" + SectionProxyData.GetSetValue(3);

        IList<PointsInfo> lists = new List<PointsInfo>();

        //绑定商家信息
        WebUtility.BindRepeater(rpshop, tlist);

        //菜品分类绑定
        IList<EFoodSortInfo> list = ProxyData.getTogoSort(togoID);
        WebUtility.BindRepeater(rptSortList, list);
        IList<FoodinfoInfo> foodlist = ProxyData.getTogoFood(togoID);
        foreach (var item in list)
        {
            item.Foodlist = foodlist.Where(p => p.FoodType == item.SortID).OrderBy(a => a.FPrice).ToList();
        }
        WebUtility.BindRepeater(rptSortList2, list);

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


        if (sr == "已打烊-Close" || sr == "暂停营业")
        {
            rs = "<span class=\"off\">" + sr + "</span>";
        }

        return rs;
    }
}
