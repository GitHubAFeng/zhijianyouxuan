using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ϵͳ��ɫʵ��
	/// </summary>
	[Serializable]
	public class sys_RolesInfo
	{
		private int _roleid;
		private string _r_rolename;
		private string _r_description;
		/// <summary>
		/// ��ɫID�Զ�ID
		/// </summary>
		public int RoleID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// ��ɫ����
		/// </summary>
		public string R_RoleName
		{
			set{ _r_rolename=value;}
			get{return _r_rolename;}
		}
		/// <summary>
		/// ��ɫ����
		/// </summary>
		public string R_Description
		{
			set{ _r_description=value;}
			get{return _r_description;}
		}
	}
}

