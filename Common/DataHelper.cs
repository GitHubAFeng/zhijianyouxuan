using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

// DataHelper.cs
// lichunlin@ihangjing.com
// 2015-06-1
// 数据帮助类
namespace Hangjing.Common
{
    public static class DataHelper
    {
        static JavaScriptSerializer jss = new JavaScriptSerializer();// js 序列化器

        #region 将 对象 转成 json格式字符串
        /// <summary>
        /// 将 对象 转成 json格式字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Obj2Json(object obj)
        {
            //把集合 转成 json 数组格式字符串
            return jss.Serialize(obj);
        }
        #endregion

        #region 将 json格式字符串 转成 对象
        /// <summary>
        /// 将 json格式字符串 转成 对象 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Json2Obj<T>(string json)
        {
            //把 json 数组格式字符串 转成 指定对象
            return jss.Deserialize<T>(json);
        }
        #endregion

        #region 加密操作
        #region SHA1加密字符串 
        /// <summary> 
        /// SHA1加密字符串 
        /// </summary> 
        /// <param name="source">源字符串</param> 
        /// <returns>加密后的字符串</returns> 
        public static string SHA1(string source)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(source, "SHA1");
        }  
        #endregion
        #region 返回 MD5 加密字符串（1）
        /// <summary>
        /// 返回 MD5 加密字符串（小写）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5_1(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
        }
        #endregion
        #region 返回 MD5 加密字符串（2）
        /// <summary>
        /// 返回 MD5 加密字符串（大写）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5_2(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToUpper();
        }
        #endregion
        #region  返回 MD5 加密字符串（3）
        /// <summary>
        /// 返回 MD5 加密字符串（3）
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns></returns>
        public static string MD5_3(string str)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
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
        #region 返回 MD5 加密字符串（4）
        /// <summary>
        /// MD5加密（小写）
        /// </summary>
        /// <param name="src">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5_4(string src)
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
        #endregion
        #region 加密解密操作
        /// <summary>
        /// 加密（说明 使用URL传参一定记得编码）
        /// </summary>
        /// <param name="inputString">需要加密的字符串</param>
        /// <param name="EncryptionKey">密钥</param>
        /// <returns></returns>
        public static string DesEncrypt(string inputString, string EncryptionKey)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return "";
            }
            byte[] byKey = null;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(inputString);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 解密（说明 解码时不要URL解码，在asp.net中自带已解码，如果再次解码将出现问题）
        /// </summary>
        /// <param name="inputString">需要解密的字符串</param>
        /// <param name="EncryptionKey">密钥</param>
        public static string DesDecrypt(string inputString, string EncryptionKey)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return "";
            }
            byte[] byKey = null;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] inputByteArray = new Byte[inputString.Length];
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(inputString);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = new System.Text.UTF8Encoding();
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion
        #endregion

        #region   HTML字符串操作
        #region 去除HTML标记
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
        #endregion
        #region 去除脚本
        public static string NoScript(string Htmlstring)
        {
            Htmlstring = HttpUtility.UrlDecode(Htmlstring);
            Htmlstring = HttpUtility.HtmlDecode(Htmlstring);
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            Htmlstring = HttpUtility.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
        #endregion
        #endregion

        #region 获取Json字符串某节点的值
        /// <summary>
        /// 获取Json字符串某节点的值
        /// </summary>
        public static string GetJsonValue(string jsonStr, string key)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(jsonStr))
            {
                key = "\"" + key.Trim('"') + "\"";
                int index = jsonStr.IndexOf(key) + key.Length + 1;
                if (index > key.Length + 1)
                {
                    //先截逗号，若是最后一个，截“｝”号，取最小值
                    int end = jsonStr.IndexOf(',', index);
                    if (end == -1)
                    {
                        end = jsonStr.IndexOf('}', index);
                    }
                    result = jsonStr.Substring(index, end - index);
                    result = result.Trim(new char[] { '"', ' ', '\'' }); //过滤引号或空格
                }
            }
            return result;
        }
        #endregion

    }
}
