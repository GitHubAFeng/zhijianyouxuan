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

public partial class ShowNews : System.Web.UI.Page
{
    News dal = new News();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetNewsInfo();
            GetList();
        }
    }

    protected void GetNewsInfo()
    {
        int id = HjNetHelper.GetQueryInt("id" ,0);
        NewsInfo modle = dal.GetModel(id);
        if (modle != null)
        {
            this.newstitle.InnerHtml = modle.Title;
            this.newsContent.InnerHtml = modle.EContent;
            Page.Title = modle.Title + " - "+ SectionProxyData.GetSetValue(3);
            divtime.InnerHtml = modle.Posttime.ToLongDateString();
        }
    }

    protected void GetList()
    {
        rptNewsList.DataSource = dal.GetList(12, 1, "", "dataid", 1);
        rptNewsList.DataBind();
    }
}
