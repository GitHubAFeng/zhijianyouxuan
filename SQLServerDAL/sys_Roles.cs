using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Hangjing.Model;
using Hangjing.DBUtility;
namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 数据访问类sys_Roles。
    /// </summary>
    public class sys_Roles
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(sys_RolesInfo model)
        { 
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_Roles(");
            strSql.Append("R_RoleName,R_Description)");
            strSql.Append(" values (");
            strSql.Append("@R_RoleName,@R_Description)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@R_RoleName", SqlDbType.NVarChar,50),
				new SqlParameter("@R_Description", SqlDbType.NVarChar,255)
            };
            parameters[0].Value = model.R_RoleName;
            parameters[1].Value = model.R_Description;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.sys_RolesInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Roles set ");
            strSql.Append("R_RoleName=@R_RoleName,");
            strSql.Append("R_Description=@R_Description");
            strSql.Append(" where RoleID=@RoleID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@RoleID", SqlDbType.Int,4),
				new SqlParameter("@R_RoleName", SqlDbType.NVarChar,50),
				new SqlParameter("@R_Description", SqlDbType.NVarChar,255)
            };
            parameters[0].Value = model.RoleID;
            parameters[1].Value = model.R_RoleName;
            parameters[2].Value = model.R_Description;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>RoleID</param>
        /// <returns>sys_RolesInfo</returns>
        public sys_RolesInfo GetModel(int RoleID)
        {
            string sql = "select RoleID,R_RoleName,R_Description from sys_Roles where  RoleID = @RoleID";
            SqlParameter parameter = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameter.Value = RoleID;
            sys_RolesInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new sys_RolesInfo();
                    model.RoleID = HJConvert.ToInt32(dr["RoleID"]);
                    model.R_RoleName = HJConvert.ToString(dr["R_RoleName"]);
                    model.R_Description = HJConvert.ToString(dr["R_Description"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "sys_Roles"), new SqlParameter("@strWhere", strWhere));
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
        public IList<sys_RolesInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<sys_RolesInfo> infos = new List<sys_RolesInfo>();
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
            parameters[0].Value = "sys_Roles";
            parameters[1].Value = "RoleID,R_RoleName,R_Description";
            parameters[2].Value = "RoleID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    sys_RolesInfo info = new sys_RolesInfo();
                    info.RoleID = HJConvert.ToInt32(dr["RoleID"]);
                    info.R_RoleName = HJConvert.ToString(dr["R_RoleName"]);
                    info.R_Description = HJConvert.ToString(dr["R_Description"]);
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
        public int Delsys_Roles(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from sys_Roles where RoleID=@RoleID");
            SqlParameter[] Para = 
			{
				new SqlParameter("@RoleID",SqlDbType.Int)
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
            str.Append("delete from sys_Roles where RoleID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<sys_RolesInfo> getall(string where)
        {
            IList<sys_RolesInfo> infos = new List<sys_RolesInfo>();
            string sql = "select RoleID,R_RoleName,R_Description from sys_Roles where  " + where;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text,sql, null))
            {
                while (dr.Read())
                {
                    sys_RolesInfo info = new sys_RolesInfo();
                    info.RoleID = HJConvert.ToInt32(dr["RoleID"]);
                    info.R_RoleName = HJConvert.ToString(dr["R_RoleName"]);
                    info.R_Description = HJConvert.ToString(dr["R_Description"]);
                    infos.Add(info);
                }
            }
            return infos;
        }
    }
}

