
// EAdmin.css:管理员类.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-03-05
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;
using System.Data.SqlClient;

namespace Hangjing.SQLServerDAL
{
	/// <summary>
	/// 数据访问类EAdmin。
	/// </summary>
	public class EAdmin
	{
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Hangjing.Model.EAdminInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into EAdmin(");
            strSql.Append("AdminName,AdminPassword,AdminStatus,LastAccess ,Permission,Rem ,realname,CityID,root)");
			strSql.Append(" values (");
            strSql.Append("@AdminName,@AdminPassword,@AdminStatus,@LastAccess ,@Permission ,@Rem,@realname,@CityID,@root)");
			SqlParameter[] parameters = 
            {
			    new SqlParameter("@Permission", SqlDbType.VarChar , 4),
			    new SqlParameter("@AdminName", SqlDbType.VarChar,12),
			    new SqlParameter("@AdminPassword", SqlDbType.VarChar,50),
			    new SqlParameter("@AdminStatus", SqlDbType.Int,4),
			    new SqlParameter("@LastAccess", SqlDbType.DateTime),
                new SqlParameter("@Rem" , SqlDbType.VarChar , 1000),
                new SqlParameter("@realname" , SqlDbType.VarChar , 12),
                new SqlParameter("@CityID", SqlDbType.Int,4),
                new SqlParameter("@root", SqlDbType.Int,4)
            };
            parameters[0].Value = model.Permission;
			parameters[1].Value = model.AdminName;
			parameters[2].Value = model.AdminPassword;
			parameters[3].Value = model.AdminStatus;
			parameters[4].Value = model.LastAccess;
            parameters[5].Value = model.Rem;
            parameters[6].Value = model.RealName;
            parameters[7].Value = model.CityID;
            parameters[8].Value = model.root;

            return  SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(Hangjing.Model.EAdminInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update EAdmin set ");
			strSql.Append("AdminName=@AdminName,");
            strSql.Append("RealName=@RealName,");
			strSql.Append("AdminPassword=@AdminPassword,");
			strSql.Append("AdminStatus=@AdminStatus,");
            strSql.Append("LastAccess=@LastAccess,Permission=@Permission,");
            strSql.Append("Rem=@Rem,");
            strSql.Append("CityID=@CityID,");
            strSql.Append("root=@root");
            strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@ID", SqlDbType.Int,4),
				new SqlParameter("@AdminName", SqlDbType.VarChar,12),
                new SqlParameter("@RealName", SqlDbType.VarChar,20),
				new SqlParameter("@AdminPassword", SqlDbType.VarChar,50),
				new SqlParameter("@AdminStatus", SqlDbType.Int,4),
				new SqlParameter("@LastAccess", SqlDbType.DateTime),
                new SqlParameter("@Permission", SqlDbType.VarChar , 4),
                new SqlParameter("@Rem", SqlDbType.VarChar,1000),
                new SqlParameter("@CityID", SqlDbType.Int,4),
                new SqlParameter("@root", SqlDbType.Int,4)
            };
			parameters[0].Value = model.ID;
			parameters[1].Value = model.AdminName;
            parameters[2].Value = model.RealName;
			parameters[3].Value = model.AdminPassword;
			parameters[4].Value = model.AdminStatus;
			parameters[5].Value = model.LastAccess;
            parameters[6].Value = model.Permission;
            parameters[7].Value = model.Rem;
            parameters[8].Value = model.CityID;
            parameters[9].Value = model.root;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}

