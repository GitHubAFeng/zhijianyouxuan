#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-8-1 16:43:43.
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

public partial class qy_54tss_Admin_ajax_uploadpic_indexad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request["id"].ToString();
        string pic = Request["pic"].ToString();
        Response.Clear();
        string sql = "update indexlink set pic = '" + pic + "' where id =" + id;
        WebUtility.excutesql(sql);
        SectionProxyData.Clearindexlinklist();
        Response.End();
    }
}
