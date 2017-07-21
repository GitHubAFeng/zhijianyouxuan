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
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using System.Collections.Generic;

public partial class Foot : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IList<aboutClassInfo> typelist = new aboutClass().GetList(5, 1, "1=1", "FullId", 1);
            IList<aboutusInfo> alist = new aboutClass().GetSortList(6, "1=1");
            foreach (aboutClassInfo x in typelist)
            {
                x.glist = alist.Where(p => p.SortId == x.Id).ToList<aboutusInfo>();             
            }
            WebUtility.BindRepeater(rptsort, typelist);
        }
    }

 
}
