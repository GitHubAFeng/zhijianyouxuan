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
    /// 数据访问类UserDelMoneyLog。
    /// </summary>
    public class UserDelMoneyLog
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(UserDelMoneyLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserDelMoneyLog(");
            strSql.Append("DataId,UserId,DelMoney,AddDate,BuyItem,Inve1,Inve2)");
            strSql.Append(" values (");
            strSql.Append("@DataId,@UserId,@DelMoney,@AddDate,@BuyItem,@Inve1,@Inve2)");
            SqlParameter[] parameters = 
            {
			    new SqlParameter("@DataId", SqlDbType.Int,4),
			    new SqlParameter("@UserId", SqlDbType.Int,4),
			    new SqlParameter("@DelMoney", SqlDbType.Decimal,5),
			    new SqlParameter("@AddDate", SqlDbType.DateTime),
			    new SqlParameter("@BuyItem", SqlDbType.VarChar,50),
			    new SqlParameter("@Inve1", SqlDbType.Int,4),
			    new SqlParameter("@Inve2", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.DelMoney;
            parameters[3].Value = model.AddDate;
            parameters[4].Value = model.BuyItem;
            parameters[5].Value = model.Inve1;
            parameters[6].Value = model.Inve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(UserDelMoneyLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserDelMoneyLog set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("DelMoney=@DelMoney,");
            strSql.Append("AddDate=@AddDate,");
            strSql.Append("BuyItem=@BuyItem,");
            strSql.Append("Inve1=@Inve1,");
            strSql.Append("Inve2=@Inve2");
            strSql.Append(" where DataId=@DataId ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataId", SqlDbType.Int,4),
				new SqlParameter("@UserId", SqlDbType.Int,4),
				new SqlParameter("@DelMoney", SqlDbType.Decimal,5),
				new SqlParameter("@AddDate", SqlDbType.DateTime),
				new SqlParameter("@BuyItem", SqlDbType.VarChar,50),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.DelMoney;
            parameters[3].Value = model.AddDate;
            parameters[4].Value = model.BuyItem;
            parameters[5].Value = model.Inve1;
            parameters[6].Value = model.Inve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>UserDelMoneyLogInfo</returns>
        public UserDelMoneyLogInfo GetModel(int DataId)
        {
            string sql = "select DataId,UserId,DelMoney,AddDate,BuyItem,Inve1,Inve2,(select ecustomer.Name from ecustomer where ecustomer.DataID=UserDelMoneyLog.UserId ) as UserName from UserDelMoneyLog where UserDelMoneyLog.DataId=@DataId ";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            UserDelMoneyLogInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new UserDelMoneyLogInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.UserId = HJConvert.ToInt32(dr["UserId"]);
                    model.DelMoney = HJConvert.ToDecimal(dr["DelMoney"]);
                    model.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    model.BuyItem = HJConvert.ToString(dr["BuyItem"]);
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
            parameters[0].Value = "UserDelMoneyLog";
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
        public IList<UserDelMoneyLogInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<UserDelMoneyLogInfo> infos = new List<UserDelMoneyLogInfo>();
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
            parameters[0].Value = "UserDelMoneyLog";
            parameters[1].Value = "DataId,UserId,DelMoney,AddDate,BuyItem,Inve1,Inve2,(select ecustomer.Name from ecustomer where ecustomer.DataID=UserDelMoneyLog.UserId ) as UserName";
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
                    UserDelMoneyLogInfo info = new UserDelMoneyLogInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.UserId = HJConvert.ToInt32(dr["UserId"]);
                    info.DelMoney = HJConvert.ToDecimal(dr["DelMoney"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    info.BuyItem = HJConvert.ToString(dr["BuyItem"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.TogoName = HJConvert.ToString(dr["UserName"]);
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
        public int DelUserDelMoneyLog(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from UserDelMoneyLog where DataId=@DataId");
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
            str.Append("delete from UserDelMoneyLog where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}
