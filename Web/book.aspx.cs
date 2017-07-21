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

public partial class shop_book : System.Web.UI.Page
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
            AlertScript.RegScript(this.Page, UpdatePanel1, "alert('您还没有定位，点击确定开始定位！');gourl('shoplist.aspx');");
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
                AlertScript.RegScript(this.Page, UpdatePanel1, "alert('超过" + item.Name + "配送距离" + item.Inve1 + "公里，点击确定，重新选择商家！');gourl('shoplist.aspx');");
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
            img_licensePic.Src = togomodel.licensePic;
            hfisLicense.Value = "1";
        }

        if (togomodel.cateringPic != "" && togomodel.isCatering == 1)
        {
            img_cateringPic.Src = togomodel.cateringPic;
            hfisCatering.Value = "1";
        }

        this.Page.Title = togomodel.Name + "-" + SectionProxyData.GetSetValue(3);

        IList<PointsInfo> lists = new List<PointsInfo>();

        //绑定商家信息
        WebUtility.BindRepeater(rpshop, tlist);

        //菜品分类绑定
        IList<EFoodSortInfo> list = ProxyData.getTogoSort(id);
        WebUtility.BindRepeater(rptSortList, list);
        IList<FoodinfoInfo> foodlist = ProxyData.getTogoFood(id);
        foreach (var item in list)
        {
            item.Foodlist = foodlist.Where(p => p.FoodType == item.SortID).ToList();//.OrderBy(a => a.FPrice)
        }
        WebUtility.BindRepeater(rptSortList2, list);

        //精品菜品绑定
        BindFoodPackage();
        GetAllCommend();
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


    protected void BindFoodPackage()
    {
        IList<FoodPackagInfo> list = new List<FoodPackagInfo>();
        FoodPackag dal = new FoodPackag();
        list = dal.GetFoodPackage(Convert.ToInt32(togoID), SqlWhere);
        list = list.OrderBy(a => a.Price).ToList();
        if (list.Count == 0)
        {
            sort0.Style["display"] = "none";
            sort0_1.Style["display"] = "none";
        }
        WebUtility.BindRepeater(rptfoodpackage, list);
    }

    //评论
    protected void GetAllCommend()
    {

        string InveInt2 = SectionProxyData.GetSetValue(11);//0表示不审核，1表示要审核。
        string sql = "togoid = " + HjNetHelper.GetQueryInt("id", 0);
        AspNetPager1.RecordCount = daloption.GetCount(sql);
        rptopion.DataSource = daloption.GetList(5, AspNetPager1.CurrentPageIndex, sql, "dataid", 1);
        rptopion.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetAllCommend();
    }

    /// <summary>
    /// 提交评论
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Postt_Click(object sender, EventArgs e)
    {
        ECustomerInfo model = UserHelp.GetUser();
        if (model == null)
        {
            AlertScript.RegScript(Page, UpdatePanel1, "tipsWindown('提示信息', 'text:登录后再评论,您会获取更多惊喜!', '250', '150', 'true', '3000', 'true', 'text');test();");
            return;
        }
        ETogoOpinion dalopinion = new ETogoOpinion();

        int id = HjNetHelper.GetQueryInt("id", 0);
        int OrderID = dalorder.OrderDataID(model.DataID, id);
        if (model == null)
        {
            AlertScript.RegScript(Page, UpdatePanel1, "tipsWindown('提示信息', 'text:登录后再评论,您会获取更多惊喜!', '250', '150', 'true', '3000', 'true', 'text');test();");
            return;
        }
        else if (OrderID == 0)
        {
            if (dalorder.OrderDataIDstate(model.DataID, id) > 0)
            {
                AlertScript.RegScript(Page, UpdatePanel1, "tipsWindown('提示信息', 'text:很抱歉，您已评论过!', '250', '150', 'true', '3000', 'true', 'text');test();");
            }
            else
            {
                AlertScript.RegScript(Page, UpdatePanel1, "tipsWindown('提示信息', 'text:很抱歉，您没有订餐无法评论!', '250', '150', 'true', '3000', 'true', 'text');test();");
            }
        }
        else
        {
            ETogoOpinionInfo opinionmodel = new ETogoOpinionInfo();
            opinionmodel.UserID = model.DataID;
            opinionmodel.UserName = model.Name;
            opinionmodel.TogoID = HjNetHelper.GetQueryInt("id", 0);
            opinionmodel.Point = 0;
            opinionmodel.ServiceGrade = Convert.ToInt32(ddlservice.SelectedValue);
            opinionmodel.FlavorGrade = Convert.ToInt32(ddlflover.SelectedValue);
            opinionmodel.SpeedGrade = Convert.ToInt32(ddlspeed.SelectedValue);
            opinionmodel.PostTime = DateTime.Now;
            opinionmodel.Comment = WebUtility.InputText(this.textarea.Value);
            opinionmodel.Rcontent = "";
            opinionmodel.Rtype = 0;
            opinionmodel.Rtime = Convert.ToDateTime("1900-01-01");

            if (dalopinion.Add(opinionmodel) > 0)
            {
                dalopinion.setReviewData(opinionmodel.TogoID);

                string InveInt2 = SectionProxyData.GetSetValue(11);//0表示不审核，1表示要审核。
                if (InveInt2 == "0")
                {
                    ECustomer userBLL = new ECustomer();
                    ECustomerInfo customer = userBLL.GetModelByNameAPassword(model.Name, model.Password);
                    userBLL.addpoint(model.DataID, "评论获取积分", Convert.ToInt32(SectionProxyData.GetSetValue(23)));
                    UserHelp.SetLogin(customer);

                    dalorder.OrderUnid(OrderID);//设置些用户已评论
                    AlertScript.RegScript(Page, UpdatePanel1, "tipsWindown('提示信息', 'text:评论成功，谢谢您的关注!', '250', '150', 'true', '3000', 'true', 'text');test();");
                    GetAllCommend();
                }
            }
            else
            {
                AlertScript.RegScript(Page, "alert('提示信息', 'text:服务器繁忙，请稍后再试!', '250', '150', 'true', '3000', 'true', 'text');");
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
        string rs = "<span class=\"state\">" + sr + "</span>";


        if (status.ToString() == "0" || isbiseniss.ToString() == "0")
        {
            rs = "<span class=\"off\">" + sr + "</span>";
        }

        return rs;
    }
}
