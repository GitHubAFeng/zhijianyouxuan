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
	/// ��ζ
	/// </summary>
	public class Practice
	{
		/// <summary>
		/// ����һ������
		/// </summary>
		public int Add(Hangjing.Model.PracticeInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Practice(");
			strSql.Append("pnum,pname,namepy,Inve1,Inve2,cityid)");
			strSql.Append(" values (");
			strSql.Append("@pnum,@pname,@namepy,@Inve1,@Inve2,@cityid)");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@pnum", SqlDbType.VarChar,50),
				new SqlParameter("@pname", SqlDbType.VarChar,50),
				new SqlParameter("@namepy", SqlDbType.VarChar,50),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,256),
				new SqlParameter("@cityid", SqlDbType.Int,4)
            };
			parameters[0].Value = model.pnum;
			parameters[1].Value = model.pname;
			parameters[2].Value = model.namepy;
			parameters[3].Value = model.Inve1;
			parameters[4].Value = model.Inve2;
			parameters[5].Value = model.cityid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public int Update(Hangjing.Model.PracticeInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Practice set ");
			strSql.Append("pnum=@pnum,");
			strSql.Append("pname=@pname,");
			strSql.Append("namepy=@namepy,");
			strSql.Append("Inve1=@Inve1,");
			strSql.Append("Inve2=@Inve2,");
			strSql.Append("cityid=@cityid");
			strSql.Append(" where pId=@pId ");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@pId", SqlDbType.Int,4),
				new SqlParameter("@pnum", SqlDbType.VarChar,50),
				new SqlParameter("@pname", SqlDbType.VarChar,50),
				new SqlParameter("@namepy", SqlDbType.VarChar,50),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,256),
				new SqlParameter("@cityid", SqlDbType.Int,4)
            };
			parameters[0].Value = model.pId;
			parameters[1].Value = model.pnum;
			parameters[2].Value = model.pname;
			parameters[3].Value = model.namepy;
			parameters[4].Value = model.Inve1;
			parameters[5].Value = model.Inve2;
			parameters[6].Value = model.cityid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}

        /// <summary>
        /// ��ȡ��¼�����ֶ�
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        /// <param>pId</param>
        /// <returns>PracticeInfo</returns>
        public PracticeInfo GetModel(int pId)
        {
            string sql = "select pId,pnum,pname,namepy,Inve1,Inve2,cityid from Practice where  pId = @pId";
            SqlParameter parameter = new SqlParameter("@pId", SqlDbType.Int, 4);
            parameter.Value = pId;
            PracticeInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new PracticeInfo();
                    model.pId = HJConvert.ToInt32(dr["pId"]);
                    model.pnum = HJConvert.ToString(dr["pnum"]);
                    model.pname = HJConvert.ToString(dr["pname"]);
                    model.namepy = HJConvert.ToString(dr["namepy"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.cityid = HJConvert.ToInt32(dr["cityid"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "Practice"), new SqlParameter("@strWhere", strWhere));
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
        public IList<PracticeInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<PracticeInfo> infos = new List<PracticeInfo>();
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
            parameters[0].Value = "Practice";
            parameters[1].Value = "pId,pnum,pname,namepy,Inve1,Inve2,cityid";
            parameters[2].Value = "pId";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    PracticeInfo info = new PracticeInfo();
                    info.pId = HJConvert.ToInt32(dr["pId"]);
                    info.pnum = HJConvert.ToString(dr["pnum"]);
                    info.pname = HJConvert.ToString(dr["pname"]);
                    info.namepy = HJConvert.ToString(dr["namepy"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.cityid = HJConvert.ToInt32(dr["cityid"]);
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
        public int DelPractice(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from Practice where pId=@pId");
            SqlParameter[] Para = 
			{
				new SqlParameter("@pId",SqlDbType.Int)
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
            str.Append("delete from Practice where pId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
	}
}

