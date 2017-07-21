// EPointRecord.cs 
// CopyRight (c) 2009-2011 HangJing Teconology. All Rights Reserved.
// zjf@@ihangjing.com
// 2011-05-06
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;

using System.Data.SqlClient;

namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 数据访问类EPointRecord。
    /// </summary>
    public class EPointRecord
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(EPointRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into EPointRecord(");
            strSql.Append("UserID,Point,Event,Time)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@Point,@Event,@Time)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@UserID", SqlDbType.Int,20),
				new SqlParameter("@Point", SqlDbType.Int,4),
				new SqlParameter("@Event", SqlDbType.VarChar,256),
				new SqlParameter("@Time", SqlDbType.DateTime)
            };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.Point;
            parameters[2].Value = model.Event;
            parameters[3].Value = model.Time;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(EPointRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update EPointRecord set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("Point=@Point,");
            strSql.Append("Event=@Event,");
            strSql.Append("Time=@Time");
            strSql.Append(" where DataID=@DataID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataID", SqlDbType.VarChar,20),
				new SqlParameter("@UserID", SqlDbType.VarChar,20),
				new SqlParameter("@Point", SqlDbType.Int,4),
				new SqlParameter("@Event", SqlDbType.VarChar,256),
				new SqlParameter("@Time", SqlDbType.DateTime)
            };
            parameters[0].Value = model.DataID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.Point;
            parameters[3].Value = model.Event;
            parameters[4].Value = model.Time;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataID</param>
        /// <returns>EPointRecordInfo</returns>
        public EPointRecordInfo GetModel(int DataID)
        {
            string sql = "select DataID,UserID,Point,Event,Time from EPointRecord";
            SqlParameter parameter = new SqlParameter("@DataID", SqlDbType.Int, 4);
            parameter.Value = DataID;
            EPointRecordInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new EPointRecordInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.UserID = HJConvert.ToInt32(dr["UserID"]);
                    model.Point = HJConvert.ToInt32(dr["Point"]);
                    model.Event = HJConvert.ToString(dr["Event"]);
                    model.Time = HJConvert.ToDateTime(dr["Time"]);
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
                new SqlParameter("@strWhere" , SqlDbType.VarChar ,500)
            };
            parameters[0].Value = "EPointRecord";
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
        public IList<EPointRecordInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<EPointRecordInfo> infos = new List<EPointRecordInfo>();

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
            parameters[0].Value = "EPointRecord";
            parameters[1].Value = "DataID,UserID,Point,Event,Time , (select name from ECustomer where  DataID =EPointRecord.UserID ) as uname";
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
                    EPointRecordInfo info = new EPointRecordInfo();
                    info.DataID = HJConvert.ToInt32(dr["DataID"]);
                    info.UserID = HJConvert.ToInt32(dr["UserID"]);
                    info.Point = HJConvert.ToInt32(dr["Point"]);
                    info.Event = HJConvert.ToString(dr["Event"]);
                    info.Time = HJConvert.ToDateTime(dr["Time"]);
                    info.uname = HJConvert.ToString(dr["uname"]);
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
        public int DelEPointRecord(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from EPointRecord where DataID=@DataID");
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
            str.Append("delete from EPointRecord where DataID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}

