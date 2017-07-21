// EUserWordInfo.css:�û�����ģ��.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-03-05

using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ʵ����EUserWord ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// �û�ͷ��
        /// </summary>
        public string pic
        {
            set { _pic = value; }
            get { return _pic; }
        }

        //��ӵ��ֶ�
        private int _mytype;
		/// <summary>
		/// �������
		/// </summary>
		public int DataID
		{
			set{ _dataid=value;}
			get{return _dataid;}
		}
		/// <summary>
		/// �û����
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string Word
		{
			set{ _word=value;}
			get{return _word;}
		}
		/// <summary>
		/// �����Ƿ���˱�־:0��ʾ������� ,1��ʾͨ�����..
		/// </summary>
		public int State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime Time
		{
			set{ _time=value;}
			get{return _time;}
		}
		/// <summary>
		/// ��ϵ��ʽ
		/// </summary>
		public string Rremark
		{
			set{ _rremark=value;}
			get{return _rremark;}
		}
		/// <summary>
		/// δ��
		/// </summary>
		public DateTime RTime
		{
			set{ _rtime=value;}
			get{return _rtime;}
		}
		/// <summary>
		/// �ظ�id
		/// </summary>
		public string adminID
		{
			set{ _adminid=value;}
			get{return _adminid;}
		}
        /// <summary>
        /// �û�����
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// ����Ա����
        /// </summary>
        public string Adminname
        {
            set { _adminname = value; }
            get { return _adminname;  }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public int MyType
        {
            set { _mytype = value; }
            get { return _mytype; }
        }
	}
}

