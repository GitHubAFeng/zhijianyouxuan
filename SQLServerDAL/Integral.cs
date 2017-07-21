/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * Created by wanghui at 2011-5-12 9:03:30.
 * E-Mail   : wanghui@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;


namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 数据访问类Integral。
    /// </summary>
    public class Integral
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(IntegralInfo model)
        {
            StringBuilder strSql = new StringBuilder(); 
            strSql.Append("insert into Integral(");
            strSql.Append("CustId,PayIntegral,DetailId,Cdate,State,GiftsId,Address,Phone,Person,Date,GiftName,UserName,remark)");
            strSql.Append(" values (");
            strSql.Append("@CustId,@PayIntegral,@DetailId,@Cdate,@State,@GiftsId,@Address,@Phone,@Person,@Date,@GiftName,@UserName,@remark)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@CustId", SqlDbType.VarChar,20),
				new SqlParameter("@PayIntegral", SqlDbType.VarChar,20),
				new SqlParameter("@DetailId", SqlDbType.Int,4),
				new SqlParameter("@Cdate", SqlDbType.DateTime),
				new SqlParameter("@State", SqlDbType.VarChar,10),
				new SqlParameter("@GiftsId", SqlDbType.Int,4),
				new SqlParameter("@Address", SqlDbType.VarChar,256),
				new SqlParameter("@Phone", SqlDbType.VarChar,128),
				new SqlParameter("@Person", SqlDbType.VarChar,128),
                new SqlParameter("@Date", SqlDbType.VarChar,128),
                new SqlParameter("@GiftName",SqlDbType.VarChar,128),
                new SqlParameter("@UserName",SqlDbType.VarChar,128),
                new SqlParameter("@remark",SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.CustId;
            parameters[1].Value = model.PayIntegral;
            parameters[2].Value = model.DetailId;
            parameters[3].Value = model.Cdate;
            parameters[4].Value = model.State;
            parameters[5].Value = model.GiftsId;
            parameters[6].Value = model.Address;
            parameters[7].Value = model.Phone;
            parameters[8].Value = model.Person;
            parameters[9].Value = model.Date;
            parameters[10].Value = model.GiftName;
            parameters[11].Value = model.UserName;
            parameters[12].Value = model.Remark;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(IntegralInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Integral set ");
            strSql.Append("CustId=@CustId,");
            strSql.Append("PayIntegral=@PayIntegral,");
            strSql.Append("DetailId=@DetailId,");
            strSql.Append("Cdate=@Cdate,");
            strSql.Append("State=@State,");
            strSql.Append("GiftsId=@GiftsId,");
            strSql.Append("Address=@Address,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("Person=@Person,");
            strSql.Append("Date=@Date,");
            strSql.Append("GiftName=@GiftName,");
            strSql.Append("UserName=@UserName ,remark=@remark ");
            strSql.Append(" where IntegralId=@IntegralId ");
            SqlParameter[] parameters =
            {
				new SqlParameter("@IntegralId", SqlDbType.Int,4),
				new SqlParameter("@CustId", SqlDbType.VarChar,20),
				new SqlParameter("@PayIntegral", SqlDbType.VarChar,20),
				new SqlParameter("@DetailId", SqlDbType.Int,4),
				new SqlParameter("@Cdate", SqlDbType.DateTime),
				new SqlParameter("@State", SqlDbType.VarChar,10),
				new SqlParameter("@GiftsId", SqlDbType.Int,4),
				new SqlParameter("@Address", SqlDbType.VarChar,256),
				new SqlParameter("@Phone", SqlDbType.VarChar,128),
				new SqlParameter("@Person", SqlDbType.VarChar,128),
				new SqlParameter("@Date", SqlDbType.VarChar,128),
                new SqlParameter("@GiftName",SqlDbType.VarChar,128),
                new SqlParameter("@UserName",SqlDbType.VarChar,128),
                new SqlParameter("@remark",SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.IntegralId;
            parameters[1].Value = model.CustId;
            parameters[2].Value = model.PayIntegral;
            parameters[3].Value = model.DetailId;
            parameters[4].Value = model.Cdate;
            parameters[5].Value = model.State;
            parameters[6].Value = model.GiftsId;
            parameters[7].Value = model.Address;
            parameters[8].Value = model.Phone;
            parameters[9].Value = model.Person;
            parameters[10].Value = model.Date;
            parameters[11].Value = model.GiftName;
            parameters[12].Value = model.UserName;
            parameters[13].Value = model.Remark;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>IntegralId</param>
        /// <returns>IntegralInfo</returns>
        public IntegralInfo GetModel(int IntegralId)
        {
            string sql = "select IntegralId,CustId,PayIntegral,DetailId,Cdate,State,GiftsId,Address,Phone,Person,Date,(select Gname from gifts where GiftsId = Integral.GiftsId) as GiftName,UserName,remark from Integral where IntegralId=@IntegralId ";
            SqlParameter parameter = new SqlParameter("@IntegralId", SqlDbType.Int, 4);
            parameter.Value = IntegralId;
            IntegralInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new IntegralInfo();
                    model.IntegralId = HJConvert.ToInt32(dr["IntegralId"]);
                    model.CustId = HJConvert.ToString(dr["CustId"]);
                    model.PayIntegral = HJConvert.ToString(dr["PayIntegral"]);
                    model.DetailId = HJConvert.ToInt32(dr["DetailId"]);
                    model.Cdate = HJConvert.ToDateTime(dr["Cdate"]);
                    model.State = HJConvert.ToString(dr["State"]);
                    model.GiftsId = HJConvert.ToInt32(dr["GiftsId"]);
                    model.Address = HJConvert.ToString(dr["Address"]);
                    model.Phone = HJConvert.ToString(dr["Phone"]);
                    model.Person = HJConvert.ToString(dr["Person"]);
                    model.Date = HJConvert.ToString(dr["Date"]);
                    model.GiftName = HJConvert.ToString(dr["GiftName"]);
                    model.UserName = HJConvert.ToString(dr["UserName"]);
                    model.Remark = HJConvert.ToString(dr["Remark"]);
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
            SqlParameter[] parameters =
            {
                new SqlParameter("@tblName" , SqlDbType.VarChar ,30),
                new SqlParameter("@strWhere" , SqlDbType.VarChar ,50)
            };
            parameters[0].Value = "Integral";
            parameters[1].Value = strWhere;
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", parameters));
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
        public IList<IntegralInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<IntegralInfo> infos = new List<IntegralInfo>();
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
            parameters[0].Value = "Integral";
            string field = "IntegralId,CustId,PayIntegral,DetailId,Cdate,State,GiftsId,Address,Phone,Person,Date,UserName";
            field += ",(select Gname from gifts where GiftsId = Integral.GiftsId) as GiftName";
            parameters[1].Value = field;
            parameters[2].Value = "IntegralId";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    IntegralInfo info = new IntegralInfo();
                    info.IntegralId = HJConvert.ToInt32(dr["IntegralId"]);
                    info.CustId = HJConvert.ToString(dr["CustId"]);
                    info.PayIntegral = HJConvert.ToString(dr["PayIntegral"]);
                    info.DetailId = HJConvert.ToInt32(dr["DetailId"]);
                    info.Cdate = HJConvert.ToDateTime(dr["Cdate"]);
                    info.State = HJConvert.ToString(dr["State"]);
                    info.GiftsId = HJConvert.ToInt32(dr["GiftsId"]);
                    info.Address = HJConvert.ToString(dr["Address"]);
                    info.Phone = HJConvert.ToString(dr["Phone"]);
                    info.Person = HJConvert.ToString(dr["Person"]);
                    info.Date = HJConvert.ToString(dr["Date"]);
                    info.GiftName = HJConvert.ToString(dr["GiftName"]);
                    info.UserName = HJConvert.ToString(dr["UserName"]);
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
        public int DelIntegral(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from Integral where IntegralId=@IntegralId");
            SqlParameter[] Para = 
            {
                new SqlParameter("@IntegralId",SqlDbType.Int)
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
            str.Append("delete from Integral where IntegralId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

    }
}
