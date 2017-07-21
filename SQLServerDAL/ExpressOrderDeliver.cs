using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

using Hangjing.DBUtility;//请先添加引用
using Hangjing.Model;

namespace Hangjing.SQLServerDAL
{
	/// <summary>
	/// 跑腿订单调度操作
	/// </summary>
    public class ExpressOrderDeliver
	{
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ExpressOrderDeliverInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ExpressOrderDeliver(");
            strSql.Append("OrderId,DeliverId,DeliverName,Dispatcher,DispatchTime,DeliveryTime,Section,Inve1,Inve2)");
			strSql.Append(" values (");
            strSql.Append("@OrderId,@DeliverId,@DeliverName,@Dispatcher,@DispatchTime,@DeliveryTime,@Section,@Inve1,@Inve2)");
			SqlParameter[] parameters = 
            {
                new SqlParameter("@OrderId", SqlDbType.VarChar,50),
				new SqlParameter("@DeliverId", SqlDbType.Int,4),
				new SqlParameter("@DeliverName", SqlDbType.VarChar,50),
				new SqlParameter("@Dispatcher", SqlDbType.VarChar,50),
				new SqlParameter("@DispatchTime", SqlDbType.DateTime),
				new SqlParameter("@DeliveryTime", SqlDbType.DateTime),
				new SqlParameter("@Section", SqlDbType.VarChar,50),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,50)
            };
            parameters[0].Value = model.Orderid;
			parameters[1].Value = model.DeliverId;
			parameters[2].Value = model.DeliverName;
			parameters[3].Value = model.Dispatcher;
			parameters[4].Value = model.DispatchTime;
			parameters[5].Value = model.DeliveryTime;
			parameters[6].Value = model.Section;
			parameters[7].Value = model.Inve1;
			parameters[8].Value = model.Inve2;

