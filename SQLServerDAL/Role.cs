//---------------------------------------------------------------------
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :
// Created by tuhui at 2010-8-7 12:04:03.
// E-Mail: tuhui@ihangjing.com
//----------------------------------------------------------------------
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
    public class Role
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(RoleInfo model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("insert into Role(");
            strSql.Append("RoleName,Basic,Column,User,Template,System,Describe,Authorize,AddDate)");
            strSql.Append(" values (");
            strSql.Append("@RoleName,@Basic,@Column,@User,@Template,@System,@Describe,@Authorize,@AddDate)");

            SqlParameter[] parameters = 
            {
				new SqlParameter("@RoleName", SqlDbType.VarChar,20),
				new SqlParameter("@Basic", SqlDbType.VarChar,100),
				new SqlParameter("@Column", SqlDbType.VarChar,100),
				new SqlParameter("@User", SqlDbType.VarChar,100),
				new SqlParameter("@Template", SqlDbType.VarChar,100),
				new SqlParameter("@System", SqlDbType.VarChar,100),
				new SqlParameter("@Describe", SqlDbType.VarChar,1000),
				new SqlParameter("@Authorize", SqlDbType.Bit,1),
				new SqlParameter("@AddDate", SqlDbType.DateTime)
            };
            parameters[0].Value = model.RoleName;
            parameters[1].Value = model.Basic;
            parameters[2].Value = model.Column;
            parameters[3].Value = model.User;
            parameters[4].Value = model.Template;
            parameters[5].Value = model.System;
            parameters[6].Value = model.Describe;
            parameters[7].Value = model.Authorize;
            parameters[8].Value = model.AddDate;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(RoleInfo model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update Role set ");
            strSql.Append("RoleName=@RoleName,");
            strSql.Append("Basic=@Basic,");
            strSql.Append("Column=@Column,");
            strSql.Append("User=@User,");
            strSql.Append("Template=@Template,");
            strSql.Append("System=@System,");
            strSql.Append("Describe=@Describe,");
            strSql.Append("Authorize=@Authorize");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = 
            {
				new SqlParameter("@ID", SqlDbType.Int,4),
				new SqlParameter("@RoleName", SqlDbType.VarChar,20),
				new SqlParameter("@Basic", SqlDbType.VarChar,100),
				new SqlParameter("@Column", SqlDbType.VarChar,100),
				new SqlParameter("@User", SqlDbType.VarChar,100),
				new SqlParameter("@Template", SqlDbType.VarChar,100),
				new SqlParameter("@System", SqlDbType.VarChar,100),
				new SqlParameter("@Describe", SqlDbType.VarChar,1000),
				new SqlParameter("@Authorize", SqlDbType.Bit,1)
            };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.RoleName;
            parameters[2].Value = model.Basic;
            parameters[3].Value = model.Column;
            parameters[4].Value = model.User;
            parameters[5].Value = model.Template;
            parameters[6].Value = model.System;
            parameters[7].Value = model.Describe;
            parameters[8].Value = model.Authorize;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>ID</param>
        /// <returns>RoleInfo</returns>
        public RoleInfo GetModel(int ID)
        {
            string sql = "select ID,RoleName,Basic,Column,User,Template,System,Describe,Authorize,AddDate from Role where ID=@ID";

            SqlParameter parameter = new SqlParameter("@ID", SqlDbType.Int, 4);
            parameter.Value = ID;
            RoleInfo model = null;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new RoleInfo();
                    model.ID = HJConvert.ToInt32(dr["ID"]);
                    model.RoleName = HJConvert.ToString(dr["RoleName"]);
                    model.Basic = HJConvert.ToString(dr["Basic"]);
                    model.Column = HJConvert.ToString(dr["Column"]);
                    model.User = HJConvert.ToString(dr["User"]);
                    model.Template = HJConvert.ToString(dr["Template"]);
                    model.System = HJConvert.ToString(dr["System"]);
                    model.Describe = HJConvert.ToString(dr["Describe"]);
                    model.Authorize = HJConvert.ToBoolean(dr["Authorize"]);
                    model.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "Role"), new SqlParameter("@strWhere", strWhere));
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
        public IList<RoleInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<RoleInfo> infos = new List<RoleInfo>();

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
            parameters[0].Value = "Role";
            parameters[1].Value = "ID,RoleName,Basic,Column,User,Template,System,Describe,Authorize,AddDate";
            parameters[2].Value = "ID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    RoleInfo info = new RoleInfo();
                    info.ID = HJConvert.ToInt32(dr["ID"]);
                    info.RoleName = HJConvert.ToString(dr["RoleName"]);
                    info.Basic = HJConvert.ToString(dr["Basic"]);
                    info.Column = HJConvert.ToString(dr["Column"]);
                    info.User = HJConvert.ToString(dr["User"]);
                    info.Template = HJConvert.ToString(dr["Template"]);
                    info.System = HJConvert.ToString(dr["System"]);
                    info.Describe = HJConvert.ToString(dr["Describe"]);
                    info.Authorize = HJConvert.ToBoolean(dr["Authorize"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
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
        public int DelRole(int Id)
        {
            StringBuilder str = new StringBuilder();

            str.Append("delete from Role where ID=ID");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@ID",SqlDbType.Int,11)
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

            str.Append("delete from Role where ID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        ///// <summary>
        ///// 修改禁用设置
        ///// </summary>
        ///// <param name="eui"></param>
        ///// <returns></returns>
        //public int UpdateVerify(RoleInfo eui)
        //{
        //    int IsUse = 0;
        //    if (eui.Authorize)
        //    {
        //        IsUse = 1;
        //    }
        //    else
        //    {
        //        IsUse = 0;
        //    }

        //    SqlParameter[] param = new SqlParameter[] 
        //    { 
        //        new SqlParameter("@strTableName", "Role"), 
        //        new SqlParameter("@strData", "Authorize="+IsUse+""), 
        //        new SqlParameter("@strWhere", "ID="+eui.ID)
        //    };
        //    return SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "SP_P_UpdateByWhere", param);
        //}
    }
}
