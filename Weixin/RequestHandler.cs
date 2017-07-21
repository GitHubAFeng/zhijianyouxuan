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
    'ǩ��������
     ============================================================================/// <summary>
    'api˵����
    'init();
    '��ʼ��������Ĭ�ϸ�һЩ������ֵ��
    'setKey(key_)'�����̻���Կ
    'createMd5Sign(signParams);�ֵ�����Md5ǩ��
    'genPackage(packageParams);��ȡpackage��
    'createSHA1Sign(signParams);����ǩ��SHA1
    'parseXML();���xml
    'getDebugInfo(),��ȡdebug��Ϣ
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
        /// ǩ�������ܴ�
        /// </summary>
        public string signstr
        {
            get;
            set;
        }


        // ��Կ 
        private string key = "";

        protected HttpContext httpContext;

        /** ����Ĳ��� */
        protected Hashtable parameters;

        /** debug��Ϣ */
        private string debugInfo;

        /** ��ʼ������ */
        public virtual void init()
        {
        }
        /** ��ȡdebug��Ϣ */
        public String getDebugInfo()
        {
            return debugInfo;
        }
        /** ��ȡ��Կ */
        public String getKey()
        {
            return key;
        }

        /** ������Կ */
        public void setKey(string key)
        {
            this.key = key;
        }

        /** ���ò���ֵ */
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
        /// ��ȡpackage��������ǩ����
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

            //ȥ�����һ��&
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            HJlog.toLog("getRequestURL" + sb.ToString());
            return sb.ToString();
        }


        //����md5ժҪ,������:����������a-z����,������ֵ�Ĳ������μ�ǩ����

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

            //debug��Ϣ
            this.setDebugInfo(sb.ToString() + " => sign:" + sign);
        }


        //����packageǩ��
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


        //����sha1ǩ��
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

            //debug��Ϣ
            this.setDebugInfo(sb.ToString() + " => sign:" + paySign);
            return paySign;
        }


        /// <summary>
        /// ���XML (�ύҪ��xml����ʽ)
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
        /// ����sha1����ǩ��
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

            //debug��Ϣ
            this.setDebugInfo(sb.ToString() + "=> sign:" + paySign);
            return paySign;
        }

        /// <summary>
        /// ���ַ�������SHA1����
        /// </summary>
        /// <param name="strIN">��Ҫ���ܵ��ַ���</param>
        /// <returns>����</returns>
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


        /** ����debug��Ϣ */
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
