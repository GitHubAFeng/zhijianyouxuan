using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

using Hangjing.SQLServerDAL;
using Hangjing.Model;

/// <summary>
/// 飞印打印机相关
/// </summary>
public class FeYinPrinter
{
    /// <summary>
    /// 商户编码
    /// </summary>
    private string memberCode;
    /// <summary>
    /// API 密钥
    /// </summary>
    private string securityKey;
    /// <summary>
    /// 终端编号
    /// </summary>
    private string deviceNo;
    /// <summary>
    /// 订单编号
    /// </summary>
    private string msgNo;

    /// <summary>
    /// 根据订单编号实例
    /// </summary>
    /// <param name="orderid"></param>
    public FeYinPrinter(string orderid)
    {
        msgNo = orderid;
        memberCode = SectionProxyData.GetSetValue(55);
        securityKey = SectionProxyData.GetSetValue(56);
        InitDeviceNo();
    }

    /// <summary>
    /// 默认
    /// </summary>
    /// <param name="orderid"></param>
    public FeYinPrinter()
    {
        memberCode = SectionProxyData.GetSetValue(55);
        securityKey = SectionProxyData.GetSetValue(56);
    }

    /// <summary>
    /// 根据订单编号，设置终端编号
    /// </summary>
    private void InitDeviceNo()
    {
        deviceNo = new TogoPrinter().GetPrinterSNByOrderid(msgNo);
    }

    /// <summary>
    /// 取得时间戳，类似于 java中的 System.currentTimeMillis()
    /// </summary>
    /// <returns></returns>
    public static long GetCurrentMilli()
    {
        DateTime Jan1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan javaSpan = DateTime.UtcNow - Jan1970;
        return (long)javaSpan.TotalMilliseconds;
    }

