using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    public class expressinfo
    {
        /// <summary>
        /// 取件纬度
        /// </summary>
        public string hidflat { get; set; }

        /// <summary>
        /// 取件经度
        /// </summary>
        public string hidflng { get; set; }

        /// <summary>
        /// 收件纬度
        /// </summary>
        public string hidtlat { get; set; }

        /// <summary>
        /// 收件经度
        /// </summary>
        public string hidtlng { get; set; }

        /// <summary>
        /// 距离
        /// </summary>
        public string hiddistance { get; set; }

        /// <summary>
        /// 配送费
        /// </summary>
        public string hidsendfee { get; set; }

        /// <summary>
        /// 取件地址
        /// </summary>
        public string tbAddress { get; set; }

        /// <summary>
        /// 取件电话
        /// </summary>
        public string tbTel { get; set; }

        /// <summary>
        /// 取件用户名
        /// </summary>
        public string tbUserName { get; set; }

        /// <summary>
        /// 收件地址
        /// </summary>
        public string tbOorderid { get; set; }

        /// <summary>
        /// 收件电话
        /// </summary>
        public string tbReveVar { get; set; }

        /// <summary>
        ///收件人
        /// </summary>
        public string tbcallmsg { get; set; }


        /// <summary>
        /// 取件地址详情
        /// </summary>
        public string tbAddressdetail { get; set; }

        /// <summary>
        /// 取件详情地址
        /// </summary>
        public string tbOorderiddetail { get; set; }

        /// <取件时间>
        /// 取件地址
        /// </summary>
        public string tbSentTime { get; set; }

        /// <summary>
        /// 类型 0表示代取，1表示代买
        /// </summary>
        public string tbcallcount { get; set; }

        /// <summary>
        /// 商品备注
        /// </summary>
        public string tbRemark { get; set; }

        /// <summary>
        /// 商品费用
        /// </summary>
        public string tbTotalPrice { get; set; }

        /// <summary>
        /// cityid
        /// </summary>
        public string cityid { get; set; }


    }
}
