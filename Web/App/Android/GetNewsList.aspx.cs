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

public partial class APP_Android_GetNewsList_2 : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IList<NewsInfo> list = new List<NewsInfo>();
        News dal = new News();


        //获取最新5条新闻公告
        int pagesize = HjNetHelper.GetPostParam("pagesize", 5);

        list = dal.GetList(pagesize, 1, "", "dataid", 1);

        Response.Clear();

        StringBuilder listjson = new StringBuilder();
        listjson.Append("{\"page\":\"1\",\"total\":\"" + list.Count.ToString() + "\", \"newslist\":[");

        NewsInfo info = new NewsInfo();

        for (int i = 0; i < list.Count; i++)
        {
            info = new NewsInfo();

            info = list[i];

            listjson.Append("{\"id\":\"" + info.DataID.ToString() + "\",\"title\":\"" + info.Title.ToString() + "\"},");
        }

        listjson.Append("]}");

        Response.Write(listjson.ToString().Replace("},]}", "}]}"));
        Response.End();
    }
}
