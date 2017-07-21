using System;
using System.Text;
using System.Web;
using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

namespace Hangjing.Weixin
{
    /// <summary>
    /// TenpayUtil 的摘要说明。
    /// 配置文件
    /// </summary>
    public class TenpayUtil
    {
        public static string charset = "UTF-8";
        public static string tenpay = "1";
        public static string partner = "";//商户号(微信公众号中的微信支付商户号，不是财富通邮件中的)
        public static string key = "";  //密钥(PartnerKey)
        public static string appid = "";//appid
        public static string tenpay_notify = CacheHelper.GetWeiXinAccount().revevar2 + "/weixinpay/payNotifyUrl.aspx"; //支付完成后的回调处理页面,*替换成notify_url.asp所在路径

        public TenpayUtil()
        {
            WeiXinAccountInfo config = CacheHelper.GetWeiXinAccount();
            partner = config.partnerid;
            key = config.apikey;
            appid = config.AppId;

        }
        /// <summary>
        /// 生成随机串
        /// </summary>
        /// <returns></returns>
        public static string getNoncestr()
        {
            Random random = new Random();
            return Utils.GetMD5(random.Next(1000).ToString(), charset);
        }


        public static string getTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }


        /** 对字符串进行URL编码 */
        public static string UrlEncode(string instr, string charset)
        {
            //return instr;
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;
                res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));
                return res;
            }
        }

        /** 对字符串进行URL解码 */
        public static string UrlDecode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;
                res = HttpUtility.UrlDecode(instr, Encoding.UTF8);
                return res;

            }
        }


        /** 取时间戳生成随即数,替换交易单号中的后10位流水号 */
        public static UInt32 UnixStamp()
        {
            TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return Convert.ToUInt32(ts.TotalSeconds);
        }
        /** 取随机数 */
        public static string BuildRandomStr(int length)
        {
            Random rand = new Random();

            int num = rand.Next();

            string str = num.ToString();

            if (str.Length > length)
            {
                str = str.Substring(0, length);
            }
            else if (str.Length < length)
            {
                int n = length - str.Length;
                while (n > 0)
                {
                    str.Insert(0, "0");
                    n--;
                }
            }

            return str;
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

    }
}