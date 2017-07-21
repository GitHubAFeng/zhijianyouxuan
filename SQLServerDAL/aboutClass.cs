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
	/// ���ݷ�����aboutClass��
	/// </summary>
	public class aboutClass
	{
		/// <summary>
		/// ����һ������
		/// </summary>
		public int Add(Hangjing.Model.aboutClassInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into aboutClass(");
			strSql.Append("Name,ParentId,FullId)");
			strSql.Append(" values (");
			strSql.Append("@Name,@ParentId,@FullId)");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@Name", SqlDbType.VarChar,50),
				new SqlParameter("@ParentId", SqlDbType.Int,4),
				new SqlParameter("@FullId", SqlDbType.Int,4)
            };
			parameters[0].Value = model.Name;
			parameters[1].Value = model.ParentId;
			parameters[2].Value = model.FullId;

			return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public int Update(Hangjing.Model.aboutClassInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update aboutClass set ");
			strSql.Append("Name=@Name,");
			strSql.Append("ParentId=@ParentId,");
			strSql.Append("FullId=@FullId");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@Id", SqlDbType.Int,4),
				new SqlParameter("@Name", SqlDbType.VarChar,50),
				new SqlParameter("@ParentId", SqlDbType.Int,4),
				new SqlParameter("@FullId", SqlDbType.Int,4)
            };
			parameters[0].Value = model.Id;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.ParentId;
			parameters[3].Value = model.FullId;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}

        /// <summary>
        /// ��ȡ��¼�����ֶ�
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        /// <param>Id</param>
        /// <returns>aboutClassInfo</returns>
        public aboutClassInfo GetModel(int Id)
        {
            string sql = "select Id,Name,ParentId,FullId from aboutClass where id = @Id";
            SqlParameter parameter = new SqlParameter("@Id", SqlDbType.Int, 4);
            parameter.Value = Id;
            aboutClassInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new aboutClassInfo();
                    model.Id = HJConvert.ToInt32(dr["Id"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.ParentId = HJConvert.ToInt32(dr["ParentId"]);
                    model.FullId = HJConvert.ToInt32(dr["FullId"]);
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
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "aboutClass"), new SqlParameter("@strWhere", strWhere)));
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
        public IList<aboutClassInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<aboutClassInfo> infos = new List<aboutClassInfo>();
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
            parameters[0].Value = "aboutClass";
            parameters[1].Value = "Id,Name,ParentId,FullId";
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
                    aboutClassInfo info = new aboutClassInfo();
                    info.Id = HJConvert.ToInt32(dr["Id"]);
                    info.Name = HJConvert.ToString(dr["Name"]);
                    info.ParentId = HJConvert.ToInt32(dr["ParentId"]);
                    info.FullId = HJConvert.ToInt32(dr["FullId"]);
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
        public int DelaboutClass(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from aboutClass where Id=@Id");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@Id",SqlDbType.Int)
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
            str.Append("delete from aboutClass where Id in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// �������ȡǰtop������
        /// </summary>
        /// <param name="top">��ȡǰ��������</param>
        /// <returns>ͼ���б�</returns>
        ///�˴����ɺ����Ƽ������ڲ��������Զ�����
        public IList<aboutusInfo> GetSortList(int top, string where)
        {
            IList<aboutusInfo> infos = new List<aboutusInfo>();
            string sql = "SELECT dataid , title,OrderNum,SortId";
            sql += " FROM (SELECT dataid , OrderNum,title,SortId, ROW_NUMBER() OVER(PARTITION BY SortId order by OrderNum desc) rk from aboutus where " + where + ") t ";
            sql += "WHERE rk <= " + top + " order by SortId";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    aboutusInfo info = new aboutusInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.Title = HJConvert.ToString(dr["Title"]);
                    info.SortId = HJConvert.ToInt32(dr["SortId"]);
                    infos.Add(info);
                }
            }
            return infos;
        }
	}
}

