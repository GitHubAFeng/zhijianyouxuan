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

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using System.Text;

//zjf@ihangjing.com 2012-5-10
public partial class qy_54tss_AreaAdmin_ajax_GetDeliverWithOrderNum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        StringBuilder deliver = new StringBuilder("<ul>");

        IList<DeliverInfo> list = new List<DeliverInfo>();

        Deliver dal = new Deliver();

        list = dal.GetListWithOrderNum(1000, 1, "", "TOrderNum", 1);

        foreach (DeliverInfo info in list)
        {
            //WebUtility.GetDeliverStatusByOrderNum(info.OrderNum)
            deliver.Append("<h6><a href=\"#\" class=\"\">"+info.Name+"-"+info.DataId+"（"+info.OrderNum.ToString()+"）</a></a></h6>");
        }

        deliver.Append("</ul>");

        Response.Write(deliver.ToString());

        Response.End();
    }
}
