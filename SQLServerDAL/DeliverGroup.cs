using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hangjing.DBUtility;//请先添加引用
using Hangjing.Model;
using System.Data.SqlClient;
using System.Data;
namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 配送员分组管理
    /// </summary>
    public class DeliverGroup 
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DeliverGroupInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DeliverGroup(");
            strSql.Append("classname,Depth,Status,Priority,Parentid,isDel)");
            strSql.Append(" values (");
            strSql.Append("@classname,@Depth,@Status,@Priority,@Parentid,@isDel)");
            strSql.Append(";select @@IDENTITY");
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
        public int Update(DeliverGroupInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DeliverGroup set ");
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
        public DeliverGroupInfo GetModel(int ID)
        {
            string sql = "select ID,classname,Depth,Status,Priority,Parentid,isDel from DeliverGroup where  ID = @ID";
            SqlParameter parameter = new SqlParameter("@ID", SqlDbType.Int, 4);
            parameter.Value = ID;
            DeliverGroupInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new DeliverGroupInfo();
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
        /// 获得总记录数
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int GetCount(string strWhere)
        {
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "DeliverGroup"), new SqlParameter("@strWhere", strWhere));
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
        public IList<DeliverGroupInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<DeliverGroupInfo> infos = new List<DeliverGroupInfo>();
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
            parameters[0].Value = "DeliverGroup";
            parameters[1].Value = "*,(select cname from City where cid=DeliverGroup.Status) cityname";
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
                    DeliverGroupInfo info = new DeliverGroupInfo();
                    info.ID = HJConvert.ToInt32(dr["ID"]);
                    info.classname = HJConvert.ToString(dr["classname"]);
                    info.Depth = HJConvert.ToInt32(dr["Depth"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.Priority = HJConvert.ToInt32(dr["Priority"]);
                    info.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    info.isDel = HJConvert.ToInt32(dr["isDel"]);
                    info.pic = HJConvert.ToString(dr["pic"]);
                    info.hovepic = HJConvert.ToString(dr["hovepic"]);
                    info.cityname = HJConvert.ToString(dr["cityname"]);
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
            str.Append("delete from DeliverGroup where ID=@ID");
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
            str.Append("delete from DeliverGroup where ID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }


        /// <summary>
        /// 获取子项列表
        /// </summary>
        /// <param name="pid">父类编号</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<DeliverGroupInfo> GetsubList(int pid)
        {
            return GetsubData(pid);
        }

        /// <summary>
        /// 获取子项列表
        /// </summary>
        /// <param name="pid">父类编号</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<DeliverGroupInfo> GetsubData(int pid)
        {
            IList<DeliverGroupInfo> infos = new List<DeliverGroupInfo>();
            SqlParameter[] parameters = 
			{
				new SqlParameter("@pid", SqlDbType.Int,5)
			};
            parameters[0].Value = pid;
            string sql = "select * from DeliverGroup where  parentid = @pid";
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameters))
            {
                while (dr.Read())
                {
                    DeliverGroupInfo info = new DeliverGroupInfo();
                    info.ID = HJConvert.ToInt32(dr["ID"]);
                    info.classname = HJConvert.ToString(dr["classname"]);
                    info.Depth = HJConvert.ToInt32(dr["Depth"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.Priority = HJConvert.ToInt32(dr["Priority"]);
                    info.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    info.isDel = HJConvert.ToInt32(dr["isDel"]);
                    info.pic = HJConvert.ToString(dr["pic"]);
                    info.hovepic = HJConvert.ToString(dr["hovepic"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public IList<DeliverGroupInfo> GetAll()
        {
            IList<DeliverGroupInfo> infos = new List<DeliverGroupInfo>();
            string sql = "select * from DeliverGroup where 1=1";
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    DeliverGroupInfo info = new DeliverGroupInfo();
                    info.ID = HJConvert.ToInt32(dr["ID"]);
                    info.classname = HJConvert.ToString(dr["classname"]);
                    info.Depth = HJConvert.ToInt32(dr["Depth"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.Priority = HJConvert.ToInt32(dr["Priority"]);
                    info.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    info.isDel = HJConvert.ToInt32(dr["isDel"]);
                    info.pic = HJConvert.ToString(dr["pic"]);
                    info.hovepic = HJConvert.ToString(dr["hovepic"]);
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
        public int updateName(int id, string classname, int pri, int isdel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DeliverGroup set ");
            strSql.Append("classname=@classname,");
            strSql.Append("Priority=@Priority,isDel=@isDel");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@ID", SqlDbType.Int,4),
				new SqlParameter("@classname", SqlDbType.VarChar,256),
				new SqlParameter("@Priority", SqlDbType.Int,4),
                new SqlParameter("@isDel", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            parameters[1].Value = classname;
            parameters[2].Value = pri;
            parameters[3].Value = isdel;

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
            string sql = "select classname from DeliverGroup where id in (" + idlist + ")";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    rs += HJConvert.ToString(dr["classname"]) + ",";
                }
            }
            rs = System.Text.RegularExpressions.Regex.Replace(rs, @",$", " ");
            return rs;
        }
    }
}

