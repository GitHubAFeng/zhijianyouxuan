using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

namespace Html5
{
    public partial class myshopcard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int pagesize = 8;
            int pageindex = HjNetHelper.GetQueryInt("PageNo", 1);

            ECustomerInfo user = UserHelp.GetUser();
            string sql = " 1=1 ";
            if (user == null)
            {
            }
            else
            {
                sql += " and userid=" + user.DataID;
            }

            IList<ShopCardInfo> orderlist = new ShopCard().GetList(pagesize, pageindex, sql, "isused asc, usergettime", 1);

            WebUtility.BindRepeater(rptorder, orderlist);

            int countNum = new ShopCard().GetCount(sql);
            int pagecount = 0;

            if (countNum % pagesize == 0)
            {
                pagecount = countNum / pagesize;
            }
            else
            {
                pagecount = countNum / pagesize + 1;
            }

            if (pagecount <= 1)
            {
                pages.Style["display"] = "none";
            }
            else
            {
                pages.InnerHtml = WebUtility.GetPageString(pageindex, pagecount, "myshopcard.aspx?a=1");
            }
        }
        public DateTime getdate(object gdate, object deadline)
        {
            DateTime gdates = Convert.ToDateTime(gdate);
            int daedline = Convert.ToInt32(deadline);
            DateTime strdate = gdates.AddDays(daedline);
            return strdate;
        }
    }
}
