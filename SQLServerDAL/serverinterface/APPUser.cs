/*********************************************************************
 * CopyRight (c) 2009-2014 HangJing Teconology. All Rights Reserved.
 * FileName : 
 * Function : 
 * Created by jijunjian at 2014-10-23 14:33:20.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using System.Data.SqlClient;
using System.Data;
using Hangjing.DBUtility;

namespace Hangjing.SQLServerDAL.serverinterface
{
    /// <summary>
    /// 这个类主要用来服务器程序那调用，完成登录，状态，等功能（这样是为了以后的项目直接加这个类既可）
    /// </summary>
    public class APPUser
    {
        public string UserName;
        public string Password;
        public string sessionid;
        public int usertype;
        public IAPP app = null;

        /// <summary>
        /// 根据类型初始化app
        /// </summary>
        /// <param name="_UserName"></param>
        /// <param name="_Password"></param>
        /// <param name="_sessionid"></param>
        /// <param name="_usertype"></param>
        public APPUser(string _UserName, string _Password, string _sessionid, int _usertype)
        {
            UserName = _UserName;
            Password = _Password;
            sessionid = _sessionid;
            usertype = _usertype;

            switch (usertype)
            {
                case (int)UserType.Deliver:
                    app = new DeliverAPP();
                    break;
                case (int)UserType.Shop:
                    app = new ShopAPP();
                    break;
                case (int)UserType.Site:
                    app = new SiteAPP();
                    break;
                case (int)UserType.Taker:
                    app = new TakerAPP();
                    break;
                default:
                    break;
            }

        }
    }

    /// <summary>
    /// app用户类型
    /// </summary>
    public enum UserType : int
    {
        /// <summary>
        /// 骑士
        /// </summary>
        Deliver = 1,
        /// <summary>
        /// 商家
        /// </summary>
        Shop = 2,
        /// <summary>
        /// 配送点
        /// </summary>
        Site = 3,
        /// <summary>
        /// 取餐员
        /// </summary>
        Taker = 4
    }

}
