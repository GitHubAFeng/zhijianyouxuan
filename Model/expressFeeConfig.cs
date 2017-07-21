using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Hangjing.Model
{

    /// <summary>
    /// 跑腿配送费
    /// </summary>
    public class expressFeeConfigInfo
    {

        /// <summary>
        /// 自增编号
        /// </summary>
        public int fid
        {
            get;
            set;
        }

        /// <summary>
        /// 城市编号
        /// </summary>
        public int cityid
        {
            get;
            set;
        }

        /// <summary>
        /// 起步公里数
        /// </summary>
        public int basedistance
        {
            get;
            set;
        }

        /// <summary>
        /// 起步价
        /// </summary>
        public decimal basedistanceprice
        {
            get;
            set;
        }

        /// <summary>
        /// 第二阶段公里数
        /// </summary>
        public int seconddistance
        {
            get;
            set;
        }

        /// <summary>
        /// 第二阶段每公里价格
        /// </summary>
        public decimal seconddistancePerPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 第三阶梯每公里价格
        /// </summary>
        public decimal thirdDistancePerPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 夜间第一段时间开始
        /// </summary>
        public DateTime starttime1
        {
            get;
            set;
        }

        /// <summary>
        /// 夜间第一段时间结束
        /// </summary>
        public string endtime1
        {
            get;
            set;
        }

        /// <summary>
        /// 夜间第2段时间开始
        /// </summary>
        public DateTime starttime2
        {
            get;
            set;
        }

        /// <summary>
        /// 夜间第2段时间结束
        /// </summary>
        public string endtime2
        {
            get;
            set;
        }

        /// <summary>
        /// 夜间第一段时间，每单加多少元
        /// </summary>
        public int reveint1
        {
            get;
            set;
        }

        /// <summary>
        /// 夜间第二段时间，每单加多少元
        /// </summary>
        public int reveint2
        {
            get;
            set;
        }

        /// <summary>
        /// reveint3
        /// </summary>
        public int reveint3
        {
            get;
            set;
        }

        /// <summary>
        /// reveint4
        /// </summary>
        public int reveint4
        {
            get;
            set;
        }

        /// <summary>
        /// revevar1
        /// </summary>
        public string revevar1
        {
            get;
            set;
        }

        /// <summary>
        /// revevar2
        /// </summary>
        public string revevar2
        {
            get;
            set;
        }

        /// <summary>
        /// revevar3
        /// </summary>
        public string revevar3
        {
            get;
            set;
        }

        /// <summary>
        /// revefloat1
        /// </summary>
        public decimal revefloat1
        {
            get;
            set;
        }

        /// <summary>
        /// revefloat2
        /// </summary>
        public decimal revefloat2
        {
            get;
            set;
        }

        /// <summary>
        /// revedate1
        /// </summary>
        public DateTime revedate1
        {
            get;
            set;
        }

        /// <summary>
        /// revedate2
        /// </summary>
        public DateTime revedate2
        {
            get;
            set;
        }


    }
}

