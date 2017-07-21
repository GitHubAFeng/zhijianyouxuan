using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class qy_55tuan_AreaAdmin_buildtable_1ddd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        City dal = new City();

        Hangjing.Model.EAdminInfo model = UserHelp.GetAdmin();


        IList<CityInfo> list = dal.GetList(10, 1, "cid=" + model.CityID, "ReveInt", 1);
        WebUtility.BindRepeater(rptcity, list);
    }
}
