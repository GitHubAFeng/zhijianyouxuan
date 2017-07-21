// WebMessageLog.cs:վ���ŷ��ͼ�¼��.
// CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ʵ����WebMessageLog ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// վ��������
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// �û���
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
		/// �û����
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// վ���ű��(WebMessage�е�����DataId)
		/// </summary>
		public int MessageId
		{
			set{ _messageid=value;}
			get{return _messageid;}
		}
		/// <summary>
        /// ʱ��
		/// </summary>
		public DateTime AddDate
		{
			set{ _adddate=value;}
			get{return _adddate;}
		}
		/// <summary>
        /// ״̬:0 δ�� 1 �Ѷ�
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
	}
}