        /// <summary>
        /// 分页获得新闻列表
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页码</param>
        /// <param name="where">查询条件</param>
        /// <returns>IList<NewsInfo></returns>
        public IList<Hangjing.Model.EAdminInfo> GetList(int pagesize, int pageindex, string where )
        {
            IList<Hangjing.Model.EAdminInfo> infos = new List<Hangjing.Model.EAdminInfo>();

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
            parameters[0].Value = "eadmin";
            parameters[1].Value = " ID,adminName,adminPassword,RealName,Permission ,rem ,LastAccess,CityID,root ";
            parameters[2].Value = "ID";
            parameters[3].Value = "ID";
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = 1;
            parameters[7].Value = where;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    Hangjing.Model.EAdminInfo model = new Hangjing.Model.EAdminInfo();

                    model.ID = HJConvert.ToInt32(dr["ID"]);
                    model.AdminName = HJConvert.ToString(dr["adminName"]);
                    model.AdminPassword = HJConvert.ToString(dr["adminPassword"]);
                    model.RealName = HJConvert.ToString(dr["RealName"]);
                    model.Permission = HJConvert.ToString(dr["Permission"]);
                    model.Rem = HJConvert.ToString(dr["Rem"]);
                    model.LastAccess = HJConvert.ToDateTime(dr["LastAccess"]);
                    model.CityID = HJConvert.ToInt32(dr["CityID"]);
                    model.root = HJConvert.ToInt32(dr["root"]);
                    infos.Add(model);
                }
            }
            return infos;
        }

        /// <summary>
        /// 取得eadmin的实例
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Hangjing.Model.EAdminInfo GetModel(string name, string password)
        {
            SqlParameter [] parameters = 
            {
                new SqlParameter("@adminname" , SqlDbType.VarChar , 12),
                new SqlParameter("@adminpassword" , SqlDbType.VarChar ,50)
            };
            parameters[0].Value = name;
            parameters[1].Value = password;

            Hangjing.Model.EAdminInfo model = null;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "Eadmin_Getmodel", parameters))
            {
                if (dr.Read())
                {
                    model = new Hangjing.Model.EAdminInfo();
                    model.ID = HJConvert.ToInt32(dr["ID"]);
                    model.AdminName = HJConvert.ToString(dr["Adminname"]);
                    model.AdminPassword = HJConvert.ToString(dr["adminPassword"]);
                    model.RealName= HJConvert.ToString(dr["RealName"]);
                    model.Permission = HJConvert.ToString(dr["Permission"]);
                    model.Rem = HJConvert.ToString(dr["rem"]);
                    model.CityID = HJConvert.ToInt32(dr["CityID"]);
                    
                }
            }

            return model;     
        }

        /// <summary>
        /// 取得eadmin的实例
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Hangjing.Model.EAdminInfo GetModelByRoot(string name, string password)
        {
            SqlParameter[] parameters = 
            {
                new SqlParameter("@adminname" , SqlDbType.VarChar , 12),
                new SqlParameter("@adminpassword" , SqlDbType.VarChar ,50)
            };
            parameters[0].Value = name;
            parameters[1].Value = password;

            Hangjing.Model.EAdminInfo model = null;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "Eadmin_GetmodelByRoot", parameters))
            {
                if (dr.Read())
                {
                    model = new Hangjing.Model.EAdminInfo();
                    model.ID = HJConvert.ToInt32(dr["ID"]);
                    model.AdminName = HJConvert.ToString(dr["Adminname"]);
                    model.AdminPassword = HJConvert.ToString(dr["adminPassword"]);
                    model.RealName = HJConvert.ToString(dr["RealName"]);
                    model.Permission = HJConvert.ToString(dr["Permission"]);
                    model.Rem = HJConvert.ToString(dr["rem"]);
                    model.CityID = HJConvert.ToInt32(dr["CityID"]);
                    
                }
            }

            return model;
        }

        /// <summary>
        /// 获得总记录数
        /// </summary>
        public int GetCount(string strWhere)
        {
            SqlParameter[] parameters = 
            {
                new SqlParameter("@tblName" , SqlDbType.VarChar , 20),
                new SqlParameter("@strWhere" , SqlDbType.VarChar , 50)
            };
            parameters[0].Value = "EAdmin";
            parameters[1].Value = strWhere;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", parameters));
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int id)
        {
            string sql = "delete from eadmin  where ID=@ID ";
            SqlParameter parameter = new SqlParameter("@ID", SqlDbType.Int, 4);
            parameter.Value = id;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
        }

        /// <summary>
        /// 删除不定条数记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int Delete(string ids)
        {
            string sql = "delete from eadmin  where ID in ({0}) ";

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(sql, ids), null);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="NewPwd"></param>
        /// <returns></returns>
        public int UpdataPwd(int id, string NewPwd)
        {
            StringBuilder str = new StringBuilder();
            str.Append("Update eadmin set ");
            str.Append("adminPassword=@NewPwd ");
            str.Append("where ID=@UserID");

            SqlParameter[] Para = 
            {
                new SqlParameter("@UserID",SqlDbType.Int),
                new SqlParameter("@NewPwd",SqlDbType.VarChar,50)
            };
            Para[0].Value = id;
            Para[1].Value = NewPwd;

            int i = SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
            return i;
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>ID</param>
        /// <returns>EAdminInfo</returns>
        public EAdminInfo GetModel(int ID)
        {
            SqlParameter parameter = new SqlParameter("@dataid", SqlDbType.Int, 4);
            parameter.Value = ID;
            EAdminInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "EAdmin_GetModelbyID", parameter))
            {
                if (dr.Read())
                {
                    model = new EAdminInfo();
                    model.ID = HJConvert.ToInt32(dr["ID"]);
                    model.AdminName = HJConvert.ToString(dr["AdminName"]);
                    model.AdminPassword = HJConvert.ToString(dr["AdminPassword"]);
                    model.AdminStatus = HJConvert.ToInt32(dr["AdminStatus"]);
                    model.LastAccess = HJConvert.ToDateTime(dr["LastAccess"]);
                    model.Permission = HJConvert.ToString(dr["Permission"]);
                    model.RealName = HJConvert.ToString(dr["RealName"]);
                    model.Rem = HJConvert.ToString(dr["Rem"]);
                    model.CityID = HJConvert.ToInt32(dr["CityID"]);
                    model.root = HJConvert.ToInt32(dr["root"]);
                }
            }
            return model;
        }
	}
}

