using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 事件消息
    /// </summary>
    public class Event : BaseNotice
    {
        /// <summary>
        /// 根据xml返回对像
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public override BaseNotice LoadXml(string xml)
        {
            Event notice = new Event();

            //<xml><ToUserName><![CDATA[toUser]]></ToUserName>
            //<FromUserName><![CDATA[FromUser]]></FromUserName>
            //<CreateTime>123456789</CreateTime>
            //<MsgType><![CDATA[event]]></MsgType>
            //<Event><![CDATA[EVENT]]></Event>
            //<EventKey><![CDATA[EVENTKEY]]></EventKey>
            //</xml>



            System.Xml.XmlDocument d = new System.Xml.XmlDocument();
            d.LoadXml(xml);
            System.Xml.XmlCDataSection n = d.SelectSingleNode("/xml/ToUserName").FirstChild as System.Xml.XmlCDataSection;

            notice.ToUserName = n.Value;

            n = d.SelectSingleNode("/xml/FromUserName").FirstChild as System.Xml.XmlCDataSection;
            notice.FromUserName = n.Value;


            n = d.SelectSingleNode("/xml/MsgType").FirstChild as System.Xml.XmlCDataSection;
            notice.MsgType = n.Value;

            n = d.SelectSingleNode("/xml/Event").FirstChild as System.Xml.XmlCDataSection;
            notice.EventType = n.Value;

            XmlNode node = d.SelectSingleNode("/xml/EventKey");
            if (node != null)
            {
                n = node.FirstChild as System.Xml.XmlCDataSection;
                notice.EventKey = n.Value;
            }

            node = d.SelectSingleNode("/xml/Latitude");
            if (node != null)
            {
                node = node.FirstChild as System.Xml.XmlNode;
                notice.Latitude = node.Value;
            }

            node = d.SelectSingleNode("/xml/Longitude");
            if (node != null)
            {
                node = node.FirstChild as System.Xml.XmlNode;
                notice.Longitude = node.Value;
            }

            return notice;
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应   
        /// </summary>
        public string EventKey
        {
            get;
            set;
        }

        /// <summary>
        /// 事件类型,subscribe(订阅)、unsubscribe(取消订阅) ,click(点击)，LOCATION（自动定位）
        /// </summary>
        public string EventType
        {
            get;
            set;
        }

        /// <summary>
        /// LOCATION（自动定位）时纬度
        /// </summary>
        public string Latitude
        {
            get;
            set;
        }

        /// <summary>
        /// LOCATION（自动定位）时经度
        /// </summary>
        public string Longitude
        {
            get;
            set;
        }
    }
}
