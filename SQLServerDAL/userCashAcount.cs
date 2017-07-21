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
    /// /商家结算的帐号信息（银行支付宝等）
    /// </summary>
    public partial class userCashAcount
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(userCashAcountInfo model)
        {
            SqlParameter[] parameters = 
			{
			    new SqlParameter("@shopid", SqlDbType.Int,4) ,            
                new SqlParameter("@bankname", SqlDbType.VarChar,256) ,            
                new SqlParameter("@bankusername", SqlDbType.VarChar,256) ,            
                new SqlParameter("@aliaccount", SqlDbType.VarChar,256) ,            
                new SqlParameter("@aliname", SqlDbType.VarChar,256) ,            
                new SqlParameter("@remark", SqlDbType.VarChar,256) ,            
                new SqlParameter("@opuser", SqlDbType.VarChar,256) ,            
                new SqlParameter("@optime", SqlDbType.DateTime) ,            
                new SqlParameter("@reveint1", SqlDbType.Int,4) ,            
                new SqlParameter("@reveint2", SqlDbType.Int,4) ,            
                new SqlParameter("@reveint3", SqlDbType.Int,4) ,            
                new SqlParameter("@reveint4", SqlDbType.Int,4) ,            
                new SqlParameter("@reveint5", SqlDbType.Int,4) ,            
                new SqlParameter("@revefloat1", SqlDbType.Decimal,5) ,            
                new SqlParameter("@revefloat2", SqlDbType.Decimal,5) ,            
                new SqlParameter("@revefloat3", SqlDbType.Decimal,5) ,            
                new SqlParameter("@revevar1", SqlDbType.VarChar,256) ,            
                new SqlParameter("@revevar2", SqlDbType.VarChar,256) ,            
                new SqlParameter("@revevar3", SqlDbType.VarChar,256) ,            
                new SqlParameter("@revevar4", SqlDbType.VarChar,256) ,            
                new SqlParameter("@revevar5", SqlDbType.VarChar,256) ,            
                new SqlParameter("@revetext", SqlDbType.Text)             
              
            };

            parameters[0].Value = model.shopid;
            parameters[1].Value = model.bankname;
            parameters[2].Value = model.bankusername;
            parameters[3].Value = model.aliaccount;
            parameters[4].Value = model.aliname;
            parameters[5].Value = model.remark;
            parameters[6].Value = model.opuser;
            parameters[7].Value = model.optime;
            parameters[8].Value = model.reveint1;
            parameters[9].Value = model.reveint2;
            parameters[10].Value = model.reveint3;
            parameters[11].Value = model.reveint4;
            parameters[12].Value = model.reveint5;
            parameters[13].Value = model.revefloat1;
            parameters[14].Value = model.revefloat2;
            parameters[15].Value = model.revefloat3;
            parameters[16].Value = model.revevar1;
            parameters[17].Value = model.revevar2;
            parameters[18].Value = model.revevar3;
            parameters[19].Value = model.revevar4;
            parameters[20].Value = model.revevar5;
            parameters[21].Value = model.revetext;

            return HJConvert.ToInt32(SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "userCashAcount_ADD", parameters));
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(userCashAcountInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update userCashAcount set ");

            strSql.Append(" shopid = @shopid , ");
            strSql.Append(" bankname = @bankname , ");
            strSql.Append(" bankusername = @bankusername , ");
            strSql.Append(" aliaccount = @aliaccount , ");
            strSql.Append(" aliname = @aliname , ");
            strSql.Append(" remark = @remark , ");
            strSql.Append(" opuser = @opuser , ");
            strSql.Append(" optime = @optime , ");
            strSql.Append(" reveint1 = @reveint1 , ");
            strSql.Append(" reveint2 = @reveint2 , ");
            strSql.Append(" reveint3 = @reveint3 , ");
            strSql.Append(" reveint4 = @reveint4 , ");
            strSql.Append(" reveint5 = @reveint5 , ");
            strSql.Append(" revefloat1 = @revefloat1 , ");
            strSql.Append(" revefloat2 = @revefloat2 , ");
            strSql.Append(" revefloat3 = @revefloat3 , ");
            strSql.Append(" revevar1 = @revevar1 , ");
            strSql.Append(" revevar2 = @revevar2 , ");
            strSql.Append(" revevar3 = @revevar3 , ");
            strSql.Append(" revevar4 = @revevar4 , ");
            strSql.Append(" revevar5 = @revevar5 , ");
            strSql.Append(" revetext = @revetext  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = 
			{
			    new SqlParameter("@Id", SqlDbType.Int,4) ,            
                new SqlParameter("@shopid", SqlDbType.Int,4) ,            
                new SqlParameter("@bankname", SqlDbType.VarChar,256) ,            
                new SqlParameter("@bankusername", SqlDbType.VarChar,256) ,            
                new SqlParameter("@aliaccount", SqlDbType.VarChar,256) ,            
                new SqlParameter("@aliname", SqlDbType.VarChar,256) ,            
                new SqlParameter("@remark", SqlDbType.VarChar,256) ,            
                new SqlParameter("@opuser", SqlDbType.VarChar,256) ,            
                new SqlParameter("@optime", SqlDbType.DateTime) ,            
                new SqlParameter("@reveint1", SqlDbType.Int,4) ,            
                new SqlParameter("@reveint2", SqlDbType.Int,4) ,            
                new SqlParameter("@reveint3", SqlDbType.Int,4) ,            
                new SqlParameter("@reveint4", SqlDbType.Int,4) ,            
                new SqlParameter("@reveint5", SqlDbType.Int,4) ,            
                new SqlParameter("@revefloat1", SqlDbType.Decimal,5) ,            
                new SqlParameter("@revefloat2", SqlDbType.Decimal,5) ,            
                new SqlParameter("@revefloat3", SqlDbType.Decimal,5) ,            
                new SqlParameter("@revevar1", SqlDbType.VarChar,256) ,            
                new SqlParameter("@revevar2", SqlDbType.VarChar,256) ,            
                new SqlParameter("@revevar3", SqlDbType.VarChar,256) ,            
                new SqlParameter("@revevar4", SqlDbType.VarChar,256) ,            
                new SqlParameter("@revevar5", SqlDbType.VarChar,256) ,            
                new SqlParameter("@revetext", SqlDbType.Text)             
              
            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.shopid;
            parameters[2].Value = model.bankname;
            parameters[3].Value = model.bankusername;
            parameters[4].Value = model.aliaccount;
            parameters[5].Value = model.aliname;
            parameters[6].Value = model.remark;
            parameters[7].Value = model.opuser;
            parameters[8].Value = model.optime;
            parameters[9].Value = model.reveint1;
            parameters[10].Value = model.reveint2;
            parameters[11].Value = model.reveint3;
            parameters[12].Value = model.reveint4;
            parameters[13].Value = model.reveint5;
            parameters[14].Value = model.revefloat1;
            parameters[15].Value = model.revefloat2;
            parameters[16].Value = model.revefloat3;
            parameters[17].Value = model.revevar1;
            parameters[18].Value = model.revevar2;
            parameters[19].Value = model.revevar3;
            parameters[20].Value = model.revevar4;
            parameters[21].Value = model.revevar5;
            parameters[22].Value = model.revetext;
            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>Id</param>
        /// <returns>userCashAcountInfo</returns>
        public userCashAcountInfo GetModel(int Id)
        {
            string sql = "select Id,shopid,bankname,bankusername,aliaccount,aliname,remark,opuser,optime,reveint1,reveint2,reveint3,reveint4,reveint5,revefloat1,revefloat2,revefloat3,revevar1,revevar2,revevar3,revevar4,revevar5,revetext from userCashAcount where  Id = @Id";
            SqlParameter parameter = new SqlParameter("@Id", SqlDbType.Int, 4);
            parameter.Value = Id;
            userCashAcountInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new userCashAcountInfo();
                    model.Id = HJConvert.ToInt32(dr["Id"]);
                    model.shopid = HJConvert.ToInt32(dr["shopid"]);
                    model.bankname = HJConvert.ToString(dr["bankname"]);
                    model.bankusername = HJConvert.ToString(dr["bankusername"]);
                    model.aliaccount = HJConvert.ToString(dr["aliaccount"]);
                    model.aliname = HJConvert.ToString(dr["aliname"]);
                    model.remark = HJConvert.ToString(dr["remark"]);
                    model.opuser = HJConvert.ToString(dr["opuser"]);
                    model.optime = HJConvert.ToDateTime(dr["optime"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.reveint3 = HJConvert.ToInt32(dr["reveint3"]);
                    model.reveint4 = HJConvert.ToInt32(dr["reveint4"]);
                    model.reveint5 = HJConvert.ToInt32(dr["reveint5"]);
                    model.revefloat1 = HJConvert.ToDecimal(dr["revefloat1"]);
                    model.revefloat2 = HJConvert.ToDecimal(dr["revefloat2"]);
                    model.revefloat3 = HJConvert.ToDecimal(dr["revefloat3"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    model.revevar3 = HJConvert.ToString(dr["revevar3"]);
                    model.revevar4 = HJConvert.ToString(dr["revevar4"]);
                    model.revevar5 = HJConvert.ToString(dr["revevar5"]);
                    model.revetext = HJConvert.ToString(dr["revetext"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 根据用户编号，获取
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>Id</param>
        /// <returns>userCashAcountInfo</returns>
        public userCashAcountInfo GetModelByUser(int userid)
        {
            string sql = "select Id,shopid,bankname,bankusername,aliaccount,aliname,remark,opuser,optime,reveint1,reveint2,reveint3,reveint4,reveint5,revefloat1,revefloat2,revefloat3,revevar1,revevar2,revevar3,revevar4,revevar5,revetext from userCashAcount where  shopid = @Id";
            SqlParameter parameter = new SqlParameter("@Id", SqlDbType.Int, 4);
            parameter.Value = userid;
            userCashAcountInfo model = new userCashAcountInfo();
            model.Id = 0;
            model.shopid = userid;
            model.bankname = "";
            model.bankusername = "";
            model.aliaccount = "";
            model.aliname = "";
            model.remark = "";
            model.opuser = "";
            model.optime = DateTime.Now;
            model.reveint1 = 0;
            model.reveint2 = 0;
            model.reveint3 = 0;
            model.reveint4 = 0;
            model.reveint5 = 0;
            model.revefloat1 = 0;
            model.revefloat2 = 0;
            model.revefloat3 = 0;
            model.revevar1 = "";
            model.revevar2 = "";
            model.revevar3 = "";
            model.revevar4 = "";
            model.revevar5 = "";
            model.revetext = "";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model.Id = HJConvert.ToInt32(dr["Id"]);
                    model.shopid = HJConvert.ToInt32(dr["shopid"]);
                    model.bankname = HJConvert.ToString(dr["bankname"]);
                    model.bankusername = HJConvert.ToString(dr["bankusername"]);
                    model.aliaccount = HJConvert.ToString(dr["aliaccount"]);
                    model.aliname = HJConvert.ToString(dr["aliname"]);
                    model.remark = HJConvert.ToString(dr["remark"]);
                    model.opuser = HJConvert.ToString(dr["opuser"]);
                    model.optime = HJConvert.ToDateTime(dr["optime"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.reveint3 = HJConvert.ToInt32(dr["reveint3"]);
                    model.reveint4 = HJConvert.ToInt32(dr["reveint4"]);
                    model.reveint5 = HJConvert.ToInt32(dr["reveint5"]);
                    model.revefloat1 = HJConvert.ToDecimal(dr["revefloat1"]);
                    model.revefloat2 = HJConvert.ToDecimal(dr["revefloat2"]);
                    model.revefloat3 = HJConvert.ToDecimal(dr["revefloat3"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    model.revevar3 = HJConvert.ToString(dr["revevar3"]);
                    model.revevar4 = HJConvert.ToString(dr["revevar4"]);
                    model.revevar5 = HJConvert.ToString(dr["revevar5"]);
                    model.revetext = HJConvert.ToString(dr["revetext"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "userCashAcount"), new SqlParameter("@strWhere", strWhere));
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
        public IList<userCashAcountInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<userCashAcountInfo> infos = new List<userCashAcountInfo>();
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
            parameters[0].Value = "userCashAcount";
            parameters[1].Value = "Id,shopid,bankname,bankusername,aliaccount,aliname,remark,opuser,optime,reveint1,reveint2,reveint3,reveint4,reveint5,revefloat1,revefloat2,revefloat3,revevar1,revevar2,revevar3,revevar4,revevar5,revetext";
            parameters[2].Value = "Id";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    userCashAcountInfo info = new userCashAcountInfo();
                    info.Id = HJConvert.ToInt32(dr["Id"]);
                    info.shopid = HJConvert.ToInt32(dr["shopid"]);
                    info.bankname = HJConvert.ToString(dr["bankname"]);
                    info.bankusername = HJConvert.ToString(dr["bankusername"]);
                    info.aliaccount = HJConvert.ToString(dr["aliaccount"]);
                    info.aliname = HJConvert.ToString(dr["aliname"]);
                    info.remark = HJConvert.ToString(dr["remark"]);
                    info.opuser = HJConvert.ToString(dr["opuser"]);
                    info.optime = HJConvert.ToDateTime(dr["optime"]);
                    info.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    info.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    info.reveint3 = HJConvert.ToInt32(dr["reveint3"]);
                    info.reveint4 = HJConvert.ToInt32(dr["reveint4"]);
                    info.reveint5 = HJConvert.ToInt32(dr["reveint5"]);
                    info.revefloat1 = HJConvert.ToDecimal(dr["revefloat1"]);
                    info.revefloat2 = HJConvert.ToDecimal(dr["revefloat2"]);
                    info.revefloat3 = HJConvert.ToDecimal(dr["revefloat3"]);
                    info.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    info.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    info.revevar3 = HJConvert.ToString(dr["revevar3"]);
                    info.revevar4 = HJConvert.ToString(dr["revevar4"]);
                    info.revevar5 = HJConvert.ToString(dr["revevar5"]);
                    info.revetext = HJConvert.ToString(dr["revetext"]);
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
        public int DeluserCashAcount(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from userCashAcount where Id=@Id");
            SqlParameter[] Para = 
			{
				new SqlParameter("@Id",SqlDbType.Int)
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
            str.Append("delete from userCashAcount where Id in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }



    }
}

