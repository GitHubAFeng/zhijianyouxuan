using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;


namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// ���ݷ�����ShopData��
    /// </summary>
    public class ShopData 
    {
        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ShopDataInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ShopData(");
            strSql.Append("classname,Depth,Status,Priority,Parentid,isDel)");
            strSql.Append(" values (");
            strSql.Append("@classname,@Depth,@Status,@Priority,@Parentid,@isDel);");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@classname", SqlDbType.VarChar,256),
				new SqlParameter("@Depth", SqlDbType.Int,4),
				new SqlParameter("@Status", SqlDbType.Int,4),
				new SqlParameter("@Priority", SqlDbType.Int,4),
				new SqlParameter("@Parentid", SqlDbType.Int,4),
				new SqlParameter("@isDel", SqlDbType.Int,4)
            };
            parameters[0].Value = model.classname;
            parameters[1].Value = model.Depth;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.Priority;
            parameters[4].Value = model.Parentid;
            parameters[5].Value = model.isDel;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public int Update(ShopDataInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ShopData set ");
            strSql.Append("classname=@classname,");
            strSql.Append("Depth=@Depth,");
            strSql.Append("Status=@Status,");
            strSql.Append("Priority=@Priority,");
            strSql.Append("Parentid=@Parentid,");
            strSql.Append("isDel=@isDel");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@ID", SqlDbType.Int,4),
				new SqlParameter("@classname", SqlDbType.VarChar,256),
				new SqlParameter("@Depth", SqlDbType.Int,4),
				new SqlParameter("@Status", SqlDbType.Int,4),
				new SqlParameter("@Priority", SqlDbType.Int,4),
				new SqlParameter("@Parentid", SqlDbType.Int,4),
				new SqlParameter("@isDel", SqlDbType.Int,4)
            };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.classname;
            parameters[2].Value = model.Depth;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.Priority;
            parameters[5].Value = model.Parentid;
            parameters[6].Value = model.isDel;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// ��ȡ��¼�����ֶ�
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        /// <param>ID</param>
        /// <returns>ShopDataInfo</returns>
        public ShopDataInfo GetModel(int ID)
        {
            string sql = "select ID,classname,Depth,Status,Priority,Parentid,isDel from ShopData where  ID = @ID";
            SqlParameter parameter = new SqlParameter("@ID", SqlDbType.Int, 4);
            parameter.Value = ID;
            ShopDataInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new ShopDataInfo();
                    model.ID = HJConvert.ToInt32(dr["ID"]);
                    model.classname = HJConvert.ToString(dr["classname"]);
                    model.Depth = HJConvert.ToInt32(dr["Depth"]);
                    model.Status = HJConvert.ToInt32(dr["Status"]);
                    model.Priority = HJConvert.ToInt32(dr["Priority"]);
                    model.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    model.isDel = HJConvert.ToInt32(dr["isDel"]);
                }
            }
            return model;
        }


        /// <summary>
        /// ��ȡ��¼�����ֶ�
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        /// <param>ID</param>
        /// <returns>ShopDataInfo</returns>
        public  string ShopDataclassname(string  strwhere)
        {
            string sql = "select classname from ShopData where  " + strwhere + "";
            return Convert.ToString(SQLHelper.ExecuteScalar(CommandType.Text, sql, null));
         
        }


        /// <summary>
        /// ����ܼ�¼��
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        public int GetCount(string strWhere)
        {
            SqlParameter[] Para = 
            {
                new SqlParameter("@tblName",SqlDbType.VarChar,255),
                new SqlParameter("@strWhere",SqlDbType.VarChar,1500)
            };
            Para[0].Value = "ShopData";
            Para[1].Value = strWhere;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", Para));
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
        public IList<ShopDataInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ShopDataInfo> infos = new List<ShopDataInfo>();
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
            parameters[0].Value = "ShopData";
            parameters[1].Value = "*";
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
                    ShopDataInfo info = new ShopDataInfo();
                    info.ID = HJConvert.ToInt32(dr["ID"]);
                    info.classname = HJConvert.ToString(dr["classname"]);
                    info.Depth = HJConvert.ToInt32(dr["Depth"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.Priority = HJConvert.ToInt32(dr["Priority"]);
                    info.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    info.isDel = HJConvert.ToInt32(dr["isDel"]);

                    info.Pic = HJConvert.ToString(dr["Pic"]);
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
        public int DelShopData(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ShopData where ID=@ID");
            SqlParameter[] Para = 
			{
				new SqlParameter("@ID",SqlDbType.Int)
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
            str.Append("delete from ShopData where ID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }


        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="pid">������</param>
        /// <returns>ͼ���б�</returns>
        ///�˴����ɺ����Ƽ������ڲ��������Զ�����
        public IList<ShopDataInfo> GetsubList(int pid)
        {
            IList<ShopDataInfo> infos = new List<ShopDataInfo>();
            SqlParameter[] parameters = 
			{
				new SqlParameter("@pid", SqlDbType.Int)
			};
            parameters[0].Value = pid;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "ShopData_getSublist", parameters))
            {
                while (dr.Read())
                {
                    ShopDataInfo info = new ShopDataInfo();
                    info.ID = HJConvert.ToInt32(dr["ID"]);
                    info.classname = HJConvert.ToString(dr["classname"]);
                    info.Depth = HJConvert.ToInt32(dr["Depth"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.Priority = HJConvert.ToInt32(dr["Priority"]);
                    info.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    info.isDel = HJConvert.ToInt32(dr["isDel"]);
                    info.Pic = HJConvert.ToString(dr["pic"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// �������ƣ�����
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        public int updateName(int id , string classname , int pri)
        {
           StringBuilder strSql = new StringBuilder();
            strSql.Append("update ShopData set ");
            strSql.Append("classname=@classname,");
            strSql.Append("Priority=@Priority");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@ID", SqlDbType.Int,4),
				new SqlParameter("@classname", SqlDbType.VarChar,256),
				new SqlParameter("@Priority", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            parameters[1].Value = classname;
            parameters[2].Value = pri;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// �������ƣ���','�ֿ�
        /// </summary>
        /// <param name="pagesize">ҳ�ߴ�</param>
        /// <param name="pageindex">ҳ����</param>
        /// <param name="strWhere">��ѯ����</param>
        /// <param name="orderName">�����ֶ�</param>
        /// <param name="orderType">�������ͣ�1Ϊ����0Ϊ����</param>
        /// <returns>ͼ���б�</returns>
        ///�˴����ɺ����Ƽ������ڲ��������Զ�����
        public string GetListNames(string idlist)
        {
            string rs = "";
            string sql = "select classname from shopdata where id in ("+idlist+")";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    rs += HJConvert.ToString(dr["classname"])+",";    
                }
            }
            rs = System.Text.RegularExpressions.Regex.Replace(rs, @",$", " ");
            return rs;
        }
    }
}

