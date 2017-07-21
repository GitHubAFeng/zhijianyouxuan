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
// 2012-02-18

public partial class APP_Android_GetNewsDetail_2 : APIPageBase
{
    //{dataid:1,title:"title",content:"content"}
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        StringBuilder json = new StringBuilder("");

        News dal = new News();

        NewsInfo modle = dal.GetModel(HjNetHelper.GetPostParam("dataid", 0));

        if (modle != null)
        {
            json.Append("{\"dataid\":\"" + modle.DataID.ToString() + "\",\"title\":\"" + modle.Title + "\",\"content\":\"" + WebUtility.FileterJson(WebUtility.NoHTML(modle.EContent)) + "\"}");
        }
        else
        {
            json.Append("{\"dataid\":\"0\",\"title\":\"该公告已经不存在可能已经被删除\",\"content\":\"该公告已经不存在可能已经被删除\"}");
        }

        Response.Write(json.ToString());
        Response.End();
    }
}
