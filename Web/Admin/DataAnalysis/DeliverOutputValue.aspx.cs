
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
using Hangjing.Model;
using Hangjing.Common;
using System.Collections.Generic;

/// <summary>
/// 配送员产值表
/// </summary>
public partial class qy_55tuan_Admin_DeliverOutputValue : System.Web.UI.Page
{

    /// <summary>
    /// 订单条件
    /// </summary>
    private string orderwhere
    {
        get
        {
            object o = ViewState["orderwhere"];
            return (o == null) ? "1=1" : o.ToString();
        }
        set
        {
            ViewState["orderwhere"] = value;
        }
    }

    Custorder dal = new Custorder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            orderwhere = " orderstatus = 3 ";
            WebUtility.SetDDLCity(ddlcity);

        }
    }

    private void BindData()
    {
        DateTime start1 = Convert.ToDateTime(tbStartTime.Text);
        DateTime end1 = Convert.ToDateTime(tbEndTime.Text);

        DateTime start2 = start1;
        DateTime end2 = end1;
        if (tbStartTime2.Text.Length > 0)
        {
            start2 = Convert.ToDateTime(tbStartTime2.Text);
            end2 = Convert.ToDateTime(tbEndTime2.Text);
        }


        //两个时间段的相差天数要相等
        if ((end1 - start1).TotalDays != (end2 - start2).TotalDays)
        {
            AlertScript.RegScript(this, "alert('两段时间天数必须相等','success','true','5');");
            return;
        }

        //第一时间段
        string firsttimespanwhere = orderwhere;
        firsttimespanwhere += "  and  OrderDateTime > '" + WebUtility.InputText(this.tbStartTime.Text.Trim()) + "' ";
        firsttimespanwhere += " and OrderDateTime < '" + WebUtility.InputText(this.tbEndTime.Text.Trim()) + " 23:59:59' ";
        IList<CustorderInfo> firsttimespandata = dal.GetSumGroupByDay(firsttimespanwhere);

        //第二时间段
        string secondtimespanwhere = orderwhere;
        secondtimespanwhere += "  and  OrderDateTime > '" + start2.ToShortDateString() + "' ";
        secondtimespanwhere += " and OrderDateTime < '" + end2.ToShortDateString() + " 23:59:59' ";
        IList<CustorderInfo> secondttimespandata = dal.GetSumGroupByDay(secondtimespanwhere);

        string xjson = "";

        string yjson = "[";
        int xAxisCount = Convert.ToInt32((end1 - start1).TotalDays) + 1;

        decimal perordercount_1 = 0;
        decimal persendfee_1 = 0;
        decimal perallprice_1 = 0;
        decimal perordercount_2 = 0;
        decimal persendfee_2 = 0;
        decimal perallprice_2 = 0;


        for (int i = 0; i < xAxisCount; i++)
        {
            xjson += (i) + ",";
            string firstkey = start1.AddDays(i).ToString("yyyy-MM-dd");
            string secondkey = start2.AddDays(i).ToString("yyyy-MM-dd");

            int ordercount_1 = 0;
            decimal sendfee_1 = 0;
            decimal allprice_1 = 0;
            int ordercount_2 = 0;
            decimal sendfee_2 = 0;
            decimal allprice_2 = 0;

            foreach (var item in firsttimespandata)
            {
                if (firstkey == item.tempcode)
                {
                    ordercount_1 = item.OrderCount;
                    sendfee_1 = item.shopdiscountmoney;
                    allprice_1 = item.OrderTotal;
                    break;
                }
            }
            foreach (var item in secondttimespandata)
            {
                if (secondkey == item.tempcode)
                {
                    ordercount_2 = item.OrderCount;
                    sendfee_2 = item.shopdiscountmoney;
                    allprice_2 = item.OrderTotal;
                    break;
                }
            }

            perordercount_1 += ordercount_1;
            persendfee_1 += sendfee_1;
            perallprice_1 += allprice_1;
            perordercount_2 += ordercount_2;
            persendfee_2 += sendfee_2;
            perallprice_2 += allprice_2;


            yjson += "{'ordercount_1':" + ordercount_1 + ",'sendfee_1':'" + sendfee_1 + "','allprice_1':'" + allprice_1 + "','ordercount_2':" + ordercount_2 + ",'sendfee_2':'" + sendfee_2 + "','allprice_2':'" + allprice_2 + "'},";
        }

        xjson = WebUtility.dellast(xjson);
        yjson = WebUtility.dellast(yjson);

        yjson += "]";

        hfxjson.Value = xjson;
        hfyjson.Value = yjson;

        OrderCountInfo time1sum = new OrderCountInfo();
        time1sum.CountKey = (perordercount_1 * 1.0M / xAxisCount).ToString("0.0");
        time1sum.picstr = (perallprice_1 * 1.0M / xAxisCount).ToString("0.0");

        List<OrderCountInfo> time1sums = new List<OrderCountInfo>();
        time1sums.Add(time1sum);

        WebUtility.BindRepeater(rpttimesum1, time1sums);

        OrderCountInfo time2sum = new OrderCountInfo();
        time2sum.CountKey = (perordercount_2 * 1.0M / xAxisCount).ToString("0.0");
        time2sum.picstr = (perallprice_2 * 1.0M / xAxisCount).ToString("0.0");

        List<OrderCountInfo> time2sums = new List<OrderCountInfo>();
        time2sums.Add(time2sum);

        WebUtility.BindRepeater(rpttimesum2, time2sums);


    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        orderwhere = "  orderstatus = 3 ";
        if (ddlcity.SelectedValue != "0")
        {
            orderwhere += " and cityid = " + ddlcity.SelectedValue + "";
        }
        if (tbKeyword.Text != "")
        {
            orderwhere += " and EXISTS (SELECT dataid FROM dbo.Deliver WHERE DataId = dbo.Custorder.deliverid  ";
            //以下是查配送员
            orderwhere += " and  Name like '%" + WebUtility.InputText(tbKeyword.Text) + "%'";
            orderwhere += " ) ";
        }



        //以下是查商家
        orderwhere += " and EXISTS (SELECT unid FROM dbo.Points WHERE unid = dbo.Custorder.togoid  ";

        if (tbshopname.Text != "")
        {
            orderwhere += " and  Name like '%" + WebUtility.InputText(tbshopname.Text) + "%'";
        }
        orderwhere += " ) ";


        HJlog.toLog(orderwhere);


        BindData();
    }


}
