// SystemGsmList.cs:查看邮件短信信息记录.
// CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;

using System.Data.SqlClient;
using System.Data;

namespace Hangjing.SQLServerDAL
{
    public class SystemGsmList
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SystemGsmListInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Systemgsmlist(");
            strSql.Append("TogoId,SentTime,AddDate,Sum,SentType,Content,UserIdList,Status,Inve1,Inve2,DelMoney)");
            strSql.Append(" values (");
            strSql.Append("@TogoId,@SentTime,@AddDate,@Sum,@SentType,@Content,@UserIdList,@Status,@Inve1,@Inve2,@DelMoney)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@TogoId", SqlDbType.Int,4),
				new SqlParameter("@SentTime", SqlDbType.DateTime),
				new SqlParameter("@AddDate", SqlDbType.DateTime),
				new SqlParameter("@Sum", SqlDbType.Int,4),
				new SqlParameter("@SentType", SqlDbType.Int,4),
				new SqlParameter("@Content", SqlDbType.VarChar,4096),
				new SqlParameter("@UserIdList", SqlDbType.VarChar,4096),
				new SqlParameter("@Status", SqlDbType.Int,4),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,256),
                new SqlParameter("@DelMoney", SqlDbType.Decimal,9)
            };
            parameters[0].Value = model.TogoId;
            parameters[1].Value = model.SentTime;
            parameters[2].Value = model.AddDate;
            parameters[3].Value = model.Sum;
            parameters[4].Value = model.SentType;
            parameters[5].Value = model.Content;
            parameters[6].Value = model.UserIdList;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.Inve1;
            parameters[9].Value = model.Inve2;
            parameters[10].Value = model.DelMoney;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(SystemGsmListInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SystemGsmList set ");
            strSql.Append("TogoId=@TogoId,");
            strSql.Append("SentTime=@SentTime,");
            strSql.Append("AddDate=@AddDate,");
            strSql.Append("Sum=@Sum,");
            strSql.Append("SentType=@SentType,");
            strSql.Append("Content=@Content,");
            strSql.Append("UserIdList=@UserIdList,");
            strSql.Append("Status=@Status,");
            strSql.Append("Inve1=@Inve1,");
            strSql.Append("Inve2=@Inve2,");
            strSql.Append("DelMoney=@DelMoney");
            strSql.Append(" where DataId=@DataId ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataId", SqlDbType.Int,4),
				new SqlParameter("@TogoId", SqlDbType.Int,4),
				new SqlParameter("@SentTime", SqlDbType.DateTime),
				new SqlParameter("@AddDate", SqlDbType.DateTime),
				new SqlParameter("@Sum", SqlDbType.Int,4),
				new SqlParameter("@SentType", SqlDbType.Int,4),
				new SqlParameter("@Content", SqlDbType.VarChar,4096),
				new SqlParameter("@UserIdList", SqlDbType.VarChar,4096),
				new SqlParameter("@Status", SqlDbType.Int,4),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,256),
                new SqlParameter("@DelMoney", SqlDbType.Decimal,9)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.TogoId;
            parameters[2].Value = model.SentTime;
            parameters[3].Value = model.AddDate;
            parameters[4].Value = model.Sum;
            parameters[5].Value = model.SentType;
            parameters[6].Value = model.Content;
            parameters[7].Value = model.UserIdList;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.Inve1;
            parameters[10].Value = model.Inve2;
            parameters[11].Value = model.DelMoney;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>SystemGsmListInfo</returns>
        public SystemGsmListInfo GetModel(int DataId)
        {
            string sql = "select SystemGsmList.DataId,SystemGsmList.TogoId,SystemGsmList.SentTime,SystemGsmList.AddDate,SystemGsmList.Sum,SystemGsmList.SentType,SystemGsmList.Content,SystemGsmList.UserIdList,SystemGsmList.Status,SystemGsmList.Inve1,SystemGsmList.Inve2,SystemGsmList.DelMoney,etogo.TogoName from SystemGsmList Left join etogo on SystemGsmList.TogoId=etogo.DataId where  SystemGsmList.DataId = @DataId";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            SystemGsmListInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new SystemGsmListInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    model.SentTime = HJConvert.ToDateTime(dr["SentTime"]);
                    model.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    model.Sum = HJConvert.ToInt32(dr["Sum"]);
                    model.SentType = HJConvert.ToInt32(dr["SentType"]);
                    model.Content = HJConvert.ToString(dr["Content"]);
                    model.UserIdList = HJConvert.ToString(dr["UserIdList"]);
                    model.Status = HJConvert.ToInt32(dr["Status"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.DelMoney = HJConvert.ToDecimal(dr["DelMoney"]);
                    model.TogoName = HJConvert.ToString(dr["TogoName"]);
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
            SqlParameter[] parameters =
            {
                new SqlParameter("@tblName" , SqlDbType.VarChar ,30),
                new SqlParameter("@strWhere" , SqlDbType.VarChar ,50)
            };
            parameters[0].Value = "SystemGsmList";
            parameters[1].Value = strWhere;
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", parameters));
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
        public IList<SystemGsmListInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<SystemGsmListInfo> infos = new List<SystemGsmListInfo>();
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
            parameters[0].Value = "SystemGsmList";
            parameters[1].Value = "DataId,TogoId,SentTime,AddDate,Sum,SentType,Content,UserIdList,Status,Inve1,Inve2,DelMoney,(select Name from points Where unid=SystemGsmList.TogoId ) as TogoName";
            parameters[2].Value = "DataId";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    SystemGsmListInfo info = new SystemGsmListInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    info.SentTime = HJConvert.ToDateTime(dr["SentTime"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    info.Sum = HJConvert.ToInt32(dr["Sum"]);
                    info.SentType = HJConvert.ToInt32(dr["SentType"]);
                    info.Content = HJConvert.ToString(dr["Content"]);
                    info.UserIdList = HJConvert.ToString(dr["UserIdList"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.DelMoney = HJConvert.ToDecimal(dr["DelMoney"]);
                    info.TogoName = HJConvert.ToString(dr["TogoName"]);
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
        public int DelSystemGsmList(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from SystemGsmList where DataId=@DataId");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@DataId",SqlDbType.Int)
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
            str.Append("delete from SystemGsmList where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}