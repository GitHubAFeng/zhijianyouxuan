using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 配送员结算对像
    /// </summary>
    [Serializable]
    public class deliverSettleInfo
    {
        /// <summary>
        /// 说明
        /// </summary>
        public string title
        {
            set;
            get;
        }

        /// <summary>
        /// 营业额
        /// </summary>
        public decimal allprice
        {
            set;
            get;
        }

        /// <summary>
        /// 订单数
        /// </summary>
        public int allcount
        {
            set;
            get;
        }

        /// <summary>
        /// 总工资
        /// </summary>
        public decimal allwage
        {
            get;
            set;
        }

        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal basewage
        {
            get;
            set;
        }

        /// <summary>
        /// 提成比例
        /// </summary>
        public decimal percentagewage
        {
            get;
            set;
        }


        /// <summary>
        /// 配送费
        /// </summary>
        public decimal SendFee
        {
            get;
            set;
        }

        /// <summary>
        /// 配送员应上缴款
        /// </summary>
        public decimal deliverpayweb
        {
            get;
            set;
        }


        /// <summary>
        ///  应收款 
        /// </summary>
        public decimal getmoney
        {
            get;
            set;
        }


    }
}
