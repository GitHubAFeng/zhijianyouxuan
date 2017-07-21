#region license
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :礼品类别数据层
// Created by tuhui at 2010-6-22 15:03:49.
// E-Mail: tuhui@ihangjing.com
#endregion
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
    public class GiftsClass
    {
        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>ClassId</param>
        /// <returns>GiftsClassInfo</returns>
        public GiftsClassInfo GetModel(int ClassId)
        {
            SqlParameter parameter = new SqlParameter("@nClassId", SqlDbType.Int, 4);
            parameter.Value = ClassId;
            GiftsClassInfo model = null;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, "select * from GiftsClass where ClassId=@nClassId", parameter))
            {
                if (dr.Read())
                {
                    model = new GiftsClassInfo();
                    model.ClassId = HJConvert.ToInt32(dr["ClassId"]);
                    model.ClassName = HJConvert.ToString(dr["ClassName"]);
                    model.ParentId = HJConvert.ToString(dr["ParentId"]);
                    model.ClassOrder = HJConvert.ToInt32(dr["ClassOrder"]);
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
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "GiftsClass"), new SqlParameter("@strWhere", strWhere)));
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
        public IList<GiftsClassInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<GiftsClassInfo> infos = new List<GiftsClassInfo>();
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
            parameters[0].Value = "GiftsClass";
            parameters[1].Value = "ClassId,ClassName,ParentId,ClassOrder";
            parameters[2].Value = "ClassId";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    GiftsClassInfo info = new GiftsClassInfo();
                    info.ClassId = HJConvert.ToInt32(dr["ClassId"]);
                    info.ClassName = HJConvert.ToString(dr["ClassName"]);
                    info.ParentId = HJConvert.ToString(dr["ParentId"]);
                    info.ClassOrder = HJConvert.ToInt32(dr["ClassOrder"]);
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
        public int DelGiftsClass(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from GiftsClass where ClassId=@ClassId");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@ClassId",SqlDbType.Int)
	        };
            Para[0].Value = Id;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), Para);
        }

        /// <summary>
        /// 批量删除记录
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int DelList(string IdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from GiftsClass where ClassId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(strSql.ToString(), IdList), null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.GiftsClassInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into GiftsClass(");
            strSql.Append("ClassName,ParentId,ClassOrder)");
            strSql.Append(" values (");
            strSql.Append("@ClassName,@ParentId,@ClassOrder)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@ClassName", SqlDbType.VarChar,100),
				new SqlParameter("@ParentId", SqlDbType.VarChar,10),
				new SqlParameter("@ClassOrder", SqlDbType.Int,4)
            };
            parameters[0].Value = model.ClassName;
            parameters[1].Value = model.ParentId;
            parameters[2].Value = model.ClassOrder;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.GiftsClassInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GiftsClass set ");
            strSql.Append("ClassName=@ClassName,");
            strSql.Append("ParentId=@ParentId,");
            strSql.Append("ClassOrder=@ClassOrder");
            strSql.Append(" where ClassId=@ClassId ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@ClassId", SqlDbType.Int,4),
				new SqlParameter("@ClassName", SqlDbType.VarChar,100),
				new SqlParameter("@ParentId", SqlDbType.VarChar,10),
				new SqlParameter("@ClassOrder", SqlDbType.Int,4)
            };
            parameters[0].Value = model.ClassId;
            parameters[1].Value = model.ClassName;
            parameters[2].Value = model.ParentId;
            parameters[3].Value = model.ClassOrder;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
    }
}
