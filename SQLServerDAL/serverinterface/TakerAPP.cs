/*********************************************************************
 * CopyRight (c) 2009-2014 HangJing Teconology. All Rights Reserved.
 * FileName : 
 * Function : 
 * Created by jijunjian at 2014-10-23 19:02:38.
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
    /// 取餐员相关接口实现
    /// </summary>
    public class TakerAPP : IAPP
    {
        /// <summary>
        /// app登录返回用户编号
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="Password">密码</param>
        /// <param name="sessionid">app链接时生成的sessionid</param>
        /// <param name="usertpe">1骑士，2商家，3站点，4取餐员</param>
        /// <returns></returns>
        public int APPLogin(string UserName, string Password, string sessionid)
        {
            int APPUserID = 0;
            PointsInfo model = new Points().GetModel(UserName, Password);
            if (model != null)
            {
                APPUserID = model.Unid;

                //这里保存sessionid,方便查看登录状态
                //string sql = "UPDATE Points SET sessionid = '" + sessionid + "' WHERE Unid = " + APPUserID;
                //SQLHelper.excutesql(sql);
            }

            return APPUserID;
        }

        /// <summary>
        /// 获取app登录时返回的json（这样的目前是让服务器程序只依赖引用的dll）
        /// </summary>
        /// <param name="model">配送员对像</param>
        /// <param name="state">1表示正常登录，-2 表示踢下线，-1表示失败</param>
        /// <returns></returns>
        public string getLoginJSON(int userid, string state)
        {
            PointsInfo model = new Points().GetModel(userid);
            int newordercount = new Custorder().SendShopOrderCount(model.Unid.ToString());
            string msg = "Login::{\"shopid\":\"" + model.Unid + "\",\"state\":\"" + state + "\",\"togoname\":\"" + model.Name + "\",\"newordercount\":\"" + newordercount + "\",\"sendtype\":\"" + model.sentorg + "\"}";
            return msg;
        }

        /// <summary>
        /// 骑士根据sessionid更新app登入信息,登录时设置1，退出设置0
        /// </summary>
        /// <param name="TogoNum"></param>
        /// <param name="NewPwd"></param>
        /// <returns></returns>
        public int UpdateLoginState(string sessionid, int State)
        {
            return 0; //本项目不实现商家登录状态
            StringBuilder str = new StringBuilder();
            str.Append("update Points set appstate=@State where sessionid=@sessionid");

            SqlParameter[] Para = 
            {
                new SqlParameter("@sessionid",SqlDbType.VarChar,256),
                new SqlParameter("@State",SqlDbType.Int)
            };
            Para[0].Value = sessionid;
            Para[1].Value = State;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }
    }
}
