using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 终端实体
    /// </summary>
    public class deviceInfo
    {
        /// <summary>
        /// 终端编号
        /// </summary>
        public string id
        {
            set;
            get;
        }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string address
        {
            set;
            get;
        }

        /// <summary>
        /// 是指打印机的激活日期
        /// </summary>
        public string since
        {
            set;
            get;
        }

        /// <summary>
        /// 是指打印机对应的IMSI编码（打印机本身采用的移动SIM卡唯一识别号）。
        /// </summary>
        public string simCode
        {
            set;
            get;
        }

        /// <summary>
        /// 是指打印机最后一次通信连接发生的时刻。
        /// </summary>
        public string lastConnected 
        {
            set;
            get;
        }

        /// <summary>
        /// 是指打印机的连接状态，包括： 离线 ， 在线 。
        /// </summary>
        public string deviceStatus
        {
            set;
            get;
        }

        /// <summary>
        /// 是指打印纸张的状态，包括 正常 或 缺纸 。
        /// </summary>
        public string paperStatus
        {
            set;
            get;
        }
    }
}
