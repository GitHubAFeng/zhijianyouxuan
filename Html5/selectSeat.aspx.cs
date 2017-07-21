using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Newtonsoft.Json;

namespace Html5
{
    public partial class selectSeat : System.Web.UI.Page
    {
        Points daltogo = new Points();
        EFoodSort dalsort = new EFoodSort();
        Foodinfo dalfood = new Foodinfo();
        ETogoOpinion dal = new ETogoOpinion();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = HjNetHelper.GetQueryInt("id", 0);
                GetTogoInfo();
                string pageurl = "ShowTogo.aspx?id=" + id;
                togourl.HRef = "shopdetail.aspx?id=" + id;

                IList<EFoodSortInfo> sortlist = dalsort.GetListByTogoNum(id);

                try
                {
                    int count = dal.GetCount("TogoID=" + id);
                    IList<ETogoOpinionInfo> infolist = dal.GetList(1000, 1, "TogoID=" + id, "PostTime", 1);
                    int ServiceGrade = 0;
                    int FlavorGrade = 0;
                    int SpeedGrade = 0;
                    int all = 0;
                    foreach (var item in infolist)
                    {
                        ServiceGrade += item.ServiceGrade;
                        FlavorGrade += item.FlavorGrade;
                        SpeedGrade += item.SpeedGrade;
                        all += 15;
                    }
                    
                    if (count > 0)
                    {
                        int rev = ((ServiceGrade + FlavorGrade + SpeedGrade) * 100 / all);
                    }
                }
                catch (Exception ex)
                {
                    HJlog.toLog(ex.ToString());
                }
                string sql = " FPMaster =" + id + " and InUse = 'y' ";
                int sortid = HjNetHelper.GetQueryInt("sortid", 0);
                if (sortid > 0)
                {
                    sql += " and foodtype = " + sortid;
                    pageurl += "&sortid=" + sortid;
                }
                int pagesize = 1000;
                int pageindex = HjNetHelper.GetQueryInt("PageNo", 1);

                int countNum = dalfood.GetCount(sql);
                int pagecount = 0;

                if (countNum % pagesize == 0)
                {
                    pagecount = countNum / pagesize;
                }
                else
                {
                    pagecount = countNum / pagesize + 1;
                }

                IList<FoodinfoInfo> foodlist = dalfood.GetList(pagesize, pageindex, sql, "OrderNum", 1);
                //WebUtility.BindRepeater(rptFood, foodlist);

                //WebUtility.BindRepeater(rptmoresort, sortlist);//显示全部分裂

                string openid = Request["openid"];
                if (openid != "")
                {
                    WebUtility.FixsetCookie("openid", openid, 1);
                }

                //if (pagecount <= 1)
                //{
                //    pages.Style["display"] = "none";
                //}
                //else
                //{
                //    pages.InnerHtml = WebUtility.GetPageString(pageindex, pagecount, pageurl);
                //}

                StyleAndAttr foodext = dalfood.getAllByShopid(id);
                hfstyle.Value = JsonConvert.SerializeObject(foodext.styles);
                hfattr.Value = JsonConvert.SerializeObject(foodext.attrs);



            }
        }
        PointsInfo togomodel = null;
        public decimal distance = 0;
        /// <summary>
        /// 获取商家信息
        /// </summary>
        protected void GetTogoInfo()
        {
            int id = HjNetHelper.GetQueryInt("id", 0);
            string SqlWhere = String.Format("Unid={0}", id);

           
            string strDistance = " 1=1 ";
            string lat = WebUtility.FixgetCookie("mylat");
            string lng = WebUtility.FixgetCookie("mylng");
            string address = Server.UrlDecode(WebUtility.FixgetCookie("address"));
            back.HRef = "waimaijie.aspx?islocal=1";

            HJlog.toLog("lat=" + lat + "  lng=" + lng);

            if (lat == "" || lng == "" || lat == null || lng == null)
            {
                lat = "0";
                lng = "0";
                return;
            }
            IList<shopdeliveryInfo> deliveryrecord = new shopdelivery().GetList(" tid = " + id);

            IList<PointsInfo> shoplist = daltogo.GetDistanceListSuper(1, 1, SqlWhere, "unid", 1, lat, lng, strDistance);
            foreach (var item in shoplist)
            {
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
            
            //餐车的起送价
            hfminimoney.Value = togomodel.SendLimit.ToString();
            daltogo.UpdateValue("ViewTimes", togomodel.ViewTimes + 1, " where unid = " + id);//更改浏览量
            hftogotype.Value = togomodel.SN1.ToString();
            hnTogoBusiness.Value = togomodel.isbisness + ""; //时间
            hnTogoStatus.Value = togomodel.Status + "";     //状态
            this.hidTogoName.Value = togomodel.Name;

            //spansendmoney.InnerText = togomodel.SendFee.ToString();
            hfsendfree.Value = togomodel.SendFee.ToString();//配送费
            h1togoname.InnerHtml = "<img src=\"images/logo_weixin.png\" style=\"display:none;\"> " + WebUtility.Left(togomodel.Name, 5);
            hffreemoney.Value = togomodel.PTimes.ToString();//满多少免配送费，0表示未启用
        }
    }
}
