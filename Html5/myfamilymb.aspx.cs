using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Html5
{
    public partial class myfamilymb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            ECustomerInfo user = UserHelp.GetUser();
            if (user == null)
            {
                Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }


            int type = HjNetHelper.GetQueryInt("type", 1);
            string[] type2word = { "", "onegradeID", "twogradeID", "thressgradeID" };

            string[] numstr = {"","一级", "二级", "三级" };


            string sql = " EXISTS(SELECT dId FROM dbo.distributor WHERE userid = ECustomer.DataID AND  " + type2word[type] + " = " + user.DataID + ") ";
            int pageindex = HjNetHelper.GetQueryInt("PageNo", 1);

            string url = "myfamilymb.aspx?type="+type;
            int pagesize = 8;
            IList<ECustomerInfo> orderlist = new ECustomer().GetList(pagesize, pageindex, sql, "dataid", 1);

            WebUtility.BindRepeater(rptusers, orderlist);

            int countNum = new ECustomer().GetCount(sql);
            int pagecount = 0;

            if (countNum % pagesize == 0)
            {
                pagecount = countNum / pagesize;
            }
            else
            {
                pagecount = countNum / pagesize + 1;
            }


            allchild.InnerText = countNum.ToString();
            gradelabel.InnerText = numstr[type];

            if (pagecount <= 1)
            {
                pages.Style["display"] = "none";
            }
            else
            {
                pages.InnerHtml = WebUtility.GetPageString(pageindex, pagecount, url);
            }


        }
    }
}