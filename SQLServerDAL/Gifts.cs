#region license
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :礼品信息
// Created by tuhui at 2010-6-22 14:58:31.
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
    public class Gifts
    {
        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>GiftsId</param>
        /// <returns>GiftsInfo</returns> 
        public GiftsInfo GetModel(int GiftsId)
        {
            string sql = "select * from Gifts where GiftsId=@GiftsId";
            SqlParameter parameter = new SqlParameter("@GiftsId", SqlDbType.Int, 4);
	        parameter.Value = GiftsId;
	        GiftsInfo model = null;
	        using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
	        {
		        if (dr.Read())
		        {
			        model = new GiftsInfo();
			        model.GiftsId = HJConvert.ToInt32(dr["GiftsId"]);
			        model.ClassId = HJConvert.ToString(dr["ClassId"]);
			        model.Gname = HJConvert.ToString(dr["Gname"]);
			        model.Content = HJConvert.ToString(dr["Content"]);
			        model.NeedIntegral = HJConvert.ToString(dr["NeedIntegral"]);
			        model.GiftsPrice = HJConvert.ToDecimal(dr["GiftsPrice"]);
			        model.Picture = HJConvert.ToString(dr["Picture"]);
                    model.Stocks = HJConvert.ToInt32(dr["stocks"]);
                    model.bigpicture = HJConvert.ToString(dr["bigpicture"]);
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
            return  Convert.ToInt32( SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "Gifts"), new SqlParameter("@strWhere", strWhere)));
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
        public IList<GiftsInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
	        IList<GiftsInfo> infos = new List<GiftsInfo>();
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
	        parameters[0].Value = "Gifts";
            parameters[1].Value = "GiftsId,ClassId,Gname,Content,NeedIntegral,GiftsPrice,Picture,stocks,bigpicture";
	        parameters[2].Value = "GiftsId";
	        parameters[3].Value = orderName;
	        parameters[4].Value = pagesize;
	        parameters[5].Value = pageindex;
	        parameters[6].Value = orderType;
	        parameters[7].Value = strWhere;
	        using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
	        {
		        while (dr.Read())
		        {
			        GiftsInfo info = new GiftsInfo();
			        info.GiftsId = HJConvert.ToInt32(dr["GiftsId"]);
			        info.ClassId = HJConvert.ToString(dr["ClassId"]);
			        info.Gname = HJConvert.ToString(dr["Gname"]);
			        info.Content = HJConvert.ToString(dr["Content"]);
			        info.NeedIntegral = HJConvert.ToString(dr["NeedIntegral"]);
			        info.GiftsPrice = HJConvert.ToDecimal(dr["GiftsPrice"]);
			        info.Picture = HJConvert.ToString(dr["Picture"]);
                    info.Stocks = HJConvert.ToInt32(dr["stocks"]);
                    info.bigpicture = HJConvert.ToString(dr["bigpicture"]);
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
        public int DelGifts(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from Gifts where GiftsId=@GiftsId");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@GiftsId",SqlDbType.Int)
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
            str.Append("delete from Gifts where GiftsId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.GiftsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Gifts(");
            strSql.Append("ClassId,Gname,[Content],NeedIntegral,GiftsPrice,Picture,stocks,bigpicture,sortnum)");
            strSql.Append(" values (");
            strSql.Append("@ClassId,@Gname,@Content,@NeedIntegral,@GiftsPrice,@Picture,@stocks,@bigpicture,@sortnum)");

            SqlParameter[] parameters = 
            {
				new SqlParameter("@ClassId", SqlDbType.VarChar,10),
				new SqlParameter("@Gname", SqlDbType.VarChar,100),
				new SqlParameter("@Content", SqlDbType.Text),
				new SqlParameter("@NeedIntegral", SqlDbType.VarChar,10),
				new SqlParameter("@GiftsPrice", SqlDbType.Decimal,8),
				new SqlParameter("@Picture", SqlDbType.VarChar,200),
                new SqlParameter("@stocks", SqlDbType.Int,4),
                new SqlParameter("@bigpicture", SqlDbType.VarChar,200),
                new SqlParameter("@sortnum", SqlDbType.Int,4)
            };
            parameters[0].Value = model.ClassId;
            parameters[1].Value = model.Gname;
            parameters[2].Value = model.Content;
            parameters[3].Value = model.NeedIntegral;
            parameters[4].Value = model.GiftsPrice;
            parameters[5].Value = model.Picture;
            parameters[6].Value = model.Stocks;
            parameters[7].Value = model.bigpicture;
            parameters[8].Value = model.sortnum;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.GiftsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Gifts set ");
            strSql.Append("ClassId=@ClassId,");
            strSql.Append("Gname=@Gname,");
            strSql.Append("Content=@Content,");
            strSql.Append("NeedIntegral=@NeedIntegral,");
            strSql.Append("GiftsPrice=@GiftsPrice,");
            strSql.Append("Picture=@Picture,");
            strSql.Append("bigpicture=@bigpicture,");
            strSql.Append("sortnum=@sortnum,");
            strSql.Append("Stocks=@Stocks");
            strSql.Append(" where GiftsId=@GiftsId ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@GiftsId", SqlDbType.Int,4),
				new SqlParameter("@ClassId", SqlDbType.VarChar,10),
				new SqlParameter("@Gname", SqlDbType.VarChar,100),
				new SqlParameter("@Content", SqlDbType.Text),
				new SqlParameter("@NeedIntegral", SqlDbType.VarChar,10),
				new SqlParameter("@GiftsPrice", SqlDbType.Decimal,8),
				new SqlParameter("@Picture", SqlDbType.VarChar,200),
                new SqlParameter("@bigpicture", SqlDbType.VarChar,200),
                new SqlParameter("@sortnum", SqlDbType.Int,4),
                 new SqlParameter("@stocks", SqlDbType.Int,4)
            };
            parameters[0].Value = model.GiftsId;
            parameters[1].Value = model.ClassId;
            parameters[2].Value = model.Gname;
            parameters[3].Value = model.Content;
            parameters[4].Value = model.NeedIntegral;
            parameters[5].Value = model.GiftsPrice;
            parameters[6].Value = model.Picture;
            parameters[7].Value = model.bigpicture;
            parameters[8].Value = model.sortnum;
            parameters[9].Value = model.Stocks;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据类型获取礼品编号列表
        /// </summary>
        /// <returns></returns>
        public string GetGiftIdList(int UserId, string State)
        {
            SqlParameter[] Para = 
            {
                new SqlParameter("@CustId",SqlDbType.Int),
                new SqlParameter("@State", SqlDbType.VarChar, 128)
            };
            Para[0].Value = UserId;
            Para[1].Value = State;

            string id_list = "";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "GetGiftIdByUserIdAndState", Para))
            {
                while (dr.Read())
                {
                    id_list += HJConvert.ToString(dr["GiftsId"]);
                    id_list += ",";
                }
            }

            return id_list.Substring(0, id_list.Length - 1);
        }
    }
}
