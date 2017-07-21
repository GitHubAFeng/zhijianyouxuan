using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Hangjing.Model
{
    /// <summary>
    /// 红包配置
    /// </summary>
    public class packetconfigInfo
    {

        /// <summary>
        /// 1
        /// </summary>
        public int dataid
        {
            get;
            set;
        }

        /// <summary>
        /// 0表示关闭，1表示开启
        /// </summary>
        public int isopen
        {
            get;
            set;
        }

        /// <summary>
        /// 0:注册获得红包,1:下单获得红包
        /// </summary>
        public int autotype
        {
            get;
            set;
        }

        /// <summary>
        ///  reveint2 =0，表示随机的最大值 ，reveint2 =1，表示金额
        /// </summary>
        public decimal distance
        {
            get;
            set;
        }

        /// <summary>
        /// 红包个数
        /// </summary>
        public int reveint1
        {
            get;
            set;
        }

        /// <summary>
        /// 金额:   0:随机,1：固定
        /// </summary>
        public int reveint2
        {
            get;
            set;
        }
        /// <summary>
        /// 满多少可用
        /// </summary>
        public string revevar1
        {
            get;
            set;
        }

        /// <summary>
        /// 有效时间（天数）
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

