// ETogoCollect.cs:餐馆/餐品收藏
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-04-01
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
    public class ETogoCollect
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddFood(ETogoFoodCollectInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ETogoFoodCollect(");
            strSql.Append("foodid,togoid,userid,ctime,inve1,inve2)");
            strSql.Append(" values (");
            strSql.Append("@foodid,@togoid,@userid,@ctime,@inve1,@inve2)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@foodid", SqlDbType.Int,4),
				new SqlParameter("@togoid", SqlDbType.Int,4),
				new SqlParameter("@userid", SqlDbType.Int,4),
				new SqlParameter("@ctime", SqlDbType.DateTime),
				new SqlParameter("@inve1", SqlDbType.Int,4),
				new SqlParameter("@inve2", SqlDbType.VarChar,50)
            };
            parameters[0].Value = model.foodid;
            parameters[1].Value = model.togoid;
            parameters[2].Value = model.userid;
            parameters[3].Value = model.ctime;
            parameters[4].Value = model.inve1;
            parameters[5].Value = model.inve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddTogo(ETogoCollectInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ETogoCollect(");
            strSql.Append("togoid,userid,ctime,inve1,inve2)");
            strSql.Append(" values (");
            strSql.Append("@togoid,@userid,@ctime,@inve1,@inve2)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@togoid", SqlDbType.Int,4),
				new SqlParameter("@userid", SqlDbType.Int,4),
				new SqlParameter("@ctime", SqlDbType.DateTime),
				new SqlParameter("@inve1", SqlDbType.Int,4),
				new SqlParameter("@inve2", SqlDbType.VarChar,50)
            };
            parameters[0].Value = model.togoid;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.ctime;
            parameters[3].Value = model.inve1;
            parameters[4].Value = model.inve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>dataid</param>
        /// <returns>ETogoFoodCollectInfo</returns>
        public ETogoFoodCollectInfo GetFoodModel(int dataid)
        {
            string sql = "select dataid,foodid,togoid,userid,ctime,inve1,inve2 , (select name from points where unid = ETogoFoodCollect.togoid) as togoname from ETogoFoodCollect";
            SqlParameter parameter = new SqlParameter("@dataid", SqlDbType.Int, 4);
            parameter.Value = dataid;
            ETogoFoodCollectInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new ETogoFoodCollectInfo();
                    model.dataid = HJConvert.ToInt32(dr["dataid"]);
                    model.foodid = HJConvert.ToInt32(dr["foodid"]);
                    model.togoid = HJConvert.ToInt32(dr["togoid"]);
                    model.userid = HJConvert.ToInt32(dr["userid"]);
                    model.ctime = HJConvert.ToDateTime(dr["ctime"]);
                    model.inve1 = HJConvert.ToInt32(dr["inve1"]);
                    model.inve2 = HJConvert.ToString(dr["inve2"]);
                    model.togoname = HJConvert.ToString(dr["togoname"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>dataid</param>
        /// <returns>ETogoFoodCollectInfo</returns>
        public ETogoCollectInfo GetTogoModel(int dataid)
        {
            string sql = "select dataid,togoid,userid,ctime,inve1,inve2 ,(select name from points where unid = ETogoCollect.togoid ) as togoname from ETogoCollect";
            SqlParameter parameter = new SqlParameter("@dataid", SqlDbType.Int, 4);
            parameter.Value = dataid;
            ETogoCollectInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new ETogoCollectInfo();
                    model.dataid = HJConvert.ToInt32(dr["dataid"]);
                    model.togoid = HJConvert.ToInt32(dr["togoid"]);
                    model.userid = HJConvert.ToInt32(dr["userid"]);
                    model.ctime = HJConvert.ToDateTime(dr["ctime"]);
                    model.inve1 = HJConvert.ToInt32(dr["inve1"]);
                    model.inve2 = HJConvert.ToString(dr["inve2"]);
                    model.Togoname = HJConvert.ToString(dr["togoaname"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 获得总记录数
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int GetFoodCount(string strWhere)
        {
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "ETogoFoodCollect"), new SqlParameter("@strWhere", strWhere)));
        }

        /// <summary>
        /// 获得总记录数
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int GetTogoCount(string strWhere)
        {
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "ETogoCollect"), new SqlParameter("@strWhere", strWhere)));
        }

        /// <summary>
        /// 获取收藏的餐品列表
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<ETogoFoodCollectInfo> GetFoodList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ETogoFoodCollectInfo> infos = new List<ETogoFoodCollectInfo>();
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
            parameters[0].Value = "ETogofoodCollect";
            parameters[1].Value = "etogofoodcollect.* ,(select name from point where unid=ETogofoodCollect.togoid) as togoname,(select name from efood where efood.foodid=ETogofoodCollect.foodid ) as foodname , (select price from efood where foodid =etogofoodcollect.foodid ) as foodprice";
            parameters[2].Value = "dataid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;          
            parameters[7].Value = strWhere;
            
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    ETogoFoodCollectInfo info = new ETogoFoodCollectInfo();
                    info.dataid = HJConvert.ToInt32(dr["dataid"]);
                    info.foodid = HJConvert.ToInt32(dr["foodid"]);
                    info.togoid = HJConvert.ToInt32(dr["togoid"]);
                    info.userid = HJConvert.ToInt32(dr["userid"]);
                    info.ctime = HJConvert.ToDateTime(dr["ctime"]);
                    info.inve1 = HJConvert.ToInt32(dr["inve1"]);
                    info.inve2 = HJConvert.ToString(dr["inve2"]);
                    info.togoname = HJConvert.ToString(dr["togoname"]);
                    info.foodname = HJConvert.ToString(dr["foodname"]);
                    info.Foodprice = HJConvert.ToDecimal(dr["foodprice"]);

                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 获取收藏的餐品列表
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<ETogoCollectInfo> GetTogoList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ETogoCollectInfo> infos = new List<ETogoCollectInfo>();
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
            parameters[0].Value = "ETogoCollect";
            parameters[1].Value = "dataid,togoid,userid,ctime,inve1,inve2,(select name from points where unid = ETogoCollect.togoid ) as togoname";
            parameters[2].Value = "dataid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    ETogoCollectInfo info = new ETogoCollectInfo();
                    info.dataid = HJConvert.ToInt32(dr["dataid"]);
                    info.togoid = HJConvert.ToInt32(dr["togoid"]);
                    info.userid = HJConvert.ToInt32(dr["userid"]);
                    info.ctime = HJConvert.ToDateTime(dr["ctime"]);
                    info.inve1 = HJConvert.ToInt32(dr["inve1"]);
                    info.inve2 = HJConvert.ToString(dr["inve2"]);
                    info.Togoname = HJConvert.ToString(dr["togoname"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 删除收藏餐品记录
        /// </summary>
        /// <param name="Id"></param>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <returns></returns>
        public int DelEFoodCollect(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ETogoFoodCollect where dataid=@dataid");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@dataid",SqlDbType.Int)
	        };
            Para[0].Value = Id;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 删除收藏餐馆记录
        /// </summary>
        /// <param name="Id"></param>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <returns></returns>
        public int DelETogoCollect(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ETogoCollect where dataid=@dataid");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@dataid",SqlDbType.Int)
	        };
            Para[0].Value = Id;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 删除收藏餐馆记录(根据用户和商家编号)
        /// </summary>
        /// <param name="Id"></param>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <returns></returns>
        public int DelETogoCollect(int userid, int togoid)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ETogoCollect where userid=@userid and togoid=@togoid");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@userid",SqlDbType.Int),
                new SqlParameter("@togoid",SqlDbType.Int)

	        };
            Para[0].Value = userid;
            Para[1].Value = togoid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 清空餐品收藏
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int ClearFood(int UserId)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ETogoFoodCollect where userid = @UserId");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@UserId",SqlDbType.Int)
	        };
            Para[0].Value = UserId;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 清空餐馆收藏
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int ClearTogo(int UserId)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ETogoCollect where userid = @UserId");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@UserId",SqlDbType.Int)
	        };
            Para[0].Value = UserId;

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
            str.Append("delete from ETogoCollect where dataid in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 批量删除餐品收藏记录
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int DelFoodList(string IdList)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ETogoFoodCollect where dataid in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>dataid</param>
        /// <returns>ETogoFoodCollectInfo</returns>
        public int TogoCollect(int userid, int togoid, int bid)
        {
            SqlParameter[] parameters = 
		    {
				new SqlParameter("@userid", SqlDbType.Int),
				new SqlParameter("@togoid", SqlDbType.Int),
                new SqlParameter("@bid", SqlDbType.Int),
                new SqlParameter("@msg", SqlDbType.Int),
			};
            parameters[0].Value = userid;
            parameters[1].Value = togoid;
            parameters[2].Value = bid;
            parameters[3].Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "collect_msg", parameters);
            return Convert.ToInt32(parameters[3].Value);
        }
    }
}
