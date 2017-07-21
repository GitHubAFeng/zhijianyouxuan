
// hjnetHelper.cs:参数的相关操作.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-03-05

using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Net;

namespace Hangjing.Common
{
    public static class HjNetHelper
    {
        /// <summary>
        /// 获取url中参数的值
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Request(string text)
        {
            string str = string.Empty;
            if ((HttpContext.Current.Request[text.ToLower()] != null) && (HttpContext.Current.Request[text] != string.Empty))
            {
                str = HttpContext.Current.Request[text];
            }
            return str;
        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            return GetQueryString(strName, true);
        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary> 
        /// <param name="strName">Url参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return "";
            }

            if (sqlSafeCheck && !StringHelper.IsSafeSqlString(HttpContext.Current.Request.QueryString[strName]))
            {
                return "unsafe string";
            }

            return HttpContext.Current.Request.QueryString[strName];
        }

        /// <summary>
        /// 获得指定Url Post参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetPostParam(string strName)
        {
            if (HttpContext.Current.Request[strName] == null)
            {
                return "";
            }

            if (true && !StringHelper.IsSafeSqlString(HttpContext.Current.Request[strName]))
            {
                return "unsafe string";
            }

            return HttpContext.Current.Request[strName];
        }

        /// <summary>
        /// 获得指定Url Post参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static int GetPostParam(string strName, int defultvalue)
        {
            if (HttpContext.Current.Request[strName] == null)
            {
                return defultvalue;
            }

            return Convert.ToInt32(GetPostParam(strName));
        }

