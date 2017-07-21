
// EAddress.css:��ַ����ز���.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-03-05

using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ʵ����EAdmin ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class EAdminInfo
	{
		#region Model

		private int _id;
		private string _adminname;
		private string _adminpassword;
		private int _adminstatus;
		private DateTime _lastaccess;
        private string _permission;
        private string  _realname;
        private string _rem;
        private int _cityID;

        public int CityID
        {
            set { _cityID = value; }
            get { return _cityID; }
        }
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AdminName
		{
			set{ _adminname=value;}
			get{return _adminname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AdminPassword
		{
			set{ _adminpassword=value;}
			get{return _adminpassword;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int AdminStatus
		{
			set{ _adminstatus=value;}
			get{return _adminstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime LastAccess
		{
			set{ _lastaccess=value;}
			get{return _lastaccess;}
		}
        /// <summary>
        /// 1����Ա��2�ͷ�
        /// </summary>
        public string Permission
        {
            set { _permission = value; }
            get { return _permission; }
        }
        public string RealName
        {
            set { _realname = value; }
            get { return _realname; }
        }
        public string Rem
        {
            set { _rem = value; }
            get { return  _rem;}
        }

        /// <summary>
        /// �Ƿ���������Ա��1��ʾ�Ƿ��������Ա
        /// </summary>
        public int root
        {
            set;
            get;
        }
		#endregion Model

	}
}

