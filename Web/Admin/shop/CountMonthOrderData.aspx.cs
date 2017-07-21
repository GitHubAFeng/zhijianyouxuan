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

// 获取统计数据返回统计图形 月份统计
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-07-17

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
public partial class Admin_Sale_CountMonthOrderData :AdminPageBase
{
    //Hangjing.BLL.ETogoOrder bll = new Hangjing.BLL.ETogoOrder();

    protected void Page_Load(object sender, EventArgs e)
    {
        GetDayCount();
    }

    private void GetDayCount()
    {

        //IList<OrderCountInfo> list = new List<OrderCountInfo>();

        //DateTime Now = DateTime.Now;

        //list = bll.GetOrderCount(2, Now.Year.ToString(), Now.Month.ToString(), Now.Day.ToString());

        //Graph graph = new Graph();
        ////定义图表的标题。。
        //graph.Title = new Title("This Month Order", "{font-size:20px; color: #736AFF; margin: 5px; padding:5px; padding-left: 20px; padding-right: 20px;}");

        ////计算此次订单量的最大值
        //int MaxCount = 0;
        //for (int j = 0; j < list.Count; j++)
        //{
        //    if (list[j].CountIntValue > MaxCount)
        //    {
        //        MaxCount = list[j].CountIntValue;
        //    }
        //}

        ////Y轴起始终止坐标
        //graph.MaxY = GetMaxY(MaxCount);//根据实际情况赋值最大值
        //graph.MinY = 0;

        //////定义柱状对象(链接透明度，bar条颜色，项目标题，项目标题字体大小)
        ////OpenFlashChart.Charts.LineHollow bar =
        ////    new OpenFlashChart.Charts.LineHollow(3, 5, "#99c844", "有打印机商家订单", 15);5964a4

        //OpenFlashChart.Charts.LineHollow bar2 = new OpenFlashChart.Charts.LineHollow(3, 5, "#5964a4", "Orders", 15);

        ////赋值份额
        //Random rd = new Random();
        //for (int i = 1; i < 31; i++)
        //{
        //    float value = (float)0.0;
        //    //bar.Data.Add(rd.Next(0, 100));
        //    for (int j = 0; j < list.Count; j++)
        //    {
        //        if (list[j].CountKey == i.ToString())
        //        {
        //            value = (float)(list[j].CountIntValue);
        //            break;
        //        }
        //    }

        //    bar2.Data.Add(value);
        //    //X轴标签
        //    graph.LabelsX.Add(string.Format("Day:{0}", i));

        //}
        //graph.TickSizeX = 5;

        ////3d X轴高度
        //graph.AxisX3D = 8;

        ////x,y轴颜色。
        //graph.AxisColorX = "#909090";
        //graph.AxisColorY = "#909090";

        ////Y轴标签
        //graph.LegendY = new LegendY("Order Number", 15, "#736AFF");

        ////graph.Data.Add(bar);
        //graph.Data.Add(bar2);

        //Response.Clear();
        //Response.Write(graph.ToString());
        //Response.End();
    }

    private int GetMaxY(int MaxCount)
    {
        int MaxY = 0;
        //根据最大值计算此次图形显示的Y轴的最大值
        if (MaxCount < 100)
        {
            MaxY = 100;
        }
        else
        {
            int Step = (int)System.Math.Pow((double)10, (double)(MaxCount.ToString().Length - 1));
            MaxY = ((MaxCount + Step) / Step) * Step;
        }

        return MaxY;
    }
}
