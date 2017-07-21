using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.SQLServerDAL;

// CopyRight (c) 2009-2012 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2012-03-12

public partial class APP_Android_Deliver_ResetPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        Deliver dal = new Deliver();
        StringBuilder json = new StringBuilder("");

        string userid = Request["userid"].ToString();
        string oldpassword = Request["oldpassword"].ToString();
        string newpassword = Request["newpassword"].ToString();
        string state = "0";

        DeliverInfo info = new DeliverInfo();
        info = dal.GetModel(Convert.ToInt32(userid));

        if (WebUtility.GetMd5(oldpassword.Trim()) == info.Password)
        {
            if (dal.ChangePassword(Convert.ToInt32(userid), WebUtility.GetMd5(newpassword.Trim())) == 1)
            {
                state = "1";
            }
            else
            {
                state = "-1";
            }
        }
        else
        {
            state = "0";
        }

        Response.Write("{\"userid\":\"" + userid + "\",\"state\":\"" + state + "\"}");

        Response.End();
    }
}
