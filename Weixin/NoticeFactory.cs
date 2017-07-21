using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hangjing.Common;
using System.Reflection;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 根据消息类型，返回对像
    /// </summary>
    public class NoticeFactory
    {
        const string AssemblyPath = "Hangjing.Weixin";//用于反射
        public static BaseHandler CreateInstance(string xml)
        {
            BaseHandler handler = null;
            //解析数据
            System.Xml.XmlDocument d = new System.Xml.XmlDocument();
            d.LoadXml(xml);
            System.Xml.XmlCDataSection n = d.SelectSingleNode("/xml/MsgType").FirstChild as System.Xml.XmlCDataSection;
            HJlog.toLog("MsgType=" + n.Value);

            Type type = Type.GetType(string.Format(AssemblyPath + ".{0}," + AssemblyPath, n.Value.Trim()), false, true);
            BaseNotice noticemodel = (BaseNotice)Activator.CreateInstance(type);
               
            if (noticemodel != null)
            {
                noticemodel = noticemodel.LoadXml(xml);
                switch (noticemodel.MsgType)
                {
                    case "text":
                        handler = new TextHandler(noticemodel);
                        break;
                    case "location":
                        handler = new LocationHandler(noticemodel);
                        break;
                    case "event":
                        handler = new EventHandler(noticemodel);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                HJlog.toLog("noticemodel=mull");
            }

            return handler;
        }
    }
}
