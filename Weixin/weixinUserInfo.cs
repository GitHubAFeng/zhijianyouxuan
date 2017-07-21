/*********************************************************************
 * CopyRight (c) 2009-2014 HangJing Teconology. All Rights Reserved.
 * FileName : 
 * Function : 
 * Created by jijunjian at 2015-01-21 17:55:52.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 用户信息（微信中）
    /// </summary>
    public class weixinUserInfo
    {
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。 
        /// </summary>
        public string subscribe
        {
            get;
            set;
        }

        /// <summary>
        /// 用户的标识，对当前公众号唯一 
        /// </summary>
        public string openid
        {
            get;
            set;
        }

        /// <summary>
        /// 用户的昵称 
        /// </summary>
        public string nickname
        {
            get;
            set;
        }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知 
        /// </summary>
        public string sex
        {
            get;
            set;
        }

        /// <summary>
        /// 用户头像 
        /// </summary>
        public string headimgurl
        {
            get;
            set;
        }

    }
}
