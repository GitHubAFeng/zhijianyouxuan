using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Hangjing.Model
{
    /// <summary>
    /// 系统促销配置
    /// </summary>
    public class webPromotionConfigInfo
    {

        /// <summary>
        /// 编号
        /// </summary>
        public int pId
        {
            get;
            set;
        }

        /// <summary>
        /// 商家编号，0表示系统配置，否则表示商家单独配置
        /// </summary>
        public int shopid
        {
            get;
            set;
        }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime startdate
        {
            get;
            set;
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime enddate
        {
            get;
            set;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime starttime
        {
            get;
            set;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endtime
        {
            get;
            set;
        }

        /// <summary>
        /// 类别，对应promotiontype.status
        /// </summary>
        public int ptype
        {
            get;
            set;
        }

        /// <summary>
        /// 是否开户，1表示是，0表示否
        /// </summary>
        public int isopen
        {
            get;
            set;
        }

        /// <summary>
        /// 未用
        /// </summary>
        public decimal freeSendFee
        {
            get;
            set;
        }

        /// <summary>
        /// 满多少优惠（或者是提前多少分钟）
        /// </summary>
        public int overmoney
        {
            get;
            set;
        }

        /// <summary>
        /// 立减多少元（首单减，满减，提前减）
        /// </summary>
        public int minusmoney
        {
            get;
            set;
        }

        /// <summary>
        /// reveint1
        /// </summary>
        public int reveint1
        {
            get;
            set;
        }

        /// <summary>
        /// reveint2
        /// </summary>
        public int reveint2
        {
            get;
            set;
        }

        /// <summary>
        /// 促销标题
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
        /// 标题
        /// </summary>
        public string title
        {
            get;
            set;
        }

        /// <summary>
        /// 链接
        /// </summary>
        public string url
        {
            get;
            set;
        }

    }
}

