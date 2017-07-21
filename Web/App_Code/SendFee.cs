using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Cache;

/// <summary>
///更新坐标得出距离
/// </summary>
public class SendFee
{
    private decimal distance = 0; 

    private SendfeeInfo param = null;
    public SendFee(SendfeeInfo info)
    {
        param = info;
    }

    /// <summary>
    /// 计算配送费
    /// </summary>
    /// <returns></returns>
    public SendInfo getSendFee()
    {
        SendInfo send = new SendInfo();
        distance = getDistance(param.latlng.ulat, param.latlng.ulng, param.latlng.slat, param.latlng.slng);
        send.Distance = distance;
        send.sendmoney = GetSendMoney();//这里暂时使用的原来的（每公里多少的配送费） 2015-12-02 

        return send;
    }

    /// <summary>
    /// 每公里多少的配送费
    /// </summary>
    /// <returns></returns>
    public decimal GetSendMoney()
    {
        decimal sendmoney = 0;
        decimal price = 0;
        try
        {
            price = Convert.ToDecimal(CacheHelper.GetSetValue(66));//跑腿的每公里的配送费
        }
        catch
        {
            Hangjing.WebCommon.HJlogx.toLog("网站配置表DataId=66： 跑腿配送费格式错误");
        }

        sendmoney = distance * price;
        return Convert.ToDecimal(sendmoney.ToString("0.0"));//保存1位小数
    }





    public SendFee(string ulat,string ulng,string tlat,string tlng)
    {
        distance = getDistance(ulat, ulng, tlat, tlng);
    }

    /// <summary>
    /// 计算两点距离(公里)
    /// </summary>
    /// <param name="lat1"></param>
    /// <param name="lng1"></param>
    /// <param name="lat2"></param>
    /// <param name="lng2"></param>
    /// <returns></returns>
    protected decimal getDistance(string _lat1, string _lng1, string _lat2, string _lng2)
    {
        double PI = 3.14159265358979323; // 圆周率
        double R = 6371; // 地球的半径
        double distance = 0;
        double x, y;
        double lat1 = Convert.ToDouble(_lat1);
        double longt1 = Convert.ToDouble(_lng1);
        double lat2 = Convert.ToDouble(_lat2);
        double longt2 = Convert.ToDouble(_lng2);
        x = (longt2 - longt1) * PI * R * Math.Cos(((lat1 + lat2) / 2) * PI / 180) / 180;
        y = (lat2 - lat1) * PI * R / 180;
        distance = Math.Sqrt(x * x + y * y);
        return Convert.ToDecimal(distance);
    }


}
