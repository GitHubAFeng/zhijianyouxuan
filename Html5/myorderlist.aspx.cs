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
    public partial class myorderlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int pagesize = 8;
            int pageindex = HjNetHelper.GetQueryInt("PageNo", 1);

            int dayorder = HjNetHelper.GetQueryInt("t", 0);

              ECustomerInfo user = UserHelp.GetUser();
              string openid = "";
              string sql = " 1=1 ";
              if (user == null)
              {
                  openid = WebUtility.FixgetCookie("openid");
                  if (openid == null || openid == "")
                  {
                      return;
                  }
                  sql += " and tempcode = '" + openid + "'";
              }
              else
              {
                  sql += " and UserId=" + user.DataID;

                  if (dayorder==1)
                  {
                      sql += " and OrderDateTime > '" + DateTime.Now.ToShortDateString() + "' and OrderDateTime < '" + DateTime.Now.ToString() + "'";
                  }
                 
              }
          
            IList<CustorderInfo> orderlist = new Custorder().GetList(pagesize, pageindex, sql, "Unid", 1);
            //添加支付状态 2015-11-24 
            foreach (var item in orderlist)
            {
                item.cityid = 0;
                if (Hangjing.WebCommon.WebHelper.CanPayAgain(item) && item.paymode == 5)//微信没有支付宝
                {
                    item.cityid = 1;
                }
            }


            WebUtility.BindRepeater(rptorder, orderlist);

            int countNum = new Custorder().GetCount(sql);
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
                pages.InnerHtml = WebUtility.GetPageString(pageindex, pagecount, "MyOrderList.aspx?a=1");
            }
        }
    }
}
