// ESection:区域数据访问类
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// update by yangxiaolong@ihangjing.com
// 2010-03-11
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Hangjing.DBUtility;
using Hangjing.Model;

using System.Collections.Generic;

namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 数据访问类SectionInfo。
    /// </summary>
    public class ESection 
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SectionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SectionInfo(");
            strSql.Append("SectionName,pri,Parentid,cityid)");
            strSql.Append(" values (");
            strSql.Append("@SectionName,@pri,@Parentid,@cityid)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@SectionName", SqlDbType.VarChar,256),
				new SqlParameter("@pri", SqlDbType.Int,4),
				new SqlParameter("@Parentid", SqlDbType.Int,4),
				new SqlParameter("@cityid", SqlDbType.Int,4)
            };
            parameters[0].Value = model.SectionName;
            parameters[1].Value = model.pri;
            parameters[2].Value = model.Parentid;
            parameters[3].Value = model.cityid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(SectionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SectionInfo set ");
            strSql.Append("SectionName=@SectionName,");
            strSql.Append("pri=@pri,");
            strSql.Append("Parentid=@Parentid");
            strSql.Append(" where SectionID=@SectionID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@SectionID", SqlDbType.Int,4),
				new SqlParameter("@SectionName", SqlDbType.VarChar,256),
				new SqlParameter("@pri", SqlDbType.Int,4),
				new SqlParameter("@Parentid", SqlDbType.Int,4),
            };
            parameters[0].Value = model.SectionID;
            parameters[1].Value = model.SectionName;
            parameters[2].Value = model.pri;
            parameters[3].Value = model.Parentid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int SectionID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from SectionInfo where SectionID=@SectionID");

            SqlParameter[] Para = 
            {
			    new SqlParameter("@SectionID", SqlDbType.Int)
            };
            Para[0].Value = SectionID;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        public int DelList(string IdList)
        {
            SqlParameter[] Para = 
            {
                new SqlParameter("@IdList",SqlDbType.VarChar,200)
            };
            Para[0].Value = IdList;

            return SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "DelSectionInfoList", Para);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SectionInfo GetModel(int SectionID)
        {
            SqlParameter[] Para =
            {
                new SqlParameter("@SectionID",SqlDbType.Int)  
            };
            Para[0].Value = SectionID;
            string sql = "select * from SectionInfo where SectionID = @SectionID";

            SectionInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, Para))
            {
                if (dr.Read())
                {
                    model = new SectionInfo();
                    model.SectionID = HJConvert.ToInt32(dr["SectionID"]);
                    model.SectionName = HJConvert.ToString(dr["SectionName"]);
                    model.pri = HJConvert.ToInt32(dr["pri"]);
                    model.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    model.cityid = HJConvert.ToInt32(dr["cityid"]);
                }
            }

            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<SectionInfo> GetList(int pageSize, int pageIndex, string where, string orderField, int orderType)
        {
            IList<SectionInfo> DataList = new List<SectionInfo>();

            SqlParameter[] Para = 
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
            Para[0].Value = "SectionInfo";
            Para[1].Value = "*";
            Para[2].Value = "SectionID";
            Para[3].Value = orderField;
            Para[4].Value = pageSize;
            Para[5].Value = pageIndex;
            Para[6].Value = 1;
            Para[7].Value = where;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", Para))
            {
                while (dr.Read())
                {
                    SectionInfo model = new SectionInfo();
                    model.SectionID = HJConvert.ToInt32(dr["SectionID"]);
                    model.SectionName = HJConvert.ToString(dr["SectionName"]);
                    model.pri = HJConvert.ToInt32(dr["pri"]);
                    model.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    model.cityid = HJConvert.ToInt32(dr["cityid"]);
                    DataList.Add(model);
                }
            }
            return DataList;
        }

        /// <summary>
        /// 获取所有区域信息
        /// </summary>
        /// <returns></returns>
        public IList<SectionInfo> GetAll()
        {
            string sqlsp = "SectionInfo_GetALL";
            IList<SectionInfo> DataList = new List<SectionInfo>();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, sqlsp, null))
            {
                while (dr.Read())
                {
                    SectionInfo model = new SectionInfo();
                    model.SectionID = HJConvert.ToInt32(dr["SectionID"]);
                    model.SectionName = HJConvert.ToString(dr["SectionName"]);
                    model.pri = HJConvert.ToInt32(dr["pri"]);
                    model.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    model.cityid = HJConvert.ToInt32(dr["cityid"]);
                    DataList.Add(model);
                }
            }
            return DataList;
        }

        /// <summary>
        /// 取得记录数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetCount(string strWhere)
        {
            SqlParameter[] parameters = 
            {
                new SqlParameter("@tblName", "SectionInfo"),
                new SqlParameter("@strWhere", strWhere)
            };
            parameters[0].Value = "SectionInfo";
            parameters[1].Value = strWhere;

            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", parameters);
        }


        /// <summary>
        /// 获取所有区域信息(含子区域)(根据对应城市)
        /// </summary>
        /// <returns></returns>
        public IList<SectionInfo> GetAll_fix(int cityid)
        {
            string sqlsp = "select * from SectionInfo where cityid = @cityid  order by pri desc";
            IList<SectionInfo> DataList = new List<SectionInfo>();
            SqlParameter[] Para =
            {
                new SqlParameter("@cityid",SqlDbType.Int)  
            };
            Para[0].Value = cityid;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sqlsp, Para))
            {
                while (dr.Read())
                {
                    SectionInfo model = new SectionInfo();
                    model.SectionID = HJConvert.ToInt32(dr["SectionID"]);
                    model.SectionName = HJConvert.ToString(dr["SectionName"]);
                    model.pri = HJConvert.ToInt32(dr["pri"]);
                    model.Parentid = HJConvert.ToInt32(dr["Parentid"]);
                    model.cityid = HJConvert.ToInt32(dr["cityid"]);
                    DataList.Add(model);
                }
            }
            return DataList;
        }
    }
}

