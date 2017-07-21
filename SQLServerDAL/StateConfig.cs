//各个状态的操作。
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

using Hangjing.DBUtility;//请先添加引用
using Hangjing.Model;

namespace Hangjing.SQLServerDAL
{
    public class StateConfig
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.StateConfigInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into StateConfig(");
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
        public int Update(Hangjing.Model.StateConfigInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update StateConfig set ");
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
        /// <returns>comOppTypeInfo</returns>
        public StateConfigInfo GetModel(int ID)
        {
            string sql = "select ID,classname,Depth,Status,Priority,Parentid,isDel from StateConfig where  ID = @ID";
            SqlParameter parameter = new SqlParameter("@ID", SqlDbType.Int, 4);
            parameter.Value = ID;
            StateConfigInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new StateConfigInfo();
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "StateConfig"), new SqlParameter("@strWhere", strWhere));
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
        public IList<StateConfigInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<StateConfigInfo> infos = new List<StateConfigInfo>();
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
            parameters[0].Value = "StateConfig";
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
                    StateConfigInfo info = new StateConfigInfo();
                    info.ID = HJConvert.ToInt32(dr["ID"]);
                    info.classname = HJConvert.ToString(dr["classname"]);
                    info.Depth = HJConvert.ToInt32(dr["Depth"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.Priority = HJConvert.ToInt32(dr["Priority"]);
                    info.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    info.isDel = HJConvert.ToInt32(dr["isDel"]);
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
        public int DelcomOppType(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from StateConfig where ID=@ID");
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
            str.Append("delete from StateConfig where ID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pir"></param>
        /// <returns></returns>
        public int updatePri(int id, int pir)
        {
            string sp = "StateConfig_UpdatePri";
            SqlParameter[] Para = 
            {
	            new SqlParameter("@ID",SqlDbType.Int , 4),
                new SqlParameter("@Pri",SqlDbType.Int , 4)
            };
            Para[0].Value = id;
            Para[1].Value = pir;

            return SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, sp, Para);
        }

        /// <summary>
        /// 获取子项列表
        /// </summary>
        /// <param name="pid">父类编号</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<StateConfigInfo> GetsubList(int pid)
        {
            IList<StateConfigInfo> infos = new List<StateConfigInfo>();
            SqlParameter[] parameters = 
			{
				new SqlParameter("@pid", SqlDbType.Int,5)
			};
            parameters[0].Value = pid;

            string sql = "select * from StateConfig where parentid = @pid and isdel = 0 order by Priority asc";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameters))
            {
                while (dr.Read())
                {
                    StateConfigInfo info = new StateConfigInfo();
                    info.ID = HJConvert.ToInt32(dr["ID"]);
                    info.classname = HJConvert.ToString(dr["classname"]);
                    info.Depth = HJConvert.ToInt32(dr["Depth"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.Priority = HJConvert.ToInt32(dr["Priority"]);
                    info.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    info.isDel = HJConvert.ToInt32(dr["isDel"]);
                    infos.Add(info);
                }
            }
            return infos;
        }
    }
}
