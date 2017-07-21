// WebMessage.cs:站内信发送记录类.
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
    public class WebMessageLog
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(WebMessageLogInfo  model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WebMessageLog(");
            strSql.Append("UserId,MessageId,AddDate,Status)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@MessageId,@AddDate,@Status)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@UserId", SqlDbType.Int,4),
				new SqlParameter("@MessageId", SqlDbType.Int,4),
				new SqlParameter("@AddDate", SqlDbType.DateTime),
				new SqlParameter("@Status", SqlDbType.Int,4)
            };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.MessageId;
            parameters[2].Value = model.AddDate;
            parameters[3].Value = model.Status;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(WebMessageLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WebMessageLog set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("MessageId=@MessageId,");
            strSql.Append("AddDate=@AddDate,");
            strSql.Append("Status=@Status");
            strSql.Append(" where DataId=@DataId ");
            SqlParameter[] parameters =
            {
				new SqlParameter("@DataId", SqlDbType.Int,4),
				new SqlParameter("@UserId", SqlDbType.Int,4),
				new SqlParameter("@MessageId", SqlDbType.Int,4),
				new SqlParameter("@AddDate", SqlDbType.DateTime),
				new SqlParameter("@Status", SqlDbType.Int,4)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.MessageId;
            parameters[3].Value = model.AddDate;
            parameters[4].Value = model.Status;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>WebMessageLogInfo</returns>
        public WebMessageLogInfo GetModel(int DataId)
        {
           //string sql=" webmessageLog_GetBywebmessageDataId ";
            string sql = "select DataId,UserId,MessageId,AddDate,Status,(select ecustomer.Name from ecustomer where WebMessageLog.UserId = ecustomer.DataID) as UserName ,(select WebMessage.Title from WebMessage where WebMessageLog.MessageId = WebMessage.DataId ) as Title  from WebMessageLog where  WebMessageLog.DataId = @nDataId";
            SqlParameter parameter = new SqlParameter("@nDataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            WebMessageLogInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text,sql , parameter))
            {
                if (dr.Read())
                {
                    model = new WebMessageLogInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.UserId = HJConvert.ToInt32(dr["UserId"]);
                    model.MessageId = HJConvert.ToInt32(dr["MessageId"]);
                    model.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    model.Status = HJConvert.ToInt32(dr["Status"]);
                    model.Title = HJConvert.ToString(dr["Title"]);
                    model.UserName = HJConvert.ToString(dr["UserName"]);
                  //  model.Message = HJConvert.ToString(dr["Message"]);
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
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "WebMessageLog"), new SqlParameter("@strWhere", strWhere)));
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
        public IList<WebMessageLogInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<WebMessageLogInfo> infos = new List<WebMessageLogInfo>();
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
            parameters[0].Value = "WebMessageLog";//Left join WebMessage on WebMessageLog.MessageId = WebMessage.DataId Title,
            parameters[1].Value = "DataId,UserId,MessageId,AddDate,Status,(select ecustomer.Name from ecustomer where WebMessageLog.UserId = ecustomer.DataID) as UserName ,(select WebMessage.Title from WebMessage where WebMessageLog.MessageId = WebMessage.DataId ) as Title  ";
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
                    WebMessageLogInfo info = new WebMessageLogInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.UserId = HJConvert.ToInt32(dr["UserId"]);
                    info.MessageId = HJConvert.ToInt32(dr["MessageId"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.Title = HJConvert.ToString(dr["Title"]);
                    info.UserName = HJConvert.ToString(dr["UserName"]);
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
        public int DelWebMessageLog(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from WebMessageLog where DataId=@DataId");
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
            str.Append("delete from WebMessageLog where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

    }
}
