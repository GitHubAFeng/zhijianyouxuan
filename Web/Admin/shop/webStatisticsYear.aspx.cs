using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;

public partial class Admin_Sale_webStatisticsYear : System.Web.UI.Page
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

    /// <summary>
    /// 1表示标签可点
    /// </summary>
    private string flag
    {
        get
        {
            object o = ViewState["flag"];
            return (o == null) ? "1" : Convert.ToString(o);
        }
        set
        {
            ViewState["flag"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlWhere = " 1= 1 and orderstatus = 3 ";
            if (Request["tid"] != null)
            {
                int tid = HjNetHelper.GetQueryInt("tid", 0);
                SqlWhere += " and togoid =  " + tid;
                PointsInfo tmdel = daltogo.GetModel(tid);
                h4title.InnerHtml = tmdel.Name + "订单统计";
            }
            DateTime now = DateTime.Now;
            WebUtility.SelectValue(ddlyear, now.Year.ToString());
            if (Request["date"] != null)
            {
                now = Convert.ToDateTime(Request["date"]);
                WebUtility.SelectValue(ddlyear, now.Year.ToString());
              //  WebUtility.SelectValue(ddlmounth, now.Month.ToString());
                getdays();
            }
        }
    }

    protected void search_click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(5);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        IList<OrderCountInfo> list = new List<OrderCountInfo>();
        IList<OrderCountInfo> rslist = new List<OrderCountInfo>();
        decimal allprice = 0;
        int allcount = 0;
        if (ddlmounth.SelectedValue != "0")
        {
            lbnotice.Style["display"] = "none";
            lbmytype.InnerText = "日期";
        }
        else
        {
            lbnotice.Style["display"] = "";
            lbmytype.InnerText = "月份";
            list = dal.GetOrderCount(4, ddlyear.SelectedValue, "", "", SqlWhere);

            WebUtility.FixdelCookie("ddlyear");
            //WebUtility.FixdelCookie("thedate");
            WebUtility.FixdelCookie("webStatisticsExcleOrder");
            WebUtility.FixsetCookie("ddlyear", ddlyear.SelectedValue, 1);
           // WebUtility.FixsetCookie("thedate", thedate.Month.ToString(), 1);
            WebUtility.FixsetCookie("webStatisticsExcleOrder", SqlWhere, 1);

            for (int i = 1; i <= 12; i++)
            {
                int x = 0;
                foreach (OrderCountInfo item in list)
                {
                    if (item.CountKey == i.ToString())
                    {
                        x = 1;
                        item.CountKey = ddlyear.SelectedValue + "-" + item.CountKey;
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
                    addmodel.CountKey = ddlyear.SelectedValue + "-" + i.ToString();
                    addmodel.CountDrinkPrice = 0;
                    addmodel.CountSendFee = 0;
                    rslist.Add(addmodel);
                }
            }
        }
        if (allprice > 0)
        {
            foreach (OrderCountInfo item in rslist)
            {
                item.rat = Convert.ToInt32(item.CountDecimalValue / allprice * 100 * Convert.ToDecimal(0.8));
            }
        }
        WebUtility.BindRepeater(rptsum, rslist);
        lborder.InnerText = allcount + "";
        lbcount.InnerText = allprice + "";
        flag = "1";
    }

    protected void getdays(object sender, RepeaterCommandEventArgs e)
    {
        string str = e.CommandArgument.ToString();
        if (str.IndexOf('-') < 0 || flag == "0")//天数不能再点击
        {
            int tid = 0;
            string date = ddlyear.SelectedValue + "-" + str;
            if (Request["tid"] != null)
            {
                tid = HjNetHelper.GetQueryInt("tid", 0);
            }
            AlertScript.RegScript(Page, UpdatePanel1, "window.open('OrderList.aspx?dates=" + date + "&tid=" + tid + "');");
            return;
        }
        DateTime thedate = Convert.ToDateTime(str + "-1");
        //月份内的天数
        IList<OrderCountInfo> list = new List<OrderCountInfo>();
        IList<OrderCountInfo> rslist = new List<OrderCountInfo>();
        decimal allprice = 0;
        int allcount = 0;
        lbnotice.Style["display"] = "none";
        lbmytype.InnerText = "日期";
        list = dal.GetOrderCount(2, ddlyear.SelectedValue, thedate.Month.ToString(), "", SqlWhere);
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
        flag = "0";
    }

    protected void getdays()
    {
        DateTime thedate = Convert.ToDateTime(Request["date"]);
        //月份内的天数
        IList<OrderCountInfo> list = new List<OrderCountInfo>();
        IList<OrderCountInfo> rslist = new List<OrderCountInfo>();
        decimal allprice = 0;
        int allcount = 0;
        lbnotice.Style["display"] = "none";
        lbmytype.InnerText = "日期";
        list = dal.GetOrderCount(2, ddlyear.SelectedValue, thedate.Month.ToString(), "", SqlWhere);
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
        flag = "0";
    }

    protected void btExport_Click(object sender, EventArgs e)
    {
        Response.Redirect("SearchshopStatisticsExcelOrder.aspx?type=1");
    }
}
