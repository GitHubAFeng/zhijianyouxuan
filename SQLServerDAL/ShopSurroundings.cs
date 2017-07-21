
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

using Hangjing.Model;
using Hangjing.DBUtility;
namespace Hangjing.SQLServerDAL
{
	/// <summary>
	/// 数据访问类:ShopSurroundings
	/// </summary>
	public partial class ShopSurroundings
	{

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Hangjing.Model.ShopSurroundingsInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ShopSurroundings(");
			strSql.Append("Shopid,Title,Picture,Sort,ReveInt1,ReveInt2,ReveVar1,ReveVar2)");
			strSql.Append(" values (");
			strSql.Append("@Shopid,@Title,@Picture,@Sort,@ReveInt1,@ReveInt2,@ReveVar1,@ReveVar2)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Shopid", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,50),
					new SqlParameter("@Picture", SqlDbType.VarChar,512),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@ReveInt1", SqlDbType.Int,4),
					new SqlParameter("@ReveInt2", SqlDbType.Int,4),
					new SqlParameter("@ReveVar1", SqlDbType.VarChar,256),
					new SqlParameter("@ReveVar2", SqlDbType.VarChar,256)};
			parameters[0].Value = model.Shopid;
			parameters[1].Value = model.Title;
			parameters[2].Value = model.Picture;
			parameters[3].Value = model.Sort;
			parameters[4].Value = model.ReveInt1;
			parameters[5].Value = model.ReveInt2;
			parameters[6].Value = model.ReveVar1;
			parameters[7].Value = model.ReveVar2;

			return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(Hangjing.Model.ShopSurroundingsInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ShopSurroundings set ");
			strSql.Append("Shopid=@Shopid,");
			strSql.Append("Title=@Title,");
			strSql.Append("Picture=@Picture,");
			strSql.Append("Sort=@Sort,");
			strSql.Append("ReveInt1=@ReveInt1,");
			strSql.Append("ReveInt2=@ReveInt2,");
			strSql.Append("ReveVar1=@ReveVar1,");
			strSql.Append("ReveVar2=@ReveVar2");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@Shopid", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,50),
					new SqlParameter("@Picture", SqlDbType.VarChar,512),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@ReveInt1", SqlDbType.Int,4),
					new SqlParameter("@ReveInt2", SqlDbType.Int,4),
					new SqlParameter("@ReveVar1", SqlDbType.VarChar,256),
					new SqlParameter("@ReveVar2", SqlDbType.VarChar,256),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.Shopid;
			parameters[1].Value = model.Title;
			parameters[2].Value = model.Picture;
			parameters[3].Value = model.Sort;
			parameters[4].Value = model.ReveInt1;
			parameters[5].Value = model.ReveInt2;
			parameters[6].Value = model.ReveVar1;
			parameters[7].Value = model.ReveVar2;
			parameters[8].Value = model.ID;

			return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ShopSurroundings ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public int DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ShopSurroundings ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(strSql.ToString(), IDlist), null);
		}


        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataID</param>
        /// <returns>PPTInfo</returns>
        public ShopSurroundingsInfo  GetModel(int ID)
        {
            string sql = "select * from ShopSurroundings where ID = @ID";
            SqlParameter parameter = new SqlParameter("@ID", SqlDbType.Int, 4);
            parameter.Value = ID;
            ShopSurroundingsInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new ShopSurroundingsInfo();
                    model.ID = HJConvert.ToInt32(dr["ID"]);
                    model.Shopid = HJConvert.ToInt32(dr["Shopid"]);
                    model.Title = HJConvert.ToString(dr["Title"]);
                    model.Picture = HJConvert.ToString(dr["Picture"]);
                    model.Sort = HJConvert.ToInt32(dr["Sort"]);
                    model.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    model.ReveInt2 = HJConvert.ToInt32(dr["ReveInt2"]);
                    model.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
                    model.ReveVar2 = HJConvert.ToString(dr["ReveVar2"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "ShopSurroundings"), new SqlParameter("@strWhere", strWhere));
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
        public IList<ShopSurroundingsInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ShopSurroundingsInfo> infos = new List<ShopSurroundingsInfo>();
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
            parameters[0].Value = "ShopSurroundings";
            parameters[1].Value = "ID,Shopid,Title,Picture,Sort,ReveInt1,ReveInt2,ReveVar1,ReveVar2";
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
                    ShopSurroundingsInfo info = new ShopSurroundingsInfo();
                    info.ID = HJConvert.ToInt32(dr["ID"]);
                    info.Shopid = HJConvert.ToInt32(dr["Shopid"]);
                    info.Title = HJConvert.ToString(dr["Title"]);
                    info.Picture = HJConvert.ToString(dr["Picture"]);
                    info.Sort = HJConvert.ToInt32(dr["Sort"]);
                    info.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    info.ReveInt2 = HJConvert.ToInt32(dr["ReveInt2"]);
                    info.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
                    info.ReveVar2 = HJConvert.ToString(dr["ReveVar2"]);
                    infos.Add(info);
                }
            }
            return infos;
        }
	}
}

