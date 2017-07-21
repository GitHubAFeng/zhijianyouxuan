using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;

using Hangjing.Model;
using Hangjing.DBUtility;
using System.Data;
using System.Data.SqlClient;
using Hangjing.SQLServerDAL;

/// <summary>
///HJSendMsg 的摘要说明
/// </summary>
public class SendMsg
{

    /// <summary>
    ///  发送短信
    /// </summary>
    /// <param name="mobile"></param>
    /// <param name="cont"></param>
    /// <returns>1表示成功，0表示失败</returns>
    public static int send(string mobile, string content)
    {
        return send(mobile, content, 0);
    }

    /// <summary>
    /// 发送短信,带是否验证次数
    /// </summary>
    /// <param name="mobile">手机号</param>
    /// <param name="content">内容</param>
    /// <param name="islimity">0表示不限制次数，1表示要要限制</param>
    /// <returns></returns>
    public static int send(string mobile, string content, int islimity)
    {
        string account = SectionProxyData.GetSetValue(67);
        string password = SectionProxyData.GetSetValue(68);

        string PostUrl = ConfigurationManager.AppSettings["WebReference.Service.PostUrl"];
        Regex rx = new Regex(@"^[0-9]*$");
        if (rx.IsMatch(mobile) && mobile.Length == 11)
        {

        }
        else
        {
            return 0;
        }

        //if ((islimity == 1) && !checkphone(mobile))
        //{
        //    Hangjing.Common.HJlog.toLog("同一号码，ip一天内发送5次以上：" + mobile);
        //    return 0;
        //}

        int status = 1;
        string statusMsg = "";

        string postStrTpl = "account={0}&password={1}&mobile={2}&content={3}";

        UTF8Encoding encoding = new UTF8Encoding();
        byte[] postData = encoding.GetBytes(string.Format(postStrTpl, account, password, mobile, content));

        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(PostUrl);
        myRequest.Method = "POST";
        myRequest.ContentType = "application/x-www-form-urlencoded";
        myRequest.ContentLength = postData.Length;

        Stream newStream = myRequest.GetRequestStream();
        // Send the data.
        newStream.Write(postData, 0, postData.Length);
        newStream.Flush();
        newStream.Close();

        HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
        if (myResponse.StatusCode == HttpStatusCode.OK)
        {
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

            //Response.Write(reader.ReadToEnd());

            string res = reader.ReadToEnd();
            int len1 = res.IndexOf("</code>");
            int len2 = res.IndexOf("<code>");
            string code = res.Substring((len2 + 6), (len1 - len2 - 6));

            int len3 = res.IndexOf("</msg>");
            int len4 = res.IndexOf("<msg>");
            statusMsg = res.Substring((len4 + 5), (len3 - len4 - 5));

        }
        else
        {
            status = 0;
            statusMsg = "短信接口访问失败";
            Hangjing.Common.HJlog.toLog("短信接口访问失败");
        }

        //发送记录
        msgRecordInfo info = new msgRecordInfo();
        info.RanId = 0;
        info.OrderId = mobile;
        info.AddDate = DateTime.Now;
        info.Contents = content;
        info.Inve1 = 0;
        info.Inve2 = statusMsg;
        info.Inve3 = GetIP();
        info.Inve4 = status.ToString();

        new msgRecord().Add(info);
        return 1;
    }

    /// <summary>
    /// 判断当前手机号,ip一天内发的短信。不能操作过10
    /// </summary>
    /// <param name="phone"></param>
    /// <returns>true表示可继续发短信</returns>
    private static bool checkphone(string phone)
    {
        msgRecord dalmsg = new msgRecord();
        bool issend = false;
        DateTime now = DateTime.Now;
        string phonesql = " OrderId = '" + phone + "' and AddDate  between '" + now.AddDays(-1) + "' and '" + now + "'";

        string ip = GetIP();

        string ipsql = " Inve3 = '" + ip + "' and AddDate  between '" + now.AddDays(-1) + "' and '" + now + "'";

        int ipcount = 50;
        int phonecount = 50;
        if (dalmsg.GetCount(phonesql) >= phonecount || dalmsg.GetCount(ipsql) >= ipcount)
        {
            issend = false;
        }
        else
        {
            issend = true;

        }
        return issend;
    }

    /// <summary>
    /// 获得当前页面客户端的IP
    /// </summary>
    /// <returns>当前页面客户端的IP</returns>
    public static string GetIP()
    {
        string result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        if (string.IsNullOrEmpty(result))
        {
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }

        if (string.IsNullOrEmpty(result))
        {
            result = HttpContext.Current.Request.UserHostAddress;
        }

        if (string.IsNullOrEmpty(result))
        {
            return "127.0.0.1";
        }

        return result;
    }
}
