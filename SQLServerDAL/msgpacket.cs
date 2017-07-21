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
    /// 用户红包表
    /// </summary>
	public partial class msgpacket
	{

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(msgpacketInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into msgpacket(");
			strSql.Append("pid,alltotal,num,eachmoney,validitytime,moneyline,cmoney,starttime,endtime,ReveInt,ReveInt1,ReveVar,ReveVar1,datetime1,datetime2)");
			strSql.Append(" values (");
			strSql.Append("@pid,@alltotal,@num,@eachmoney,@validitytime,@moneyline,@cmoney,@starttime,@endtime,@ReveInt,@ReveInt1,@ReveVar,@ReveVar1,@datetime1,@datetime2)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = 
            {
                new SqlParameter("@pid", SqlDbType.VarChar,50),
				new SqlParameter("@alltotal", SqlDbType.Decimal,9),
				new SqlParameter("@num", SqlDbType.Int,4),
				new SqlParameter("@eachmoney", SqlDbType.Decimal,9),
				new SqlParameter("@validitytime", SqlDbType.DateTime),
				new SqlParameter("@moneyline", SqlDbType.Decimal,9),
				new SqlParameter("@cmoney", SqlDbType.Decimal,9),
				new SqlParameter("@starttime", SqlDbType.DateTime),
				new SqlParameter("@endtime", SqlDbType.DateTime),
				new SqlParameter("@ReveInt", SqlDbType.Int,4),
				new SqlParameter("@ReveInt1", SqlDbType.Int,4),
				new SqlParameter("@ReveVar", SqlDbType.VarChar,50),
				new SqlParameter("@ReveVar1", SqlDbType.VarChar,50),
				new SqlParameter("@datetime1", SqlDbType.DateTime),
				new SqlParameter("@datetime2", SqlDbType.DateTime)
            };
            parameters[0].Value = model.pid;
			parameters[1].Value = model.alltotal;
			parameters[2].Value = model.num;
			parameters[3].Value = model.eachmoney;
			parameters[4].Value = model.validitytime;
			parameters[5].Value = model.moneyline;
			parameters[6].Value = model.cmoney;
			parameters[7].Value = model.starttime;
			parameters[8].Value = model.endtime;
			parameters[9].Value = model.ReveInt;
			parameters[10].Value = model.ReveInt1;
			parameters[11].Value = model.ReveVar;
			parameters[12].Value = model.ReveVar1;
			parameters[13].Value = model.datetime1;
			parameters[14].Value = model.datetime2;

			return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(msgpacketInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update msgpacket set ");
			strSql.Append("alltotal=@alltotal,");
			strSql.Append("num=@num,");
			strSql.Append("eachmoney=@eachmoney,");
			strSql.Append("validitytime=@validitytime,");
			strSql.Append("moneyline=@moneyline,");
			strSql.Append("cmoney=@cmoney,");
			strSql.Append("starttime=@starttime,");
			strSql.Append("endtime=@endtime,");
			strSql.Append("ReveInt=@ReveInt,");
			strSql.Append("ReveInt1=@ReveInt1,");
			strSql.Append("ReveVar=@ReveVar,");
			strSql.Append("ReveVar1=@ReveVar1,");
			strSql.Append("datetime1=@datetime1,");
			strSql.Append("datetime2=@datetime2,");
            strSql.Append("pid=@pid");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@alltotal", SqlDbType.Decimal,9),
				new SqlParameter("@num", SqlDbType.Int,4),
				new SqlParameter("@eachmoney", SqlDbType.Decimal,9),
				new SqlParameter("@validitytime", SqlDbType.DateTime),
				new SqlParameter("@moneyline", SqlDbType.Decimal,9),
				new SqlParameter("@cmoney", SqlDbType.Decimal,9),
				new SqlParameter("@starttime", SqlDbType.DateTime),
				new SqlParameter("@endtime", SqlDbType.DateTime),
				new SqlParameter("@ReveInt", SqlDbType.Int,4),
				new SqlParameter("@ReveInt1", SqlDbType.Int,4),
				new SqlParameter("@ReveVar", SqlDbType.VarChar,50),
				new SqlParameter("@ReveVar1", SqlDbType.VarChar,50),
				new SqlParameter("@datetime1", SqlDbType.DateTime),
				new SqlParameter("@datetime2", SqlDbType.DateTime),
				new SqlParameter("@pid", SqlDbType.VarChar,50)
            };
			parameters[0].Value = model.alltotal;
			parameters[1].Value = model.num;
			parameters[2].Value = model.eachmoney;
			parameters[3].Value = model.validitytime;
			parameters[4].Value = model.moneyline;
			parameters[5].Value = model.cmoney;
			parameters[6].Value = model.starttime;
			parameters[7].Value = model.endtime;
			parameters[8].Value = model.ReveInt;
			parameters[9].Value = model.ReveInt1;
			parameters[10].Value = model.ReveVar;
			parameters[11].Value = model.ReveVar1;
			parameters[12].Value = model.datetime1;
			parameters[13].Value = model.datetime2;
			parameters[14].Value = model.pid;

			return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from msgpacket ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>IntegralId</param>
        /// <returns>IntegralInfo</returns>
        public msgpacketInfo GetModel(int Id)
        {
            string sql = "select id,pid,alltotal,num,eachmoney,validitytime,moneyline,cmoney,starttime,endtime,ReveInt,ReveInt1,ReveVar,ReveVar1,datetime1,datetime2 from msgpacket where id=@id";
            SqlParameter parameter = new SqlParameter("@Id", SqlDbType.Int, 4);
            parameter.Value = Id;
            msgpacketInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new msgpacketInfo();
                    model.id = HJConvert.ToInt32(dr["id"]);
                    model.pid = HJConvert.ToString(dr["pid"]);
                    model.alltotal = HJConvert.ToDecimal(dr["alltotal"]);
                    model.num = HJConvert.ToInt32(dr["num"]);
                    model.eachmoney = HJConvert.ToDecimal(dr["eachmoney"]);
                    model.validitytime = HJConvert.ToDateTime(dr["validitytime"]);
                    model.moneyline = HJConvert.ToDecimal(dr["moneyline"]);
                    model.cmoney = HJConvert.ToDecimal(dr["cmoney"]);
                    model.starttime = HJConvert.ToDateTime(dr["starttime"]);
                    model.endtime = HJConvert.ToDateTime(dr["endtime"]);
                    model.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    model.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    model.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    model.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
                    model.datetime1 = HJConvert.ToDateTime(dr["datetime1"]);
                    model.datetime2 = HJConvert.ToDateTime(dr["datetime2"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "msgpacket"), new SqlParameter("@strWhere", strWhere));
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
        public IList<msgpacketInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<msgpacketInfo> infos = new List<msgpacketInfo>();
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
            parameters[0].Value = "msgpacket";
            string field = "id,pid,alltotal,num,eachmoney,validitytime,moneyline,cmoney,starttime,endtime,ReveInt,ReveInt1,ReveVar,ReveVar1,datetime1,datetime2";
            parameters[1].Value = field;
            parameters[2].Value = "id";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    msgpacketInfo model = new msgpacketInfo();
                    model.id = HJConvert.ToInt32(dr["id"]);
                    model.pid = HJConvert.ToString(dr["pid"]);
                    model.alltotal = HJConvert.ToDecimal(dr["alltotal"]);
                    model.num = HJConvert.ToInt32(dr["num"]);
                    model.eachmoney = HJConvert.ToDecimal(dr["eachmoney"]);
                    model.validitytime = HJConvert.ToDateTime(dr["validitytime"]);
                    model.moneyline = HJConvert.ToDecimal(dr["moneyline"]);
                    model.cmoney = HJConvert.ToDecimal(dr["cmoney"]);
                    model.starttime = HJConvert.ToDateTime(dr["starttime"]);
                    model.endtime = HJConvert.ToDateTime(dr["endtime"]);
                    model.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    model.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    model.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    model.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
                    model.datetime1 = HJConvert.ToDateTime(dr["datetime1"]);
                    model.datetime2 = HJConvert.ToDateTime(dr["datetime2"]);
                    infos.Add(model);
                }
            }
            return infos;
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
            str.Append("delete from msgpacket where id in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
	}
}

