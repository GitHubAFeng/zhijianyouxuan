using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

using Hangjing.Model;
using Hangjing.DBUtility;//请先添加引用
namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 优惠券批次
    /// </summary>
    public class batshopcard
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(batshopcardInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into batshopcard(");
            strSql.Append("title,batnum,AddDate,cantimes,point,Inve1,Inve2,CardCount,contents,AdminName,sortnum,usedcount,starttime,endtime,mtype,mydiscount,foossortids,timelimity,moneylimity,moneyline");
            strSql.Append(") values (");
            strSql.Append("@title,@batnum,@AddDate,@cantimes,@point,@Inve1,@Inve2,@CardCount,@contents,@AdminName,@sortnum,@usedcount,@starttime,@endtime,@mtype,@mydiscount,@foossortids,@timelimity,@moneylimity,@moneyline");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
			{
	            new SqlParameter("@title", SqlDbType.VarChar,256) ,            
                new SqlParameter("@batnum", SqlDbType.VarChar,20) ,            
                new SqlParameter("@AddDate", SqlDbType.DateTime) ,            
                new SqlParameter("@cantimes", SqlDbType.Int,4) ,            
                new SqlParameter("@point", SqlDbType.Decimal) ,            
                new SqlParameter("@Inve1", SqlDbType.Int,4) ,            
                new SqlParameter("@Inve2", SqlDbType.VarChar,256) ,            
                new SqlParameter("@CardCount", SqlDbType.Int,4) ,            
                new SqlParameter("@contents", SqlDbType.Text) ,            
                new SqlParameter("@AdminName", SqlDbType.VarChar,50) ,            
                new SqlParameter("@sortnum", SqlDbType.Int,4) ,            
                new SqlParameter("@usedcount", SqlDbType.Int,4) ,            
                new SqlParameter("@starttime", SqlDbType.DateTime) ,            
                new SqlParameter("@endtime", SqlDbType.DateTime) ,            
                new SqlParameter("@mtype", SqlDbType.Int,4) ,            
                new SqlParameter("@mydiscount", SqlDbType.Int,4) ,            
                new SqlParameter("@foossortids", SqlDbType.VarChar,256) ,            
                new SqlParameter("@timelimity", SqlDbType.Int,4) ,            
                new SqlParameter("@moneylimity", SqlDbType.Int,4) ,            
                new SqlParameter("@moneyline", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.title;
            parameters[1].Value = model.batnum;
            parameters[2].Value = model.AddDate;
            parameters[3].Value = model.cantimes;
            parameters[4].Value = model.point;
            parameters[5].Value = model.Inve1;
            parameters[6].Value = model.Inve2;
            parameters[7].Value = model.CardCount;
            parameters[8].Value = model.Contents;
            parameters[9].Value = model.AdminName;
            parameters[10].Value = model.sortnum;
            parameters[11].Value = 0;
            parameters[12].Value = model.starttime;
            parameters[13].Value = model.endtime;
            parameters[14].Value = model.mtype;
            parameters[15].Value = model.mydiscount;
            parameters[16].Value = model.foossortids;
            parameters[17].Value = model.timelimity;
            parameters[18].Value = model.moneylimity;
            parameters[19].Value = model.moneyline;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(batshopcardInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update batshopcard set ");
            strSql.Append("title=@title,");
            strSql.Append("batnum=@batnum,");
            strSql.Append("AddDate=@AddDate,");
            strSql.Append("cantimes=@cantimes,");
            strSql.Append("point=@point,");
            strSql.Append("Inve1=@Inve1,");
            strSql.Append("mydiscount=@mydiscount,");
            strSql.Append("Inve2=@Inve2,");
            strSql.Append("CardCount=@CardCount,");
            strSql.Append("contents=@contents,sortnum=@sortnum");
            strSql.Append(" where DataID=@DataID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataID", SqlDbType.Int,4),
				new SqlParameter("@title", SqlDbType.VarChar,256),
				new SqlParameter("@batnum", SqlDbType.VarChar,20),
				new SqlParameter("@AddDate", SqlDbType.DateTime),
				new SqlParameter("@cantimes", SqlDbType.Int,4),
				 new SqlParameter("@point", SqlDbType.Decimal) ,       
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,256),
				new SqlParameter("@CardCount", SqlDbType.Int,4),
                new SqlParameter("@contents",SqlDbType.Text),
                new SqlParameter("@sortnum", SqlDbType.Int,4),
                new SqlParameter("@mydiscount", SqlDbType.Int,4),
            };
            parameters[0].Value = model.DataID;
            parameters[1].Value = model.title;
            parameters[2].Value = model.batnum;
            parameters[3].Value = model.AddDate;
            parameters[4].Value = model.cantimes;
            parameters[5].Value = model.point;
            parameters[6].Value = model.Inve1;
            parameters[7].Value = model.Inve2;
            parameters[8].Value = model.CardCount;
            parameters[9].Value = model.Contents;
            parameters[10].Value = model.sortnum;
            parameters[11].Value = model.mydiscount;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataID</param>
        /// <returns>batshopcardInfo</returns>
        public batshopcardInfo GetModel(int DataID)
        {
            string sql = "select * from batshopcard where DataID = @DataID";
            SqlParameter parameter = new SqlParameter("@DataID", SqlDbType.Int, 4);
            parameter.Value = DataID;
            batshopcardInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new batshopcardInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.title = HJConvert.ToString(dr["title"]);
                    model.batnum = HJConvert.ToString(dr["batnum"]);
                    model.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    model.cantimes = HJConvert.ToInt32(dr["cantimes"]);
                    model.point = HJConvert.ToDecimal(dr["point"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.CardCount = HJConvert.ToInt32(dr["CardCount"]);
                    model.Contents = HJConvert.ToString(dr["contents"]);
                    model.AdminName = HJConvert.ToString(dr["AdminName"]);
                    model.sortnum = HJConvert.ToInt32(dr["sortnum"]);
                    model.starttime = HJConvert.ToDateTime(dr["starttime"]);
                    model.endtime = HJConvert.ToDateTime(dr["endtime"]);
                    model.mtype = HJConvert.ToInt32(dr["mtype"]);
                    model.mydiscount = HJConvert.ToInt32(dr["mydiscount"]);
                    model.foossortids = HJConvert.ToString(dr["foossortids"]);
                    model.timelimity = HJConvert.ToInt32(dr["timelimity"]);
                    model.moneylimity = HJConvert.ToInt32(dr["moneylimity"]);
                    model.moneyline = HJConvert.ToInt32(dr["moneyline"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "batshopcard"), new SqlParameter("@strWhere", strWhere));
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
        public IList<batshopcardInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<batshopcardInfo> infos = new List<batshopcardInfo>();
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
            parameters[0].Value = "batshopcard";
            parameters[1].Value = "*";
            parameters[2].Value = "DataID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    batshopcardInfo info = new batshopcardInfo();
                    info.DataID = HJConvert.ToInt32(dr["DataID"]);
                    info.title = HJConvert.ToString(dr["title"]);
                    info.batnum = HJConvert.ToString(dr["batnum"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    info.cantimes = HJConvert.ToInt32(dr["cantimes"]);
                    info.point = HJConvert.ToDecimal(dr["point"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.CardCount = HJConvert.ToInt32(dr["CardCount"]);
                    info.Contents = HJConvert.ToString(dr["Contents"]);
                    info.AdminName = HJConvert.ToString(dr["AdminName"]);
                    info.sortnum = HJConvert.ToInt32(dr["sortnum"]);
                    info.starttime = HJConvert.ToDateTime(dr["starttime"]);
                    info.endtime = HJConvert.ToDateTime(dr["endtime"]);
                    info.mtype = HJConvert.ToInt32(dr["mtype"]);
                    info.mydiscount = HJConvert.ToInt32(dr["mydiscount"]);
                    info.foossortids = HJConvert.ToString(dr["foossortids"]);
                    info.timelimity = HJConvert.ToInt32(dr["timelimity"]);
                    info.moneylimity = HJConvert.ToInt32(dr["moneylimity"]);
                    info.moneyline = HJConvert.ToInt32(dr["moneyline"]);

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
        public int Delbatcard(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from batshopcard where DataID=@DataID");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@DataID",SqlDbType.Int)
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
            str.Append("delete from batshopcard where DataID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 更改库存
        /// </summary>
        /// <param name="DataID"></param>
        /// <param name="cutnum"></param>
        /// <returns></returns>
        public int UpdateCount(int DataID, int cutnum)
        {
            string sqlwhere = "update batshopcard set CardCount=CardCount-" + cutnum + " where DataID=" + DataID;
            return SQLHelper.ExecuteNonQuery(CommandType.Text, sqlwhere, null);
        }

        /// <summary>
        /// 前台获取列表(返回)
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<batshopcardInfo> GetMyCard(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<batshopcardInfo> infos = new List<batshopcardInfo>();
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
            parameters[0].Value = "batshopcard";
            //子查询获取这当前批次能用的礼品卡:没有购买，没有绑定，激活状态的。
            string filed = "*,(select count(*) from card where 1=1 and isbuy = 0 and State = 0 and Inve2 = 1 and batid = batshopcard.dataid) as canusecount";
            parameters[1].Value = filed;
            parameters[2].Value = "DataID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    batshopcardInfo info = new batshopcardInfo();
                    info.DataID = HJConvert.ToInt32(dr["DataID"]);
                    info.title = HJConvert.ToString(dr["title"]);
                    info.batnum = HJConvert.ToString(dr["batnum"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    info.cantimes = HJConvert.ToInt32(dr["cantimes"]);
                    info.point = HJConvert.ToDecimal(dr["point"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.CardCount = HJConvert.ToInt32(dr["CardCount"]);
                    info.Contents = HJConvert.ToString(dr["Contents"]);
                    info.AdminName = HJConvert.ToString(dr["AdminName"]);
                    info.sortnum = HJConvert.ToInt32(dr["sortnum"]);
                    info.canusecount = HJConvert.ToInt32(dr["canusecount"]);
                    infos.Add(info);
                }
            }
            return infos;
        }
        
        /// <summary>
        /// 用户兑换优惠券
        /// </summary>
        /// <param name="userid">会员编号</param>
        /// <param name="batcardid">券批次号</param>
        /// <param name="needpoint">需要积分</param>
        /// <returns></returns>
        public int ExchangeVoucher(int userid , int batcardid,int needpoint)
        {
            SqlParameter[] parameters = 
	        {
		        new SqlParameter("@userid", SqlDbType.Int),
		        new SqlParameter("@batcardid", SqlDbType.Int),
		        new SqlParameter("@needpoint", SqlDbType.Int),
		        new SqlParameter("@dataid", SqlDbType.Int),
		      
	        };
            parameters[0].Value = userid;
            parameters[1].Value = batcardid;
            parameters[2].Value = needpoint;
            parameters[3].Direction = ParameterDirection.Output;

            SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "batshopcard_ExchangeVoucher", parameters);

            return HJConvert.ToInt32(parameters[3].Value);
        }

    }
}

