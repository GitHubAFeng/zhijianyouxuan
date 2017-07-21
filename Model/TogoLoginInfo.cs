// ETogoLoginInfo：商家登录信息实体
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Add by yangxiaolong@ihangjing.com
// 2010-03-13


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 商家登录信息
    /// </summary>
    public class TogoLoginInfo
    {
        private int _dataid;
        private string _togoaccount;
        private string _togopassword;
        private int _togonum;
        private DateTime _lastlogintime;
        private string _lastloginip;
        private int _logintimes;
        private string _lastaction;
        private int _useraccountstatus;

        /// <summary>
        /// 数据编号
        /// </summary>
        public int DataID
        {
            set { _dataid = value; }
            get { return _dataid; }
        }

        /// <summary>
        /// 商家登录帐号
        /// </summary>
        public string TogoAccount
        {
            set { _togoaccount = value; }
            get { return _togoaccount; }
        }

        /// <summary>
        /// 商家登录密码
        /// </summary>
        public string TogoPassword
        {
            set { _togopassword = value; }
            get { return _togopassword; }
        }

        /// <summary>
        /// 对应的商家编号
        /// </summary>
        public int TogoNum
        {
            set { _togonum = value; }
            get { return _togonum; }
        }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime
        {
            set { _lastlogintime = value; }
            get { return _lastlogintime; }
        }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LastLoginIp
        {
            set { _lastloginip = value; }
            get { return _lastloginip; }
        }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginTimes
        {
            set { _logintimes = value; }
            get { return _logintimes; }
        }

        /// <summary>
        /// 最后操作
        /// </summary>
        public string LastAction
        {
            set { _lastaction = value; }
            get { return _lastaction; }
        }

        /// <summary>
        /// 账户状态 0:未激活 1:正常  
        /// </summary>
        public int UserAccountStatus
        {
            set { _useraccountstatus = value; }
            get { return _useraccountstatus; }
        }
    }
}
