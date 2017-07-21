using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Net.Mail;


using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Drawing.Drawing2D;

using Hangjing.Common;
using Hangjing.DBUtility;
using System.Net;
using Hangjing.Model;
using Hangjing.SQLServerDAL;

/// <summary>
///WebUtility 的摘要说明
/// </summary>
public static class WebUtility
{
    /// <summary>
    /// 显示图片，无图片时显示默认图片(微信站点的)
    /// </summary>
    public static string ShowLocalPic(string src)
    {
        string ret = (new Control()).ResolveUrl("~/images/s_nopic.jpg");
        if (src == "")
        {
            return ret;
        }
        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(src)))
        {
            return (new Control()).ResolveUrl(src);
        }
        else
        {
            return ret;
        }
    }



    /// <summary>
    /// clone 对像
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RealObject"></param>
    /// <returns></returns>
    public static T Clone<T>(T RealObject)
    {
        using (Stream objectStream = new MemoryStream())
        {
            //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制
            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            formatter.Serialize(objectStream, RealObject);
            objectStream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(objectStream);
        }
    }
    /// <summary>
    /// 取出用户输入的危险字符并限制长度
    /// </summary>
    public static string InputText(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        text = text.Trim();
        if (text.Length > maxLength)
            text = text.Substring(0, maxLength);
        text = Regex.Replace(text, "[\\s]{2,}", " ");
        text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");
        text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");
        text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);
        text = text.Replace("'", "''");
        text = NoHTML(text);

        return text;
    }
    public static string GetConfigSid()
    {
        return ConfigurationManager.AppSettings["sectionid"].ToString();
    }
    /// <summary>
    ///保留一位小数
    /// </summary>
    public static string getOnepoint(object cost)
    {
        if (cost == null)
        {
            return "0.0";
        }


        string res = cost.ToString().Substring(0, cost.ToString().IndexOf('.') + 2);
        return res;
    }
    /// <summary>
    /// 取出用户输入的危险字符
    /// </summary>
    public static string InputText(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        text = text.Trim();
        text = Regex.Replace(text, "[\\s]{2,}", " ");
        text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");
        text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");
        text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);
        text = text.Replace("'", "''");
        text = NoHTML(text);

        return text;
    }

    /// <summary>
    /// 取出用户输入的危险字符
    /// </summary>
    public static string InputTextfix(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        text = text.Trim();
        text = Regex.Replace(text, "[\\s]{2,}", " ");
        text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");
        text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");
        text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);
        text = text.Replace("'", "''");

        return text;
    }

    /// <summary>
    /// 去除数字字符串中的非数字字符
    /// </summary>
    public static string CleanNonWord(string text)
    {
        return Regex.Replace(text, "\\W", "");
    }

    /// <summary>
    /// 去除html标签，除了\n
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static string RemoveUnsafeHtml(string content)
    {
        content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
        content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
        return content;
    }
    /// <summary>
    /// 用五角星显示等级
    /// </summary>
    /// <param name="obj">总共的星</param>
    /// <param name="avtive">亮的星</param>
    /// <returns></returns>
    public static string ShowXin(object obj, int avtive)
    {
        int i = Convert.ToInt32(obj);
        string url = new Control().ResolveUrl("~/images/xing_2.png");
        string url1 = new Control().ResolveUrl("~/images/xing_1.png");
        System.Text.StringBuilder str = new System.Text.StringBuilder();

        for (int j = 0; j < avtive; j++)
        {
            str.Append("<img src='" + url + "' />");
        }
        for (int j = 0; j < i - avtive; j++)
        {
            str.Append("<img src='" + url1 + "' />");
        }

        return str.ToString();
    }

    /// <summary>
    /// 截取字符串
    /// </summary>
    public static string Left(object str, int len)
    {
        if (str == null)
        {
            return "";
        }
        return Left(str.ToString(), len);
    }

    public static string Left(string str, int len)
    {
        char[] temp = WebUtility.InputText(str).ToCharArray();
        string ss = string.Empty;
        if (len < temp.Length)
        {
            for (int i = 0; i < len; i++)
            {
                ss = ss + temp[i].ToString();
            }
            return ss + "...";
        }
        else
        {
            return str;
        }
    }

    public static string LeftStr(string str, int len)
    {
        char[] temp = WebUtility.InputText(str).ToCharArray();
        string ss = string.Empty;
        if (len < temp.Length)
        {
            for (int i = 0; i < len; i++)
            {
                ss = ss + temp[i].ToString();
            }
            return ss + "...";
        }
        else
        {
            return str;
        }
    }

    /// <summary>
    /// 显示图片，无图片时显示默认图片
    /// </summary>
    public static string ShowPic(string src)
    {
        string ret = (new Control()).ResolveUrl("~/images/s_nopic.jpg");
        if (src == "")
        {
            return ret;
        }
        else
        {
            return src.Replace("~", GetMasterUrl());
        }
    }

    /// <summary>
    /// 显示图片，无图片时显示默认图片,不用分类传不能图片
    /// </summary>
    public static string ShowPicFix(string src, string img)
    {
        string ret = (new Control()).ResolveUrl("~/images/" + img);
        if (src == "")
        {
            return ret;
        }
        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(src)))
        {
            return (new Control()).ResolveUrl(src);
        }
        else
        {
            return ret;
        }
    }





    /// <summary>
    /// 获取城市选择
    /// </summary>
    /// <returns></returns>
    public static string get_userCityid()
    {
        string cityid = WebUtility.FixgetCookie("user_cityid");
        string temp = "0";
        if (cityid != "" && cityid != null)
        {
            WebUtility.FixsetCookie("user_cityid", temp, 365);
            temp = cityid;
        }
        return temp;
    }

    /// <summary>
    /// 通过判断COOKIE查看是否已阅读
    /// </summary>
    /// <param name="cookiename">cookies名称</param>
    /// <param name="cookievalue">cookie值</param>
    /// <returns>是否已阅读</returns>
    public static bool IsRead(string cookiename, string cookievalue)
    {
        //浏览次数加一
        HttpCookie myCookie = HttpContext.Current.Request.Cookies[cookiename];
        bool hasCookie = false;
        if (myCookie != null)
        {
            for (int i = 0; i < myCookie.Values.Count; i++)
            {
                if (myCookie.Values[i] == cookievalue)
                {
                    hasCookie = true;
                    break;
                }
            }
        }
        if (!hasCookie)
        {
            if (myCookie == null)
            {
                myCookie = new HttpCookie(cookiename);

            }
            myCookie.Values[cookievalue] = cookievalue;
            myCookie.Expires = DateTime.Now.AddDays(7);
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }
        return hasCookie;
    }

    public static bool IsRead(string cookiename, string key, string value)
    {
        HttpCookie myCookie = HttpContext.Current.Request.Cookies[cookiename];
        bool hasCookie = false;
        if (myCookie != null)
        {
            for (int i = 0; i < myCookie.Values.Count; i++)
            {
                if (myCookie.Values.Keys[i] == key)
                {
                    hasCookie = true;
                    break;
                }
            }
        }
        if (!hasCookie)
        {
            if (myCookie == null)
            {
                myCookie = new HttpCookie(cookiename);

            }
            else
            {
                if (myCookie.Values.Count == 6)
                {
                    myCookie.Values[0].Remove(0);
                }
            }
            myCookie.Values[key] = System.Web.HttpUtility.UrlEncode(value, Encoding.UTF8);
            myCookie.Expires = DateTime.Now.AddDays(7);
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }
        return hasCookie;
    }

    /// <summary>
    /// 生成订单ID 网上银行支付和支付宝支付  20090716-2793-14563412 (2793是首信易支付商家代码 14563412是秒数加随机数)
    /// </summary>
    public static string CreateOrderID()
    {
        string temp = string.Empty;
        System.Text.StringBuilder orderID = new System.Text.StringBuilder();
        DateTime now = System.DateTime.Now;
        orderID.Append(now.ToString("yyyyMMdd"));
        orderID.Append("-");
        orderID.Append(System.Configuration.ConfigurationManager.AppSettings["Sxy_Partner"]);
        orderID.Append("-");

        ////取年的最后一位 
        //temp = now.Year.ToString();
        //orderID.Append(temp.Substring(temp.Length - 1, 1));
        ////取天数
        //temp = now.DayOfYear.ToString();
        //orderID.Append(temp.PadRight(3, '0'));
        //取秒数
        temp = (now.Second + now.Minute * 60 + now.Hour * 360).ToString();
        orderID.Append(temp.PadLeft(5, '0'));
        //取随机数
        Random rand = new Random();
        temp = rand.Next(99).ToString();
        orderID.Append(temp.PadLeft(2, '0'));

        return orderID.ToString();
    }

    /*2009-08-26 加密解密*/
    private static byte[] Key64 = { 42, 16, 93, 156, 78, 4, 218, 32 };
    private static byte[] IV64 = { 55, 103, 246, 79, 36, 99, 167, 3 };
    private static byte[] Key192 = { 42, 16, 93, 156, 78, 4, 218, 32, 15, 167, 44, 80, 26, 250, 155, 112, 2, 94, 11, 204, 119, 35, 184, 197 };
    private static byte[] IV192 = { 55, 103, 246, 79, 36, 99, 167, 3, 42, 5, 62, 83, 184, 7, 209, 13, 145, 23, 200, 58, 173, 10, 121, 222 };

    public static String Encrypt(String valueString)
    {
        if (valueString != "")
        {　 //定义DES的Provider
            DESCryptoServiceProvider desprovider = new DESCryptoServiceProvider();
            //定义内存流
            MemoryStream memoryStream = new MemoryStream();
            //定义加密流
            CryptoStream cryptoStream = new CryptoStream(memoryStream, desprovider.CreateEncryptor(Key64, IV64),
            CryptoStreamMode.Write);
            //定义写IO流
            StreamWriter writerStream = new StreamWriter(cryptoStream);
            //写入加密后的字符流
            writerStream.Write(valueString);
            writerStream.Flush();
            cryptoStream.FlushFinalBlock();
            memoryStream.Flush();
            //返回加密后的字符串
            return (Convert.ToBase64String(memoryStream.GetBuffer(), 0,
            (int)memoryStream.Length));
        }

        return (null);
    }

    public static String Decrypt(String valueString)
    {
        if (valueString != "")
        {
            //定义DES的Provider
            DESCryptoServiceProvider desprovider = new DESCryptoServiceProvider();
            //转换解密的字符串为二进制
            byte[] buffer = Convert.FromBase64String(valueString);
            //定义内存流
            MemoryStream memoryStream = new MemoryStream();
            //定义加密流
            CryptoStream cryptoStream = new CryptoStream(memoryStream, desprovider.CreateEncryptor(Key64, IV64),
            CryptoStreamMode.Read);
            //定义读IO流
            StreamReader readerStream = new StreamReader(cryptoStream);
            //返回解密后的字符串
            return (readerStream.ReadToEnd());
        }

        return (null);
    }
    /*2009-08-26 end*/

    /// <summary>
    /// 用五角星显示等级
    /// </summary>
    /// <param name="obj">数字</param>
    /// <returns></returns>
    public static string ShowPentacle(object obj)
    {
        int i;
        string url = new Control().ResolveUrl("~/images/st5.gif");
        System.Text.StringBuilder str = new System.Text.StringBuilder();
        if (int.TryParse(obj.ToString(), out i))
        {
            for (int j = 0; j < i; j++)
            {

                str.Append("<img src='" + url + "' />");
            }
        }
        else
        {
            str.Append("<img src='" + url + "' />");
        }
        return str.ToString();
    }

    public static string ShowPentacle2(object obj)
    {
        int i;
        string url = new Control().ResolveUrl("~/UserHome/Images/easy_xingxing.gif");
        System.Text.StringBuilder str = new System.Text.StringBuilder();
        if (int.TryParse(obj.ToString(), out i))
        {
            for (int j = 0; j < i; j++)
            {

                str.Append("<img src='" + url + "' />");
            }
        }
        else
        {
            str.Append("<img src='" + url + "' />");
        }
        return str.ToString();
    }

    /// <summary>
    /// 跳转至错误页面
    /// </summary>
    /// <param name="errMsg">错误信息</param>
    /// <param name="backUrl">返回地址</param>
    public static void RedirError(string errMsg, string backUrl)
    {
        HttpContext.Current.Response.Redirect(string.Format("~/Error.aspx?msg={0}&url={1}", errMsg, backUrl));
    }

    /// <summary>
    /// 判断是否有微信浏览
    /// </summary>
    /// <param name="ostr"></param>
    /// <returns></returns>
    public static bool isWeiXin()
    {
        string HTTP_USER_AGENT = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
        if (HTTP_USER_AGENT == null)
        {
            return false;
        }
        if (HTTP_USER_AGENT.IndexOf("MicroMessenger") >= 0)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 跳转至错误页面
    /// </summary>
    /// <param name="errMsg">错误信息</param>
    public static void RedirError(string errMsg)
    {
        HttpContext.Current.Response.Redirect(string.Format("~/Error.aspx?msg={0}", errMsg));
    }

    /// <summary>
    /// 判断当前是否有授权，没有就去授权(根据openid,只有微信再判断)
    /// </summary>
    public static void WebOauth(HttpContext context)
    {
        WebUtility.FixsetCookie("reurl", context.Request.Url.ToString(), 365);
        string openid = WebUtility.FixgetCookie("openid");
        if ((openid == null || openid == "") && isWeiXin())
        {
            new Hangjing.Weixin.WebOAuth(context);
        }
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="destFileName">DirUrl路径</param>
    public static void getFile(string destFileName)
    {
        if (File.Exists(destFileName))
        {
            const int BUFFERSIZE = 1024;
            byte[] fileBuffer = new byte[BUFFERSIZE];
            int count = 0;
            FileStream fileStream = new FileStream(destFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            long fileSize = fileStream.Length;

            FileInfo fi = new FileInfo(destFileName);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = false;

            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(destFileName));
            HttpContext.Current.Response.AppendHeader("Content-Length", fileSize.ToString());
            HttpContext.Current.Response.AddHeader("Accept-Ranges", "bytes");
            HttpContext.Current.Response.ContentType = "application/octet-stream";

            try
            {
                while (true)
                {
                    int remain = ((int)fileSize - count);
                    if (remain >= BUFFERSIZE)
                    {
                        fileStream.Read(fileBuffer, 0, BUFFERSIZE);
                        HttpContext.Current.Response.BinaryWrite(fileBuffer);
                        count += BUFFERSIZE;
                    }
                    else if (remain > 0 && remain < BUFFERSIZE)
                    {
                        byte[] lastblock = new byte[remain];
                        fileStream.Read(lastblock, 0, remain);
                        HttpContext.Current.Response.BinaryWrite(lastblock);
                        count += BUFFERSIZE;
                        break;
                    }
                    else
                    {
                        if (count == fileSize)
                            break;
                        else
                            throw new Exception("Impossible");
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                fileStream.Close();
                HttpContext.Current.Response.End();
            }
        }
        else
        {
            RedirError("文件不存在或已删除！");
        }
    }

    /// <summary>
    /// 把指定数据源绑定到控件
    /// </summary>
    public static void BindList<T>(string str_Value, string str_Text, IList<T> IL, DropDownList myDropDownList)
    {
        myDropDownList.DataSource = IL;
        myDropDownList.DataValueField = str_Value;
        myDropDownList.DataTextField = str_Text;
        myDropDownList.DataBind();
    }

    /// <summary>
    /// 设置控件的选定项
    /// </summary>
    /// <param name="ddl">要设置的控件</param>
    /// <param name="value">要选定的value值</param>
    public static void RadioValue(RadioButtonList rblist, string value)
    {
        for (int i = 0; i < rblist.Items.Count; i++)
        {
            if (rblist.Items[i].Value == value)
            {
                rblist.Items[i].Selected = true;
                break;
            }
        }
    }

    /// <summary>
    /// 设置控件的选定项(CheckBoxList)
    /// </summary>
    /// <param name="ddl">要设置的控件</param>
    /// <param name="value">要选定的value值</param>
    public static void CheckValue(CheckBoxList ckblist, string value)
    {
        for (int i = 0; i < ckblist.Items.Count; i++)
        {
            if (ckblist.Items[i].Value == value)
            {
                ckblist.Items[i].Selected = true;
                break;
            }
        }
    }

    /// <summary>
    /// 设置控件的选定项
    /// </summary>
    /// <param name="ddl">要设置的控件</param>
    /// <param name="value">要选定的value值</param>
    public static void SelectValue(DropDownList ddl, string value)
    {
        for (int i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Value == value)
            {
                ddl.Items[i].Selected = true;
                break;
            }
        }
    }

    /// <summary>
    /// 设置控件的选定项
    /// </summary>
    /// <param name="ddl">要设置的控件</param>
    /// <param name="value">要选定的value值</param>
    public static void SelectValue(HtmlSelect ddl, string value)
    {
        for (int i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Value == value)
            {
                ddl.Items[i].Selected = true;
                break;
            }
        }
    }

    /// <summary>
    /// 转换商家营业状态
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string GetShopStatus(object status, object timee)
    {
        string rs = "";

        switch (status.ToString())
        {
            case "1":
                if (timee.ToString() == "0")
                {
                    rs = "已打烊";
                }
                else
                {
                    rs = "营业中";
                }

                break;
            case "0":
                rs = "暂停营业";
                break;
        }
        return rs;
    }
    /// <summary>
    /// 发送EMAIL
    /// </summary>
    /// <returns>发送是否成功</returns>
    public static bool SendMail(string to, string title, string body)
    {
        //邮件对象
        MailMessage emailMessage;
        //smtp客户端对象
        SmtpClient client;
        // 初始化邮件对象
        string from = ConfigurationManager.AppSettings["EmailAddress"];
        emailMessage = new MailMessage(from, to, title, body);
        emailMessage.IsBodyHtml = true;
        emailMessage.SubjectEncoding = System.Text.Encoding.Default;
        emailMessage.BodyEncoding = System.Text.Encoding.Default;
        //加入
        emailMessage.Headers.Add("X-Priority", "3");
        emailMessage.Headers.Add("X-MSMail-Priority", "Normal");
        emailMessage.Headers.Add("X-Mailer", "Microsoft Outlook Express 6.00.2900.2869");
        emailMessage.Headers.Add("X-MimeOLE", "Produced By Microsoft MimeOLE V6.00.2900.2869");
        emailMessage.Headers.Add("ReturnReceipt", "1");

        //邮件发送客户端
        client = new SmtpClient();

        //邮件服务器及帐户信息
        client.Host = ConfigurationManager.AppSettings["EmailServer"];
        //client.Host = "smtp.163.com";

        //client.Port = 465;
        //client.EnableSsl = true;
        System.Net.NetworkCredential Credential = new System.Net.NetworkCredential();

        Credential.UserName = ConfigurationManager.AppSettings["EmailUserName"];
        Credential.Password = ConfigurationManager.AppSettings["EmailPassword"];

        client.Credentials = Credential;

        try
        {
            client.Send(emailMessage);
        }
        catch (Exception e)
        {
            //错误处理待定
            string sMsg = e.Message;
            return false;
        }
        return true;

    }

    /// <summary>
    /// 产生随机字符串
    /// </summary>
    /// <param name="len">字符串长度</param>
    /// <returns>随机字符串</returns>
    public static string RandStr(int len)
    {
        char[] s = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        string str = String.Empty;
        Random random = new Random();
        for (int i = 0; i < len; i++)
        {
            str += s[random.Next(0, s.Length)].ToString();
        }
        return str;
    }

    /// <summary>
    /// 把字符中的相对路径替换为完整的客户端路径
    /// </summary>
    /// <param name="page">页面，一般为this</param>
    /// <param name="html">要替换的html字符串</param>
    /// <returns>替换后的字符串</returns>
    public static string ClientUrl(Page page, string html)
    {
        MatchCollection collection = Regex.Matches(html, "<.[^>]*(href|src)=(\\\"|'|)(.[^\\\"|'|]*)(\\\"|'|)[^>]*>", RegexOptions.IgnoreCase);

        foreach (Match match in collection)
        {
            Uri src = new Uri(match.Groups[match.Groups.Count - 2].Value, UriKind.RelativeOrAbsolute);
            Uri url = new Uri(page.Request.Url, src);
            html = html.Replace(match.Groups[match.Groups.Count - 2].Value, url.ToString());

        }
        return html;
    }

    /// <summary>
    /// 返回bool的字符表示
    /// </summary>
    /// <param name="ret"></param>
    /// <returns></returns>
    public static string ReturnBool(string ret)
    {
        return ret == "1" ? "是" : "否";
    }

    private const string StrKeyWord = @".*(select|insert|delete|from|count(|drop table|update|truncate|asc(|mid(|char(|xp_cmdshell|exec master|netlocalgroup administrators|:|net user|""|or|and).*";
    private const string StrRegex = @"[-|;|,|/|(|)|[|]|}|{|%|@|*|!|']";

    /// <summary>
    /// 获取Post的数据
    /// </summary>
    public static bool ValidUrlPostData()
    {
        bool result = false;

        for (int i = 0; i < HttpContext.Current.Request.Form.Count; i++)
        {
            result = ValidData(HttpContext.Current.Request.Form[i].ToString());
            if (result)
            {
                break;
            }//如果检测存在漏洞
        }
        return result;
    }

    /// <summary>
    /// 获取QueryString中的数据
    /// </summary>
    public static bool ValidUrlGetData()
    {
        bool result = false;

        for (int i = 0; i < HttpContext.Current.Request.QueryString.Count; i++)
        {
            result = ValidData(HttpContext.Current.Request.QueryString[i].ToString());
            if (result)
            {
                break;
            }//如果检测存在漏洞
        }
        return result;
    }

    /// <summary>
    /// 验证是否存在注入代码
    /// </summary>
    /// <param name="inputData"></param>
    public static bool ValidData(string inputData)
    {
        //里面定义恶意字符集合
        //验证inputData是否包含恶意集合
        string SqlStr = @"exec|execute|insert|delete|update|alter|create|drop|\*|chr|char|mid|substring|truncate|declare|xp_cmdshell|restore|backup|net +user|net +localgroup +administrators";
        string str_Regex = @"\b(" + SqlStr + @")\b";

        Regex Regex = new Regex(str_Regex, RegexOptions.IgnoreCase);
        if (Regex.IsMatch(inputData))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 获取正则表达式
    /// </summary>
    /// <param name="queryConditions"></param>
    /// <returns></returns>
    private static string GetRegexString()
    {
        //构造SQL的注入关键字符
        string[] strBadChar = 
        {
            //"and"
            "exec"
            ,"insert"
            //,"select"
            ,"delete"
            ,"update"
            ,"count"
            //,"from"
            ,"drop"
            //,"asc"
            // ,"char"
            //,"or"
            //,"*"
            //,"%"
            ,";"
            ,":"
           // ,"\'"
           // ,"\""
           // ,"-"
            ,"chr"
            ,"mid"
            ,"master"
            ,"truncate"
            //,"char"
            ,"declare"
            ,"SiteName"
            ,"net user"
            ,"xp_cmdshell"
            ,"/add"
            ,"exec master.dbo.xp_cmdshell"
            ,"net localgroup administrators"
        };

        //构造正则表达式
        string str_Regex = ".*(";
        for (int i = 0; i < strBadChar.Length - 1; i++)
        {
            str_Regex += strBadChar[i] + "|";
        }
        str_Regex += strBadChar[strBadChar.Length - 1] + ").*";

        return str_Regex;
    }

    /// <summary>
    ///SQL注入过滤
    /// </summary>
    /// <param name="InText">要过滤的字符串</param>
    /// <returns>如果参数存在不安全字符，则返回true</returns>
    public static bool SqlFilter(string InText)
    {
        string word = "and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join|'";
        if (InText == null)
            return false;
        foreach (string str_t in word.Split('|'))
        {
            if ((InText.ToLower().IndexOf(str_t + " ") > -1) || (InText.ToLower().IndexOf(" " + str_t) > -1) || (InText.ToLower().IndexOf(str_t) > -1))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 过滤标记
    /// </summary>
    /// <param name="NoHTML">包括HTML，脚本，数据库关键字，特殊字符的源码 </param>
    /// <returns>已经去除标记后的文字</returns>
    public static string NoHTML(string Htmlstring)
    {
        if (Htmlstring == null)
        {
            return "";
        }
        else
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);


            //删除与数据库相关的词
            Htmlstring = Regex.Replace(Htmlstring, "select", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "insert", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "delete from", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "count''", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "drop table", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "truncate", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "asc", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "mid", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "char", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "exec master", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "net localgroup administrators", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "and", "", RegexOptions.IgnoreCase);

        }

        return Htmlstring;
    }

    /// <summary>
    /// 获取母版的路径
    /// </summary>
    /// <returns></returns>
    public static string GetMasterPath()
    {
        return System.Configuration.ConfigurationManager.AppSettings["MasterPath"];
    }

    /// <summary>
    /// 获取出版社名称
    /// </summary>
    /// <returns></returns>
    public static string GetPublishName()
    {
        return System.Configuration.ConfigurationManager.AppSettings["PublishName"];
    }

    /// <summary>
    /// 获取Keywords
    /// </summary>
    /// <returns></returns>
    public static string GetKeywords()
    {
        return System.Configuration.ConfigurationManager.AppSettings["Keywords"];
    }

    /// <summary>
    /// 获取Description
    /// </summary>
    /// <returns></returns>
    public static string GetDescription()
    {
        return System.Configuration.ConfigurationManager.AppSettings["Description"];
    }

    /// <summary>
    /// 获取母版图片地址的路径
    /// </summary>
    /// <returns></returns>
    public static string GetMasterPicturePath()
    {
        return (new Control()).ResolveUrl(System.Configuration.ConfigurationManager.AppSettings["MasterPath"]);
    }

    /// <summary>
    /// 获取母版图片地址的路径
    /// </summary>
    /// <returns></returns>
    public static string GetMasterUrl(string url)
    {
        return (new Control()).ResolveUrl(url);
    }

    /// <summary>
    /// 获取当前时间，转化成字符串   (09-09-01 yangxiaolong@ihangjing.com)
    /// </summary>
    /// <returns></returns>
    public static string GetTime()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmss");
    }

    /// <summary >
    /// 创建Cookies
    /// </summary >
    /// <param   name="strName" >Cookie主键 </param >
    /// <param   name="strValue" >Cookie键值 </param >
    /// <param   name="strDay" >Cookie天数 </param >
    /// <code >Cookie ck = new Cookie(); </code >
    /// <code >ck.setCookie("主键","键值","天数"); </code >
    public static bool FixsetCookie(string strName, string strValue, int strDay)
    {
        try
        {
            HttpCookie Cookie = new HttpCookie(strName);
            Cookie.Expires = DateTime.Now.AddDays(strDay);
            Cookie.Value = strValue;
            System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary >
    /// 创建Cookies,过期时间为分钟
    /// </summary >
    /// <param   name="strName" >Cookie主键 </param >
    /// <param   name="strValue" >Cookie键值 </param >
    /// <param   name="strDay" >Cookie天数 </param >
    /// <code >Cookie ck = new Cookie(); </code >
    /// <code >ck.setCookie("主键","键值","分数"); </code >
    public static bool FixsetCookie_Minutes(string strName, string strValue, int strDay)
    {
        try
        {
            HttpCookie Cookie = new HttpCookie(strName);
            Cookie.Expires = DateTime.Now.AddMinutes(strDay);
            Cookie.Value = strValue;
            System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary >
    /// 创建Cookies
    /// </summary >
    /// <param   name="strName" >Cookie主键 </param >
    /// <param   name="strValue" >Cookie键值 </param >
    /// <param   name="strDay" >Cookie年数 </param >
    /// <code >Cookie ck = new Cookie(); </code >
    /// <code >ck.setCookie("主键","键值","年数"); </code >
    public static bool FixsetCookie_year(string strName, string strValue, int year)
    {
        try
        {
            HttpCookie Cookie = new HttpCookie(strName);
            Cookie.Expires = DateTime.Now.AddYears(year);
            Cookie.Value = strValue;
            System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary >
    /// 读取Cookies
    /// </summary >
    /// <param   name="strName" >Cookie 主键 </param >
    /// <code >Cookie ck = new Cookie(); </code >
    /// <code >ck.getCookie("主键"); </code >
    public static string FixgetCookie(string strName)
    {
        HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[strName];
        if (Cookie != null && Cookie.Value!=null)
        {
            return Cookie.Value.ToString();
        }
        else
        {
            return null;
        }
    }

    /// <summary >
    /// 删除Cookies
    /// </summary >
    /// <param   name="strName" >Cookie 主键 </param >
    /// <code >Cookie ck = new Cookie(); </code >
    /// <code >ck.delCookie("主键"); </code >
    public static bool FixdelCookie(string strName)
    {
        try
        {
            HttpCookie Cookie = new HttpCookie(strName);
            Cookie.Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// md5加密
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    public static string GetMd5(string src)
    {

        System.Security.Cryptography.MD5 MD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] t = MD5.ComputeHash(System.Text.Encoding.GetEncoding("GB2312").GetBytes(src));
        System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
        for (int i = 0; i < t.Length; i++)
        {
            sb.Append(t[i].ToString("x").PadLeft(2, '0'));
        }
        return sb.ToString();
    }

    /// <summary>
    /// 获得当前页面客户端的IP
    /// </summary>
    /// <returns>当前页面客户端的IP</returns>
    public static string GetIP()
    {
        string result = String.Empty;

        result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(result))
        {
            result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        if (string.IsNullOrEmpty(result))
        {
            result = HttpContext.Current.Request.UserHostAddress;
        }

        if (string.IsNullOrEmpty(result) || !Utils.IsIP(result))
        {
            return "127.0.0.1";
        }

        return result;
    }

    /// <summary>
    /// 向Page对象注册脚本
    /// </summary>
    public static void RegScript(Page page, string script)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "ShowWindow", "<script type='text/javascript' defer>" + script + "</script>");
    }
    /// <summary>
    /// 向页面注册脚本
    /// </summary>
    public static void RegScript(Page page, Control control, string script)
    {
        ScriptManager.RegisterStartupScript(control, page.GetType(), "UpdatePanel", script, true);
    }

    private const string PROC_PAGE = "pageselect";
    private const string PROC_PAGEPRI = "pageselectpri";
    private const string PROC_COUNT = "pagecount";
    private const string PARAM_TBLNAME = "@tblName";
    private const string PARAM_GETFIELDS = "@strGetFields";
    private const string PARAM_PRIMARY = "@primary";
    private const string PARAM_ORDERNAME = "@orderName";
    private const string PARAM_PAGESIZE = "@PageSize";
    private const string PARAM_PAGEINDEX = "@PageIndex";
    private const string PARAM_ORDERTYPE = "@OrderType";
    private const string PARAM_STRWHERE = "@strWhere";

    //zheng_jianfeng  for BBS search
    private const string BBS_PROC_PAGE = "pageselect_bbs";
    private const string BBS_PROC_PAGEPRI = "pageselectpri_bbs";


    /// <summary>
    /// 返回正确的论坛版主导航链接
    /// </summary>
    /// <param name="mlist"></param>
    /// <returns></returns>
    public static string InitModerator(string mlist)
    {
        string ret = "";
        if (mlist.Length > 0)
        {
            foreach (string m in mlist.Split(','))
            {
                ret += "<a href='showuser.aspx?uname=" + m + "'><span class='font_f'>" + m + "</span></a>&nbsp;&nbsp;";
            }

            return ret;
        }
        else
        {
            return "版主暂缺中";
        }
    }

    /// <summary>
    /// 设置控件的选定项
    /// </summary>
    /// <param name="ddl">要设置的控件</param>
    /// <param name="value">要选定的value值</param>
    public static void SelectValue(RadioButtonList ddl, string value)
    {
        for (int i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Value == value)
            {
                ddl.Items[i].Selected = true;
                break;
            }
        }
    }


    //public static string SendEmail(string email, string code)
    //{
    //    string url = "http://localhost:11398/Web/Activecount.aspx?email=" + email + "&code=" + code;
    //    string Title = "尊敬的用户： 你好";
    //    string Body = "感谢您注册成为我们的会员 , 请点击下面的链接来激活您的帐号!";
    //    Body += url;
    //    Email mail = new Email(email, Title, Body);
    //    return mail.Send();
    //}

    public static string ReturnLogType(string Type)
    {
        if (Type == "General")
        {
            return "常规";
        }
        else if (Type == "Error")
        {
            return "错误";
        }
        else
        {
            return "警告";
        }
    }

    /*使用示例 配置信息在config/SiteInfo.config
    if (fileUpload.HasFile)
    {
        string filepath = Server.MapPath("~/upload/togo/"+WebUtility.GetTime()+""+Path.GetExtension(fileUpload.PostedFile.FileName));
        this.fileUpload.SaveAs(filepath);

        System.Drawing.Image img = System.Drawing.Image.FromFile(filepath);
        string filepath_water = Server.MapPath("~/upload/togo/w" + WebUtility.GetTime() + "" + Path.GetExtension(fileUpload.PostedFile.FileName));

        WebUtility.AddImageSignPic(img, filepath_water, "~/images/watermark/watermark.gif", 9, 100,2);
        WebUtility.AddImageSignText(img, filepath_water, "Dianyifen.com", 9, 100, "Tahoma", 12);
        //删除原图片
    }
    */
    /// <summary>
    /// 加图片水印 代码来自discuz nt做部分调整 zjf@ihangjing.com
    /// </summary>
    /// <param name="filename">文件名</param>
    /// <param name="watermarkFilename">水印文件名</param>
    /// <param name="watermarkStatus">图片水印位置</param>
    /// 图片附件添加水印 0=不使用 1=左上 2=中上 3=右上 4=左中 5=中间 6=右中 7=左下 8=中下 9=右下
    public static void AddImageSignPic(System.Drawing.Image img, string filename, string watermarkFilename, int watermarkStatus, int quality, int watermarkTransparency)
    {
        //System.Drawing.Image img = System.Drawing.Image.FromFile(UploadDir + savedFileName);
        Graphics g = Graphics.FromImage(img);
        //设置高质量插值法
        //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //设置高质量,低速度呈现平滑程度
        //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //获取水印图片文件
        System.Drawing.Image watermark = new Bitmap(Utils.GetMapPath(watermarkFilename));

        //水印图片如果大于上传的图片则不能加水印
        if (watermark.Height >= img.Height || watermark.Width >= img.Width)
        {
            return;
        }

        ImageAttributes imageAttributes = new ImageAttributes();
        ColorMap colorMap = new ColorMap();

        colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
        colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
        ColorMap[] remapTable = { colorMap };

        imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

        float transparency = 0.5F;
        if (watermarkTransparency >= 1 && watermarkTransparency <= 10)
        {
            transparency = (watermarkTransparency / 10.0F);
        }

        float[][] colorMatrixElements = 
        {
            new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
            new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
            new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
            new float[] {0.0f,  0.0f,  0.0f,  transparency, 0.0f},
            new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
		};

        ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

        imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

        int xpos = 0;
        int ypos = 0;

        switch (watermarkStatus)
        {
            case 1:
                xpos = (int)(img.Width * (float).01);
                ypos = (int)(img.Height * (float).01);
                break;
            case 2:
                xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                ypos = (int)(img.Height * (float).01);
                break;
            case 3:
                xpos = (int)((img.Width * (float).99) - (watermark.Width));
                ypos = (int)(img.Height * (float).01);
                break;
            case 4:
                xpos = (int)(img.Width * (float).01);
                ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                break;
            case 5:
                xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                break;
            case 6:
                xpos = (int)((img.Width * (float).99) - (watermark.Width));
                ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                break;
            case 7:
                xpos = (int)(img.Width * (float).01);
                ypos = (int)((img.Height * (float).99) - watermark.Height);
                break;
            case 8:
                xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                ypos = (int)((img.Height * (float).99) - watermark.Height);
                break;
            case 9:
                xpos = (int)((img.Width * (float).99) - (watermark.Width));
                ypos = (int)((img.Height * (float).99) - watermark.Height);
                break;
        }

        g.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);

        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
        ImageCodecInfo ici = null;
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.MimeType.IndexOf("jpeg") > -1)
                ici = codec;
        }

        EncoderParameters encoderParams = new EncoderParameters();
        long[] qualityParam = new long[1];
        if (quality < 0 || quality > 100)
        {
            quality = 80;
        }

        qualityParam[0] = quality;

        EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
        encoderParams.Param[0] = encoderParam;

        if (ici != null)
        {
            img.Save(filename, ici, encoderParams);
        }
        else
        {
            img.Save(filename);
        }

        g.Dispose();
        img.Dispose();
        watermark.Dispose();
        imageAttributes.Dispose();
    }


    /// <summary>
    /// 增加图片文字水印 代码来自discuz nt做部分调整 zjf@ihangjing.com
    /// </summary>
    /// <param name="filename">文件名</param>
    /// <param name="watermarkText">水印文字</param>
    /// 图片附件添加文字水印的内容 {1}表示论坛标题 {2}表示论坛地址 {3}表示当前日期 {4}表示当前时间, 例如: {3} {4}上传于{1} {2}
    /// <param name="watermarkStatus">图片水印位置</param>
    public static void AddImageSignText(System.Drawing.Image img, string filename, string watermarkText, int watermarkStatus, int quality, string fontname, int fontsize)
    {
        Graphics g = Graphics.FromImage(img);
        Font drawFont = new Font(fontname, fontsize, FontStyle.Regular, GraphicsUnit.Pixel);
        SizeF crSize;
        crSize = g.MeasureString(watermarkText, drawFont);

        float xpos = 0;
        float ypos = 0;

        switch (watermarkStatus)
        {
            case 1:
                xpos = (float)img.Width * (float).01;
                ypos = (float)img.Height * (float).01;
                break;
            case 2:
                xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                ypos = (float)img.Height * (float).01;
                break;
            case 3:
                xpos = ((float)img.Width * (float).99) - crSize.Width;
                ypos = (float)img.Height * (float).01;
                break;
            case 4:
                xpos = (float)img.Width * (float).01;
                ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                break;
            case 5:
                xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                break;
            case 6:
                xpos = ((float)img.Width * (float).99) - crSize.Width;
                ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                break;
            case 7:
                xpos = (float)img.Width * (float).01;
                ypos = ((float)img.Height * (float).99) - crSize.Height;
                break;
            case 8:
                xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                ypos = ((float)img.Height * (float).99) - crSize.Height;
                break;
            case 9:
                xpos = ((float)img.Width * (float).99) - crSize.Width;
                ypos = ((float)img.Height * (float).99) - crSize.Height;
                break;
        }

        g.DrawString(watermarkText, drawFont, new SolidBrush(Color.White), xpos + 1, ypos + 1);
        g.DrawString(watermarkText, drawFont, new SolidBrush(Color.Black), xpos, ypos);

        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
        ImageCodecInfo ici = null;
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.MimeType.IndexOf("jpeg") > -1)
                ici = codec;
        }

        EncoderParameters encoderParams = new EncoderParameters();
        long[] qualityParam = new long[1];
        if (quality < 0 || quality > 100)
        {
            quality = 80;
        }

        qualityParam[0] = quality;

        EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
        encoderParams.Param[0] = encoderParam;

        if (ici != null)
        {
            img.Save(filename, ici, encoderParams);
        }
        else
        {
            img.Save(filename);
        }

        g.Dispose();
        img.Dispose();
    }

    /// <summary>
    /// 返回订单状态的中文显示
    /// 状态(1新增订单  2下单成功  3已经调度(已经删除此状态)  4 正在配送  5处理成功  0已经取消)
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
    public static string TurnOrderState(object _State)
    {
        return new OrderState().TurnOrderState(_State);
    }


    /// <summary>
    /// 1支付宝/2银联/3账户余额/4货到付款
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static string TurnPayModel(object x)
    {
        int d = Convert.ToInt32(x);
        string rs = "";
        switch (d)
        {
            case 4:
                rs = "货到付款";
                break;
            case 1:
                rs = "支付宝";
                break;
            case 2:
                rs = "银联";
                break;
            case 3:
                rs = "账户扣款";
                break;
            case 5:
                rs = "微信支付";
                break;
        }
        return rs;
    }

    /// <summary>
    /// 返回订单状态的中文显示
    /// 状态 0 未支付 1 成功 2支付未完成 3 支付失败
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
    public static string TurnPayState(string State)
    {
        string ret = "";
        switch (Convert.ToInt32(State))
        {
            case 0:
                ret = "未支付"; break;
            case 1:
                ret = "成功"; break;

        }

        return ret;
    }

    /// <summary>
    /// 返回订单来源的中文显示
    /// 0正常、1android、2iphone、3点餐580、4淘宝,5表示商家中心下订单（无菜品），6客服下订单
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
    public static string TurnOrderSource(object State)
    {
        string ret = "";
        switch (Convert.ToInt32(State))
        {
            case 0:
                ret = "网站"; break;
            case 1:
                ret = "微信"; break;
            case 2:
                ret = "android"; break;
            case 3:
                ret = "ios"; break;
        }

        return ret;
    }

    /// <summary>
    /// 返回分站地址
    /// </summary>
    /// <param name="site">所在分站:列如--hangzhou</param>
    /// <param name="type"> 0 返回分站首页; 1 -- 返回订单地址 ; 2 返回收藏地址 ; 3 返回地址簿地址 ; 4 返回评论地址  </param>
    /// <returns>分站的url</returns>
    public static string GetSiteurl(Hangjing.Model.ECustomerInfo model, int type)
    {
        string host = GetConfigsite();
        try
        {
            if (model == null || model.WebSite == "")
            {
                return "login.aspx";
            }
            string siteurl = host + "/userhome/";
            switch (type)
            {
                case 0:
                    siteurl = host;
                    break;
                case 1:
                    siteurl = host + "/userhome/MyOrderList.aspx";
                    break;
                case 2:
                    siteurl = host + "/userhome/MyShops.aspx";
                    break;
                case 3:
                    siteurl = host + "/userhome/MyAddress.aspx";
                    break;
                case 4:
                    siteurl = host + "/userhome/ShopCommentList.aspx";
                    break;
            }
            return siteurl;
        }
        catch
        {
            return GetConfigsite();
        }
    }

    /// <summary>
    /// 获取主站名
    /// </summary>
    /// <returns></returns>
    public static string GetMasterUrl()
    {
        return ConfigurationManager.AppSettings["Master"];
    }

    public static string GetSite(string url)
    {
        try
        {
            if (url.IndexOf("http") >= 0)
            {
                string[] temp = url.Split('.');
                string[] temp2 = temp[0].Split('/');
                return temp2[temp2.Length - 1];
            }
            else
            {
                string[] temp = url.Split('.');
                return temp[0];
            }
        }
        catch
        {
            return "hangzhou";
        }
    }

    public static string GetConfigsite()
    {
        return ConfigurationManager.AppSettings["siteurl"].ToString();
    }

    /// <summary>
    /// 获取网站名称
    /// </summary>
    /// <returns></returns>
    public static string GetWebName()
    {
        return SectionProxyData.GetSetValue(8);
    }

    /// <summary>
    /// 绑定红星
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ShowPentacleRed(object obj)
    {
        int i;
        string url = new Control().ResolveUrl("~/Images/easy_xingxing.jpg");
        System.Text.StringBuilder str = new System.Text.StringBuilder();
        if (int.TryParse(obj.ToString(), out i))
        {
            for (int j = 0; j < i; j++)
            {

                str.Append("<img src='" + url + "' />");
            }
        }
        else
        {
            str.Append("<img src='" + url + "' />");
        }
        return str.ToString();
    }

    /// <summary>
    /// 绑定灰星
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ShowPentacleBlank(object obj)
    {
        int i;
        string url = new Control().ResolveUrl("~/Images/easy_xingxing2.jpg");
        System.Text.StringBuilder str = new System.Text.StringBuilder();
        if (int.TryParse(obj.ToString(), out i))
        {
            for (int j = 0; j < i; j++)
            {

                str.Append("<img src='" + url + "' />");
            }
        }
        else
        {
            str.Append("<img src='" + url + "' />");
        }
        return str.ToString();
    }

    public static string GetGradeName(object point)
    {
        return new Hangjing.SQLServerDAL.VipGrade().GetGradeName(Convert.ToInt32(point));
    }

    /// <summary>
    /// 设置控件的选定项(CheckBoxList)
    /// </summary>
    /// <param name="ddl">要设置的控件</param>
    /// <param name="value">要选定的value值,多个值以","隔开.</param>
    public static void CheckValueS(CheckBoxList ckblist, string value)
    {
        string[] ids = value.Split(',');
        for (int j = 0; j < ids.Length; j++)
        {
            for (int i = 0; i < ckblist.Items.Count; i++)
            {
                if (ckblist.Items[i].Value == ids[j])
                {
                    ckblist.Items[i].Selected = true;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 把指定数据源绑定到控件
    /// </summary>
    public static void BindList<T>(string str_Value, string str_Text, IList<T> IL, CheckBoxList mycbList)
    {
        mycbList.DataSource = IL;
        mycbList.DataValueField = str_Value;
        mycbList.DataTextField = str_Text;
        mycbList.DataBind();
    }
    /// <summary>
    /// 把指定数据源绑定到控件
    /// </summary>
    public static void BindList<T>(string str_Value, string str_Text, IList<T> IL, HtmlSelect myDropDownList)
    {
        myDropDownList.DataSource = IL;
        myDropDownList.DataValueField = str_Value;
        myDropDownList.DataTextField = str_Text;
        myDropDownList.DataBind();
    }

    /// <summary>
    /// 获取选择的项，拼成字符串.
    /// </summary>
    /// <param name="mycbList"></param>
    /// <returns></returns>
    public static string GetcheckStr(CheckBoxList mycbList)
    {
        string rs = "";
        for (int i = 0; i < mycbList.Items.Count; i++)
        {
            if (mycbList.Items[i].Selected == true)
            {
                rs += mycbList.Items[i].Value + ",";
            }
        }
        return dellast(rs);
    }

    /// <summary>
    /// 执行语句
    /// </summary>
    /// <param name="strTableName">表名</param>
    /// <param name="strDate">要更新的SQL语句</param>
    /// <param name="strWhere">条件</param>
    /// <returns>返回是否更新成功</returns>
    /// <example>UpOnlyDate("C_Supply", "Verify=3","id=1")</example>
    public static int excutesql(string sql)
    {
        return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, null);
    }


    /// <summary>
    /// 当显示的字符串为空时，返回'-';
    /// </summary>
    /// <param name="old"></param>
    /// <returns></returns>
    public static string handlerempty(string old)
    {
        string rs = "-";
        if (old.Trim() != "")
        {
            rs = old;
        }
        return rs;
    }

    /// <summary>
    /// 去最后的符号
    /// </summary>
    /// <param name="old"></param>
    /// <returns></returns>
    public static string dellast(string old)
    {
        return System.Text.RegularExpressions.Regex.Replace(old, @",$", "");
    }

    /// <summary>
    /// 去最后的符号
    /// </summary>
    /// <param name="old"></param>
    /// <returns></returns>
    public static string dellast(string old, string c)
    {
        return System.Text.RegularExpressions.Regex.Replace(old, @"" + c + "$", "");
    }

    public static string GetRandom(int len)
    {
        char[] s = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        string str = String.Empty;
        Random random = new Random(GetRandomSeed());
        for (int i = 0; i < len; i++)
        {
            str += s[random.Next(0, s.Length)].ToString();
        }
        return str;
    }
    public static string GetRandomOnlyNum(int len)
    {
        char[] s = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        string str = String.Empty;
        Random random = new Random(GetRandomSeed());
        for (int i = 0; i < len; i++)
        {
            str += s[random.Next(0, s.Length)].ToString();
        }
        return str;
    }


    public static int GetRandomSeed()
    {
        byte[] bytes = new byte[4];
        System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
        rng.GetBytes(bytes);
        return BitConverter.ToInt32(bytes, 0);
    }

    public static void BindRepeater<T>(Repeater rpt, IList<T> list)
    {
        rpt.DataSource = list;
        rpt.DataBind();
    }

    public static string GetUrl(string src)
    {
        if (src == "")
        {
            return "";
        }
        return (new Control()).ResolveUrl(src);
    }

    /// <summary>
    /// 获取全城商家
    /// </summary>
    /// <returns></returns>
    public static string getAllCity()
    {
        return ConfigurationManager.AppSettings["all"].ToString();
    }

    /// <summary>
    /// 后台名称
    /// </summary>
    /// <returns></returns>
    public static string GetMyName()
    {
        return ConfigurationManager.AppSettings["myName"].ToString();
    }

    /// <summary>
    /// 获取选择的项，拼成字符串.把每个项拼成"{1}"的形式，便于查询
    /// </summary>
    /// <param name="mycbList"></param>
    /// <returns></returns>
    public static string GetcheckStrFix(CheckBoxList mycbList)
    {
        string rs = "";
        for (int i = 0; i < mycbList.Items.Count; i++)
        {
            if (mycbList.Items[i].Selected == true)
            {
                rs += "{" + mycbList.Items[i].Value + "}" + ",";
            }
        }
        return dellast(rs);
    }

    /// <summary>
    /// 计算两点距离(公里)
    /// </summary>
    /// <param name="lat1"></param>
    /// <param name="lng1"></param>
    /// <param name="lat2"></param>
    /// <param name="lng2"></param>
    /// <returns></returns>
    public static string getDistance(string _lat1, string _lng1, string _lat2, string _lng2)
    {
        if (_lat1 == "" || _lat2 == "")
        {
            return "--";
        }
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
        return Convert.ToDecimal(distance).ToString("#0.0");
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
    ///  0未审核，1正在处理，2已经送出，3处理完成。4失败。(用户中心显示)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static string GroupStateToStr(object x)
    {
        int d = Convert.ToInt32(x);
        string rs = "";
        switch (d)
        {
            case 0:
            case 1:
            case 2:
                rs = "处理中";
                break;
            case 3:
                rs = "处理成功";
                break;
            case 4:
                rs = "<font color='green'>处理失败</font>";
                break;
        }
        return rs;
    }


    /// <summary>
    /// 生成積點序號
    /// </summary>
    public static string CreateInve2(string userid)
    {
        string temp = string.Empty;
        System.Text.StringBuilder orderID = new System.Text.StringBuilder();
        DateTime now = System.DateTime.Now;
        orderID.Append(now.ToString("yyyyMMdd"));
        //取秒数
        temp = (now.Second + now.Minute * 60 + now.Hour * 360).ToString();
        orderID.Append(temp.PadLeft(5, '0'));
        //取随机数
        Random rand = new Random();
        temp = rand.Next(99).ToString();
        orderID.Append(temp.PadLeft(2, '0'));

        orderID.Append(userid);//加入用戶序號

        return orderID.ToString();
    }

    /// <summary>
    /// 根据配送员状态返回配送员颜色的class
    /// </summary>
    public static string GetDeliverStatus(string status)
    {
        //状态 1空闲 0 离线 -1 请假  2 繁忙
        // kongxian likai qingjia  peisong
        if (status == "1")
        {
            return "kongxian";
        }
        else if (status == "0")
        {
            return "likai";
        }
        else if (status == "-1")
        {
            return "qingjia";
        }
        else if (status == "2")
        {
            return "peisong";
        }

        return "kongxian";
    }

    public static string GetDeliverStatusByOrderNum(int ordernum)
    {
        if (ordernum == 0)
        {
            return "blue";
        }
        else
        {
            return "red";
        }

        return "";

    }

    /// <summary>
    /// 返回留言的中文显示
    /// 状态(0服务的留言  1餐品的留言  2建议的留言)
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
    public static string TurnOpinionState(object State)
    {
        string ret = "";
        switch (Convert.ToInt32(State))
        {
            case 0:
                ret = "服务的留言"; break;
            case 1:
                ret = "餐品的留言"; break;
            case 2:
                ret = "建议的留言"; break;

        }

        return ret;
    }

    /// <summary>
    /// 计算配送费
    /// 现在是分3档.0-3公里，3-4公里，4-5公里
    /// </summary>
    /// <param name="distance">距离</param>
    /// <returns></returns>
    public static int carSendMoney(decimal distance)
    {
        int sendmoney = 0;

        int money1 = Convert.ToInt32(SectionProxyData.GetSetValue(22));//0-2公里
        int money2 = Convert.ToInt32(SectionProxyData.GetSetValue(23));//2-3公里
        int money3 = Convert.ToInt32(SectionProxyData.GetSetValue(24));//3-4公里

        if (distance > 3)
        {
            sendmoney = money3;// 3-4公里
        }
        else
        {
            if (distance <= 2)
            {
                sendmoney = money1;//0-2公里
            }
            else
            {
                sendmoney = money2;//2-3公里
            }
        }
        return sendmoney;
    }

    /// <summary>
    /// 计算运费
    /// </summary>
    /// <param name="_lat1"></param>
    /// <param name="_lng1"></param>
    /// <param name="_lat2"></param>
    /// <param name="_lng2"></param>
    /// <returns></returns>
    public static int carSendMoney(string _lat1, string _lng1, string _lat2, string _lng2)
    {
        int sendmoney = 0;

        //距离
        if (_lat1 == "" || _lng1 == "" || _lat2 == "" || _lng2 == "")
        {
            sendmoney = 0;
        }
        else
        {
            decimal span = Convert.ToDecimal(WebUtility.getDistance(_lat1, _lng1, _lat2, _lng2));
            sendmoney = carSendMoney(span);
        }
        return sendmoney;
    }

    /// <summary>
    /// 直接返回值(json字符串)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonName"></param>
    /// <param name="IL"></param>
    /// <returns></returns>
    public static string ObjectToJson<T>(IList<T> IL)
    {
        StringBuilder Json = new StringBuilder();
        Json.Append("[");
        if (IL.Count > 0)
        {
            for (int i = 0; i < IL.Count; i++)
            {
                T obj = Activator.CreateInstance<T>();
                Type type = obj.GetType();
                PropertyInfo[] pis = type.GetProperties();
                Json.Append("{");

                for (int j = 0; j < pis.Length; j++)
                {
                    Json.Append("\"" + pis[j].Name.ToString() + "\":\"" + pis[j].GetValue(IL[i], null) + "\"");
                    if (j < pis.Length - 1)
                    {
                        Json.Append(",");
                    }
                }
                Json.Append("}");
                if (i < IL.Count - 1)
                {
                    Json.Append(",");
                }
            }
        }
        Json.Append("]");
        return Json.ToString();
    }


    public static string ObjectToJson<T>(string jsonName, IList<T> IL)
    {
        StringBuilder Json = new StringBuilder();
        Json.Append("{\"" + jsonName + "\":[");
        if (IL.Count > 0)
        {
            for (int i = 0; i < IL.Count; i++)
            {
                T obj = Activator.CreateInstance<T>();
                Type type = obj.GetType();
                PropertyInfo[] pis = type.GetProperties();
                Json.Append("{");

                for (int j = 0; j < pis.Length; j++)
                {
                    Json.Append("\"" + pis[j].Name.ToString() + "\":\"" + pis[j].GetValue(IL[i], null) + "\"");
                    if (j < pis.Length - 1)
                    {
                        Json.Append(",");
                    }
                }
                Json.Append("}");
                if (i < IL.Count - 1)
                {
                    Json.Append(",");
                }
            }
        }
        Json.Append("]}");
        return Json.ToString();
    }


    /// <summary>
    /// 直接返回值有参数是ilist类型的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonName"></param>
    /// <param name="IL"></param>
    /// <returns></returns>
    public static string ObjectToJson_Fix<T>(IList<T> IL)
    {
        StringBuilder Json = new StringBuilder();
        //Json.Append("[");
        if (IL.Count > 0)
        {
            //字段反射前的字段类型:System.String,System.Collections.Generic.IList`1[Hangjing.Model.FoodStyleInfo]
            string _PropertyType = "";
            for (int i = 0; i < IL.Count; i++)
            {
                T obj = Activator.CreateInstance<T>();
                Type type = obj.GetType();
                PropertyInfo[] pis = type.GetProperties();
                Json.Append("{");

                for (int j = 0; j < pis.Length; j++)
                {
                    _PropertyType = pis[j].PropertyType.ToString();
                    if (_PropertyType.IndexOf("Collections") > 0)//这个字段是一个列表
                    {
                        if (_PropertyType.IndexOf("FoodInOrderInfo") > 0)
                        {
                            //string tempjson = ObjectToJson_Fix((pis[j].PropertyType)pis[j].GetValue(IL[i], null));
                            //如果这里上面这样的写法可以成立，那这个方法就很有用了。
                            string tempjson = ObjectToJson_Fix2((IList<Hangjing.Model.FoodInOrderInfo>)pis[j].GetValue(IL[i], null));
                            Json.Append("\"" + pis[j].Name.ToString() + "\":" + tempjson + "");
                        }
                    }
                    else
                    {
                        Json.Append("\"" + pis[j].Name.ToString() + "\":\"" + pis[j].GetValue(IL[i], null) + "\"");
                    }
                    if (j < pis.Length - 1)
                    {
                        Json.Append(",");
                    }
                }
                Json.Append("}");
                if (i < IL.Count - 1)
                {
                    Json.Append(",");
                }
            }
        }
        //Json.Append("]");
        return delMore(Json.ToString());
    }

    /// <summary>
    /// 直接返回值有参数是ilist类型的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonName"></param>
    /// <param name="IL"></param>
    /// <returns></returns>
    public static string ObjectToJson_Fix2<T>(IList<T> IL)
    {
        StringBuilder Json = new StringBuilder();
        Json.Append("[");
        if (IL.Count > 0)
        {
            //字段反射前的字段类型:System.String,System.Collections.Generic.IList`1[Hangjing.Model.FoodStyleInfo]
            string _PropertyType = "";
            for (int i = 0; i < IL.Count; i++)
            {
                T obj = Activator.CreateInstance<T>();
                Type type = obj.GetType();
                PropertyInfo[] pis = type.GetProperties();
                Json.Append("{");

                for (int j = 0; j < pis.Length; j++)
                {
                    _PropertyType = pis[j].PropertyType.ToString();
                    if (_PropertyType.IndexOf("Collections") > 0)//这个字段是一个列表
                    {
                        if (_PropertyType.IndexOf("FoodInOrderInfo") > 0)
                        {
                            //string tempjson = ObjectToJson_Fix((pis[j].PropertyType)pis[j].GetValue(IL[i], null));
                            //如果这里上面这样的写法可以成立，那这个方法就很有用了。
                            string tempjson = ObjectToJson_Fix((IList<Hangjing.Model.FoodInOrderInfo>)pis[j].GetValue(IL[i], null));
                            Json.Append("\"" + pis[j].Name.ToString() + "\":" + tempjson + "");
                        }
                    }
                    else
                    {
                        Json.Append("\"" + pis[j].Name.ToString() + "\":\"" + pis[j].GetValue(IL[i], null) + "\"");
                    }
                    if (j < pis.Length - 1)
                    {
                        Json.Append(",");
                    }
                }
                Json.Append("}");
                if (i < IL.Count - 1)
                {
                    Json.Append(",");
                }
            }
        }
        Json.Append("]");
        return Json.ToString();
    }

    /// <summary>
    /// 获取配置信息(key)
    /// </summary>
    /// <returns></returns>
    public static string GetConfigKey(string key)
    {
        return ConfigurationManager.AppSettings[key].ToString();
    }

    /// <summary>
    /// 根据商家输入url(连锁店要分转到列表页面)
    /// </summary>
    /// <returns></returns>
    public static string getshopurl(object dataid, object grade)
    {
        string url = "ShowTogo.aspx?id=" + dataid;
        return url;
    }


    /// <summary>
    /// 获取人人网chientid
    /// </summary>
    /// <returns></returns>
    public static string getRRClienID()
    {
        return ConfigurationManager.AppSettings["rrappid"].ToString();
    }

    /// <summary>
    /// 通过返回的用户名，编号，转到登录处理的页面
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="uname"></param>
    /// <param name="utype"></param>
    /// <returns></returns>
    public static void getRequestUrl(string uid, string uname, string utype)
    {
        string url = "~/account/ThirdpartyLogin.aspx";
        url += "?uid=" + uid + "&uname=" + HttpContext.Current.Server.UrlEncode(uname) + "&utype=" + utype;
        HttpContext.Current.Response.Redirect(url);
    }

    /// <summary>
    /// 处理json特殊字符(变成中文输入法下的)
    /// </summary>
    /// <param name="sorce"></param>
    /// <returns></returns>
    public static string FileterJson(string sorce)
    {
        string newstr = "";
        Dictionary<Regex, string> list = new Dictionary<Regex, string>();
        list.Add(new Regex("\\,+"), "，");
        list.Add(new Regex("\\:+"), "：");
        list.Add(new Regex("\'"), "‘");
        list.Add(new Regex("\""), "“");
        list.Add(new Regex("\n"), "");
        list.Add(new Regex("\r"), "");
        list.Add(new Regex("\b"), "");
        newstr = sorce;
        foreach (var item in list)
        {
            newstr = item.Key.Replace(newstr, item.Value);
        }

        return newstr;
    }

    /// <summary>
    /// 去第一个的符号
    /// </summary>
    /// <param name="old"></param>
    /// <returns></returns>
    public static string delfirst(string old)
    {
        return System.Text.RegularExpressions.Regex.Replace(old, @"^,", "");
    }

    /// <summary>
    /// 把多个字符转换成一个
    /// </summary>
    /// <param name="old"></param>
    /// <returns></returns>
    public static string delMore(string old)
    {
        Regex r = new Regex("\\,+");
        return r.Replace(old, ",");
    }

    /// <summary>
    /// 处理建筑物字符串
    /// </summary>
    /// <param name="old"></param>
    /// <returns></returns>
    public static string OpBuildstr(string buildstr)
    {
        return delfirst(dellast(delMore(delBrackets(buildstr))));
    }

    /// <summary>
    /// 传入{1},{2}..的字符串,过滤其中的括号
    /// </summary>
    /// <param name="mycbList"></param>
    /// <returns></returns>
    public static string delBrackets(string mycbList)
    {
        string tempcat = mycbList.Replace("{", "").Replace("}", "");
        return tempcat;
    }

    /// <summary>
    /// 获取选择的项，拼成字符串.把每个项拼成"{1}"的形式，便于查询
    /// </summary>
    /// <param name="mycbList"></param>
    /// <returns></returns>
    public static string GetStrAdd(string str)
    {
        string rs = "";

        foreach (string item in str.Split(','))
        {
            rs += "{" + item + "}" + ",";
        }
        return dellast(rs);
    }

    /// <summary>
    /// 获取API提交的参数[get方式的参数]
    /// </summary>
    /// <param name="request">request对象</param>
    /// <returns>参数数组</returns>
    public static Param[] GetParamsFromRequest(HttpRequest request)
    {
        List<Param> list = new List<Param>();
        foreach (string key in request.QueryString.AllKeys)
        {
            list.Add(Param.Create(key, request.QueryString[key]));
        }

        list.Sort();
        return list.ToArray();
    }

    public static string GetSHA1(string password)
    {
        string shh1string = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
        return shh1string;
    }

    public static string GetPageString(int page, int pagecount, string url)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<a href=\"" + url + "\" >首页</a>");
        if (page == 1)
        {
            sb.Append("<a href=\"javascript:;\"  class='cul' style='color:#fff'>上一页</a> ");
        }
        else
        {
            sb.Append("<a data-ajax='false' href=\"" + url + "&PageNo=" + (page - 1) + "\" >上一页</a> ");
        }
        if (page >= pagecount)
        {
            sb.Append("<a href=\"javascript:;\"  class='cul' style='color:#fff'>下一页</a>");
        }
        else
        {
            sb.Append("<a href=\"" + url + "&PageNo=" + (page + 1) + "\" >下一页</a>");
        }

        sb.Append("<a  href=\"" + url + "&PageNo=" + pagecount + "\" >尾页</a>");
        return sb.ToString();
    }
    /// <summary>
    /// 获取cookie,没有就生成 
    /// </summary>
    /// <param name="mycbList"></param>
    /// <returns></returns>
    public static string getUserCookie()
    {
        string uc = WebUtility.FixgetCookie("openid");
        if (uc == null || uc == "")
        {
            uc = Guid.NewGuid().ToString();
        }
        WebUtility.FixsetCookie("openid", uc, 365);

        return uc;
    }

    /// <summary>
    /// 用户绑定的编号,没的返回空
    /// </summary>
    /// <param name="mycbList"></param>
    /// <returns></returns>
    public static string getBingding_Uid()
    {
        string uc = WebUtility.FixgetCookie("binding_uid");
        if (uc == null || uc == "")
        {
            uc = "";
        }
        return uc;
    }

    /// <summary>
    /// 根据定位的地址，获取城市编号
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public static int getCityIDByAddress(string address)
    {
        int cityid = 0;
        IList<CityInfo> citys = CacheHelper.GetCityList();
        foreach (var item in citys)
        {
            if (address.IndexOf(item.cname) >= 0)
            {
                cityid = item.cid;
                WebUtility.FixsetCookie("user_cityid", cityid + "", 365);
                break;
            }
        }

        return cityid;
    }
}
