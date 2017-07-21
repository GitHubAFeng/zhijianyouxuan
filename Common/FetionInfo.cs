using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// FetionInfo.cs :FetionInfo  飞信的配置信息
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 20010-06-19

namespace Hangjing.Common
{
    public class FetionInfo
    {
        private string _username;
        private string _password;

        
        /// <summary>
        /// 飞信用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        /// <summary>
        /// 飞信密码
        /// </summary>
        public string PassWord
        {
            set { _password = value; }
            get { return _password; }
        }
    }
}
