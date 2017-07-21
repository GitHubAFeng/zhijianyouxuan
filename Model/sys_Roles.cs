using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 系统角色实体
	/// </summary>
	[Serializable]
	public class sys_RolesInfo
	{
		private int _roleid;
		private string _r_rolename;
		private string _r_description;
		/// <summary>
		/// 角色ID自动ID
		/// </summary>
		public int RoleID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// 角色名称
		/// </summary>
		public string R_RoleName
		{
			set{ _r_rolename=value;}
			get{return _r_rolename;}
		}
		/// <summary>
		/// 角色介绍
		/// </summary>
		public string R_Description
		{
			set{ _r_description=value;}
			get{return _r_description;}
		}
	}
}

