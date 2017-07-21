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

namespace Hangjing.WebCommon
{
    /// <summary>
    ///HJSendMsg 的摘要说明
    /// </summary>
    public class SendMsg
    {
        private static readonly string userName = CacheUtility.GetSetValue(67);//账号
        private static readonly string passWord = CacheUtility.GetSetValue(68);//密码
        public static string PostUrl = System.Configuration.ConfigurationManager.AppSettings["WebReference.Service.PostUrl"];



        /// <summary>
        /// 发送验证码短信
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="type">类型 1.注册 2.登录  3.其他</param>
        /// <returns></returns>
        public static int SendValidCode(string mobile, int type)
        {
            HttpContext context = HttpContext.Current;
            string code = GetRandomOnlyNum(6);
            switch (type)
            {
                case 1: context.Session["RegisterGsmCode"] = code; break;//注册
                case 2: context.Session["LoginGsmCode"] = code; break;//登录
                case 3: context.Session["GsmCode"] = code; break;//其他 比如提交订单
            }
            context.Session.Timeout = 10;
            string cont = "您的验证码是：" + code + "。请不要把验证码泄露给其他人。如非本人操作，可不用理会！";
            return send(mobile, cont, 1);
        }


        /// <summary>
        /// 发送验证码短信
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        public static int SendValidCode(string mobile, string code)
        {
            string cont = "您的验证码是：" + code + "。请不要把验证码泄露给其他人。如非本人操作，可不用理会！";
            return send(mobile, cont, 1);
        }


        /// <summary>
        ///  发送短信 不限制次数
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
            Regex rx = new Regex(@"^[0-9]*$");
            if (!rx.IsMatch(mobile) || mobile.Length != 11)
            {
                return 0;
            }

            if ((islimity == 1) && !checkphone(mobile))
            {
                HJlogx.toLog("同一号码，ip一天内发送5次以上：" + mobile);
                return 0;
            }


            int status = 1;
            string statusMsg = "";

            string postStrTpl = "account={0}&password={1}&mobile={2}&content={3}";

            UTF8Encoding encoding = new UTF8Encoding();
            byte[] postData = encoding.GetBytes(string.Format(postStrTpl, userName, passWord, mobile, content));

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


            #region 添加发送记录
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
            #endregion

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


        /// <summary>
        /// 发送post请求 并提交数据
        /// </summary>
        /// <param name="purl">要提交到的URL</param>
        /// <param name="str">要提交的数据</param>
        /// <returns></returns>
        private static string PostData(string purl, string str)
        {
            try
            {
                byte[] data = System.Text.Encoding.GetEncoding("utf-8").GetBytes(str);
                // 准备请求 
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(purl);

                //设置超时
                req.Timeout = 30000;
                req.Method = "Post";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = data.Length;
                Stream stream = req.GetRequestStream();
                // 发送数据 
                stream.Write(data, 0, data.Length);
                stream.Close();

                HttpWebResponse rep = (HttpWebResponse)req.GetResponse();
                Stream receiveStream = rep.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, encode);

                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                StringBuilder sb = new StringBuilder("");
                while (count > 0)
                {
                    String readstr = new String(read, 0, count);
                    sb.Append(readstr);
                    count = readStream.Read(read, 0, 256);
                }

                rep.Close();
                readStream.Close();

                return sb.ToString();

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// 得到随机数
        /// </summary>
        /// <param name="len">随机数长度</param>
        /// <returns></returns>
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



        /// <summary>
        /// 充值发短信 用于APP支付宝充值  本项目暂没用
        /// </summary>
        /// <param name="tel"></param>
        /// <param name="givemoney"></param>
        /// <returns></returns>
        public static int recharegeSuccess(string tel, string recharegemoney)
        {
            string cont = "恭喜您充值成功，充值金额为：" + recharegemoney + "元，请到个人账户余额中查看。";
            return send(tel, cont, 0);
        }


    }
}
