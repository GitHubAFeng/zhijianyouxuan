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
    /// 数据访问类sys_RolePermission。
    /// </summary>
    public class sys_RolePermission
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(sys_RolePermissionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_RolePermission(");
            strSql.Append("P_RoleID,P_ApplicationID,P_PageCode,P_Value)");
            strSql.Append(" values (");
            strSql.Append("@P_RoleID,@P_ApplicationID,@P_PageCode,@P_Value)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@P_RoleID", SqlDbType.Int,4),
				new SqlParameter("@P_ApplicationID", SqlDbType.Int,4),
				new SqlParameter("@P_PageCode", SqlDbType.VarChar,20),
				new SqlParameter("@P_Value", SqlDbType.Int,4)
            };
            parameters[0].Value = model.P_RoleID;
            parameters[1].Value = model.P_ApplicationID;
            parameters[2].Value = model.P_PageCode;
            parameters[3].Value = model.P_Value;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(sys_RolePermissionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_RolePermission set ");
            strSql.Append("P_Value=@P_Value");
            strSql.Append(" where P_RoleID=@P_RoleID and P_ApplicationID=@P_ApplicationID and P_PageCode=@P_PageCode and PermissionID=@PermissionID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@PermissionID", SqlDbType.Int,4),
				new SqlParameter("@P_RoleID", SqlDbType.Int,4),
				new SqlParameter("@P_ApplicationID", SqlDbType.Int,4),
				new SqlParameter("@P_PageCode", SqlDbType.VarChar,20),
				new SqlParameter("@P_Value", SqlDbType.Int,4)
            };
            parameters[0].Value = model.PermissionID;
            parameters[1].Value = model.P_RoleID;
            parameters[2].Value = model.P_ApplicationID;
            parameters[3].Value = model.P_PageCode;
            parameters[4].Value = model.P_Value;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>PermissionID</param>
        /// <returns>sys_RolePermissionInfo</returns>
        public sys_RolePermissionInfo GetModel(int PermissionID)
        {
            string sql = "select PermissionID,P_RoleID,P_ApplicationID,P_PageCode,P_Value from sys_RolePermission where  PermissionID = @PermissionID";
            SqlParameter parameter = new SqlParameter("@PermissionID", SqlDbType.Int, 4);
            parameter.Value = PermissionID;
            sys_RolePermissionInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new sys_RolePermissionInfo();
                    model.PermissionID = HJConvert.ToInt32(dr["PermissionID"]);
                    model.P_RoleID = HJConvert.ToInt32(dr["P_RoleID"]);
                    model.P_ApplicationID = HJConvert.ToInt32(dr["P_ApplicationID"]);
                    model.P_PageCode = HJConvert.ToString(dr["P_PageCode"]);
                    model.P_Value = HJConvert.ToInt32(dr["P_Value"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "sys_RolePermission"), new SqlParameter("@strWhere", strWhere));
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
        public IList<sys_RolePermissionInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<sys_RolePermissionInfo> infos = new List<sys_RolePermissionInfo>();
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
            parameters[0].Value = "sys_RolePermission";
            parameters[1].Value = "PermissionID,P_RoleID,P_ApplicationID,P_PageCode,P_Value,(select top 1 M_Directory from sys_Module where M_PageCode =sys_RolePermission.P_PageCode) as des";
            parameters[2].Value = "PermissionID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    sys_RolePermissionInfo info = new sys_RolePermissionInfo();
                    info.PermissionID = HJConvert.ToInt32(dr["PermissionID"]);
                    info.P_RoleID = HJConvert.ToInt32(dr["P_RoleID"]);
                    info.P_ApplicationID = HJConvert.ToInt32(dr["P_ApplicationID"]);
                    info.P_PageCode = HJConvert.ToString(dr["P_PageCode"]);
                    info.P_Value = HJConvert.ToInt32(dr["P_Value"]);
                    info.des = HJConvert.ToString(dr["des"]);
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
        public int Delsys_RolePermission(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from sys_RolePermission where PermissionID=@PermissionID");
            SqlParameter[] Para = 
			{
				new SqlParameter("@PermissionID",SqlDbType.Int)
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
            str.Append("delete from sys_RolePermission where PermissionID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 删除某角色的权限
        /// </summary>
        /// <param name="Id"></param>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <returns></returns>
        public int DelRolePermissionByRid(int rId)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from sys_RolePermission where P_RoleID=@PermissionID");
            SqlParameter[] Para = 
			{
				new SqlParameter("@PermissionID",SqlDbType.Int)
			};
            Para[0].Value = rId;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 删除某模块（根据pagecode）的权限
        /// </summary>
        /// <param name="mId">模块编号</param>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <returns></returns>
        public int DelRolePermissionByPageCode(int mid)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from sys_RolePermission where P_PageCode in (select M_PageCode from sys_Module where ModuleID = "+mid+")");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), null);
        }

        /// <summary>
        /// 删除某模块（根据pagecode）的权限
        /// </summary>
        /// <param name="pagecode">pagecode</param>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <returns></returns>
        public int DelRolePermissionByPageCode(string pagecode)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from sys_RolePermission where P_PageCode = '"+pagecode+"'");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), null);
        }
    }
}

