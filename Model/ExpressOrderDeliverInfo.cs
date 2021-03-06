using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
	/// <summary>
	/// 跑腿订单调度信息
	/// </summary>
    public class ExpressOrderDeliverInfo
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
        /// 订单的编号，对应ExpressOrder.dataid
        /// </summary>
        public string Orderid
        {
            get { return _orderid; }
            set { _orderid = value; }
        }

		/// <summary>
		/// 配送员得编号
		/// </summary>
		public int DeliverId
		{
			set{ _deliverid=value;}
			get{return _deliverid;}
		}

		/// <summary>
		/// 派送员姓名
		/// </summary>
		public string DeliverName
		{
			set{ _delivername=value;}
			get{return _delivername;}
		}

		/// <summary>
		/// 调度员姓名
		/// </summary>
		public string Dispatcher
		{
			set{ _dispatcher=value;}
			get{return _dispatcher;}
		}

		/// <summary>
		/// 调度时间
		/// </summary>
		public DateTime DispatchTime
		{
			set{ _dispatchtime=value;}
			get{return _dispatchtime;}
		}

		/// <summary>
		/// 派送开始时间
		/// </summary>
		public DateTime DeliveryTime
		{
			set{ _deliverytime=value;}
			get{return _deliverytime;}
		}

		/// <summary>
		/// 区域
		/// </summary>
		public string Section
		{
			set{ _section=value;}
			get{return _section;}
		}

		/// <summary>
		/// 快递员反馈的派送预计时间
		/// </summary>
		public int Inve1
		{
			set{ _inve1=value;}
			get{return _inve1;}
		}

		/// <summary>
		/// 备用string
		/// </summary>
		public string Inve2
		{
			set{ _inve2=value;}
			get{return _inve2;}
		}

        private DateTime _OverTime;
        /// <summary>
        /// 派送完成时间
        /// </summary>
        public DateTime OverTime
        {
            set { _OverTime = value; }
            get { return _OverTime; }
        }

        private int _usesecond;
        /// <summary>
        /// 用时（分）
        /// </summary>
        public int usesecond
        {
            set { _usesecond = value; }
            get { return _usesecond; }
        }

        /// <summary>
        /// 订单的配送费
        /// </summary>
        public decimal Sendmoney
        {
            set;
            get;
        }

        /// <summary>
        /// 系数:系数=配送金额/实际用时（分）
        /// </summary>
        public string SendmoneyTimeRat
        {
            set;
            get;
        }
	}
}

