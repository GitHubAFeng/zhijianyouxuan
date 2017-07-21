// EFood.cs
// CopyRight (c) 2009-2011 HangJing Teconology. All Rights Reserved.
// zjf@@ihangjing.com
// 2011-05-06
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;

using System.Data.SqlClient;

namespace Hangjing.SQLServerDAL
{
    public class TogoDiscount
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TogoDiscountInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TogoDiscount(");
            strSql.Append("togoid,starttime1,endtime,range1,range2,digital)");
            strSql.Append(" values (");
            strSql.Append("@togoid,@starttime1,@endtime,@range1,@range2,@digital)");

            SqlParameter[] parameters = 
            {
				new SqlParameter("@togoid", SqlDbType.Int,4),
				new SqlParameter("@starttime1", SqlDbType.DateTime),
				new SqlParameter("@endtime", SqlDbType.DateTime),
				new SqlParameter("@range1", SqlDbType.Decimal,5),
				new SqlParameter("@range2", SqlDbType.Decimal,5),
				new SqlParameter("@digital", SqlDbType.Decimal,5)
            };
            parameters[0].Value = model.togoid;
            parameters[1].Value = model.starttime1;
            parameters[2].Value = model.endtime;
            parameters[3].Value = model.range1;
            parameters[4].Value = model.range2;
            parameters[5].Value = model.digital;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(TogoDiscountInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TogoDiscount set ");
            strSql.Append("togoid=@togoid,");
            strSql.Append("starttime1=@starttime1,");
            strSql.Append("endtime=@endtime,");
            strSql.Append("range1=@range1,");
            strSql.Append("range2=@range2,");
            strSql.Append("digital=@digital");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@id", SqlDbType.Int,4),
				new SqlParameter("@togoid", SqlDbType.Int,4),
				new SqlParameter("@starttime1", SqlDbType.DateTime),
				new SqlParameter("@endtime", SqlDbType.DateTime),
				new SqlParameter("@range1", SqlDbType.Decimal,5),
				new SqlParameter("@range2", SqlDbType.Decimal,5),
				new SqlParameter("@digital", SqlDbType.Decimal,5)
            };
            parameters[0].Value = model.id;
            parameters[1].Value = model.togoid;
            parameters[2].Value = model.starttime1;
            parameters[3].Value = model.endtime;
            parameters[4].Value = model.range1;
            parameters[5].Value = model.range2;
            parameters[6].Value = model.digital;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }

        public int SaveModel(TogoDiscountInfo model)
        {
            SqlParameter[] parameters = 
            {
				new SqlParameter("@togoid", SqlDbType.Int,4),
				new SqlParameter("@starttime1", SqlDbType.DateTime),
				new SqlParameter("@endtime", SqlDbType.DateTime),
				new SqlParameter("@range1", SqlDbType.Decimal,5),
				new SqlParameter("@range2", SqlDbType.Decimal,5),
				new SqlParameter("@digital", SqlDbType.Decimal,5)
            };
            parameters[0].Value = model.togoid;
            parameters[1].Value = model.starttime1;
            parameters[2].Value = model.endtime;
            parameters[3].Value = model.range1;
            parameters[4].Value = model.range2;
            parameters[5].Value = model.digital;

            return SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "SaveTogoDiscount", parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>id</param>
        /// <returns>TogoDiscountInfo</returns>
        public TogoDiscountInfo GetModel(int TogoId)
        {
            string sql = "select id,togoid,starttime1,endtime,range1,range2,digital from TogoDiscount where  togoid = @togoid";
            SqlParameter parameter = new SqlParameter("@togoid", SqlDbType.Int, 4);
            parameter.Value = TogoId;
            TogoDiscountInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new TogoDiscountInfo();
                    model.id = HJConvert.ToInt32(dr["id"]);
                    model.togoid = HJConvert.ToInt32(dr["togoid"]);
                    model.starttime1 = HJConvert.ToDateTime(dr["starttime1"]);
                    model.endtime = HJConvert.ToDateTime(dr["endtime"]);
                    model.range1 = HJConvert.ToDecimal(dr["range1"]);
                    model.range2 = HJConvert.ToDecimal(dr["range2"]);
                    model.digital = HJConvert.ToDecimal(dr["digital"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "TogoDiscount"), new SqlParameter("@strWhere", strWhere));
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
        public IList<TogoDiscountInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<TogoDiscountInfo> infos = new List<TogoDiscountInfo>();
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
            parameters[0].Value = "TogoDiscount";
            parameters[1].Value = "id,togoid,starttime1,endtime,range1,range2,digital";
            parameters[2].Value = "id";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    TogoDiscountInfo info = new TogoDiscountInfo();
                    info.id = HJConvert.ToInt32(dr["id"]);
                    info.togoid = HJConvert.ToInt32(dr["togoid"]);
                    info.starttime1 = HJConvert.ToDateTime(dr["starttime1"]);
                    info.endtime = HJConvert.ToDateTime(dr["endtime"]);
                    info.range1 = HJConvert.ToDecimal(dr["range1"]);
                    info.range2 = HJConvert.ToDecimal(dr["range2"]);
                    info.digital = HJConvert.ToDecimal(dr["digital"]);
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
        public int DelTogoDiscount(int TogoId)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from TogoDiscount where togoid=@togoid");
            SqlParameter[] Para = 
			{
				new SqlParameter("@togoid",SqlDbType.Int)
			};
            Para[0].Value = TogoId;

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
            str.Append("delete from TogoDiscount where id in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}
