using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// EmailConfigInfo.cs :EmailConfigInfo 
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 20010-06-18

namespace Hangjing.Common
{
    /// <summary>
    /// 基本设置描述类, 加[Serializable]标记为可序列化
    /// </summary>
    [Serializable]
    public class EmailConfigInfo
    {
        private string _smtp;
        private string _port;
        private string _sysemail;
        private string _username;
        private string _password;

        private string _regcontent;
        private string _errorcontent;

        /// <summary>
        /// 邮箱smtp地址
        /// </summary>
        public string Smtp
        {
            set { _smtp = value; }
            get { return _smtp; }
        }

        /// <summary>
        /// smtp端口号
        /// </summary>
        public string Port
        {
            set { _port = value; }
            get { return _port; }
        }

        /// <summary>
        /// 邮箱帐号
        /// </summary>
        public string SysEmail
        {
            set { _sysemail = value; }
            get { return _sysemail; }
        }

        /// <summary>
        /// 邮箱用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string PassWord
        {
            set { _password = value; }
            get { return _password; }
        }

        /// <summary>
        /// 注册成功内容
        /// </summary>
        public string RegContent
        {
            set { _regcontent = value; }
            get { return _regcontent; }
        }
    
        /// <summary>
        /// 错误内容
        /// </summary>
        public string ErrorContent
        {
            set { _errorcontent = value; }
            get { return _errorcontent; }
        }
    }
}
