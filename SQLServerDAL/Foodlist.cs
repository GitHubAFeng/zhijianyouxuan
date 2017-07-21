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
    /// <summary>
    /// 数据访问类Foodlist
    /// </summary>
    public class Foodlist
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(FoodlistInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Foodlist(");
            strSql.Append("InUse,COUnid,FoodUnid,FoodPrice,FCounts,Remark,OldPrice,TogoId,adddate)");
            strSql.Append(" values (");
            strSql.Append("@InUse,@COUnid,@FoodUnid,@FoodPrice,@FCounts,@Remark,@OldPrice,@TogoId,@adddate)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@InUse", SqlDbType.VarChar,10),
				new SqlParameter("@COUnid", SqlDbType.Int,4),
				new SqlParameter("@FoodUnid", SqlDbType.Int,4),
				new SqlParameter("@FoodPrice", SqlDbType.Decimal,9),
				new SqlParameter("@FCounts", SqlDbType.Int,4),
				new SqlParameter("@Remark", SqlDbType.VarChar,50),
				new SqlParameter("@OldPrice", SqlDbType.Decimal,9),
				new SqlParameter("@TogoId", SqlDbType.Int,4),
				new SqlParameter("@adddate", SqlDbType.DateTime)
            };
            parameters[0].Value = model.InUse;
            parameters[1].Value = model.COUnid;
            parameters[2].Value = model.FoodUnid;
            parameters[3].Value = model.FoodPrice;
            parameters[4].Value = model.FCounts;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.OldPrice;
            parameters[7].Value = model.TogoId;
            parameters[8].Value = model.adddate;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(FoodlistInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Foodlist set ");
            strSql.Append("InUse=@InUse,");
            strSql.Append("COUnid=@COUnid,");
            strSql.Append("FoodUnid=@FoodUnid,");
            strSql.Append("FoodPrice=@FoodPrice,");
            strSql.Append("FCounts=@FCounts,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("OldPrice=@OldPrice,");
            strSql.Append("TogoId=@TogoId,");
            strSql.Append("adddate=@adddate");
            strSql.Append(" where Unid=@Unid ");
            SqlParameter[] parameters =
            {
			    new SqlParameter("@Unid", SqlDbType.Int,4),
			    new SqlParameter("@InUse", SqlDbType.VarChar,10),
			    new SqlParameter("@COUnid", SqlDbType.Int,4),
			    new SqlParameter("@FoodUnid", SqlDbType.Int,4),
			    new SqlParameter("@FoodPrice", SqlDbType.Decimal,9),
			    new SqlParameter("@FCounts", SqlDbType.Int,4),
			    new SqlParameter("@Remark", SqlDbType.VarChar,50),
			    new SqlParameter("@OldPrice", SqlDbType.Decimal,9),
			    new SqlParameter("@TogoId", SqlDbType.Int,4),
			    new SqlParameter("@adddate", SqlDbType.DateTime)
            };
            parameters[0].Value = model.Unid;
            parameters[1].Value = model.InUse;
            parameters[2].Value = model.COUnid;
            parameters[3].Value = model.FoodUnid;
            parameters[4].Value = model.FoodPrice;
            parameters[5].Value = model.FCounts;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.OldPrice;
            parameters[8].Value = model.TogoId;
            parameters[9].Value = model.adddate;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>Unid</param>
        /// <returns>FoodlistInfo</returns>
        public FoodlistInfo GetModel(int Unid)
        {
            string sql = "select Unid,InUse,COUnid,FoodUnid,FoodPrice,FCounts,Remark,OldPrice,TogoId,adddate from Foodlist where Unid=@Unid ";
            SqlParameter parameter = new SqlParameter("@Unid", SqlDbType.Int, 4);
            parameter.Value = Unid;
            FoodlistInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new FoodlistInfo();
                    model.Unid = HJConvert.ToInt32(dr["Unid"]);
                    model.InUse = HJConvert.ToString(dr["InUse"]);
                    model.COUnid = HJConvert.ToInt32(dr["COUnid"]);
                    model.FoodUnid = HJConvert.ToInt32(dr["FoodUnid"]);
                    model.FoodPrice = HJConvert.ToDecimal(dr["FoodPrice"]);
                    model.FCounts = HJConvert.ToInt32(dr["FCounts"]);
                    model.Remark = HJConvert.ToString(dr["Remark"]);
                    model.OldPrice = HJConvert.ToDecimal(dr["OldPrice"]);
                    model.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    model.adddate = HJConvert.ToDateTime(dr["adddate"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "Foodlist"), new SqlParameter("@strWhere", strWhere));
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
        public IList<FoodlistInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<FoodlistInfo> infos = new List<FoodlistInfo>();
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
            parameters[0].Value = "Foodlist";
            parameters[1].Value = "Unid,InUse,COUnid,FoodUnid,FoodPrice,FCounts,Remark,OldPrice,TogoId,adddate,fooname";
            parameters[2].Value = "Unid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    FoodlistInfo info = new FoodlistInfo();
                    info.Unid = HJConvert.ToInt32(dr["Unid"]);
                    info.InUse = HJConvert.ToString(dr["InUse"]);
                    info.COUnid = HJConvert.ToInt32(dr["COUnid"]);
                    info.FoodUnid = HJConvert.ToInt32(dr["FoodUnid"]);
                    info.FoodPrice = HJConvert.ToDecimal(dr["FoodPrice"]);
                    info.FCounts = HJConvert.ToInt32(dr["FCounts"]);
                    info.Remark = HJConvert.ToString(dr["Remark"]);
                    info.OldPrice = HJConvert.ToDecimal(dr["OldPrice"]);
                    info.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    info.adddate = HJConvert.ToDateTime(dr["adddate"]);
                    //info.FoodName = HJConvert.ToString(dr["foodname"]);
                    info.FoodName = HJConvert.ToString(dr["fooname"]);
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
        public int DelFoodlist(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from Foodlist where Unid=@Unid");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@Unid",SqlDbType.Int)
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
            str.Append("delete from Foodlist where Unid in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 根据订单编号获取所有餐品信息
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public IList<FoodlistInfo> GetAllByOrderID(string OrderID)
        {
            string sql = "select * from Foodlist where orderid=@OrderID";

            SqlParameter[] Para = 
            {
                new SqlParameter("@OrderID",SqlDbType.VarChar,20)
            };
            Para[0].Value = OrderID;

            IList<FoodlistInfo> DataList = new List<FoodlistInfo>();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, Para))
            {
                while (dr.Read())
                {
                    FoodlistInfo info = new FoodlistInfo();
                    info.Unid = HJConvert.ToInt32(dr["Unid"]);
                    info.InUse = HJConvert.ToString(dr["InUse"]);
                    info.COUnid = HJConvert.ToInt32(dr["COUnid"]);
                    info.FoodUnid = HJConvert.ToInt32(dr["FoodUnid"]);
                    info.FoodPrice = HJConvert.ToDecimal(dr["FoodPrice"]);
                    info.FCounts = HJConvert.ToInt32(dr["FCounts"]);
                    info.Remark = HJConvert.ToString(dr["Remark"]);
                    info.OldPrice = HJConvert.ToDecimal(dr["OldPrice"]);
                    info.addprice = HJConvert.ToDecimal(dr["addprice"]);
                    info.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    info.adddate = HJConvert.ToDateTime(dr["adddate"]);
                    info.FoodName = HJConvert.ToString(dr["fooname"]);
                    info.sid = HJConvert.ToInt32(dr["sid"]);
                    // info.SortName = HJConvert.ToString(dr["SortName"]);
                    DataList.Add(info);
                }
            }
            return DataList;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<FoodlistInfo> GetPageList(int orderId, int pageIndex, int pageSize)
        {
            SqlParameter[] Para = 
            {
                new SqlParameter("@OrderID",SqlDbType.Int,5),
                new SqlParameter("@PageIndex",SqlDbType.Int),
                new SqlParameter("@PageSize",SqlDbType.Int)
            };
            Para[0].Value = orderId;
            Para[1].Value = pageIndex;
            Para[2].Value = pageSize;

            IList<FoodlistInfo> DataList = new List<FoodlistInfo>();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "EOrderFood_GetPageList", Para))
            {
                while (dr.Read())
                {
                    FoodlistInfo info = new FoodlistInfo();
                    info.Unid = HJConvert.ToInt32(dr["Unid"]);
                    info.InUse = HJConvert.ToString(dr["InUse"]);
                    info.COUnid = HJConvert.ToInt32(dr["COUnid"]);
                    info.FoodUnid = HJConvert.ToInt32(dr["FoodUnid"]);
                    info.FoodPrice = HJConvert.ToDecimal(dr["FoodPrice"]);
                    info.FCounts = HJConvert.ToInt32(dr["FCounts"]);
                    info.Remark = HJConvert.ToString(dr["Remark"]);
                    info.OldPrice = HJConvert.ToDecimal(dr["OldPrice"]);
                    info.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    info.adddate = HJConvert.ToDateTime(dr["adddate"]);
                    info.FoodName = HJConvert.ToString(dr["fooname"]);
                    DataList.Add(info);
                }
            }

            return DataList;
        }

        //菜品
        //select  foodlist.FoodUnid,foodname,sum(FCounts) from foodlist inner join foodinfo on foodinfo.Unid = foodlist.FoodUnid where TogoId=1  and  adddate  like '2011-05-30 %'   group by foodlist.FoodUnid 
        public IList<FoodCountInfo> GetFoodCount(string TogoId, string AddDate)
        {
            string sql = "select  foodlist.FoodUnid,foodname,sum(FCounts)  as foodcount  from foodlist inner join foodinfo on foodinfo.Unid = foodlist.FoodUnid where TogoId=" + TogoId + "  and  adddate  like '" + AddDate + " %'   group by foodlist.FoodUnid";

            IList<FoodCountInfo> DataList = new List<FoodCountInfo>();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    FoodCountInfo info = new FoodCountInfo();

                    info.FoodUnid = HJConvert.ToInt32(dr["FoodUnid"]);
                    info.Count = HJConvert.ToInt32(dr["foodcount"]);
                    info.FoodName = HJConvert.ToString(dr["foodname"]);

                    DataList.Add(info);
                }
            }

            //以下代码测试使用
            //FoodCountInfo info = new FoodCountInfo();

            //info.FoodUnid = 1;
            //info.Count = 10;
            //info.FoodName = "餐品名称一";

            //DataList.Add(info);

            //Random rd = new Random();

            //for (int i = 0; i < 25; i++)
            //{
            //    FoodCountInfo info1 = new FoodCountInfo();

            //    info1.FoodUnid = i + 1;
            //    info1.Count = i + rd.Next(500);
            //    info1.FoodName = "餐品名称" + i.ToString() + "";

            //    DataList.Add(info1);
            //}
            //以上代码测试使用

            return DataList;
        }

        /// <summary>
        /// 商品销量Top10
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public IList<FoodlistInfo> FoodSaleTOP10(string orderwhere)
        {
            SqlParameter[] Para = 
            {
                new SqlParameter("@orderwhere",SqlDbType.VarChar,400)
            };
            Para[0].Value = orderwhere;

            IList<FoodlistInfo> DataList = new List<FoodlistInfo>();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "FoodSaleTOP10", Para))
            {
                while (dr.Read())
                {
                    FoodlistInfo info = new FoodlistInfo();

                    info.FoodUnid = HJConvert.ToInt32(dr["FoodUnid"]);
                    info.FoodPrice = HJConvert.ToDecimal(dr["FoodPrice"]);
                    info.FCounts = HJConvert.ToInt32(dr["salecount"]);
                    info.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    info.FoodName = HJConvert.ToString(dr["fooname"]);
                    info.Remark = HJConvert.ToString(dr["shopname"]);
                    DataList.Add(info);
                }
            }
            return DataList;
        }
    }
}
