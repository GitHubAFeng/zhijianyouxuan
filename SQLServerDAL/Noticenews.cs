
/*********************************************************************
 * CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved.
 * Function : 商家公告类
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
using System.Data;
namespace Hangjing.SQLServerDAL
{
   public class Noticenews
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
       public int Add(Hangjing.Model.NoticenewsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into noticenews(");
            strSql.Append("Title,producer,status,Adddate,inve1,inve2)");
            strSql.Append(" values (");
            strSql.Append("@Title,@producer,@status,@Adddate,@inve1,@inve2)");
            SqlParameter[] parameters =
            {
				new SqlParameter("@Title", SqlDbType.VarChar,50),
				new SqlParameter("@producer", SqlDbType.VarChar,256),
				new SqlParameter("@status", SqlDbType.Int,4),
				new SqlParameter("@Adddate", SqlDbType.DateTime),
				new SqlParameter("@inve1", SqlDbType.Int,4),
				new SqlParameter("@inve2", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.Title;
            parameters[1].Value = model.producer;
            parameters[2].Value = model.status;
            parameters[3].Value = model.Adddate;
            parameters[4].Value = model.inve1;
            parameters[5].Value = model.inve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.NoticenewsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update noticenews set ");
            strSql.Append("Title=@Title,");
            strSql.Append("producer=@producer,");
            strSql.Append("status=@status,");
            strSql.Append("Adddate=@Adddate,");
            strSql.Append("inve1=@inve1,");
            strSql.Append("inve2=@inve2");
            strSql.Append(" where DataId=@DataId ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataId", SqlDbType.Int,4),
				new SqlParameter("@Title", SqlDbType.VarChar,50),
				new SqlParameter("@producer", SqlDbType.VarChar,256),
				new SqlParameter("@status", SqlDbType.Int,4),
				new SqlParameter("@Adddate", SqlDbType.DateTime),
				new SqlParameter("@inve1", SqlDbType.Int,4),
				new SqlParameter("@inve2", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.producer;
            parameters[3].Value = model.status;
            parameters[4].Value = model.Adddate;
            parameters[5].Value = model.inve1;
            parameters[6].Value = model.inve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>noticenewsInfo</returns>
        public NoticenewsInfo GetModel(int DataId)
        {
            string sql = "select DataId,Title,producer,status,Adddate,inve1,inve2,(select Name from points where noticenews.inve1 =points.unid) as TogoName  from noticenews where  noticenews.DataId = @DataId";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            NoticenewsInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new NoticenewsInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.Title = HJConvert.ToString(dr["Title"]);
                    model.producer = HJConvert.ToString(dr["producer"]);
                    model.status = HJConvert.ToInt32(dr["status"]);
                    model.Adddate = HJConvert.ToDateTime(dr["Adddate"]);
                    model.inve1 = HJConvert.ToInt32(dr["inve1"]);
                    model.inve2 = HJConvert.ToString(dr["inve2"]);
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
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "noticenews"), new SqlParameter("@strWhere", strWhere)));
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
        public IList<NoticenewsInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<NoticenewsInfo> infos = new List<NoticenewsInfo>();
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
            parameters[0].Value = "noticenews";
            parameters[1].Value = "DataId,Title,producer,status,Adddate,inve1,inve2,(select name from points where unid = noticenews.inve1) as togoname";
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
                    NoticenewsInfo info = new NoticenewsInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.Title = HJConvert.ToString(dr["Title"]);
                    info.producer = HJConvert.ToString(dr["producer"]);
                    info.status = HJConvert.ToInt32(dr["status"]);
                    info.Adddate = HJConvert.ToDateTime(dr["Adddate"]);
                    info.inve1 = HJConvert.ToInt32(dr["inve1"]);
                    info.inve2 = HJConvert.ToString(dr["inve2"]);
                    info.TogoName = HJConvert.ToString(dr["togoname"]);
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
        public int Delnoticenews(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from noticenews where DataId=@DataId");
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
            str.Append("delete from noticenews where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

    }
}
