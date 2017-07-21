using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Weixin;
using System.Text.RegularExpressions;

namespace Html5
{
    public partial class showshopic : MasterPageBase
    {

        Points daltogo = new Points();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Points daltogo = new Points();
                int id = HjNetHelper.GetQueryInt("id", 0);
               
                string SqlWhere = String.Format("Shopid={0}", id);

                WebUtility.BindRepeater(rptppt, new ShopSurroundings().GetList(100,1,SqlWhere, "Sort", 1));


            }
        }

    }
}
