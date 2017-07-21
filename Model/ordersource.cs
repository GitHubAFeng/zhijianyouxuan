using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 充值优惠
	/// </summary>
    public class ordersourcesInfo
	{
		private int _id;
		private string _classname;
        public string _value;
		private int _depth;
		private int _status;
		private int _priority;
		private int _parentid;
		private int _isdel;

		/// <summary>
        /// 编号
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 充值M
		/// </summary>
		public string classname
		{
			set{ _classname=value;}
			get{return _classname;}
		}
        /// <summary>
        /// 新增，
        /// </summary>
        public string value
        {
            set { _value = value; }
            get { return _value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public int Depth
		{
			set{ _depth=value;}
			get{return _depth;}
		}
		/// <summary>
		/// 送x元
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Priority
		{
			set{ _priority=value;}
			get{return _priority;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int isDel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
	}
}

