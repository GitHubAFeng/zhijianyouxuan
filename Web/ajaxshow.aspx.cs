#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-7-14 11:12:10.
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

public partial class ajaxshow : System.Web.UI.Page
{
    Noticenews dal = new Noticenews();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetNewsInfo();
        }
    }

    protected void GetNewsInfo()
    {
        int id = HjNetHelper.GetQueryInt("id", 0);
        NoticenewsInfo modle = dal.GetModel(id);
        if (modle != null)
        {
            this.newstitle.InnerHtml = modle.Title;
            this.newsContent.InnerHtml = modle.producer;
            divtime.InnerHtml = modle.Adddate.ToLongDateString();
        }
    }
}
