using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

using Hangjing.DBUtility;//请先添加引用
using Hangjing.Model;

namespace Hangjing.SQLServerDAL
{
	/// <summary>
	/// 数据访问类WebBasic。
	/// </summary>
	public class WebBasic
	{
		/// <summary>
        /// 
		/// 增加一条数据
		/// </summary>
		public int Add(Hangjing.Model.WebBasicInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into WebBasic(");
			strSql.Append("CKey,CValue,Inve1)");
			strSql.Append(" values (");
			strSql.Append("@Key,@Value,@Inve1)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@Key", SqlDbType.VarChar,50),
				new SqlParameter("@Value", SqlDbType.VarChar,4096),
				new SqlParameter("@Inve1", SqlDbType.VarChar,50)
            };
			parameters[0].Value = model.Key;
			parameters[1].Value = model.Value;
			parameters[2].Value = model.Inve1;

			return SQLHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(Hangjing.Model.WebBasicInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update WebBasic set ");
            strSql.Append("[CKey]=@Key,");
            strSql.Append("CValue=@Value,");
			strSql.Append("Inve1=@Inve1");
			strSql.Append(" where DataId=@DataId ");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@DataId", SqlDbType.Int,4),
				new SqlParameter("@Key", SqlDbType.VarChar,50),
				new SqlParameter("@Value", SqlDbType.Text),
				new SqlParameter("@Inve1", SqlDbType.VarChar,512)
            };
			parameters[0].Value = model.DataId;
			parameters[1].Value = model.Key;
			parameters[2].Value = model.Value;
			parameters[3].Value = model.Inve1;

		    return	SQLHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(),parameters);
		}

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>WebBasicInfo</returns>
        public WebBasicInfo GetModel(int DataId)
        {
            string sql = "select DataId,[CKey],[CValue],Inve1 from WebBasic WHERE DataId = @DataId";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            WebBasicInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new WebBasicInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.Key = HJConvert.ToString(dr["CKey"]);
                    model.Value = HJConvert.ToString(dr["CValue"]);
                    model.Inve1 = HJConvert.ToString(dr["Inve1"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "WebBasic"), new SqlParameter("@strWhere", strWhere));
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
        public List<WebBasicInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            List<WebBasicInfo> infos = new List<WebBasicInfo>();
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
            parameters[0].Value = "WebBasic";
            parameters[1].Value = "DataId,[CKey],[CValue],Inve1,stype";
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
                    WebBasicInfo info = new WebBasicInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.Key = HJConvert.ToString(dr["CKey"]);
                    info.Value = HJConvert.ToString(dr["CValue"]);
                    info.Inve1 = HJConvert.ToString(dr["Inve1"]);
                    info.stype = HJConvert.ToInt32(dr["stype"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        public List<WebBasicInfo> GetAllData(string strWhere)
        {
            List<WebBasicInfo> infos = new List<WebBasicInfo>();
            string condition = "SELECT * FROM WebBasic";
            if (string.IsNullOrEmpty(strWhere) == false)
            {
                condition += (" WHERE " + strWhere);
            }                                      
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, condition, null))
            {
                while (dr.Read())
                {
                    WebBasicInfo info = new WebBasicInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.Key = HJConvert.ToString(dr["CKey"]);
                    info.Value = HJConvert.ToString(dr["CValue"]);
                    info.Inve1 = HJConvert.ToString(dr["Inve1"]);
                    info.stype = HJConvert.ToInt32(dr["stype"]);
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
        public int DelWebBasic(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from WebBasic where DataId=@DataId");
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
            str.Append("delete from WebBasic where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

	}
}

