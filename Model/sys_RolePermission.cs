using System;
namespace Hangjing.Model
{
    /// <summary>
    /// 角色模块关系表
    /// </summary>
    [Serializable]
    public class sys_RolePermissionInfo
    {
        private int _permissionid;
        private int _p_roleid;
        private int _p_applicationid;
        private string _p_pagecode;
        private int _p_value;

        private string _des;
        /// <summary>
        /// 对应pagecode的模块的描述
        /// </summary>
        public string des
        {
            set { _des = value; }
            get { return _des; }
        }
        /// <summary>
        /// 角色应用权限自动ID
        /// </summary>
        public int PermissionID
        {
            set { _permissionid = value; }
            get { return _permissionid; }
        }
        /// <summary>
        /// 角色ID与sys_Roles表中RoleID相
        /// </summary>
        public int P_RoleID
        {
            set { _p_roleid = value; }
            get { return _p_roleid; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public int P_ApplicationID
        {
            set { _p_applicationid = value; }
            get { return _p_applicationid; }
        }
        /// <summary>
        /// 角色应用中页面权限代码
        /// </summary>
        public string P_PageCode
        {
            set { _p_pagecode = value; }
            get { return _p_pagecode; }
        }
        /// <summary>
        /// 权限值
        ///权限分：查（2） , 增(4),改(8),删（16）
        ///最后的权限值为这个几个和;比如用户有：查，增的权限，权限值为6
        /// </summary>
        public int P_Value
        {
            set { _p_value = value; }
            get { return _p_value; }
        }
    }
}

