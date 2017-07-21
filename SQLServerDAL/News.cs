/*********************************************************************
 * CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved.
 * Function : 公告信息实现类
 * Created by jijunjian at 2010-8-5 17:53:31.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;
using System.Data.SqlClient;

using System.Data;//请先添加引用

namespace Hangjing.SQLServerDAL
{
    public class News
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(NewsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into News(");
            strSql.Append("Title,Posttime,SortNum,EContent,reve1,Reve2)");
            strSql.Append(" values (");
            strSql.Append("@Title,@Posttime,@SortNum,@EContent,@reve1,@Reve2)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@Title", SqlDbType.VarChar,256),
				new SqlParameter("@Posttime", SqlDbType.DateTime),
				new SqlParameter("@SortNum", SqlDbType.Int,4),
				new SqlParameter("@EContent", SqlDbType.Text),
				new SqlParameter("@reve1", SqlDbType.Int,4),
				new SqlParameter("@Reve2", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Posttime;
            parameters[2].Value = model.SortNum;
            parameters[3].Value = model.EContent;
            parameters[4].Value = model.reve1;
            parameters[5].Value = model.Reve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(NewsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update News set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Posttime=@Posttime,");
            strSql.Append("SortNum=@SortNum,");
            strSql.Append("EContent=@EContent,");
            strSql.Append("reve1=@reve1,");
            strSql.Append("Reve2=@Reve2");
            strSql.Append(" where DataID=@DataID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataID", SqlDbType.Int,4),
				new SqlParameter("@Title", SqlDbType.VarChar,256),
				new SqlParameter("@Posttime", SqlDbType.DateTime),
				new SqlParameter("@SortNum", SqlDbType.Int,4),
				new SqlParameter("@EContent", SqlDbType.Text),
				new SqlParameter("@reve1", SqlDbType.Int,4),
				new SqlParameter("@Reve2", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.DataID;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Posttime;
            parameters[3].Value = model.SortNum;
            parameters[4].Value = model.EContent;
            parameters[5].Value = model.reve1;
            parameters[6].Value = model.Reve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataID</param>
        /// <returns>NewsInfo</returns>
        public NewsInfo GetModel(int DataID)
        {
            string sql = "select DataID,Title,Posttime,SortNum,EContent,reve1,Reve2 from News where dataid = @DataID";
            SqlParameter parameter = new SqlParameter("@DataID", SqlDbType.Int, 4);
            parameter.Value = DataID;
            NewsInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new NewsInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.Title = HJConvert.ToString(dr["Title"]);
                    model.Posttime = HJConvert.ToDateTime(dr["Posttime"]);
                    model.SortNum = HJConvert.ToInt32(dr["SortNum"]);
                    model.EContent = HJConvert.ToString(dr["EContent"]);
                    model.reve1 = HJConvert.ToInt32(dr["reve1"]);
                    model.Reve2 = HJConvert.ToString(dr["Reve2"]);
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
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "News"), new SqlParameter("@strWhere", strWhere)));
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
        public IList<NewsInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<NewsInfo> infos = new List<NewsInfo>();
            SqlParameter[] parameters = 
	        {
		        new SqlParameter("@tblName",SqlDbType.VarChar,255),
		        new SqlParameter("@strGetFields", SqlDbType.VarChar,1000),
		        new SqlParameter("@primary", SqlDbType.VarChar,255),
		        new SqlParameter("@orderName",SqlDbType.VarChar,255),
		        new SqlParameter("@PageSize", SqlDbType.Int),
		        new SqlParameter("@PageIndex", SqlDbType.Int),
		        new SqlParameter("@OrderType", SqlDbType.Bit),
		        new SqlParameter("@strWhere", SqlDbType.VarChar,1500)
	        };
            parameters[0].Value = "News";
            parameters[1].Value = "DataID,Title,Posttime,SortNum,EContent,reve1,Reve2";
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
                    NewsInfo info = new NewsInfo();
                    info.DataID = HJConvert.ToInt32(dr["DataID"]);
                    info.Title = HJConvert.ToString(dr["Title"]);
                    info.Posttime = HJConvert.ToDateTime(dr["Posttime"]);
                    info.SortNum = HJConvert.ToInt32(dr["SortNum"]);
                    info.EContent = HJConvert.ToString(dr["EContent"]);
                    info.reve1 = HJConvert.ToInt32(dr["reve1"]);
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
        public int DelNews(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from News where DataID=@DataID");
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
            str.Append("delete from News where DataID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}
