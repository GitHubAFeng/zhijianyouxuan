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
    /// 数据访问类:shopdelivery
    /// </summary>
    public partial class shopdelivery
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.shopdeliveryInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into shopdelivery(");
            strSql.Append("tid,AddTime,distancestart,distanceend,minmoney,sendmoney,ReveInt1,ReveInt2,ReveVar1,ReveVar2,ReveFloat1,ReveFloat2)");
            strSql.Append(" values (");
            strSql.Append("@tid,@AddTime,@distancestart,@distanceend,@minmoney,@sendmoney,@ReveInt1,@ReveInt2,@ReveVar1,@ReveVar2,@ReveFloat1,@ReveFloat2)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@tid", SqlDbType.Int,4),
				new SqlParameter("@AddTime", SqlDbType.DateTime),
				new SqlParameter("@distancestart", SqlDbType.Decimal,5),
				new SqlParameter("@distanceend", SqlDbType.Decimal,5),
				new SqlParameter("@minmoney", SqlDbType.Int,4),
				new SqlParameter("@sendmoney", SqlDbType.Decimal,5),
				new SqlParameter("@ReveInt1", SqlDbType.Int,4),
				new SqlParameter("@ReveInt2", SqlDbType.Int,4),
				new SqlParameter("@ReveVar1", SqlDbType.VarChar,256),
				new SqlParameter("@ReveVar2", SqlDbType.VarChar,256),
				new SqlParameter("@ReveFloat1", SqlDbType.Decimal,5),
				new SqlParameter("@ReveFloat2", SqlDbType.Decimal,5)
            };
            parameters[0].Value = model.tid;
            parameters[1].Value = model.AddTime;
            parameters[2].Value = model.distancestart;
            parameters[3].Value = model.distanceend;
            parameters[4].Value = model.minmoney;
            parameters[5].Value = model.sendmoney;
            parameters[6].Value = model.ReveInt1;
            parameters[7].Value = model.ReveInt2;
            parameters[8].Value = model.ReveVar1;
            parameters[9].Value = model.ReveVar2;
            parameters[10].Value = model.ReveFloat1;
            parameters[11].Value = model.ReveFloat2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.shopdeliveryInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update shopdelivery set ");
            strSql.Append("tid=@tid,");
            strSql.Append("AddTime=@AddTime,");
            strSql.Append("distancestart=@distancestart,");
            strSql.Append("distanceend=@distanceend,");
            strSql.Append("minmoney=@minmoney,");
            strSql.Append("sendmoney=@sendmoney,");
            strSql.Append("ReveInt1=@ReveInt1,");
            strSql.Append("ReveInt2=@ReveInt2,");
            strSql.Append("ReveVar1=@ReveVar1,");
            strSql.Append("ReveVar2=@ReveVar2,");
            strSql.Append("ReveFloat1=@ReveFloat1,");
            strSql.Append("ReveFloat2=@ReveFloat2");
            strSql.Append(" where rId=@rId");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@tid", SqlDbType.Int,4),
				new SqlParameter("@AddTime", SqlDbType.DateTime),
				new SqlParameter("@distancestart", SqlDbType.Decimal,5),
				new SqlParameter("@distanceend", SqlDbType.Decimal,5),
				new SqlParameter("@minmoney", SqlDbType.Int,4),
				new SqlParameter("@sendmoney", SqlDbType.Decimal,5),
				new SqlParameter("@ReveInt1", SqlDbType.Int,4),
				new SqlParameter("@ReveInt2", SqlDbType.Int,4),
				new SqlParameter("@ReveVar1", SqlDbType.VarChar,256),
				new SqlParameter("@ReveVar2", SqlDbType.VarChar,256),
				new SqlParameter("@ReveFloat1", SqlDbType.Decimal,5),
				new SqlParameter("@ReveFloat2", SqlDbType.Decimal,5),
				new SqlParameter("@rId", SqlDbType.Int,4)
            };
            parameters[0].Value = model.tid;
            parameters[1].Value = model.AddTime;
            parameters[2].Value = model.distancestart;
            parameters[3].Value = model.distanceend;
            parameters[4].Value = model.minmoney;
            parameters[5].Value = model.sendmoney;
            parameters[6].Value = model.ReveInt1;
            parameters[7].Value = model.ReveInt2;
            parameters[8].Value = model.ReveVar1;
            parameters[9].Value = model.ReveVar2;
            parameters[10].Value = model.ReveFloat1;
            parameters[11].Value = model.ReveFloat2;
            parameters[12].Value = model.rId;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>rId</param>
        /// <returns>shopdeliveryInfo</returns>
        public shopdeliveryInfo GetModel(int rId)
        {
            string sql = "select rId,tid,AddTime,distancestart,distanceend,minmoney,sendmoney,ReveInt1,ReveInt2,ReveVar1,ReveVar2,ReveFloat1,ReveFloat2 from shopdelivery where  rId = @rId";
            SqlParameter parameter = new SqlParameter("@rId", SqlDbType.Int, 4);
            parameter.Value = rId;
            shopdeliveryInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new shopdeliveryInfo();
                    model.rId = HJConvert.ToInt32(dr["rId"]);
                    model.tid = HJConvert.ToInt32(dr["tid"]);
                    model.AddTime = HJConvert.ToDateTime(dr["AddTime"]);
                    model.distancestart = HJConvert.ToDecimal(dr["distancestart"]);
                    model.distanceend = HJConvert.ToDecimal(dr["distanceend"]);
                    model.minmoney = HJConvert.ToInt32(dr["minmoney"]);
                    model.sendmoney = HJConvert.ToDecimal(dr["sendmoney"]);
                    model.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    model.ReveInt2 = HJConvert.ToInt32(dr["ReveInt2"]);
                    model.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
                    model.ReveVar2 = HJConvert.ToString(dr["ReveVar2"]);
                    model.ReveFloat1 = HJConvert.ToDecimal(dr["ReveFloat1"]);
                    model.ReveFloat2 = HJConvert.ToDecimal(dr["ReveFloat2"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "shopdelivery"), new SqlParameter("@strWhere", strWhere));
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
        public IList<shopdeliveryInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<shopdeliveryInfo> infos = new List<shopdeliveryInfo>();
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
            parameters[0].Value = "shopdelivery";
            parameters[1].Value = "rId,tid,AddTime,distancestart,distanceend,minmoney,sendmoney,ReveInt1,ReveInt2,ReveVar1,ReveVar2,ReveFloat1,ReveFloat2";
            parameters[2].Value = "rId";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    shopdeliveryInfo info = new shopdeliveryInfo();
                    info.rId = HJConvert.ToInt32(dr["rId"]);
                    info.tid = HJConvert.ToInt32(dr["tid"]);
                    info.AddTime = HJConvert.ToDateTime(dr["AddTime"]);
                    info.distancestart = HJConvert.ToDecimal(dr["distancestart"]);
                    info.distanceend = HJConvert.ToDecimal(dr["distanceend"]);
                    info.minmoney = HJConvert.ToInt32(dr["minmoney"]);
                    info.sendmoney = HJConvert.ToDecimal(dr["sendmoney"]);
                    info.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    info.ReveInt2 = HJConvert.ToInt32(dr["ReveInt2"]);
                    info.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
                    info.ReveVar2 = HJConvert.ToString(dr["ReveVar2"]);
                    info.ReveFloat1 = HJConvert.ToDecimal(dr["ReveFloat1"]);
                    info.ReveFloat2 = HJConvert.ToDecimal(dr["ReveFloat2"]);
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
        public int Delete(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from shopdelivery where rId=@rId");
            SqlParameter[] Para = 
			    {
				    new SqlParameter("@rId",SqlDbType.Int)
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
            str.Append("delete from shopdelivery where rId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
        /// <summary>
        /// 获取某个商家的所有配送距离列表
        /// </summary>
        /// <param name="TogoNum"></param>
        /// <returns></returns>
        public IList<shopdeliveryInfo> GetListByTogoNum(int TogoNum)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select * from shopdelivery where tid=@TogoNum order by distancestart asc");
            SqlParameter[] Para = 
            {
                new SqlParameter("@TogoNum",SqlDbType.Int,4)
            };
            Para[0].Value = TogoNum;

            IList<shopdeliveryInfo> infos = new List<shopdeliveryInfo>();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, str.ToString(), Para))
            {
                while (dr.Read())
                {
                    shopdeliveryInfo info = new shopdeliveryInfo();
                    info.rId = HJConvert.ToInt32(dr["rId"]);
                    info.tid = HJConvert.ToInt32(dr["tid"]);
                    info.AddTime = HJConvert.ToDateTime(dr["AddTime"]);
                    info.distancestart = HJConvert.ToDecimal(dr["distancestart"]);
                    info.distanceend = HJConvert.ToDecimal(dr["distanceend"]);
                    info.minmoney = HJConvert.ToInt32(dr["minmoney"]);
                    info.sendmoney = HJConvert.ToDecimal(dr["sendmoney"]);
                    info.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    info.ReveInt2 = HJConvert.ToInt32(dr["ReveInt2"]);
                    info.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
                    info.ReveVar2 = HJConvert.ToString(dr["ReveVar2"]);
                    info.ReveFloat1 = HJConvert.ToDecimal(dr["ReveFloat1"]);
                    info.ReveFloat2 = HJConvert.ToDecimal(dr["ReveFloat2"]);


                    infos.Add(info);
                }
            }
            return infos;
        }
        /// <summary>
        /// 获取distanceend记录最大的所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>rId</param>
        /// <returns>shopdeliveryInfo</returns>
        public void GetModelbyShopId(int tId)
        {
            SqlParameter[] parameters = 
			{
				new SqlParameter("@tId", SqlDbType.Int,4)
			};
            parameters[0].Value = tId;
            SQLHelper.ExecuteReader(CommandType.StoredProcedure, "shopdelivery_GetMax", parameters);
        }

        /// <summary>
        ///根据条件返回商家所有配送段记录(按距离升序排序)
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public IList<shopdeliveryInfo> GetList(string where)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select * from shopdelivery where " + where + " order by distancestart asc");

            IList<shopdeliveryInfo> infos = new List<shopdeliveryInfo>();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, str.ToString(), null))
            {
                while (dr.Read())
                {
                    shopdeliveryInfo info = new shopdeliveryInfo();
                    info.rId = HJConvert.ToInt32(dr["rId"]);
                    info.tid = HJConvert.ToInt32(dr["tid"]);
                    info.AddTime = HJConvert.ToDateTime(dr["AddTime"]);
                    info.distancestart = HJConvert.ToDecimal(dr["distancestart"]);
                    info.distanceend = HJConvert.ToDecimal(dr["distanceend"]);
                    info.minmoney = HJConvert.ToInt32(dr["minmoney"]);
                    info.sendmoney = HJConvert.ToDecimal(dr["sendmoney"]);
                    info.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    info.ReveInt2 = HJConvert.ToInt32(dr["ReveInt2"]);
                    info.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
                    info.ReveVar2 = HJConvert.ToString(dr["ReveVar2"]);
                    info.ReveFloat1 = HJConvert.ToDecimal(dr["ReveFloat1"]);
                    info.ReveFloat2 = HJConvert.ToDecimal(dr["ReveFloat2"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

    }
}

