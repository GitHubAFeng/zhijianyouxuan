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
    /// ���ݷ�����sys_Roles��
    /// </summary>
    public class sys_Roles
    {
        /// <summary>
        /// ����һ������
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
        /// ����һ������
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
        /// ��ȡ��¼�����ֶ�
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
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
        /// ����ܼ�¼��
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        public int GetCount(string strWhere)
        {
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "sys_Roles"), new SqlParameter("@strWhere", strWhere));
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagesize">ҳ�ߴ�</param>
        /// <param name="pageindex">ҳ����</param>
        /// <param name="strWhere">��ѯ����</param>
        /// <param name="orderName">�����ֶ�</param>
        /// <param name="orderType">�������ͣ�1Ϊ����0Ϊ����</param>
        /// <returns>ͼ���б�</returns>
        ///�˴����ɺ����Ƽ������ڲ��������Զ�����
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
        /// ɾ��һ����¼
        /// </summary>
        /// <param name="Id"></param>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
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
        /// ����ɾ����¼
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        public int DelList(string IdList)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from sys_Roles where RoleID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// ��ȡ���н�ɫ�б�
        /// </summary>
        /// <returns>ͼ���б�</returns>
        ///�˴����ɺ����Ƽ������ڲ��������Զ�����
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

