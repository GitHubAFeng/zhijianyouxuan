using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Hangjing.DBUtility;
using Hangjing.Model;

namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 套餐中的商品类
    /// </summary>
    public partial class PackFoodlist
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PackFoodlistInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PackFoodlist(");
            strSql.Append("shopid,pid,foodname,fid,foodcount,sortnum,sid,ReveVar,ReveVar1");
            strSql.Append(") values (");
            strSql.Append("@shopid,@pid,@foodname,@fid,@foodcount,@sortnum,@sid,@ReveVar,@ReveVar1");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
			{
			    new SqlParameter("@shopid", SqlDbType.Int,4) ,            
                new SqlParameter("@pid", SqlDbType.Int,4) ,            
                new SqlParameter("@foodname", SqlDbType.VarChar,256) ,            
                new SqlParameter("@fid", SqlDbType.Int,4) ,            
                new SqlParameter("@foodcount", SqlDbType.Int,4) ,            
                new SqlParameter("@sortnum", SqlDbType.Int,4) ,            
                new SqlParameter("@sid", SqlDbType.Int,4) ,            
                new SqlParameter("@ReveVar", SqlDbType.VarChar,256) ,            
                new SqlParameter("@ReveVar1", SqlDbType.VarChar,256)             
              
            };

            parameters[0].Value = model.shopid;
            parameters[1].Value = model.pid;
            parameters[2].Value = model.foodname;
            parameters[3].Value = model.fid;
            parameters[4].Value = model.foodcount;
            parameters[5].Value = model.sortnum;
            parameters[6].Value = model.sid;
            parameters[7].Value = model.ReveVar;
            parameters[8].Value = model.ReveVar1;
            return HJConvert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(PackFoodlistInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PackFoodlist set ");
            strSql.Append(" shopid = @shopid , ");
            strSql.Append(" pid = @pid , ");
            strSql.Append(" foodname = @foodname , ");
            strSql.Append(" fid = @fid , ");
            strSql.Append(" foodcount = @foodcount , ");
            strSql.Append(" sortnum = @sortnum , ");
            strSql.Append(" sid = @sid , ");
            strSql.Append(" ReveVar = @ReveVar , ");
            strSql.Append(" ReveVar1 = @ReveVar1  ");
            strSql.Append(" where mID=@mID ");

            SqlParameter[] parameters = 
			{
			    new SqlParameter("@mID", SqlDbType.Int,4) ,            
                new SqlParameter("@shopid", SqlDbType.Int,4) ,            
                new SqlParameter("@pid", SqlDbType.Int,4) ,            
                new SqlParameter("@foodname", SqlDbType.VarChar,256) ,            
                new SqlParameter("@fid", SqlDbType.Int,4) ,            
                new SqlParameter("@foodcount", SqlDbType.Int,4) ,            
                new SqlParameter("@sortnum", SqlDbType.Int,4) ,            
                new SqlParameter("@sid", SqlDbType.Int,4) ,            
                new SqlParameter("@ReveVar", SqlDbType.VarChar,256) ,            
                new SqlParameter("@ReveVar1", SqlDbType.VarChar,256)             
              
            };

            parameters[0].Value = model.mID;
            parameters[1].Value = model.shopid;
            parameters[2].Value = model.pid;
            parameters[3].Value = model.foodname;
            parameters[4].Value = model.fid;
            parameters[5].Value = model.foodcount;
            parameters[6].Value = model.sortnum;
            parameters[7].Value = model.sid;
            parameters[8].Value = model.ReveVar;
            parameters[9].Value = model.ReveVar1;
            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>mID</param>
        /// <returns>PackFoodlistInfo</returns>
        public PackFoodlistInfo GetModel(int mID)
        {
            string sql = "select mID,shopid,pid,foodname,fid,foodcount,sortnum,sid,ReveVar,ReveVar1 from PackFoodlist where  mID = @mID";
            SqlParameter parameter = new SqlParameter("@mID", SqlDbType.Int, 4);
            parameter.Value = mID;
            PackFoodlistInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new PackFoodlistInfo();
                    model.mID = HJConvert.ToInt32(dr["mID"]);
                    model.shopid = HJConvert.ToInt32(dr["shopid"]);
                    model.pid = HJConvert.ToInt32(dr["pid"]);
                    model.foodname = HJConvert.ToString(dr["foodname"]);
                    model.fid = HJConvert.ToInt32(dr["fid"]);
                    model.foodcount = HJConvert.ToInt32(dr["foodcount"]);
                    model.sortnum = HJConvert.ToInt32(dr["sortnum"]);
                    model.sid = HJConvert.ToInt32(dr["sid"]);
                    model.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    model.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "PackFoodlist"), new SqlParameter("@strWhere", strWhere));
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
        public IList<PackFoodlistInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<PackFoodlistInfo> infos = new List<PackFoodlistInfo>();
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
            parameters[0].Value = "PackFoodlist";
            parameters[1].Value = "mID,shopid,pid,foodname,fid,foodcount,sortnum,sid,ReveVar,ReveVar1";
            parameters[2].Value = "mID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    PackFoodlistInfo info = new PackFoodlistInfo();
                    info.mID = HJConvert.ToInt32(dr["mID"]);
                    info.shopid = HJConvert.ToInt32(dr["shopid"]);
                    info.pid = HJConvert.ToInt32(dr["pid"]);
                    info.foodname = HJConvert.ToString(dr["foodname"]);
                    info.fid = HJConvert.ToInt32(dr["fid"]);
                    info.foodcount = HJConvert.ToInt32(dr["foodcount"]);
                    info.sortnum = HJConvert.ToInt32(dr["sortnum"]);
                    info.sid = HJConvert.ToInt32(dr["sid"]);
                    info.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    info.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
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
        public int DelPackFoodlist(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from PackFoodlist where mID=@mID");
            SqlParameter[] Para = 
			{
				new SqlParameter("@mID",SqlDbType.Int)
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
            str.Append("delete from PackFoodlist where mID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }



        /// <summary>
        /// 批添加记录
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        /// 此代码由杭景科技代码内部生成器自动生成
        public void AddWidhDataTable(DataTable dt)
        {
            SQLHelper.SqlBulkCopyData(dt);
        }
    }
}