			return Convert.ToInt32(SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters));
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ExpressOrderDeliverInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ExpressOrderDeliver set ");
            strSql.Append("OrderId=@OrderId,");
            strSql.Append("DeliverId=@DeliverId,");
			strSql.Append("DeliverName=@DeliverName,");
			strSql.Append("Dispatcher=@Dispatcher,");
			strSql.Append("DispatchTime=@DispatchTime,");
			strSql.Append("DeliveryTime=@DeliveryTime,");
			strSql.Append("Section=@Section,");
			strSql.Append("Inve1=@Inve1,");
			strSql.Append("Inve2=@Inve2");
			strSql.Append(" where DataId=@DataId ");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@DataId", SqlDbType.Int,4),
                new SqlParameter("@OrderId", SqlDbType.VarChar,50),
				new SqlParameter("@DeliverId", SqlDbType.Int,4),
				new SqlParameter("@DeliverName", SqlDbType.VarChar,50),
				new SqlParameter("@Dispatcher", SqlDbType.VarChar,50),
				new SqlParameter("@DispatchTime", SqlDbType.DateTime),
				new SqlParameter("@DeliveryTime", SqlDbType.DateTime),
				new SqlParameter("@Section", SqlDbType.VarChar,50),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,50)
            };
			parameters[0].Value = model.DataId;
            parameters[1].Value = model.Orderid;
			parameters[2].Value = model.DeliverId;
			parameters[3].Value = model.DeliverName;
			parameters[4].Value = model.Dispatcher;
			parameters[5].Value = model.DispatchTime;
			parameters[6].Value = model.DeliveryTime;
			parameters[7].Value = model.Section;
			parameters[8].Value = model.Inve1;
			parameters[9].Value = model.Inve2;

			 return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}
        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>ExpressOrderDeliverInfo</returns>
        public ExpressOrderDeliverInfo GetModel(int DataId)
        {
            string sql = "select DataId,OrderId,DeliverId,DeliverName,Dispatcher,DispatchTime,DeliveryTime,Section,Inve1,Inve2 from ExpressOrderDeliver where  DataId = @DataId";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            ExpressOrderDeliverInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new ExpressOrderDeliverInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.Orderid = HJConvert.ToString(dr["OrderId"]);
                    model.DeliverId = HJConvert.ToInt32(dr["DeliverId"]);
                    model.DeliverName = HJConvert.ToString(dr["DeliverName"]);
                    model.Dispatcher = HJConvert.ToString(dr["Dispatcher"]);
                    model.DispatchTime = HJConvert.ToDateTime(dr["DispatchTime"]);
                    model.DeliveryTime = HJConvert.ToDateTime(dr["DeliveryTime"]);
                    model.Section = HJConvert.ToString(dr["Section"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "ExpressOrderDeliver"), new SqlParameter("@strWhere", strWhere));
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
        public IList<ExpressOrderDeliverInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ExpressOrderDeliverInfo> infos = new List<ExpressOrderDeliverInfo>();
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
            parameters[0].Value = "ExpressOrderDeliver";
            parameters[1].Value = "*,(select sendmoney from ExpressOrder where DataID = ExpressOrderDeliver.orderid) as sendmoney";
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
                    ExpressOrderDeliverInfo info = new ExpressOrderDeliverInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.Orderid = HJConvert.ToString(dr["OrderId"]);
                    info.DeliverId = HJConvert.ToInt32(dr["DeliverId"]);
                    info.DeliverName = HJConvert.ToString(dr["DeliverName"]);
                    info.Dispatcher = HJConvert.ToString(dr["Dispatcher"]);
                    info.DispatchTime = HJConvert.ToDateTime(dr["DispatchTime"]);
                    info.OverTime = HJConvert.ToDateTime(dr["OverTime"]);

                    info.DeliveryTime = HJConvert.ToDateTime(dr["DeliveryTime"]);
                    info.Section = HJConvert.ToString(dr["Section"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.usesecond = Convert.ToInt32((info.OverTime - info.DispatchTime).TotalMinutes);

                    info.Sendmoney = HJConvert.ToDecimal(dr["Sendmoney"]);

                    if (info.usesecond == 0)
                    {
                        info.SendmoneyTimeRat = "--";
                    }
                    else
                    {
                        info.SendmoneyTimeRat = (info.Sendmoney / info.usesecond).ToString("#0.00");
                    }

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
        public int DelOrderDeliver(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ExpressOrderDeliver where DataId=@DataId");
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
            str.Append("delete from ExpressOrderDeliver where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 修改配送信息
        /// </summary>
        /// <param name="OrderID">订单编号</param>
        /// <param name="DeliveryTime">配送员更新状态的时间</param>
        /// <param name="Interval">配送员预计的配送时间间隔</param>
        public int UpdataOrderDeliver(string OrderID, DateTime DeliveryTime, int Interval)
        {

            StringBuilder str = new StringBuilder();

            str.Append("update ExpressOrderDeliver set DeliveryTime=@DeliveryTime,Inve1=@Inve1 where OrderID=@OrderID");

            SqlParameter[] Para = 
            {
                new SqlParameter("@OrderID",SqlDbType.VarChar,20),
                new SqlParameter("@DeliveryTime",SqlDbType.DateTime),
                new SqlParameter("@Inve1",SqlDbType.Int,4)
            };
            Para[0].Value = OrderID;
            Para[1].Value = DeliveryTime;
            Para[2].Value = Interval;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>ExpressOrderDeliverInfo</returns>
        public ExpressOrderDeliverInfo GetModel(string orderid)
        {
            string sql = "select DataId,OrderId,DeliverId,DeliverName,Dispatcher,DispatchTime,DeliveryTime,Section,Inve1,Inve2 from ExpressOrderDeliver where  OrderId = @DataId";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.VarChar, 50);
            parameter.Value = orderid;
            ExpressOrderDeliverInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new ExpressOrderDeliverInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.Orderid = HJConvert.ToString(dr["OrderId"]);
                    model.DeliverId = HJConvert.ToInt32(dr["DeliverId"]);
                    model.DeliverName = HJConvert.ToString(dr["DeliverName"]);
                    model.Dispatcher = HJConvert.ToString(dr["Dispatcher"]);
                    model.DispatchTime = HJConvert.ToDateTime(dr["DispatchTime"]);
                    model.DeliveryTime = HJConvert.ToDateTime(dr["DeliveryTime"]);
                    model.Section = HJConvert.ToString(dr["Section"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 获取每单平均用时(分)
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int GetAVGusesecond(string where)
        {
            int AVGusesecond = 0;
            string sql = "SELECT avg(datediff(minute, DispatchTime,OverTime)) as usesecond  FROM ExpressOrderDeliver WHERE OverTime IS NOT  NULL and " + where;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (dr.Read())
                {
                    AVGusesecond = HJConvert.ToInt32(dr["usesecond"]);
                }
            }
            return AVGusesecond;
        }
	}
}

