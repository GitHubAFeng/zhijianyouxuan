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
    /// 数据访问类:msgpacketrecord
    /// </summary>
    public partial class msgpacketrecord
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(msgpacketrecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into msgpacketrecord(");
            strSql.Append("pid,pulltime,pullmoney,pulltel,reveint,revevar,datetime1)");
            strSql.Append(" values (");
            strSql.Append("@pid,@pulltime,@pullmoney,@pulltel,@reveint,@revevar,@datetime1)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
        {
			new SqlParameter("@pid", SqlDbType.Int,4),
			new SqlParameter("@pulltime", SqlDbType.DateTime),
			new SqlParameter("@pullmoney", SqlDbType.Decimal,9),
			new SqlParameter("@reveint", SqlDbType.Int,4),
			new SqlParameter("@revevar", SqlDbType.VarChar,50),
			new SqlParameter("@datetime1", SqlDbType.DateTime),
            new SqlParameter("@pulltel", SqlDbType.VarChar,50)
        };
            parameters[0].Value = model.pid;
            parameters[1].Value = model.pulltime;
            parameters[2].Value = model.pullmoney;
            parameters[3].Value = model.reveint;
            parameters[4].Value = model.revevar;
            parameters[5].Value = model.datetime1;
            parameters[6].Value = model.pulltel;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(msgpacketrecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update msgpacketrecord set ");
            strSql.Append("pid=@pid,");
            strSql.Append("pulltime=@pulltime,");
            strSql.Append("pullmoney=@pullmoney,");
            strSql.Append("reveint=@reveint,");
            strSql.Append("revevar=@revevar,");
            strSql.Append("datetime1=@datetime1,");
            strSql.Append("pulltel=@pulltel");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@pid", SqlDbType.Int,4),
				new SqlParameter("@pulltime", SqlDbType.DateTime),
				new SqlParameter("@pullmoney", SqlDbType.Decimal,9),
				new SqlParameter("@reveint", SqlDbType.Int,4),
				new SqlParameter("@revevar", SqlDbType.VarChar,50),
				new SqlParameter("@datetime1", SqlDbType.DateTime),
				new SqlParameter("@pulltel", SqlDbType.VarChar,50)
            };
            parameters[0].Value = model.pid;
            parameters[1].Value = model.pulltime;
            parameters[2].Value = model.pullmoney;
            parameters[3].Value = model.reveint;
            parameters[4].Value = model.revevar;
            parameters[5].Value = model.datetime1;
            parameters[6].Value = model.pulltel;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from msgpacketrecord ");
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
        public int DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from msgpacketrecord ");
            strSql.Append(" where id in (" + idlist + ")  ");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(strSql.ToString(), idlist), null);
        }
        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>IntegralId</param>
        /// <returns>IntegralInfo</returns>
        public msgpacketrecordInfo GetModel(int Id)
        {
            string sql = "select id,pid,pulltime,pullmoney,pulltel,reveint,revevar,datetime1 from msgpacketrecord where id=@id";
            SqlParameter parameter = new SqlParameter("@Id", SqlDbType.Int, 4);
            parameter.Value = Id;
            msgpacketrecordInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new msgpacketrecordInfo();
                    model.id = HJConvert.ToInt32(dr["id"]);
                    model.pid = HJConvert.ToInt32(dr["pid"]);
                    model.pulltime = HJConvert.ToDateTime(dr["pulltime"]);
                    model.pullmoney = HJConvert.ToDecimal(dr["pullmoney"]);
                    model.reveint = HJConvert.ToInt32(dr["reveint"]);
                    model.revevar = HJConvert.ToString(dr["revevar"]);
                    model.datetime1 = HJConvert.ToDateTime(dr["datetime1"]);
                    model.pulltel = HJConvert.ToString(dr["pulltel"]);
                    
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "msgpacketrecord"), new SqlParameter("@strWhere", strWhere));
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
        public IList<msgpacketrecordInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<msgpacketrecordInfo> infos = new List<msgpacketrecordInfo>();
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
            parameters[0].Value = "msgpacketrecord";
            string field = "id,pid,pulltime,pullmoney,pulltel,reveint,revevar,datetime1";
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
                    msgpacketrecordInfo model = new msgpacketrecordInfo();
                    model.id = HJConvert.ToInt32(dr["id"]);
                    model.pid = HJConvert.ToInt32(dr["pid"]);
                    model.pulltime = HJConvert.ToDateTime(dr["pulltime"]);
                    model.pullmoney = HJConvert.ToDecimal(dr["pullmoney"]);
                    model.reveint = HJConvert.ToInt32(dr["reveint"]);
                    model.revevar = HJConvert.ToString(dr["revevar"]);
                    model.datetime1 = HJConvert.ToDateTime(dr["datetime1"]);
                    model.pulltel = HJConvert.ToString(dr["pulltel"]);
                    infos.Add(model);
                }
            }
            return infos;
        }
    }
}

