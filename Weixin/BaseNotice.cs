using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 微信消息基类
    /// </summary>
    public abstract class BaseNotice
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName
        {
            get;
            set;
        }

        /// <summary>
        /// 发送方帐号（一个OpenID），就是用户对应一个公众号的唯一编号
        /// </summary>
        public string FromUserName
        {
            get;
            set;
        }

        /// <summary>
        /// 消息创建时间 （整型） 
        /// </summary>
        public string CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 消息类型：text，image，location，link
        /// </summary>
        public string MsgType
        {
            get;
            set;
        }

        /// <summary>
        /// 消息id，64位整型 
        /// </summary>
        public string MsgId
        {
            get;
            set;
        }

        /// <summary>
        /// 根据xml返回对像
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public abstract BaseNotice LoadXml(string xml);
    }
}
