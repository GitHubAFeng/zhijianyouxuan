#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-6-12 16:12:11.
* E-Mail: jijunjian@ihangjing.com
*********************************************** */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class shop_logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.Logout_Togo();


        string url = WebUtility.GetUrl("../tlogin.aspx");
        Response.Redirect(url);     

    }
}
