using Hangjing.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 处理消息基类
    /// </summary>
    public abstract class BaseHandler
    {
        protected BaseNotice notice;
        /// <summary>
        /// 微信域名
        /// </summary>
        protected string domain = "";

        public BaseHandler(BaseNotice _notice)
        {
            notice = _notice;
            domain = CacheHelper.GetWeiXinAccount().revevar2;
        }

        /// <summary>
        /// 处理消息，每个子类重写此法
        /// </summary>
        /// <returns></returns>
        public abstract string HandleNotice(HttpContext context);
        
    }
}
