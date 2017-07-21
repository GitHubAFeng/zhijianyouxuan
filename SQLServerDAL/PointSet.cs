// PointSet.cs:积分配置功能.
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
    public class PointSet
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.PointSetInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PointSet(");
            strSql.Append("KeyName,KeyValue,InUse)");
            strSql.Append(" values (");
            strSql.Append("@KeyName,@KeyValue,@InUse)");
            SqlParameter[] parameters =
            {
			    new SqlParameter("@KeyName", SqlDbType.VarChar,50),
			    new SqlParameter("@KeyValue", SqlDbType.Int,4),
			    new SqlParameter("@InUse", SqlDbType.VarChar,50)
            };
            parameters[0].Value = model.KeyName;
            parameters[1].Value = model.KeyValue;
            parameters[2].Value = model.InUse;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.PointSetInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PointSet set ");
            strSql.Append("KeyName=@KeyName,");
            strSql.Append("KeyValue=@KeyValue,");
            strSql.Append("InUse=@InUse");
            strSql.Append(" where DataId=@DataId ");
            SqlParameter[] parameters = 
            {
			    new SqlParameter("@DataId", SqlDbType.Int,4),
			    new SqlParameter("@KeyName", SqlDbType.VarChar,50),
			    new SqlParameter("@KeyValue", SqlDbType.Int,4),
			    new SqlParameter("@InUse", SqlDbType.VarChar,50)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.KeyName;
            parameters[2].Value = model.KeyValue;
            parameters[3].Value = model.InUse;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>PointSetInfo</returns>
        public PointSetInfo GetModel(int DataId)
        {
            string sql = "select DataId,KeyName,KeyValue,InUse from PointSet where  DataId = @DataId";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            PointSetInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new PointSetInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.KeyName = HJConvert.ToString(dr["KeyName"]);
                    model.KeyValue = HJConvert.ToInt32(dr["KeyValue"]);
                    model.InUse = HJConvert.ToString(dr["InUse"]);
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
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "TogoPointSet"), new SqlParameter("@strWhere", strWhere)));
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
        public IList<PointSetInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<PointSetInfo> infos = new List<PointSetInfo>();
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
            parameters[0].Value = "PointSet";
            parameters[1].Value = "DataId,KeyName,KeyValue,InUse";
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
                    PointSetInfo info = new PointSetInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.KeyName = HJConvert.ToString(dr["KeyName"]);
                    info.KeyValue = HJConvert.ToInt32(dr["KeyValue"]);
                    info.InUse = HJConvert.ToString(dr["InUse"]);
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
        public int DelPointSet(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from PointSet where DataId=@DataId");
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
            str.Append("delete from PointSet where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}
