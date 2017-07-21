using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;

/// <summary>
/// CommonClass 的摘要说明
/// </summary>
public class CommonClass
{
    #region  常用
    //截取指定长度字符串
    public static string CutStr(string str, int length)
    {
        str = NoHTML(str);
        if (str.Length > length)
        {
            return str.Substring(0, length);
        }
        return str;
    }
    //获取访问客户端IP
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
        if (string.IsNullOrEmpty(result))
        {
            return "127.0.0.1";
        }
        return result;
    }

    //根据时间创建字符串
    public static string CreateDateTimeString()
    {
        DateTime now = DateTime.Now;
        string newString = now.Year.ToString()
            + now.Month.ToString().PadLeft(2, '0')
            + now.Day.ToString().PadLeft(2, '0')
            + now.Hour.ToString().PadLeft(2, '0')
            + now.Minute.ToString().PadLeft(2, '0')
            + now.Second.ToString().PadLeft(2, '0')
            + now.Millisecond.ToString().PadLeft(3, '0');
        return (newString);
    }
    //获取时间差
    public static string GetTimeSpan(DateTime t)
    {
        string str = string.Empty;
        DateTime time = DateTime.Now;
        TimeSpan ts = time - t;
        int i = Convert.ToInt32(ts.TotalSeconds);
        if (i < 60)
        {
            str = "刚刚";
        }
        else if (i < 60 * 60)
        {
            str = Convert.ToInt32(i / 60).ToString() + "分钟前";
        }
        else if (i < 60 * 60 * 24)
        {
            str = Convert.ToInt32(i / (60 * 60)).ToString() + "小时前";
        }
        else if (i < 60 * 60 * 24 * 30)
        {
            str = Convert.ToInt32(i / (60 * 60 * 24)).ToString() + "天前";
        }
        else if (i < 60 * 60 * 24 * 30 * 12)
        {
            str = Convert.ToInt32(i / (60 * 60 * 24 * 30)).ToString() + "月前";
        }
        else
        {
            str = Convert.ToInt32(i / (60 * 60 * 24 * 30 * 12)).ToString() + "年前";
        }
        return str;
    }

    //获取当前页面名
    public static string CurrentPageName()
    {
        string url = HttpContext.Current.Request.Path;
        return url.Substring(url.LastIndexOf("/") + 1);
    }

    //时间戳转换为正常时间
    public static DateTime GetTimeByStamp(string timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        return dtStart.Add(toNow);
    }

    //生成不带指定字符的会员卡号
    public static string GetMemberNum(char outNum)
    {
        DateTime now = DateTime.Now;
        string newString = now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0') + now.Millisecond.ToString().PadLeft(3, '0');
        newString = newString.Replace(outNum, '8');
        return newString;
    }
    #endregion

    #region   HTML字符串操作
    /// <summary>
    /// 去除HTML标记
    /// </summary>
    /// <param name="Htmlstring">包括HTML的字符串</param>
    /// <returns>已经去除后的文字</returns>
    public static string NoHTML(string Htmlstring)
    {
        Htmlstring = HttpUtility.UrlDecode(Htmlstring);
        Htmlstring = HttpUtility.HtmlDecode(Htmlstring);
        //删除脚本 
        Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
        //删除HTML 
        Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", " <", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

        Htmlstring.Replace("<", "");
        Htmlstring.Replace(">", "");
        Htmlstring.Replace("p", "");
        Htmlstring.Replace("\r\n", "");
        Htmlstring = HttpUtility.HtmlEncode(Htmlstring).Trim();

        return Htmlstring;
    }

    /// <summary>
    /// 删除脚本
    /// </summary>
    /// <param name="Htmlstring">可能包含js的字符串</param>
    /// <returns>返回删除脚本后的HTML编码字符串</returns>
    public static string NoScript(string Htmlstring)
    {
        Htmlstring = HttpUtility.UrlDecode(Htmlstring);
        Htmlstring = HttpUtility.HtmlDecode(Htmlstring);
        Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
        Htmlstring = HttpUtility.HtmlEncode(Htmlstring).Trim();
        return Htmlstring;
    }
    #endregion

    #region  加密操作
    //MD5密码加密
    public static string GetMD5String(string str)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] data = Encoding.Default.GetBytes(str); //将密码字符串转化成字节数组
        byte[] md5data = md5.ComputeHash(data);  //计算指定字节数组的哈希值
        md5.Clear();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < md5data.Length - 1; i++)
        {
            sb.Append(md5data[i].ToString("X6"));
        }
        return sb.ToString();
    }
    #endregion

    #region  正则表达式验证
    //验证用户名
    public static bool CheckUserName(string str)
    {
        string valEx = @"^[a-zA-Z0-9\u4E00-\u9FA5]{4,16}$";
        return CheckRe(str, valEx);
    }
    //验证登录名
    public static bool CheckAdminName(string str)
    {
        string valEx = @"^[a-zA-Z0-9]{4,16}$";
        return CheckRe(str, valEx);
    }
    //验证密码
    public static bool CheckUserPwd(string str)
    {
        string valEx = @"^[a-zA-Z0-9_!@#$%^&*]{4,20}$";
        return CheckRe(str, valEx);
    }
    //验证邮箱
    public static bool CheckEmail(string str)
    {
        string valEx = @"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$";
        return CheckRe(str, valEx);
    }
    //验证手机号
    public static bool CheckPhone(string str)
    {
        string valEx = @"^0?(13[0-9]|15[0-9]|18[0-9]|14[0-9]|17[0])[0-9]{8}$";
        return CheckRe(str, valEx);
    }
    private static bool CheckRe(string str, string valEx)
    {
        return Regex.IsMatch(str, valEx);
    }
    #endregion

    #region  Cookie操作
    //写入Cookie
    public static void SetCookie(string cookieName, string value, int days)
    {
        //创建一个HttpCookie对象
        HttpCookie cookie = new HttpCookie(cookieName);
        value = HttpUtility.UrlEncode(value);
        //设定此cookies值
        cookie.Value = value;
        //设定cookie的生命周期
        cookie.Expires = DateTime.Now.AddDays(days);
        //加入此cookie
        HttpContext.Current.Response.Cookies.Add(cookie);
    }
    //读取Cookie
    public static string GetCookie(string cookieName)
    {
        //获得cookie
        HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
        //确定是否存在用户输入的cookie
        if (cookie == null)
        {
            return null;
        }
        else
        {
            //找到指定的cookie，显示cookie的值
            string value = cookie.Value;
            return HttpUtility.UrlDecode(value);
        }
    }
    //删除Cookie
    public static void DeleteCookie(string cookieName)
    {
        HttpCookie cookie = new HttpCookie(cookieName);
        cookie.Expires = DateTime.Now.AddDays(-1);
        HttpContext.Current.Response.Cookies.Add(cookie);
    }
    //删除所有Cookie
    public static void DeleteAllCookie()
    {
        int count = HttpContext.Current.Request.Cookies.Count;
        //删除全部Cookie
        for (int i = 0; i < count; i++)
        {
            string cookiesName = HttpContext.Current.Request.Cookies[i].Name;
            HttpCookie cookie = new HttpCookie(cookiesName);
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
    #endregion

    #region    微信Get请求
    /// <summary>
    /// 发送Post请求
    /// </summary>
    /// <param name="posturl">请求地址</param>
    /// <param name="postData">发送的json值</param>
    /// <returns>返回的执行信息</returns>
    public static string HttpPostJson(string posturl, string postData)
    {
        Stream outstream = null;
        Stream instream = null;
        StreamReader sr = null;
        HttpWebResponse response = null;
        HttpWebRequest request = null;
        Encoding encoding = Encoding.UTF8;
        byte[] data = encoding.GetBytes(postData);
        // 准备请求...
        try
        {
            // 设置参数
            request = WebRequest.Create(posturl) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            outstream = request.GetRequestStream();
            outstream.Write(data, 0, data.Length);
            outstream.Close();
            //发送请求并获取相应回应数据
            response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            instream = response.GetResponseStream();
            sr = new StreamReader(instream, encoding);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            return content;
        }
        catch (Exception ex)
        {
            string err = ex.Message;
            return err;
        }
    }
    #endregion

    #region   图片缩放
    public static void UploadImage(string fileName, string sourcePath, string newpath, int newWidth, int newHeight)
    {
        //得到小图片Image对象
        System.Drawing.Image smallImage = GetNewImage(sourcePath + fileName, newWidth, newHeight);
        if (!Directory.Exists(newpath)) //判断、创建小图片的存放路径
        {
            Directory.CreateDirectory(newpath);
        }
        smallImage.Save(newpath + fileName);
    }


    /// <summary>
    /// 对图片进行处理，返回一个Image类别的对象
    /// </summary>
    /// <param name="sourcePath">原图片路径</param>
    /// <param name="newWidth">新图片宽度</param>
    /// <param name="newHeight">新图片高度</param>
    /// <returns></returns>
    private static System.Drawing.Image GetNewImage(string sourcePath, int newWidth, int newHeight)
    {
        System.Drawing.Image newImage = null;
        System.Drawing.Image oldImage = null;
        oldImage = System.Drawing.Image.FromFile(sourcePath);  //加载原图片
        newImage = oldImage.GetThumbnailImage(newWidth, newHeight, new System.Drawing.Image.GetThumbnailImageAbort(IsTrue), IntPtr.Zero); //对原图片进行缩放
        return newImage;
    }

    /// <summary>
    /// 在Image类别对图片进行缩放的时候，需要一个返回bool类别的委托
    /// </summary>
    /// <returns></returns>
    private static bool IsTrue()
    {
        return true;
    }

    #endregion

    #region 页面通知
    public static void OpenDialogForPage(Page page, string message, string locationPage)
    {
        ScriptManager.RegisterClientScriptBlock(
          page,
          typeof(Page),
          DateTime.Now.ToString().Replace(":", " "),///使用当前时间作为标识
         "alert('" + message + "');location.href='" + locationPage + "'",
         true);
    }

    public static void OpenDialogForPage(Page page, string message)
    {
        ScriptManager.RegisterClientScriptBlock(
          page,
          typeof(Page),
          DateTime.Now.ToString().Replace(":", " "),///使用当前时间作为标识
         "alert('" + message + "')",
         true);
    }
    #endregion


    #region URL参数操作
    /// <summary>
    /// 得到当前页面返回地址 参数为returnurl 没有则返回string.Empty
    /// </summary>
    /// <returns></returns>
    public static string GetReturnURL()
    {
        string url =HttpContext.Current.Request.RawUrl;//得到域名后的URL信息
        int index = url.IndexOf("returnurl");
        if (index > -1)
        {
            url = HttpContext.Current.Server.HtmlEncode(url).Replace("&amp;", "&");//这里进行HTML编码，以防XSS攻击
            url = HttpContext.Current.Server.UrlDecode(url.Substring(index + 10)); 
            url = GetSafetyString(url);//再次使用360的去除xss
            return url;
        }
        else
        {
            return string.Empty;
        }
    }

    #endregion


    #region 数据安全检测
    private const string StrRegex = @"<[^>]+?style=[\w]+?:expression\(|\b(alert|confirm|prompt)\b|^\+/v(8|9)|<[^>]*?=[^>]*?&#[^>]*?>|\b(and|or)\b.{1,6}?(=|>|<|\bin\b|\blike\b)|/\*.+?\*/|<\s*script\b|<\s*img\b|\bEXEC\b|UNION.+?SELECT|UPDATE.+?SET|INSERT\s+INTO.+?VALUES|(SELECT|DELETE).+?FROM|(CREATE|ALTER|DROP|TRUNCATE)\s+(TABLE|DATABASE)";

    public static bool PostData()
    {
        bool result = false;
        for (int i = 0; i < HttpContext.Current.Request.Form.Count; i++)
        {
            result = CheckData(HttpContext.Current.Request.Form[i].ToString());
            if (result)
            {
                break;
            }
        }
        return result;
    }

    public static bool GetData()
    {
        bool result = false;

        for (int i = 0; i < HttpContext.Current.Request.QueryString.Count; i++)
        {
            result = CheckData(HttpContext.Current.Request.QueryString[i].ToString());
            if (result)
            {
                break;
            }
        }
        return result;
    }

    public static bool CookieData()
    {
        bool result = false;
        for (int i = 0; i < HttpContext.Current.Request.Cookies.Count; i++)
        {
            result = CheckData(HttpContext.Current.Request.Cookies[i].Value.ToLower());
            if (result)
            {
                break;
            }
        }
        return result;

    }

    public static bool referer()
    {
        bool result = false;
        return result = CheckData(HttpContext.Current.Request.UrlReferrer.ToString());
    }


    public static bool CheckData(string inputData)
    {
        if (Regex.IsMatch(inputData, StrRegex))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    /// <summary>
    /// 得到安全的字符串 没有返回为空字符串（""）
    /// </summary>
    /// <param name="str">要去除危险的字符串</param>
    /// <returns></returns>
    public static string GetSafetyString(string str)
    {
        if (Regex.IsMatch(str, StrRegex))
        {
            return "";
        }
        else
        {
            return str;
        }
    }

    /// <summary>
    /// 得到安全的字符串
    /// </summary>
    /// <param name="str">要去除危险的字符串</param>
    /// <param name="defaultStr">如果是危险字符串返回的字符串</param>
    /// <returns></returns>
    public static string GetSafetyString(string str, string defaultStr)
    {
        if (Regex.IsMatch(str, StrRegex))
        {
            return defaultStr;
        }
        else
        {
            return str;
        }
    }

    #endregion



    #region 根据图片路径 得到图片
    #region 得到本地图片
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
    #endregion
    #region 得到远程图片
    /// <summary>
    /// 显示图片，无图片时显示默认图片, 主要用于判断远程的的图片是否存在 2015-9-3 20:05:29
    /// </summary>
    public static string ShowPicFix2(string src, string img)
    {
        string ret = (new Control()).ResolveUrl("~/images/" + img);
        if (src == "")
        {
            return ret;
        }
        if (RemoteFileExists(src))
        {
            return (new Control()).ResolveUrl(src);
        }
        else
        {
            return ret;
        }
    }

    /// <summary>
    ///  判断远程文件是否存在
    /// </summary>
    /// <param name="fileUrl">文件URL</param>
    /// <returns>存在-true，不存在-false</returns>
    private static bool RemoteFileExists(string fileUrl)
    {
        bool result = false;//下载结果
        WebResponse response = null;
        try
        {
            WebRequest req = WebRequest.Create(fileUrl);
            response = req.GetResponse();
            result = response == null ? false : true;
        }
        catch
        {
            result = false;
        }
        finally
        {
            if (response != null)
            {
                response.Close();
            }
        }
        return result;
    }  
    #endregion
    #endregion


}