/*********************************************************************
 * CopyRight (c) 2009-2014 HangJing Teconology. All Rights Reserved.
 * FileName : 
 * Function : 
 * Created by jijunjian at 2014-10-23 19:46:08.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Hangjing.SQLServerDAL.serverinterface
{

    public interface IAPP
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        int APPLogin(string UserName, string Password, string sessionid);

        /// <summary>
        /// 获取app登录时返回的json
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        string getLoginJSON(int userid, string state);

        /// <summary>
        /// 骑士根据sessionid更新app登入信息,登录时设置1，退出设置0
        /// </summary>
        /// <param name="TogoNum"></param>
        /// <param name="NewPwd"></param>
        /// <returns></returns>
        int UpdateLoginState(string sessionid, int State);
    }
}
