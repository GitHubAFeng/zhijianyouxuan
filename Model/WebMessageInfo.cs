// WebMessageInfo.cs:վ����������.
// CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ʵ����WebMessage ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ����
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
        /// վ��������
		/// </summary>
		public string Message
		{
			set{ _message=value;}
			get{return _message;}
		}
		/// <summary>
        /// ʱ��
		/// </summary>
		public DateTime AddDate
		{
			set{ _adddate=value;}
			get{return _adddate;}
		}
	}
}

