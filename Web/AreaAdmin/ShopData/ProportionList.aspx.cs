using System;
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
using Hangjing.DBUtility;

/// <summary>
///商家订单统计
/// </summary>
public partial class qy_54tss_AreaAdmin_ProportionList : System.Web.UI.Page
{
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
    /// 订单条件
    /// </summary>
    private string orderWhere
    {
        get
        {
            object o = ViewState["orderWhere"];
            return (o == null) ? "" : Convert.ToString(o);
        }
        set
        {
            ViewState["orderWhere"] = value;
        }
    }

    /// <summary>
    /// 排序字段
    /// </summary>
    protected string ordersort
    {
        get
        {
            object o = ViewState["ordersort"];
            return (o == null) ? "ordercount" : Convert.ToString(o);
        }
        set
        {
            ViewState["ordersort"] = value;
        }
    }

    City bll = new City();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlWhere = "  1=1 ";
            orderWhere = "  1=1 ";
            //orderWhere = WebUtility.GetCustorderSql(orderWhere);
            SqlWhere = WebUtility.GetCitySql(SqlWhere);

            hfyestoday.Value = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            hftoday.Value = DateTime.Now.ToString("yyyy-MM-dd");

            tbStartTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            tbEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");

            InitSqlwhere();
            BindData();
        }
    }

    /// <summary>
    /// 绑定指定的数据
    /// </summary>
    protected void BindData()
    {
        IList<TogoInfo> shops = bll.GetListWithOrderStatistics(1, 1, SqlWhere, orderWhere, ordersort);
        //if (shops.Count > 0)
        //{
        //    this.AspNetPager1.RecordCount = shops[0].DataID;
        //}

        string param = "";
        string starttime = this.tbStartTime.Text;
        string endtime = this.tbEndTime.Text;
        if (starttime != "")
        {
            param += "&starttime=" + starttime;
        }
        if (endtime != "")
        {
            param += "&endtime=" + endtime;
        }

        foreach (var item in shops)
        {
            item.EBuilding = "?tid=" + item.Grade + param;
        }


        decimal allprice = 0, ordertotal = 0,profit=0;
        int allcount = 0;

        //string sql = orderWhere;
        //if (tbshopname.Text != "")
        //{
        //    sql += " and EXISTS(SELECT cid FROM dbo.City WHERE Unid = Custorder.TogoId AND cname LIKE  '%" + Utils.RegEsc(WebUtility.InputText(this.tbshopname.Text.Trim())) + "%'  )";
        //}

        //CustorderInfo ordercount = new Custorder().SiteIncomeStatistics(sql);
        //allprice = ordercount.OrderSums;
        //allcount = ordercount.OrderCount;
        //ordertotal = ordercount.shopdiscountmoney;
        //profit = ordercount.OrderTotal;

        if (allprice > 0)
        {
            foreach (TogoInfo item in shops)
            {
                item.Status = Convert.ToInt32(Convert.ToDecimal(item.allprice) / allprice * 100 * Convert.ToDecimal(0.8));
            }
        }


        this.rptsum.DataSource = shops;
        this.rptsum.DataBind();

    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        InitSqlwhere();
        BindData();
    }

    /// <summary>
    /// 时间变化搜索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void settime_Click(object sender, EventArgs e)
    {
        //orderWhere = WebUtility.GetCustorderSql(orderWhere);
        SqlWhere = WebUtility.GetCitySql(SqlWhere);

        string starttime = this.tbStartTime.Text;
        string endtime = this.tbEndTime.Text;
        Button bt = (Button)sender;
        switch (bt.ID)
        {
            case "btpre"://前一天
                if (starttime != "")
                {
                    tbStartTime.Text = Convert.ToDateTime(starttime).AddDays(-1).ToShortDateString();
                }
                else
                {
                    return;
                }
                if (endtime != "")
                {
                    tbEndTime.Text = Convert.ToDateTime(endtime).AddDays(-1).ToShortDateString();
                }
                else
                {
                    return;
                }
                break;
            case "btyestoday"://昨天
                tbStartTime.Text = DateTime.Now.AddDays(-1).ToShortDateString();
                tbEndTime.Text = DateTime.Now.AddDays(-1).ToShortDateString();

                break;
            case "tbtoday"://今天
                tbStartTime.Text = DateTime.Now.ToShortDateString();
                tbEndTime.Text = DateTime.Now.ToShortDateString();

                break;
            case "btnext"://后一天
                if (starttime != "")
                {
                    tbStartTime.Text = Convert.ToDateTime(starttime).AddDays(1).ToShortDateString();
                }
                else
                {
                    return;
                }
                if (endtime != "")
                {
                    tbEndTime.Text = Convert.ToDateTime(endtime).AddDays(1).ToShortDateString();
                }
                else
                {
                    return;
                }
                break;

            case "btweek"://本周
                {
                    DateTime weekfirst = TimeHelper.WeekFirst();//本周一
                    DateTime StartTime = DateTime.Now;
                    DateTime EndTime = DateTime.Now;
                    if (TimeHelper.dayOfWeek() == 1) //现在已经是下一周了
                    {
                        if (DateTime.Now.Hour >= 4)
                        {
                            StartTime = weekfirst;
                            EndTime = weekfirst.AddDays(7);
                        }
                        else
                        {
                            StartTime = weekfirst.AddDays(-7);
                            EndTime = weekfirst;
                        }

                    }
                    else
                    {
                        StartTime = weekfirst;
                        EndTime = weekfirst.AddDays(7);
                    }

                    tbStartTime.Text = StartTime.ToString("yyyy-MM-dd");
                    tbEndTime.Text = EndTime.ToString("yyyy-MM-dd"); ;
                }

                break;
            case "btpreweek"://上一周
                {
                    if (starttime != "")
                    {
                        tbStartTime.Text = Convert.ToDateTime(starttime).AddDays(-7).ToShortDateString();
                    }
                    else
                    {
                        return;
                    }
                    if (endtime != "")
                    {
                        tbEndTime.Text = Convert.ToDateTime(endtime).AddDays(-7).ToShortDateString();
                    }
                    else
                    {
                        return;
                    }

                }
                break;
            case "btnextweek"://后一周
                if (starttime != "")
                {
                    tbStartTime.Text = Convert.ToDateTime(starttime).AddDays(7).ToShortDateString();
                }
                else
                {
                    return;
                }
                if (endtime != "")
                {
                    tbEndTime.Text = Convert.ToDateTime(endtime).AddDays(7).ToShortDateString();
                }
                else
                {
                    return;
                }
                break;

            case "btmounth"://本月
                {
                    DateTime mounthfirst = TimeHelper.MounthFirst();//本月一号
                    DateTime StartTime = DateTime.Now;
                    DateTime EndTime = DateTime.Now;
                    if (DateTime.Now.Day == 1) //现在已经是下一月了
                    {
                        if (DateTime.Now.Hour >= 4)
                        {
                            StartTime = mounthfirst;
                            EndTime = mounthfirst.AddMonths(1);
                        }
                        else
                        {
                            StartTime = mounthfirst.AddMonths(-1);
                            EndTime = mounthfirst;
                        }

                    }
                    else
                    {
                        StartTime = mounthfirst;
                        EndTime = mounthfirst.AddMonths(1);
                    }

                    tbStartTime.Text = StartTime.ToString("yyyy-MM-dd");
                    tbEndTime.Text = EndTime.ToString("yyyy-MM-dd"); ;
                }

                break;
            case "btpremounth"://前一月
                {
                    if (starttime != "")
                    {
                        tbStartTime.Text = Convert.ToDateTime(starttime).AddMonths(-1).ToShortDateString();
                    }
                    else
                    {
                        return;
                    }
                    if (endtime != "")
                    {
                        tbEndTime.Text = Convert.ToDateTime(endtime).AddMonths(-1).ToShortDateString();
                    }
                    else
                    {
                        return;
                    }

                }
                break;
            case "btnextmounth"://后一月
                if (starttime != "")
                {
                    tbStartTime.Text = Convert.ToDateTime(starttime).AddMonths(1).ToShortDateString();
                }
                else
                {
                    return;
                }
                if (endtime != "")
                {
                    tbEndTime.Text = Convert.ToDateTime(endtime).AddMonths(1).ToShortDateString();
                }
                else
                {
                    return;
                }
                break;
            default:
                break;
        }
        InitSqlwhere();

        BindData();
    }

    /// <summary>
    /// 生成查询条件
    /// </summary>
    protected void InitSqlwhere()
    {
        SqlWhere = "  1=1 ";

        orderWhere = "  1=1 and OrderStatus = 3  and (paymode = 4 or paystate = 1) ";

        //orderWhere = WebUtility.GetCustorderSql(orderWhere);
        SqlWhere = WebUtility.GetCitySql(SqlWhere);

        if (this.tbStartTime.Text != "")
        {
            orderWhere += " and  [OrderDateTime] > '" + WebUtility.InputText(this.tbStartTime.Text) + "' ";
        }
        if (this.tbEndTime.Text != "")
        {
            orderWhere += " and OrderDateTime < '" + WebUtility.InputText(this.tbEndTime.Text) + " 23:59:59'";
        }

        //if (tbshopname.Text != "")
        //{
        //    SqlWhere += " and cname like '%" + Utils.RegEsc(WebUtility.InputText(this.tbshopname.Text.Trim())) + "%' ";
        //}
    }


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void sort_click(object sender, EventArgs e)
    {
        ordersort = "ordercount";
        LinkButton lb = (LinkButton)sender;
        ordersort = lb.CommandArgument.ToString();

        BindData();
    }

}
