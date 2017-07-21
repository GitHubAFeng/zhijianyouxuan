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

public partial class Popularfood : System.Web.UI.Page
{
    Foodinfo dalfood = new Foodinfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        string lat = WebUtility.FixgetCookie("mylat");
        string lng = WebUtility.FixgetCookie("mylng");

        if (lat == "" || lng == "" || lat == null || lng == null)
        {
            lat = "0";
            lng = "0";
            return;
        }

        IList<FoodinfoInfo> foodlist = dalfood.getHotFoods(lat, lng); ;
        WebUtility.BindRepeater(rptfoodlist, foodlist);
    }
}