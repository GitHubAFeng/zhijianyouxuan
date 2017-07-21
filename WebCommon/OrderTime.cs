using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;

using Hangjing.Common;
using Hangjing.Model;
using System.Text;


/// <summary>
///处理用户下订单时可选择的送餐时间范围
/// </summary>
public class OrderTime
{
    private PointsInfo _togo;
    private int _addminute;
    public List<string> _timelist;
    private int _nowtimetoint = 0;
    private DateTime _starttime;
    private DateTime _endtime;

    /// <summary>
    /// 初始化相关信息
    /// </summary>
    /// <param name="togo"></param>
    /// <param name="addminutes">-1表示第二天的。大于0表示当前的</param>
    public OrderTime(PointsInfo togo, int addminutes)
    {
        _togo = togo;
        _addminute = addminutes;
        _timelist = new List<string>();

        if (_addminute > 0)
        {
            _nowtimetoint = Convert.ToInt32(DateTime.Now.AddMinutes(_addminute).ToString("HHmm"));
        }

        initTimelist();
    }
    private void Timelist(PointsInfo info) 
    {
        _togo = info;
        _togo.Time1Start = info.Opentimes1;
        _togo.Time2Start = info.Closetimes1;

        _togo.Time1End = info.Opentimes2;
        _togo.Time2End = info.Closetimes2;

        DateTime starttime = info.Opentimes1; // Convert.ToDateTime(SectionProxyData.GetSetValue(50));
        DateTime endtime = info.Opentimes2;  // Convert.ToDateTime(SectionProxyData.GetSetValue(51));

        if (DateTime.Now > Convert.ToDateTime(starttime))
        {
            starttime = DateTime.Now;
        }
        int addminutes = info.senttime;   //Convert.ToInt32(SectionProxyData.GetSetValue(49));

        starttime = starttime.AddMinutes(addminutes);

        _starttime = starttime;
        _endtime = endtime;

        //_togo.Time1Start = _togo.Time2Start = starttime;
        //_togo.Time1End = _togo.Time2End = endtime;


        _addminute = addminutes;
        _timelist = new List<string>();

        if (_addminute > 0)
        {
            _nowtimetoint = Convert.ToInt32(DateTime.Now.AddMinutes(_addminute).ToString("HHmm"));
        }

        initTimelist();

        if (_timelist.Count == 0)
        {
            _timelist.Add("当前不配送");
        }
    }
    /// <summary>
    /// 初始化相关信息，根据配置信息
    /// </summary>
    public OrderTime(DropDownList ddltime, PointsInfo info)
    {
        Timelist(info);
        ddltime.DataSource = _timelist;
        ddltime.DataBind();
    }
    /// <summary>
    /// 初始化相关信息，根据配置信息
    /// </summary>
    public OrderTime(HtmlSelect ddltime, PointsInfo info)
    {
        Timelist(info);
        ddltime.DataSource = _timelist;
        ddltime.DataBind();
    }

    /// <summary>
    /// 返回可选择时间的项目， 放在 _timelist中
    /// </summary>
    /// <returns></returns>
    private void initTimelist()
    {
        _timelist.Clear();
        StringBuilder sb = new StringBuilder();
        if (_togo.Time1Start == _togo.Time2Start)//两个时间段相同
        {
            int minhour = _togo.Time1Start.Hour;
            int maxhour = _togo.Time1End.Hour;
            for (int i = minhour; i <= maxhour; i++)
            {
                initOneHouerTimeBy10(i);
            }
        }
        else//
        {
            //第一个时间
            int minhour1 = _togo.Time1Start.Hour;
            int maxhour1 = _togo.Time1End.Hour;
            for (int i = minhour1; i <= maxhour1; i++)
            {
                initOneHouerTimeBy10(i);
            }
            //第二个时间
            int minhour2 = _togo.Time2Start.Hour;
            int maxhour2 = _togo.Time2End.Hour;
            for (int i = minhour2; i <= maxhour2; i++)
            {
                initOneHouerTimeBy10(i);
            }
        }
        //删除不在营业时间段的
        List<string> inbuinesslist = new List<string>();
        int timetoint = 0;
        foreach (var item in _timelist)
        {
            timetoint = Convert.ToInt32(item.Replace(":", ""));
            if ((timetoint >= TimeToInt(_starttime) && timetoint <= TimeToInt(_togo.Time1End)) || (timetoint >= TimeToInt(_starttime) && timetoint <= TimeToInt(_togo.Time2End)))
            {
                inbuinesslist.Add(item);
            }
        }
        _timelist.Clear();
        foreach (var item in inbuinesslist)
        {
            _timelist.Add(item);
        }

    }


    /// <summary>
    /// 设置一个小时的时间段
    /// </summary>
    private void initOneHouerTime(int houer)
    {
        _timelist.Add(houer + ":00");
        _timelist.Add(houer + ":15");
        _timelist.Add(houer + ":30");
        _timelist.Add(houer + ":45");
    }


    /// <summary>
    /// 设置一个小时的时间段
    /// </summary>
    private void initOneHouerTimeBy10(int houer)
    {
        _timelist.Add(houer + ":00");
        _timelist.Add(houer + ":10");
        _timelist.Add(houer + ":20");
        _timelist.Add(houer + ":30");
        _timelist.Add(houer + ":40");
        _timelist.Add(houer + ":50");
    }

    /// <summary>
    /// 根据条件，筛选相关符合记录的。返回填充select的字串。
    /// </summary>
    /// <returns></returns>
    public string getTimesStr()
    {
        List<string> goodlist = new List<string>();
        //两步，不在时间范围内的删除
        foreach (var item in _timelist)
        {
            if (_addminute < 0 || isgooditem(item))
            {
                goodlist.Add(item);
            }
        }
        StringBuilder sb = new StringBuilder();
        foreach (var item in goodlist)
        {
            sb.Append("<option value='" + item + "'>" + item + "</option>");
        }
        if (goodlist.Count == 0)
        {
            sb.Append("<option value='" + -1 + "'>当前不配送</option>");
        }

        return sb.ToString();
    }

    /// <summary>
    /// 判断一个时间，是否是用户可选择的[两个条件:当前天的要判断加了分后的时间是否在商家范围内，次日，判断]
    /// </summary>
    /// <param name="timeitem"></param>
    /// <returns></returns>
    private bool isgooditem(string timeitem)
    {
        bool isgood = false;

        if (_nowtimetoint <= Convert.ToInt32(timeitem.Replace(":", "")))
        {
            isgood = true;
        }
        return isgood;
    }

    /// <summary>
    /// 时间类型，取小时，分钟转成4位整数。
    /// </summary>
    /// <param name="sendtime"></param>
    /// <returns></returns>
    private int TimeToInt(DateTime sendtime)
    {
        return Convert.ToInt32(sendtime.ToString("HHmm"));
    }
}
