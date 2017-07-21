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
    /// 订单打印记录表，用于查询状态
    /// </summary>
    public partial class printorderlog
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(printorderlogInfo model)
        {
            SqlParameter[] parameters = 
			{
			    new SqlParameter("@orderid", SqlDbType.VarChar,256) ,            
                new SqlParameter("@memberCode", SqlDbType.VarChar,256) ,            
                new SqlParameter("@securityKey", SqlDbType.VarChar,256) ,                     
            };
            parameters[0].Value = model.orderid;
            parameters[1].Value = model.memberCode;
            parameters[2].Value = model.securityKey;

            SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "printorderlog_add", parameters);
            return 1;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(printorderlogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update printorderlog set ");
            strSql.Append(" orderid = @orderid , ");
            strSql.Append(" memberCode = @memberCode , ");
            strSql.Append(" securityKey = @securityKey , ");
            strSql.Append(" printstate = @printstate , ");
            strSql.Append(" addtime = @addtime , ");
            strSql.Append(" updatetime = @updatetime , ");
            strSql.Append(" reveint1 = @reveint1 , ");
            strSql.Append(" reveint2 = @reveint2 , ");
            strSql.Append(" revevar1 = @revevar1 , ");
            strSql.Append(" revevar2 = @revevar2  ");
            strSql.Append(" where pid=@pid ");

            SqlParameter[] parameters = 
			{
			    new SqlParameter("@pid", SqlDbType.Int,4) ,            
                new SqlParameter("@orderid", SqlDbType.VarChar,256) ,            
                new SqlParameter("@memberCode", SqlDbType.VarChar,256) ,            
                new SqlParameter("@securityKey", SqlDbType.VarChar,256) ,            
                new SqlParameter("@printstate", SqlDbType.Int,4) ,            
                new SqlParameter("@addtime", SqlDbType.DateTime) ,            
                new SqlParameter("@updatetime", SqlDbType.DateTime) ,            
                new SqlParameter("@reveint1", SqlDbType.Int,4) ,            
                new SqlParameter("@reveint2", SqlDbType.Int,4) ,            
                new SqlParameter("@revevar1", SqlDbType.VarChar,256) ,            
                new SqlParameter("@revevar2", SqlDbType.VarChar,256)             
              
            };

            parameters[0].Value = model.pid;
            parameters[1].Value = model.orderid;
            parameters[2].Value = model.memberCode;
            parameters[3].Value = model.securityKey;
            parameters[4].Value = model.printstate;
            parameters[5].Value = model.addtime;
            parameters[6].Value = model.updatetime;
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
        /// <param>pid</param>
        /// <returns>printorderlogInfo</returns>
        public printorderlogInfo GetModel(int pid)
        {
            string sql = "select pid,orderid,memberCode,securityKey,printstate,addtime,updatetime,reveint1,reveint2,revevar1,revevar2 from printorderlog where  pid = @pid";
            SqlParameter parameter = new SqlParameter("@pid", SqlDbType.Int, 4);
            parameter.Value = pid;
            printorderlogInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new printorderlogInfo();
                    model.pid = HJConvert.ToInt32(dr["pid"]);
                    model.orderid = HJConvert.ToString(dr["orderid"]);
                    model.memberCode = HJConvert.ToString(dr["memberCode"]);
                    model.securityKey = HJConvert.ToString(dr["securityKey"]);
                    model.printstate = HJConvert.ToInt32(dr["printstate"]);
                    model.addtime = HJConvert.ToDateTime(dr["addtime"]);
                    model.updatetime = HJConvert.ToDateTime(dr["updatetime"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "printorderlog"), new SqlParameter("@strWhere", strWhere));
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
        public IList<printorderlogInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<printorderlogInfo> infos = new List<printorderlogInfo>();
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
            parameters[0].Value = "printorderlog";
            parameters[1].Value = "pid,orderid,memberCode,securityKey,printstate,addtime,updatetime,reveint1,reveint2,revevar1,revevar2";
            parameters[2].Value = "pid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    printorderlogInfo info = new printorderlogInfo();
                    info.pid = HJConvert.ToInt32(dr["pid"]);
                    info.orderid = HJConvert.ToString(dr["orderid"]);
                    info.memberCode = HJConvert.ToString(dr["memberCode"]);
                    info.securityKey = HJConvert.ToString(dr["securityKey"]);
                    info.printstate = HJConvert.ToInt32(dr["printstate"]);
                    info.addtime = HJConvert.ToDateTime(dr["addtime"]);
                    info.updatetime = HJConvert.ToDateTime(dr["updatetime"]);
                    info.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    info.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    info.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    info.revevar2 = HJConvert.ToString(dr["revevar2"]);
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
        public int Delprintorderlog(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from printorderlog where pid=@pid");
            SqlParameter[] Para = 
			{
				new SqlParameter("@pid",SqlDbType.Int)
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
            str.Append("delete from printorderlog where pid in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }




    }
}

