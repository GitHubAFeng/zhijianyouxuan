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
using Hangjing.Model;
using System.Net;
using System.ComponentModel;
using System.Net.Http;

/// <summary>
///WebUtility 的摘要说明
/// </summary>
public static class WebUtility
{
    public static string GetStylePacket(string State)
    {
        string ret = "注册获得红包";

        if (State == "0")
        {
            ret = "注册获得红包";
        }
        else if (State == "1")
        {
            ret = "下单获得红包";
        }
        else if (State == "2")
        {
            ret = "网站推送获得红包";
        }

        return ret;
    }
    public static string GetStyleByPacket(string State)
    {
        string ret = "固定";

        if (State == "0")
        {
            ret = "固定";
        }
        else if (State == "1")
        {
            ret = "随机";
        }

        return ret;
    }


    public static HttpResponseMessage toJson(Object obj)
    {
        String str;
        if (obj is String || obj is Char)
        {
            str = obj.ToString();
        }
        else
        {
            str = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
        return result;
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
    /// 取出用户输入的危险字符,返回decimal
    /// </summary>
    public static decimal InputText(string text, char c)
    {
        return Convert.ToDecimal(InputText(text));
    }

    /// <summary>
    /// 取出用户输入的危险字符,返回int
    /// </summary>
    public static int InputText(string text, bool x)
    {
        text = text.Trim();
        text = Regex.Replace(text, "[\\s]{2,}", " ");
        text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");
        text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");
        text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);
        text = text.Replace("'", "''");
        text = NoHTML(text);

        return Convert.ToInt32(text);
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
        int olen = len;
        int max = 2 * len;
        char[] temp = str.ToCharArray();
        string ss = string.Empty;
        if (len < temp.Length)
        {
            for (int i = 0; i < len && i < max && i < str.Length; i++)
            {
                ss = ss + temp[i].ToString();
                int code = Convert.ToInt32(temp[i]);
                if (code < 255)//汉字加2个
                {
                    len++;
                }
                if (code > 1000)
                {
                    max -= 1;
                }

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
        string ret = (new Control()).ResolveUrl("~/images/nopic_02.jpg");
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

    public static string GetUrl(string src)
    {
        if (src == "")
        {
            return "";
        }
        return (new Control()).ResolveUrl(src);
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
        string url = new Control().ResolveUrl("~/user/Images/easy_xingxing.gif");
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
    /// 把指定数据源绑定到控件
    /// </summary>
    public static void BindList<T>(string str_Value, string str_Text, IList<T> IL, RadioButtonList myDropDownList)
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
        return SectionProxyData.GetSetValue(63);
    }

    /// <summary>
    /// 获取Description
    /// </summary>
    /// <returns></returns>
    public static string GetDescription()
    {
        return SectionProxyData.GetSetValue(64);
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
        string str = DateTime.Now.ToString();
        string s3 = null;
        if (str.Contains("-") || str.Contains(" ") || str.Contains(":"))
        {
            string s1 = str.Replace("-", "");
            string s2 = s1.Replace(" ", "");
            s3 = s2.Replace(":", "");
        }
        return s3;
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
    /// 读取Cookies
    /// </summary >
    /// <param   name="strName" >Cookie 主键 </param >
    /// <code >Cookie ck = new Cookie(); </code >
    /// <code >ck.getCookie("主键"); </code >
    public static string FixgetCookie(string strName)
    {
        HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[strName];
        if (Cookie != null && Cookie.Value != null)
        {
            return Cookie.Value.ToString();
        }
        else
        {
            return "";
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
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //设置高质量,低速度呈现平滑程度
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

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
    /// 根据星级返回显示的星级图片
    /// </summary>
    /// <param name="ReviewTimes"></param>
    /// <returns></returns>
    public static string GetStartImages(string ReviewTimes)
    {
        int Times = Convert.ToInt32(ReviewTimes);
        string ret = "";

        for (int i = 0; i < Times; i++)
        {
            ret += "<img src=\"images/sprite1.jpg\" />";
        }

        for (int j = 0; j < 5 - Times; j++)
        {
            ret += "<img src=\"images/sprite.jpg\" />";
        }

        return ret;
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
    /// 向Page对象注册脚本
    /// </summary>
    public static void RegScript(Page page, string script)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "ShowWindow", "<script type='text/javascript'>" + script + "</script>");
    }
    /// <summary>
    /// 向页面注册脚本
    /// </summary>
    public static void RegScript(Page page, Control control, string script)
    {
        ScriptManager.RegisterStartupScript(control, page.GetType(), "UpdatePanel", script, true);
    }

    /// <summary>
    /// 设置控件的选定项(CheckBoxList)
    /// </summary>
    /// <param name="ddl">要设置的控件</param>
    /// <param name="value">要选定的value值,多个值以","隔开.</param>
    public static void CheckValueS(CheckBoxList ckblist, string value)
    {
        string[] ids = delBrackets(value).Split(',');
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
    /// 网站标题后面部分
    /// </summary>
    /// <returns></returns>
    public static string GetWebName()
    {
        return SectionProxyData.GetSetValue(3);
    }

    public static string getUrl(Hangjing.Model.ECustomerInfo s)
    {
        return "";
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
        char[] s = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', };
        string str = string.Empty;
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

    /// <summary>
    /// 绑定repeater
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="rpt"></param>
    /// <param name="list"></param>
    public static void BindRepeater<T>(Repeater rpt, IList<T> list)
    {
        rpt.DataSource = list;
        rpt.DataBind();
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
    /// 返回订单状态的中文显示
    /// 状态(1：新增订单/2：正在打印/3：处理成功/4：处理失败/5:订单已经取消/6:订单已经失效(打印机获取后未反馈打印结果))
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
    public static string TurnOrderState(object _State)
    {
        int state = Convert.ToInt32(_State);
        if (state == 3)
        {
            return "已完成";
        }
        else
        {
            return "未完成";
        }
        //return new OrderState().TurnOrderState(_State);
    }
        /// <summary>
    /// 返回订单状态的中文显示
    /// 状态(1：新增订单/2：正在打印/3：处理成功/4：处理失败/5:订单已经取消/6:订单已经失效(打印机获取后未反馈打印结果))
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
    public static string UserOrderState(string paystate, string hurhav, string OrderStatus, string OrderChecker)
    {
        string rs = "订单已提交";

        if (paystate == "1")
        {
            rs = "用户已支付";
        }
        if (hurhav == "1")
        {
            rs = "催单";
        }
        if (OrderStatus == "8")
        {
            rs = "申请退款";
        }
        if (OrderChecker == "1")
        {
            rs = "确认收货";
        }

        return rs;
    }
    

    /// <summary>
    /// 已经提交的订单状态
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string TurnIoCacheOrderState(object id)
    {
        return TurnOrderState(id);
    }

    /// <summary>
    /// 绑定灰星
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ShowPentacleBlank(object obj)
    {
        int i;
        string url = new Control().ResolveUrl("~/Images/easy_xingxing2.gif");
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
    /// 转换商家合作分类
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string GetShopType(object s)
    {
        string d = s.ToString();
        string rs = "";
        switch (d)
        {
            case "1":
                rs = "签约";
                break;
            case "2":
                rs = "非签约有菜单";
                break;
            case "3":
                rs = "非签约无菜单";
                break;
        }
        return rs;
    }
    /// <summary>
    /// 是否在时间段内
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    public static bool isGood(string start, string end, string now)
    {
        bool bb = false;
        try
        {
            DateTime Jstart = Convert.ToDateTime(start);
            DateTime Jend = Convert.ToDateTime(end);
            DateTime nows = Convert.ToDateTime(now);
            int r1 = DateTime.Compare(Jstart, nows);
            int r2 = DateTime.Compare(nows, Jend);

            if (r1 < 0 && r2 < 0)
            {
                bb = true;
            }
        }
        catch (Exception ex)
        {
            return false;
        }

        return bb;
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
                    rs = "已打烊-Close";
                }
                else
                {
                    rs = "营业中-Open";
                }

                break;
            case "0":
                rs = "暂停营业";
                break;
        }
        return rs;
    }

    public static string GetShopStatusindex(object s, object timee)
    {
        string d = s.ToString();
        string rs = "";

        switch (d)
        {
            case "1":
                if (timee.ToString() == "0")
                {
                    rs = "<span class=\"close\">暂停营业</span>";
                }
                else
                {
                    rs = "<span class=\"open\">正常营业</span>";
                }

                break;
            case "0":
                rs = "<span class=\"close\">暂停营业</span>";
                break;

        }
        return rs;
    }
    /// <summary>
    /// 解析名称
    /// </summary>
    /// <param name="oldname"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public static string parsename(object oldname, object level)
    {
        string temp = oldname.ToString();
        string[] names = temp.Split('/');

        return names[names.Length - 1];
    }

    /// <summary>
    /// 解析名称拼音
    /// </summary>
    /// <param name="oldname"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public static string parsenamepy(object oldnamepy, object level)
    {
        string temp = oldnamepy.ToString();
        string[] names = temp.Split('/');

        return names[names.Length - 1];
    }

    public static string UTF8ToGB2312(string str)
    {
        try
        {
            Encoding utf8 = Encoding.GetEncoding(65001);
            Encoding gb2312 = Encoding.GetEncoding("gb2312");//Encoding.Default ,936
            byte[] temp = utf8.GetBytes(str);
            byte[] temp1 = Encoding.Convert(utf8, gb2312, temp);
            string result = gb2312.GetString(temp1);
            return result;
        }
        catch (Exception ex)//(UnsupportedEncodingException ex)
        {
            return null;
        }
    }
    public static string GB2312ToUTF8(string str)
    {
        try
        {
            Encoding uft8 = Encoding.GetEncoding(65001);
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            byte[] temp = gb2312.GetBytes(str);

            for (int i = 0; i < temp.Length; i++)
            {

            }
            byte[] temp1 = Encoding.Convert(gb2312, uft8, temp);

            for (int i = 0; i < temp1.Length; i++)
            {

            }
            string result = uft8.GetString(temp1);
            return result;
        }
        catch (Exception ex)//(UnsupportedEncodingException ex)
        {

            return null;
        }
    }

    /// <summary>
    /// 判断当前商家是否营业
    /// </summary>
    /// <param name="isbuness">在营业时间内是1</param>
    /// <param name="status">商家状态.(主要有打印机商家 , 有打印机状态)</param>
    /// <returns>1 表示正在营业 , 0表示歇业</returns>
    public static string setStatus(object isbuness, object status)
    {
        string rs = "休息中";
        if (status.ToString() == "0")
        {
            return rs;
        }
        if (isbuness.ToString() == "0")
        {
            return rs;
        }
        rs = "营业中";
        return rs;
    }

    /// <summary>
    /// 判断当前商家是否营业
    /// </summary>
    /// <param name="isbuness">在营业时间内是1</param>
    /// <param name="status">商家状态.(主要有打印机商家 , 有打印机状态)</param>
    /// <returns>1 表示正在营业 , 0表示歇业</returns>
    public static int setStatus_fix(object isbuness, object status)
    {
        int rs = 0;
        if (status.ToString() == "0")
        {
            return rs;
        }
        if (isbuness.ToString() == "0")
        {
            return rs;
        }
        rs = 1;
        return rs;
    }

    /// <summary>
    /// 访问权限判断,无权限到
    /// </summary>
    /// <param name="model">当前登录用户</param>
    /// <param name="Permission">需要要的权限</param>
    public static void checkAccess(Hangjing.Model.EAdminInfo model, string path)
    {
        int flag = 0;
        if (model != null)
        {
            if (Convert.ToInt32(model.Permission.Trim()) == 1)
            {
                flag = 1;
            }
            else
            {
                string filename = getPathFile();
                //能访问的文件夹
                IList<string> folderlist = new List<string>();
                folderlist.Add("updateorderlist.aspx");
                folderlist.Add("orderlist.aspx");
                folderlist.Add("orderdetail.aspx");
                folderlist.Add("searchorder.aspx");
                folderlist.Add("webstatisticsyear.aspx");
                foreach (string item in folderlist)
                {
                    if (path.IndexOf(item) > 0)
                    {
                        flag = 1;
                        break;
                    }
                }
            }
            if (flag > 0)
            {
                //可访问
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Admin/basic.aspx?msg=12");
            }
        }
        else
        {
            HttpContext.Current.Response.Redirect("~/Admin/login.aspx");
        }
    }

    /// <summary>
    ///  反回路径文件名(小写)
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
    public static string getPathFile()
    {
        string path = HttpContext.Current.Request.Url.PathAndQuery.ToLower();
        string[] infos = path.ToLower().Split('/');
        string filename = infos[infos.Length - 1];
        return filename.Split('?')[0];
    }

    public static string GetConfigsite()
    {
        return ConfigurationManager.AppSettings["siteurl"].ToString();
    }

    /// <summary>
    /// 设置当前城市DropDownList
    /// </summary>
    /// <param name="DDLArea"></param>
    public static void SetDDLCity(DropDownList DDLArea)
    {
        WebUtility.BindList("cid", "cname", SectionProxyData.GetCityList(), DDLArea);
    }

    /// <summary>
    /// 获取城市选择
    /// </summary>
    /// <returns></returns>
    public static string get_userCityid()
    {
        string cityid = WebUtility.FixgetCookie("user_cityid");
        if (cityid != "" && cityid != null)
        {
            WebUtility.FixsetCookie("user_cityid", cityid, 365);
        }
        else
        {
            cityid = "0";
        }
        return cityid;
    }

    /// <summary>
    /// 返回用户选择的城市名称
    /// </summary>
    /// <returns></returns>
    public static string get_userCityName()
    {
        string cityid = get_userCityid();
        string cityname = "";

        IList<CityInfo> citylist = SectionProxyData.GetCityList();
        foreach (var item in citylist)
        {
            if (item.cid == Convert.ToInt32(cityid))
            {
                cityname = item.cname;
                break;
            }
        }

        return cityname;
    }

    /// <summary>
    /// 操作权限判断,返回0表示不能操作，1表示可以操作 1,2,3,4分别表示：查，增，修改，删除
    /// </summary>
    /// <param name="model">当前登录用户</param>
    /// <param name="type">1,2,3,4分别表示：查，增，修改，删除</param>
    /// <param name="Permission">需要要的权限</param>
    public static int checkOperator(int type)
    {
        Hangjing.Model.EAdminInfo model = UserHelp.GetAdmin();
        int rs = 0;
        if (model != null)
        {
            if (model.Permission == "0")
            {
                string filename = GetUrlFileName();
                IList<sys_RolePermissionInfo> rplist = SectionProxyData.GetRolePermissions(model.Rem);
                int p_value = 0;
                //根据文件名获取当前所在的模块(一个模块包含涉及的页面，可能是多个)
                System.Globalization.CompareInfo Compare = System.Globalization.CultureInfo.InvariantCulture.CompareInfo;
                foreach (sys_RolePermissionInfo item in rplist)
                {
                    int i = Compare.IndexOf(item.des, filename, System.Globalization.CompareOptions.IgnoreCase);
                    if (item.des.IndexOf(filename) >= 0)
                    {
                        p_value = item.P_Value;
                        return 1;// 这里只判断访问权限
                        break;
                    }
                }
                if (p_value > 0)
                {
                    //此模块的权限值与当前操作 查(2)，增(4)，修改(8)，删除(16)按位与，所得结果和操作的值一样，说明有这个操作权限
                    int cvalue = Convert.ToInt32(Math.Pow(2, type));
                    if ((p_value & cvalue) == cvalue)
                    {
                        rs = 1;
                    }
                }
            }
            else
            {
                rs = 1;
            }
        }
        if (rs == 0)
        {
            if (type == 1)
            {
                HttpContext.Current.Response.Redirect("~/Admin/basic.aspx?msg=1");
            }
        }
        return rs;
    }

    /// <summary>
    /// 操作权限判断,返回0表示不能操作，1表示可以操作 1,2,3,4分别表示：查，增，修改，删除
    /// </summary>
    /// <param name="model">当前登录用户</param>
    /// <param name="type">1,2,3,4分别表示：查，增，修改，删除</param>
    /// <param name="Permission">需要要的权限</param>
    public static int AreaAdmin_checkOperator(int type)
    {
        Hangjing.Model.EAdminInfo model = UserHelp.GetAdmin();
        int rs = 0;
        if (model != null)
        {
            if (model.Permission == "0")
            {
                string filename = GetUrlFileName();
                IList<sys_RolePermissionInfo> rplist = SectionProxyData.GetRolePermissions(model.Rem);
                int p_value = 0;
                //根据文件名获取当前所在的模块(一个模块包含涉及的页面，可能是多个)
                System.Globalization.CompareInfo Compare = System.Globalization.CultureInfo.InvariantCulture.CompareInfo;
                foreach (sys_RolePermissionInfo item in rplist)
                {
                    int i = Compare.IndexOf(item.des, filename, System.Globalization.CompareOptions.IgnoreCase);
                    if (item.des.IndexOf(filename) >= 0)
                    {
                        p_value = item.P_Value;
                        return 1;// 这里只判断访问权限
                        break;
                    }
                }
                if (p_value > 0)
                {
                    //此模块的权限值与当前操作 查(2)，增(4)，修改(8)，删除(16)按位与，所得结果和操作的值一样，说明有这个操作权限
                    int cvalue = Convert.ToInt32(Math.Pow(2, type));
                    if ((p_value & cvalue) == cvalue)
                    {
                        rs = 1;
                    }
                }
            }
            else
            {
                rs = 1;
            }
        }
        if (rs == 0)
        {
            if (type == 1)
            {
                HttpContext.Current.Response.Redirect("~/AreaAdmin/basic.aspx?msg=1");
            }
        }
        return rs;
    }

    /// <summary>
    /// 获取当前url的页面名（不包含参数）
    /// </summary>
    /// <returns></returns>
    public static string GetUrlFileName()
    {
        string path = HttpContext.Current.Request.Url.PathAndQuery.Split('?')[0];
        string filename = path.Substring(path.LastIndexOf('/') + 1);
        return filename;
    }

    /// <summary>
    /// 根据当前管理的权限，返回sql语句。主要是区分总管理员cityid = 0 ,用cookie["admin_cityid"];
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public static string GetSql(string where)
    {
        if (where == "")
        {
            where = " 1=1";
        }

        EAdminInfo admin = UserHelp.GetAdmin();
        if (admin.CityID > 0)
        {
            where += " and CityID=" + admin.CityID + "  ";
        }
        return where;
    }

    /// <summary>
    /// Custorder
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public static string GetOrderSql(string where)
    {
        if (where == "")
        {
            where = " 1=1";
        }

        EAdminInfo admin = UserHelp.GetAdmin();
        if (admin.CityID > 0)
        {
            where += " and custorder.cityid=" + admin.CityID + "  ";
        }
        return where;
    }

    /// <summary>
    /// Custorder
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public static string GetCustorderSql(string where)
    {
        if (where == "")
        {
            where = " 1=1";
        }

        EAdminInfo admin = UserHelp.GetAdmin();
        if (admin.CityID > 0)
        {
            where += " and cityid=" + admin.CityID + "  ";
        }
        return where;
    }

    /// <summary>
    /// City
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public static string GetCitySql(string where)
    {
        if (where == "")
        {
            where = " 1=1";
        }

        EAdminInfo admin = UserHelp.GetAdmin();
        if (admin.CityID > 0)
        {
            where += " and cid=" + admin.CityID + "  ";
        }
        return where;
    }

    /// <summary>
    /// ExpressOrder
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public static string GetExpressOrderSql(string where)
    {
        if (where == "")
        {
            where = " 1=1";
        }

        EAdminInfo admin = UserHelp.GetAdmin();
        if (admin.CityID > 0)
        {
            where += " and Cityid=" + admin.CityID + "  ";
        }
        return where;
    }


    /// <summary>
    /// 根据当前登录的分单员，配送点的编号，添加查询条件
    /// </summary>
    /// <param name="where">当前条件</param>
    /// <param name="param">配送点在当前表的名称</param>
    /// <returns></returns>
    public static string GetDeliverSql(string where, string param)
    {
        string temp = where;
        if (temp == "")
        {
            temp = " 1= 1";
        }
        string sendsiteid = FixgetCookie("deliver_sendsiteid");
        if (sendsiteid != null && sendsiteid != "" && sendsiteid != "0")
        {
            temp += " and " + param + " = " + sendsiteid + "  ";
        }
        return temp;
    }

    /// <summary>
    /// 配送状态：0：未处理，1，取货中，2：配送中，3：配送完成， 4：配送失败
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
    public static string TurnOrderSendState(string State, string ShopSet)
    {
        string ret = "";
        switch (Convert.ToInt32(State))
        {
            case 0:
                if (ShopSet == "0")
                {
                    ret = "待发起配送";
                }
                else
                {
                    ret = "待骑手接单";
                }
                break;
            case 1:
                ret = "待骑手取货"; break;
            case 2:
                ret = "骑手已取货"; break;
            case 3:
                ret = "骑手已送达"; break;
            case 4:
                ret = "配送已取消"; break;
        }

        return ret;
    }
    /// <summary>
    /// 商家处理状态（取消订单申请）0未处理 1同意2拒绝
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
    public static string ShopState(string State)
    {
        string ret = "";
        switch (State)
        {
            case "0":
                ret = "未处理";
                break;
            case "1":
                ret = "已同意"; break;
            case "2":
                ret = "已拒绝"; break;
        }

        return ret;
    }

    /// <summary>
    /// 催单状态（0 未处理 1已处理）
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
    public static string HurryState(string State)
    {
        string ret = "";
        switch (State)
        {
            case "0":
                ret = "未处理";
                break;
            case "1":
                ret = "已处理"; break;
        }

        return ret;
    }

    /// <summary>
    /// 打印状态：999表示没有打印机商家的， 0：等待打印;1:已打印;2请求失败;3:请求已发送;-1:IP地址不允许;-2:关键参数为空或请求方式不对;-3:客户编码不正确;-4:安全校验码不正确;-5:请求时间失效;
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
    public static string TurnOrderPrintState(string State)
    {
        string ret = "";
        switch (Convert.ToInt32(State))
        {
            case 999:
                ret = "无打印机"; break;
            case 0:
                ret = "等待打印"; break;
            case 1:
                ret = "已打印"; break;
            case 2:
                ret = "请求失败"; break;
            case 3:
                ret = "请求已发送"; break;
            case -1:
                ret = "IP地址不允许"; break;
            case -2:
                ret = "关键参数为空或请求方式不对"; break;
            case -3:
                ret = "客户编码不正确"; break;
            case -4:
                ret = "安全校验码不正确"; break;
            case -5:
                ret = "请求时间失效"; break;
        }

        return ret;
    }

    /// <summary>
    ///  //1支付宝/2银联/3账户余额/4货到付款
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
            default:
                ret = "未支付"; break;

        }

        return ret;
    }


    /// <summary>
    /// 五分鐘內未更新則視為離線
    /// </summary>
    /// <param name="lastlogintime"></param>
    /// <returns></returns>
    public static string GetPrinterStatus(string lastlogintime)
    {
        TimeSpan ts1 = new TimeSpan(Convert.ToDateTime(lastlogintime).Ticks);
        TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
        TimeSpan ts = ts1.Subtract(ts2).Duration();
        double minu = Math.Abs(ts.TotalMinutes);

        if (minu < 5)
        {
            return "在线";
        }
        else
        {
            return "<font color='red'>离线</font>";
        }
    }

    /// <summary>
    /// 获取网站配置信息
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetConfigKey(string key)
    {
        return System.Configuration.ConfigurationManager.AppSettings[key].ToString();
    }

    /// <summary>
    /// 根据配送员状态返回配送员颜色的class
    /// </summary>
    public static string GetDeliverStatus(string status)
    {
        if (status == "1")
        {
            return "kongxian";
        }
        else if (status == "0")
        {
            return "likai";
        }

        return "kongxian";
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
    /// 产生随机字符串
    /// </summary>
    /// <param name="len">字符串长度</param>
    /// <returns>随机字符串</returns>
    public static string RandNun(int len)
    {
        char[] s = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        string str = String.Empty;
        Random random = new Random();
        for (int i = 0; i < len; i++)
        {
            str += s[random.Next(0, s.Length)].ToString();
        }
        return str;
    }

    /// <summary>
    /// 返回订单来源的中文显示
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
    public static string TurnOrderSource(object State)
    {
        string ret = "";
        foreach (var item in typeof(OrderSource).GetItems())
        {
            if (Convert.ToInt32(State) == Convert.ToInt32(item.Value))
            {
                ret = item.Text; break;
            }
        }

        return ret;
    }

    /// <summary>
    /// 返回跑腿订单状态的中文显示 订单状态： 0 新增;1 配送中;3 成功 ;6 取消,2：已经调度
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
    public static string TurnExpressOrderState(object State)
    {
        string ret = "";
        switch (Convert.ToInt32(State))
        {
            case 0:
                ret = "新增"; break;
            case 1:
                ret = "已经调度"; break;
            case 2:
                ret = "取货中"; break;
            case 3:
                ret = "成功"; break;
            case 4:
                ret = "配送中"; break;
            case 5:
                ret = "取消"; break;
            case 6:
                ret = "失败"; break;
        }

        return ret;
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

    /// <summary>
    /// 根据传入的时间,判断是否有编辑后（时间字段添加时默认：1970-1-1），
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="defautstr">时间未编辑时，返回默认</param>
    /// <param name="prestr">显示的时间前缀文字</param>
    /// <returns></returns>
    public static string GetTimeByDefatu(DateTime time, string defautstr, string prestr)
    {
        string rs = "";
        if (time < Convert.ToDateTime("2000-1-1"))
        {
            rs = defautstr;
        }
        else
        {
            rs = prestr + time.ToString();
        }
        return rs;
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
    public static string DelMarks(string sorce)
    {
        string newstr = "";
        Dictionary<Regex, string> list = new Dictionary<Regex, string>();
        list.Add(new Regex("\'"), "");
        list.Add(new Regex("\""), "");
        newstr = sorce;
        foreach (var item in list)
        {
            newstr = item.Key.Replace(newstr, item.Value);
        }

        return newstr;
    }

    /// <summary>
    /// 返回解码后的这字
    /// </summary>
    /// <param name="ostr"></param>
    /// <returns></returns>
    public static string UrlDecode(string ostr)
    {
        return HttpContext.Current.Server.UrlDecode(ostr);
    }

    /// <summary>
    /// 检查是否定位，没有转到首页
    /// </summary>
    public static void checkLocal()
    {
        string mylat = WebUtility.FixgetCookie("mylat");
        if (mylat == "")
        {
            HttpContext.Current.Response.Redirect("index.aspx?noaddress=1");
        }
    }

    /// <summary>
    /// 是否同步登录，注册
    /// true表示是，
    /// false表示不同步
    /// </summary>
    /// <returns></returns>
    public static bool isSyn()
    {
        return SectionProxyData.GetSetValue(44).Trim() == "1" ? true : false;
    }

    /// <summary>
    /// 充值方式
    /// </summary>
    /// <param name="paytype"></param>
    /// <returns></returns>
    public static string Recharge(string paytype)
    {
        return Hangjing.WebCommon.WebHelper.Recharge(paytype);
    }

    /// <summary>
    /// 返回订单状态的中文显示
    /// 状态 0 未支付 1 成功 -1支付失败
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
    public static string TurnPayState(object State)
    {
        return TurnPayState(State, 0);
    }


    /// <summary>
    /// 返回订单状态的中文显示[有颜色的]
    /// 状态 0 未支付 1 成功 -1支付失败
    /// </summary>
    /// <param name="State"></param>
    /// <param name="hascolor">1表示有颜色，0表示没有（用于接口）</param>
    /// <returns></returns>
    public static string TurnPayState(object State, int hascolor)
    {
        string ret = "";
        switch (Convert.ToInt32(State))
        {
            case 0:
                ret = "<font color='red'>未支付</font>"; break;
            case 1:
                ret = "<font color='green'>成功</font>"; break;
        }
        if (hascolor == 0)
        {
            ret = WebUtility.NoHTML(ret);
        }

        return ret;
    }


    /// <summary >
    /// 创建Cookies,过期时间为分钟
    /// </summary >
    /// <param   name="strName" >Cookie主键 </param >
    /// <param   name="strValue" >Cookie键值 </param >
    /// <param   name="strDay" >Cookie天数 </param >
    /// <code >Cookie ck = new Cookie(); </code >
    /// <code >ck.setCookie("主键","键值","分数"); </code >
    public static bool FixsetCookie_Minutes(string strName, string strValue, int minutes)
    {
        try
        {
            HttpCookie Cookie = new HttpCookie(strName);
            Cookie.Expires = DateTime.Now.AddMinutes(minutes);
            Cookie.Value = strValue;
            System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 根据订单生成要发送的短信字符串。
    /// </summary>
    /// <returns></returns>
    public static string getOrderInfo(Hangjing.Model.CustorderInfo info)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("订单号：" + info.orderid + " ，送货时间：" + info.SendTime + "，商家：" + info.TogoName + " 商品：");

        IList<FoodlistInfo> food_list = new Hangjing.SQLServerDAL.Foodlist().GetAllByOrderID(info.orderid);

        for (int i = 0; i < food_list.Count; i++)
        {
            string temp = food_list[i].FoodName;
            int count = 20 - temp.Length;
            if (count > 0)
            {
                while (count > 0)
                {
                    count--;
                }
            }
            if (i < (food_list.Count - 1))
            {
                sb.Append("" + temp + "" + food_list[i].FCounts + "份");
            }
            if (i == (food_list.Count - 1))
            {
                sb.Append("" + temp + "" + food_list[i].FCounts + "份，");
            }
        }
        sb.Append("备注：" + info.OrderAttach + "，总价：" + info.OrderSums + "；联系人：" + info.OrderRcver + "，地址：" + info.OrderAddress + "，电话：" + info.OrderComm);
        sb.Append("【" + SectionProxyData.GetSetValue(2) + "】");


        return sb.ToString();
    }

    /// <summary>
    /// 根据订单生成要发送的短信字符串。
    /// </summary>
    /// <returns></returns>
    public static string getExpressOrderInfo(Hangjing.Model.ExpressOrderInfo info)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("订单号：" + info.OrderID + " ，收货时间：" + info.SentTime + "，商品：" + info.Inve2);
        sb.Append("收货人姓名：" + info.callmsg + " ，收货人联系电话：" + info.ReveVar + "，收货人地址：" + info.Oorderid);
        sb.Append("发货人：" + info.UserName + " ，发货人联系电话：" + info.Tel + "，发货人地址：" + info.Address + ",");

        sb.Append("备注：" + info.Remark + "，总价：" + info.TotalPrice + "。");
        sb.Append("【" + SectionProxyData.GetSetValue(2) + "】");


        return sb.ToString();
    }

    public static string GetSex(string State)
    {
        string ret = "未知";

        if (State == "0")
        {
            ret = "男";
        }
        else if (State == "1")
        {
            ret = "女";
        }

        return ret;
    }
    public static string getCancelstate(int State)
    {
        string ret = "未处理";
        switch (State)
        {
            case 0:
                break;
            case 1:
                ret = "已同意";
                break;
            case 2:
                ret = "已拒绝";
                break;
        }
        return ret;
    }
    public static string getHurrystate(int State)
    {
        string ret = "未处理";
        switch (State)
        {
            case 0:
                break;
            case 1:
                ret = "已处理";
                break;
        }
        return ret;
    }

}
