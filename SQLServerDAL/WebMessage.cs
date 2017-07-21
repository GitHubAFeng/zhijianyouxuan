// WebMessage.cs:站内信内容类.
// CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hangjing.DBUtility;
using Hangjing.Model;

using System.Data;
using System.Data.SqlClient;

namespace Hangjing.SQLServerDAL
{
   public class WebMessage
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(WebMessageInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WebMessage(");
            strSql.Append("Title,Message,AddDate)");
            strSql.Append(" values (");
            strSql.Append("@Title,@Message,@AddDate)");
            SqlParameter [] parameters = 
            {
				new SqlParameter("@Title", SqlDbType.VarChar,50),
				new SqlParameter("@Message", SqlDbType.VarChar,50),
				new SqlParameter("@AddDate", SqlDbType.DateTime)
            };
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Message;
            parameters[2].Value = model.AddDate;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(WebMessageInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WebMessage set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Message=@Message,");
            strSql.Append("AddDate=@AddDate");
            strSql.Append(" where DataId=@DataId ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataId", SqlDbType.Int,4),
				new SqlParameter("@Title", SqlDbType.VarChar,50),
				new SqlParameter("@Message", SqlDbType.VarChar,4096),
				new SqlParameter("@AddDate", SqlDbType.DateTime)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Message;
            parameters[3].Value = model.AddDate;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>WebMessageInfo</returns>
        public WebMessageInfo GetModel(int DataId)
        {
            string sql = "select DataId,Title,Message,AddDate from WebMessage where  DataId = @DataId";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            WebMessageInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new WebMessageInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.Title = HJConvert.ToString(dr["Title"]);
                    model.Message = HJConvert.ToString(dr["Message"]);
                    model.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
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
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "WebMessage"), new SqlParameter("@strWhere", strWhere)));
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
        public IList<WebMessageInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<WebMessageInfo> infos = new List<WebMessageInfo>();
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
            parameters[0].Value = "WebMessage";
            parameters[1].Value = "DataId,Title,Message,AddDate";
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
                    WebMessageInfo info = new WebMessageInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.Title = HJConvert.ToString(dr["Title"]);
                    info.Message = HJConvert.ToString(dr["Message"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
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
        public int DelWebMessage(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from WebMessage where DataId=@DataId");
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
            str.Append("delete from WebMessage where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}
