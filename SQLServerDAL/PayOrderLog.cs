using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Hangjing.Model;
using Hangjing.DBUtility;

namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 支付日志表
    /// </summary>
    public class PayOrderLog
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PayOrderLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PayOrderLog(");
            strSql.Append("DataId,OrderId,AddTime,Batch,Price,PayType,PayTime,State,PayCallTime,Remark,Reve1,Reve2)");
            strSql.Append(" values (");
            strSql.Append("newid(),@OrderId,@AddTime,@Batch,@Price,@PayType,@PayTime,@State,@PayCallTime,@Remark,@Reve1,@Reve2)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@OrderId", SqlDbType.VarChar,50),
				new SqlParameter("@AddTime", SqlDbType.DateTime),
				new SqlParameter("@Batch", SqlDbType.VarChar,50),
				new SqlParameter("@Price", SqlDbType.Decimal,5),
				new SqlParameter("@PayType", SqlDbType.Int,4),
				new SqlParameter("@PayTime", SqlDbType.DateTime),
				new SqlParameter("@State", SqlDbType.Int,4),
				new SqlParameter("@PayCallTime", SqlDbType.DateTime),
				new SqlParameter("@Remark", SqlDbType.VarChar,256),
				new SqlParameter("@Reve1", SqlDbType.VarChar,256),
				new SqlParameter("@Reve2", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.AddTime;
            parameters[2].Value = model.Batch;
            parameters[3].Value = model.Price;
            parameters[4].Value = model.PayType;
            parameters[5].Value = model.PayTime;
            parameters[6].Value = model.State;
            parameters[7].Value = model.PayCallTime;
            parameters[8].Value = model.Remark;
            parameters[9].Value = model.Reve1;
            parameters[10].Value = model.Reve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(PayOrderLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PayOrderLog set ");
            strSql.Append("OrderId=@OrderId,");
            strSql.Append("AddTime=@AddTime,");
            strSql.Append("Batch=@Batch,");
            strSql.Append("Price=@Price,");
            strSql.Append("PayType=@PayType,");
            strSql.Append("PayTime=@PayTime,");
            strSql.Append("State=@State,");
            strSql.Append("PayCallTime=@PayCallTime,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Reve1=@Reve1,");
            strSql.Append("Reve2=@Reve2");
            strSql.Append(" where DataId=@DataId ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataId", SqlDbType.UniqueIdentifier,16),
				new SqlParameter("@OrderId", SqlDbType.VarChar,50),
				new SqlParameter("@AddTime", SqlDbType.DateTime),
				new SqlParameter("@Batch", SqlDbType.VarChar,50),
				new SqlParameter("@Price", SqlDbType.Decimal,5),
				new SqlParameter("@PayType", SqlDbType.Int,4),
				new SqlParameter("@PayTime", SqlDbType.DateTime),
				new SqlParameter("@State", SqlDbType.Int,4),
				new SqlParameter("@PayCallTime", SqlDbType.DateTime),
				new SqlParameter("@Remark", SqlDbType.VarChar,256),
				new SqlParameter("@Reve1", SqlDbType.VarChar,256),
				new SqlParameter("@Reve2", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.OrderId;
            parameters[2].Value = model.AddTime;
            parameters[3].Value = model.Batch;
            parameters[4].Value = model.Price;
            parameters[5].Value = model.PayType;
            parameters[6].Value = model.PayTime;
            parameters[7].Value = model.State;
            parameters[8].Value = model.PayCallTime;
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.Reve1;
            parameters[11].Value = model.Reve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>PayOrderLogInfo</returns>
        public PayOrderLogInfo GetModel(int DataId)
        {
            string sql = "select DataId,OrderId,AddTime,Batch,Price,PayType,PayTime,State,PayCallTime,Remark,Reve1,Reve2 from PayOrderLog where  DataId = @DataId";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            PayOrderLogInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new PayOrderLogInfo();
                    model.DataId = new Guid(dr["DataId"].ToString());
                    model.OrderId = HJConvert.ToString(dr["OrderId"]);
                    model.AddTime = HJConvert.ToDateTime(dr["AddTime"]);
                    model.Batch = HJConvert.ToString(dr["Batch"]);
                    model.Price = HJConvert.ToDecimal(dr["Price"]);
                    model.PayType = HJConvert.ToInt32(dr["PayType"]);
                    model.PayTime = HJConvert.ToDateTime(dr["PayTime"]);
                    model.State = HJConvert.ToInt32(dr["State"]);
                    model.PayCallTime = HJConvert.ToDateTime(dr["PayCallTime"]);
                    model.Remark = HJConvert.ToString(dr["Remark"]);
                    model.Reve1 = HJConvert.ToString(dr["Reve1"]);
                    model.Reve2 = HJConvert.ToString(dr["Reve2"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "PayOrderLog"), new SqlParameter("@strWhere", strWhere));
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
        public IList<PayOrderLogInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<PayOrderLogInfo> infos = new List<PayOrderLogInfo>();
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
            parameters[0].Value = "PayOrderLog";
            parameters[1].Value = "DataId,OrderId,AddTime,Batch,Price,PayType,PayTime,State,PayCallTime,Remark,Reve1,Reve2";
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
                    PayOrderLogInfo info = new PayOrderLogInfo();
                    info.DataId = new Guid(dr["DataId"].ToString());
                    info.OrderId = HJConvert.ToString(dr["OrderId"]);
                    info.AddTime = HJConvert.ToDateTime(dr["AddTime"]);
                    info.Batch = HJConvert.ToString(dr["Batch"]);
                    info.Price = HJConvert.ToDecimal(dr["Price"]);
                    info.PayType = HJConvert.ToInt32(dr["PayType"]);
                    info.PayTime = HJConvert.ToDateTime(dr["PayTime"]);
                    info.State = HJConvert.ToInt32(dr["State"]);
                    info.PayCallTime = HJConvert.ToDateTime(dr["PayCallTime"]);
                    info.Remark = HJConvert.ToString(dr["Remark"]);
                    info.Reve1 = HJConvert.ToString(dr["Reve1"]);
                    info.Reve2 = HJConvert.ToString(dr["Reve2"]);



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
        public int DelPayOrderLog(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from PayOrderLog where DataId=@DataId");
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
            str.Append("delete from PayOrderLog where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 获取当前的支付流水号（订单编号+001）：订单+(000-999)的
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>PayOrderLogInfo</returns>
        public string GetPayBatch(string orderid)
        {
            string sql = "select top 1 Batch from PayOrderLog where  orderid = @orderid order by AddTime desc";
            SqlParameter parameter = new SqlParameter("@orderid", SqlDbType.VarChar, 50);
            parameter.Value = orderid;

            string PayBatch = "";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    PayBatch = HJConvert.ToString(dr["Batch"]);
                }
            }
            if (PayBatch == "")//没有支付过，
            {
                PayBatch = orderid + "001";
            }
            else//已经支付过
            {
                string temp = PayBatch.Replace(orderid , "");
                //当前支付的序号
                int currentnum = Convert.ToInt32(temp)+1;
                PayBatch = orderid + currentnum.ToString("000");
            }

            return PayBatch;
        }

        /// <summary>
        /// 更新一条数据(支付宝，同步通知修改信息：支付宝交易号),此方法不修改状态，只有异步通知修改接口
        /// </summary>
        public int UpdateByAlipay(PayOrderLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PayOrderLog set ");
            strSql.Append("PayCallTime=@PayCallTime,");
            strSql.Append("Reve2=@Reve2,Remark=@Remark");
            strSql.Append(" where Batch=@Batch ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@Batch", SqlDbType.VarChar,50),
				new SqlParameter("@PayCallTime", SqlDbType.DateTime),
				new SqlParameter("@Remark", SqlDbType.VarChar,256),
				new SqlParameter("@Reve2", SqlDbType.VarChar,256)
            };

            parameters[0].Value = model.Batch;
            parameters[1].Value = model.PayCallTime;
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.Reve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
    }
}
