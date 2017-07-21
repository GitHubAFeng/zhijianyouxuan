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
	/// 数据访问类Links。
	/// </summary>
	public class Links
	{
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Hangjing.Model.LinksInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Links(");
            strSql.Append("Type,Url,Picture,Introduce,title)");
			strSql.Append(" values (");
            strSql.Append("@Type,@Url,@Picture,@Introduce , @title)");

			SqlParameter[] parameters = 
            {
				new SqlParameter("@Type", SqlDbType.Int),
				new SqlParameter("@Url", SqlDbType.VarChar,500),
				new SqlParameter("@Picture", SqlDbType.VarChar,100),
                new SqlParameter("@Introduce",SqlDbType.Int,4),
                new SqlParameter("@title" , SqlDbType.VarChar , 256)
            };
            parameters[0].Value = model.Type;
			parameters[1].Value = model.Url;
			parameters[2].Value = model.Picture;
            parameters[3].Value = model.Introduce;
            parameters[4].Value = model.title;

			return SQLHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(),parameters);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(Hangjing.Model.LinksInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Links set ");
			strSql.Append("Type=@Type,");
			strSql.Append("Url=@Url,");
			strSql.Append("Picture=@Picture,");
            strSql.Append("Introduce=@Introduce , title =@title");
			strSql.Append(" where LinkID=@LinkID ");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@LinkID", SqlDbType.Int,4),
				new SqlParameter("@Type", SqlDbType.Int),
				new SqlParameter("@Url", SqlDbType.VarChar,500),
				new SqlParameter("@Picture", SqlDbType.VarChar,100),
                new SqlParameter("@Introduce",SqlDbType.Int,4),
                new SqlParameter("@title" , SqlDbType.VarChar , 256)
            };
			parameters[0].Value = model.LinkID;
            parameters[1].Value = model.Type;
			parameters[2].Value = model.Url;
			parameters[3].Value = model.Picture;
            parameters[4].Value = model.Introduce;
            parameters[5].Value = model.title;

			return SQLHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(),parameters);
		}

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>LinkID</param>
        /// <returns>LinksInfo</returns>
        public LinksInfo GetModel(int LinkID)
        {
            string sql = "select LinkID,Type,Url,Picture,Introduce , title from Links WHERE LinkID = @LinkID ";
            SqlParameter parameter = new SqlParameter("@LinkID", SqlDbType.Int, 4);
            parameter.Value = LinkID;
            LinksInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new LinksInfo();
                    model.LinkID = HJConvert.ToInt32(dr["LinkID"]);
                    model.Type = HJConvert.ToInt32(dr["Type"]);
                    model.Url = HJConvert.ToString(dr["Url"]);
                    model.Picture = HJConvert.ToString(dr["Picture"]);
                    model.Introduce = HJConvert.ToInt32(dr["Introduce"]);
                    model.title = HJConvert.ToString(dr["title"]);
                }
            }

            return model;
        }

        /// <summary>
        /// 获得总记录数
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int GetCount(string strWhere)
        {
            SqlParameter[] Para = 
            {
                new SqlParameter("@tblName",SqlDbType.VarChar,255),
                new SqlParameter("@strWhere",SqlDbType.VarChar,1500)
            };
            Para[0].Value = "Links";
            Para[1].Value = strWhere;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", Para));
        
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<LinksInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<LinksInfo> infos = new List<LinksInfo>();
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
            parameters[0].Value = "Links";
            parameters[1].Value = "*";
            parameters[2].Value = "LinkID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    LinksInfo info = new LinksInfo();
                    info.LinkID = HJConvert.ToInt32(dr["LinkID"]);
                    info.Type = HJConvert.ToInt32(dr["Type"]);
                    info.Url = HJConvert.ToString(dr["Url"]);
                    info.Picture = HJConvert.ToString(dr["Picture"]);
                    info.Introduce = HJConvert.ToInt32(dr["Introduce"]);
                    info.title = HJConvert.ToString(dr["title"]);
                    infos.Add(info);
                }
            }

            return infos;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="Id"></param>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <returns></returns>
        public int DelLinks(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from Links where LinkID=@LinkID");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@LinkID",SqlDbType.Int)
	        };
            Para[0].Value = Id;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 批量删除记录
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int DelList(string IdList)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from Links where LinkID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        ///<summary>
        ///获得文字链接
        ///</summary>
        public List<LinksInfo> GetTextLinks()
        {
            List<LinksInfo> infos = new List<LinksInfo>();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Links WHERE Type = 1");
            using(SqlDataReader theReader = SQLHelper.ExecuteReader(CommandType.Text,sb.ToString(),null))
            {
                while(theReader.Read())
                {
                    LinksInfo info = new LinksInfo();
                    info.LinkID = HJConvert.ToInt32(theReader["LinkID"]);
                    info.Type = HJConvert.ToInt32(theReader["Type"]);
                    info.Url = HJConvert.ToString(theReader["Url"]);
                    info.Picture = HJConvert.ToString(theReader["Picture"]);
                    info.Introduce = HJConvert.ToInt32(theReader["Introduce"]);
                    info.title = HJConvert.ToString(theReader["title"]);
                    infos.Add(info);
                }
            }

            return infos;
        }

        ///<summary>
        ///获得图片链接
        ///</summary>
        public List<LinksInfo> GetImageLinks()
        {
            List<LinksInfo> infos = new List<LinksInfo>();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Links WHERE Type = 2");
            using(SqlDataReader theReader = SQLHelper.ExecuteReader(CommandType.Text,sb.ToString(),null))
            {
                while(theReader.Read())
                {
                    LinksInfo info = new LinksInfo();
                    info.LinkID = HJConvert.ToInt32(theReader["LinkID"]);
                    info.Type = HJConvert.ToInt32(theReader["Type"]);
                    info.Url = HJConvert.ToString(theReader["Url"]);
                    info.Picture = HJConvert.ToString(theReader["Picture"]);
                    info.Introduce = HJConvert.ToInt32(theReader["Introduce"]);
                    info.title = HJConvert.ToString(theReader["title"]);
                    infos.Add(info);
                }
            }

            return infos;
        }
	}
}

