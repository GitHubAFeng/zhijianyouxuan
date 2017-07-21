//ScheduledEventsInfo.cs 任务执行记录
// CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved
using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 实体类ScheduledEvents 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public class ScheduledEventsInfo
	{
		private int _scheduleid;
		private string _key;
		private DateTime _lastexecuted;
		private string _servername;
		/// <summary>
		/// 
		/// </summary>
		public int scheduleID
		{
			set{ _scheduleid=value;}
			get{return _scheduleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string key
		{
			set{ _key=value;}
			get{return _key;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime lastexecuted
		{
			set{ _lastexecuted=value;}
			get{return _lastexecuted;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string servername
		{
			set{ _servername=value;}
			get{return _servername;}
		}
	}
}

