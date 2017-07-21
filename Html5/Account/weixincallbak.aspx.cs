using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Weixin;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

namespace Html5.Account
{
    /// <summary>
    /// 微信授权回调界面
    /// </summary>
    public partial class weixincallbak : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string returnurl = Request["returnurl"];
            string utype = Request["utype"];
            if (Request["code"] != null)
            {
                string code = Request["code"];
                WebOAuth wo = new WebOAuth(Context, code);
                string openid = wo.getOpenid();

                WebUtility.FixsetCookie("openid", openid, 7);

                if (returnurl != null && returnurl != "")
                {
                    Response.Redirect(returnurl);
                }
                else
                {
                    Response.Redirect("/index.aspx");
                }
            }
            else
            {
                Response.Write("授权失败,请重试");
            }

        }
    }
}