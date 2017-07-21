using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Hangjing.DBUtility;
using Hangjing.Model;

namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 分销比例
    /// </summary>
    public partial class distributeRatio
    {

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(distributeRatioInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update distributeRatio set ");
            strSql.Append(" drId = @drId , ");
            strSql.Append(" title = @title , ");
            strSql.Append(" onegraderatio = @onegraderatio , ");
            strSql.Append(" twograderatio = @twograderatio , ");
            strSql.Append(" threegraderatio = @threegraderatio , ");
            strSql.Append(" reveint1 = @reveint1 , ");
            strSql.Append(" reveint2 = @reveint2 , ");
            strSql.Append(" revevar1 = @revevar1 , ");
            strSql.Append(" revevar2 = @revevar2 , ");
            strSql.Append(" revevar3 = @revevar3 , ");
            strSql.Append(" revevar4 = @revevar4  ");
            strSql.Append(" where drId=@drId  ");

            SqlParameter[] parameters =
            {
                 new SqlParameter("@drId", SqlDbType.Int,4) ,
                 new SqlParameter("@title", SqlDbType.VarChar,256) ,
                 new SqlParameter("@onegraderatio", SqlDbType.Int,4) ,
                 new SqlParameter("@twograderatio", SqlDbType.Int,4) ,
                 new SqlParameter("@threegraderatio", SqlDbType.Int,4) ,
                 new SqlParameter("@reveint1", SqlDbType.Int,4) ,
                 new SqlParameter("@reveint2", SqlDbType.Int,4) ,
                 new SqlParameter("@revevar1", SqlDbType.VarChar,256) ,
                 new SqlParameter("@revevar2", SqlDbType.VarChar,256) ,
                 new SqlParameter("@revevar3", SqlDbType.VarChar,256) ,
                 new SqlParameter("@revevar4", SqlDbType.VarChar,256)

            };

            parameters[0].Value = model.drId;
            parameters[1].Value = model.title;
            parameters[2].Value = model.onegraderatio;
            parameters[3].Value = model.twograderatio;
            parameters[4].Value = model.threegraderatio;
            parameters[5].Value = model.reveint1;
            parameters[6].Value = model.reveint2;
            parameters[7].Value = model.revevar1;
            parameters[8].Value = model.revevar2;
            parameters[9].Value = model.revevar3;
            parameters[10].Value = model.revevar4;
            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>drId</param>
        /// <returns>distributeRatioInfo</returns>
        public distributeRatioInfo GetModel(int drId)
        {
            string sql = "select drId,title,onegraderatio,twograderatio,threegraderatio,reveint1,reveint2,revevar1,revevar2,revevar3,revevar4 from distributeRatio where  drId = @drId";
            SqlParameter parameter = new SqlParameter("@drId", SqlDbType.Int, 4);
            parameter.Value = drId;
            distributeRatioInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new distributeRatioInfo();
                    model.drId = HJConvert.ToInt32(dr["drId"]);
                    model.title = HJConvert.ToString(dr["title"]);
                    model.onegraderatio = HJConvert.ToInt32(dr["onegraderatio"]);
                    model.twograderatio = HJConvert.ToInt32(dr["twograderatio"]);
                    model.threegraderatio = HJConvert.ToInt32(dr["threegraderatio"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    model.revevar3 = HJConvert.ToString(dr["revevar3"]);
                    model.revevar4 = HJConvert.ToString(dr["revevar4"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "distributeRatio"), new SqlParameter("@strWhere", strWhere));
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<distributeRatioInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<distributeRatioInfo> infos = new List<distributeRatioInfo>();
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
            parameters[0].Value = "distributeRatio";
            parameters[1].Value = "drId,title,onegraderatio,twograderatio,threegraderatio,reveint1,reveint2,revevar1,revevar2,revevar3,revevar4";
            parameters[2].Value = "drId";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    distributeRatioInfo model = new distributeRatioInfo();
                    model.drId = HJConvert.ToInt32(dr["drId"]);
                    model.title = HJConvert.ToString(dr["title"]);
                    model.onegraderatio = HJConvert.ToInt32(dr["onegraderatio"]);
                    model.twograderatio = HJConvert.ToInt32(dr["twograderatio"]);
                    model.threegraderatio = HJConvert.ToInt32(dr["threegraderatio"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    model.revevar3 = HJConvert.ToString(dr["revevar3"]);
                    model.revevar4 = HJConvert.ToString(dr["revevar4"]);
                    infos.Add(model);
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
        public int DeldistributeRatio(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from distributeRatio where drId=@drId");
            SqlParameter[] Para =
                {
                new SqlParameter("@drId",SqlDbType.Int)
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
            str.Append("delete from distributeRatio where drId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

    }

}

