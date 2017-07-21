using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ʵ����ETogoOrder ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ֧��ģʽ��1֧����/2����/3�˻����/4�������
        /// </summary>
        public int paymode
        {
            set { _paymode = value; }
            get { return _paymode; }
        }

        /// <summary>
        /// ֧��ʱ��
        /// </summary>
        public DateTime paytime
        {
            set { _paytime = value; }
            get { return _paytime; }
        }

        /// <summary>
        /// ֧�����
        /// </summary>
        public decimal paymoney
        {
            set { _paymoney = value; }
            get { return _paymoney; }
        }

        /// <summary>
        /// ֧�����  0 δ֧�� 1 �ɹ� 2֧��δ��� 3 ֧��ʧ��
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
        /// �����ֶ�
        /// </summary>
        public int DataID
        {
            set { _dataid = value; }
            get { return _dataid;  }
        }

		/// <summary>
		/// �������(��ǰʱ���Զ�����)
		/// </summary>
		public string OrderID
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}

		/// <summary>
		/// �û�ID
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}

		/// <summary>
		/// �û���ʵ����(�ռ���)
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}

		/// <summary>
		/// �ֻ�����
		/// </summary>
		public string Tel
		{
			set{ _tel=value;}
			get{return _tel;}
		}

		/// <summary>
		/// �Ͳ�ʱ��
		/// </summary>
		public string SentTime
		{
			set{ _senttime=value;}
            get { return _senttime; }
		}

		/// <summary>
		/// �Ͳ͵�ַ
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}

		/// <summary>
		/// ״̬(1����������/2�����ڴ�ӡ/3������ɹ�/4������ʧ��/5:�����Ѿ�ȡ��/6:�����Ѿ�ʧЧ(��ӡ����ȡ��δ������ӡ���))
		/// </summary>
		public int State
		{
			set{ _state=value;}
			get{return _state;}
		}

		/// <summary>
		/// �¶���ʱ��
		/// </summary>
		public DateTime orderTime
		{
			set{ _ordertime=value;}
			get{return _ordertime;}
		}

        /// <summary>
        /// �����ܼ�
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }

        /// <summary>
        /// ����ͳ����Ŀ
        /// </summary>
        public int OrderCount
        {
            get { return _ordercount; }
            set { _ordercount = value; }
        }

        /// <summary>
        /// �������ͳ��
        /// </summary>
        public Decimal OrderTotal
        {
            get { return _ordertotal; }
            set { _ordertotal = value; }
        }

        /// <summary>
        /// ��ȡ������ʱ�䲢���޸�״̬
        /// ������ɨ���ʱ�䣨�ж϶����Ƿ�ʧЧ��
        /// </summary>
        public DateTime SetStateTime
        {
            get { return _setstatetime; }
            set { _setstatetime = value; }
        }

        /// <summary>
        /// �̼�ID by jijunjian
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
        ///�����ּ�
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
        /// �̼�����
        /// 2010-03-15 8:47 by jijunjian
        /// </summary>
        public string TogoName
        {
            set { _togoname = value; }
            get { return _togoname; }
        }

        /// <summary>
        /// ���������Ĳ�Ʒ��Ŀ
        /// </summary>
        public int FoodCount
        {
            get { return _foodcount; }
            set { _foodcount = value; }
        }

        /// <summary>
        /// �û���
        /// </summary>
        public string CustomerName
        {
            get { return _customername; }
            set { _customername = value; }
        }

        private decimal _acountpay;
        /// <summary>
        /// �ʻ�֧�����
        /// </summary>
        public decimal acountpay
        {
            get { return _acountpay; }
            set { _acountpay = value; }
        }

	}
}

