/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : ShowNews.aspx.cs
 * Function : 公告详细信息
 * Created by jijunjian at 2010-8-21 1:10:04.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class Newslist : System.Web.UI.Page
{
    News dal = new News();
    private string SqlWhere
    {
        get { object o = ViewState["SqlWhere"]; return (o == null ? "" : o.ToString()); }
        set { ViewState["SqlWhere"] = value; }
    }

    /// <summary>
    ///排序字段
    /// </summary>
    private string sortname
    {
        get { object o = ViewState["sortname"]; return (o == null ? "sortnum" : o.ToString()); }
        set { ViewState["sortname"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            SqlWhere = " 1=1";
            string sql = "1=1";
            Page.Title = "网站公告 - " + WebUtility.GetWebName();

            rptNewsList.DataSource = dal.GetList(5, 1, sql, sortname, 1);
            rptNewsList.DataBind();

            getlist();

        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        getlist();
    }

    protected void getlist()
    {
        AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        rptnews.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, sortname, 1);
        rptnews.DataBind();
        if (AspNetPager1.RecordCount == 0)
        {
            divnojoin.Style["display"] = "";
        }
        else
        {
            divnojoin.Style["display"] = "none";
        }
    }
}
