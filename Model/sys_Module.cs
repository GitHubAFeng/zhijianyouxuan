using System;
using System.Collections.Generic;
namespace Hangjing.Model
{
	/// <summary>
	/// 系统模块
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
        /// 子类
        /// </summary>
        public IList<sys_ModuleInfo> sublist
        {
            set { _sublist = value; }
            get { return _sublist; }
        }

        /// <summary>
        /// 操作项目
        /// </summary>
        public IList<sys_ModulePermitionInfo> mplist
        {
            set { _mplist = value; }
            get { return _mplist; }
        }

		/// <summary>
		/// 功能模块ID号
		/// </summary>
		public int ModuleID
		{
			set{ _moduleid=value;}
			get{return _moduleid;}
		}
		/// <summary>
		/// 未用
		/// </summary>
		public int M_ApplicationID
		{
			set{ _m_applicationid=value;}
			get{return _m_applicationid;}
		}
		/// <summary>
		/// 所属父级模块ID与ModuleID关联,0为顶级
		/// </summary>
		public int M_ParentID
		{
			set{ _m_parentid=value;}
			get{return _m_parentid;}
		}
		/// <summary>
		/// 模块编码Parent为0,则为S00(xx),否则为S00M00(xx) 编号规则待定
		/// </summary>
		public string M_PageCode
		{
			set{ _m_pagecode=value;}
			get{return _m_pagecode;}
		}
		/// <summary>
		/// 模块/栏目名称当ParentID为0为模块名称
		/// </summary>
		public string M_CName
		{
			set{ _m_cname=value;}
			get{return _m_cname;}
		}
		/// <summary>
		/// 保存对应的文件名称，例如：商家管理模块的此字段的值：ShopList.aspx,ShopDetail.aspx就是会包含多个文件
		/// </summary>
		public string M_Directory
		{
			set{ _m_directory=value;}
			get{return _m_directory;}
		}
		/// <summary>
		/// 排序（降序）
		/// </summary>
		public int M_OrderLevel
		{
			set{ _m_orderlevel=value;}
			get{return _m_orderlevel;}
		}
		/// <summary>
		/// 未用
		/// </summary>
		public int M_IsSystem
		{
			set{ _m_issystem=value;}
			get{return _m_issystem;}
		}
		/// <summary>
		/// 未用
		/// </summary>
		public int M_Close
		{
			set{ _m_close=value;}
			get{return _m_close;}
		}

        /// <summary>
        /// 模块父类page_code
        /// </summary>
        public string parend_pagecode
        {
            set { _parend_pagecode = value; }
            get { return _parend_pagecode; }
        }
	}
}

