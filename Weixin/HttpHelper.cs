using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

/// <summary>
/// 类说明：HttpHelps类，用来实现Http访问，Post或者Get方式的，直接访问，带Cookie的，带证书的等方式，可以设置代理
/// 编码日期：2011-09-20
/// 编 码 人：苏飞
/// 联系方式：361983679  Blogs：<a href=\"http://www.cckan.net\" target=\"_blank\">http://www.cckan.net</a>
/// 修改日期：2012-09-27

/*
 *使用方法
 *
 *      httphelper objhttp = new httphelper();
        //不需要转为小写
        objhttp.isToLower = false;
        //参数对象
        HttpItem objHttpItem = new HttpItem()
        {
            URL = "http://www.cckan.net",
            Encoding = "gb2312",
            Method = "GET",
        };
        //取Html
        string text = objhttp.GetHtml(objHttpItem);
        //参数对象
        objHttpItem = new HttpItem()
        {
            URL = “cckan.net",
            Postdata = "username=sufei&userpwd=123456",
            Encoding = "gb2312",
            Method = "POST",
            Referer = "",
            Cookie = objhttp.cookie
        };
        //取Html
        string html = objhttp.GetHtml(objHttpItem);
 */
namespace Hangjing.Weixin
{
    public class HttpHelper
    {
        #region 预定义方法或者变更
        //默认的编码
        public Encoding encoding = new UTF8Encoding();
        //HttpWebRequest对象用来发起请求
        public HttpWebRequest request = null;
        //获取影响流的数据对象
        private HttpWebResponse response = null;
        //返回的Cookie
        public string cookie = "";
        //是否设置为全文小写
        public Boolean isToLower = true;
        //读取流的对象
        private StreamReader reader = null;
        //需要返回的数据对象
        private string returnData = "String Error";

        private HttpItem req_data;

