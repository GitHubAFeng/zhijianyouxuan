
// EOrderFoodInfo.css:订单餐品关系模型.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-03-13

using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 实体类EOrderFood 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class FoodInOrderInfo
	{
        public FoodInOrderInfo()
		{}
		private int _dataid;
		private string _orderid;
		private int _foodid;
		private int _num;
		private string _remark;
        private decimal _foodprice;

        private string _foodname;
        private decimal _fooddiscount;
        private decimal _currentprice;

		/// <summary>
		/// 自增编号
		/// </summary>
		public int DataID
		{
			set{ _dataid=value;}
			get{return _dataid;}
		}

		/// <summary>
		/// 订单编号
		/// </summary>
		public string OrderID
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}

		/// <summary>
		/// 餐品编号 
		/// </summary>
		public int FoodID
		{
			set{ _foodid=value;}
			get{return _foodid;}
		}

		/// <summary>
		/// 餐品份数
		/// </summary>
		public int Num
		{
			set{ _num=value;}
			get{return _num;}
		}

		/// <summary>
		/// 餐品要求
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}

        /// <summary>
        /// 餐品价格
        /// </summary>
        public decimal FoodPrice
        {
            set { _foodprice = value; }
            get { return _foodprice; }
        }

        /// <summary>
        /// 餐品折扣
        /// </summary>
        public decimal FoodDiscount
        {
            get { return _fooddiscount; }
            set { _fooddiscount = value; }
        }

        /// <summary>
        /// 餐品现价
        /// </summary>
        public decimal Currentprice
        {
            get { return _currentprice; }
            set { _currentprice = value; }
        }

        /// <summary>
        /// 餐品名称
        /// </summary>
        public string FoodName
        {
            get { return _foodname; }
            set { _foodname = value; }
        }
	}
}

