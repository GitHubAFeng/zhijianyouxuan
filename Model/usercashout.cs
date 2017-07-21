using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Hangjing.Model
{

    /// <summary>
    /// 用户提现记录
    /// </summary>
    public class usercashoutInfo
    {

        /// <summary>
        /// cid
        /// </summary>
        public int cid
        {
            get;
            set;
        }

        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal wantmoney
        {
            get;
            set;
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int shopid
        {
            get;
            set;
        }

        /// <summary>
        /// 提现时间
        /// </summary>
        public DateTime addtime
        {
            get;
            set;
        }

        /// <summary>
        /// 状态：0表示未处理，1表示审核通过，2表示管理员拒绝,3表示商家取消 100：提现完成
        /// </summary>
        public int state
        {
            get;
            set;
        }

        /// <summary>
        /// 网站备注
        /// </summary>
        public string remark
        {
            get;
            set;
        }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime optime
        {
            get;
            set;
        }

        /// <summary>
        /// 处理管理员
        /// </summary>
        public string opuser
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

        /// <summary>
        /// 提现订单开始时间
        /// </summary>
        public DateTime starttime
        {
            get;
            set;
        }

        /// <summary>
        /// 提现订单结算时间
        /// </summary>
        public DateTime endtime
        {
            get;
            set;
        }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string TogoName
        {
            get;
            set;
        }

    }
}

