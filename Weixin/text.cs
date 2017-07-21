using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class text : BaseNotice
    {
        /// <summary>
        /// 根据xml返回对像
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public override BaseNotice LoadXml(string xml)
        {
            text notice = new text();

            //<xml>
            //<ToUserName><![CDATA[toUser]]></ToUserName>
            //<FromUserName><![CDATA[fromUser]]></FromUserName> 
            //<CreateTime>1348831860</CreateTime>
            //<MsgType><![CDATA[text]]></MsgType>
            //<Content><![CDATA[this is a test]]></Content>
            //<MsgId>1234567890123456</MsgId>
            //</xml>

            System.Xml.XmlDocument d = new System.Xml.XmlDocument();
            d.LoadXml(xml);
            System.Xml.XmlCDataSection n = d.SelectSingleNode("/xml/ToUserName").FirstChild as System.Xml.XmlCDataSection;

            notice.ToUserName = n.Value;

            n = d.SelectSingleNode("/xml/FromUserName").FirstChild as System.Xml.XmlCDataSection;
            notice.FromUserName = n.Value;

            //n = d.SelectSingleNode("/xml/CreateTime").FirstChild as System.Xml.XmlCDataSection;
            //notice.CreateTime = n.Value;

            n = d.SelectSingleNode("/xml/MsgType").FirstChild as System.Xml.XmlCDataSection;
            notice.MsgType = n.Value;

            n = d.SelectSingleNode("/xml/Content").FirstChild as System.Xml.XmlCDataSection;
            notice.Content = n.Value;

            //n = d.SelectSingleNode("/xml/MsgId").FirstChild as System.Xml.XmlCDataSection;
            //notice.MsgId = n.Value;


            return notice;
        }

        /// <summary>
        /// 消息内容  
        /// </summary>
        public string Content
        {
            get;
            set;
        }
    }
}
