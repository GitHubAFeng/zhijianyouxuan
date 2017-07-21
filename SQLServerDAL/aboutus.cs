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
	/// ���ݷ�����aboutus��
	/// </summary>
	public class aboutus
	{
		/// <summary>
		/// ����һ������
		/// </summary>
		public int Add(Hangjing.Model.aboutusInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into aboutus(");
			strSql.Append("SortId,Title,HelpContent,AddTime,ViewTimes,OrderNum,KeyWord,IsVisiableAtHome,IsVisiablePictureAtHome)");
			strSql.Append(" values (");
            strSql.Append("@SortId,@Title,@HelpContent,@AddTime,@ViewTimes,@OrderNum,@KeyWord,@IsVisiableAtHome,@IsVisiablePictureAtHome);");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@SortId", SqlDbType.Int,4),
				new SqlParameter("@Title", SqlDbType.VarChar,256),
				new SqlParameter("@HelpContent", SqlDbType.Text),
				new SqlParameter("@AddTime", SqlDbType.DateTime),
				new SqlParameter("@ViewTimes", SqlDbType.Int,4),
				new SqlParameter("@OrderNum", SqlDbType.Int,4),
				new SqlParameter("@KeyWord", SqlDbType.VarChar,256),
				new SqlParameter("@IsVisiableAtHome", SqlDbType.Bit,1),
				new SqlParameter("@IsVisiablePictureAtHome", SqlDbType.Bit,1)
            };
			parameters[0].Value = model.SortId;
			parameters[1].Value = model.Title;
			parameters[2].Value = model.HelpContent;
			parameters[3].Value = model.AddTime;
			parameters[4].Value = model.ViewTimes;
			parameters[5].Value = model.OrderNum;
			parameters[6].Value = model.KeyWord;
			parameters[7].Value = model.IsVisiableAtHome;
			parameters[8].Value = model.IsVisiablePictureAtHome;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public int Update(Hangjing.Model.aboutusInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update aboutus set ");
			strSql.Append("SortId=@SortId,");
			strSql.Append("Title=@Title,");
			strSql.Append("HelpContent=@HelpContent,");
			strSql.Append("AddTime=@AddTime,");
			strSql.Append("ViewTimes=@ViewTimes,");
			strSql.Append("OrderNum=@OrderNum,");
			strSql.Append("KeyWord=@KeyWord,");
			strSql.Append("IsVisiableAtHome=@IsVisiableAtHome,");
			strSql.Append("IsVisiablePictureAtHome=@IsVisiablePictureAtHome");
			strSql.Append(" where DataId=@DataId ");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@DataId", SqlDbType.Int,4),
				new SqlParameter("@SortId", SqlDbType.Int,4),
				new SqlParameter("@Title", SqlDbType.VarChar,256),
				new SqlParameter("@HelpContent", SqlDbType.Text),
				new SqlParameter("@AddTime", SqlDbType.DateTime),
				new SqlParameter("@ViewTimes", SqlDbType.Int,4),
				new SqlParameter("@OrderNum", SqlDbType.Int,4),
				new SqlParameter("@KeyWord", SqlDbType.VarChar,256),
				new SqlParameter("@IsVisiableAtHome", SqlDbType.Bit,1),
				new SqlParameter("@IsVisiablePictureAtHome", SqlDbType.Bit,1)
            };
			parameters[0].Value = model.DataId;
			parameters[1].Value = model.SortId;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.HelpContent;
			parameters[4].Value = model.AddTime;
			parameters[5].Value = model.ViewTimes;
			parameters[6].Value = model.OrderNum;
			parameters[7].Value = model.KeyWord;
			parameters[8].Value = model.IsVisiableAtHome;
			parameters[9].Value = model.IsVisiablePictureAtHome;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}

        /// <summary>
        /// ��ȡ��¼�����ֶ�
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        /// <param>DataId</param>
        /// <returns>aboutusInfo</returns>
        public aboutusInfo GetModel(int DataId)
        {
            string sql = "select DataId,SortId,Title,HelpContent,AddTime,ViewTimes,OrderNum,KeyWord,IsVisiableAtHome,IsVisiablePictureAtHome from aboutus where dataid = @DataId";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            aboutusInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new aboutusInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.SortId = HJConvert.ToInt32(dr["SortId"]);
                    model.Title = HJConvert.ToString(dr["Title"]);
                    model.HelpContent = HJConvert.ToString(dr["HelpContent"]);
                    model.AddTime = HJConvert.ToDateTime(dr["AddTime"]);
                    model.ViewTimes = HJConvert.ToInt32(dr["ViewTimes"]);
                    model.OrderNum = HJConvert.ToInt32(dr["OrderNum"]);
                    model.KeyWord = HJConvert.ToString(dr["KeyWord"]);
                    model.IsVisiableAtHome = HJConvert.ToBoolean(dr["IsVisiableAtHome"]);
                    model.IsVisiablePictureAtHome = HJConvert.ToBoolean(dr["IsVisiablePictureAtHome"]);
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
            return  Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "aboutus"), new SqlParameter("@strWhere", strWhere)));
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
        public IList<aboutusInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<aboutusInfo> infos = new List<aboutusInfo>();
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
            parameters[0].Value = "aboutus left join aboutClass on aboutus.sortid = aboutClass.id";
            parameters[1].Value = "DataId,SortId,Title,HelpContent,AddTime,ViewTimes,OrderNum,KeyWord,IsVisiableAtHome,IsVisiablePictureAtHome,name  ";// ,(select Name from aboutsclass where aboutClass.id  = aboutus.sortid) as name, (select Name from aboutsclass where aboutclass.id  = aboutus.SortId) as name ,(select Name from aboutsclass where aboutclass.id  = aboutus.SortId) as name
            parameters[2].Value = "DataId";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectprifix", parameters))
            {
                while (dr.Read())
                {
                    aboutusInfo info = new aboutusInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.SortId = HJConvert.ToInt32(dr["SortId"]);
                    info.Title = HJConvert.ToString(dr["Title"]);
                    info.HelpContent = HJConvert.ToString(dr["HelpContent"]);
                    info.AddTime = HJConvert.ToDateTime(dr["AddTime"]);
                    info.ViewTimes = HJConvert.ToInt32(dr["ViewTimes"]);
                    info.OrderNum = HJConvert.ToInt32(dr["OrderNum"]);
                   // info.KeyWord =""; //HJConvert.ToString(dr["name"]);
                    info.Name = HJConvert.ToString(dr["name"]);
                    info.IsVisiableAtHome = HJConvert.ToBoolean(dr["IsVisiableAtHome"]);
                    info.IsVisiablePictureAtHome = HJConvert.ToBoolean(dr["IsVisiablePictureAtHome"]);
                   
                    infos.Add(info);
                }
            }
            return infos;
        }

        public IList<aboutusInfo> GetListIndex(int pagesize, string strWhere, string orderName, string orderType)
        {
            IList<aboutusInfo> infos = new List<aboutusInfo>();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, "select top(" + pagesize + ") DataId,Title from aboutus where " + strWhere + "order by " + orderName + " " + orderType + "", null))
            {
                while (dr.Read())
                {
                    aboutusInfo info = new aboutusInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                   
                    info.Title = HJConvert.ToString(dr["Title"]);
                    
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
        public int Delaboutus(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from aboutus where DataId=@DataId");
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
            str.Append("delete from aboutus where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
	}
}

