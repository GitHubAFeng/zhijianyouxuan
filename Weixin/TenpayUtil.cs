using System;
using System.Text;
using System.Web;
using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

namespace Hangjing.Weixin
{
    /// <summary>
    /// TenpayUtil ��ժҪ˵����
    /// �����ļ�
    /// </summary>
    public class TenpayUtil
    {
        public static string charset = "UTF-8";
        public static string tenpay = "1";
        public static string partner = "";//�̻���(΢�Ź��ں��е�΢��֧���̻��ţ����ǲƸ�ͨ�ʼ��е�)
        public static string key = "";  //��Կ(PartnerKey)
        public static string appid = "";//appid
        public static string tenpay_notify = CacheHelper.GetWeiXinAccount().revevar2 + "/weixinpay/payNotifyUrl.aspx"; //֧����ɺ�Ļص�����ҳ��,*�滻��notify_url.asp����·��

        public TenpayUtil()
        {
            WeiXinAccountInfo config = CacheHelper.GetWeiXinAccount();
            partner = config.partnerid;
            key = config.apikey;
            appid = config.AppId;

        }
        /// <summary>
        /// ���������
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


        /** ���ַ�������URL���� */
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

        /** ���ַ�������URL���� */
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


        /** ȡʱ��������漴��,�滻���׵����еĺ�10λ��ˮ�� */
        public static UInt32 UnixStamp()
        {
            TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return Convert.ToUInt32(ts.TotalSeconds);
        }
        /** ȡ����� */
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
        /// ��õ�ǰҳ��ͻ��˵�IP
        /// </summary>
        /// <returns>��ǰҳ��ͻ��˵�IP</returns>
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