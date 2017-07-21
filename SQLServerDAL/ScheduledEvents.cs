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
    public class ScheduledEvents
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.ScheduledEventsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ScheduledEvents(");
            strSql.Append("key,lastexecuted,servername)");
            strSql.Append(" values (");
            strSql.Append("@key,@lastexecuted,@servername)");
            SqlParameter[] parameters =
            {
				new SqlParameter("@key", SqlDbType.VarChar,50),
				new SqlParameter("@lastexecuted", SqlDbType.DateTime),
				new SqlParameter("@servername", SqlDbType.VarChar,100)
            };
            parameters[0].Value = model.key;
            parameters[1].Value = model.lastexecuted;
            parameters[2].Value = model.servername;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.ScheduledEventsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ScheduledEvents set ");
            strSql.Append("key=@key,");
            strSql.Append("lastexecuted=@lastexecuted,");
            strSql.Append("servername=@servername");
            strSql.Append(" where scheduleID=@scheduleID ");
            SqlParameter[] parameters =
            {
				new SqlParameter("@scheduleID", SqlDbType.Int,4),
				new SqlParameter("@key", SqlDbType.VarChar,50),
				new SqlParameter("@lastexecuted", SqlDbType.DateTime),
				new SqlParameter("@servername", SqlDbType.VarChar,100)
            };
            parameters[0].Value = model.scheduleID;
            parameters[1].Value = model.key;
            parameters[2].Value = model.lastexecuted;
            parameters[3].Value = model.servername;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>scheduleID</param>
        /// <returns>ScheduledEventsInfo</returns>
        public ScheduledEventsInfo GetModel(int scheduleID)
        {
            string sql = "select scheduleID,key,lastexecuted,servername from ScheduledEvents where  scheduleID = @scheduleID";
            SqlParameter parameter = new SqlParameter("@scheduleID", SqlDbType.Int, 4);
            parameter.Value = scheduleID;
            ScheduledEventsInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new ScheduledEventsInfo();
                    model.scheduleID = HJConvert.ToInt32(dr["scheduleID"]);
                    model.key = HJConvert.ToString(dr["key"]);
                    model.lastexecuted = HJConvert.ToDateTime(dr["lastexecuted"]);
                    model.servername = HJConvert.ToString(dr["servername"]);
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
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "ScheduledEvents"), new SqlParameter("@strWhere", strWhere)));
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
        public IList<ScheduledEventsInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ScheduledEventsInfo> infos = new List<ScheduledEventsInfo>();
            SqlParameter[] parameters = 
			{
				new SqlParameter("@tblName", SqlDbType.VarChar,255),
				new SqlParameter("@strGetFields", SqlDbType.VarChar,1000),
				new SqlParameter("@primaryKey", SqlDbType.VarChar,255),
				new SqlParameter("@orderName", SqlDbType.VarChar,255),
				new SqlParameter("@PageSize", SqlDbType.Int),
				new SqlParameter("@PageIndex", SqlDbType.Int),
				new SqlParameter("@OrderType", SqlDbType.Bit),
				new SqlParameter("@strWhere", SqlDbType.VarChar,1500)
			};
            parameters[0].Value = "ScheduledEvents";
            parameters[1].Value = "scheduleID,key,lastexecuted,servername";
            parameters[2].Value = "scheduleID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    ScheduledEventsInfo info = new ScheduledEventsInfo();
                    info.scheduleID = HJConvert.ToInt32(dr["scheduleID"]);
                    info.key = HJConvert.ToString(dr["key"]);
                    info.lastexecuted = HJConvert.ToDateTime(dr["lastexecuted"]);
                    info.servername = HJConvert.ToString(dr["servername"]);
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
        public int DelScheduledEvents(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ScheduledEvents where scheduleID=@scheduleID");
            SqlParameter[] Para = 
			{
				new SqlParameter("@scheduleID",SqlDbType.Int)
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
            str.Append("delete from ScheduledEvents where scheduleID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 设置任务最后执行时间
        /// </summary>
        /// <param name="@"></param>
        /// <returns></returns>
        public int SetLastScheduled(ScheduledEventsInfo info)
        {

            SqlParameter[] parameters = 
		    {
				new SqlParameter("@key", SqlDbType.VarChar,100),
				new SqlParameter("@lastexecuted", SqlDbType.DateTime),
				new SqlParameter("@servername", SqlDbType.VarChar,100)
			};
            parameters[0].Value = info.key;
            parameters[1].Value = info.lastexecuted;
            parameters[2].Value = info.servername;

            return SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "setlastexecutescheduledeventdatetime", parameters);
        }

        /// <summary>
        /// 清除一段时间之前的执行记录 以免数据库记录不断增长 到时性能的下降 因为此类数据保留无任何意义
        /// </summary>
        /// <param name="@"></param>
        /// <returns></returns>
        public int ClearLastScheduled(string Key)
        {
            SqlParameter[] parameters = 
		    {
				new SqlParameter("@key", SqlDbType.VarChar,100)
			};
            parameters[0].Value = Key;

            return SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "clearoldexecutescheduledevent", parameters);
        }

        /// <summary>
        /// 获得最近一次执行的时间
        /// </summary>
        /// <param name="@"></param>
        /// <returns></returns>
        public DateTime GetLastScheduled(ScheduledEventsInfo info)
        {
            SqlParameter[] parameters = 
		    {
				new SqlParameter("@key", SqlDbType.VarChar,100),
				new SqlParameter("@servername", SqlDbType.VarChar,100),
                new SqlParameter("@lastexecuted",SqlDbType.DateTime)
			};
            parameters[0].Value = info.key;
            parameters[1].Value = info.servername;
            parameters[2].Direction = ParameterDirection.Output;

            DateTime dt = DateTime.Now.AddDays(-1);

            SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "getlastexecutescheduledeventdatetime", parameters);

            dt = HJConvert.ToDateTime(parameters[2].Value);
            //AppLog.Debug("+++" + dt.ToString());

            return dt;
        }
    }
}
