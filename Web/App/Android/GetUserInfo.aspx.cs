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

public partial class AndroidAPI_GetUserInfov : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        StringBuilder json = new StringBuilder("");

        ECustomer dal = new ECustomer();

        string userid = Request["userid"].ToString();
        ECustomerInfo info = new ECustomerInfo();

        info = dal.GetModel(Convert.ToInt32(userid));


        if (info != null)
        {
            json.Append("{\"userid\":\"" + info.DataID.ToString() + "\",\"username\":\"" + info.Name + "\",\"truename\":\"" + info.TrueName + "\",\"email\":\"" + info.EMAIL + "\",\"qq\":\"" + info.QQ + "\",\"phone\":\"" + info.Tell + "\",\"HaveMoney\":\"" + info.Usermoney + "\",\"Point\":\"" + info.Point + "\",\"historypoint\":\"" + 0 + "\",\"publicgood\":\"" + 0 + "\",\"gradename\":\"" + "" + "\",\"pic\":\"" + info.Picture.Replace("~", WebUtility.GetConfigsite()) + "\"}");
        }
        else
        {
            json.Append("{\"userid\":\"-1\"}");
        }

        Response.Write(json.ToString());
        Response.End();
    }
}
