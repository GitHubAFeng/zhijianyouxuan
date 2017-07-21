using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class d : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            androidurl.Value = ConfigurationManager.AppSettings["DeliverAndroidUrl"];
            iphoneurl.Value = ConfigurationManager.AppSettings["DeliverIosUrl"];
        }
    }
}