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
    /// <summary>
    /// 数据访问类TogoPointSet。
    /// </summary>
    public class TogoPointSet
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TogoPointSetInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TogoPointSet(");
            strSql.Append("DataId,TogoId,Multiple,StartTime,EndTime,Inve1,Inve2)");
            strSql.Append(" values (");
            strSql.Append("@DataId,@TogoId,@Multiple,@StartTime,@EndTime,@Inve1,@Inve2)");
            SqlParameter[] parameters = 
            {
			    new SqlParameter("@DataId", SqlDbType.Int,4),
				new SqlParameter("@TogoId", SqlDbType.Int,4),
				new SqlParameter("@Multiple", SqlDbType.Decimal,5),
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EndTime", SqlDbType.DateTime),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.Int,4)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.TogoId;
            parameters[2].Value = model.Multiple;
            parameters[3].Value = model.StartTime;
            parameters[4].Value = model.EndTime;
            parameters[5].Value = model.Inve1;
            parameters[6].Value = model.Inve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(TogoPointSetInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TogoPointSet set ");
            strSql.Append("TogoId=@TogoId,");
            strSql.Append("Multiple=@Multiple,");
            strSql.Append("StartTime=@StartTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("Inve1=@Inve1,");
            strSql.Append("Inve2=@Inve2");
            strSql.Append(" where DataId=@DataId ");
            SqlParameter[] parameters = 
            {
			    new SqlParameter("@DataId", SqlDbType.Int,4),
			    new SqlParameter("@TogoId", SqlDbType.Int,4),
			    new SqlParameter("@Multiple", SqlDbType.Decimal,5),
			    new SqlParameter("@StartTime", SqlDbType.DateTime),
			    new SqlParameter("@EndTime", SqlDbType.DateTime),
			    new SqlParameter("@Inve1", SqlDbType.Int,4),
			    new SqlParameter("@Inve2", SqlDbType.Int,4)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.TogoId;
            parameters[2].Value = model.Multiple;
            parameters[3].Value = model.StartTime;
            parameters[4].Value = model.EndTime;
            parameters[5].Value = model.Inve1;
            parameters[6].Value = model.Inve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>TogoPointSetInfo</returns>
        public TogoPointSetInfo GetModel(int DataId)
        {
            string sql = "select DataId,TogoId,Multiple,StartTime,EndTime,Inve1,Inve2 from TogoPointSet where DataId=@DataId ";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            TogoPointSetInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new TogoPointSetInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    model.Multiple = HJConvert.ToDecimal(dr["Multiple"]);
                    model.StartTime = HJConvert.ToDateTime(dr["StartTime"]);
                    model.EndTime = HJConvert.ToDateTime(dr["EndTime"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToInt32(dr["Inve2"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "TogoPointSet "), new SqlParameter("@strWhere", strWhere));
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
        public IList<TogoPointSetInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<TogoPointSetInfo> infos = new List<TogoPointSetInfo>();
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
            parameters[0].Value = "TogoPointSet";
            parameters[1].Value = "DataId,TogoId,Multiple,StartTime,EndTime,Inve1,Inve2";
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
                    TogoPointSetInfo info = new TogoPointSetInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    info.Multiple = HJConvert.ToDecimal(dr["Multiple"]);
                    info.StartTime = HJConvert.ToDateTime(dr["StartTime"]);
                    info.EndTime = HJConvert.ToDateTime(dr["EndTime"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToInt32(dr["Inve2"]);
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
        public int DelTogoPointSet(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from TogoPointSet where DataId=@DataId");
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
            str.Append("delete from TogoPointSet where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}
