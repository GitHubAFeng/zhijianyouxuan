using System;
using System.Collections;
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

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.DBUtility;
using System.Collections.Generic;

public partial class qy_54tss_Admin_GpsSet_OrderCrm : System.Web.UI.Page
{
    Points daltogo = new Points();
    Custorder dal = new Custorder();
    City dalcity = new City();
    protected string _sortname
    {
        set
        {
            ViewState["_sortname"] = value;
        }
        get
        {
            return ViewState["_sortname"] == null ? "sortnum" : ViewState["_sortname"].ToString();
        }
    }

    protected int _sortflag
    {
        set
        {
            ViewState["_sortflag"] = value;
        }
        get
        {
            return ViewState["_sortflag"] == null ? 0 : Convert.ToInt32(ViewState["_sortflag"].ToString());
        }
    }


    protected string sqlWhere
    {
        set
        {
            ViewState["sqlWhere"] = value;
        }
        get
        {
            return ViewState["sqlWhere"] == null ? "" : ViewState["sqlWhere"].ToString();
        }
    }

    /// <summary>
    /// 订单语句
    /// </summary>
    protected string ordersqlWhere
    {
        set
        {
            ViewState["ordersqlWhere"] = value;
        }
        get
        {
            return ViewState["ordersqlWhere"] == null ? "" : ViewState["ordersqlWhere"].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //清楚地址编号cookie
            //WebUtility.FixdelCookie("used_addressid");
            if (Request["user"] != null)
            {
                string uname = WebUtility.InputText(Request["user"]);
                string password = WebUtility.GetMd5(Request["p"]);
                EAdminInfo model = new EAdmin().GetModel(uname, password);
                UserHelp.AdminLogin(model);
            }
            WebUtility.BindList("id", "classname", SectionProxyData.GetSortList(), ddlsection);
            //商家信息
            _sortname = "sortnum";
            _sortflag = 1;
            //显示在线点餐的商家
            sqlWhere = "  1=1 and IsDelete = 0 and Star = 1 and InUse = 'Y'";
            Databind(_sortname);

            //地图默认坐标
            hidLat.Value = Map.DefautLocal.getlocalInfo().Lat;
            hidLng.Value = Map.DefautLocal.getlocalInfo().Lng;

            string cityid = WebUtility.FixgetCookie("admin_cityid");
            if (cityid != null && cityid != "" && cityid != "0")
            {
                CityInfo cinfo = dalcity.GetModel(Convert.ToInt32(cityid));
                hfcityname.Value = cinfo.cname;
            }
            else
            {
                hfcityname.Value = SectionProxyData.GetSetValue(25);
            }
        }
    }

    protected void Databind(string sortname)
    {
        string lat = hidLat.Value;
        string lng = hidLng.Value;

        if (lat == "" || lng == "")
        {
            return;
        }


        string strDistance = " distance <= Inve1 and Status=1  ";

        int recoundcount = daltogo.GetCountWidthDistance(sqlWhere, sortname, _sortflag, lat, lng, strDistance);
        AspNetPager1.RecordCount = recoundcount;
        IList<PointsInfo> list = daltogo.GetDistanceListSuper(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, sqlWhere, sortname, _sortflag, lat, lng, strDistance);

        IList<shopdeliveryInfo> deliveryrecord = new List<shopdeliveryInfo>();

        string shopids = "";
        foreach (var item in list)
        {
            shopids += item.Unid + ",";
        }
        shopids = WebUtility.dellast(shopids);
        deliveryrecord = new List<shopdeliveryInfo>();
        if (shopids != "")
        {
            shopids = " tid in (" + shopids + ") ";
            deliveryrecord = new shopdelivery().GetList(shopids);
        }

        foreach (var item in list)
        {
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

        this.rptJoinTogolist.DataSource = list;
        this.rptJoinTogolist.DataBind();
        if (list.Count == 0)
        {
            divnocord.Style["display"] = "";
        }
        else
        {
            divnocord.Style["display"] = "none";
        }
        div_notice.Visible = false;
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Databind(_sortname);
    }

    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void search_Click(object sender, EventArgs e)
    {
        sqlWhere = "  1=1 and IsDelete = 0 and Star = 1 and InUse = 'Y'";
        string _tbshopkey = WebUtility.InputText(tbshopkey.Value);
        if (_tbshopkey == "" || _tbshopkey != "请输入商家名称")
        {
            sqlWhere += " and ( name like'%" + _tbshopkey + "%' or  address like'%" + _tbshopkey + "%')";
        }

        Databind(_sortname);
    }

}
