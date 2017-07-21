using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// OrderCountInfo.cs:订单月份/年份/日/小时/周 统计.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-07-17

namespace Hangjing.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderCountInfo
    {
        private string _countkey;  //月份 年份 日 小时 周
        private int _countintvalue;//数据
        private decimal _countdecimalvalue;//金额
        private int _rat;
        private string _picstr;
        private int _countsendfee;//配送费
        private decimal _countdrinkprice;//饮品金额

        /// <summary>
        /// 饮品金额
        /// </summary>
        public decimal CountDrinkPrice
        {
            set { _countdrinkprice = value; }
            get { return _countdrinkprice; }
        }

        /// <summary>
        /// 配送费
        /// </summary>
        public int CountSendFee
        {
            set { _countsendfee = value; }
            get { return _countsendfee; }
        }

        /// <summary>
        /// 统计的分类 如按照日统计 则此字段是 1－31的数字 年份则是2010、2011这样的年份数字
        /// </summary>
        public string CountKey
        {
            set { _countkey = value; }
            get { return _countkey; }
        }

        /// <summary>
        /// 分类对应的数据
        /// </summary>
        public int CountIntValue
        {
            set { _countintvalue = value; }
            get { return _countintvalue; }
        }

        /// <summary>
        /// 分类对应的金额
        /// </summary>
        public decimal CountDecimalValue
        {
            set { _countdecimalvalue = value; }
            get { return _countdecimalvalue; }
        }

        /// <summary>
        /// 当前占的比例
        /// </summary>
        public int rat
        {
            set { _rat = value; }
            get { return _rat; }
        }

        /// <summary>
        /// 所占比例串
        /// </summary>
        public string picstr
        {
            set { _picstr = value; }
            get { return _picstr; }
        }
    }
}
