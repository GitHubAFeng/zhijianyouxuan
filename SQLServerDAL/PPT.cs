/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : $codebesideclassname$
 * Function : 
 * Created by jijunjian at 2010-8-21 14:26:55.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;


using Hangjing.DBUtility;
using Hangjing.Model;

namespace Hangjing.SQLServerDAL
{
    public class PPT
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PPTInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PPT(");
            strSql.Append("picture,Title,PUrl,Reve1,Reve2,SecID)");
            strSql.Append(" values (");
            strSql.Append("@picture,@Title,@PUrl,@Reve1,@Reve2,@SecID)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@picture", SqlDbType.VarChar,512),
				new SqlParameter("@Title", SqlDbType.VarChar,50),
				new SqlParameter("@PUrl", SqlDbType.VarChar,512),
				new SqlParameter("@Reve1", SqlDbType.Int,4),
				new SqlParameter("@Reve2", SqlDbType.VarChar,256),
                new SqlParameter("@SecID", SqlDbType.Int,4)
            };
            parameters[0].Value = model.picture;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.PUrl;
            parameters[3].Value = model.Reve1;
            parameters[4].Value = model.Reve2;
            parameters[5].Value = model.SecID;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(PPTInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PPT set ");
            strSql.Append("picture=@picture,");
            strSql.Append("Title=@Title,");
            strSql.Append("PUrl=@PUrl,");
            strSql.Append("Reve1=@Reve1,");
            strSql.Append("Reve2=@Reve2,SecID=@SecID");
            strSql.Append(" where DataID=@DataID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataID", SqlDbType.Int,4),
				new SqlParameter("@picture", SqlDbType.VarChar,512),
				new SqlParameter("@Title", SqlDbType.VarChar,50),
				new SqlParameter("@PUrl", SqlDbType.VarChar,512),
				new SqlParameter("@Reve1", SqlDbType.Int,4),
				new SqlParameter("@Reve2", SqlDbType.VarChar,256),
                new SqlParameter("@SecID", SqlDbType.Int,4)
            };
            parameters[0].Value = model.DataID;
            parameters[1].Value = model.picture;
            parameters[2].Value = model.Title;
            parameters[3].Value = model.PUrl;
            parameters[4].Value = model.Reve1;
            parameters[5].Value = model.Reve2;
            parameters[6].Value = model.SecID;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataID</param>
        /// <returns>PPTInfo</returns>
        public PPTInfo GetModel(int DataID)
        {
            string sql = "select * from PPT where dataid = @DataID";
            SqlParameter parameter = new SqlParameter("@DataID", SqlDbType.Int, 4);
            parameter.Value = DataID;
            PPTInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new PPTInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.picture = HJConvert.ToString(dr["picture"]);
                    model.Title = HJConvert.ToString(dr["Title"]);
                    model.PUrl = HJConvert.ToString(dr["PUrl"]);
                    model.Reve1 = HJConvert.ToInt32(dr["Reve1"]);
                    model.Reve2 = HJConvert.ToString(dr["Reve2"]);
                    model.SecID = HJConvert.ToInt32(dr["SecID"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "PPT"), new SqlParameter("@strWhere", strWhere));
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
        public IList<PPTInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<PPTInfo> infos = new List<PPTInfo>();
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
            parameters[0].Value = "PPT";
            parameters[1].Value = "DataID,picture,Title,PUrl,Reve1,Reve2";
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
                    PPTInfo info = new PPTInfo();
                    info.DataID = HJConvert.ToInt32(dr["DataID"]);
                    info.picture = HJConvert.ToString(dr["picture"]);
                    info.Title = HJConvert.ToString(dr["Title"]);
                    info.PUrl = HJConvert.ToString(dr["PUrl"]);
                    info.Reve1 = HJConvert.ToInt32(dr["Reve1"]);
                    info.Reve2 = HJConvert.ToString(dr["Reve2"]);
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
        public int DelPPT(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from PPT where DataID=@DataID");
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
            str.Append("delete from PPT where DataID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

    }
}
