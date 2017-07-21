// TogoLogin.cs
// CopyRight (c) 2009-2011 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2011-05-06
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;

using System.Data.SqlClient;

namespace Hangjing.SQLServerDAL
{
    public class TogoLogin
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TogoLoginInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ETogoLogin(");
            strSql.Append("TogoAccount,TogoPassword,TogoNum,LastLoginTime,LastLoginIp,LoginTimes,LastAction,UserAccountStatus)");
            strSql.Append(" values (");
            strSql.Append("@TogoAccount,@TogoPassword,@TogoNum,@LastLoginTime,@LastLoginIp,@LoginTimes,@LastAction,@UserAccountStatus)");

            SqlParameter[] parameters = 
            {
				new SqlParameter("@TogoAccount", SqlDbType.VarChar,50),
				new SqlParameter("@TogoPassword", SqlDbType.VarChar,50),
				new SqlParameter("@TogoNum", SqlDbType.Int,4),
				new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
				new SqlParameter("@LastLoginIp", SqlDbType.VarChar,50),
				new SqlParameter("@LoginTimes", SqlDbType.Int,4),
				new SqlParameter("@LastAction", SqlDbType.VarChar,1024),
				new SqlParameter("@UserAccountStatus", SqlDbType.Int,4)
            };
            parameters[0].Value = model.TogoAccount;
            parameters[1].Value = model.TogoPassword;
            parameters[2].Value = model.TogoNum;
            parameters[3].Value = model.LastLoginTime;
            parameters[4].Value = model.LastLoginIp;
            parameters[5].Value = model.LoginTimes;
            parameters[6].Value = model.LastAction;
            parameters[7].Value = model.UserAccountStatus;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(TogoLoginInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ETogoLogin set ");
            strSql.Append("TogoAccount=@TogoAccount,");
            strSql.Append("TogoPassword=@TogoPassword,");
            strSql.Append("TogoNum=@TogoNum,");
            strSql.Append("LastLoginTime=@LastLoginTime,");
            strSql.Append("LastLoginIp=@LastLoginIp,");
            strSql.Append("LoginTimes=@LoginTimes,");
            strSql.Append("LastAction=@LastAction,");
            strSql.Append("UserAccountStatus=@UserAccountStatus");
            strSql.Append(" where DataID=@DataID ");

            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataID", SqlDbType.Int,4),
				new SqlParameter("@TogoAccount", SqlDbType.VarChar,50),
				new SqlParameter("@TogoPassword", SqlDbType.VarChar,50),
				new SqlParameter("@TogoNum", SqlDbType.Int,4),
				new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
				new SqlParameter("@LastLoginIp", SqlDbType.VarChar,50),
				new SqlParameter("@LoginTimes", SqlDbType.Int,4),
				new SqlParameter("@LastAction", SqlDbType.VarChar,1024),
				new SqlParameter("@UserAccountStatus", SqlDbType.Int,4)
            };
            parameters[0].Value = model.DataID;
            parameters[1].Value = model.TogoAccount;
            parameters[2].Value = model.TogoPassword;
            parameters[3].Value = model.TogoNum;
            parameters[4].Value = model.LastLoginTime;
            parameters[5].Value = model.LastLoginIp;
            parameters[6].Value = model.LoginTimes;
            parameters[7].Value = model.LastAction;
            parameters[8].Value = model.UserAccountStatus;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataID</param>
        /// <returns>ETogoLoginInfo</returns>
        public TogoLoginInfo GetModel(int DataID)
        {
            string sql = "select DataID,TogoAccount,TogoPassword,TogoNum,LastLoginTime,LastLoginIp,LoginTimes,LastAction,UserAccountStatus from ETogoLogin where  DataID = @DataID";
            SqlParameter parameter = new SqlParameter("@DataID", SqlDbType.Int, 4);
            parameter.Value = DataID;
            TogoLoginInfo model = null;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
	            if (dr.Read())
	            {
		            model = new TogoLoginInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
		            model.TogoAccount = HJConvert.ToString(dr["TogoAccount"]);
		            model.TogoPassword = HJConvert.ToString(dr["TogoPassword"]);
		            model.TogoNum = HJConvert.ToInt32(dr["TogoNum"]);
		            model.LastLoginTime = HJConvert.ToDateTime(dr["LastLoginTime"]);
		            model.LastLoginIp = HJConvert.ToString(dr["LastLoginIp"]);
                    model.LoginTimes = HJConvert.ToInt32(dr["LoginTimes"]);
		            model.LastAction = HJConvert.ToString(dr["LastAction"]);
                    model.UserAccountStatus = HJConvert.ToInt32(dr["UserAccountStatus"]);
	            }
            }

            return model;

        }

        /// <summary>
        /// 获得总记录数
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int GetCount(string strWhere)
        {
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "ETogoLogin"), new SqlParameter("@strWhere", strWhere));
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<TogoLoginInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
	        IList<TogoLoginInfo> infos = new List<TogoLoginInfo>();
	        SqlParameter[] parameters = 
	        {
		        new SqlParameter("@tblName", SqlDbType.VarChar,255),
		        new SqlParameter("@strGetFields", SqlDbType.VarChar,1000),
		        new SqlParameter("@primary", SqlDbType.VarChar,255),
		        new SqlParameter("@orderName", SqlDbType.VarChar,255),
		        new SqlParameter("@PageSize", SqlDbType.Int),
		        new SqlParameter("@PageIndex", SqlDbType.Int),
		        new SqlParameter("@OrderType", SqlDbType.Bit),
		        new SqlParameter("@strWhere", SqlDbType.VarChar,1500)
	        };
	        parameters[0].Value = "ETogoLogin";
	        parameters[1].Value = "DataID,TogoAccount,TogoPassword,TogoNum,LastLoginTime,LastLoginIp,LoginTimes,LastAction,UserAccountStatus";
	        parameters[2].Value = "DataID";
	        parameters[3].Value = orderName;
	        parameters[4].Value = pagesize;
	        parameters[5].Value = pageindex;
	        parameters[6].Value = orderType;
	        parameters[7].Value = strWhere;

	        using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
	        {
		        while (dr.Read())
		        {
			        TogoLoginInfo info = new TogoLoginInfo();
			        info.DataID = HJConvert.ToInt32(dr["DataID"]);
			        info.TogoAccount = HJConvert.ToString(dr["TogoAccount"]);
			        info.TogoPassword = HJConvert.ToString(dr["TogoPassword"]);
			        info.TogoNum = HJConvert.ToInt32(dr["TogoNum"]);
			        info.LastLoginTime = HJConvert.ToDateTime(dr["LastLoginTime"]);
			        info.LastLoginIp = HJConvert.ToString(dr["LastLoginIp"]);
			        info.LoginTimes = HJConvert.ToInt32(dr["LoginTimes"]);
                    info.LastAction = HJConvert.ToString(dr["LastAction"]);
			        info.UserAccountStatus = HJConvert.ToInt32(dr["UserAccountStatus"]);
			        infos.Add(info);
		        }
	        }
	        return infos;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="Id"></param>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <returns></returns>
        public int DelTogoLogin(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ETogoLogin where DataID=@DataID");
            SqlParameter[] Para = 
			{
				new SqlParameter("@DataID",SqlDbType.Int)
			};
            Para[0].Value = Id;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 批量删除记录
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int DelList(string IdList)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ETogoLogin where DataID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}
