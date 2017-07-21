/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * Created by wanghui at 2011-5-12 9:03:30.
 * E-Mail   : wanghui@ihangjing.com
*********************************************************************/
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
    /// 数据访问类EmailGsmRecord
    /// </summary>
    public class EmailGsmRecord
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(EmailGsmRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into EmailGsmRecord(");
            strSql.Append("DataId,TogoId,DelMoney,AddDate,Sum,SentType,Content,UserIdList,Status,Inve1,Inve2)");
            strSql.Append(" values (");
            strSql.Append("@DataId,@TogoId,@DelMoney,@AddDate,@Sum,@SentType,@Content,@UserIdList,@Status,@Inve1,@Inve2);SELECT LAST_INSERT_ID();");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataId", SqlDbType.Int,4),
				new SqlParameter("@TogoId", SqlDbType.Int,4),
				new SqlParameter("@DelMoney", SqlDbType.Decimal,9),
				new SqlParameter("@AddDate", SqlDbType.DateTime),
				new SqlParameter("@Sum", SqlDbType.Int,4),
				new SqlParameter("@SentType", SqlDbType.Int,4),
				new SqlParameter("@Content", SqlDbType.VarChar,4096),
				new SqlParameter("@UserIdList", SqlDbType.VarChar,4096),
				new SqlParameter("@Status", SqlDbType.Int,4),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.TogoId;
            parameters[2].Value = model.DelMoney;
            parameters[3].Value = model.AddDate;
            parameters[4].Value = model.Sum;
            parameters[5].Value = model.SentType;
            parameters[6].Value = model.Content;
            parameters[7].Value = model.UserIdList;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.Inve1;
            parameters[10].Value = model.Inve2;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(EmailGsmRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update EmailGsmRecord set ");
            strSql.Append("TogoId=@TogoId,");
            strSql.Append("DelMoney=@DelMoney,");
            strSql.Append("AddDate=@AddDate,");
            strSql.Append("Sum=@Sum,");
            strSql.Append("SentType=@SentType,");
            strSql.Append("Content=@Content,");
            strSql.Append("UserIdList=@UserIdList,");
            strSql.Append("Status=@Status,");
            strSql.Append("Inve1=@Inve1,");
            strSql.Append("Inve2=@Inve2");
            strSql.Append(" where DataId=@DataId ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataId", SqlDbType.Int,4),
				new SqlParameter("@TogoId", SqlDbType.Int,4),
				new SqlParameter("@DelMoney", SqlDbType.Decimal,9),
				new SqlParameter("@AddDate", SqlDbType.DateTime),
				new SqlParameter("@Sum", SqlDbType.Int,4),
				new SqlParameter("@SentType", SqlDbType.Int,4),
				new SqlParameter("@Content", SqlDbType.VarChar,4096),
				new SqlParameter("@UserIdList", SqlDbType.VarChar,4096),
				new SqlParameter("@Status", SqlDbType.Int,4),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.TogoId;
            parameters[2].Value = model.DelMoney;
            parameters[3].Value = model.AddDate;
            parameters[4].Value = model.Sum;
            parameters[5].Value = model.SentType;
            parameters[6].Value = model.Content;
            parameters[7].Value = model.UserIdList;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.Inve1;
            parameters[10].Value = model.Inve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>EmailGsmRecordInfo</returns>
        public EmailGsmRecordInfo GetModel(int DataId)
        {
            string sql = "select EmailGsmRecord.DataId,EmailGsmRecord.TogoId,EmailGsmRecord.DelMoney,EmailGsmRecord.AddDate,EmailGsmRecord.Sum,EmailGsmRecord.SentType,EmailGsmRecord.Content,EmailGsmRecord.UserIdList,EmailGsmRecord.Status,EmailGsmRecord.Inve1,EmailGsmRecord.Inve2,points.Name as togoname from EmailGsmRecord Left join points on EmailGsmRecord.TogoId=points.unid where  EmailGsmRecord.DataId = @DataId";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            EmailGsmRecordInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new EmailGsmRecordInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    model.DelMoney = HJConvert.ToDecimal(dr["DelMoney"]);
                    model.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    model.Sum = HJConvert.ToInt32(dr["Sum"]);
                    model.SentType = HJConvert.ToInt32(dr["SentType"]);
                    model.Content = HJConvert.ToString(dr["Content"]);
                    model.UserIdList = HJConvert.ToString(dr["UserIdList"]);
                    model.Status = HJConvert.ToInt32(dr["Status"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
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
            parameters[0].Value = "EmailGsmRecord";
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
        public IList<EmailGsmRecordInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<EmailGsmRecordInfo> infos = new List<EmailGsmRecordInfo>();
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
            parameters[0].Value = "EmailGsmRecord";
            parameters[1].Value = "DataId,TogoId,DelMoney,AddDate,Sum,SentType,Content,UserIdList,Status,Inve1,Inve2,(select name from points Where unid=EmailGsmRecord.TogoId ) as TogoName";
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
                    EmailGsmRecordInfo info = new EmailGsmRecordInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    info.DelMoney = HJConvert.ToDecimal(dr["DelMoney"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    info.Sum = HJConvert.ToInt32(dr["Sum"]);
                    info.SentType = HJConvert.ToInt32(dr["SentType"]);
                    info.Content = HJConvert.ToString(dr["Content"]);
                    info.UserIdList = HJConvert.ToString(dr["UserIdList"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
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
        public int DelEmailGsmRecord(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from EmailGsmRecord where DataId=@DataId");
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
            str.Append("delete from EmailGsmRecord where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}
