using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Hangjing.DBUtility;//请先添加引用
using Hangjing.Model;
namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 会员等级
    /// </summary>
    public class User_Grade_R
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.User_Grade_RInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into User_Grade_R(");
            strSql.Append("gid,sendmoneyDiscount,foodmoneyDiscount,pointrat,sendprior,ReveInt,ReveVar,ReveFlat)");
            strSql.Append(" values (");
            strSql.Append("@gid,@sendmoneyDiscount,@foodmoneyDiscount,@pointrat,@sendprior,@ReveInt,@ReveVar,@ReveFlat)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@gid", SqlDbType.Int,4),
				new SqlParameter("@sendmoneyDiscount", SqlDbType.Decimal,5),
				new SqlParameter("@foodmoneyDiscount", SqlDbType.Decimal,5),
				new SqlParameter("@pointrat", SqlDbType.Decimal,5),
				new SqlParameter("@sendprior", SqlDbType.Int,4),
				new SqlParameter("@ReveInt", SqlDbType.Int,4),
				new SqlParameter("@ReveVar", SqlDbType.VarChar,256),
				new SqlParameter("@ReveFlat", SqlDbType.Decimal,5)
            };
            parameters[0].Value = model.gid;
            parameters[1].Value = model.sendmoneyDiscount;
            parameters[2].Value = model.foodmoneyDiscount;
            parameters[3].Value = model.pointrat;
            parameters[4].Value = model.sendprior;
            parameters[5].Value = model.ReveInt;
            parameters[6].Value = model.ReveVar;
            parameters[7].Value = model.ReveFlat;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.User_Grade_RInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update User_Grade_R set ");
            strSql.Append("gid=@gid,");
            strSql.Append("sendmoneyDiscount=@sendmoneyDiscount,");
            strSql.Append("foodmoneyDiscount=@foodmoneyDiscount,");
            strSql.Append("pointrat=@pointrat,");
            strSql.Append("sendprior=@sendprior,");
            strSql.Append("ReveInt=@ReveInt,");
            strSql.Append("ReveVar=@ReveVar,");
            strSql.Append("ReveFlat=@ReveFlat");
            strSql.Append(" where pid=@pid");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@gid", SqlDbType.Int,4),
				new SqlParameter("@sendmoneyDiscount", SqlDbType.Decimal,5),
				new SqlParameter("@foodmoneyDiscount", SqlDbType.Decimal,5),
				new SqlParameter("@pointrat", SqlDbType.Decimal,5),
				new SqlParameter("@sendprior", SqlDbType.Int,4),
				new SqlParameter("@ReveInt", SqlDbType.Int,4),
				new SqlParameter("@ReveVar", SqlDbType.VarChar,256),
				new SqlParameter("@ReveFlat", SqlDbType.Decimal,5),
				new SqlParameter("@pid", SqlDbType.Int,4)
            };
            parameters[0].Value = model.gid;
            parameters[1].Value = model.sendmoneyDiscount;
            parameters[2].Value = model.foodmoneyDiscount;
            parameters[3].Value = model.pointrat;
            parameters[4].Value = model.sendprior;
            parameters[5].Value = model.ReveInt;
            parameters[6].Value = model.ReveVar;
            parameters[7].Value = model.ReveFlat;
            parameters[8].Value = model.pid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>pid</param>
        /// <returns>User_Grade_RInfo</returns>
        public User_Grade_RInfo GetModel(int pid)
        {
            string sql = "select pid,gid,sendmoneyDiscount,foodmoneyDiscount,pointrat,sendprior,ReveInt,ReveVar,ReveFlat from User_Grade_R where  pid = @pid";
            SqlParameter parameter = new SqlParameter("@pid", SqlDbType.Int, 4);
            parameter.Value = pid;
            User_Grade_RInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new User_Grade_RInfo();
                    model.pid = HJConvert.ToInt32(dr["pid"]);
                    model.gid = HJConvert.ToInt32(dr["gid"]);
                    model.sendmoneyDiscount = HJConvert.ToDecimal(dr["sendmoneyDiscount"]);
                    model.foodmoneyDiscount = HJConvert.ToDecimal(dr["foodmoneyDiscount"]);
                    model.pointrat = HJConvert.ToDecimal(dr["pointrat"]);
                    model.sendprior = HJConvert.ToInt32(dr["sendprior"]);
                    model.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    model.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    model.ReveFlat = HJConvert.ToDecimal(dr["ReveFlat"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 根据等级编号获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>pid</param>
        /// <returns>User_Grade_RInfo</returns>
        public User_Grade_RInfo GetModelByGid(int gid)
        {
            string sql = "select pid,gid,sendmoneyDiscount,foodmoneyDiscount,pointrat,sendprior,ReveInt,ReveVar,ReveFlat from User_Grade_R where  gid = @gid";
            SqlParameter parameter = new SqlParameter("@gid", SqlDbType.Int, 4);
            parameter.Value = gid;
            User_Grade_RInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new User_Grade_RInfo();
                    model.pid = HJConvert.ToInt32(dr["pid"]);
                    model.gid = HJConvert.ToInt32(dr["gid"]);
                    model.sendmoneyDiscount = HJConvert.ToDecimal(dr["sendmoneyDiscount"]);
                    model.foodmoneyDiscount = HJConvert.ToDecimal(dr["foodmoneyDiscount"]);
                    model.pointrat = HJConvert.ToDecimal(dr["pointrat"]);
                    model.sendprior = HJConvert.ToInt32(dr["sendprior"]);
                    model.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    model.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    model.ReveFlat = HJConvert.ToDecimal(dr["ReveFlat"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "User_Grade_R"), new SqlParameter("@strWhere", strWhere));
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
        public IList<User_Grade_RInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<User_Grade_RInfo> infos = new List<User_Grade_RInfo>();
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
            parameters[0].Value = "User_Grade_R";
            parameters[1].Value = "pid,gid,sendmoneyDiscount,foodmoneyDiscount,pointrat,sendprior,ReveInt,ReveVar,ReveFlat";
            parameters[2].Value = "pid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    User_Grade_RInfo info = new User_Grade_RInfo();
                    info.pid = HJConvert.ToInt32(dr["pid"]);
                    info.gid = HJConvert.ToInt32(dr["gid"]);
                    info.sendmoneyDiscount = HJConvert.ToDecimal(dr["sendmoneyDiscount"]);
                    info.foodmoneyDiscount = HJConvert.ToDecimal(dr["foodmoneyDiscount"]);
                    info.pointrat = HJConvert.ToDecimal(dr["pointrat"]);
                    info.sendprior = HJConvert.ToInt32(dr["sendprior"]);
                    info.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    info.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    info.ReveFlat = HJConvert.ToDecimal(dr["ReveFlat"]);
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
        public int DelUser_Grade_R(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from User_Grade_R where pid=@pid");
            SqlParameter[] Para = 
			{
				new SqlParameter("@pid",SqlDbType.Int)
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
            str.Append("delete from User_Grade_R where pid in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}
