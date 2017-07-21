/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : $codebesideclassname$
 * Function : 
 * Created by jijunjian at 2010-11-16 21:54:06.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Hangjing.Common;
using Hangjing.Model;
using Hangjing.DBUtility;

namespace Hangjing.SQLServerDAL
{
    public class acount 
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(acountInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into acount(");
            strSql.Append("DataID,Ali_Seller_Mail,Ali_Key,Ali_Partner,Sxy_Partner,Sxy_Key,ALI_NOTIFY_URL,ALI_RETURN_URL,Reve1,Reve2,Reve3)");
            strSql.Append(" values (");
            strSql.Append("@DataID,@Ali_Seller_Mail,@Ali_Key,@Ali_Partner,@Sxy_Partner,@Sxy_Key,@ALI_NOTIFY_URL,@ALI_RETURN_URL,@Reve1,@Reve2,@Reve3)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataID", SqlDbType.Int,4),
				new SqlParameter("@Ali_Seller_Mail", SqlDbType.VarChar,100),
				new SqlParameter("@Ali_Key", SqlDbType.VarChar,256),
				new SqlParameter("@Ali_Partner", SqlDbType.VarChar,256),
				new SqlParameter("@Sxy_Partner", SqlDbType.VarChar,50),
				new SqlParameter("@Sxy_Key", SqlDbType.VarChar,50),
				new SqlParameter("@ALI_NOTIFY_URL", SqlDbType.VarChar,256),
				new SqlParameter("@ALI_RETURN_URL", SqlDbType.VarChar,256),
				new SqlParameter("@Reve1", SqlDbType.VarChar,256),
				new SqlParameter("@Reve2", SqlDbType.VarChar,256),
				new SqlParameter("@Reve3", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.DataID;
            parameters[1].Value = model.Ali_Seller_Mail;
            parameters[2].Value = model.Ali_Key;
            parameters[3].Value = model.Ali_Partner;
            parameters[4].Value = model.Sxy_Partner;
            parameters[5].Value = model.Sxy_Key;
            parameters[6].Value = model.ALI_NOTIFY_URL;
            parameters[7].Value = model.ALI_RETURN_URL;
            parameters[8].Value = model.Reve1;
            parameters[9].Value = model.Reve2;
            parameters[10].Value = model.Reve3;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(acountInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update acount set ");
            strSql.Append("Ali_Seller_Mail=@Ali_Seller_Mail,");
            strSql.Append("Ali_Key=@Ali_Key,");
            strSql.Append("Ali_Partner=@Ali_Partner,");
            strSql.Append("Sxy_Partner=@Sxy_Partner,");
            strSql.Append("Sxy_Key=@Sxy_Key,");
            strSql.Append("ALI_NOTIFY_URL=@ALI_NOTIFY_URL,");
            strSql.Append("ALI_RETURN_URL=@ALI_RETURN_URL,");
            strSql.Append("Reve1=@Reve1,");
            strSql.Append("Reve2=@Reve2,");
            strSql.Append("Reve3=@Reve3");
            strSql.Append(" where ");
            strSql.Append("DataID=@DataID");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataID", SqlDbType.Int,4),
				new SqlParameter("@Ali_Seller_Mail", SqlDbType.VarChar,100),
				new SqlParameter("@Ali_Key", SqlDbType.VarChar,256),
				new SqlParameter("@Ali_Partner", SqlDbType.VarChar,256),
				new SqlParameter("@Sxy_Partner", SqlDbType.VarChar,50),
				new SqlParameter("@Sxy_Key", SqlDbType.VarChar,50),
				new SqlParameter("@ALI_NOTIFY_URL", SqlDbType.VarChar,256),
				new SqlParameter("@ALI_RETURN_URL", SqlDbType.VarChar,256),
				new SqlParameter("@Reve1", SqlDbType.VarChar,256),
				new SqlParameter("@Reve2", SqlDbType.VarChar,256),
				new SqlParameter("@Reve3", SqlDbType.VarChar,1024)
            };
            parameters[0].Value = model.DataID;
            parameters[1].Value = model.Ali_Seller_Mail;
            parameters[2].Value = model.Ali_Key;
            parameters[3].Value = model.Ali_Partner;
            parameters[4].Value = model.Sxy_Partner;
            parameters[5].Value = model.Sxy_Key;
            parameters[6].Value = model.ALI_NOTIFY_URL;
            parameters[7].Value = model.ALI_RETURN_URL;
            parameters[8].Value = model.Reve1;
            parameters[9].Value = model.Reve2;
            parameters[10].Value = model.Reve3;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataID</param>
        /// <returns>acountInfo</returns>
        public acountInfo GetModel(int DataID)
        {
            string sql = "select DataID,Ali_Seller_Mail,Ali_Key,Ali_Partner,Sxy_Partner,Sxy_Key,ALI_NOTIFY_URL,ALI_RETURN_URL,Reve1,Reve2,Reve3 from acount where dataid =@DataID";
            SqlParameter parameter = new SqlParameter("@DataID", SqlDbType.Int, 4);
            parameter.Value = DataID;
            acountInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new acountInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.Ali_Seller_Mail = HJConvert.ToString(dr["Ali_Seller_Mail"]);
                    model.Ali_Key = HJConvert.ToString(dr["Ali_Key"]);
                    model.Ali_Partner = HJConvert.ToString(dr["Ali_Partner"]);
                    model.Sxy_Partner = HJConvert.ToString(dr["Sxy_Partner"]);
                    model.Sxy_Key = HJConvert.ToString(dr["Sxy_Key"]);
                    model.ALI_NOTIFY_URL = HJConvert.ToString(dr["ALI_NOTIFY_URL"]);
                    model.ALI_RETURN_URL = HJConvert.ToString(dr["ALI_RETURN_URL"]);
                    model.Reve1 = HJConvert.ToString(dr["Reve1"]);
                    model.Reve2 = HJConvert.ToString(dr["Reve2"]);
                    model.Reve3 = HJConvert.ToString(dr["Reve3"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "acount"), new SqlParameter("@strWhere", strWhere));
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
        public IList<acountInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<acountInfo> infos = new List<acountInfo>();
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
            parameters[0].Value = "acount";
            parameters[1].Value = "DataID,Ali_Seller_Mail,Ali_Key,Ali_Partner,Sxy_Partner,Sxy_Key,ALI_NOTIFY_URL,ALI_RETURN_URL,Reve1,Reve2,Reve3";
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
                    acountInfo info = new acountInfo();
                    info.DataID = HJConvert.ToInt32(dr["DataID"]);
                    info.Ali_Seller_Mail = HJConvert.ToString(dr["Ali_Seller_Mail"]);
                    info.Ali_Key = HJConvert.ToString(dr["Ali_Key"]);
                    info.Ali_Partner = HJConvert.ToString(dr["Ali_Partner"]);
                    info.Sxy_Partner = HJConvert.ToString(dr["Sxy_Partner"]);
                    info.Sxy_Key = HJConvert.ToString(dr["Sxy_Key"]);
                    info.ALI_NOTIFY_URL = HJConvert.ToString(dr["ALI_NOTIFY_URL"]);
                    info.ALI_RETURN_URL = HJConvert.ToString(dr["ALI_RETURN_URL"]);
                    info.Reve1 = HJConvert.ToString(dr["Reve1"]);
                    info.Reve2 = HJConvert.ToString(dr["Reve2"]);
                    info.Reve3 = HJConvert.ToString(dr["Reve3"]);
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
        public int Delacount(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from acount where DataID=@DataID");
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
            str.Append("delete from acount where DataID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}
