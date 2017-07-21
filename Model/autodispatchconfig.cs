using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Hangjing.Model
{
    /// <summary>
    /// 自动调度配置
    /// </summary>
    public class autodispatchconfigInfo
    {

        /// <summary>
        /// dataid
        /// </summary>
        public int dataid
        {
            get;
            set;
        }

        /// <summary>
        /// 0表示关闭自动调度，1表示开启
        /// </summary>
        public int isopen
        {
            get;
            set;
        }

        /// <summary>
        /// 自动调度类型：0表示未用，1，表示发给所有人，2表示发给商家3公里内的，3，表示发给最近且在线的配送员
        /// </summary>
        public int autotype
        {
            get;
            set;
        }

        /// <summary>
        /// autotype=2时，表示商家到骑士的距离
        /// </summary>
        public decimal distance
        {
            get;
            set;
        }

        /// <summary>
        /// 距离商家近的N个配送员
        /// </summary>
        public int reveint1
        {
            get;
            set;
        }

        /// <summary>
        /// 城市编号
        /// </summary>
        public int reveint2
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string revevar1
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string revevar2
        {
            get;
            set;
        }

        /// <summary>
        /// revedate
        /// </summary>
        public DateTime revedate
        {
            get;
            set;
        }

    }
}

