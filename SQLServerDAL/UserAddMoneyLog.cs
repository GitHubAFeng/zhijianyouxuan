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

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;

using System.Data.SqlClient;

namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 数据访问类UserAddMoneyLog。
    /// </summary>
    public class UserAddMoneyLog
    {
        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>cid</param>
        /// <returns>usercashoutInfo</returns>
        public CanCashOutInfo GetCanCashOutmoney(int shopid)
        {
            CanCashOutInfo model = new CanCashOutInfo();

            ECustomerInfo shop = new ECustomer().GetModel(shopid);
            model.AllMoney = shop.Usermoney;

            string SqlWhere = "  1=1 and state = 0 and PayType =8 and userid = " + shopid + "";
            model.nousemoney = GetNoUseMoney(SqlWhere);

            model.usemoney = model.AllMoney - model.nousemoney;

            return model;
        }

        /// <summary>
        /// 查询冻结的金额
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public decimal GetNoUseMoney(string where)
        {
            string sql = "select sum(AddMoney) as  OrderTotal ";
            sql += " from UserAddMoneyLog where " + where;

            decimal nousemoney = HJConvert.ToDecimal(SQLHelper.ExecuteScalar(CommandType.Text, sql, null));
            return nousemoney;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.UserAddMoneyLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserAddMoneyLog(");
            strSql.Append("UserId,AddMoney,State,PayType,PayDate,PayState,AddDate,Inve1,Inve2)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@AddMoney,@State,@PayType,@PayDate,@PayState,@AddDate,@Inve1,@Inve2)");
            SqlParameter[] parameters =
            {
				new SqlParameter("@UserId", SqlDbType.Int,4),
				new SqlParameter("@AddMoney", SqlDbType.Decimal,5),
				new SqlParameter("@State", SqlDbType.Int,4),
				new SqlParameter("@PayType", SqlDbType.Int,4),
				new SqlParameter("@PayDate", SqlDbType.DateTime),
				new SqlParameter("@PayState", SqlDbType.Int,4),
				new SqlParameter("@AddDate", SqlDbType.DateTime),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.AddMoney;
            parameters[2].Value = model.State;
            parameters[3].Value = model.PayType;
            parameters[4].Value = model.PayDate;
            parameters[5].Value = model.PayState;
            parameters[6].Value = model.AddDate;
            parameters[7].Value = model.Inve1;
            parameters[8].Value = model.Inve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(UserAddMoneyLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserAddMoneyLog set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("AddMoney=@AddMoney,");
            strSql.Append("State=@State,");
            strSql.Append("PayType=@PayType,");
            strSql.Append("PayDate=@PayDate,");
            strSql.Append("PayState=@PayState,");
            strSql.Append("AddDate=@AddDate,");
            strSql.Append("Inve1=@Inve1,");
            strSql.Append("Inve2=@Inve2");
            strSql.Append(" where DataId=@DataId ");
            SqlParameter[] parameters = 
            {
			    new SqlParameter("@UserId", SqlDbType.Int,4),
			    new SqlParameter("@AddMoney", SqlDbType.Decimal,5),
			    new SqlParameter("@State", SqlDbType.Int,4),
			    new SqlParameter("@PayType", SqlDbType.Int,4),
			    new SqlParameter("@PayDate", SqlDbType.DateTime),
			    new SqlParameter("@PayState", SqlDbType.Int,4),
			    new SqlParameter("@AddDate", SqlDbType.DateTime),
			    new SqlParameter("@Inve1", SqlDbType.Int,4),
			    new SqlParameter("@Inve2", SqlDbType.VarChar,256),
                new SqlParameter("@DataId", SqlDbType.Int,4)
            };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.AddMoney;
            parameters[2].Value = model.State;
            parameters[3].Value = model.PayType;
            parameters[4].Value = model.PayDate;
            parameters[5].Value = model.PayState;
            parameters[6].Value = model.AddDate;
            parameters[7].Value = model.Inve1;
            parameters[8].Value = model.Inve2;
            parameters[9].Value = model.DataId;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>UserAddMoneyLogInfo</returns>
        public UserAddMoneyLogInfo GetModel(int DataId)
        {
            string sql = "select DataId,UserId,AddMoney,State,PayType,PayDate,PayState,AddDate,Inve1,Inve2,(select ecustomer.Name from ecustomer where ecustomer.DataID=UserAddMoneyLog.UserId ) as UserName from UserAddMoneyLog where DataId=@DataId ";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            UserAddMoneyLogInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new UserAddMoneyLogInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.UserId = HJConvert.ToInt32(dr["UserId"]);
                    model.AddMoney = HJConvert.ToDecimal(dr["AddMoney"]);
                    model.State = HJConvert.ToInt32(dr["State"]);
                    model.PayType = HJConvert.ToInt32(dr["PayType"]);
                    model.PayDate = HJConvert.ToDateTime(dr["PayDate"]);
                    model.PayState = HJConvert.ToInt32(dr["PayState"]);
                    model.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.UserName = HJConvert.ToString(dr["UserName"]);
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
                new SqlParameter("@strWhere" , SqlDbType.VarChar ,500)
            };
            parameters[0].Value = "UserAddMoneyLog";
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
        public IList<UserAddMoneyLogInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<UserAddMoneyLogInfo> infos = new List<UserAddMoneyLogInfo>();
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
            parameters[0].Value = "UserAddMoneyLog";
            parameters[1].Value = "DataId,UserId,AddMoney,State,PayType,PayDate,PayState,AddDate,Inve1,Inve2,(select ecustomer.Name from ecustomer where ecustomer.DataID=UserAddMoneyLog.UserId) as UserName,(select eadmin.adminname from eadmin where eadmin.id=UserAddMoneyLog.inve1) AdminName";
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
                    UserAddMoneyLogInfo info = new UserAddMoneyLogInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.UserId = HJConvert.ToInt32(dr["UserId"]);
                    info.AddMoney = HJConvert.ToDecimal(dr["AddMoney"]);
                    info.State = HJConvert.ToInt32(dr["State"]);
                    info.PayType = HJConvert.ToInt32(dr["PayType"]);
                    info.PayDate = HJConvert.ToDateTime(dr["PayDate"]);
                    info.PayState = HJConvert.ToInt32(dr["PayState"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.UserName = HJConvert.ToString(dr["UserName"]);
                    info.AdminName = HJConvert.ToString(dr["AdminName"]);
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
        public int DelUserAddMoneyLog(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from UserAddMoneyLog where DataId=@DataId");
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
            str.Append("delete from UserAddMoneyLog where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdatePayState(UserAddMoneyLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserAddMoneyLog set ");
            strSql.Append("State=@State,");
            strSql.Append("PayDate=@PayDate,");
            strSql.Append("PayState=@PayState ");
            strSql.Append(" where Inve2=@Inve2 ");
            SqlParameter[] parameters = 
            {
			    new SqlParameter("@State", SqlDbType.Int,4),
			    new SqlParameter("@PayDate", SqlDbType.DateTime),
			    new SqlParameter("@PayState", SqlDbType.Int,4)
            };
            parameters[0].Value = model.State;
            parameters[1].Value = model.PayDate;
            parameters[2].Value = model.PayState;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        public UserAddMoneyLogInfo GetModel(string Inve2)
        {
            string sql = "select DataId,UserId,AddMoney,State,PayType,PayDate,PayState,AddDate,Inve1,Inve2 from UserAddMoneyLog where Inve2=@Inve2 ";

            SqlParameter parameter = new SqlParameter("@Inve2", SqlDbType.VarChar,256);

            parameter.Value = Inve2;
            UserAddMoneyLogInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new UserAddMoneyLogInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.UserId = HJConvert.ToInt32(dr["UserId"]);
                    model.AddMoney = HJConvert.ToDecimal(dr["AddMoney"]);
                    model.State = HJConvert.ToInt32(dr["State"]);
                    model.PayType = HJConvert.ToInt32(dr["PayType"]);
                    model.PayDate = HJConvert.ToDateTime(dr["PayDate"]);
                    model.PayState = HJConvert.ToInt32(dr["PayState"]);
                    model.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                }
            }
            return model;
        }


        /// <summary>
        /// 支付宝回调成功
        /// </summary>
        public int CallBlackByAli(UserAddMoneyLogInfo model)
        {
            SqlParameter[] parameters = 
            {
			    new SqlParameter("@rechargorderid", SqlDbType.VarChar,50),
			    new SqlParameter("@addmoney", SqlDbType.Decimal)
            };
            parameters[0].Value = model.Inve2;
            parameters[1].Value = model.AddMoney;

            return SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "RechargCallBlackByAli", parameters);
        }
    }
}
