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
    /// ���ݷ�����sys_ModulePermition��
    /// </summary>
    public class sys_ModulePermition
    {
        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Hangjing.Model.sys_ModulePermitionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_ModulePermition(");
            strSql.Append("ModuleID,pername,pvalue,ReveInt,ReveVar)");
            strSql.Append(" values (");
            strSql.Append("@ModuleID,@pername,@pvalue,@ReveInt,@ReveVar)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@ModuleID", SqlDbType.Int,4),
				new SqlParameter("@pername", SqlDbType.VarChar,256),
				new SqlParameter("@pvalue", SqlDbType.Int,4),
				new SqlParameter("@ReveInt", SqlDbType.Int,4),
				new SqlParameter("@ReveVar", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.ModuleID;
            parameters[1].Value = model.pername;
            parameters[2].Value = model.pvalue;
            parameters[3].Value = model.ReveInt;
            parameters[4].Value = model.ReveVar;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public int Update(Hangjing.Model.sys_ModulePermitionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_ModulePermition set ");
            strSql.Append("ModuleID=@ModuleID,");
            strSql.Append("pername=@pername,");
            strSql.Append("pvalue=@pvalue,");
            strSql.Append("ReveInt=@ReveInt,");
            strSql.Append("ReveVar=@ReveVar");
            strSql.Append(" where mid=@mid ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@mid", SqlDbType.Int,4),
				new SqlParameter("@ModuleID", SqlDbType.Int,4),
				new SqlParameter("@pername", SqlDbType.VarChar,256),
				new SqlParameter("@pvalue", SqlDbType.Int,4),
				new SqlParameter("@ReveInt", SqlDbType.Int,4),
				new SqlParameter("@ReveVar", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.mid;
            parameters[1].Value = model.ModuleID;
            parameters[2].Value = model.pername;
            parameters[3].Value = model.pvalue;
            parameters[4].Value = model.ReveInt;
            parameters[5].Value = model.ReveVar;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// ��ȡ��¼�����ֶ�
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        /// <param>mid</param>
        /// <returns>sys_ModulePermitionInfo</returns>
        public sys_ModulePermitionInfo GetModel(int mid)
        {
            string sql = "select mid,ModuleID,pername,pvalue,ReveInt,ReveVar from sys_ModulePermition where  mid = @mid";
            SqlParameter parameter = new SqlParameter("@mid", SqlDbType.Int, 4);
            parameter.Value = mid;
            sys_ModulePermitionInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new sys_ModulePermitionInfo();
                    model.mid = HJConvert.ToInt32(dr["mid"]);
                    model.ModuleID = HJConvert.ToInt32(dr["ModuleID"]);
                    model.pername = HJConvert.ToString(dr["pername"]);
                    model.pvalue = HJConvert.ToInt32(dr["pvalue"]);
                    model.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    model.ReveVar = HJConvert.ToString(dr["ReveVar"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "sys_ModulePermition"), new SqlParameter("@strWhere", strWhere));
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
        public IList<sys_ModulePermitionInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<sys_ModulePermitionInfo> infos = new List<sys_ModulePermitionInfo>();
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
            parameters[0].Value = "sys_ModulePermition";
            parameters[1].Value = "mid,ModuleID,pername,pvalue,ReveInt,ReveVar";
            parameters[2].Value = "mid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    sys_ModulePermitionInfo info = new sys_ModulePermitionInfo();
                    info.mid = HJConvert.ToInt32(dr["mid"]);
                    info.ModuleID = HJConvert.ToInt32(dr["ModuleID"]);
                    info.pername = HJConvert.ToString(dr["pername"]);
                    info.pvalue = HJConvert.ToInt32(dr["pvalue"]);
                    info.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    info.ReveVar = HJConvert.ToString(dr["ReveVar"]);
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
        public int Delsys_ModulePermition(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from sys_ModulePermition where mid=@mid");
            SqlParameter[] Para = 
			{
				new SqlParameter("@mid",SqlDbType.Int)
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
            str.Append("delete from sys_ModulePermition where mid in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

    }
}

