using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hangjing.Common;

using Hangjing.Weixin;

namespace Html5
{
    public partial class mycommission : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ECustomerInfo user = UserHelp.GetUser();
            if (user == null)
            {
                Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            else
            {
                this.username.InnerText = user.Name;
                user = new ECustomer().GetModel(user.DataID);
                isdistributor.Value = user.PayPWDAnswer;


                if (user.PayPWDAnswer.Trim() == "1" && user.qrcodeurl.Length == 0)
                {
                    WechatQRcode qrcode = new WechatQRcode();
                    qrcode.getTicket(user.DataID);
                 
                    string imgurl =  HJQRCode.BuildQRCode("", user.DataID.ToString(), qrcode.qrcodeurl);
                    if (imgurl.Length > 0)
                    {
                        new ECustomer().UpdateUserValue(user.DataID, "qrcodeurl", imgurl);
                    }
                  

                    user.qrcodeurl = imgurl;

                }

                myrq.Src = WebUtility.ShowLocalPic(user.qrcodeurl);


                UserHelp.SetLogin(user);
            }

           
        }
    }
}