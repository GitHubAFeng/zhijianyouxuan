using Hangjing.DBUtility;
using Hangjing.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 备注快捷选项
    /// </summary>
    public class STemplate
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(STemplateInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into STemplate(");
            strSql.Append("classname,Depth,Status,Priority,Parentid,isDel,pic)");
            strSql.Append(" values (");
            strSql.Append("@classname,@Depth,@Status,@Priority,@Parentid,@isDel,@pic)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@classname", SqlDbType.VarChar,256),
				new SqlParameter("@Depth", SqlDbType.Int,4),
				new SqlParameter("@Status", SqlDbType.Int,4),
				new SqlParameter("@Priority", SqlDbType.Int,4),
				new SqlParameter("@Parentid", SqlDbType.Int,4),
				new SqlParameter("@isDel", SqlDbType.Int,4),
				new SqlParameter("@pic", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.classname;
            parameters[1].Value = model.Depth;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.Priority;
            parameters[4].Value = model.Parentid;
            parameters[5].Value = model.isDel;
            parameters[6].Value = model.pic;


            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(STemplateInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update STemplate set ");
            strSql.Append("classname=@classname,");
            strSql.Append("Depth=@Depth,");
            strSql.Append("Status=@Status,");
            strSql.Append("Priority=@Priority,");
            strSql.Append("Parentid=@Parentid,");
            strSql.Append("isDel=@isDel,");
            strSql.Append("pic=@pic");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters =
            {
				new SqlParameter("@ID", SqlDbType.Int,4),
				new SqlParameter("@classname", SqlDbType.VarChar,256),
				new SqlParameter("@Depth", SqlDbType.Int,4),
				new SqlParameter("@Status", SqlDbType.Int,4),
				new SqlParameter("@Priority", SqlDbType.Int,4),
				new SqlParameter("@Parentid", SqlDbType.Int,4),
				new SqlParameter("@isDel", SqlDbType.Int,4),
				new SqlParameter("@pic", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.classname;
            parameters[2].Value = model.Depth;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.Priority;
            parameters[5].Value = model.Parentid;
            parameters[6].Value = model.isDel;
            parameters[7].Value = model.pic;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>ID</param>
        /// <returns>STemplateInfo</returns>
        public STemplateInfo GetModel(int ID)
        {
            string sql = "select * from STemplate where  ID = @ID";
            SqlParameter parameter = new SqlParameter("@ID", SqlDbType.Int, 4);
            parameter.Value = ID;
            STemplateInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new STemplateInfo();
                    model.ID = HJConvert.ToInt32(dr["ID"]);
                    model.classname = HJConvert.ToString(dr["classname"]);
                    model.Depth = HJConvert.ToInt32(dr["Depth"]);
                    model.Status = HJConvert.ToInt32(dr["Status"]);
                    model.Priority = HJConvert.ToInt32(dr["Priority"]);
                    model.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    model.isDel = HJConvert.ToInt32(dr["isDel"]);
                    model.pic = HJConvert.ToString(dr["pic"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "STemplate"), new SqlParameter("@strWhere", strWhere));
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
        public IList<STemplateInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<STemplateInfo> infos = new List<STemplateInfo>();
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
            parameters[0].Value = "STemplate";
            parameters[1].Value = "ID,classname,Depth,Status,Priority,Parentid,isDel,pic";
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
                    STemplateInfo info = new STemplateInfo();
                    info.ID = HJConvert.ToInt32(dr["ID"]);
                    info.classname = HJConvert.ToString(dr["classname"]);
                    info.Depth = HJConvert.ToInt32(dr["Depth"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.Priority = HJConvert.ToInt32(dr["Priority"]);
                    info.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    info.isDel = HJConvert.ToInt32(dr["isDel"]);
                    info.pic = HJConvert.ToString(dr["pic"]);
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
            str.Append("delete from STemplate where ID=@ID");
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
            str.Append("delete from STemplate where ID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }


        /// <summary>
        /// 获取子项列表
        /// </summary>
        /// <param name="pid">父类编号</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<STemplateInfo> GetsubList(int pid,int type)
        {
            IList<STemplateInfo> infos = new List<STemplateInfo>();
            string sql = "select * from STemplate where  parentid = @pid  and depth=@depth   order by Priority desc";
            SqlParameter[] parameters = 
			{
				new SqlParameter("@pid", SqlDbType.Int,5),
                new SqlParameter("@depth", SqlDbType.Int,5)
			};
            parameters[0].Value = pid;
            parameters[1].Value = type;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameters))
            {
                while (dr.Read())
                {
                    STemplateInfo info = new STemplateInfo();
                    info.ID = HJConvert.ToInt32(dr["ID"]);
                    info.classname = HJConvert.ToString(dr["classname"]);
                    info.Depth = HJConvert.ToInt32(dr["Depth"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.Priority = HJConvert.ToInt32(dr["Priority"]);
                    info.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    info.isDel = HJConvert.ToInt32(dr["isDel"]);
                    info.pic = HJConvert.ToString(dr["pic"]);
                    infos.Add(info);
                }
            }
            return infos;
        }
        /// <summary>
        /// 获取子项列表
        /// </summary>
        /// <param name="pid">父类编号</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<STemplateInfo> GetsubList(int pid)
        {
            IList<STemplateInfo> infos = new List<STemplateInfo>();
            string sql = "select * from STemplate where  parentid = @pid order by Priority desc";
            SqlParameter[] parameters = 
			{
				new SqlParameter("@pid", SqlDbType.Int,5)
			};
            parameters[0].Value = pid;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameters))
            {
                while (dr.Read())
                {
                    STemplateInfo info = new STemplateInfo();
                    info.ID = HJConvert.ToInt32(dr["ID"]);
                    info.classname = HJConvert.ToString(dr["classname"]);
                    info.Depth = HJConvert.ToInt32(dr["Depth"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.Priority = HJConvert.ToInt32(dr["Priority"]);
                    info.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    info.isDel = HJConvert.ToInt32(dr["isDel"]);
                    info.pic = HJConvert.ToString(dr["pic"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 更新名称，排序,编号
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int updateName(int id, string classname, int pri, string pic)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update STemplate set ");
            strSql.Append("classname=@classname,");
            strSql.Append("Priority=@Priority,pic=@pic");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@ID", SqlDbType.Int,4),
				new SqlParameter("@classname", SqlDbType.VarChar,256),
				new SqlParameter("@Priority", SqlDbType.Int,4),
                new SqlParameter("@pic", SqlDbType.VarChar , 256)
            };
            parameters[0].Value = id;
            parameters[1].Value = classname;
            parameters[2].Value = pri;
            parameters[3].Value = pic;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
    }
}