        /// <summary>
        /// 根据相传入的数据，得到相应页面数据
        /// </summary>
        /// <param name="strPostdata">传入的数据Post方式,get方式传NUll或者空字符串都可以</param>
        /// <returns>string类型的响应数据</returns>
        private string GetHttpRequestData(string strPostdata)
        {
            try
            {

                if (req_data.CerPath != null && req_data.CerPath.Length > 0)
                {

                 
                    ServicePointManager.DefaultConnectionLimit = 200;
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                    Hangjing.AppLog.AppLog.Info("CerPath=" + req_data.CerPath + "#############        Cer_PWD=" + req_data.Cer_PWD);

                    X509Certificate2 certificate = new X509Certificate2(req_data.CerPath, req_data.Cer_PWD, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);


                  

                    request.ClientCertificates.Add(certificate);

                }
                else
                {
                    ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
                }


                request.AllowAutoRedirect = true;
                //验证在得到结果时是否有传入数据
                if (!string.IsNullOrEmpty(strPostdata) && request.Method.Trim().ToLower().Contains("post"))
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                    UTF8Encoding utf8encoding = new UTF8Encoding();
                    byte[] buffer = utf8encoding.GetBytes(strPostdata);
                    request.ContentLength = buffer.Length;
                    using (Stream postStream = request.GetRequestStream())
                    {
                        postStream.Write(buffer, 0, buffer.Length);
                    }
                   
                }


                using (response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.ContentEncoding != null && response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //开始读取流并设置编码方式
                        using (reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), encoding))
                        {
                            returnData = reader.ReadToEnd();
                        }
                    }
                    else
                    {
                        //开始读取流并设置编码方式
                        using (reader = new StreamReader(response.GetResponseStream()))
                        {
                            returnData = reader.ReadToEnd();
                        }
                    }

                }

            }
            catch (WebException ex)
            {
                //这里是在发生异常时返回的错误信息
                returnData = "String Error";
                response = (HttpWebResponse)ex.Response;
                Hangjing.Common.HJlog.toLog("请求出错：" + ex);
            }
            if (isToLower)
            {
                returnData = returnData.ToLower();
            }
            return returnData;
        }


        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>
        /// 4.0以下.net版本取水运
        /// </summary>
        /// <param name="streamResponse"></param>
        private static MemoryStream GetMemoryStream(Stream streamResponse)
        {
            MemoryStream _stream = new MemoryStream();
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = streamResponse.Read(buffer, 0, Length);
            // write the required bytes  
            while (bytesRead > 0)
            {
                _stream.Write(buffer, 0, bytesRead);
                bytesRead = streamResponse.Read(buffer, 0, Length);
            }
            return _stream;
        }

        /// <summary>
        /// 为请求准备参数
        /// </summary>
        ///<param name="objhttpItem">参数列表</param>
        /// <param name="_Encoding">读取数据时的编码方式</param>
        private void SetRequest(HttpItem objhttpItem)
        {
            //初始化对像，并设置请求的URL地址
            request = (HttpWebRequest)WebRequest.Create(GetUrl(objhttpItem.URL));
            //请求方式Get或者Post
            request.Method = objhttpItem.Method;
            //Accept
            request.Accept = objhttpItem.Accept;
            //ContentType返回类型
            request.ContentType = objhttpItem.ContentType;
            //UserAgent客户端的访问类型，包括浏览器版本和操作系统信息
            request.UserAgent = objhttpItem.UserAgent;

            //读取数据时的编码方式
            encoding = new UTF8Encoding();

            //Cookie
            request.Headers[HttpRequestHeader.Cookie] = objhttpItem.Cookie;
            //来源地址
            request.Referer = objhttpItem.Referer;
        }

        /// <summary>
        /// 设置当前访问使用的代理
        /// </summary>
        /// <param name="userName">代理 服务器用户名</param>
        /// <param name="passWord">代理 服务器密码</param>
        /// <param name="ip">代理 服务器地址</param>
        public void SetWebProxy(string userName, string passWord, string ip)
        {
            //设置代理服务器
            WebProxy myProxy = new WebProxy(ip, false);
            //建议连接
            myProxy.Credentials = new NetworkCredential(userName, passWord);
            //给当前请求对象
            request.Proxy = myProxy;
            //设置安全凭证
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
        }

        //回调验证证书问题
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            // 总是接受    
            return true;
        }

        #endregion
        #region 普通类型
        /// <summary>    
        /// 传入一个正确或不正确的URl，返回正确的URL
        /// </summary>    
        /// <param name="URL">url</param>   
        /// <returns>
        /// </returns>    
        public static string GetUrl(string URL)
        {
            if (!(URL.Contains("http://") || URL.Contains("https://")))
            {
                URL = "http://" + URL;
            }
            return URL;
        }

        ///<summary>
        ///采用https协议访问网络,根据传入的URl地址，得到响应的数据字符串。
        ///</summary>
        ///<param name="objhttpItem">参数列表</param>
        ///<returns>String类型的数据</returns>
        public string GetHtml(HttpItem objhttpItem)
        {
            req_data = objhttpItem;
            //准备参数
            SetRequest(objhttpItem);
            //调用专门读取数据的类
            return GetHttpRequestData(objhttpItem.Postdata);
        }

        #endregion
    }

    /// <summary>
    /// Http请求参考类 
    /// </summary>
    public class HttpItem
    {
        string _URL;

        /// <summary>
        /// 请求URL必须填写
        /// </summary>
        public string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }

        string _Method = "GET";

        /// <summary>
        /// 请求方式默认为GET方式
        /// </summary>
        public string Method
        {
            get { return _Method; }
            set { _Method = value; }
        }

        string _Accept = "text/html, application/xhtml+xml, */*";

        /// <summary>
        /// 请求标头值 默认为text/html, application/xhtml+xml, */*
        /// </summary>
        public string Accept
        {
            get { return _Accept; }
            set { _Accept = value; }
        }

        string _ContentType = "text/json";

        /// <summary>
        /// 请求返回类型默认 text/html
        /// </summary>
        public string ContentType
        {
            get { return _ContentType; }
            set { _ContentType = value; }
        }

        string _UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";

        /// <summary>
        /// 客户端访问信息默认Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)
        /// </summary>
        public string UserAgent
        {
            get { return _UserAgent; }
            set { _UserAgent = value; }
        }

        string _Encoding = "NULL";

        /// <summary>
        /// 返回数据编码默认为NUll,可以自动识别
        /// </summary>
        public string Encoding
        {
            get { return _Encoding; }
            set { _Encoding = value; }
        }

        string _Postdata;

        /// <summary>
        /// Post请求时要发送的Post数据
        /// </summary>
        public string Postdata
        {
            get { return _Postdata; }
            set { _Postdata = value; }
        }

        string _Cookie;

        /// <summary>
        /// 请求时的Cookie
        /// </summary>
        public string Cookie
        {
            get { return _Cookie; }
            set { _Cookie = value; }
        }

        string _Referer;

        /// <summary>
        /// 来源地址，上次访问地址
        /// </summary>
        public string Referer
        {
            get { return _Referer; }
            set { _Referer = value; }
        }

        string _CerPath = string.Empty;

        /// <summary>
        /// 证书绝对路径
        /// </summary>
        public string CerPath
        {
            get { return _CerPath; }
            set { _CerPath = value; }
        }

        /// <summary>
        /// 证书绝密码
        /// </summary>
        public string Cer_PWD
        {
            get;
            set;
        }
    }
}
