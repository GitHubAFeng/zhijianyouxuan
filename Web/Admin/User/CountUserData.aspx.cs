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

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class Admin_User_CountUserData :AdminPageBase
{
    ECustomer bll = new ECustomer();

    private double _definition = 0.05;

    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime start;
        DateTime end;
        string type = "";
        if (Request.QueryString["data"] != null)
        {
            string data = Request.QueryString["data"].ToString();
            int startindex = data.IndexOf("start");

            type = data.Substring(4, startindex - 4);
            data = data.Substring(startindex);
            int endindex = data.IndexOf("end");
            string str1 = data.Substring(5, endindex - 5);
            data = data.Substring(endindex);
            string str2 = data.Substring(3);
            start = Convert.ToDateTime(str1);
            end = Convert.ToDateTime(str2);
            switch (type)
            {
                case "day": CountDay(start, end); break;
                case "month": CountMonth(start, end); break;
                case "year": CountYear(start, end); break;
            }
        }
        else
        {
            if (Request.QueryString["type"] != null)
            {
                string type2 = Request.QueryString["type"].ToString();
                DateTime now = DateTime.Now;

                switch (type2)
                {
                    case "hour": CountHour(); break;
                    case "day": TimeSpan span = new TimeSpan(19, 0, 0, 0); start = now.Subtract(span); CountDay(start, now); break;
                    case "month": start = new DateTime(now.Year, 1, 1); end = new DateTime(now.Year, 12, 1); CountMonth(start, end); break;
                    case "year": start = new DateTime(now.Year - 10, 1, 1); end = new DateTime(now.Year, 1, 1); CountYear(start, end); break;
                }
            }
        }
    }

    private void CountHour()
    {
        OpenFlashChart.OpenFlashChart chart = new OpenFlashChart.OpenFlashChart();
        chart.Title = new Title("本日用户统计数量表");
        Bar bar = new Bar();
        bar.Colour = "#345";
        //bar.Fillalpha = 0.5;
        //bar.Fontsize = 10;
        bar.Text = "用户数量";
        List<double> values = new List<double>();
        string condition = " RegTime BETWEEN '{0}' AND '{1}'";
        List<string> hours = new List<string>();
        DateTime now = DateTime.Now;
        for (int i = 0; i < 24; i++)
        {
            object[] args = new object[2];
            args[0] = now.ToShortDateString() + " " + i.ToString() + ":0:0";
            args[1] = now.ToShortDateString() + " " + i.ToString() + ":59:59";
            values.Add((double)bll.GetCount(string.Format(condition, args)));
            hours.Add((i + 1).ToString());
        }
        bar.Values = values;
        chart.AddElement(bar);
        YAxis y = new YAxis();
        chart.X_Axis.Labels.SetLabels(hours);// = hours; //fix ?
        chart.X_Axis.Steps = 1;
        double maxY = GetMax(values);
        y.SetRange(0, maxY);
        int step = Convert.ToInt32(maxY * _definition);
        if (step > 0)
        {
            y.Steps = step;
        }
        chart.Y_Axis = y;
        Response.Clear();
        Response.Write(chart.ToString());
        Response.End();
    }

    private void CountYear(DateTime start, DateTime end)
    {
        OpenFlashChart.OpenFlashChart chart = new OpenFlashChart.OpenFlashChart();
        chart.Title = new Title("年统计用户数量表(" + start.Year.ToString() + " 至 " + end.Year.ToString() + ")");
        Bar bar = new Bar();

        bar.Colour = "#345";
        //bar.Fillalpha = 0.5;
        bar.Text = "用户数量";
        //bar.Fontsize = 10;
        List<double> values = new List<double>();
        string condition = " RegTime BETWEEN '{0}' AND '{1}'";
        List<string> yeas = new List<string>();
        for (; start.Year <= end.Year; start = start.AddYears(1))
        {
            object[] args = new object[2];
            args[0] = start.Year.ToString() + "-1-1 0:0:0";
            args[1] = start.Year.ToString() + "-12-" + DateTime.DaysInMonth(start.Year, 12).ToString() + " 23:59:59";
            values.Add((double)bll.GetCount(string.Format(condition, args)));
            yeas.Add(start.Year.ToString() + "年");
        }
        bar.Values = values;

        chart.AddElement(bar);

        YAxis y = new YAxis();

        chart.X_Axis.Labels.SetLabels(yeas);// = yeas; //.Values fix
        chart.X_Axis.Steps = 1;
        double maxY = GetMax(values);
        y.SetRange(0, maxY);
        int step = Convert.ToInt32(maxY * _definition);
        if (step > 0)
        {
            y.Steps = step;
        }
        chart.Y_Axis = y;
        Response.Clear();
        Response.Write(chart.ToString());
        Response.End();
    }

    private void CountMonth(DateTime start, DateTime end)
    {
        OpenFlashChart.OpenFlashChart chart = new OpenFlashChart.OpenFlashChart();
        chart.Title = new Title("月统计用户数量表(" + start.Year.ToString() + "年" + start.Month.ToString() + "月 至 " + start.Year.ToString() + "年" + end.Month.ToString() + "月)");
        Bar bar = new Bar();
        bar.Colour = "#345";
        //bar.Fillalpha = 0.5;
        //bar.Fontsize = 10;
        bar.Text = "用户数量";
        List<double> values = new List<double>();
        string condition = " RegTime BETWEEN '{0}' AND '{1}'";
        List<string> months = new List<string>();
        int startIndex = start.Month;
        for (; startIndex <= end.Month; startIndex++)
        {
            object[] args = new object[2];
            args[0] = start.Year.ToString() + "-" + startIndex.ToString() + "-1 0:0:0";
            args[1] = start.Year.ToString() + "-" + startIndex.ToString() + "-" + DateTime.DaysInMonth(start.Year, startIndex).ToString() + " 23:59:59";
            values.Add((double)bll.GetCount(string.Format(condition, args)));
            months.Add(startIndex.ToString() + "月");
        }
        bar.Values = values;

        chart.AddElement(bar);
        YAxis y = new YAxis();
        chart.X_Axis.Labels.SetLabels(months);
        chart.X_Axis.Steps = 1;
        double maxY = GetMax(values);
        y.SetRange(0, maxY);
        int step = Convert.ToInt32(maxY * _definition);
        if (step > 0)
        {
            y.Steps = step;
        }
        chart.Y_Axis = y;
        Response.Clear();
        Response.Write(chart.ToString());
        Response.End();
    }

    private void CountDay(DateTime start, DateTime end)
    {
        OpenFlashChart.OpenFlashChart chart = new OpenFlashChart.OpenFlashChart();
        chart.Title = new Title("日统计用户数量表(" + start.ToShortDateString() + " 至 " + end.ToShortDateString() + ")");
        Bar bar = new Bar();
        bar.Colour = "#345";
        //bar.Fillalpha = 0.5;
        //bar.Fontsize = 10;
        bar.Text = "用户数量";
        List<double> values = new List<double>();
        string condition = " RegTime BETWEEN '{0}' AND '{1}'";
        List<string> days = new List<string>();
        int breakIndex = 0;
        if (start.Month != end.Month)
        {
            breakIndex = DateTime.DaysInMonth(start.Year, start.Month);
        }
        TimeSpan span = end.Subtract(start);
        int startDay = start.Day;
        int startMonth = start.Month;
        for (int index = 0; index <= span.Days; index++)
        {
            object[] args = new object[2];
            args[0] = start.Year.ToString() + "-" + startMonth.ToString() + "-" + startDay.ToString() + " 0:0:0";
            args[1] = start.Year.ToString() + "-" + startMonth.ToString() + "-" + startDay.ToString() + " 23:59:59";
            values.Add((double)bll.GetCount(string.Format(condition, args)));
            days.Add(startMonth.ToString() + "." + startDay.ToString());

            if (startDay == breakIndex)
            {
                startMonth++;
                startDay = 1;
            }
            else
            {
                startDay++;
            }
        }

        bar.Values = values;

        chart.AddElement(bar);
        YAxis y = new YAxis();
        chart.X_Axis.Labels.SetLabels(days);// = days;//.Values fix
        chart.X_Axis.Steps = 1;
        double maxY = GetMax(values);
        y.SetRange(0, maxY);
        int step = Convert.ToInt32(maxY * _definition);
        if (step > 0)
        {
            y.Steps = step;
        }
        chart.Y_Axis = y;
        Response.Clear();
        Response.Write(chart.ToString());
        Response.End();
    }

    private double GetMax(List<double> ld)
    {
        double max = 0;
        foreach (double temp in ld)
        {
            if (temp > max)
            {
                max = temp;
            }
        }

        return max;
    }
}
