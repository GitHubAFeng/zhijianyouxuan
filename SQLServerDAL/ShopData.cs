using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;


namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 数据访问类ShopData。
    /// </summary>
    public class ShopData 
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ShopDataInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ShopData(");
            strSql.Append("classname,Depth,Status,Priority,Parentid,isDel)");
            strSql.Append(" values (");
            strSql.Append("@classname,@Depth,@Status,@Priority,@Parentid,@isDel);");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@classname", SqlDbType.VarChar,256),
				new SqlParameter("@Depth", SqlDbType.Int,4),
				new SqlParameter("@Status", SqlDbType.Int,4),
				new SqlParameter("@Priority", SqlDbType.Int,4),
				new SqlParameter("@Parentid", SqlDbType.Int,4),
				new SqlParameter("@isDel", SqlDbType.Int,4)
            };
            parameters[0].Value = model.classname;
            parameters[1].Value = model.Depth;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.Priority;
            parameters[4].Value = model.Parentid;
            parameters[5].Value = model.isDel;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ShopDataInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ShopData set ");
            strSql.Append("classname=@classname,");
            strSql.Append("Depth=@Depth,");
            strSql.Append("Status=@Status,");
            strSql.Append("Priority=@Priority,");
            strSql.Append("Parentid=@Parentid,");
            strSql.Append("isDel=@isDel");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@ID", SqlDbType.Int,4),
				new SqlParameter("@classname", SqlDbType.VarChar,256),
				new SqlParameter("@Depth", SqlDbType.Int,4),
				new SqlParameter("@Status", SqlDbType.Int,4),
				new SqlParameter("@Priority", SqlDbType.Int,4),
				new SqlParameter("@Parentid", SqlDbType.Int,4),
				new SqlParameter("@isDel", SqlDbType.Int,4)
            };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.classname;
            parameters[2].Value = model.Depth;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.Priority;
            parameters[5].Value = model.Parentid;
            parameters[6].Value = model.isDel;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>ID</param>
        /// <returns>ShopDataInfo</returns>
        public ShopDataInfo GetModel(int ID)
        {
            string sql = "select ID,classname,Depth,Status,Priority,Parentid,isDel from ShopData where  ID = @ID";
            SqlParameter parameter = new SqlParameter("@ID", SqlDbType.Int, 4);
            parameter.Value = ID;
            ShopDataInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new ShopDataInfo();
                    model.ID = HJConvert.ToInt32(dr["ID"]);
                    model.classname = HJConvert.ToString(dr["classname"]);
                    model.Depth = HJConvert.ToInt32(dr["Depth"]);
                    model.Status = HJConvert.ToInt32(dr["Status"]);
                    model.Priority = HJConvert.ToInt32(dr["Priority"]);
                    model.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    model.isDel = HJConvert.ToInt32(dr["isDel"]);
                }
            }
            return model;
        }


        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>ID</param>
        /// <returns>ShopDataInfo</returns>
        public  string ShopDataclassname(string  strwhere)
        {
            string sql = "select classname from ShopData where  " + strwhere + "";
            return Convert.ToString(SQLHelper.ExecuteScalar(CommandType.Text, sql, null));
         
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
            Para[0].Value = "ShopData";
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
        public IList<ShopDataInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ShopDataInfo> infos = new List<ShopDataInfo>();
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
            parameters[0].Value = "ShopData";
            parameters[1].Value = "*";
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
                    ShopDataInfo info = new ShopDataInfo();
                    info.ID = HJConvert.ToInt32(dr["ID"]);
                    info.classname = HJConvert.ToString(dr["classname"]);
                    info.Depth = HJConvert.ToInt32(dr["Depth"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.Priority = HJConvert.ToInt32(dr["Priority"]);
                    info.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    info.isDel = HJConvert.ToInt32(dr["isDel"]);

                    info.Pic = HJConvert.ToString(dr["Pic"]);
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
        public int DelShopData(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ShopData where ID=@ID");
            SqlParameter[] Para = 
			{
				new SqlParameter("@ID",SqlDbType.Int)
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
            str.Append("delete from ShopData where ID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }


        /// <summary>
        /// 获取子项列表
        /// </summary>
        /// <param name="pid">父类编号</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<ShopDataInfo> GetsubList(int pid)
        {
            IList<ShopDataInfo> infos = new List<ShopDataInfo>();
            SqlParameter[] parameters = 
			{
				new SqlParameter("@pid", SqlDbType.Int)
			};
            parameters[0].Value = pid;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "ShopData_getSublist", parameters))
            {
                while (dr.Read())
                {
                    ShopDataInfo info = new ShopDataInfo();
                    info.ID = HJConvert.ToInt32(dr["ID"]);
                    info.classname = HJConvert.ToString(dr["classname"]);
                    info.Depth = HJConvert.ToInt32(dr["Depth"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.Priority = HJConvert.ToInt32(dr["Priority"]);
                    info.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    info.isDel = HJConvert.ToInt32(dr["isDel"]);
                    info.Pic = HJConvert.ToString(dr["pic"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 更新名称，排序
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int updateName(int id , string classname , int pri)
        {
           StringBuilder strSql = new StringBuilder();
            strSql.Append("update ShopData set ");
            strSql.Append("classname=@classname,");
            strSql.Append("Priority=@Priority");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@ID", SqlDbType.Int,4),
				new SqlParameter("@classname", SqlDbType.VarChar,256),
				new SqlParameter("@Priority", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            parameters[1].Value = classname;
            parameters[2].Value = pri;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回名称，用','分开
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public string GetListNames(string idlist)
        {
            string rs = "";
            string sql = "select classname from shopdata where id in ("+idlist+")";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    rs += HJConvert.ToString(dr["classname"])+",";    
                }
            }
            rs = System.Text.RegularExpressions.Regex.Replace(rs, @",$", " ");
            return rs;
        }
    }
}

