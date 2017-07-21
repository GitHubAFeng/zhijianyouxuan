using System;
using System.Collections.Generic;
namespace Hangjing.Model
{
	/// <summary>
	/// ϵͳģ��
	/// </summary>
	[Serializable]
	public class sys_ModuleInfo
	{
		private int _moduleid;
		private int _m_applicationid;
		private int _m_parentid;
		private string _m_pagecode;
		private string _m_cname;
		private string _m_directory;
		private int _m_orderlevel;
		private int _m_issystem;
		private int _m_close;
        private string _parend_pagecode;

        private IList<sys_ModuleInfo> _sublist;
        private IList<sys_ModulePermitionInfo> _mplist;


        /// <summary>
        /// ����
        /// </summary>
        public IList<sys_ModuleInfo> sublist
        {
            set { _sublist = value; }
            get { return _sublist; }
        }

        /// <summary>
        /// ������Ŀ
        /// </summary>
        public IList<sys_ModulePermitionInfo> mplist
        {
            set { _mplist = value; }
            get { return _mplist; }
        }

		/// <summary>
		/// ����ģ��ID��
		/// </summary>
		public int ModuleID
		{
			set{ _moduleid=value;}
			get{return _moduleid;}
		}
		/// <summary>
		/// δ��
		/// </summary>
		public int M_ApplicationID
		{
			set{ _m_applicationid=value;}
			get{return _m_applicationid;}
		}
		/// <summary>
		/// ��������ģ��ID��ModuleID����,0Ϊ����
		/// </summary>
		public int M_ParentID
		{
			set{ _m_parentid=value;}
			get{return _m_parentid;}
		}
		/// <summary>
		/// ģ�����ParentΪ0,��ΪS00(xx),����ΪS00M00(xx) ��Ź������
		/// </summary>
		public string M_PageCode
		{
			set{ _m_pagecode=value;}
			get{return _m_pagecode;}
		}
		/// <summary>
		/// ģ��/��Ŀ���Ƶ�ParentIDΪ0Ϊģ������
		/// </summary>
		public string M_CName
		{
			set{ _m_cname=value;}
			get{return _m_cname;}
		}
		/// <summary>
		/// �����Ӧ���ļ����ƣ����磺�̼ҹ���ģ��Ĵ��ֶε�ֵ��ShopList.aspx,ShopDetail.aspx���ǻ��������ļ�
		/// </summary>
		public string M_Directory
		{
			set{ _m_directory=value;}
			get{return _m_directory;}
		}
		/// <summary>
		/// ���򣨽���
		/// </summary>
		public int M_OrderLevel
		{
			set{ _m_orderlevel=value;}
			get{return _m_orderlevel;}
		}
		/// <summary>
		/// δ��
		/// </summary>
		public int M_IsSystem
		{
			set{ _m_issystem=value;}
			get{return _m_issystem;}
		}
		/// <summary>
		/// δ��
		/// </summary>
		public int M_Close
		{
			set{ _m_close=value;}
			get{return _m_close;}
		}

        /// <summary>
        /// ģ�鸸��page_code
        /// </summary>
        public string parend_pagecode
        {
            set { _parend_pagecode = value; }
            get { return _parend_pagecode; }
        }
	}
}

