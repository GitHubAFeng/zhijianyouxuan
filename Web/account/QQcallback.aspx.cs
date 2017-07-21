using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Configuration;

public partial class account_QQcallback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string key = ConfigurationManager.AppSettings["ConsumerKey"];
        string secret = ConfigurationManager.AppSettings["ConsumerSecret"];
        //从登录页面转到这个页面s
        string url = "https://graph.qq.com/oauth2.0/authorize?response_type=code&client_id=" + key + "&redirect_uri=" + WebUtility.GetConfigsite() + "/Account/_QQcallback.aspx";
        url += "&scope=get_user_info";
        url += "&state=1";
        url += "&display=1";

        Response.Redirect(url);

    }

}
