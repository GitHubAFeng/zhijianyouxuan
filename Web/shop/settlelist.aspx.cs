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

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.WebCommon;
using Hangjing.Model;

/// <summary>
/// 结算报表
/// </summary>
public partial class shop_settlelist : System.Web.UI.Page
{
    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? string.Empty : o.ToString();
        }
        set
        {
            ViewState["SqlWhere"] = value;
        }
    }

    /// <summary>
    /// 商家编号
    /// </summary>
    private int tid
    {
        get
        {
            object o = ViewState["tid"];
            return (o == null) ? 0 : Convert.ToInt32(o);
        }
        set
        {
            ViewState["tid"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);

        if (!this.Page.IsPostBack)
        {

            tbStartTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            tbEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            InitSqlwhere();

            BindData();

        }
    }

    Custorder dal = new Custorder();
    protected void BindData()
    {

        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptOrderList.DataSource = dal.GetListFix(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "OrderDateTime", 1);
        this.rptOrderList.DataBind();
        NoRecord();

        CustorderInfo count = dal.GetCountAndTotal1(SqlWhere);
        lborder.InnerText = count.OrderCount.ToString();
        lbcount.InnerText = count.OrderTotal.ToString();
        lbsettlemoney.InnerText = count.shopdiscountmoney.ToString();
        lbSendFee.InnerText = count.SendFee.ToString();
        lbCardPay.InnerText = count.cardpay.ToString();

        WebUtility.FixsetCookie("SearchExcelTogoSetSqlWhere", SqlWhere, 1);
    }

    private void NoRecord()
    {
        if (rptOrderList.Items.Count == 0)
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "block");
        }
        else
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void rptOrderList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            Response.Redirect("OrderDetail.aspx=" + Convert.ToInt32(e.CommandArgument));
        }
    }


    /// <summary>
    /// 时间变化搜索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void settime_Click(object sender, EventArgs e)
    {
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
        SqlWhere = "  1=1 and OrderStatus = 3  and (paymode = 4 OR paystate = 1) ";
        tid = UserHelp.GetUser_Togo().Unid;
        SqlWhere += " and TogoId = " + tid + "";


        if (this.tbStartTime.Text != "")
        {
            SqlWhere += " and  [OrderDateTime] > '" + WebUtility.InputText(this.tbStartTime.Text) + "' ";
        }
        if (this.tbEndTime.Text != "")
        {
            SqlWhere += " and OrderDateTime < '" + WebUtility.InputText(this.tbEndTime.Text) + " 23:59:59'";
        }
        if (this.ddldeliversiteid.SelectedValue != "-1")
        {
            SqlWhere = SqlWhere + " and deliversiteid=" + ddldeliversiteid.SelectedValue + "";
        }

    }

    /// <summary>
    /// 提现
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cashout_Click(object sender, EventArgs e)
    {
        string url = "cachoutdetail.aspx?a=1";
        Response.Redirect(url);
    }
}
