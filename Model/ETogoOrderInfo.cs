using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 实体类ETogoOrder 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class ETogoOrderInfo
	{
        public ETogoOrderInfo()
		{}
        private int _dataid;
		private string _orderid;
		private int _userid;
		private string _username;
		private string _tel;
		private string _senttime;
		private string _address;
		private int _state;
        private int _togoid;
		private DateTime _ordertime;
        private decimal _totalprice;
        private DateTime _setstatetime;
        private decimal _currentprice;

        private int _foodcount;
        private int _ordercount;
        private decimal _ordertotal;

        private string _togoname;
        private string _customername;
        private string _togotel;

        private int _paymode;
        private DateTime _paytime;
        private decimal _paymoney;
        private int _paystate;

        /// <summary>
        /// 支付模式（1支付宝/2银联/3账户余额/4货到付款）
        /// </summary>
        public int paymode
        {
            set { _paymode = value; }
            get { return _paymode; }
        }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime paytime
        {
            set { _paytime = value; }
            get { return _paytime; }
        }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal paymoney
        {
            set { _paymoney = value; }
            get { return _paymoney; }
        }

        /// <summary>
        /// 支付结果  0 未支付 1 成功 2支付未完成 3 支付失败
        /// </summary>
        public int paystate
        {
            set { _paystate = value; }
            get { return _paystate; }
        }

        public string TogoTel
        {
            get { return _togotel; }
            set { _togotel = value; }
        }

        /// <summary>
        /// 自增字段
        /// </summary>
        public int DataID
        {
            set { _dataid = value; }
            get { return _dataid;  }
        }

		/// <summary>
		/// 订单编号(当前时间自动生成)
		/// </summary>
		public string OrderID
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}

		/// <summary>
		/// 用户ID
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}

		/// <summary>
		/// 用户真实名称(收件人)
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}

		/// <summary>
		/// 手机号码
		/// </summary>
		public string Tel
		{
			set{ _tel=value;}
			get{return _tel;}
		}

		/// <summary>
		/// 送餐时间
		/// </summary>
		public string SentTime
		{
			set{ _senttime=value;}
            get { return _senttime; }
		}

		/// <summary>
		/// 送餐地址
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}

		/// <summary>
		/// 状态(1：新增订单/2：正在打印/3：处理成功/4：处理失败/5:订单已经取消/6:订单已经失效(打印机获取后未反馈打印结果))
		/// </summary>
		public int State
		{
			set{ _state=value;}
			get{return _state;}
		}

		/// <summary>
		/// 下订单时间
		/// </summary>
		public DateTime orderTime
		{
			set{ _ordertime=value;}
			get{return _ordertime;}
		}

        /// <summary>
        /// 订单总价
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }

        /// <summary>
        /// 订单统计数目
        /// </summary>
        public int OrderCount
        {
            get { return _ordercount; }
            set { _ordercount = value; }
        }

        /// <summary>
        /// 订单金额统计
        /// </summary>
        public Decimal OrderTotal
        {
            get { return _ordertotal; }
            set { _ordertotal = value; }
        }

        /// <summary>
        /// 读取订单的时间并且修改状态
        /// 订单被扫描的时间（判断订单是否失效）
        /// </summary>
        public DateTime SetStateTime
        {
            get { return _setstatetime; }
            set { _setstatetime = value; }
        }

        /// <summary>
        /// 商家ID by jijunjian
        /// </summary>
        public int TogoID
        {
            set
            {
                _togoid = value;
            }
            get
            {
                return _togoid;
            }
        }

        /// <summary>
        ///订单现价
        /// </summary>
        public decimal Currentprice
        {
            set
            {
                _currentprice = value;
            }
            get
            {
                return _currentprice;
            }
        }

        /// <summary>
        /// 商家名称
        /// 2010-03-15 8:47 by jijunjian
        /// </summary>
        public string TogoName
        {
            set { _togoname = value; }
            get { return _togoname; }
        }

        /// <summary>
        /// 订单包含的菜品数目
        /// </summary>
        public int FoodCount
        {
            get { return _foodcount; }
            set { _foodcount = value; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string CustomerName
        {
            get { return _customername; }
            set { _customername = value; }
        }

        private decimal _acountpay;
        /// <summary>
        /// 帐户支付金额
        /// </summary>
        public decimal acountpay
        {
            get { return _acountpay; }
            set { _acountpay = value; }
        }

	}
}