        /// <summary>
        /// 获得指定Url参数的值 去除 - 检查 
        /// </summary> 
        /// <param name="strName">Url参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryStringFix(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return "";
            }

            if (sqlSafeCheck && !StringHelper.IsSafeSqlStringFix(HttpContext.Current.Request.QueryString[strName]))
            {
                return "unsafe string";
            }

            return HttpContext.Current.Request.QueryString[strName];
        }

        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName)
        {
            return GetFormString(strName, true);
        }

        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormStringFix(string strName)
        {
            return GetFormStringFix(strName, true);
        }

        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return "";
            }

            if (sqlSafeCheck && !StringHelper.IsSafeSqlString(HttpContext.Current.Request.Form[strName]))
            {
                return "unsafe string";
            }

            return HttpContext.Current.Request.Form[strName];
        }

        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormStringFix(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return "";
            }

            if (sqlSafeCheck && !StringHelper.IsSafeSqlStringFix(HttpContext.Current.Request.Form[strName]))
            {
                return "unsafe string";
            }

            return HttpContext.Current.Request.Form[strName];
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName)
        {
            return GetString(strName, false);
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName, bool sqlSafeCheck)
        {
            if ("".Equals(GetQueryString(strName)))
            {
                return GetFormString(strName, sqlSafeCheck);
            }
            else
            {
                return GetQueryString(strName, sqlSafeCheck);
            }
        }

        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName, int defValue)
        {
            return StringHelper.StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
        }

        /// <summary>
        /// 获得指定表单参数的int类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的int类型值</returns>
        public static int GetFormInt(string strName, int defValue)
        {
            return StringHelper.StrToInt(HttpContext.Current.Request.Form[strName], defValue);
        }

        /// <summary>
        /// 获得指定Url或表单参数的int类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static int GetInt(string strName, int defValue)
        {
            if (GetQueryInt(strName, defValue) == defValue)
            {
                return GetFormInt(strName, defValue);
            }
            else
            {
                return GetQueryInt(strName, defValue);
            }
        }

        /// <summary>
        /// 获得指定Url参数的float类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static float GetQueryFloat(string strName, float defValue)
        {
            return StringHelper.StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
        }

        /// <summary>
        /// 获得指定表单参数的float类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的float类型值</returns>
        public static float GetFormFloat(string strName, float defValue)
        {
            return StringHelper.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
        }

        /// <summary>
        /// 获得指定Url或表单参数的float类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static float GetFloat(string strName, float defValue)
        {
            if (GetQueryFloat(strName, defValue) == defValue)
            {
                return GetFormFloat(strName, defValue);
            }
            else
            {
                return GetQueryFloat(strName, defValue);
            }
        }

        /// <summary>
        /// 网站根路径
        /// </summary>
       public static string SiteRootPath = HttpContext.Current.Request.PhysicalApplicationPath.Substring(0, HttpContext.Current.Request.PhysicalApplicationPath.Length - 1);
        //public static string SiteRootPath = @"D:\wwwroot\web";
        /// <summary>
        /// 网站配置目录目录名称
        /// </summary>
        public static string ConfDirName = ConfigurationManager.AppSettings["ConfDirName"];
        
        private static object lockHelper = new object();


        ///// <summary>
        ///// 检查文件的属性
        ///// </summary>
        ///// <param name="fileInfo"></param>
        ///// <param name="fileSystemRights"></param>
        ///// <returns></returns>
        //public static bool HasAccces(FileInfo fileInfo, FileSystemRights fileSystemRights)
        //{
        //    string str = WindowsIdentity.GetCurrent().Name.ToUpper();
        //    foreach (FileSystemAccessRule rule in fileInfo.GetAccessControl().GetAccessRules(true, true, typeof(NTAccount)))
        //    {
        //        if (str == rule.get_IdentityReference().get_Value().ToUpper())
        //        {
        //            return ((rule.get_AccessControlType() == null) && (fileSystemRights == (rule.get_FileSystemRights() & fileSystemRights)));
        //        }
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// 检查文件是否可读写
        ///// </summary>
        ///// <param name="filePath"></param>
        ///// <returns></returns>
        //public static bool TestReadWriteAccces(string filePath)
        //{
        //    FileInfo fileInfo = new FileInfo(filePath);
        //    return (!fileInfo.get_IsReadOnly() && HasAccces(fileInfo, 0x301bf));
        //}

        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }

        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }

        /// <summary>
        /// 返回指定的服务器变量信息
        /// </summary>
        /// <param name="strName">服务器变量名</param>
        /// <returns>服务器变量信息</returns>
        public static string GetServerString(string strName)
        {
            if (HttpContext.Current.Request.ServerVariables[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.ServerVariables[strName].ToString();
        }

        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer()
        {
            string retVal = null;

            try
            {
                retVal = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch { }

            if (retVal == null)
                return "";

            return retVal;

        }
        /// <summary>
        /// 获取网站配置文件(整站全局)
        /// </summary>
        /// <returns></returns>
        public static SiteInfo GetSiteConfig()
        {
            SiteInfo config = new SiteInfo();
            HttpContext context = HttpContext.Current;
            string filename = string.Empty;
            if (context != null)
            {
                filename = context.Server.MapPath("~/config/Site.config");
                if (!File.Exists(filename))
                {
                    filename = context.Server.MapPath("/config/Site.config");
                }
            }
            else
            {
                filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config/Site.config");
            }

            if (!File.Exists(filename))
            {
                //throw new EBException("发生错误: 虚拟目录或网站根目录下没有正确的Hangjing.config文件");
            }

            config = (SiteInfo)SerializationHelper.Load(typeof(SiteInfo), filename);

            return config;
        }

        /// <summary>
        /// 得到当前完整主机头
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFullHost()
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            if (!request.Url.IsDefaultPort)
            {
                return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
            }
            return request.Url.Host;
        }

        /// <summary>
        /// 得到主机头
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }

        /// <summary>
        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))
        /// </summary>
        /// <returns>原始 URL</returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        /// <summary>
        /// 判断当前访问是否来自浏览器软件
        /// </summary>
        /// <returns>当前访问是否来自浏览器软件</returns>
        public static bool IsBrowserGet()
        {
            string[] BrowserName = { "ie", "opera", "netscape", "mozilla", "konqueror", "firefox" };
            string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (curBrowser.IndexOf(BrowserName[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断是否来自搜索引擎链接
        /// </summary>
        /// <returns>是否来自搜索引擎链接</returns>
        public static bool IsSearchEnginesGet()
        {
            if (HttpContext.Current.Request.UrlReferrer == null)
            {
                return false;
            }
            string[] SearchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou" };
            string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
            for (int i = 0; i < SearchEngine.Length; i++)
            {
                if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        /// <summary>
        /// 获得当前页面的名称
        /// </summary>
        /// <returns>当前页面的名称</returns>
        public static string GetPageName()
        {
            string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return urlArr[urlArr.Length - 1].ToLower();
        }

        /// <summary>
        /// 返回表单或Url参数的总个数
        /// </summary>
        /// <returns></returns>
        public static int GetParamCount()
        {
            return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = String.Empty;

            // 优先取得代理IP 
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
        /// 保存用户上传的文件
        /// </summary>
        /// <param name="path">保存路径</param>
        public static void SaveRequestFile(string path)
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpContext.Current.Request.Files[0].SaveAs(path);
            }
        }

        /// <summary>
        /// SQL SERVER SQL语句转义
        /// </summary>
        /// <param name="str">需要转义的关键字符串</param>
        /// <param name="pattern">需要转义的字符数组</param>
        /// <returns>转义后的字符串</returns>
        public static string RegEsc(string str)
        {
            string[] pattern = { @"%", @"_", @"'" };
            foreach (string s in pattern)
            {
                switch (s)
                {
                    case "%":
                        str = str.Replace(s, "[%]");
                        break;
                    case "_":
                        str = str.Replace(s, "[_]");
                        break;
                    case "'":
                        str = str.Replace(s, "['']");
                        break;
                }
            }
            return str;
        }

        /// <summary>
        /// 判断IP/Email地址/时间格式是否正确
        /// </summary>
        /// <param name="NewHash"></param>
        /// <param name="ruletype"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsRuleTip(Hashtable NewHash, string ruletype, out string key)
        {
            key = "";
            foreach (DictionaryEntry str in NewHash)
            {

                try
                {
                    string[] single = StringHelper.SplitString(str.Value.ToString(), "\r\n");

                    foreach (string strs in single)
                    {
                        if (strs != "")


                            switch (ruletype.Trim().ToLower())
                            {
                                case "email":
                                    if (IsValidDoEmail(strs.ToString()) == false)
                                        throw new Exception();
                                    break;

                                case "ip":
                                    if (IsIPSect(strs.ToString()) == false)
                                        throw new Exception();
                                    break;

                                case "timesect":
                                    string[] splitetime = strs.Split('-');
                                    if (IsTime(splitetime[1].ToString()) == false || IsTime(splitetime[0].ToString()) == false)
                                        throw new Exception();
                                    break;
                            }
                    }
                }
                catch
                {
                    key = str.Key.ToString();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 是否为ip不能有*号
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 是否为ip地址，其中可以有*
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIPSect(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");
        }

        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
        }

        public static bool IsValidDoEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }

        /// <summary>
        /// 根据IP查找用户 注册IP限制后需要判断是否在72个小时的限制时间内（72小时此IP只能注册一个用户）
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns>用户信息</returns>
        public static string CheckRegisterDateDiff(string ip)
        {
            //ShortUserInfo userinfo = Users.GetShortUserInfoByIP(ip);

            //if (GeneralConfigs.GetConfig().Regctrl > 0 && userinfo != null)
            //{
            //    int Interval = Utils.StrDateDiffHours(userinfo.Joindate, GeneralConfigs.GetConfig().Regctrl);
            //    if (Interval <= 0)
            //        return "抱歉, 系统设置了IP注册间隔限制, 您必须在 " + (Interval * -1) + " 小时后才可以注册";
            //}

            //if (GeneralConfigs.GetConfig().Ipregctrl.Trim() != "" && Utils.InIPArray(DNTRequest.GetIP(), Utils.SplitString(GeneralConfigs.GetConfig().Ipregctrl, "\n")) && userinfo != null)
            //{
            //    int Interval = Utils.StrDateDiffHours(userinfo.Joindate, 72);
            //    if (Interval < 0)
            //        return "抱歉, 系统设置了特殊IP注册限制, 您必须在 " + (Interval * -1) + " 小时后才可以注册";
            //}
            return null;
        }

        /// <summary>
        /// 获取当前时间，转化成字符串   (09-09-01 yangxiaolong@ihangjing.com)
        /// </summary>
        /// <returns></returns>
        public static string GetTime()
        {
            string str = DateTime.Now.ToString("yyMMddHHmmss");

            return str;
        }

        /// <summary>
        ///  检测是否全部为数字
        /// </summary>
        /// <param name="num">要检测的字符串</param>
        /// <returns></returns>
        public static Boolean CheckNum(string num)
        {
            return Regex.IsMatch(num, @"^\+?[0-9][0-9]*$");
        }

        /// <summary>
        /// 中文检测
        /// </summary>
        /// <param name="strTest"></param>
        /// <returns></returns>
        public static Boolean IsIncludeChineseCode(string strTest)
        {
            return Regex.IsMatch(strTest, @"[\u4e00-\u9fa5]+");
        }

        /// <summary>
        /// MD5 16位加密
        /// </summary>
        /// <param name="STR">要加密的字符串</param>
        /// <returns></returns>  
        public static string Encryption(string STR)
        {
            string password = "";
            //pwd为加密结果
            MD5 ps = MD5.Create();
            byte[] s = ps.ComputeHash(Encoding.UTF8.GetBytes(STR));
            //这里的UTF8是编码方式，你可以采用你喜欢的方式进行，比如UNcode等等
            for (int i = 0; i < s.Length; i++)
            {
                password = password + s[i].ToString();
            }
            return password;
        }

        /// <summary>
        /// 邮箱检测 
        /// </summary>
        /// <param name="strEMail"></param>
        /// <returns></returns>
        public static bool isMail(string strEMail)
        {
            return Regex.IsMatch(strEMail, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }

        //判断用户名称格式
        public static bool isUserName(string name)
        {
            return Regex.IsMatch(name, @"^[0-9a-zA-Z_]{6,20}$");
        }

        //必须为数字
        public static bool isNum(string strNum)
        {
            if (strNum == null)
                return false;
            return Regex.IsMatch(strNum, @"^\+?[1-9][0-9]*$");
        }

        //验证价格
        public static bool isProce(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(0|[1-9]\d*)(\.\d{1,2})?$");
        }

        //判断日期格式 如:2008-1-20 或2008-01-20 而且包含了对不同年份2月的天数，闰年的控制等等：
        public static bool isDataTime(string datetime)
        {
            return Regex.IsMatch(datetime, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
        }

        /// <summary>
        /// 验证网址检测 
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public static bool isUrl(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$");
        }

        /// <summary>
        ///  非法SQL字符检测
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckBadWord(string str)
        {
            string pattern = @"select|insert|delete|from|count\(|drop table|update|truncate|asc\(|mid\(|char\(|xp_cmdshell|exec   master|netlocalgroup administrators|net user|or|and";
            if (Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase))
                return true;
            return false;
        }

        public static string Filter(string str)
        {
            string[] pattern = { "select", "insert", "delete", "from", "count\\(", "drop table", "update", "truncate", "asc\\(", "mid\\(", "char\\(", "xp_cmdshell", "exec   master", "netlocalgroup administrators", "net user", "or", "and" };
            for (int i = 0; i < pattern.Length; i++)
            {
                str = str.Replace(pattern[i].ToString(), "");
            }
            return str;
        }

        /// <summary>
        /// 创建Cookies
        /// </summary>
        /// <param name="strName">Cookie 主键</param>
        /// <param name="strValue">Cookie 键值</param>
        /// <code>ck.setCookie("主键","键值","天数");</code>
        public static bool SetCookie(string strName, string strValue)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(strName);
                Cookie.Value = strValue;
                HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取Cookies
        /// </summary>
        /// <param name="strName">Cookie 主键</param>
        /// <code>Cookie ck = new Cookie();</code>
        /// <code>ck.getCookie("主键");</code>
        public static string GetCookie(string strName)
        {
            HttpCookie Cookie = HttpContext.Current.Request.Cookies[strName];
            if (Cookie != null)
            {
                return Cookie.Value.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 清除Cookies
        /// </summary>
        /// <param name="strName"></param>
        public static bool ClearCookie(string strName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie != null)
            {
                cookie.Value = null;
                cookie.Expires = DateTime.Now.AddDays(-1.0);
                cookie.Values.Clear();
                HttpContext.Current.Response.Cookies.Set(cookie);

            }
            return true;
        }

        /// <summary>
        /// 过滤JS脚本
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string WipeScript(string html)
        {
            Regex[] regex = new Regex[12];
            RegexOptions options;
            options = RegexOptions.Singleline | RegexOptions.IgnoreCase;
            regex[0] = new Regex(@"<marquee[\s\S]+</marquee *>", options);
            regex[1] = new Regex(@"<script[\s\S]+</script *>", options);
            regex[2] = new Regex(@"href *= *[\s\S]*script *:", options);
            regex[3] = new Regex(@"<iframe[\s\S]+</iframe *>", options);
            regex[4] = new Regex(@"<frameset[\s\S]+</frameset *>", options);
            regex[5] = new Regex(@"<input[\s\S]+</input *>", options);
            regex[6] = new Regex(@"<button[\s\S]+</button *>", options);
            regex[7] = new Regex(@"<select[\s\S]+</select *>", options);
            regex[8] = new Regex(@"<textarea[\s\S]+</textarea *>", options);
            regex[9] = new Regex(@"<form[\s\S]+</form *>", options);
            regex[10] = new Regex(@"<embed[\s\S]+</embed *>", options);
            regex[11] = new Regex(@"on[/s/S]*=", options);
            for (int i = 0; i < regex.Length - 1; i++)
            {
                foreach (Match match in regex[i].Matches(html))
                {
                    html = html.Replace(match.Groups[0].ToString(), "");
                }
            }
            return html;
        }

        //验证内容长度
        public static bool isContent(string strContent)
        {
            return Regex.IsMatch(strContent, @"^(\s|\S){2,5000}$");
        }

        /// <summary>
        /// 清除编辑器中的非法字符串
        /// </summary>
        /// <param name="strHTML"></param>
        /// <returns></returns>
        public static string RemoveHTMLForEditor(string strHTML)
        {
            string input = strHTML;
            Regex regex = new Regex(@"<script[\s\S]+</script *>", RegexOptions.IgnoreCase);
            //Regex regex2 = new Regex(@" no[\s\S]*=", RegexOptions.IgnoreCase);
            Regex regex3 = new Regex(@"<iframe[\s\S]+</iframe *>", RegexOptions.IgnoreCase);
            Regex regex4 = new Regex(@"<frameset[\s\S]+</frameset *>", RegexOptions.IgnoreCase);
            //Regex regex5 = new Regex(@"<div[\s\S]+</div *>", RegexOptions.IgnoreCase);
            input = regex.Replace(input, "");
            //input = regex2.Replace(input, " _disibledevent=");
            input = regex3.Replace(input, "");
            input = regex4.Replace(input, "");
            //input = regex5.Replace(input, "");
            return input;
        }

        /// <summary>
        /// 过滤掉不合格的字符
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static string Filtrate(string strSource)
        {
            strSource = strSource.Replace("'", "");
            strSource = strSource.Replace("\"", "");
            strSource = strSource.Replace("<", "");
            strSource = strSource.Replace(">", "");
            strSource = strSource.Replace("=", "");
            strSource = strSource.Replace("or", "");
            strSource = strSource.Replace("select", "");
            strSource = strSource.Trim();
            return strSource;

        }

        /// <summary>
        /// soso,google坐标转百度坐标,返回数组[0]=lat,[1]=lng
        /// </summary>
        /// <param name="gg_lat"></param>
        /// <param name="gg_lon"></param>
        /// <returns></returns>
        public static string[] bd_encrypt(double gg_lat, double gg_lon)
        {
            const double x_pi = 3.14159265358979324 * 3000.0 / 180.0;
            double x = gg_lon, y = gg_lat;

            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * x_pi);
            double bd_lat = (z * Math.Sin(theta) + 0.006 - 0.002090646);
            double bd_lon = (z * Math.Cos(theta) + 0.0065 + 0.002991365);


            string[] latlgn = new string[2];

            latlgn[0] = bd_lat.ToString();
            latlgn[1] = bd_lon.ToString();

            return latlgn;
        }

        /// <summary>
        /// 通过访问SocketServer向android发送消息
        /// </summary>
        /// <param name="id">骑士或者商家编号</param>
        /// <param name="type">消息类型：0表示订单，1表示纯消息。</param>
        /// <param name="se">1表示骑士，2表示商家</param>
        /// <param name="msg">msg（通知内容） 新订单通知 {\"state\":"\"1\,\"count\":\"1\"}  count 表示订单数量  消息通知   {\"state\":"\"1\,\"msg\":\"XX订单取消配送\"} msg 表示消息内容</param>
        /// <returns></returns>
        public static String SendMsg2Android(string id, int type, int se, string msg, string _url)
        {
            String stream = "";
            try
            {
                string url = _url+"/SendWebSocketMSG.aspx?id=" + id + "&se=" + se + "&ty=" + type + "&msg=" + msg;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream resStream = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            stream = sr.ReadToEnd();
                        }
                    }
                }
                //  return stream;
            }
            catch (Exception ex)
            {
                HJlog.toLog("SocketServer向android发送消息：" + ex.ToString());
            }
            return stream;

        }

    }
}
