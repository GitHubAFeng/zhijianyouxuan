using System;
using System.Collections;
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

public partial class Admin_Sale_OrderCount:AdminPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckRights("G");
        string type = HjNetHelper.GetQueryString("type");
        if (!string.IsNullOrEmpty(type))
        {
            if (type == "hour")//小时统计
            {
                Chart2.DataFile = "CountOrderData.aspx?type=hour";
                Chart2.DataBind();
                lbTitle.Text = "今日("+DateTime.Now.ToString("yyyy-MM-dd")+")订单统计";
            }
            else if (type == "day")
            {
                Chart2.DataFile = "CountOrderData.aspx?type=day";
                Chart2.DataBind();
                lbTitle.Text = "当月(" + DateTime.Now.ToString("yyyy-MM") + ")订单统计";
            }
            else if (type == "month")
            {
                Chart2.DataFile = "CountOrderData.aspx?type=month";
                Chart2.DataBind();
                lbTitle.Text = "当年(" + DateTime.Now.ToString("yyyy") + ")订单统计";
            }
            else
            {
                Chart2.DataFile = "CountOrderData.aspx?type=hour";
                Chart2.DataBind();
                lbTitle.Text = "今日(" + DateTime.Now.ToString("yyyy-MM-dd") + ")订单统计";
            }
        }
        else
        {
            Chart2.DataFile = "CountOrderData.aspx?type=hour";
            Chart2.DataBind();
            lbTitle.Text = "今日(" + DateTime.Now.ToString("yyyy-MM-dd") + ")订单统计";
        }
    }
}
