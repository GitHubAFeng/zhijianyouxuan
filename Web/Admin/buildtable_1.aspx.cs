using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class qy_55tuan_Admin_buildtable_1ddd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WebUtility.BindRepeater(rptcity, SectionProxyData.GetCityList());
    }
}
