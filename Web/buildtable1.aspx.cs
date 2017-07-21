using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

public partial class buildtable1 : System.Web.UI.Page
{
    EBuilding bdal = new EBuilding();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int sectionid = Convert.ToInt32(Request[""]);
            int pagenum = Convert.ToInt32(Request[""]);

            IList<BuildingInfo> blist = bdal.GetList(30, 1, "1=1", "DataID", 1);
            rptTogolist.DataSource = blist;
            rptTogolist.DataBind();
        }
    }
}
