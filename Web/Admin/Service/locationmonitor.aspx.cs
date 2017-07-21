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

/// <summary>
/// 订单调度系统
/// </summary>
public partial class qy_54tss_Admin_GpsSet_locationmonitor : System.Web.UI.Page
{

    Deliver daldel = new Deliver();
    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? "" : Convert.ToString(o);
        }
        set
        {
            ViewState["SqlWhere"] = value;
        }
    }


    protected string ordersort
    {
        get
        {
            object o = ViewState["ordersort"];
            return (o == null) ? "orderTime" : Convert.ToString(o);
        }
        set
        {
            ViewState["ordersort"] = value;
        }
    }

    /// <summary>
    /// 城市编号
    /// </summary>
    protected int cityid
    {
        get
        {
            object o = ViewState["cityid"];
            return (o == null) ? 0 : Convert.ToInt32(o);
        }
        set
        {
            ViewState["cityid"] = value;
        }
    }

    Custorder dal = new Custorder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (UserHelp.GetAdmin() == null)
            {
                Response.Redirect("../login.aspx");
                return;
            }
            WebUtility.checkOperator(1);
            ordersort = "deliverheaderid desc , SendTime";

            hidLat.Value = SectionProxyData.GetSetValue(4);
            hidLng.Value = SectionProxyData.GetSetValue(5);

            string cityname = "";

            cityid = HjNetHelper.GetQueryInt("cid", 0);
            if (cityid == 0)
            {
                string cityid_cookie = WebUtility.FixgetCookie("d_cityid");
                if (cityid_cookie == null || cityid_cookie == "")
                {
                    hfflag.Value = "1";
                    return;
                }
                else
                {
                    cityid = Convert.ToInt32(cityid_cookie);
                    cityname = Server.UrlDecode(WebUtility.FixgetCookie("d_cityname"));
                }
            }
            else
            {
                WebUtility.FixsetCookie("d_cityid", cityid.ToString(), 30);
                cityname = HjNetHelper.GetQueryString("cname");
                WebUtility.FixsetCookie("d_cityname", Server.UrlEncode(cityname), 30);
            }
            hfcityname.Value = cityname;
            hfcityid.Value = cityid.ToString();

            lbadminname.InnerText = UserHelp.GetAdmin().AdminName;
            this.snDate.InnerHtml = cityname + "[<a href=\"javascript:show_citytable();\" class='orange'>切换城市</a>]";

        }

    }

}
