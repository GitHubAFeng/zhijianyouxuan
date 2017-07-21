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

public partial class APP_Android_SaveUserInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        StringBuilder json = new StringBuilder("");
        string state = "0";

        Deliver dal = new Deliver();

        string userid = Request["userid"].ToString();
        string truename = Request["truename"].ToString();
        string phone = Request["phone"].ToString();
        string username = Request["username"].ToString();


        DeliverInfo info = new DeliverInfo();
        info = dal.GetModel(Convert.ToInt32(userid));

        //检查昵称是否有重复的
        if (dal.GetCount("[username] ='" + username + "'") > 1)
        {
            state = "0";
        }
        else
        {
            info.Name = truename;
            info.Phone = phone;
            info.UserName = username;

            if (dal.Update(info) > 0 )
            {
                state = "1";
            }
            else
            {
                state = "-1";
            }
        }

        Response.Write("{\"userid\":\"" + userid + "\",\"state\":\"" + state + "\"}");

        Response.End();
    }
}
