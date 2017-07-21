using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System.Web;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 微信打钱，或者发红包给用户
    /// </summary>
    public class WechatPay2User
    {
        protected HttpContext context;
        public RequestHandler rh = null;
        public WechatPay2User(HttpContext context)
        {
            this.context = context;
            new TenpayUtil();

            rh = new RequestHandler(context);
            rh.init();

        }

        /// <summary>
        /// 根据openid给用户支付
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="requestid"></param>
        /// <param name="mondey"></param>
        /// <returns></returns>
        public bool Pay(string openid, int requestid, decimal mondey)
        {

            if (openid.Length == 0)
            {
                return false;
            }
            rh.setParameter("wxappid", TenpayUtil.appid);
            rh.setParameter("mch_id", TenpayUtil.partner);
            rh.setParameter("nonce_str", TenpayUtil.getNoncestr());
            rh.setParameter("re_openid", openid);
            rh.setParameter("client_ip", TenpayUtil.GetIP());
            rh.setParameter("mch_billno", TenpayUtil.partner+DateTime.Now.ToString("yyyyMMdd")+requestid.ToString("0000000000"));
            rh.setParameter("total_amount", (mondey * 100).ToString("#0"));
            rh.setParameter("send_name", CacheHelper.GetSetValue(2));
            rh.setParameter("total_num", "1");
            rh.setParameter("wishing", "恭喜发财");
            rh.setParameter("act_name", "自动提现");
            rh.setParameter("remark", "余额自动提现");

            rh.setKey(TenpayUtil.key);

            return query();

        }

        /// <summary>
        /// 根据openid给用户支付
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="requestid"></param>
        /// <param name="mondey"></param>
        /// <returns></returns>
        public bool Pay(int userid, int requestid, decimal mondey)
        {
            ECustomerInfo user = new ECustomer().GetModel(userid);
            Pay(user.PayPWDQuestion, requestid, mondey);


            return true;
        }

        /// <summary>
        /// 发红包
        /// </summary>
        /// <returns></returns>
        public bool query()
        {
            rh.getRequestURL();
            string Postdata = rh.parseXML();
            string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";
            HttpHelper objhttp = new HttpHelper();
            objhttp.isToLower = false;
            HttpItem objHttpItem = new HttpItem()
            {
                URL = url,
                Encoding = "utf-8",
                Method = "post",
                CerPath = @"D:\wwwroot\cert\apiclient_cert.p12",
                Cer_PWD = TenpayUtil.partner,
                Postdata = Postdata
            };

            string returnmsg = objhttp.GetHtml(objHttpItem);

            Hangjing.AppLog.AppLog.Info("Postdata=" + Postdata + "\r\nreturnmsg=" + returnmsg);



            System.Xml.XmlDocument d = new System.Xml.XmlDocument();
            d.LoadXml(returnmsg);
            System.Xml.XmlCDataSection n = d.SelectSingleNode("/xml/return_code").FirstChild as System.Xml.XmlCDataSection;
            System.Xml.XmlCDataSection result_code = d.SelectSingleNode("/xml/result_code").FirstChild as System.Xml.XmlCDataSection;


            if (n != null && n.Value == "SUCCESS" && result_code != null && result_code.Value == "SUCCESS")
            {
                return true;

            }

            return false;

        }


    }
}
