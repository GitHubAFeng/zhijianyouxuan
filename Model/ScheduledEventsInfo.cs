//ScheduledEventsInfo.cs ����ִ�м�¼
// CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved
using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ʵ����ScheduledEvents ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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

