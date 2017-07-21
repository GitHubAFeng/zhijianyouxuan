using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;
using System.Web.UI.HtmlControls;

public partial class newIndex1 : System.Web.UI.Page
{
    EAddress daladdress = new EAddress();
    protected void Page_Load(object sender, EventArgs e)
    {

        //绑定城市列表
        City mycity = new City();
        IList<CityInfo> citylist = mycity.getCityPy();
        foreach (var item in citylist)
        {
            item.CityJuniorList = SectionProxyData.GetCityList().Where(m => m.ReveVar == item.ReveVar).ToList();
        }
        rptCtiy.DataSource = citylist;
        rptCtiy.DataBind();


        int cityid = HjNetHelper.GetQueryInt("cid", 0);
        CityInfo cityinfo = null;
        if (cityid != 0)
        {
            cityinfo = mycity.GetModel(cityid);
        }
        else
        {
            cityinfo = SectionProxyData.GetCityList()[0];
        }
        setCityData(cityinfo);

        ECustomerInfo user = UserHelp.GetUser();
        if (user != null)
        {
            string SqlWhere = string.Format("UserId={0}", user.DataID);
            IList<EAddressInfo> list = new List<EAddressInfo>();
            list = daladdress.GetList(15, 1, SqlWhere, "pri", 1);
            WebUtility.BindRepeater(rptaddress, list);
        }

        HtmlMeta desc = new HtmlMeta();
        desc.Name = "Description";
        desc.Content = WebUtility.GetDescription();
        Page.Header.Controls.Add(desc);
        HtmlMeta keywords = new HtmlMeta();
        keywords.Name = "keywords";
        keywords.Content = WebUtility.GetKeywords();
        Page.Header.Controls.Add(keywords);
    }


    public void setCityData(CityInfo cityinfo)
    {
        tbcityname.Value = cityinfo.cname;
        mapcityname.Value = cityinfo.cname;
        hfcityname.Value = cityinfo.cname; ;

        hidLat.Value = cityinfo.Lat;
        hidLng.Value = cityinfo.Lng;
        WebUtility.FixsetCookie("user_cityid", cityinfo.cid.ToString(), 365);
    }


}