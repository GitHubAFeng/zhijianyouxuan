using System;
namespace Hangjing.Model
{
    /// <summary>
    /// ��ɫģ���ϵ��
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
        /// ��Ӧpagecode��ģ�������
        /// </summary>
        public string des
        {
            set { _des = value; }
            get { return _des; }
        }
        /// <summary>
        /// ��ɫӦ��Ȩ���Զ�ID
        /// </summary>
        public int PermissionID
        {
            set { _permissionid = value; }
            get { return _permissionid; }
        }
        /// <summary>
        /// ��ɫID��sys_Roles����RoleID��
        /// </summary>
        public int P_RoleID
        {
            set { _p_roleid = value; }
            get { return _p_roleid; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public int P_ApplicationID
        {
            set { _p_applicationid = value; }
            get { return _p_applicationid; }
        }
        /// <summary>
        /// ��ɫӦ����ҳ��Ȩ�޴���
        /// </summary>
        public string P_PageCode
        {
            set { _p_pagecode = value; }
            get { return _p_pagecode; }
        }
        /// <summary>
        /// Ȩ��ֵ
        ///Ȩ�޷֣��飨2�� , ��(4),��(8),ɾ��16��
        ///����Ȩ��ֵΪ���������;�����û��У��飬����Ȩ�ޣ�Ȩ��ֵΪ6
        /// </summary>
        public int P_Value
        {
            set { _p_value = value; }
            get { return _p_value; }
        }
    }
}

