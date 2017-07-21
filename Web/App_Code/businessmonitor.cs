using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// 实时监控数据
/// </summary>
public class businessmonitor
{
    /// <summary>
    /// 第一行数据
    /// </summary>
    public decimal quota;
    /// <summary>
    /// 第二行的数据
    /// </summary>
    public string quota1;

    private int cityid = 0;


    public businessmonitor(string method, int _cityid)
    {
        cityid = _cityid;

        //反射调用方法
        MethodInfo methodInfo = this.GetType().GetMethod(method, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
        methodInfo.Invoke(this, null);

    }

    /// <summary>
    /// 配送员相关
    /// </summary>
    public void rider()
    {
        int alldeliver = SectionProxyData.GetDeliverList().Where(a => a.Inve1 == cityid).ToList().Count;
        int onlinedeliver = 0;
        string onlinerat = "0";

        IList<DeliverInfo> onlinedelivers = new Deliver().GetDeliverList(1000, 1, "Inve1=" + cityid, "dataid", 1, "lasttime >= '" + DateTime.Now.AddMinutes(-1 * Constant.OffLineDoor) + "'");


        if (onlinedelivers.Count > 0)
        {
            onlinedeliver = onlinedelivers[0].recordtcount;
        }
        if (alldeliver > 0)
        {
            onlinerat = (onlinedeliver * 1.0 / alldeliver * 100).ToString("0");
        }

        quota = alldeliver;
        quota1 = onlinerat;
    }

    /// <summary>
    /// 超时
    /// </summary>
    public void timeout()
    {
        Custorder dal = new Custorder();
        int allorder = dal.GetCount("cityid=" + cityid + " and DATEDIFF(day,OrderDateTime,GETDATE()) =0");
        int timeoutorder = dal.GetCount("cityid=" + cityid + " and DATEDIFF(day,OrderDateTime,GETDATE()) =0 and OrderStatus in (1,2,7) and  SendTime < getdate()");
        string timeoutrat = "0";


        if (allorder > 0)
        {
            timeoutrat = (timeoutorder * 1.0 / allorder * 100).ToString("0");
        }

        quota = Convert.ToDecimal(timeoutrat);
        quota1 = timeoutorder.ToString();
    }

    /// <summary>
    /// 负载
    /// </summary>
    public void loadCount()
    {
        Custorder dal = new Custorder();
        int allorder = dal.GetCount("cityid=" + cityid + " and DATEDIFF(day,OrderDateTime,GETDATE()) =0 and OrderStatus in (1,2,7) ");

        int onlinedeliver = 0;
        string onlinerat = "0";

        IList<DeliverInfo> onlinedelivers = new Deliver().GetDeliverList(1000, 1, "Inve1=" + cityid, "dataid", 1, "lasttime >= '" + DateTime.Now.AddMinutes(-1 * Constant.OffLineDoor) + "'");


        if (onlinedelivers.Count > 0)
        {
            onlinedeliver = onlinedelivers[0].recordtcount;
        }

        if (onlinedeliver > 0)
        {
            onlinerat = (allorder * 1.0 / onlinedeliver).ToString("0.00");
        }


        quota = Convert.ToDecimal(onlinerat);
        quota1 = "0";
    }

    /// <summary>
    /// 订单数
    /// </summary>
    public void total()
    {
        Custorder dal = new Custorder();
        int todayorder = dal.GetCount("cityid=" + cityid + " and DATEDIFF(day,OrderDateTime,GETDATE()) =0");
        int yestodayorder = dal.GetCount("cityid=" + cityid + " and OrderDateTime > '"+DateTime.Now.AddDays(-1).ToShortDateString()+ "' and OrderDateTime < '"+DateTime.Now.AddDays(-1)+"'");
       


        quota = todayorder;
        quota1 = yestodayorder.ToString();
    }
}