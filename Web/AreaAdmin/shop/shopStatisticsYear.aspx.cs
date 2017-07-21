using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;

public partial class AreaAdmin_Sale_shopStatisticsYear : System.Web.UI.Page
{
    Custorder dal = new Custorder();
    Points daltogo = new Points();

    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? "" : Convert.ToString(o);
        }
        set
        {
            ViewState["SqlWhere"] = value;
        }
    }

    private string orderWhere
    {
        get
        {
            object o = ViewState["orderWhere"];
            return (o == null) ? "1=1" : Convert.ToString(o);
        }
        set
        {
            ViewState["orderWhere"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlWhere = " 1= 1 and isdelete = 0";

            DateTime now = DateTime.Now;
            WebUtility.SelectValue(ddlyear, now.Year.ToString());
            WebUtility.SelectValue(ddlmounth, now.Month.ToString());
        }
    }

    protected void search_click(object sender, EventArgs e)
    {
        orderWhere = "OrderStatus = 3 and TogoId = Points.Unid ";
        if (ddlmounth.SelectedValue != "0")//统计月
        {
            string otart = ddlyear.SelectedValue + "-" + ddlmounth.SelectedValue + "-1";
            int days = DateTime.DaysInMonth(Convert.ToInt32(ddlyear.SelectedValue), Convert.ToInt32(ddlmounth.SelectedValue));
            string oend = ddlyear.SelectedValue + "-" + ddlmounth.SelectedValue + "-" + days + " 23:59:59";
            orderWhere += " and OrderDateTime > '" + otart + "' and OrderDateTime < '" + oend + "'";
        }
        else
        {
            string otart = ddlyear.SelectedValue + "-1-1";
            string oend = ddlyear.SelectedValue + "-12-31 23:59:59";
            orderWhere += " and OrderDateTime > '" + otart + "' and OrderDateTime < '" + oend + "'";
        }
        binddata();
    }

    protected void getdays(object sender, RepeaterCommandEventArgs e)
    {
        string str = e.CommandArgument.ToString();
        if (str.IndexOf('-') < 0)//天数不能再点击
        {
            return;
        }
        DateTime thedate = Convert.ToDateTime(str + "-1");
        //月份内的天数
        IList<OrderCountInfo> list = new List<OrderCountInfo>();
        IList<OrderCountInfo> rslist = new List<OrderCountInfo>();
        decimal allprice = 0;
        int allcount = 0;
        lbnotice.Style["display"] = "none";
        list = dal.GetOrderCount(2, ddlyear.SelectedValue, thedate.Month.ToString(), "");
        int days = DateTime.DaysInMonth(thedate.Year, thedate.Month);

        for (int i = 1; i <= days; i++)
        {
            int x = 0;
            foreach (OrderCountInfo item in list)
            {
                if (item.CountKey == i.ToString())
                {
                    x = 1;
                    item.CountKey = thedate.Month + "-" + item.CountKey;
                    rslist.Add(item);
                    allprice += item.CountDecimalValue;
                    allcount += item.CountIntValue;
                }
            }
            if (x == 0)//此月没有订单设置为0
            {
                OrderCountInfo addmodel = new OrderCountInfo();
                addmodel.CountIntValue = 0;
                addmodel.CountDecimalValue = 0;
                addmodel.CountKey = thedate.Month + "-" + i.ToString();
                rslist.Add(addmodel);
            }
        }

        if (allprice > 0)
        {
            foreach (OrderCountInfo item in rslist)
            {
                item.rat = Convert.ToInt32(item.CountDecimalValue / allprice * 100 * Convert.ToDecimal(0.8));
            }
        }
        lborder.InnerText = allcount + "";
        lbcount.InnerText = allprice + "";
        WebUtility.BindRepeater(rptsum, rslist);
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        binddata();
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    protected void binddata()
    {
        decimal allprice = 0;
        int allcount = 0;
        AspNetPager1.RecordCount = daltogo.GetCount(SqlWhere);
        IList<PointsInfo> tlist = daltogo.GetSummary(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "allprice", 1, orderWhere);
        foreach (PointsInfo item in tlist)
        {
            allprice += Convert.ToDecimal(item.allprice);
            allcount += Convert.ToInt32(item.allcount);
        }
        if (allprice > 0)
        {
            foreach (PointsInfo item in tlist)
            {
                item.Status = Convert.ToInt32(Convert.ToDecimal(item.allprice) / allprice * 100 * Convert.ToDecimal(0.8));
            }
        }
        lborder.InnerText = allcount + "";
        lbcount.InnerText = allprice + "";
        WebUtility.BindRepeater(rptsum, tlist);
    }

    protected string geturl(object dataid)
    {
        string url = "webStatisticsYear.aspx?tid="+dataid;
        string date = ddlyear.SelectedValue + "-" + ddlmounth.SelectedValue + "-1";
        url += "&date="+date;
        return url;
    }
}
