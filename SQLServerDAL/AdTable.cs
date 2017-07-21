// AdInfoInfo.css:广告图.
// CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-05-12

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hangjing.DBUtility;
using Hangjing.Model;
using System.Data;
using System.Data.SqlClient;

namespace Hangjing.SQLServerDAL
{
    public class AdTable 
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.AdTableInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AdTable(");
            strSql.Append("AdType,AdName,AdPage,AdHeight,AdWidth,AdImageAdrees,AdAdrees,DayMode,DayMoney,UserID,MID,AdStartDate,AdAddDate)");
            strSql.Append(" values (");
            strSql.Append("@AdType,@AdName,@AdPage,@AdHeight,@AdWidth,@AdImageAdrees,@AdAdrees,@DayMode,@DayMoney,@UserID,@MID,@AdStartDate,@AdAddDate)");
            SqlParameter[] parameters = 
            {
			    new SqlParameter("@AdType", SqlDbType.VarChar,20),
				new SqlParameter("@AdName", SqlDbType.VarChar,50),
				new SqlParameter("@AdPage", SqlDbType.VarChar,30),
				new SqlParameter("@AdHeight", SqlDbType.Int,4),
				new SqlParameter("@AdWidth", SqlDbType.Int,4),
				new SqlParameter("@AdImageAdrees", SqlDbType.VarChar,200),
				new SqlParameter("@AdAdrees", SqlDbType.VarChar,200),
				new SqlParameter("@DayMode", SqlDbType.Int,4),
				new SqlParameter("@DayMoney", SqlDbType.Decimal,8),
				new SqlParameter("@UserID", SqlDbType.Int,4),
				new SqlParameter("@MID", SqlDbType.Int,4),
				new SqlParameter("@AdStartDate", SqlDbType.DateTime),
				new SqlParameter("@AdAddDate", SqlDbType.DateTime)
              };
            parameters[0].Value = model.AdType;
            parameters[1].Value = model.AdName;
            parameters[2].Value = model.AdPage;
            parameters[3].Value = model.AdHeight;
            parameters[4].Value = model.AdWidth;
            parameters[5].Value = model.AdImageAdrees;
            parameters[6].Value = model.AdAdrees;
            parameters[7].Value = model.DayMode;
            parameters[8].Value = model.DayMoney;
            parameters[9].Value = model.UserID;
            parameters[10].Value = model.MID;
            parameters[11].Value = model.AdStartDate;
            parameters[12].Value = model.AdAddDate;

            return Convert.ToInt32(SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.AdTableInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AdTable set ");
            strSql.Append("AdType=@AdType,");
            strSql.Append("AdName=@AdName,");
            strSql.Append("AdPage=@AdPage,");
            strSql.Append("AdHeight=@AdHeight,");
            strSql.Append("AdWidth=@AdWidth,");
            strSql.Append("AdImageAdrees=@AdImageAdrees,");
            strSql.Append("AdAdrees=@AdAdrees,");
            strSql.Append("DayMode=@DayMode,");
            strSql.Append("DayMoney=@DayMoney,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("MID=@MID,");
            strSql.Append("AdStartDate=@AdStartDate,");
            strSql.Append("AdAddDate=@AdAddDate");
            strSql.Append(" where TID=@TID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@TID", SqlDbType.Int,4),
				new SqlParameter("@AdType", SqlDbType.VarChar,20),
				new SqlParameter("@AdName", SqlDbType.VarChar,50),
				new SqlParameter("@AdPage", SqlDbType.VarChar,30),
				new SqlParameter("@AdHeight", SqlDbType.Int,4),
				new SqlParameter("@AdWidth", SqlDbType.Int,4),
				new SqlParameter("@AdImageAdrees", SqlDbType.VarChar,200),
				new SqlParameter("@AdAdrees", SqlDbType.VarChar,200),
				new SqlParameter("@DayMode", SqlDbType.Int,4),
				new SqlParameter("@DayMoney", SqlDbType.Decimal,8),
				new SqlParameter("@UserID", SqlDbType.Int,4),
				new SqlParameter("@MID", SqlDbType.Int,4),
				new SqlParameter("@AdStartDate", SqlDbType.DateTime),
				new SqlParameter("@AdAddDate", SqlDbType.DateTime)
            };
            parameters[0].Value = model.TID;
            parameters[1].Value = model.AdType;
            parameters[2].Value = model.AdName;
            parameters[3].Value = model.AdPage;
            parameters[4].Value = model.AdHeight;
            parameters[5].Value = model.AdWidth;
            parameters[6].Value = model.AdImageAdrees;
            parameters[7].Value = model.AdAdrees;
            parameters[8].Value = model.DayMode;
            parameters[9].Value = model.DayMoney;
            parameters[10].Value = model.UserID;
            parameters[11].Value = model.MID;
            parameters[12].Value = model.AdStartDate;
            parameters[13].Value = model.AdAddDate;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>TID</param>
        /// <returns>AdTableInfo</returns>
        public AdTableInfo GetModel(int TID)
        {
            string sql = "select * from AdTable where  TID = @TID";
            SqlParameter parameter = new SqlParameter("@TID", SqlDbType.Int, 4);
            parameter.Value = TID;
            AdTableInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new AdTableInfo();
                    model.TID = HJConvert.ToInt32(dr["TID"]);
                    model.AdType = HJConvert.ToString(dr["AdType"]);
                    model.AdName = HJConvert.ToString(dr["AdName"]);
                    model.AdPage = HJConvert.ToString(dr["AdPage"]);
                    model.AdHeight = HJConvert.ToInt32(dr["AdHeight"]);
                    model.AdWidth = HJConvert.ToInt32(dr["AdWidth"]);
                    model.AdImageAdrees = HJConvert.ToString(dr["AdImageAdrees"]);
                    model.AdAdrees = HJConvert.ToString(dr["AdAdrees"]);
                    model.DayMode = HJConvert.ToInt32(dr["DayMode"]);
                    model.DayMoney = HJConvert.ToInt32(dr["DayMoney"]);
                    model.UserID = HJConvert.ToInt32(dr["UserID"]);
                    model.MID = HJConvert.ToInt32(dr["MID"]);
                    model.AdStartDate = HJConvert.ToDateTime(dr["AdStartDate"]);
                    model.AdAddDate = HJConvert.ToDateTime(dr["AdAddDate"]);

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
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "AdTable"), new SqlParameter("@strWhere", strWhere)));
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
        public IList<AdTableInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<AdTableInfo> infos = new List<AdTableInfo>();
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
            parameters[0].Value = "AdTable";
            parameters[1].Value = "TID,AdType,AdName,AdPage,AdHeight,AdWidth,AdImageAdrees,AdAdrees,DayMode,DayMoney,UserID,MID,AdStartDate,AdAddDate";
            parameters[2].Value = "TID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    AdTableInfo info = new AdTableInfo();
                    info.TID = HJConvert.ToInt32(dr["TID"]);
                    info.AdType = HJConvert.ToString(dr["AdType"]);
                    info.AdName = HJConvert.ToString(dr["AdName"]);
                    info.AdPage = HJConvert.ToString(dr["AdPage"]);
                    info.AdHeight = HJConvert.ToInt32(dr["AdHeight"]);
                    info.AdWidth = HJConvert.ToInt32(dr["AdWidth"]);
                    info.AdImageAdrees = HJConvert.ToString(dr["AdImageAdrees"]);
                    info.AdAdrees = HJConvert.ToString(dr["AdAdrees"]);
                    info.DayMode = HJConvert.ToInt32(dr["DayMode"]);
                    info.DayMoney = HJConvert.ToInt32(dr["DayMoney"]);
                    info.UserID = HJConvert.ToInt32(dr["UserID"]);
                    info.MID = HJConvert.ToInt32(dr["MID"]);
                    info.AdStartDate = HJConvert.ToDateTime(dr["AdStartDate"]);
                    info.AdAddDate = HJConvert.ToDateTime(dr["AdAddDate"]);
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
        public int DelAdTable(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from AdTable where TID=@TID");
            SqlParameter[] Para = 
            {
                new SqlParameter("@TID",SqlDbType.Int)
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
            str.Append("delete from AdTable where TID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}