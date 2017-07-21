// WebMessageInfo.cs:站内信内容类.
// CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 实体类WebMessage 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public class WebMessageInfo
	{
		private int _dataid;
		private string _title;
		private string _message;
		private DateTime _adddate;
		/// <summary>
		/// 
		/// </summary>
		public int DataId
		{
			set{ _dataid=value;}
			get{return _dataid;}
		}
		/// <summary>
        /// 标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
        /// 站内信内容
		/// </summary>
		public string Message
		{
			set{ _message=value;}
			get{return _message;}
		}
		/// <summary>
        /// 时间
		/// </summary>
		public DateTime AddDate
		{
			set{ _adddate=value;}
			get{return _adddate;}
		}
	}
}

