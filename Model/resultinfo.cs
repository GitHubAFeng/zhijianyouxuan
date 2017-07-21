using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 普通判断返回结果实体
    /// </summary>
    [Serializable]
    public class resultinfo
    {
        /// <summary>
        /// 返回状态：1表示正常，其他状态具体说明
        /// </summary>
        public int status
        {
            set;
            get;
        }

        /// <summary>
        /// 友好提示
        /// </summary>
        public string message
        {
            set;
            get;
        }

        /// <summary>
        /// 返回数据
        /// </summary>
        public string data
        {
            set;
            get;
        }

        /// <summary>
        /// 最低使用价
        /// </summary>
        public string moneyline
        {
            set;
            get;
        }

    }
}
