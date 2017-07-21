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
using System.Collections.Generic;
using Hangjing.Model;
using Hangjing.SQLServerDAL;

/// <summary>
/// 商品top10
/// </summary>
public partial class Admin_Sale_foodTOP10 : AdminPageBase
{
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

    Foodlist bll = new Foodlist();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
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
        IList<FoodlistInfo> shops = bll.FoodSaleTOP10(orderWhere);
        string xjson = "";
        string yjson = "[";
        foreach (var item in shops)
        {
            xjson += item.FoodName+",";
            yjson += "{'ordercount':" + item.FCounts + ",'allprice':" + item.FoodPrice + ",'shopname':'" + item.Remark + "'},";
        }
        xjson = WebUtility.dellast(xjson);
        yjson = WebUtility.dellast(yjson);

        yjson+= "]";

        hfxjson.Value = xjson;
        hfyjson.Value = yjson;

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
        DateTime dt = DateTime.Now; //当前时间
        string starttime = this.tbStartTime.Text;
        string endtime = this.tbEndTime.Text;
        Button bt = (Button)sender;
        switch (bt.ID)
        {
            case "btpre"://前一天
                if (starttime != "")
                {
                    tbStartTime.Text = Convert.ToDateTime(starttime).AddDays(-1).ToShortDateString() + "";
                }
                else
                {
                    return;
                }
                if (endtime != "")
                {
                    tbEndTime.Text = Convert.ToDateTime(endtime).AddDays(-1).ToShortDateString() + "";
                }
                else
                {
                    return;
                }
                break;
            case "btyestoday"://昨天
                tbStartTime.Text = DateTime.Now.AddDays(-1).ToShortDateString() + "";
                tbEndTime.Text = DateTime.Now.AddDays(-1).ToShortDateString() + "";

                break;
            case "tbtoday"://今天
                tbStartTime.Text = DateTime.Now.ToShortDateString() + "";
                tbEndTime.Text = DateTime.Now.ToShortDateString() + "";

                break;
            case "btnext"://后一天
                if (starttime != "")
                {
                    tbStartTime.Text = Convert.ToDateTime(starttime).AddDays(1).ToShortDateString() + "";
                }
                else
                {
                    return;
                }
                if (endtime != "")
                {
                    tbEndTime.Text = Convert.ToDateTime(endtime).AddDays(1).ToShortDateString() + "";
                }
                else
                {
                    return;
                }
                break;
            case "tbweek"://本周
             
                DateTime startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")));
                tbStartTime.Text = startWeek.ToDate();

                tbEndTime.Text = DateTime.Now.ToShortDateString() + "";
                break;
            case "tbmonth"://本月
                tbStartTime.Text = DateTime.Now.ToString("yyyy-MM-01") + "";
                tbEndTime.Text = DateTime.Now.ToShortDateString() + "";
                break;
            case "tbyear"://本年
                tbStartTime.Text = DateTime.Now.ToString("yyyy-01-01") + "";
                tbEndTime.Text = DateTime.Now.ToShortDateString() + "";

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
        orderWhere = "  1=1 and OrderStatus = 3";
        if (this.tbStartTime.Text != "")
        {
            orderWhere += " and  [OrderDateTime] > '" + WebUtility.InputText(this.tbStartTime.Text) + "' ";
        }
        if (this.tbEndTime.Text != "")
        {
            orderWhere += " and OrderDateTime < '" + WebUtility.InputText(this.tbEndTime.Text) + " 23:59:59'";
        }
    }


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

}
