using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;

namespace Hangjing.Model
{
    /// <summary>
    /// 用户提现的帐号信息（银行支付宝等）
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class userCashAcountInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 商家编号
        /// </summary>
        public int shopid
        {
            get;
            set;
        }

    
        /// <summary>
        /// 开户行
        /// </summary>
        [JsonProperty]
        public string bankname
        {
            get;
            set;
        }

        /// <summary>
        /// 开户名
        /// </summary>
        [JsonProperty]
        public string bankusername
        {
            get;
            set;
        }

        /// <summary>
        /// 支付宝帐号
        /// </summary>
        [JsonProperty]
        public string aliaccount
        {
            get;
            set;
        }

        /// <summary>
        /// 支付宝姓名
        /// </summary>
        [JsonProperty]
        public string aliname
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark
        {
            get;
            set;
        }

        /// <summary>
        /// 微信账号
        /// </summary>
        [JsonProperty]
        public string opuser
        {
            get;
            set;
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime optime
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
        /// reveint5
        /// </summary>
        public int reveint5
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
        /// revefloat3
        /// </summary>
        public decimal revefloat3
        {
            get;
            set;
        }

        /// <summary>
        /// 银行卡号
        /// </summary>
        [JsonProperty]
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
        /// revevar4
        /// </summary>
        public string revevar4
        {
            get;
            set;
        }

        /// <summary>
        /// revevar5
        /// </summary>
        public string revevar5
        {
            get;
            set;
        }

        /// <summary>
        /// revetext
        /// </summary>
        public string revetext
        {
            get;
            set;
        }

    }
}

