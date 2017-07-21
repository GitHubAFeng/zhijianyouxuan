using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Hangjing.DBUtility;
using Hangjing.Model;

namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 订单步骤记录表
    /// </summary>
    public partial class OrderStep
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(OrderStepInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OrderStep(");
            strSql.Append("stepcode,orderid,title,subtitle,addtime,deliverid,reveint1,reveint2,revevar1,revevar2,revevar3");
            strSql.Append(") values (");
            strSql.Append("@stepcode,@orderid,@title,@subtitle,@addtime,@deliverid,@reveint1,@reveint2,@revevar1,@revevar2,@revevar3");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters =
            {
               new SqlParameter("@stepcode", SqlDbType.Int,4) ,
               new SqlParameter("@orderid", SqlDbType.VarChar,50) ,
               new SqlParameter("@title", SqlDbType.VarChar,50) ,
               new SqlParameter("@subtitle", SqlDbType.VarChar,256) ,
               new SqlParameter("@addtime", SqlDbType.DateTime) ,
               new SqlParameter("@deliverid", SqlDbType.Int,4) ,
               new SqlParameter("@reveint1", SqlDbType.Int,4) ,
               new SqlParameter("@reveint2", SqlDbType.Int,4) ,
               new SqlParameter("@revevar1", SqlDbType.VarChar,256) ,
               new SqlParameter("@revevar2", SqlDbType.VarChar,256) ,
               new SqlParameter("@revevar3", SqlDbType.VarChar,256)

            };

            parameters[0].Value = model.stepcode;
            parameters[1].Value = model.orderid;
            parameters[2].Value = model.title;
            parameters[3].Value = model.subtitle;
            parameters[4].Value = model.addtime;
            parameters[5].Value = model.deliverid;
            parameters[6].Value = model.reveint1;
            parameters[7].Value = model.reveint2;
            parameters[8].Value = model.revevar1;
            parameters[9].Value = model.revevar2;
            parameters[10].Value = model.revevar3;
            return HJConvert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(OrderStepInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OrderStep set ");
            strSql.Append(" stepcode = @stepcode , ");
            strSql.Append(" orderid = @orderid , ");
            strSql.Append(" title = @title , ");
            strSql.Append(" subtitle = @subtitle , ");
            strSql.Append(" addtime = @addtime , ");
            strSql.Append(" deliverid = @deliverid , ");
            strSql.Append(" reveint1 = @reveint1 , ");
            strSql.Append(" reveint2 = @reveint2 , ");
            strSql.Append(" revevar1 = @revevar1 , ");
            strSql.Append(" revevar2 = @revevar2  ");
            strSql.Append(" where sId=@sId ");

            SqlParameter[] parameters =
            {
                 new SqlParameter("@sId", SqlDbType.Int,4) ,
                 new SqlParameter("@stepcode", SqlDbType.Int,4) ,
                 new SqlParameter("@orderid", SqlDbType.VarChar,50) ,
                 new SqlParameter("@title", SqlDbType.VarChar,50) ,
                 new SqlParameter("@subtitle", SqlDbType.VarChar,256) ,
                 new SqlParameter("@addtime", SqlDbType.DateTime) ,
                 new SqlParameter("@deliverid", SqlDbType.Int,4) ,
                 new SqlParameter("@reveint1", SqlDbType.Int,4) ,
                 new SqlParameter("@reveint2", SqlDbType.Int,4) ,
                 new SqlParameter("@revevar1", SqlDbType.VarChar,256) ,
                 new SqlParameter("@revevar2", SqlDbType.VarChar,256)

            };

            parameters[0].Value = model.sId;
            parameters[1].Value = model.stepcode;
            parameters[2].Value = model.orderid;
            parameters[3].Value = model.title;
            parameters[4].Value = model.subtitle;
            parameters[5].Value = model.addtime;
            parameters[6].Value = model.deliverid;
            parameters[7].Value = model.reveint1;
            parameters[8].Value = model.reveint2;
            parameters[9].Value = model.revevar1;
            parameters[10].Value = model.revevar2;
            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>sId</param>
        /// <returns>OrderStepInfo</returns>
        public OrderStepInfo GetModel(int sId)
        {
            string sql = "select sId,stepcode,orderid,title,subtitle,addtime,deliverid,reveint1,reveint2,revevar1,revevar2 from OrderStep where  sId = @sId";
            SqlParameter parameter = new SqlParameter("@sId", SqlDbType.Int, 4);
            parameter.Value = sId;
            OrderStepInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new OrderStepInfo();
                    model.sId = HJConvert.ToInt32(dr["sId"]);
                    model.stepcode = HJConvert.ToInt32(dr["stepcode"]);
                    model.orderid = HJConvert.ToString(dr["orderid"]);
                    model.title = HJConvert.ToString(dr["title"]);
                    model.subtitle = HJConvert.ToString(dr["subtitle"]);
                    model.addtime = HJConvert.ToDateTime(dr["addtime"]);
                    model.deliverid = HJConvert.ToInt32(dr["deliverid"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "OrderStep"), new SqlParameter("@strWhere", strWhere));
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<OrderStepInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<OrderStepInfo> infos = new List<OrderStepInfo>();
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
            parameters[0].Value = "OrderStep";
            parameters[1].Value = "sId,stepcode,orderid,title,subtitle,addtime,deliverid,reveint1,reveint2,revevar1,revevar2";
            parameters[2].Value = "sId";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    OrderStepInfo model = new OrderStepInfo();
                    model.sId = HJConvert.ToInt32(dr["sId"]);
                    model.stepcode = HJConvert.ToInt32(dr["stepcode"]);
                    model.orderid = HJConvert.ToString(dr["orderid"]);
                    model.title = HJConvert.ToString(dr["title"]);
                    model.subtitle = HJConvert.ToString(dr["subtitle"]);
                    model.addtime = HJConvert.ToDateTime(dr["addtime"]);
                    model.deliverid = HJConvert.ToInt32(dr["deliverid"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    infos.Add(model);
                }
            }
            return infos;
        }

        /// <summary>
        /// 获取订单步骤
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public IList<OrderStepInfo> GetOrderSteps(string orderid)
        {
            IList<OrderStepInfo> infos = new List<OrderStepInfo>();
            SqlParameter[] parameters =
                    {
                new SqlParameter("@orderid", SqlDbType.VarChar,255),
            };
            parameters[0].Value = orderid;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, "SELECT * FROM OrderStep WHERE orderid =@orderid  ORDER BY addtime asc", parameters))
            {
                while (dr.Read())
                {
                    OrderStepInfo model = new OrderStepInfo();
                    model.sId = HJConvert.ToInt32(dr["sId"]);
                    model.stepcode = HJConvert.ToInt32(dr["stepcode"]);
                    model.orderid = HJConvert.ToString(dr["orderid"]);
                    model.title = HJConvert.ToString(dr["title"]);
                    model.subtitle = HJConvert.ToString(dr["subtitle"]);
                    model.addtime = HJConvert.ToDateTime(dr["addtime"]);
                    model.deliverid = HJConvert.ToInt32(dr["deliverid"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    model.revevar3 = HJConvert.ToString(dr["revevar3"]);
                    infos.Add(model);
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
        public int DelOrderStep(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from OrderStep where sId=@sId");
            SqlParameter[] Para =
                {
                new SqlParameter("@sId",SqlDbType.Int)
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
            str.Append("delete from OrderStep where sId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

    }
}

