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
    /// 促销类型
    /// </summary>
    public partial class promotionType
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(promotionTypeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into promotionType(");
            strSql.Append("title,state,sortnum,iswebtype,reveint1,reveint2,revevar1");
            strSql.Append(") values (");
            strSql.Append("@title,@state,@sortnum,@iswebtype,@reveint1,@reveint2,@revevar1");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters =
            {
                new SqlParameter("@title", SqlDbType.VarChar,256) ,
                new SqlParameter("@state", SqlDbType.Int,4) ,
                new SqlParameter("@sortnum", SqlDbType.Int,4) ,
                new SqlParameter("@iswebtype", SqlDbType.Int,4) ,
                new SqlParameter("@reveint1", SqlDbType.Int,4) ,
                new SqlParameter("@reveint2", SqlDbType.Int,4) ,
                new SqlParameter("@revevar1", SqlDbType.VarChar,256)

            };

            parameters[0].Value = model.title;
            parameters[1].Value = model.state;
            parameters[2].Value = model.sortnum;
            parameters[3].Value = model.iswebtype;
            parameters[4].Value = model.reveint1;
            parameters[5].Value = model.reveint2;
            parameters[6].Value = model.revevar1;
            return HJConvert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(promotionTypeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update promotionType set ");

            strSql.Append(" title = @title , ");
            strSql.Append(" state = @state , ");
            strSql.Append(" sortnum = @sortnum , ");
            strSql.Append(" iswebtype = @iswebtype , ");
            strSql.Append(" reveint1 = @reveint1 , ");
            strSql.Append(" reveint2 = @reveint2 , ");
            strSql.Append(" revevar1 = @revevar1  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", SqlDbType.Int,4) ,
                new SqlParameter("@title", SqlDbType.VarChar,256) ,
                new SqlParameter("@state", SqlDbType.Int,4) ,
                new SqlParameter("@sortnum", SqlDbType.Int,4) ,
                new SqlParameter("@iswebtype", SqlDbType.Int,4) ,
                new SqlParameter("@reveint1", SqlDbType.Int,4) ,
                new SqlParameter("@reveint2", SqlDbType.Int,4) ,
                new SqlParameter("@revevar1", SqlDbType.VarChar,256)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.title;
            parameters[2].Value = model.state;
            parameters[3].Value = model.sortnum;
            parameters[4].Value = model.iswebtype;
            parameters[5].Value = model.reveint1;
            parameters[6].Value = model.reveint2;
            parameters[7].Value = model.revevar1;
            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>Id</param>
        /// <returns>promotionTypeInfo</returns>
        public promotionTypeInfo GetModel(int Id)
        {
            string sql = "select Id,title,state,sortnum,iswebtype,reveint1,reveint2,revevar1 from promotionType where  Id = @Id";
            SqlParameter parameter = new SqlParameter("@Id", SqlDbType.Int, 4);
            parameter.Value = Id;
            promotionTypeInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new promotionTypeInfo();
                    model.Id = HJConvert.ToInt32(dr["Id"]);
                    model.title = HJConvert.ToString(dr["title"]);
                    model.state = HJConvert.ToInt32(dr["state"]);
                    model.sortnum = HJConvert.ToInt32(dr["sortnum"]);
                    model.iswebtype = HJConvert.ToInt32(dr["iswebtype"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "promotionType"), new SqlParameter("@strWhere", strWhere));
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
        public IList<promotionTypeInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<promotionTypeInfo> infos = new List<promotionTypeInfo>();
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
            parameters[0].Value = "promotionType";
            parameters[1].Value = "Id,title,state,sortnum,iswebtype,reveint1,reveint2,revevar1";
            parameters[2].Value = "Id";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    promotionTypeInfo model = new promotionTypeInfo();
                    model.Id = HJConvert.ToInt32(dr["Id"]);
                    model.title = HJConvert.ToString(dr["title"]);
                    model.state = HJConvert.ToInt32(dr["state"]);
                    model.sortnum = HJConvert.ToInt32(dr["sortnum"]);
                    model.iswebtype = HJConvert.ToInt32(dr["iswebtype"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    infos.Add(model);
                }
            }
            return infos;
        }

        /// <summary>
        /// 根据分类获取所有  type=0表示系统所有，type=1表示商家所有
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<promotionTypeInfo> GetAllByType(int type)
        {
            IList<promotionTypeInfo> infos = new List<promotionTypeInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, "SELECT * FROM promotionType WHERE iswebtype = " + type, null))
            {
                while (dr.Read())
                {
                    promotionTypeInfo model = new promotionTypeInfo();
                    model.Id = HJConvert.ToInt32(dr["Id"]);
                    model.title = HJConvert.ToString(dr["title"]);
                    model.state = HJConvert.ToInt32(dr["state"]);
                    model.sortnum = HJConvert.ToInt32(dr["sortnum"]);
                    model.iswebtype = HJConvert.ToInt32(dr["iswebtype"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
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
        public int DelpromotionType(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from promotionType where Id=@Id");
            SqlParameter[] Para =
                {
                new SqlParameter("@Id",SqlDbType.Int)
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
            str.Append("delete from promotionType where Id in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }



    }
}

