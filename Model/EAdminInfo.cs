
// EAddress.css:地址簿相关操作.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-03-05

using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 实体类EAdmin 。(属性说明自动提取数据库字段的描述信息)
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
        /// 1管理员，2客服
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
        /// 是否分区域管理员，1表示是分区域管理员
        /// </summary>
        public int root
        {
            set;
            get;
        }
		#endregion Model

	}
}

