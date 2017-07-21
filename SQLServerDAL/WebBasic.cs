using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

using Hangjing.DBUtility;//�����������
using Hangjing.Model;

namespace Hangjing.SQLServerDAL
{
	/// <summary>
	/// ���ݷ�����WebBasic��
	/// </summary>
	public class WebBasic
	{
		/// <summary>
        /// 
		/// ����һ������
		/// </summary>
		public int Add(Hangjing.Model.WebBasicInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into WebBasic(");
			strSql.Append("CKey,CValue,Inve1)");
			strSql.Append(" values (");
			strSql.Append("@Key,@Value,@Inve1)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@Key", SqlDbType.VarChar,50),
				new SqlParameter("@Value", SqlDbType.VarChar,4096),
				new SqlParameter("@Inve1", SqlDbType.VarChar,50)
            };
			parameters[0].Value = model.Key;
			parameters[1].Value = model.Value;
			parameters[2].Value = model.Inve1;

			return SQLHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(),parameters);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public int Update(Hangjing.Model.WebBasicInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update WebBasic set ");
            strSql.Append("[CKey]=@Key,");
            strSql.Append("CValue=@Value,");
			strSql.Append("Inve1=@Inve1");
			strSql.Append(" where DataId=@DataId ");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@DataId", SqlDbType.Int,4),
				new SqlParameter("@Key", SqlDbType.VarChar,50),
				new SqlParameter("@Value", SqlDbType.Text),
				new SqlParameter("@Inve1", SqlDbType.VarChar,512)
            };
			parameters[0].Value = model.DataId;
			parameters[1].Value = model.Key;
			parameters[2].Value = model.Value;
			parameters[3].Value = model.Inve1;

		    return	SQLHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(),parameters);
		}

        /// <summary>
        /// ��ȡ��¼�����ֶ�
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        /// <param>DataId</param>
        /// <returns>WebBasicInfo</returns>
        public WebBasicInfo GetModel(int DataId)
        {
            string sql = "select DataId,[CKey],[CValue],Inve1 from WebBasic WHERE DataId = @DataId";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            WebBasicInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new WebBasicInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.Key = HJConvert.ToString(dr["CKey"]);
                    model.Value = HJConvert.ToString(dr["CValue"]);
                    model.Inve1 = HJConvert.ToString(dr["Inve1"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "WebBasic"), new SqlParameter("@strWhere", strWhere));
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
        public List<WebBasicInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            List<WebBasicInfo> infos = new List<WebBasicInfo>();
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
            parameters[0].Value = "WebBasic";
            parameters[1].Value = "DataId,[CKey],[CValue],Inve1,stype";
            parameters[2].Value = "DataId";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    WebBasicInfo info = new WebBasicInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.Key = HJConvert.ToString(dr["CKey"]);
                    info.Value = HJConvert.ToString(dr["CValue"]);
                    info.Inve1 = HJConvert.ToString(dr["Inve1"]);
                    info.stype = HJConvert.ToInt32(dr["stype"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        public List<WebBasicInfo> GetAllData(string strWhere)
        {
            List<WebBasicInfo> infos = new List<WebBasicInfo>();
            string condition = "SELECT * FROM WebBasic";
            if (string.IsNullOrEmpty(strWhere) == false)
            {
                condition += (" WHERE " + strWhere);
            }                                      
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, condition, null))
            {
                while (dr.Read())
                {
                    WebBasicInfo info = new WebBasicInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.Key = HJConvert.ToString(dr["CKey"]);
                    info.Value = HJConvert.ToString(dr["CValue"]);
                    info.Inve1 = HJConvert.ToString(dr["Inve1"]);
                    info.stype = HJConvert.ToInt32(dr["stype"]);
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
        public int DelWebBasic(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from WebBasic where DataId=@DataId");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@DataId",SqlDbType.Int)
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
            str.Append("delete from WebBasic where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

	}
}

