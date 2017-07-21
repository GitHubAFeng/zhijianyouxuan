using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Timers;

/// <summary>
///asp.net 防止ddos攻击
/// </summary>
public class ANTDDOS : IHttpModule
{
    void IHttpModule.Dispose() { }

    void IHttpModule.Init(HttpApplication context)
    {
        context.BeginRequest += new EventHandler(context_BeginRequest);
    }

    private static Dictionary<string, short> _IpAdresses = new Dictionary<string, short>();
    private static Stack<string> _Banned = new Stack<string>();
    private static Timer _Timer = CreateTimer();
    private static Timer _BannedTimer = CreateBanningTimer();

    private const int BANNED_REQUESTS = 4; //规定时间内访问的最大次数
    private const int REDUCTION_INTERVAL = 120000; // 1 秒（检查访问次数的时间段）
    private const int RELEASE_INTERVAL = 24*60 * 60 * 1000; // 5 分钟（清除一个禁止IP的时间段）

    private void context_BeginRequest(object sender, EventArgs e)
    {
        if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().IndexOf("sendgmscode.aspx") > 0)
        {
            string ip = HttpContext.Current.Request.UserHostAddress;
            if (_Banned.Contains(ip))
            {
                HttpContext.Current.Response.StatusCode = 403;
                HttpContext.Current.Response.End();
            }

            CheckIpAddress(ip);
        }
    }

    /// <summary>
    /// 检查访问IP
    /// </summary>
    private static void CheckIpAddress(string ip)
    {
        if (!_IpAdresses.ContainsKey(ip)) //如果没有当前访问IP的记录就将访问次数设为1
        {
            _IpAdresses[ip] = 1;
        }
        else if (_IpAdresses[ip] == BANNED_REQUESTS) //如果当前IP访问次数等于规定时间段的最大访问次数就拉于“黑名单”
        {
            _Banned.Push(ip);
            _IpAdresses.Remove(ip);
        }
        else //正常访问就加次数 1
        {
            _IpAdresses[ip]++;
        }
    }

    #region Timers

    /// <summary>
    /// 创建计时器，从_IpAddress减去一个请求。
    /// </summary>
    private static Timer CreateTimer()
    {
        Timer timer = GetTimer(REDUCTION_INTERVAL);
        timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
        return timer;
    }

    /// <summary>
    /// 创建定时器，消除一个禁止的IP地址
    /// </summary>
    /// <returns></returns>
    private static Timer CreateBanningTimer()
    {
        Timer timer = GetTimer(RELEASE_INTERVAL);
        timer.Elapsed += delegate { _Banned.Pop(); }; //消除一个禁止IP
        return timer;
    }

    /// <summary>
    /// 创建一个时间器，并启动它
    /// </summary>
    /// <param name="interval">以毫秒为单位的时间间隔</param>
    private static Timer GetTimer(int interval)
    {
        Timer timer = new Timer();
        timer.Interval = interval;
        timer.Start();

        return timer;
    }

    /// <summary>
    /// 减去从集合中的每个IP地址的请求
    /// </summary>
    private static void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        foreach (string key in _IpAdresses.Keys)
        {
            _IpAdresses[key]--;
            if (_IpAdresses[key] == 0)
                _IpAdresses.Remove(key);
        }
    }

    #endregion

}
