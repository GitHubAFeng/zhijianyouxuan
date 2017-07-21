// msgRecord.cs : 催单
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hangjing.Model;
using Hangjing.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace Hangjing.SQLServerDAL
{
    public class msgRecord
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.msgRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into msgRecord(");
            strSql.Append("OrderId,AddDate,Contents,Inve1,Inve2,Inve3,Inve4)");
            strSql.Append(" values (");
            strSql.Append("@OrderId,@AddDate,@Contents,@Inve1,@Inve2,@Inve3,@Inve4)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@OrderId", SqlDbType.VarChar,256),
				new SqlParameter("@AddDate", SqlDbType.DateTime),
				new SqlParameter("@Contents", SqlDbType.Text),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.Text),
				new SqlParameter("@Inve3", SqlDbType.VarChar,256),
				new SqlParameter("@Inve4", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.AddDate;
            parameters[2].Value = model.Contents;
            parameters[3].Value = model.Inve1;
            parameters[4].Value = model.Inve2;
            parameters[5].Value = model.Inve3;
            parameters[6].Value = model.Inve4;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.msgRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update msgRecord set ");
            strSql.Append("OrderId=@OrderId,");
            strSql.Append("AddDate=@AddDate,");
            strSql.Append("Contents=@Contents,");
            strSql.Append("Inve1=@Inve1,");
            strSql.Append("Inve2=@Inve2,");
            strSql.Append("Inve3=@Inve3,");
            strSql.Append("Inve4=@Inve4");
            strSql.Append(" where RanId=@RanId ");
            SqlParameter[] parameters =
            {
				new SqlParameter("@RanId", SqlDbType.Int,4),
				new SqlParameter("@OrderId", SqlDbType.VarChar,256),
				new SqlParameter("@AddDate", SqlDbType.DateTime),
				new SqlParameter("@Contents", SqlDbType.Text),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,256),
				new SqlParameter("@Inve3", SqlDbType.VarChar,256),
				new SqlParameter("@Inve4", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.RanId;
            parameters[1].Value = model.OrderId;
            parameters[2].Value = model.AddDate;
            parameters[3].Value = model.Contents;
            parameters[4].Value = model.Inve1;
            parameters[5].Value = model.Inve2;
            parameters[6].Value = model.Inve3;
            parameters[7].Value = model.Inve4;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>RanId</param>
        /// <returns>msgRecordInfo</returns>
        public msgRecordInfo GetModel(int RanId)
        {
            string sql = "select RanId,OrderId,AddDate,Contents,Inve1,Inve2,Inve3,Inve4 from msgRecord where  RanId = @RanId";
            SqlParameter parameter = new SqlParameter("@RanId", SqlDbType.Int, 4);
            parameter.Value = RanId;
            msgRecordInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new msgRecordInfo();
                    model.RanId = HJConvert.ToInt32(dr["RanId"]);
                    model.OrderId = HJConvert.ToString(dr["OrderId"]);
                    model.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    model.Contents = HJConvert.ToString(dr["Contents"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.Inve3 = HJConvert.ToString(dr["Inve3"]);
                    model.Inve4 = HJConvert.ToString(dr["Inve4"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "msgRecord"), new SqlParameter("@strWhere", strWhere));
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
        public IList<msgRecordInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<msgRecordInfo> infos = new List<msgRecordInfo>();
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
            parameters[0].Value = "msgRecord";
            parameters[1].Value = "RanId,OrderId,AddDate,Contents,Inve1,Inve2,Inve3,Inve4";
            parameters[2].Value = "RanId";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    msgRecordInfo info = new msgRecordInfo();
                    info.RanId = HJConvert.ToInt32(dr["RanId"]);
                    info.OrderId = HJConvert.ToString(dr["OrderId"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    info.Contents = HJConvert.ToString(dr["Contents"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.Inve3 = HJConvert.ToString(dr["Inve3"]);
                    info.Inve4 = HJConvert.ToString(dr["Inve4"]);
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
        public int DelReminder(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from msgRecord where RanId=@RanId");
            SqlParameter[] Para = 
			{
				new SqlParameter("@RanId",SqlDbType.Int)
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
            str.Append("delete from msgRecord where RanId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}
