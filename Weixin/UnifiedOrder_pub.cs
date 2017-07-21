/*********************************************************************
 * CopyRight (c) 2009-2014 HangJing Teconology. All Rights Reserved.
 * FileName : 
 * Function : 
 * Created by jijunjian at 2014-05-24 15:18:46.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Xml;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 统一支付接口类 
    /// 统一支付接口，可接受 JSAPI/NATIVE/APP 下预支付订单，返回预支付订单号
    /// </summary>
    public class UnifiedOrder_pub
    {
        protected HttpContext context;
        public string prepay_id = "";//微信生成的预支付 ID，用于后续接口调用中使用
        public string code_url = ""; //可将该参数值生成二维码展示出来进行扫码支付 
        public RequestHandler rh = null;

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="callbackurl"></param>
        public UnifiedOrder_pub(HttpContext _context)
        {
            context = _context;
            rh = new RequestHandler(context);
            rh.init();
        }


        /// <summary>
        /// 获取微信生成的预支付 ID，用于后续接口调用中使用
        /// </summary>
        /// <returns></returns>
        public void getPrepayId()
        {
            rh.getRequestURL();
            string Postdata = rh.parseXML();
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            HttpHelper objhttp = new HttpHelper();
            objhttp.isToLower = false;
            HttpItem objHttpItem = new HttpItem()
            {
                URL = url,
                Encoding = "utf-8",
                Method = "post",
                Postdata = Postdata,
                UserAgent = context.Request.UserAgent,
            };
            string returnmsg = objhttp.GetHtml(objHttpItem);


            // Hangjing.Common.HJlog.toLog("Postdata=" + Postdata);

            Hangjing.Common.HJlog.toLog("Postdata=" + Postdata + "\r\nreturnmsg=" + returnmsg);

            System.Xml.XmlDocument d = new System.Xml.XmlDocument();
            d.LoadXml(returnmsg);
            System.Xml.XmlCDataSection n = d.SelectSingleNode("/xml/return_code").FirstChild as System.Xml.XmlCDataSection;
            System.Xml.XmlCDataSection result_code = d.SelectSingleNode("/xml/result_code").FirstChild as System.Xml.XmlCDataSection;


            if (n != null && n.Value == "SUCCESS" && result_code != null && result_code.Value == "SUCCESS")
            {
                prepay_id = (d.SelectSingleNode("/xml/prepay_id").FirstChild as System.Xml.XmlCDataSection).Value;

                XmlNode code_urlnode = d.SelectSingleNode("/xml/code_url ");
                if (code_urlnode != null)
                {
                    code_url = (code_urlnode.FirstChild as System.Xml.XmlCDataSection).Value;
                }

            }

        }

    }
}
