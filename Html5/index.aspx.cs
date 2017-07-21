using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;
using Newtonsoft.Json;


namespace wap
{
    public partial class index : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            EAddress daladdress = new EAddress();
            if (!IsPostBack)
            {
                lat.Value = SectionProxyData.GetSetValue(4);
                lng.Value = SectionProxyData.GetSetValue(5);

                string _lat = WebUtility.FixgetCookie("lat");
                string _lng = WebUtility.FixgetCookie("lng");

                string openid = Request["openid"];
                if (openid != null && openid != "")
                {
                    WebUtility.FixsetCookie("openid", openid, 1);
                }

                rptCtiy.DataSource = CacheHelper.GetCityList();
                rptCtiy.DataBind();

                hfcityjson.Value = JsonConvert.SerializeObject(CacheHelper.GetCityList());

            }
        }
    }
}