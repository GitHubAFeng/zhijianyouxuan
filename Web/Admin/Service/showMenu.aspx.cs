using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.DBUtility;

public partial class qy_54tss_Admin_Service_showMenu : System.Web.UI.Page
{
    EFoodSort dalsort = new EFoodSort();
    Foodinfo dalfood = new Foodinfo();
    Points daltogo = new Points();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            IList<EFoodSortInfo> list = dalsort.GetListByTogoNum(id);

            rptFoodSortList.DataSource = list;
            rptFoodSortList.DataBind();
            rptFoodSort.DataSource = list;
            rptFoodSort.DataBind();

            IList<shopdeliveryInfo> deliveryrecord = new shopdelivery().GetList(" tid = " + id);

            string strDistance = " 1=1 ";

            string lat = WebUtility.FixgetCookie("mylat");
            string lng = WebUtility.FixgetCookie("mylng");

            IList<PointsInfo> shoplist = daltogo.GetDistanceListSuper(1, 1, "Points.unid=" + id, "unid", 1, lat, lng, strDistance);
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


            PointsInfo model = shoplist[0];
            this.hidTogoName.Value = model.Name;
            this.hidTogoId.Value = id + "";
            hnTogoStatus.Value = model.Status.ToString();
            hidUid.Value = WebUtility.FixgetCookie("crm_uid");
            hftime.Value = "0";
            int jstatus = model.Status;

            strongRemark.InnerHtml = model.SendLimit + "";
            strongsendcount.InnerHtml = model.SendFee + "";
            strongsendtime.InnerHtml = model.senttime.ToString();

            string usercode = WebUtility.FixgetCookie("uc");
            if (usercode == null || usercode == "")
            {
                usercode = WebUtility.GetRandom(10);
                WebUtility.FixsetCookie("uc", usercode, 1);
            }
            hfcode.Value = usercode;


        }
    }

    protected void rptProductSort_ItemCommand(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rpt = (Repeater)e.Item.FindControl("rptFoodList");
            EFoodSortInfo model = (EFoodSortInfo)e.Item.DataItem;
            int sordid = Convert.ToInt32(model.SortID);
            if (model != null)
            {
                string sql = "foodtype = " + sordid + " and FPMaster =" + HjNetHelper.GetQueryString("id") + " and InUse = 'y'";
                IList<FoodinfoInfo> list = dalfood.GetList(100, 1, sql, "OrderNum", 1);
                rpt.DataSource = list;
                rpt.DataBind();

                string name = hfname.Value;
                for (int i = 0; i < list.Count; i++)
                {
                    string py = "-";
                    if (name == "")
                    {
                        name = list[i].Unid + "_" + list[i].FoodName + "_" + py;
                    }
                    else
                    {
                        name += "," + list[i].Unid + "_" + list[i].FoodName + "_" + py;
                    }
                }
                hfname.Value = name;
            }
        }
    }

}
