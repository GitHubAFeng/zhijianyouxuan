//---------------------------------------------------------------------
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :
// Created by tuhui at 2010-8-6 16:34:39.
// E-Mail: tuhui@ihangjing.com
//----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 实体类Role 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class RoleInfo
    {
        public RoleInfo()
        { }

        private int _id;
        private string _rolename;
        private string _basic;
        private string _column;
        private string _user;
        private string _template;
        private string _system;
        private string _describe;
        private bool _authorize;
        private DateTime _adddate;

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Basic
        {
            set { _basic = value; }
            get { return _basic; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Column
        {
            set { _column = value; }
            get { return _column; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string User
        {
            set { _user = value; }
            get { return _user; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Template
        {
            set { _template = value; }
            get { return _template; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string System
        {
            set { _system = value; }
            get { return _system; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Describe
        {
            set { _describe = value; }
            get { return _describe; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Authorize
        {
            set { _authorize = value; }
            get { return _authorize; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }
    }
}
