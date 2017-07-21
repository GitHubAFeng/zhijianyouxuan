using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AreaAdmin_testbuildSelect : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string cityid =WebUtility.FixgetCookie("admin_cityid");
            if (cityid != null && cityid != "" && cityid != "0")
            {
                //缓存
                rptcity.DataSource = SectionProxyData.GetCityList().Where(p => p.cid == Convert.ToInt32(cityid)).ToList<Hangjing.Model.CityInfo>();
                rptcity.DataBind();
            }
            else
            {
                //缓存
                rptcity.DataSource = SectionProxyData.GetCityList();
                rptcity.DataBind();
            }
           
        }
    }
}
