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
using System.Text.RegularExpressions;

namespace Html5
{
    public partial class hotstores : System.Web.UI.Page
    {


        Foodinfo dalfood = new Foodinfo();

        protected void Page_Load(object sender, EventArgs e)
        {

            string lat = WebUtility.FixgetCookie("mylat");
            string lng = WebUtility.FixgetCookie("mylng");
          
            WebUtility.BindRepeater(rptppt, SectionProxyData.GetPPTList().Where(a => a.Reve2 == "2").ToList());

            if (lat == "" || lng == "" || lat == null || lng == null)
            {
                lat = "0";
                lng = "0";
                return;
            }

            IList<FoodinfoInfo> foodlist = dalfood.getHotFoods(lat, lng);
            WebUtility.BindRepeater(rptfoodlist, foodlist);
            back.HRef = "Togolist.aspx?islocal=1&address=" + WebUtility.FixgetCookie("address") + "&lat=" + lat + "&lng=" + lng;


        }
    }
}