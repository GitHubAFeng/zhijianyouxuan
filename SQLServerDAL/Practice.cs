using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

using Hangjing.DBUtility;//请先添加引用
using Hangjing.Model;
namespace Hangjing.SQLServerDAL
{
	/// <summary>
	/// 口味
	/// </summary>
	public class Practice
	{
		/// <summary>
		/// 增加一条数据
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
		/// 更新一条数据
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
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
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
        /// 获得总记录数
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int GetCount(string strWhere)
        {
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "Practice"), new SqlParameter("@strWhere", strWhere));
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
        /// 删除一条记录
        /// </summary>
        /// <param name="Id"></param>
        /// 此代码由杭景科技代码内部生成器自动生成
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
        /// 批量删除记录
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int DelList(string IdList)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from Practice where pId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
	}
}

