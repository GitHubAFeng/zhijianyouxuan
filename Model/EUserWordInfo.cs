// EUserWordInfo.css:用户留言模型.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-03-05

using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 实体类EUserWord 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class EUserWordInfo
	{

		private int _dataid;
		private int _userid;
		private string _word;
		private int _state;
		private DateTime _time;
		private string _rremark;
		private DateTime _rtime;
		private string _adminid;
        private string _username;
        private string _adminname;

        private string _pic;

        /// <summary>
        /// 用户头像
        /// </summary>
        public string pic
        {
            set { _pic = value; }
            get { return _pic; }
        }

        //添加的字段
        private int _mytype;
		/// <summary>
		/// 自增编号
		/// </summary>
		public int DataID
		{
			set{ _dataid=value;}
			get{return _dataid;}
		}
		/// <summary>
		/// 用户编号
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 留言内容
		/// </summary>
		public string Word
		{
			set{ _word=value;}
			get{return _word;}
		}
		/// <summary>
		/// 留言是否审核标志:0表示正在审核 ,1表示通过审核..
		/// </summary>
		public int State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 留言时间
		/// </summary>
		public DateTime Time
		{
			set{ _time=value;}
			get{return _time;}
		}
		/// <summary>
		/// 联系方式
		/// </summary>
		public string Rremark
		{
			set{ _rremark=value;}
			get{return _rremark;}
		}
		/// <summary>
		/// 未用
		/// </summary>
		public DateTime RTime
		{
			set{ _rtime=value;}
			get{return _rtime;}
		}
		/// <summary>
		/// 回复id
		/// </summary>
		public string adminID
		{
			set{ _adminid=value;}
			get{return _adminid;}
		}
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string Adminname
        {
            set { _adminname = value; }
            get { return _adminname;  }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public int MyType
        {
            set { _mytype = value; }
            get { return _mytype; }
        }
	}
}

