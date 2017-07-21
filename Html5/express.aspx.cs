using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

namespace Html5
{
    public partial class express : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hfcityjson.Value = JsonConvert.SerializeObject(CacheHelper.GetCityList());
            hffeesjon.Value = SectionProxyData.GetSetValue(66);
            string openid = Request["openid"];
            if (openid != null && openid != "")
            {
                WebUtility.FixsetCookie("openid", openid, 365);
            }
        }
    }
}