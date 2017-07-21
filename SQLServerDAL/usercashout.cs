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
    /// 商家提现记录
    /// </summary>
    public partial class usercashout
    {
        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>cid</param>
        /// <returns>usercashoutInfo</returns>
        public CanCashOutInfo GetCanCashOutmoney(int shopid)
        {
            CanCashOutInfo model = new CanCashOutInfo();

            ECustomerInfo shop = new ECustomer().GetModel(shopid);
            model.AllMoney = shop.Usermoney;

            string SqlWhere = "  1=1 and state = 0 and shopid = " + shopid + "";
            model.nousemoney = GetNoUseMoney(SqlWhere);

            model.usemoney = model.AllMoney - model.nousemoney;


            return model;
        }

        /// <summary>
        /// 查询冻结的金额
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public decimal GetNoUseMoney(string where)
        {
            string sql = "select sum(wantmoney) as  OrderTotal ";
            sql += " from usercashout where " + where;

            decimal nousemoney = HJConvert.ToDecimal(SQLHelper.ExecuteScalar(CommandType.Text, sql, null));
            return nousemoney;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(usercashoutInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into usercashout(");
            strSql.Append("wantmoney,shopid,addtime,state,remark,optime,opuser,reveint1,reveint2,reveint3,reveint4,reveint5,revefloat1,revefloat2,revefloat3,revevar1,revevar2,revevar3,revevar4,revevar5,revetext,starttime,endtime");
            strSql.Append(") values (");
            strSql.Append("@wantmoney,@shopid,@addtime,@state,@remark,@optime,@opuser,@reveint1,@reveint2,@reveint3,@reveint4,@reveint5,@revefloat1,@revefloat2,@revefloat3,@revevar1,@revevar2,@revevar3,@revevar4,@revevar5,@revetext,@starttime,@endtime");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
			{
			    new SqlParameter("@wantmoney", SqlDbType.Decimal,5) ,            
                new SqlParameter("@shopid", SqlDbType.Int,4) ,            
                new SqlParameter("@addtime", SqlDbType.DateTime) ,            
                new SqlParameter("@state", SqlDbType.Int,4) ,            
                new SqlParameter("@remark", SqlDbType.VarChar,256) ,            
                new SqlParameter("@optime", SqlDbType.DateTime) ,            
                new SqlParameter("@opuser", SqlDbType.VarChar,256) ,            
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
                new SqlParameter("@revetext", SqlDbType.Text) ,            
                new SqlParameter("@starttime", SqlDbType.DateTime) ,            
                new SqlParameter("@endtime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.wantmoney;
            parameters[1].Value = model.shopid;
            parameters[2].Value = model.addtime;
            parameters[3].Value = model.state;
            parameters[4].Value = model.remark;
            parameters[5].Value = model.optime;
            parameters[6].Value = model.opuser;
            parameters[7].Value = model.reveint1;
            parameters[8].Value = model.reveint2;
            parameters[9].Value = model.reveint3;
            parameters[10].Value = model.reveint4;
            parameters[11].Value = model.reveint5;
            parameters[12].Value = model.revefloat1;
            parameters[13].Value = model.revefloat2;
            parameters[14].Value = model.revefloat3;
            parameters[15].Value = model.revevar1;
            parameters[16].Value = model.revevar2;
            parameters[17].Value = model.revevar3;
            parameters[18].Value = model.revevar4;
            parameters[19].Value = model.revevar5;
            parameters[20].Value = model.revetext;
            parameters[21].Value = model.starttime;
            parameters[22].Value = model.endtime;
            return HJConvert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(usercashoutInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update usercashout set ");
            strSql.Append(" wantmoney = @wantmoney , ");
            strSql.Append(" shopid = @shopid , ");
            strSql.Append(" addtime = @addtime , ");
            strSql.Append(" state = @state , ");
            strSql.Append(" remark = @remark , ");
            strSql.Append(" optime = @optime , ");
            strSql.Append(" opuser = @opuser , ");
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
            strSql.Append(" revetext = @revetext , ");
            strSql.Append(" starttime = @starttime , ");
            strSql.Append(" endtime = @endtime  ");
            strSql.Append(" where cid=@cid ");

            SqlParameter[] parameters = 
			{
			    new SqlParameter("@cid", SqlDbType.Int,4) ,            
                new SqlParameter("@wantmoney", SqlDbType.Decimal,5) ,            
                new SqlParameter("@shopid", SqlDbType.Int,4) ,            
                new SqlParameter("@addtime", SqlDbType.DateTime) ,            
                new SqlParameter("@state", SqlDbType.Int,4) ,            
                new SqlParameter("@remark", SqlDbType.VarChar,256) ,            
                new SqlParameter("@optime", SqlDbType.DateTime) ,            
                new SqlParameter("@opuser", SqlDbType.VarChar,256) ,            
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
                new SqlParameter("@revetext", SqlDbType.Text) ,            
                new SqlParameter("@starttime", SqlDbType.DateTime) ,            
                new SqlParameter("@endtime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.cid;
            parameters[1].Value = model.wantmoney;
            parameters[2].Value = model.shopid;
            parameters[3].Value = model.addtime;
            parameters[4].Value = model.state;
            parameters[5].Value = model.remark;
            parameters[6].Value = model.optime;
            parameters[7].Value = model.opuser;
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
            parameters[22].Value = model.starttime;
            parameters[23].Value = model.endtime;
            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>cid</param>
        /// <returns>usercashoutInfo</returns>
        public usercashoutInfo GetModel(int cid)
        {
            string sql = "select cid,wantmoney,shopid,addtime,state,remark,optime,opuser,reveint1,reveint2,reveint3,reveint4,reveint5,revefloat1,revefloat2,revefloat3,revevar1,revevar2,revevar3,revevar4,revevar5,revetext,starttime,endtime from usercashout where  cid = @cid";
            SqlParameter parameter = new SqlParameter("@cid", SqlDbType.Int, 4);
            parameter.Value = cid;
            usercashoutInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new usercashoutInfo();
                    model.cid = HJConvert.ToInt32(dr["cid"]);
                    model.wantmoney = HJConvert.ToDecimal(dr["wantmoney"]);
                    model.shopid = HJConvert.ToInt32(dr["shopid"]);
                    model.addtime = HJConvert.ToDateTime(dr["addtime"]);
                    model.state = HJConvert.ToInt32(dr["state"]);
                    model.remark = HJConvert.ToString(dr["remark"]);
                    model.optime = HJConvert.ToDateTime(dr["optime"]);
                    model.opuser = HJConvert.ToString(dr["opuser"]);
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
                    model.starttime = HJConvert.ToDateTime(dr["starttime"]);
                    model.endtime = HJConvert.ToDateTime(dr["endtime"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "usercashout"), new SqlParameter("@strWhere", strWhere));
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
        public IList<usercashoutInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<usercashoutInfo> infos = new List<usercashoutInfo>();
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
            parameters[0].Value = "usercashout";
            parameters[1].Value = "*,(select Name from Points where Unid=usercashout.shopid) as TogoName";
            parameters[2].Value = "cid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    usercashoutInfo info = new usercashoutInfo();
                    info.cid = HJConvert.ToInt32(dr["cid"]);
                    info.wantmoney = HJConvert.ToDecimal(dr["wantmoney"]);
                    info.shopid = HJConvert.ToInt32(dr["shopid"]);
                    info.TogoName = HJConvert.ToString(dr["TogoName"]);
                    info.addtime = HJConvert.ToDateTime(dr["addtime"]);
                    info.state = HJConvert.ToInt32(dr["state"]);
                    info.remark = HJConvert.ToString(dr["remark"]);
                    info.optime = HJConvert.ToDateTime(dr["optime"]);
                    info.opuser = HJConvert.ToString(dr["opuser"]);
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
                    info.starttime = HJConvert.ToDateTime(dr["starttime"]);
                    info.endtime = HJConvert.ToDateTime(dr["endtime"]);
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
        public int Delusercashout(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from usercashout where cid=@cid");
            SqlParameter[] Para = 
			{
				new SqlParameter("@cid",SqlDbType.Int)
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
            str.Append("delete from usercashout where cid in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 只更新申请中的提现状态
        /// </summary>
        /// <param name="state"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public int updateState(int state, int cid)
        {
            string sql = "update usercashout set state = @state where cid = @cid and state = 0";
            SqlParameter[] Para = 
			{
				new SqlParameter("@state",SqlDbType.Int),
                new SqlParameter("@cid",SqlDbType.Int)
			};
            Para[0].Value = state;
            Para[1].Value = cid;
            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, Para);
        }
    }
}

