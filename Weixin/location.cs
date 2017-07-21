using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 地理位置消息
    /// </summary>
    public class location : BaseNotice
    {
        /// <summary>
        /// 根据xml返回对像
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public override BaseNotice LoadXml(string xml)
        {
            location notice = new location();

            //<xml>
            //<ToUserName><![CDATA[toUser]]></ToUserName>
            //<FromUserName><![CDATA[fromUser]]></FromUserName> 
            //<CreateTime>1348831860</CreateTime>
            //<MsgType><![CDATA[text]]></MsgType>
            //<Location_X>23.134521</Location_X>
            //<Location_Y>113.358803</Location_Y>
            //<Scale>20</Scale>
            //<Label><![CDATA[位置信息]]></Label>
            //<MsgId>1234567890123456</MsgId>
            //</xml>

            System.Xml.XmlDocument d = new System.Xml.XmlDocument();
            d.LoadXml(xml);
            System.Xml.XmlCDataSection n = d.SelectSingleNode("/xml/ToUserName").FirstChild as System.Xml.XmlCDataSection;

            System.Xml.XmlNode textdata = d.ChildNodes[0];

            notice.ToUserName = n.Value;

            n = d.SelectSingleNode("/xml/FromUserName").FirstChild as System.Xml.XmlCDataSection;
            notice.FromUserName = n.Value;

            //n = d.SelectSingleNode("/xml/CreateTime").FirstChild as System.Xml.XmlCDataSection;
            //notice.CreateTime = n.Value;

            n = d.SelectSingleNode("/xml/MsgType").FirstChild as System.Xml.XmlCDataSection;
            notice.MsgType = n.Value;

            //n = d.SelectSingleNode("/xml/MsgId").FirstChild as System.Xml.XmlCDataSection;
            //notice.MsgId = n.Value;

            textdata = d.SelectSingleNode("/xml/Location_X").FirstChild as System.Xml.XmlNode;
            notice.Location_X = textdata.Value;

            textdata = d.SelectSingleNode("/xml/Location_Y").FirstChild as System.Xml.XmlNode;
            notice.Location_Y = textdata.Value;

            textdata = d.SelectSingleNode("/xml/Label").FirstChild as System.Xml.XmlNode;
            notice.Label = textdata.Value;

            return notice;
        }

        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Location_X
        {
            get;
            set;
        }

        /// <summary>
        /// 地理位置经度 
        /// </summary>
        public string Location_Y
        {
            get;
            set;
        }

        /// <summary>
        /// 地图缩放大小 
        /// </summary>
        public string Scale
        {
            get;
            set;
        }

        /// <summary>
        /// 地理位置信息 
        /// </summary>
        public string Label
        {
            get;
            set;
        }
    }
}
