using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Hangjing.Model;
using Hangjing.DBUtility;

namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 数据访问类sys_Module。
    /// </summary>
    public class sys_Module
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(sys_ModuleInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_Module(");
            strSql.Append("M_ApplicationID,M_ParentID,M_PageCode,M_CName,M_Directory,M_OrderLevel,M_IsSystem,M_Close)");
            strSql.Append(" values (");
            strSql.Append("@M_ApplicationID,@M_ParentID,@M_PageCode,@M_CName,@M_Directory,@M_OrderLevel,@M_IsSystem,@M_Close)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@M_ApplicationID", SqlDbType.Int,4),
				new SqlParameter("@M_ParentID", SqlDbType.Int,4),
				new SqlParameter("@M_PageCode", SqlDbType.VarChar,6),
				new SqlParameter("@M_CName", SqlDbType.NVarChar,50),
				new SqlParameter("@M_Directory", SqlDbType.NVarChar,2000),
				new SqlParameter("@M_OrderLevel", SqlDbType.Int,4),
				new SqlParameter("@M_IsSystem", SqlDbType.Int,4),
				new SqlParameter("@M_Close", SqlDbType.Int,5)
            };
            parameters[0].Value = model.M_ApplicationID;
            parameters[1].Value = model.M_ParentID;
            parameters[2].Value = model.M_PageCode;
            parameters[3].Value = model.M_CName;
            parameters[4].Value = model.M_Directory;
            parameters[5].Value = model.M_OrderLevel;
            parameters[6].Value = model.M_IsSystem;
            parameters[7].Value = model.M_Close;

            return HJConvert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.sys_ModuleInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Module set ");
            strSql.Append("M_ParentID=@M_ParentID,");
            strSql.Append("M_CName=@M_CName,");
            strSql.Append("M_Directory=@M_Directory,");
            strSql.Append("M_OrderLevel=@M_OrderLevel,");
            strSql.Append("M_IsSystem=@M_IsSystem,");
            strSql.Append("M_Close=@M_Close");
            strSql.Append(" where M_ApplicationID=@M_ApplicationID and M_PageCode=@M_PageCode and ModuleID=@ModuleID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@ModuleID", SqlDbType.Int,4),
				new SqlParameter("@M_ApplicationID", SqlDbType.Int,4),
				new SqlParameter("@M_ParentID", SqlDbType.Int,4),
				new SqlParameter("@M_PageCode", SqlDbType.VarChar,6),
				new SqlParameter("@M_CName", SqlDbType.NVarChar,50),
				new SqlParameter("@M_Directory", SqlDbType.NVarChar,2000),
				new SqlParameter("@M_OrderLevel", SqlDbType.Int,4),
				new SqlParameter("@M_IsSystem", SqlDbType.TinyInt,1),
				new SqlParameter("@M_Close", SqlDbType.TinyInt,1)
            };
            parameters[0].Value = model.ModuleID;
            parameters[1].Value = model.M_ApplicationID;
            parameters[2].Value = model.M_ParentID;
            parameters[3].Value = model.M_PageCode;
            parameters[4].Value = model.M_CName;
            parameters[5].Value = model.M_Directory;
            parameters[6].Value = model.M_OrderLevel;
            parameters[7].Value = model.M_IsSystem;
            parameters[8].Value = model.M_Close;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>ModuleID</param>
        /// <returns>sys_ModuleInfo</returns>
        public sys_ModuleInfo GetModel(int ModuleID)
        {
            string sql = "select ModuleID,M_ApplicationID,M_ParentID,M_PageCode,M_CName,M_Directory,M_OrderLevel,M_IsSystem,M_Close,(select M_PageCode from sys_Module where ModuleID = a.M_ParentID) as parend_pagecode  from sys_Module as a where  ModuleID = @ModuleID";
            SqlParameter parameter = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameter.Value = ModuleID;
            sys_ModuleInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new sys_ModuleInfo();
                    model.ModuleID = HJConvert.ToInt32(dr["ModuleID"]);
                    model.M_ApplicationID = HJConvert.ToInt32(dr["M_ApplicationID"]);
                    model.M_ParentID = HJConvert.ToInt32(dr["M_ParentID"]);
                    model.M_PageCode = HJConvert.ToString(dr["M_PageCode"]);
                    model.M_CName = HJConvert.ToString(dr["M_CName"]);
                    model.M_Directory = HJConvert.ToString(dr["M_Directory"]);
                    model.M_OrderLevel = HJConvert.ToInt32(dr["M_OrderLevel"]);
                    model.M_IsSystem = HJConvert.ToInt32(dr["M_IsSystem"]);
                    model.M_Close = HJConvert.ToInt32(dr["M_Close"]);
                    model.parend_pagecode = HJConvert.ToString(dr["parend_pagecode"]);

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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "sys_Module"), new SqlParameter("@strWhere", strWhere));
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
        public IList<sys_ModuleInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<sys_ModuleInfo> infos = new List<sys_ModuleInfo>();
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
            parameters[0].Value = "sys_Module";
            parameters[1].Value = "ModuleID,M_ApplicationID,M_ParentID,M_PageCode,M_CName,M_Directory,M_OrderLevel,M_IsSystem,M_Close";
            parameters[2].Value = "ModuleID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    sys_ModuleInfo info = new sys_ModuleInfo();
                    info.ModuleID = HJConvert.ToInt32(dr["ModuleID"]);
                    info.M_ApplicationID = HJConvert.ToInt32(dr["M_ApplicationID"]);
                    info.M_ParentID = HJConvert.ToInt32(dr["M_ParentID"]);
                    info.M_PageCode = HJConvert.ToString(dr["M_PageCode"]);
                    info.M_CName = HJConvert.ToString(dr["M_CName"]);
                    info.M_Directory = HJConvert.ToString(dr["M_Directory"]);
                    info.M_OrderLevel = HJConvert.ToInt32(dr["M_OrderLevel"]);
                    info.M_IsSystem = HJConvert.ToInt32(dr["M_IsSystem"]);
                    info.M_Close = HJConvert.ToInt32(dr["M_Close"]);
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
        public int Delsys_Module(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from sys_Module where ModuleID=@ModuleID");
            SqlParameter[] Para = 
			{
				new SqlParameter("@ModuleID",SqlDbType.Int)
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
            str.Append("delete from sys_Module where ModuleID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 获取所有列表
        /// </summary>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<sys_ModuleInfo> getAll()
        {
            IList<sys_ModuleInfo> infos = new List<sys_ModuleInfo>();
            string sql = "select * from sys_Module where M_Close = 0 order by M_OrderLevel desc";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text,sql, null))
            {
                while (dr.Read())
                {
                    sys_ModuleInfo info = new sys_ModuleInfo();
                    info.ModuleID = HJConvert.ToInt32(dr["ModuleID"]);
                    info.M_ApplicationID = HJConvert.ToInt32(dr["M_ApplicationID"]);
                    info.M_ParentID = HJConvert.ToInt32(dr["M_ParentID"]);
                    info.M_PageCode = HJConvert.ToString(dr["M_PageCode"]);
                    info.M_CName = HJConvert.ToString(dr["M_CName"]);
                    info.M_Directory = HJConvert.ToString(dr["M_Directory"]);
                    info.M_OrderLevel = HJConvert.ToInt32(dr["M_OrderLevel"]);
                    info.M_IsSystem = HJConvert.ToInt32(dr["M_IsSystem"]);
                    info.M_Close = HJConvert.ToInt32(dr["M_Close"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 删除子类
        /// </summary>
        /// <param name="Id"></param>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <returns></returns>
        public int Delsub(int pId)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from sys_Module where M_ParentID=@ModuleID");
            SqlParameter[] Para = 
			{
				new SqlParameter("@ModuleID",SqlDbType.Int)
			};
            Para[0].Value = pId;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }
        
        /// <summary>
        /// 获取当前最大的pagecode
        /// </summary>
        /// <param name="where"></param>
        /// <param name="type">0表示一级,1表示二级</param>
        /// <returns></returns>
        public string  getMaxPagecode(string where , int type , string parentcode)
        {   
            IList<sys_ModuleInfo> infos = new List<sys_ModuleInfo>();
            string sql = "select top 1 M_PageCode from sys_Module where "+where+" order by ModuleID desc";
            string pagecode = "";
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (dr.Read())
                {
                    pagecode = HJConvert.ToString(dr["M_PageCode"]);
                }
            }
            if (type == 0)
            {
                if (pagecode == "")
                {
                    pagecode = "S00";
                }
                else
                {
                    string temp = pagecode.Substring(1, 2);
                    int newnum = Convert.ToInt32(temp) + 1;
                    pagecode = "S" + newnum.ToString("00");
                }
            }
            else
            {
                if (pagecode == "")
                {
                    pagecode = parentcode + "M00";
                }
                else
                {
                    string temp = pagecode.Substring(4, 2);
                    int newnum = Convert.ToInt32(temp) + 1;
                    pagecode = parentcode + "M" + newnum.ToString("00");
                }
            }
            return pagecode;
        }
    }
}

