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
using OpenFlashChart;

// 获取统计数据返回统计图形
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-07-17

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

public partial class Admin_Sale_CountOrderData :AdminPageBase
{
   Custorder bll = new Custorder();

    protected void Page_Load(object sender, EventArgs e)
    {
        string type = HjNetHelper.GetQueryString("type");
        if (!string.IsNullOrEmpty(type))
        {
            if (type == "hour")//小时统计
            {
                GetHourCount();
            }
            else if (type == "day")
            {
                GetDayCount();
            }
            else if(type=="month")
            {
                GetMonthCount();
            }
        }
    }

    private void GetHourCount()
    {
        IList<OrderCountInfo> list = new List<OrderCountInfo>();
        OpenFlashChart.OpenFlashChart chart = new OpenFlashChart.OpenFlashChart();
        DateTime Now = DateTime.Now;
        List<double> data1 = new List<double>();

        list = bll.GetOrderCount(1, Now.Year.ToString(), Now.Month.ToString("00"), Now.Day.ToString("00"));

        //计算此次订单量的最大值
        int MaxCount = 0;
        for (int j = 0; j < list.Count; j++)
        {
            if (list[j].CountIntValue > MaxCount)
            {
                MaxCount = list[j].CountIntValue;
            }
        }

        MaxCount = GetMaxY(MaxCount);
        
        for (int i = 0; i <24; i++)//有几个数据 X轴自动
        {
            double value = (double)0.0;
            //bar.Data.Add(rd.Next(0, 100));
            for (int j = 0; j < list.Count; j++)
            {
                if (list[j].CountKey == i.ToString())
                {
                    value = (float)(list[j].CountIntValue);
                    break;
                }
            }

            data1.Add(value);
        }

        
        OpenFlashChart.LineHollow line1 = new LineHollow();

        line1.Values = data1;
        line1.HaloSize = 0;
        line1.Width = 2;
        line1.DotSize = 5;
        line1.DotStyleType.Tip = "#x_label#<br>#val#";
        line1.DotStyleType.Colour = "#467533";
        line1.Tooltip = "提示：#val#";

        chart.AddElement(line1);
        chart.Y_Legend = new Legend("Order");
        chart.Title = new Title("今日订单量统计");
        chart.Y_Axis.SetRange(0, MaxCount, MaxCount/10);
        chart.X_Axis.Labels.Color = "#e43456";
        chart.X_Axis.Steps = 1;
        chart.Tooltip = new ToolTip("全局提示：#val#");
        chart.Tooltip.Shadow = true;
        chart.Tooltip.Colour = "#e43456";
        chart.Tooltip.MouseStyle = ToolTipStyle.CLOSEST;

        Response.Clear();
        Response.CacheControl = "no-cache";
        Response.Write(chart.ToPrettyString());
        Response.End();
    }

    private void GetDayCount()
    {

        IList<OrderCountInfo> list = new List<OrderCountInfo>();
        List<double> data1 = new List<double>();
        DateTime Now = DateTime.Now;
        OpenFlashChart.OpenFlashChart chart = new OpenFlashChart.OpenFlashChart();

        list = bll.GetOrderCount(2, Now.Year.ToString(), Now.Month.ToString("00"), Now.Day.ToString("00"));
        //计算此次订单量的最大值
        int MaxCount = 0;
        for (int j = 0; j < list.Count; j++)
        {
            if (list[j].CountIntValue > MaxCount)
            {
                MaxCount = list[j].CountIntValue;
            }
        }

        MaxCount = GetMaxY(MaxCount);

        for (int i = 1; i < 32; i++)
        {
            double value = (double)0.0;
            //bar.Data.Add(rd.Next(0, 100));
            for (int j = 0; j < list.Count; j++)
            {
                if (list[j].CountKey == i.ToString())
                {
                    value = (float)(list[j].CountIntValue);
                    break;
                }
            }

            data1.Add(value);
        }


        OpenFlashChart.LineHollow line1 = new LineHollow();

        line1.Values = data1;
        line1.HaloSize = 0;
        line1.Width = 2;
        line1.DotSize = 5;
        line1.DotStyleType.Tip = "#x_label#<br>#val#";
        line1.DotStyleType.Colour = "#467533";
        line1.Tooltip = "提示：#val#";

        chart.AddElement(line1);
        chart.Y_Legend = new Legend("Order");
        chart.Title = new Title("日订单量统计");
        chart.Y_Axis.SetRange(0, MaxCount, MaxCount / 10);
        chart.X_Axis.Labels.Color = "#e43456";
        chart.X_Axis.Steps = 1;
        chart.Tooltip = new ToolTip("全局提示：#val#");
        chart.Tooltip.Shadow = true;
        chart.Tooltip.Colour = "#e43456";
        chart.Tooltip.MouseStyle = ToolTipStyle.CLOSEST;


        Response.Clear();
        Response.CacheControl = "no-cache";
        Response.Write(chart.ToPrettyString());
        Response.End();

    }

    protected void GetMonthCount()
    {
        IList<OrderCountInfo> list = new List<OrderCountInfo>();
        List<double> data1 = new List<double>();
        DateTime Now = DateTime.Now;
        OpenFlashChart.OpenFlashChart chart = new OpenFlashChart.OpenFlashChart();

        list = bll.GetOrderCount(4, Now.Year.ToString(), Now.Month.ToString(), Now.Day.ToString());

        //计算此次订单量的最大值
        int MaxCount = 0;
        for (int j = 0; j < list.Count; j++)
        {
            if (list[j].CountIntValue > MaxCount)
            {
                MaxCount = list[j].CountIntValue;
            }
        }

        MaxCount = GetMaxY(MaxCount);

        for (int i = 1; i < 13; i++)
        {
            double value = (double)0.0;
            //bar.Data.Add(rd.Next(0, 100));
            for (int j = 0; j < list.Count; j++)
            {
                if (list[j].CountKey == i.ToString())
                {
                    value = (float)(list[j].CountIntValue);
                    break;
                }
            }

            data1.Add(value);

        }

        OpenFlashChart.LineHollow line1 = new LineHollow();

        line1.Values = data1;
        line1.HaloSize = 0;
        line1.Width = 2;
        line1.DotSize = 5;
        line1.DotStyleType.Tip = "#x_label#<br>#val#";
        line1.DotStyleType.Colour = "#467533";
        line1.Tooltip = "提示：#val#";

        chart.AddElement(line1);
        chart.Y_Legend = new Legend("Order");
        chart.Title = new Title("月订单量统计");
        chart.Y_Axis.SetRange(0, MaxCount, MaxCount / 10);
        chart.X_Axis.Labels.Color = "#e43456";
        chart.X_Axis.Steps = 1;
        chart.Tooltip = new ToolTip("全局提示：#val#");
        chart.Tooltip.Shadow = true;
        chart.Tooltip.Colour = "#e43456";
        chart.Tooltip.MouseStyle = ToolTipStyle.CLOSEST;


        Response.Clear();
        Response.CacheControl = "no-cache";
        Response.Write(chart.ToPrettyString());
        Response.End();

    }

    private int GetMaxY(int MaxCount)
    {
        int MaxY = 0;
        //根据最大值计算此次图形显示的Y轴的最大值
        if (MaxCount < 20)
        {
            MaxY = 20;
        }
        else
        {
            int Step = (int)System.Math.Pow((double)10, (double)(MaxCount.ToString().Length - 1));
            MaxY = ((MaxCount + Step) / Step) * Step;
        }

        return MaxY;
    }
}
