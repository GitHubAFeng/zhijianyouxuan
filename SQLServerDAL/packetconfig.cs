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
    /// 红包配置
    /// </summary>
    public partial class packetconfig
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(packetconfigInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into packetconfig(");
            strSql.Append("isopen,autotype,distance,reveint1,reveint2,revevar1,revevar2,revedate");
            strSql.Append(") values (");
            strSql.Append("@isopen,@autotype,@distance,@reveint1,@reveint2,@revevar1,@revevar2,@revedate");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters =
            {
                new SqlParameter("@isopen", SqlDbType.Int,4) ,
                new SqlParameter("@autotype", SqlDbType.Int,4) ,
                new SqlParameter("@distance", SqlDbType.Float,8) ,
                new SqlParameter("@reveint1", SqlDbType.Int,4) ,
                new SqlParameter("@reveint2", SqlDbType.Int,4) ,
                new SqlParameter("@revevar1", SqlDbType.VarChar,256) ,
                new SqlParameter("@revevar2", SqlDbType.VarChar,4000) ,
                new SqlParameter("@revedate", SqlDbType.DateTime)

            };

            parameters[0].Value = model.isopen;
            parameters[1].Value = model.autotype;
            parameters[2].Value = model.distance;
            parameters[3].Value = model.reveint1;
            parameters[4].Value = model.reveint2;
            parameters[5].Value = model.revevar1;
            parameters[6].Value = model.revevar2;
            parameters[7].Value = model.revedate;
            return HJConvert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(packetconfigInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update packetconfig set ");
            strSql.Append(" isopen = @isopen , ");
            strSql.Append(" autotype = @autotype , ");
            strSql.Append(" distance = @distance , ");
            strSql.Append(" reveint1 = @reveint1 , ");
            strSql.Append(" reveint2 = @reveint2 , ");
            strSql.Append(" revevar1 = @revevar1 , ");
            strSql.Append(" revevar2 = @revevar2 , ");
            strSql.Append(" revedate = @revedate  ");
            strSql.Append(" where dataid=@dataid ");

            SqlParameter[] parameters =
            {
                new SqlParameter("@dataid", SqlDbType.Int,4) ,
                new SqlParameter("@isopen", SqlDbType.Int,4) ,
                new SqlParameter("@autotype", SqlDbType.Int,4) ,
                new SqlParameter("@distance", SqlDbType.Float,8) ,
                new SqlParameter("@reveint1", SqlDbType.Int,4) ,
                new SqlParameter("@reveint2", SqlDbType.Int,4) ,
                new SqlParameter("@revevar1", SqlDbType.VarChar,256) ,
                new SqlParameter("@revevar2", SqlDbType.VarChar,4000) ,
                new SqlParameter("@revedate", SqlDbType.DateTime)

            };

            parameters[0].Value = model.dataid;
            parameters[1].Value = model.isopen;
            parameters[2].Value = model.autotype;
            parameters[3].Value = model.distance;
            parameters[4].Value = model.reveint1;
            parameters[5].Value = model.reveint2;
            parameters[6].Value = model.revevar1;
            parameters[7].Value = model.revevar2;
            parameters[8].Value = model.revedate;
            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>dataid</param>
        /// <returns>packetconfigInfo</returns>
        public packetconfigInfo GetModel(int dataid)
        {
            string sql = "select dataid,isopen,autotype,distance,reveint1,reveint2,revevar1,revevar2,revedate from packetconfig where  dataid = @dataid";
            SqlParameter parameter = new SqlParameter("@dataid", SqlDbType.Int, 4);
            parameter.Value = dataid;
            packetconfigInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new packetconfigInfo();
                    model.dataid = HJConvert.ToInt32(dr["dataid"]);
                    model.isopen = HJConvert.ToInt32(dr["isopen"]);
                    model.autotype = HJConvert.ToInt32(dr["autotype"]);
                    model.distance = HJConvert.ToDecimal(dr["distance"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    model.revedate = HJConvert.ToDateTime(dr["revedate"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>dataid</param>
        /// <returns>packetconfigInfo</returns>
        public packetconfigInfo GetModelByCity(int cityid)
        {
            string sql = "select dataid,isopen,autotype,distance,reveint1,reveint2,revevar1,revevar2,revedate from packetconfig where  reveint2 = @cityid";
            SqlParameter parameter = new SqlParameter("@cityid", SqlDbType.Int, 4);
            parameter.Value = cityid;
            packetconfigInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new packetconfigInfo();
                    model.dataid = HJConvert.ToInt32(dr["dataid"]);
                    model.isopen = HJConvert.ToInt32(dr["isopen"]);
                    model.autotype = HJConvert.ToInt32(dr["autotype"]);
                    model.distance = HJConvert.ToDecimal(dr["distance"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    model.revedate = HJConvert.ToDateTime(dr["revedate"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "packetconfig"), new SqlParameter("@strWhere", strWhere));
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
        public IList<packetconfigInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<packetconfigInfo> infos = new List<packetconfigInfo>();
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
            parameters[0].Value = "packetconfig";
            parameters[1].Value = "dataid,isopen,autotype,distance,reveint1,reveint2,revevar1,revevar2,revedate";
            parameters[2].Value = "dataid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    packetconfigInfo info = new packetconfigInfo();
                    info.dataid = HJConvert.ToInt32(dr["dataid"]);
                    info.isopen = HJConvert.ToInt32(dr["isopen"]);
                    info.autotype = HJConvert.ToInt32(dr["autotype"]);
                    info.distance = HJConvert.ToDecimal(dr["distance"]);
                    info.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    info.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    info.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    info.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    info.revedate = HJConvert.ToDateTime(dr["revedate"]);
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
        public int Delautodispatchconfig(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from packetconfig where dataid=@dataid");
            SqlParameter[] Para =
            {
                new SqlParameter("@dataid",SqlDbType.Int)
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
            str.Append("delete from packetconfig where dataid in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}

