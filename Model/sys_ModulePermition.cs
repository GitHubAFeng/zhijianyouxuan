using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 模块对应的操作(查看,新添,编辑,删除等)
	/// </summary>
	[Serializable]
	public class sys_ModulePermitionInfo
	{
		private int _mid;
		private int _moduleid;
		private string _pername;
		private int _pvalue;
		private int _reveint;
		private string _revevar;
		/// <summary>
		/// 
		/// </summary>
		public int mid
		{
			set{ _mid=value;}
			get{return _mid;}
		}
		/// <summary>
		/// 模块编号
		/// </summary>
		public int ModuleID
		{
			set{ _moduleid=value;}
			get{return _moduleid;}
		}
		/// <summary>
		/// 模块权限名称
		/// </summary>
		public string pername
		{
			set{ _pername=value;}
			get{return _pername;}
		}
		/// <summary>
		/// 模块权限权值(2的1,2,3,4..次方)。查看，添加，编辑，删除是确认的，分别是(2^1,2^2,2^3,2^4)
		/// </summary>
		public int pvalue
		{
			set{ _pvalue=value;}
			get{return _pvalue;}
		}
		/// <summary>
        /// 排序(降序)
		/// </summary>
		public int ReveInt
		{
			set{ _reveint=value;}
			get{return _reveint;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReveVar
		{
			set{ _revevar=value;}
			get{return _revevar;}
		}
	}
}

