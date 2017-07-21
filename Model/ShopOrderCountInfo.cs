using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 商家每日销售金额统计
    /// </summary>
    public class ShopOrderCountInfo
    {
        private DateTime _OrderDate;
        private int _OrderCount;
        private decimal _countdecimalvalue;

        /// <summary>
        /// 统计数据日期
        /// </summary>
        public DateTime OrderDate
        {
            set { _OrderDate = value; }
            get { return _OrderDate; }
        }

        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderCount
        {
            set { _OrderCount = value; }
            get { return _OrderCount; }
        }

        /// <summary>
        /// 当日订单总金额
        /// </summary>
        public decimal CountDecimalValue
        {
            set { _countdecimalvalue = value; }
            get { return _countdecimalvalue; }
        }
    }
}
