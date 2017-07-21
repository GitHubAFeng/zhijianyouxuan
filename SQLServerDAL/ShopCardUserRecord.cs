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
    /// 优惠券使用记录
    /// </summary>
    public class ShopCardUserRecord
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.ShopCardUserRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ShopCardUserRecord(");
            strSql.Append("cardnum,ckey,AddDate,State,Point,batid,canday,Inve1,Inve2,userid,username,cmoney,ReveInt,ReveVar,usergettime,isbuy,buyuid,buytime)");
            strSql.Append(" values (");
            strSql.Append("@cardnum,@ckey,@AddDate,@State,@Point,@batid,@canday,@Inve1,@Inve2,@userid,@username,@cmoney,@ReveInt,@ReveVar,@usergettime,@isbuy,@buyuid,@buytime)");
            strSql.Append(";select @@IDENTITY");

            SqlParameter[] parameters = 
            {
				new SqlParameter("@cardnum", SqlDbType.VarChar,50),
				new SqlParameter("@ckey", SqlDbType.VarChar,50),
				new SqlParameter("@AddDate", SqlDbType.DateTime),
				new SqlParameter("@State", SqlDbType.Int,4),
				new SqlParameter("@point", SqlDbType.Decimal) ,       
				new SqlParameter("@batid", SqlDbType.Int,4),
				new SqlParameter("@canday", SqlDbType.Int,4),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,256),
				new SqlParameter("@userid", SqlDbType.Int,4),
				new SqlParameter("@username", SqlDbType.VarChar,256),
				new SqlParameter("@cmoney", SqlDbType.Decimal,5),
				new SqlParameter("@ReveInt", SqlDbType.Int,4),
				new SqlParameter("@ReveVar", SqlDbType.VarChar,256),
				new SqlParameter("@usergettime", SqlDbType.DateTime),
				new SqlParameter("@isbuy", SqlDbType.Int,4),
				new SqlParameter("@buyuid", SqlDbType.Int,4),
				new SqlParameter("@buytime", SqlDbType.DateTime)
            };
            parameters[0].Value = model.cardnum;
            parameters[1].Value = model.ckey;
            parameters[2].Value = model.AddDate;
            parameters[3].Value = model.State;
            parameters[4].Value = model.Point;
            parameters[5].Value = model.batid;
            parameters[6].Value = model.canday;
            parameters[7].Value = model.Inve1;
            parameters[8].Value = model.Inve2;
            parameters[9].Value = model.UserID;
            parameters[10].Value = model.username;
            parameters[11].Value = model.cmoney;
            parameters[12].Value = model.ReveInt;
            parameters[13].Value = model.ReveVar;
            parameters[14].Value = model.usergettime;
            parameters[15].Value = model.isbuy;
            parameters[16].Value = model.buyuid;
            parameters[17].Value = model.buytime;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.ShopCardUserRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ShopCardUserRecord set ");
            strSql.Append("cardnum=@cardnum,");
            strSql.Append("ckey=@ckey,");
            strSql.Append("AddDate=@AddDate,");
            strSql.Append("State=@State,");
            strSql.Append("Point=@Point,");
            strSql.Append("batid=@batid,");
            strSql.Append("canday=@canday,");
            strSql.Append("Inve1=@Inve1,");
            strSql.Append("Inve2=@Inve2,");
            strSql.Append("userid=@userid,");
            strSql.Append("username=@username,");
            strSql.Append("cmoney=@cmoney,");
            strSql.Append("ReveInt=@ReveInt,");
            strSql.Append("ReveVar=@ReveVar,");
            strSql.Append("usergettime=@usergettime,");
            strSql.Append("isbuy=@isbuy,");
            strSql.Append("buyuid=@buyuid,");
            strSql.Append("buytime=@buytime");
            strSql.Append(" where CID=@CID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@CID", SqlDbType.Int,4),
				new SqlParameter("@cardnum", SqlDbType.VarChar,50),
				new SqlParameter("@ckey", SqlDbType.VarChar,50),
				new SqlParameter("@AddDate", SqlDbType.DateTime),
				new SqlParameter("@State", SqlDbType.Int,4),
				new SqlParameter("@point", SqlDbType.Decimal) ,       
				new SqlParameter("@batid", SqlDbType.Int,4),
				new SqlParameter("@canday", SqlDbType.Int,4),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,256),
				new SqlParameter("@userid", SqlDbType.Int,4),
				new SqlParameter("@username", SqlDbType.VarChar,256),
				new SqlParameter("@cmoney", SqlDbType.Decimal,5),
				new SqlParameter("@ReveInt", SqlDbType.Int,4),
				new SqlParameter("@ReveVar", SqlDbType.VarChar,256),
				new SqlParameter("@usergettime", SqlDbType.DateTime),
				new SqlParameter("@isbuy", SqlDbType.Int,4),
				new SqlParameter("@buyuid", SqlDbType.Int,4),
				new SqlParameter("@buytime", SqlDbType.DateTime)
            };
            parameters[0].Value = model.CID;
            parameters[1].Value = model.cardnum;
            parameters[2].Value = model.ckey;
            parameters[3].Value = model.AddDate;
            parameters[4].Value = model.State;
            parameters[5].Value = model.Point;
            parameters[6].Value = model.batid;
            parameters[7].Value = model.canday;
            parameters[8].Value = model.Inve1;
            parameters[9].Value = model.Inve2;
            parameters[10].Value = model.UserID;
            parameters[11].Value = model.username;
            parameters[12].Value = model.cmoney;
            parameters[13].Value = model.ReveInt;
            parameters[14].Value = model.ReveVar;
            parameters[15].Value = model.usergettime;
            parameters[16].Value = model.isbuy;
            parameters[17].Value = model.buyuid;
            parameters[18].Value = model.buytime;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>CID</param>
        /// <returns>CardUserRecordInfo</returns>
        public ShopCardUserRecordInfo GetModel(int CID)
        {
            string sql = "select CID,cardnum,ckey,AddDate,State,Point,batid,canday,Inve1,Inve2,userid,username,cmoney,ReveInt,ReveVar,usergettime,isbuy,buyuid,buytime from ShopCardUserRecord where  CID = @CID";
            SqlParameter parameter = new SqlParameter("@CID", SqlDbType.Int, 4);
            parameter.Value = CID;
            ShopCardUserRecordInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new ShopCardUserRecordInfo();
                    model.CID = HJConvert.ToInt32(dr["CID"]);
                    model.cardnum = HJConvert.ToString(dr["cardnum"]);
                    model.ckey = HJConvert.ToString(dr["ckey"]);
                    model.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    model.State = HJConvert.ToInt32(dr["State"]);
                    model.Point = HJConvert.ToDecimal(dr["Point"]);
                    model.batid = HJConvert.ToInt32(dr["batid"]);
                    model.canday = HJConvert.ToInt32(dr["canday"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.UserID = HJConvert.ToInt32(dr["userid"]);
                    model.username = HJConvert.ToString(dr["username"]);
                    model.cmoney = HJConvert.ToDecimal(dr["cmoney"]);
                    model.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    model.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    model.usergettime = HJConvert.ToDateTime(dr["usergettime"]);
                    model.isbuy = HJConvert.ToInt32(dr["isbuy"]);
                    model.buyuid = HJConvert.ToInt32(dr["buyuid"]);
                    model.buytime = HJConvert.ToDateTime(dr["buytime"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "ShopCardUserRecord"), new SqlParameter("@strWhere", strWhere));
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
        public IList<ShopCardUserRecordInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ShopCardUserRecordInfo> infos = new List<ShopCardUserRecordInfo>();
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
            parameters[0].Value = "ShopCardUserRecord";
            parameters[1].Value = "CID,cardnum,ckey,AddDate,State,Point,batid,canday,Inve1,Inve2,userid,username,cmoney,ReveInt,ReveVar,usergettime,isbuy,buyuid,buytime";
            parameters[2].Value = "CID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    ShopCardUserRecordInfo info = new ShopCardUserRecordInfo();
                    info.CID = HJConvert.ToInt32(dr["CID"]);
                    info.cardnum = HJConvert.ToString(dr["cardnum"]);
                    info.ckey = HJConvert.ToString(dr["ckey"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    info.State = HJConvert.ToInt32(dr["State"]);
                    info.Point = HJConvert.ToDecimal(dr["Point"]);
                    info.batid = HJConvert.ToInt32(dr["batid"]);
                    info.canday = HJConvert.ToInt32(dr["canday"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.UserID = HJConvert.ToInt32(dr["userid"]);
                    info.username = HJConvert.ToString(dr["username"]);
                    info.cmoney = HJConvert.ToDecimal(dr["cmoney"]);
                    info.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    info.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    info.usergettime = HJConvert.ToDateTime(dr["usergettime"]);
                    info.isbuy = HJConvert.ToInt32(dr["isbuy"]);
                    info.buyuid = HJConvert.ToInt32(dr["buyuid"]);
                    info.buytime = HJConvert.ToDateTime(dr["buytime"]);
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
        public int DelCardUserRecord(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ShopCardUserRecord where CID=@CID");
            SqlParameter[] Para = 
			{
				new SqlParameter("@CID",SqlDbType.Int)
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
            str.Append("delete from ShopCardUserRecord where CID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}
