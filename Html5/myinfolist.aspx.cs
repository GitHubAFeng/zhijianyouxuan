using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;


namespace Html5
{
    public partial class myinfolist : System.Web.UI.Page
    {  
        protected void Page_Load(object sender, EventArgs e)
        {
            string openid = Request["openid"];
            if (openid != null && openid != "")
            {
                WebUtility.FixsetCookie("openid", openid, 365);
            }

            ECustomerInfo user = UserHelp.GetUser();
            if (user == null)
            {
                Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            else
            {
                this.username.InnerText = user.Name;
                this.userphone.InnerText = user.Tell;
                this.userPoint.InnerHtml = new ECustomer().GetModel(user.DataID).Point.ToString();//用户积分 获取最新的

                lbpoint.InnerText = user.Point + "分";
                lbmoney.InnerText = user.Usermoney + "元";
            }

        }
    }
}
