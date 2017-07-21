// EUserWord.css:����ʵ����.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-03-12

using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

using Hangjing.Model;
using Hangjing.DBUtility;//�����������
namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// �û�����
    /// </summary>
    public class EUserWord 
    {
        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Hangjing.Model.EUserWordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into EUserWord(");
            strSql.Append("UserID,Word,state,Time,Rremark,RTime,adminID,MyType,UserName)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@Word,@state,@Time,@Rremark,@RTime,@adminID,@MyType,@UserName)");
            SqlParameter[] parameters = 
            {
                new SqlParameter("@UserID", SqlDbType.Int , 4),
                new SqlParameter("@Word", SqlDbType.VarChar , 200),
                new SqlParameter("@state", SqlDbType.Int , 4),
                new SqlParameter("@Time", SqlDbType.DateTime),
                new SqlParameter("@Rremark", SqlDbType.VarChar , 200),
                new SqlParameter("@RTime", SqlDbType.DateTime),
                new SqlParameter("@adminID", SqlDbType.VarChar,12),
                new SqlParameter("@MyType", SqlDbType.Int,4),
                new SqlParameter("@UserName", SqlDbType.VarChar , 200),
            };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.Word;
            parameters[2].Value = model.State;
            parameters[3].Value = model.Time;
            parameters[4].Value = model.Rremark;
            parameters[5].Value = model.RTime;
            parameters[6].Value = model.adminID;
            parameters[7].Value = model.MyType;
            parameters[8].Value = model.UserName;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public int Update(Hangjing.Model.EUserWordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update EUserWord set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("Word=@Word,");
            strSql.Append("state=@state,");
            strSql.Append("Time=@Time,");
            strSql.Append("Rremark=@Rremark,");
            strSql.Append("RTime=@RTime,");
            strSql.Append("adminID=@adminID,");
            strSql.Append("MyType=@MyType");
            strSql.Append(" where DataID=@DataID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@UserID", SqlDbType.Int , 4),
				new SqlParameter("@Word", SqlDbType.VarChar , 200),
				new SqlParameter("@state", SqlDbType.Int , 4),
				new SqlParameter("@Time", SqlDbType.DateTime),
				new SqlParameter("@Rremark", SqlDbType.VarChar , 200),
				new SqlParameter("@RTime", SqlDbType.DateTime),
				new SqlParameter("@adminID", SqlDbType.VarChar,12),
                new SqlParameter("@DataID" , SqlDbType.Int ),
                new SqlParameter("@MyType", SqlDbType.Int,4)
            };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.Word;
            parameters[2].Value = model.State;
            parameters[3].Value = model.Time;
            parameters[4].Value = model.Rremark;
            parameters[5].Value = model.RTime;
            parameters[6].Value = model.adminID;
            parameters[7].Value = model.DataID;
            parameters[8].Value = model.MyType;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// ��ȡ��¼�����ֶ�
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        /// <param>DataID</param>
        /// <returns>EUserWordInfo</returns>
        public EUserWordInfo GetModel(int DataID)
        {
            string sql = "select * from EUserWord where dataid = @DataID";
            SqlParameter [] parameters = 
            {
                new SqlParameter("@DataID", SqlDbType.Int, 4)
            };   
            parameters[0].Value = DataID;
            EUserWordInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameters))
            {
                if (dr.Read())
                {
                    model = new EUserWordInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.UserID = HJConvert.ToInt32(dr["UserID"]);
                    model.Word = HJConvert.ToString(dr["Word"]);
                    model.State = HJConvert.ToInt32(dr["state"]);
                    model.Time = HJConvert.ToDateTime(dr["Time"]);
                    model.Rremark = HJConvert.ToString(dr["Rremark"]);
                    model.RTime = HJConvert.ToDateTime(dr["RTime"]);
                    model.adminID = HJConvert.ToString(dr["adminID"]);
                    model.UserName = HJConvert.ToString(dr["username"]);
                    model.MyType = HJConvert.ToInt32(dr["MyType"]);
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
            SqlParameter[] parameters = 
            {
                new SqlParameter("@tblName", "ecollection"),
                new SqlParameter("@strWhere", strWhere)
            };
            parameters[0].Value = "euserword";
            parameters[1].Value = strWhere;

            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", parameters);
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
        public IList<EUserWordInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<EUserWordInfo> infos = new List<EUserWordInfo>();
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
            parameters[0].Value = "EUserWord";
            parameters[1].Value = "DataID,UserID,Word,state,Time,Rremark,RTime,adminID, userName,MyType,(select Picture from ECustomer where dataid = EUserWord.UserID) as pic ";
            parameters[2].Value = "DataID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    EUserWordInfo info = new EUserWordInfo();
                    info.DataID = HJConvert.ToInt32(dr["DataID"]);
                    info.UserID = HJConvert.ToInt32(dr["UserID"]);
                    info.Word = HJConvert.ToString(dr["Word"]);
                    info.State = HJConvert.ToInt32(dr["state"]);
                    info.Time = HJConvert.ToDateTime(dr["Time"]);
                    info.Rremark = HJConvert.ToString(dr["Rremark"]);
                    info.RTime = HJConvert.ToDateTime(dr["RTime"]);
                    info.adminID = HJConvert.ToString(dr["adminID"]);
                    info.UserName = HJConvert.ToString(dr["username"]);
                    info.MyType = HJConvert.ToInt32(dr["MyType"]);
                    info.pic = HJConvert.ToString(dr["pic"]);
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
        public int DelEUserWord(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from EUserWord where DataID=@DataID");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@DataID",SqlDbType.Int)
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
            str.Append("delete from EUserWord where DataID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}