    /// <summary>
    /// 取得一串字符串的md5值
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string GetMD5Hash(string input)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
        bs = x.ComputeHash(bs);
        System.Text.StringBuilder s = new System.Text.StringBuilder();
        foreach (byte b in bs)
        {
            s.Append(b.ToString("x2").ToLower());
        }
        string password = s.ToString();
        return password;
    }


    /// <summary>
    /// 发送格式化的消息内容
    /// </summary>
    /// <param name="memberCode">商户编码</param>
    /// <param name="securityKey">API 密钥</param>
    /// <param name="msgNo">订单编号</param>
    /// <param name="deviceNo">终端编号</param>
    /// <param name="formatedMessage">打印内容</param>
    /// <returns></returns>
    public string SendFormatedMessage(string msgDetail)
    {
        string msg = SendFormatedMessage(memberCode, securityKey, msgNo, deviceNo, msgDetail, "", "", "", "", "", "", "", "", "");
        Hangjing.Common.HJlog.toLog("msg=" + msg);
        return msg;
    }

    /// <summary>
    /// 发送自由格式的消息内容
    /// </summary>
    /// <param name="memberCode">商户编码</param>
    /// <param name="securityKey">API 密钥</param>
    /// <param name="msgNo">订单编号</param>
    /// <param name="deviceNo">终端编号</param>
    /// <param name="formatedMessage">打印内容</param>
    /// <returns></returns>
    public string SendCustomMessage()
    {
        if (deviceNo == "")//没打印机直接返回
        {
            return "";
        }
        //添加打印日志，用于查询状态
        printorderlogInfo log = new printorderlogInfo();
        log.orderid = msgNo;
        log.printstate = 0;
        log.memberCode = memberCode;
        log.securityKey = securityKey;

        new printorderlog().Add(log);
        string msgDetail = new OrderTemplate(msgNo).getPrintStr();
        string msg = SendCustomMessage(memberCode, securityKey, msgNo, deviceNo, msgDetail);
        Hangjing.Common.HJlog.toLog("msg=" + msg);
        return msg;
    }

    /// <summary>
    /// 打印固定格式内容 
    /// </summary>
    /// <param name="memberCode"></param>
    /// <param name="securityKey"></param>
    /// <param name="msgNo"></param>
    /// <param name="deviceNo"></param>
    /// <param name="msgDetail"></param>
    /// <param name="charge"></param>
    /// <param name="customerName"></param>
    /// <param name="customerPhone"></param>
    /// <param name="customerAddress"></param>
    /// <param name="customerMemo"></param>
    /// <param name="extra1"></param>
    /// <param name="extra2"></param>
    /// <param name="extra3"></param>
    /// <param name="extra4"></param>
    /// <returns></returns>
    public string SendCustomMessage(string memberCode, string securityKey, string msgNo, string deviceNo, string msgDetail)
    {
        int mode = 2;
        long reqTime = GetCurrentMilli();
        string qstr = "memberCode=" + HttpUtility.UrlEncode(memberCode, Encoding.UTF8);
        qstr += "&msgDetail=" + HttpUtility.UrlEncode(msgDetail, Encoding.UTF8);
        qstr += "&deviceNo=" + HttpUtility.UrlEncode(deviceNo, Encoding.UTF8);
        qstr += "&msgNo=" + HttpUtility.UrlEncode(msgNo, Encoding.UTF8);
        qstr += "&mode=" + mode;
        qstr += "&reqTime=" + reqTime;
        qstr += "&securityCode=" + GetMD5Hash(memberCode + msgDetail + deviceNo + msgNo + reqTime + securityKey);

        return SendMessage(qstr);
    }

    /// <summary>
    /// 打印固定格式内容 
    /// </summary>
    /// <param name="memberCode"></param>
    /// <param name="securityKey"></param>
    /// <param name="msgNo"></param>
    /// <param name="deviceNo"></param>
    /// <param name="msgDetail"></param>
    /// <param name="charge"></param>
    /// <param name="customerName"></param>
    /// <param name="customerPhone"></param>
    /// <param name="customerAddress"></param>
    /// <param name="customerMemo"></param>
    /// <param name="extra1"></param>
    /// <param name="extra2"></param>
    /// <param name="extra3"></param>
    /// <param name="extra4"></param>
    /// <returns></returns>
    public string SendFormatedMessage(string memberCode, string securityKey, string msgNo, string deviceNo, string msgDetail, string charge,
           string customerName, string customerPhone, string customerAddress, string customerMemo,
           string extra1, string extra2, string extra3, string extra4)
    {
        int mode = 1;
        long reqTime = GetCurrentMilli();
        string qstr = "memberCode=" + HttpUtility.UrlEncode(memberCode, Encoding.UTF8);
        qstr += "&charge=" + HttpUtility.UrlEncode(charge, Encoding.UTF8);
        qstr += "&customerName=" + HttpUtility.UrlEncode(customerName, Encoding.UTF8);
        qstr += "&customerPhone=" + HttpUtility.UrlEncode(customerPhone, Encoding.UTF8);
        qstr += "&customerAddress=" + HttpUtility.UrlEncode(customerAddress, Encoding.UTF8);
        qstr += "&customerMemo=" + HttpUtility.UrlEncode(customerMemo, Encoding.UTF8);
        qstr += "&msgDetail=" + HttpUtility.UrlEncode(msgDetail, Encoding.UTF8);
        qstr += "&deviceNo=" + HttpUtility.UrlEncode(deviceNo, Encoding.UTF8);
        qstr += "&msgNo=" + HttpUtility.UrlEncode(msgNo, Encoding.UTF8);
        qstr += "&extra1=" + HttpUtility.UrlEncode(extra1, Encoding.UTF8);
        qstr += "&extra2=" + HttpUtility.UrlEncode(extra2, Encoding.UTF8);
        qstr += "&extra3=" + HttpUtility.UrlEncode(extra3, Encoding.UTF8);
        qstr += "&extra4=" + HttpUtility.UrlEncode(extra4, Encoding.UTF8);
        qstr += "&mode=" + mode;
        qstr += "&reqTime=" + reqTime;
        qstr += "&securityCode=" + GetMD5Hash(memberCode + customerName + customerPhone + customerAddress
                                            + customerMemo + msgDetail + deviceNo + msgNo + reqTime + securityKey);

        return SendMessage(qstr);
    }


    /// <summary>
    /// 公用的发送消息函数
    /// </summary>
    /// <returns></returns>
    private string SendMessage(string qstr)
    {
        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://my.feyin.net/api/sendMsg");
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";
        req.ContentLength = qstr.Length;

        string data = "";

        using (StreamWriter writer = new StreamWriter(req.GetRequestStream(), Encoding.ASCII))
        {
            writer.Write(qstr);
            writer.Flush();
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码
            }
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding)))
            {
                data = reader.ReadToEnd();
                response.Close();
            }
        }

        Hangjing.Common.HJlog.toLog("SendMessage :\r\nqstr = " + qstr + "\r\ndata=" + data);

        return data;
    }

    /// <summary>
    /// 查询打印状态
    /// </summary>
    /// <param name="memberCode">商户编码</param>
    /// <param name="msgNo"></param>
    /// <param name="securityKey"></param>
    /// <returns></returns>
    public string QueryState(string memberCode, string msgNo, string securityKey)
    {
        long reqTime = GetCurrentMilli();

        string qstr = "http://my.feyin.net/api/queryState?memberCode=" + HttpUtility.UrlEncode(memberCode, Encoding.UTF8);
        qstr += "&reqTime=" + reqTime;
        qstr += "&msgNo=" + HttpUtility.UrlEncode(msgNo, Encoding.UTF8);
        qstr += "&securityCode=" + GetMD5Hash(memberCode + reqTime + securityKey + msgNo);


        return Query(qstr);

    }

    /// <summary>
    /// 查询设备列表
    /// </summary>
    /// <param name="memberCode"></param>
    /// <param name="securityKey"></param>
    /// <returns></returns>
    public string ListDevice()
    {
        long reqTime = GetCurrentMilli();
        string qstr = "http://my.feyin.net/api/listDevice?memberCode=" + HttpUtility.UrlEncode(memberCode, Encoding.UTF8);
        qstr += "&reqTime=" + reqTime;
        qstr += "&securityCode=" + GetMD5Hash(memberCode + reqTime + securityKey);

        return Query(qstr);

    }

    /// <summary>
    /// 公共的HTTP查询方法
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private string Query(string url)
    {
        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
        req.Method = "GET";

        HttpWebResponse response = (HttpWebResponse)req.GetResponse();

        string encoding = response.ContentEncoding;
        if (encoding == null || encoding.Length < 1)
        {
            encoding = "UTF-8"; //默认编码
        }
        string data = "";
        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding)))
        {
            data = reader.ReadToEnd();
            response.Close();
        }

        Hangjing.Common.HJlog.toLog("Query :\r\nurl = " + url + "\r\ndata=" + data);

        return data;

    }
}