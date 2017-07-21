using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

public partial class _account_QQcallback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //登录成功返回code,再获取access token
        if (Request["code"] != null && Request["grant_type"] == null)
        {
            string access_token = getaccess_token(Request["code"]);
            Response.Write("access_token=" + access_token + " ");
            string openid = getopenid(access_token);
            Response.Write("openid=" + openid + " ");
            QQInfo my = get_user_info(access_token, openid);

            WebUtility.getRequestUrl(openid, my.nickname, "QQ");
        }
        else
        {

        }
    }

    /// <summary>
    /// 远程接口登录,返回access_token
    /// </summary>
    /// <returns></returns>
    public string getaccess_token(string code)
    {
        string key = ConfigurationManager.AppSettings["ConsumerKey"];
        string ConsumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];

        string return_content = "";
        StringBuilder myparam = new StringBuilder();
        myparam.Append("grant_type=authorization_code");
        myparam.Append("&client_id=" + key);
        myparam.Append("&client_secret=" + ConsumerSecret);
        myparam.Append("&code=" + code);
        myparam.Append("&state=1");
        myparam.Append("&redirect_uri=" + WebUtility.GetConfigsite() + "/Account/QQcallback.aspx");
        string param = myparam.ToString();

        byte[] bs = System.Text.Encoding.Default.GetBytes(param);
        string url = "https://graph.qq.com/oauth2.0/token";
        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";
        req.ContentLength = bs.Length;
        Stream reqStream = req.GetRequestStream();
        reqStream.Write(bs, 0, bs.Length);
        reqStream.Close();

        HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();
        StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.GetEncoding("GB2312"));
        return_content = reader.ReadToEnd();
        reader.Close();
        myResponse.Close();
        //access_token=F227BF364EDEEF1C2A79E8E7F78D269D&expires_in=7776000
        string[] pras = return_content.Split('&');
        string[] access_token = pras[0].Split('=');
        return access_token[1];
    }

    /// <summary>
    /// 根据access_token 获取openid.这个openid会保存在数据库，也是一个标识
    /// </summary>
    /// <param name="access_token"></param>
    /// <returns></returns>
    public string getopenid(string access_token)
    {
        string url = "https://graph.qq.com/oauth2.0/me?access_token=" + access_token;
        string return_content = RequestUrl(url);
        string jsonstr = return_content.Replace("callback", "");
        jsonstr = jsonstr.Replace("(", "").Trim();
        jsonstr = jsonstr.Replace(")", "").Trim();
        jsonstr = jsonstr.Replace("{", "").Trim();
        jsonstr = jsonstr.Replace("}", "").Trim();
        jsonstr = jsonstr.Replace("\"", "").Trim();
        jsonstr = jsonstr.Replace(";", "").Trim();
        string[] pras = jsonstr.Trim().Split(',');
        string[] openid = pras[1].Split(':');
        return openid[1].Trim();
    }

    /// <summary>
    /// 访问远程端口，返回数据
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string RequestUrl(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.MaximumAutomaticRedirections = 3;
        request.Timeout = 0x2710;
        Stream responseStream = ((HttpWebResponse)request.GetResponse()).GetResponseStream();
        StreamReader reader = new StreamReader(responseStream);
        string str = reader.ReadToEnd();
        reader.Close();
        responseStream.Close();
        return str;
    }

    /// <summary>
    /// 根据access_token 获取openid.这个openid会保存在数据库，也是一个标识
    /// </summary>
    /// <param name="access_token"></param>
    /// <returns></returns>
    public QQInfo get_user_info(string access_token, string openid)
    {
        string key = ConfigurationManager.AppSettings["ConsumerKey"];
        string url = "https://graph.qq.com/user/get_user_info?access_token=" + access_token;
        url += "&oauth_consumer_key=" + key;
        url += "&openid=" + openid;
        url += "&format=json";
        string return_content = RequestUrl(url);

        JObject jo = JObject.Parse(return_content);

        QQInfo s = new QQInfo();
        s.ret = Convert.ToInt32(jo["ret"].ToString());
        s.Openid = openid;
        s.nickname = WebUtility.DelMarks(jo["nickname"].ToString());
        return s;
    }
}

