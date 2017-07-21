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
    /// ���ݷ�����sys_RolePermission��
    /// </summary>
    public class sys_RolePermission
    {
        /// <summary>
        /// ����һ������
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
        /// ����һ������
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
        /// ��ȡ��¼�����ֶ�
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
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
        /// ����ܼ�¼��
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        public int GetCount(string strWhere)
        {
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "sys_RolePermission"), new SqlParameter("@strWhere", strWhere));
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
        /// ɾ��һ����¼
        /// </summary>
        /// <param name="Id"></param>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
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
        /// ����ɾ����¼
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        public int DelList(string IdList)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from sys_RolePermission where PermissionID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// ɾ��ĳ��ɫ��Ȩ��
        /// </summary>
        /// <param name="Id"></param>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
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
        /// ɾ��ĳģ�飨����pagecode����Ȩ��
        /// </summary>
        /// <param name="mId">ģ����</param>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        /// <returns></returns>
        public int DelRolePermissionByPageCode(int mid)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from sys_RolePermission where P_PageCode in (select M_PageCode from sys_Module where ModuleID = "+mid+")");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), null);
        }

        /// <summary>
        /// ɾ��ĳģ�飨����pagecode����Ȩ��
        /// </summary>
        /// <param name="pagecode">pagecode</param>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        /// <returns></returns>
        public int DelRolePermissionByPageCode(string pagecode)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from sys_RolePermission where P_PageCode = '"+pagecode+"'");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), null);
        }
    }
}

