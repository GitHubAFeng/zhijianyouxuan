#region license
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :产品属性表
// Created by wanghui at 2010-6-22 10:23:25.
// E-Mail: wanghui@ihangjing.com
#endregion
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;

using Hangjing.Model;
using Hangjing.DBUtility;
using Hangjing.Common;

namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 数据访问类FoodStyle。
    /// </summary>
    public class FoodStyle 
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(FoodStyleInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ProductStyle(");
            strSql.Append("FoodtId,Title,Price,MarkeyPrice,InUser,SaleSum,MaxPerDay,Intro,Inve1,Inve2)");
            strSql.Append(" values (");
            strSql.Append("@FoodtId,@Title,@Price,@MarkeyPrice,@InUser,@SaleSum,@MaxPerDay,@Intro,@Inve1,@Inve2)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@FoodtId", SqlDbType.Int,4),
				new SqlParameter("@Title", SqlDbType.VarChar,50),
				new SqlParameter("@Price", SqlDbType.Decimal,5),
				new SqlParameter("@MarkeyPrice", SqlDbType.Decimal,5),
				new SqlParameter("@InUser", SqlDbType.Int,4),
				new SqlParameter("@SaleSum", SqlDbType.Int,4),
				new SqlParameter("@MaxPerDay", SqlDbType.Int,4),
				new SqlParameter("@Intro", SqlDbType.VarChar,256),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,50)
            };
            parameters[0].Value = model.FoodtId;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Price;
            parameters[3].Value = model.MarkeyPrice;
            parameters[4].Value = model.InUser;
            parameters[5].Value = model.SaleSum;
            parameters[6].Value = model.MaxPerDay;
            parameters[7].Value = model.Intro;
            parameters[8].Value = model.Inve1;
            parameters[9].Value = model.Inve2;

            return HJConvert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(FoodStyleInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ProductStyle set ");
            strSql.Append("FoodtId=@FoodtId,");
            strSql.Append("Title=@Title,");
            strSql.Append("Price=@Price,");
            strSql.Append("MarkeyPrice=@MarkeyPrice,");
            strSql.Append("InUser=@InUser,");
            strSql.Append("SaleSum=@SaleSum,");
            strSql.Append("MaxPerDay=@MaxPerDay,");
            strSql.Append("Intro=@Intro,");
            strSql.Append("Inve1=@Inve1,");
            strSql.Append("Inve2=@Inve2");
            strSql.Append(" where DataId=@DataId ");
            SqlParameter[] parameters = 
            {

				new SqlParameter("@FoodtId", SqlDbType.Int,4),
				new SqlParameter("@Title", SqlDbType.VarChar,50),
				new SqlParameter("@Price", SqlDbType.Decimal,5),
				new SqlParameter("@MarkeyPrice", SqlDbType.Decimal,5),
				new SqlParameter("@InUser", SqlDbType.Int,4),
				new SqlParameter("@SaleSum", SqlDbType.Int,4),
				new SqlParameter("@MaxPerDay", SqlDbType.Int,4),
				new SqlParameter("@Intro", SqlDbType.VarChar,256),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,50),
                new SqlParameter("@DataId", SqlDbType.Int,4)
            };
            parameters[0].Value = model.FoodtId;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Price;
            parameters[3].Value = model.MarkeyPrice;
            parameters[4].Value = model.InUser;
            parameters[5].Value = model.SaleSum;
            parameters[6].Value = model.MaxPerDay;
            parameters[7].Value = model.Intro;
            parameters[8].Value = model.Inve1;
            parameters[9].Value = model.Inve2;
            parameters[10].Value = model.DataId;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>ProductStyleInfo</returns>
        public FoodStyleInfo GetModel(int DataId)
        {
            string sql = "select DataId,FoodtId,Title,Price,MarkeyPrice,InUser,SaleSum,MaxPerDay,Intro,Inve1,Inve2,(select FoodName from Foodinfo where Unid = ProductStyle.FoodtId ) as Name from ProductStyle where ProductStyle.DataId=@DataId ";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            FoodStyleInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new FoodStyleInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.FoodtId = HJConvert.ToInt32(dr["FoodtId"]);
                    model.Title = HJConvert.ToString(dr["Title"]);
                    model.Price = HJConvert.ToDecimal(dr["Price"]);
                    model.MarkeyPrice = HJConvert.ToDecimal(dr["MarkeyPrice"]);
                    model.InUser = HJConvert.ToInt32(dr["InUser"]);
                    model.SaleSum = HJConvert.ToInt32(dr["SaleSum"]);
                    model.MaxPerDay = HJConvert.ToInt32(dr["MaxPerDay"]);
                    model.Intro = HJConvert.ToString(dr["Intro"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
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
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "ProductStyle"), new SqlParameter("@strWhere", strWhere)));
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagesize">頁尺寸</param>
        /// <param name="pageindex">頁索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<FoodStyleInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<FoodStyleInfo> infos = new List<FoodStyleInfo>();
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
            parameters[0].Value = "ProductStyle";
            parameters[1].Value = "DataId,FoodtId,Title,Price,MarkeyPrice,InUser,SaleSum,MaxPerDay,Intro,Inve1,Inve2,(select FoodName from Foodinfo where Unid = ProductStyle.FoodtId ) as Name";
            parameters[2].Value = "DataId";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    FoodStyleInfo info = new FoodStyleInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.FoodtId = HJConvert.ToInt32(dr["FoodtId"]);
                    info.Title = HJConvert.ToString(dr["Title"]);
                    info.Price = HJConvert.ToDecimal(dr["Price"]);
                    info.MarkeyPrice = HJConvert.ToDecimal(dr["MarkeyPrice"]);
                    info.InUser = HJConvert.ToInt32(dr["InUser"]);
                    info.SaleSum = HJConvert.ToInt32(dr["SaleSum"]);
                    info.MaxPerDay = HJConvert.ToInt32(dr["MaxPerDay"]);
                    info.Intro = HJConvert.ToString(dr["Intro"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.Name = HJConvert.ToString(dr["Name"]);
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
        public int DelProductStyle(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ProductStyle where DataId=@DataId");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@DataId",SqlDbType.Int)
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
            str.Append("delete from ProductStyle where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 根据商店编号获取所有
        /// </summary>
        /// <param name="togoid">商店编号</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<FoodStyleInfo> GetAllByTid( int togoid)
        {
            IList<FoodStyleInfo> infos = new List<FoodStyleInfo>();
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@tid",SqlDbType.Int)
	        };
            Para[0].Value = togoid;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "FoodSytle_GetAllBYTid", Para))
            {
                while (dr.Read())
                {
                    FoodStyleInfo info = new FoodStyleInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.FoodtId = HJConvert.ToInt32(dr["FoodtId"]);
                    info.Title = HJConvert.ToString(dr["Title"]);
                    info.Price = HJConvert.ToDecimal(dr["Price"]);
                    info.MarkeyPrice = HJConvert.ToDecimal(dr["MarkeyPrice"]);
                    info.InUser = HJConvert.ToInt32(dr["InUser"]);
                    info.SaleSum = HJConvert.ToInt32(dr["SaleSum"]);
                    info.MaxPerDay = HJConvert.ToInt32(dr["MaxPerDay"]);
                    info.Intro = HJConvert.ToString(dr["Intro"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    infos.Add(info);
                }
            }
            return infos;
        }
    }
}
