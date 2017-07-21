using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using System.Data.SqlClient;
using Hangjing.DBUtility;//�����������
using Hangjing.Model;
namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// ���ݷ�����hurryorder��
    /// </summary>
    public class hurryorder
    {
        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Hangjing.Model.hurryorderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into hurryorder(");
            strSql.Append("oid,Name,addtime,Ccount,ReveInt,ReveVar)");
            strSql.Append(" values (");
            strSql.Append("@oid,@Name,@addtime,@Ccount,@ReveInt,@ReveVar)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@oid", SqlDbType.VarChar,50),
				new SqlParameter("@Name", SqlDbType.VarChar,11),
				new SqlParameter("@addtime", SqlDbType.VarChar,20),
				new SqlParameter("@Ccount", SqlDbType.Int,11),
				new SqlParameter("@ReveInt", SqlDbType.Int,11),
				new SqlParameter("@ReveVar", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.oid;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.addtime;
            parameters[3].Value = model.Ccount;
            parameters[4].Value = model.ReveInt;
            parameters[5].Value = model.ReveVar;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public int Update(Hangjing.Model.hurryorderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update hurryorder set ");
            strSql.Append("oid=@oid,");
            strSql.Append("Name=@Name,");
            strSql.Append("addtime=@addtime,");
            strSql.Append("Ccount=@Ccount,");
            strSql.Append("ReveInt=@ReveInt,");
            strSql.Append("ReveVar=@ReveVar");
            strSql.Append(" where CID=@CID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@CID", SqlDbType.Int,11),
				new SqlParameter("@oid", SqlDbType.VarChar,50),
				new SqlParameter("@Name", SqlDbType.VarChar,11),
				new SqlParameter("@addtime", SqlDbType.VarChar,20),
				new SqlParameter("@Ccount", SqlDbType.Int,11),
				new SqlParameter("@ReveInt", SqlDbType.Int,11),
				new SqlParameter("@ReveVar", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.CID;
            parameters[1].Value = model.oid;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.addtime;
            parameters[4].Value = model.Ccount;
            parameters[5].Value = model.ReveInt;
            parameters[6].Value = model.ReveVar;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// ����һ��string�ֶε�ֵ
        /// where�ǲ�ѯ��� ��Ҫ��where �磺where DataId=1���� where DataId in (1,2,3)
        ///</summary>
        public int UpdateValue(string param, int strValue, string Where)
        {
            SqlParameter[] parameters = 
            {
                new SqlParameter("@value", SqlDbType.Int,4),
            };
            parameters[0].Value = strValue;

            StringBuilder sql = new StringBuilder("Update hurryorder Set ");
            sql.Append(param);
            sql.Append("=@value ");
            sql.Append(Where);

            return (int)SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);

        }

        /// <summary>
        /// ����ܼ�¼��
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        public int GetCount(string strWhere)
        {
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "hurryorder"), new SqlParameter("@strWhere", strWhere));
        }
        /// <summary>
        /// ��ȡ��¼�����ֶ�
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        /// <param>DataID</param>
        /// <returns>EUserWordInfo</returns>
        public hurryorderInfo GetModel(string oid)
        {
            string sql = "select * from hurryorder where oid ='"+oid+"'";
            hurryorderInfo model = new hurryorderInfo();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (dr.Read())
                {
                    model = new hurryorderInfo();
                    model.CID = HJConvert.ToInt32(dr["CID"]);
                    model.oid = HJConvert.ToString(dr["oid"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.addtime = HJConvert.ToString(dr["addtime"]);
                    model.Ccount = HJConvert.ToInt32(dr["Ccount"]);
                    model.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    model.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                }
            }
            return model;
        }

        public IList<hurryorderInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<hurryorderInfo> infos = new List<hurryorderInfo>();
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
            parameters[0].Value = "hurryorder";
            parameters[1].Value = "*,(select Unid from Custorder where orderid=hurryorder.oid) as id";
            parameters[2].Value = "CID";
	        parameters[3].Value = orderName;
	        parameters[4].Value = pagesize;
	        parameters[5].Value = pageindex;
	        parameters[6].Value = orderType; 
	        parameters[7].Value = strWhere;

           
	        using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
	        {
		        while (dr.Read())
		        {
                    hurryorderInfo  model = new hurryorderInfo();
                    model.CID = HJConvert.ToInt32(dr["CID"]);
                    model.oid = HJConvert.ToString(dr["oid"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.addtime = HJConvert.ToString(dr["addtime"]);
                    model.Ccount = HJConvert.ToInt32(dr["Ccount"]);
                    model.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    model.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    model.id = HJConvert.ToInt32(dr["id"]);
                    infos.Add(model);
		        }
	        }
	        return infos;
        }
    }
}

