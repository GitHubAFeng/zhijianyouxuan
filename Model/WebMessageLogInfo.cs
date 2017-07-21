// WebMessageLog.cs:站内信发送记录类.
// CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 实体类WebMessageLog 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public class WebMessageLogInfo
	{
		private int _dataid;
		private int _userid;
		private int _messageid;
		private DateTime _adddate;
		private int _status;
        private string _userName;
        private string _title;
        private string _message;

        /// <summary>
        /// 站内信内容
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public int DataId
		{
			set{ _dataid=value;}
			get{return _dataid;}
		}
		/// <summary>
		/// 用户编号
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 站内信编号(WebMessage中的主键DataId)
		/// </summary>
		public int MessageId
		{
			set{ _messageid=value;}
			get{return _messageid;}
		}
		/// <summary>
        /// 时间
		/// </summary>
		public DateTime AddDate
		{
			set{ _adddate=value;}
			get{return _adddate;}
		}
		/// <summary>
        /// 状态:0 未读 1 已读
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
	}
}

