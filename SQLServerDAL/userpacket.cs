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
    /// 红包批次表(用于分享的红包)
    /// </summary>
    public partial class userpacket
	{
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(userpacketInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into userpacket(");
			strSql.Append("pid,userid,exptime,pulltime,money,moneyline,state,pulltel,reveint,reveint1,revevar,revevar1,datetime1,datetime2)");
			strSql.Append(" values (");
			strSql.Append("@pid,@userid,@exptime,@pulltime,@money,@moneyline,@state,@pulltel,@reveint,@reveint1,@revevar,@revevar1,@datetime1,@datetime2)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@pid", SqlDbType.VarChar,256),
				new SqlParameter("@userid", SqlDbType.Int,4),
				new SqlParameter("@exptime", SqlDbType.DateTime),
				new SqlParameter("@pulltime", SqlDbType.DateTime),
				new SqlParameter("@money", SqlDbType.Decimal,9),
				new SqlParameter("@moneyline", SqlDbType.Decimal,9),
				new SqlParameter("@state", SqlDbType.Int,4),
				new SqlParameter("@pulltel", SqlDbType.VarChar,50),
				new SqlParameter("@reveint", SqlDbType.Int,4),
				new SqlParameter("@reveint1", SqlDbType.Int,4),
				new SqlParameter("@revevar", SqlDbType.VarChar,50),
				new SqlParameter("@revevar1", SqlDbType.VarChar,50),
				new SqlParameter("@datetime1", SqlDbType.DateTime),
				new SqlParameter("@datetime2", SqlDbType.DateTime)
            };
			parameters[0].Value = model.pid;
			parameters[1].Value = model.userid;
			parameters[2].Value = model.exptime;
			parameters[3].Value = model.pulltime;
			parameters[4].Value = model.money;
			parameters[5].Value = model.moneyline;
			parameters[6].Value = model.state;
			parameters[7].Value = model.pulltel;
			parameters[8].Value = model.reveint;
			parameters[9].Value = model.reveint1;
			parameters[10].Value = model.revevar;
			parameters[11].Value = model.revevar1;
			parameters[12].Value = model.datetime1;
			parameters[13].Value = model.datetime2;

			return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(userpacketInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update userpacket set ");
			strSql.Append("pid=@pid,");
			strSql.Append("userid=@userid,");
			strSql.Append("exptime=@exptime,");
			strSql.Append("pulltime=@pulltime,");
			strSql.Append("money=@money,");
			strSql.Append("moneyline=@moneyline,");
			strSql.Append("state=@state,");
			strSql.Append("pulltel=@pulltel,");
			strSql.Append("reveint=@reveint,");
			strSql.Append("reveint1=@reveint1,");
			strSql.Append("revevar=@revevar,");
			strSql.Append("revevar1=@revevar1,");
			strSql.Append("datetime1=@datetime1,");
			strSql.Append("datetime2=@datetime2");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@pid", SqlDbType.VarChar,256),
				new SqlParameter("@userid", SqlDbType.Int,4),
				new SqlParameter("@exptime", SqlDbType.DateTime),
				new SqlParameter("@pulltime", SqlDbType.DateTime),
				new SqlParameter("@money", SqlDbType.Decimal,9),
				new SqlParameter("@moneyline", SqlDbType.Decimal,9),
				new SqlParameter("@state", SqlDbType.Int,4),
				new SqlParameter("@pulltel", SqlDbType.VarChar,50),
				new SqlParameter("@reveint", SqlDbType.Int,4),
				new SqlParameter("@reveint1", SqlDbType.Int,4),
				new SqlParameter("@revevar", SqlDbType.VarChar,50),
				new SqlParameter("@revevar1", SqlDbType.VarChar,50),
				new SqlParameter("@datetime1", SqlDbType.DateTime),
				new SqlParameter("@datetime2", SqlDbType.DateTime),
				new SqlParameter("@id", SqlDbType.Int,4)
            };
			parameters[0].Value = model.pid;
			parameters[1].Value = model.userid;
			parameters[2].Value = model.exptime;
			parameters[3].Value = model.pulltime;
			parameters[4].Value = model.money;
			parameters[5].Value = model.moneyline;
			parameters[6].Value = model.state;
			parameters[7].Value = model.pulltel;
			parameters[8].Value = model.reveint;
			parameters[9].Value = model.reveint1;
			parameters[10].Value = model.revevar;
			parameters[11].Value = model.revevar1;
			parameters[12].Value = model.datetime1;
			parameters[13].Value = model.datetime2;
			parameters[14].Value = model.id;

			return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from userpacket ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public int DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from userpacket ");
			strSql.Append(" where id in ("+idlist + ")  ");

			return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(strSql.ToString(), idlist), null);
		}
        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>IntegralId</param>
        /// <returns>IntegralInfo</returns>
        public userpacketInfo GetModel(int Id)
        {
            string sql = "select  id,pid,userid,exptime,pulltime,money,moneyline,state,pulltel,reveint,reveint1,revevar,revevar1,datetime1,datetime2 from userpacket where id=@id ";
            SqlParameter parameter = new SqlParameter("@Id", SqlDbType.Int, 4);
            parameter.Value = Id;
            userpacketInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new userpacketInfo();
                    model.id = HJConvert.ToInt32(dr["id"]);
                    model.pid = HJConvert.ToString(dr["pid"]);
                    model.userid = HJConvert.ToInt32(dr["userid"]);
                    model.exptime = HJConvert.ToDateTime(dr["exptime"]);
                    model.pulltime = HJConvert.ToDateTime(dr["pulltime"]);
                    model.money = HJConvert.ToDecimal(dr["money"]);
                    model.moneyline = HJConvert.ToDecimal(dr["moneyline"]);
                    model.state = HJConvert.ToInt32(dr["state"]);
                    model.pulltel = HJConvert.ToString(dr["pulltel"]);
                    model.reveint = HJConvert.ToInt32(dr["reveint"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.revevar = HJConvert.ToString(dr["revevar"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.datetime1 = HJConvert.ToDateTime(dr["datetime1"]);
                    model.datetime2 = HJConvert.ToDateTime(dr["datetime2"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>IntegralId</param>
        /// <returns>IntegralInfo</returns>
        public userpacketInfo GetModel(string Id)
        {
            string sql = "select  id,pid,userid,exptime,pulltime,money,moneyline,state,pulltel,reveint,reveint1,revevar,revevar1,datetime1,datetime2 from userpacket where revevar=@id ";
            SqlParameter parameter = new SqlParameter("@Id", SqlDbType.VarChar, 256);
            parameter.Value = Id;
            userpacketInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new userpacketInfo();
                    model.id = HJConvert.ToInt32(dr["id"]);
                    model.pid = HJConvert.ToString(dr["pid"]);
                    model.userid = HJConvert.ToInt32(dr["userid"]);
                    model.exptime = HJConvert.ToDateTime(dr["exptime"]);
                    model.pulltime = HJConvert.ToDateTime(dr["pulltime"]);
                    model.money = HJConvert.ToDecimal(dr["money"]);
                    model.moneyline = HJConvert.ToDecimal(dr["moneyline"]);
                    model.state = HJConvert.ToInt32(dr["state"]);
                    model.pulltel = HJConvert.ToString(dr["pulltel"]);
                    model.reveint = HJConvert.ToInt32(dr["reveint"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.revevar = HJConvert.ToString(dr["revevar"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
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
            SqlParameter[] parameters =
            {
                new SqlParameter("@tblName" , SqlDbType.VarChar ,30),
                new SqlParameter("@strWhere" , SqlDbType.VarChar ,256)
            };
            parameters[0].Value = "userpacket";
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
        public IList<userpacketInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<userpacketInfo> infos = new List<userpacketInfo>();
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
            parameters[0].Value = "userpacket";
            string field = "id,pid,userid,exptime,pulltime,money,moneyline,state,pulltel,reveint,reveint1,revevar,revevar1,datetime1,datetime2";
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
                    userpacketInfo model = new userpacketInfo();
                    model.id = HJConvert.ToInt32(dr["id"]);
                    model.pid = HJConvert.ToString(dr["pid"]);
                    model.userid = HJConvert.ToInt32(dr["userid"]);
                    model.exptime = HJConvert.ToDateTime(dr["exptime"]);
                    model.pulltime = HJConvert.ToDateTime(dr["pulltime"]);
                    model.money = HJConvert.ToDecimal(dr["money"]);
                    model.moneyline = HJConvert.ToDecimal(dr["moneyline"]);
                    model.state = HJConvert.ToInt32(dr["state"]);
                    model.pulltel = HJConvert.ToString(dr["pulltel"]);
                    model.reveint = HJConvert.ToInt32(dr["reveint"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.revevar = HJConvert.ToString(dr["revevar"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.datetime1 = HJConvert.ToDateTime(dr["datetime1"]);
                    model.datetime2 = HJConvert.ToDateTime(dr["datetime2"]);
                    infos.Add(model);
                }
            }
            return infos;
        }
	}
}

