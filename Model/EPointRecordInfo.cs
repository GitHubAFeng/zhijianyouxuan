
// EAddress.css:地址簿相关操作.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-03-05

using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 实体类EPointRecord 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class EPointRecordInfo
	{
		private int _dataid;
		private int _userid;
		private int _point;
		private string _event;
		private DateTime _time;
        private string _uname;

        /// <summary>
        /// 
        /// </summary>
        public string uname
        {
            set { _uname = value; }
            get { return _uname; }
        }

		/// <summary>
		/// 
		/// </summary>
		public int DataID
		{
			set{ _dataid=value;}
			get{return _dataid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Point
		{
			set{ _point=value;}
			get{return _point;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Event
		{
			set{ _event=value;}
			get{return _event;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time
		{
			set{ _time=value;}
			get{return _time;}
		}
	}
}

