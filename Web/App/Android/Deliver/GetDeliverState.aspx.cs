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
// 2012-05-7

public partial class AndroidAPI_Deliver_GetDeliverState : System.Web.UI.Page
{
    //{"userid":"1","username":"13750821675","truename":"一号配送员1","email":"13750821675","qq":"13750821675","phone":"13750821675"}
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();



        string userid = Request["userid"].ToString();

        Deliver dal = new Deliver();
        DeliverInfo info =  dal.GetModel(Convert.ToInt32(userid));
        

        StringBuilder json = new StringBuilder("");
        if (info != null)
        {
            json.Append("{\"userid\":\"" + info.DataId.ToString() + "\",\"State\":\"" + info.IsWorking + "\"}");
        }
        else
        {
            json.Append("{\"userid\":\"-1\"}");
        }

        Response.Write(json.ToString());
        Response.End();
    }
}
