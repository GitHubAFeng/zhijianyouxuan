
// EOrderFoodInfo.css:������Ʒ��ϵģ��.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-03-13

using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ʵ����EOrderFood ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// �������
		/// </summary>
		public int DataID
		{
			set{ _dataid=value;}
			get{return _dataid;}
		}

		/// <summary>
		/// �������
		/// </summary>
		public string OrderID
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}

		/// <summary>
		/// ��Ʒ��� 
		/// </summary>
		public int FoodID
		{
			set{ _foodid=value;}
			get{return _foodid;}
		}

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public int Num
		{
			set{ _num=value;}
			get{return _num;}
		}

		/// <summary>
		/// ��ƷҪ��
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}

        /// <summary>
        /// ��Ʒ�۸�
        /// </summary>
        public decimal FoodPrice
        {
            set { _foodprice = value; }
            get { return _foodprice; }
        }

        /// <summary>
        /// ��Ʒ�ۿ�
        /// </summary>
        public decimal FoodDiscount
        {
            get { return _fooddiscount; }
            set { _fooddiscount = value; }
        }

        /// <summary>
        /// ��Ʒ�ּ�
        /// </summary>
        public decimal Currentprice
        {
            get { return _currentprice; }
            set { _currentprice = value; }
        }

        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string FoodName
        {
            get { return _foodname; }
            set { _foodname = value; }
        }
	}
}

