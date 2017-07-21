/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : Ajax_getsubsection
 * Function : 
 * Created by jijunjian at 2010-11-25 11:10:40.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class Ajax_getsubsection_xxxc : System.Web.UI.Page
{
    ESection dal = new ESection();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        string sid = "0";
        sid = WebUtility.InputText(Request["id"]);
        IList<SectionInfo> list = dal.GetList(100, 1, "cityid =" + sid, "pri", 1);
        string jsonstr = WebUtility.ObjectToJson("sort", list);
        Response.Write(jsonstr);
        Response.End();
    }
}
