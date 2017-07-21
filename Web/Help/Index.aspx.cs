using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class Help_Index : System.Web.UI.Page
{
    aboutClass dalclass = new aboutClass();
    Hangjing.SQLServerDAL.aboutus dal = new Hangjing.SQLServerDAL.aboutus();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["id"] != null)
        {
            int id = HjNetHelper.GetQueryInt("id", 0);
            aboutusInfo model = dal.GetModel(id);
            divinfo.InnerHtml = model.HelpContent;
            divtitmle.InnerHtml = model.Title;
        }
        else
        {
            if (Request["sid"] != null)
            {
                IList<aboutusInfo> list = dal.GetList(1, 1, "sortid =" + HjNetHelper.GetQueryString("sid"), "ordernum", 1);
                if (list.Count > 0)
                {
                    divinfo.InnerHtml = list[0].HelpContent;
                    divtitmle.InnerHtml = list[0].Title;
                }
            }
            else
            {
                IList<aboutusInfo> list = dal.GetList(1, 1, "", "dataid", 1);
                if (list.Count > 0)
                {
                    divinfo.InnerHtml = list[0].HelpContent;
                }
            }
        }

        rptClass.DataSource = dalclass.GetList(20, 1, "", "FullId", 1);
        rptClass.DataBind();
    }

    protected IList<aboutusInfo> getsubartic(object pid)
    {
        IList<aboutusInfo> list = dal.GetList(10, 1, "sortid = " + pid.ToString(), "ordernum", 1);
        if (list.Count >= 1)
        {
            return list;
        }
        else
        {
            return null;
        }
    }
}
