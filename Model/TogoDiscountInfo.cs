using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 实体类TogoDiscount 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class TogoDiscountInfo
	{
        private int _id;
        private int _togoid;
        private DateTime _starttime1;
        private DateTime _endtime;
        private decimal _range1;
        private decimal _range2;
        private decimal _digital;
        /// <summary>
        /// 编号 自增
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 商家编号
        /// </summary>
        public int togoid
        {
            set { _togoid = value; }
            get { return _togoid; }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime starttime1
        {
            set { _starttime1 = value; }
            get { return _starttime1; }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endtime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }

        /// <summary>
        /// 订单价格范围
        /// </summary>
        public decimal range1
        {
            set { _range1 = value; }
            get { return _range1; }
        }

        /// <summary>
        /// 订单价格范围
        /// </summary>
        public decimal range2
        {
            set { _range2 = value; }
            get { return _range2; }
        }

        /// <summary>
        /// 折扣
        /// </summary>
        public decimal digital
        {
            set { _digital = value; }
            get { return _digital; }
        }
	}
}

