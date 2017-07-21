using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 用户催单记录
	/// </summary>
	[Serializable]
	public class hurryorderInfo
	{
		private int _cid;
		private string  _oid;
        private string _Name;
		private string _addtime;
		private int _ccount;
		private int _reveint;
		private string _revevar;

        private int _id;
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }

		/// <summary>
		/// auto_increment
		/// </summary>
		public int CID
		{
			set{ _cid=value;}
			get{return _cid;}
		}
		/// <summary>
		/// 订单编号
		/// </summary>
		public string oid
		{
			set{ _oid=value;}
			get{return _oid;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
        public string Name
		{
            set { _Name = value; }
            get { return _Name; }
		}
		/// <summary>
		/// 最后一次时间
		/// </summary>
		public string addtime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
        /// 当前订单催单次数
		/// </summary>
		public int Ccount
		{
			set{ _ccount=value;}
			get{return _ccount;}
		}
		/// <summary>
		/// 状态 0 未处理 1已处理
		/// </summary>
		public int ReveInt
		{
			set{ _reveint=value;}
			get{return _reveint;}
		}
		/// <summary>
        /// 备用
		/// </summary>
		public string ReveVar
		{
			set{ _revevar=value;}
			get{return _revevar;}
		}

	}
}

