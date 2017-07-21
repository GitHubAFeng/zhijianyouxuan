using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Xml;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Hangjing.Common;

namespace Hangjing.Weixin
{
    /**
    '签名工具类
     ============================================================================/// <summary>
    'api说明：
    'init();
    '初始化函数，默认给一些参数赋值。
    'setKey(key_)'设置商户密钥
    'createMd5Sign(signParams);字典生成Md5签名
    'genPackage(packageParams);获取package包
    'createSHA1Sign(signParams);创建签名SHA1
    'parseXML();输出xml
    'getDebugInfo(),获取debug信息
     * 
     * ============================================================================
     */
    public class RequestHandler
    {
        public RequestHandler(HttpContext httpContext)
        {
            parameters = new Hashtable();

            this.httpContext = httpContext;
        }

        /// <summary>
        /// 签名，加密串
        /// </summary>
        public string signstr
        {
            get;
            set;
        }


        // 密钥 
        private string key = "";

        protected HttpContext httpContext;

        /** 请求的参数 */
        protected Hashtable parameters;

        /** debug信息 */
        private string debugInfo;

        /** 初始化函数 */
        public virtual void init()
        {
        }
        /** 获取debug信息 */
        public String getDebugInfo()
        {
            return debugInfo;
        }
        /** 获取密钥 */
        public String getKey()
        {
            return key;
        }

        /** 设置密钥 */
        public void setKey(string key)
        {
            this.key = key;
        }

        /** 设置参数值 */
        public void setParameter(string parameter, string parameterValue)
        {
            if (parameter != null && parameter != "")
            {
                if (parameters.Contains(parameter))
                {
                    parameters.Remove(parameter);
                }

                parameters.Add(parameter, parameterValue);
            }
        }

        /// <summary>
        /// 获取package带参数的签名包
        /// </summary>
        /// <returns></returns>
        public string getRequestURL()
        {
            this.createSign();
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();
            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + HttpUtility.UrlEncode(v, Encoding.UTF8) + "&");
                }
            }

            //去掉最后一个&
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            HJlog.toLog("getRequestURL" + sb.ToString());
            return sb.ToString();
        }


        //创建md5摘要,规则是:按参数名称a-z排序,遇到空值的参数不参加签名。

        protected virtual void createSign()
        {
            StringBuilder sb = new StringBuilder();

            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && "".CompareTo(v) != 0 && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + this.getKey());
            string sign = Utils.GetMD5(sb.ToString(), "UTF-8").ToUpper();

            this.setParameter("sign", sign);

            //debug信息
            this.setDebugInfo(sb.ToString() + " => sign:" + sign);
        }


        //创建package签名
        public virtual string createMd5Sign()
        {
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "".CompareTo(v) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + this.getKey());
            HJlog.toLog("createMd5Sign=" + sb.ToString());
            string sign = Utils.GetMD5(sb.ToString(), "UTF-8").ToUpper();
            this.setParameter("sign", sign);
            return sign;
        }


        //创建sha1签名
        public string createSHA1Sign()
        {
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && "".CompareTo(v) != 0
                       && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    if (sb.Length == 0)
                    {
                        sb.Append(k + "=" + v);
                    }
                    else
                    {
                        sb.Append("&" + k + "=" + v);
                    }
                }
            }
            string paySign = Utils.getSha1(sb.ToString()).ToString().ToLower();

            //debug信息
            this.setDebugInfo(sb.ToString() + " => sign:" + paySign);
            return paySign;
        }


        /// <summary>
        /// 输出XML (提交要以xml的形式)
        /// </summary>
        /// <returns></returns>
        public string parseXML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            foreach (string k in parameters.Keys)
            {
                string v = (string)parameters[k];
                if (Regex.IsMatch(v, @"^[0-9.]$"))
                {
                    sb.Append("<" + k + ">" + v + "</" + k + ">");
                }
                else
                {
                    sb.Append("<" + k + "><![CDATA[" + v + "]]></" + k + ">");
                }

            }
            sb.Append("</xml>");
            HJlog.toLog("parseXML" + sb.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// 创建sha1分享签名
        /// </summary>
        /// <returns></returns>
        public string createSHA1ShareSign()
        {
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && "".CompareTo(v) != 0)
                {
                    if (sb.Length == 0)
                    {
                        sb.Append(k + "=" + v);
                    }
                    else
                    {
                        sb.Append("&" + k + "=" + v);
                    }
                }
            }

            signstr = sb.ToString();


            string paySign = SHA1_Encrypt(signstr);

            //debug信息
            this.setDebugInfo(sb.ToString() + "=> sign:" + paySign);
            return paySign;
        }

        /// <summary>
        /// 对字符串进行SHA1加密
        /// </summary>
        /// <param name="strIN">需要加密的字符串</param>
        /// <returns>密文</returns>
        public string SHA1_Encrypt(string Source_String)
        {
            byte[] StrRes = Encoding.UTF8.GetBytes(Source_String);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }


        /** 设置debug信息 */
        public void setDebugInfo(String debugInfo)
        {
            this.debugInfo = debugInfo;
        }

        public Hashtable getAllParameters()
        {
            return this.parameters;
        }

        protected virtual string getCharset()
        {
            return this.httpContext.Request.ContentEncoding.BodyName;
        }
    }
}
