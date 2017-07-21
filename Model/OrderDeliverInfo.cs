using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
	/// <summary>
	/// ʵ����OrderDeliver ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	public class OrderDeliverInfo
	{
		private int _dataid;
        private string _orderid;
		private int _deliverid;
		private string _delivername;
		private string _dispatcher;
		private DateTime _dispatchtime;
		private DateTime _deliverytime;
		private string _section;
		private int _inve1;
		private string _inve2;
       
		/// <summary>
		/// 
		/// </summary>
		public int DataId
		{
			set{ _dataid=value;}
			get{return _dataid;}
		}

        /// <summary>
        /// �����ı��
        /// </summary>
        public string Orderid
        {
            get { return _orderid; }
            set { _orderid = value; }
        }

		/// <summary>
		/// �ӳ����
		/// </summary>
		public int DeliverId
		{
			set{ _deliverid=value;}
			get{return _deliverid;}
		}

		/// <summary>
        /// �ӳ�����
		/// </summary>
		public string DeliverName
		{
			set{ _delivername=value;}
			get{return _delivername;}
		}

		/// <summary>
		/// ����Ա����
		/// </summary>
		public string Dispatcher
		{
			set{ _dispatcher=value;}
			get{return _dispatcher;}
		}

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime DispatchTime
		{
			set{ _dispatchtime=value;}
			get{return _dispatchtime;}
		}

		/// <summary>
		/// ���Ϳ�ʼʱ��
		/// </summary>
		public DateTime DeliveryTime
		{
			set{ _deliverytime=value;}
			get{return _deliverytime;}
		}

		/// <summary>
		/// δ��
		/// </summary>
		public string Section
		{
			set{ _section=value;}
			get{return _section;}
		}

		/// <summary>
		/// ���Ա����������Ԥ��ʱ��
		/// </summary>
		public int Inve1
		{
			set{ _inve1=value;}
			get{return _inve1;}
		}

		/// <summary>
		/// ����Ա��ţ�
		/// </summary>
		public string Inve2
		{
			set{ _inve2=value;}
			get{return _inve2;}
		}

        private DateTime _OverTime;
        /// <summary>
        /// �������ʱ��
        /// </summary>
        public DateTime OverTime
        {
            set { _OverTime = value; }
            get { return _OverTime; }
        }

        private int _usesecond;
        /// <summary>
        /// ��ʱ���֣�
        /// </summary>
        public int usesecond
        {
            set { _usesecond = value; }
            get { return _usesecond; }
        }



	}
}

