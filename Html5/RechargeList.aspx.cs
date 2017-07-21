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
    public partial class mRechargeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int pagesize = 8;
            int pageindex = HjNetHelper.GetQueryInt("PageNo", 1);

            int dayorder = HjNetHelper.GetQueryInt("t", 0);

              ECustomerInfo user = UserHelp.GetUser();
              string sql = " PayState=1 ";
              if (user == null)
              {
                Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }
              else
              {
                  sql += " and UserId=" + user.DataID;
                 
              }
          
            IList<UserAddMoneyLogInfo> orderlist = new UserAddMoneyLog().GetList(pagesize, pageindex, sql, "dataid", 1);
            WebUtility.BindRepeater(rptorder, orderlist);
            int countNum = new UserAddMoneyLog().GetCount(sql);
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
                pages.InnerHtml = WebUtility.GetPageString(pageindex, pagecount, "RechargeList.aspx?a=1");
            }
        }
    }
}
