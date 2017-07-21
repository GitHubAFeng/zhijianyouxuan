using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using System.Web.Security;
using System.Collections;
using Hangjing.Common;
using System.Reflection;
using System.IO;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 此类主要处理微信相关功能
    /// </summary>
    public class weixinHelper
    {
        const string token = "ihangjing";//微信平台设置的
        const string AssemblyPath = "Hangjing.Weixin";//用于反射
        protected HttpContext context;

        public weixinHelper(HttpContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// 通过签名，判断是否来自微信的请求,如果是返回 echostr，否则返回error
        ///  加密/校验流程：
        ///  1. 将token、timestamp、nonce三个参数进行字典序排序
        ///  2. 将三个参数字符串拼接成一个字符串进行sha1加密
        ///  3. 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信
        /// </summary>
        /// <returns></returns>
        public string isValidRequest()
        {
            string flag = "error";

            NameValueCollection coll;
            coll = context.Request.Params;
            String[] requestarr = coll.AllKeys;

            string signature = coll["signature"];
            HJlog.toLog("signature：" + signature);

            string[] Sortedstr = { token, coll["nonce"], coll["timestamp"] };
            Array.Sort(Sortedstr);//字典排序
            string originalstr = string.Join("", Sortedstr);

            HJlog.toLog("未加密前：" + originalstr.ToString());

            string encryptstr = FormsAuthentication.HashPasswordForStoringInConfigFile(originalstr.ToString(), "SHA1").ToLower();
            HJlog.toLog("加密后：" + encryptstr);

            if (signature == encryptstr)
            {
                flag = context.Request.QueryString["echostr"];
            }
            HJlog.toLog(flag);
            return flag;
        }

        /// <summary>
        /// 是否是接入验证（以get方式，有4个参数），还是消息
        /// </summary>
        /// <returns></returns>
        public bool isJoin()
        {
            bool flag = true;
            String[] requestarr = GetPostParams();
            if (((IList)requestarr).Contains("signature") && ((IList)requestarr).Contains("timestamp") && ((IList)requestarr).Contains("nonce") && context.Request.QueryString["echostr"] != null)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }



            return flag;
        }

        /// <summary>
        /// 根据接到的信息，返回内容
        /// </summary>
        /// <returns></returns>
        public string HandleData()
        {
            string userdata = reciveData();

            string backmsg = "";

            BaseHandler handler = NoticeFactory.CreateInstance(userdata);
            if (handler != null)
            {
                backmsg = handler.HandleNotice(context);

                HJlog.toLog("handler != null " + backmsg);
            }
            else
            {
                HJlog.toLog("handler == null ");
            }


            return backmsg.ToString();
        }

        /// <summary>
        /// 返回微信post的参数 
        /// </summary>
        /// <returns></returns>
        private String[] GetPostParams()
        {
            NameValueCollection coll;
            coll = context.Request.Params;
            String[] requestarr = coll.AllKeys;
            return requestarr;
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <returns></returns>
        private string reciveData()
        {
            string data = "";

            var inputStream = context.Request.InputStream;

            var strLen = Convert.ToInt32(inputStream.Length);

            var strArr = new byte[strLen];

            inputStream.Read(strArr, 0, strLen);

            data = Encoding.UTF8.GetString(strArr);
            HJlog.toLog("微信服务器来的消息：" + data);
            return data;
        }

        /// <summary>
        /// 冒泡排序法
        /// 按照字母序列从a到z的顺序排列
        /// </summary>
        private static string[] BubbleSort(string[] r)
        {
            int i, j; //交换标志 
            string temp;

            bool exchange;

            for (i = 0; i < r.Length; i++) //最多做R.Length-1趟排序 
            {
                exchange = false; //本趟排序开始前，交换标志应为假

                for (j = r.Length - 2; j >= i; j--)
                {
                    if (System.String.CompareOrdinal(r[j + 1], r[j]) < 0)　//交换条件
                    {
                        temp = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = temp;

                        exchange = true; //发生了交换，故将交换标志置为真 
                    }
                }

                if (!exchange) //本趟排序未发生交换，提前终止算法 
                {
                    break;
                }
            }
            return r;
        }


    }
}
