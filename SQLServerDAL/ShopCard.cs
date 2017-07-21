using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

using Hangjing.DBUtility;
using Hangjing.Model;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 优惠券
    /// </summary>
    public class ShopCard
    {
        /// <summary>
        /// 用户绑定优惠券，要判断哦。
        /// </summary>
        /// <param name="cardkey"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public apiResultInfo userAddCard(string cardkey, int userid)
        {
            ECustomerInfo user = new ECustomer().GetModel(userid);

            apiResultInfo rs = new apiResultInfo();
            rs.state = 0;
            string sql = "  ckey = '" + cardkey + "'";
            IList<ShopCardInfo> cardlist = GetList(1, 1, sql, "cid", 1);
            if (cardlist.Count == 0)
            {
                rs.msg = "券号错误，请重新输入";
            }
            else
            {
                if (cardlist[0].Inve2 == "0")
                {
                    rs.msg = "这张券未激活，不能绑定";
                }
                else
                {
                    if (cardlist[0].State == 1)
                    {
                        rs.msg = "这张券已经被绑定了";
                    }
                    else
                    {

                        sql = "update ShopCard set usergettime = '" + DateTime.Now.ToString() + "' , state =1 , userid = " + user.DataID + ",username = '" + user.Name + "' where CID  = " + cardlist[0].CID + "";
                        if (SQLHelper.excutesql(sql) > 0)
                        {
                            rs.state = 1;
                            rs.msg = "绑定成功";
                        }
                        else
                        {
                            rs.msg = "系统错误，请稍后";
                        }
                    }
                }
            }

            return rs;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.ShopCardInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ShopCard(");
            strSql.Append("cardnum,ckey,AddDate,State,Point,batid,canday,Inve1,Inve2,userid,username,cmoney,ReveInt,ReveVar,usergettime,isbuy,buyuid,buytime,isused,timelimity,starttime,endtime,moneylimity,moneyline,ReveInt1,ReveVar1");
            strSql.Append(") values (");
            strSql.Append("@cardnum,@ckey,@AddDate,@State,@Point,@batid,@canday,@Inve1,@Inve2,@userid,@username,@cmoney,@ReveInt,@ReveVar,@usergettime,@isbuy,@buyuid,@buytime,@isused,@timelimity,@starttime,@endtime,@moneylimity,@moneyline,@ReveInt1,@ReveVar1");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters =
            {
                new SqlParameter("@cardnum", SqlDbType.VarChar,50) ,
                new SqlParameter("@ckey", SqlDbType.VarChar,50) ,
                new SqlParameter("@AddDate", SqlDbType.DateTime) ,
                new SqlParameter("@State", SqlDbType.Int,4) ,
                 new SqlParameter("@point", SqlDbType.Decimal) ,
                new SqlParameter("@batid", SqlDbType.Int,4) ,
                new SqlParameter("@canday", SqlDbType.Int,4) ,
                new SqlParameter("@Inve1", SqlDbType.Int,4) ,
                new SqlParameter("@Inve2", SqlDbType.VarChar,256) ,
                new SqlParameter("@userid", SqlDbType.Int,4) ,
                new SqlParameter("@username", SqlDbType.VarChar,256) ,
                new SqlParameter("@cmoney", SqlDbType.Decimal,5) ,
                new SqlParameter("@ReveInt", SqlDbType.Int,4) ,
                new SqlParameter("@ReveVar", SqlDbType.VarChar,256) ,
                new SqlParameter("@usergettime", SqlDbType.DateTime) ,
                new SqlParameter("@isbuy", SqlDbType.Int,4) ,
                new SqlParameter("@buyuid", SqlDbType.Int,4) ,
                new SqlParameter("@buytime", SqlDbType.DateTime) ,
                new SqlParameter("@isused", SqlDbType.Int,4) ,
                new SqlParameter("@timelimity", SqlDbType.Int,4) ,
                new SqlParameter("@starttime", SqlDbType.DateTime) ,
                new SqlParameter("@endtime", SqlDbType.DateTime) ,
                new SqlParameter("@moneylimity", SqlDbType.Int,4) ,
                new SqlParameter("@moneyline", SqlDbType.Decimal,5) ,
                new SqlParameter("@ReveInt1", SqlDbType.Int,4) ,
                new SqlParameter("@ReveVar1", SqlDbType.VarChar,256)

            };

            parameters[0].Value = model.cardnum;
            parameters[1].Value = model.ckey;
            parameters[2].Value = model.AddDate;
            parameters[3].Value = model.State;
            parameters[4].Value = model.Point;
            parameters[5].Value = model.batid;
            parameters[6].Value = model.canday;
            parameters[7].Value = model.Inve1;
            parameters[8].Value = model.Inve2;
            parameters[9].Value = model.UserID;
            parameters[10].Value = model.username;
            parameters[11].Value = model.cmoney;
            parameters[12].Value = model.ReveInt;
            parameters[13].Value = model.ReveVar;
            parameters[14].Value = Convert.ToDateTime("1970-1-1");
            parameters[15].Value = model.isbuy;
            parameters[16].Value = model.buyuid;
            parameters[17].Value = Convert.ToDateTime("1970-1-1");
            parameters[18].Value = model.isused;
            parameters[19].Value = model.timelimity;
            parameters[20].Value = model.starttime;
            parameters[21].Value = model.endtime;
            parameters[22].Value = model.moneylimity;
            parameters[23].Value = model.moneyline;
            parameters[24].Value = model.ReveInt1;
            parameters[25].Value = model.ReveVar1;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.ShopCardInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ShopCard set ");
            strSql.Append("cardnum=@cardnum,");
            strSql.Append("ckey=@ckey,");
            strSql.Append("AddDate=@AddDate,");
            strSql.Append("State=@State,");
            strSql.Append("Point=@Point,");
            strSql.Append("batid=@batid,");
            strSql.Append("canday=@canday,");
            strSql.Append("Inve1=@Inve1,");
            strSql.Append("Inve2=@Inve2,");
            strSql.Append("ReveInt=@ReveInt,");
            strSql.Append("ReveVar=@ReveVar");

            strSql.Append(" where CID=@CID ");
            SqlParameter[] parameters =
            {
                new SqlParameter("@CID", SqlDbType.Int,4),
                new SqlParameter("@cardnum", SqlDbType.VarChar,50),
                new SqlParameter("@ckey", SqlDbType.VarChar,50),
                new SqlParameter("@AddDate", SqlDbType.DateTime),
                new SqlParameter("@State", SqlDbType.Int,4),
                new SqlParameter("@point", SqlDbType.Decimal) ,
                new SqlParameter("@batid", SqlDbType.Int,4),
                new SqlParameter("@canday", SqlDbType.Int,4),
                new SqlParameter("@Inve1", SqlDbType.Int,4),
                new SqlParameter("@Inve2", SqlDbType.VarChar,256),
                new SqlParameter("@ReveInt", SqlDbType.Int,4),
                new SqlParameter("@ReveVar", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.CID;
            parameters[1].Value = model.cardnum;
            parameters[2].Value = model.ckey;
            parameters[3].Value = model.AddDate;
            parameters[4].Value = model.State;
            parameters[5].Value = model.Point;
            parameters[6].Value = model.batid;
            parameters[7].Value = model.canday;
            parameters[8].Value = model.Inve1;
            parameters[9].Value = model.Inve2;
            parameters[10].Value = model.ReveInt;
            parameters[11].Value = model.ReveVar;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>CID</param>
        /// <returns>CardInfo</returns>
        public ShopCardInfo GetModel(int CID)
        {
            string sql = "select * from ShopCard where CID = @CID";
            SqlParameter parameter = new SqlParameter("@CID", SqlDbType.Int, 4);
            parameter.Value = CID;
            ShopCardInfo info = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    info = new ShopCardInfo();
                    info.CID = HJConvert.ToInt32(dr["CID"]);
                    info.cardnum = HJConvert.ToString(dr["cardnum"]);
                    info.ckey = HJConvert.ToString(dr["ckey"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    info.State = HJConvert.ToInt32(dr["State"]);
                    info.Point = HJConvert.ToDecimal(dr["Point"]);
                    info.batid = HJConvert.ToInt32(dr["batid"]);
                    info.canday = HJConvert.ToInt32(dr["canday"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.username = HJConvert.ToString(dr["username"]);
                    info.cmoney = HJConvert.ToDecimal(dr["cmoney"]);
                    info.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    info.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    info.isbuy = HJConvert.ToInt32(dr["isbuy"]);
                    info.buyuid = HJConvert.ToInt32(dr["buyuid"]);
                    info.isused = HJConvert.ToInt32(dr["isused"]);
                    info.UserID = HJConvert.ToInt32(dr["UserID"]);
                    info.timelimity = HJConvert.ToInt32(dr["timelimity"]);
                    info.starttime = HJConvert.ToDateTime(dr["starttime"]);
                    info.endtime = HJConvert.ToDateTime(dr["endtime"]);
                    info.moneylimity = HJConvert.ToInt32(dr["moneylimity"]);
                    info.moneyline = HJConvert.ToDecimal(dr["moneyline"]);
                    info.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    info.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);

                }
            }
            return info;
        }


        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>CID</param>
        /// <returns>CardInfo</returns>
        public ShopCardInfo GetModelByCPwd(string ckey)
        {
            string sql = "select * from ShopCard where ckey = @ckey";
            SqlParameter parameter = new SqlParameter("@ckey", SqlDbType.VarChar, 50);
            parameter.Value = ckey;
            ShopCardInfo info = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    info = new ShopCardInfo();
                    info.CID = HJConvert.ToInt32(dr["CID"]);
                    info.cardnum = HJConvert.ToString(dr["cardnum"]);
                    info.ckey = HJConvert.ToString(dr["ckey"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    info.State = HJConvert.ToInt32(dr["State"]);
                    info.Point = HJConvert.ToDecimal(dr["Point"]);
                    info.batid = HJConvert.ToInt32(dr["batid"]);
                    info.canday = HJConvert.ToInt32(dr["canday"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.username = HJConvert.ToString(dr["username"]);
                    info.cmoney = HJConvert.ToDecimal(dr["cmoney"]);
                    info.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    info.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    info.isbuy = HJConvert.ToInt32(dr["isbuy"]);
                    info.buyuid = HJConvert.ToInt32(dr["buyuid"]);
                    info.isused = HJConvert.ToInt32(dr["isused"]);
                    info.UserID = HJConvert.ToInt32(dr["UserID"]);
                    info.timelimity = HJConvert.ToInt32(dr["timelimity"]);
                    info.starttime = HJConvert.ToDateTime(dr["starttime"]);
                    info.endtime = HJConvert.ToDateTime(dr["endtime"]);
                    info.moneylimity = HJConvert.ToInt32(dr["moneylimity"]);
                    info.moneyline = HJConvert.ToDecimal(dr["moneyline"]);
                    info.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    info.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
                }
            }
            return info;
        }

        /// <summary>
        /// 获得总记录数
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int GetCount(string strWhere)
        {
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "ShopCard"), new SqlParameter("@strWhere", strWhere));
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
        public IList<ShopCardInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ShopCardInfo> infos = new List<ShopCardInfo>();
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
            parameters[0].Value = "ShopCard";
            string field = "*,(select title from batshopcard where batshopcard.dataid = ShopCard.batid) as title";
            parameters[1].Value = field;
            parameters[2].Value = "CID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    ShopCardInfo info = new ShopCardInfo();
                    info.CID = HJConvert.ToInt32(dr["CID"]);
                    info.cardnum = HJConvert.ToString(dr["cardnum"]);
                    info.ckey = HJConvert.ToString(dr["ckey"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    info.State = HJConvert.ToInt32(dr["State"]);
                    info.Point = HJConvert.ToDecimal(dr["Point"]);
                    info.batid = HJConvert.ToInt32(dr["batid"]);
                    info.canday = HJConvert.ToInt32(dr["canday"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.username = HJConvert.ToString(dr["username"]);
                    info.cmoney = HJConvert.ToDecimal(dr["cmoney"]);
                    info.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    info.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    info.isbuy = HJConvert.ToInt32(dr["isbuy"]);
                    info.buyuid = HJConvert.ToInt32(dr["buyuid"]);
                    info.isused = HJConvert.ToInt32(dr["isused"]);
                    info.UserID = HJConvert.ToInt32(dr["UserID"]);
                    info.timelimity = HJConvert.ToInt32(dr["timelimity"]);
                    info.starttime = HJConvert.ToDateTime(dr["starttime"]);
                    info.endtime = HJConvert.ToDateTime(dr["endtime"]);

                    info.usergettime = HJConvert.ToDateTime(dr["usergettime"]);

                    

                    info.moneylimity = HJConvert.ToInt32(dr["moneylimity"]);
                    info.moneyline = HJConvert.ToDecimal(dr["moneyline"]);
                    info.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    info.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
                    info.title = HJConvert.ToString(dr["title"]);

                    info.ReveVar1 = "商品满 " + info.moneyline + "元";
                    switch (info.ReveInt1)
                    {
                        case 1:
                            info.ReveVar1 += "减 " + info.Point + "元";
                            break;
                        case 2:
                            info.ReveVar1 += "" + (info.Point) + "折优惠";
                            break;
                        case 3:
                            info.ReveVar1 += "" + info.Point + "倍积分";
                            break;
                        default:
                            break;
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
        public int DelCard(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ShopCard where CID=@CID");
            SqlParameter[] Para =
            {
                new SqlParameter("@CID",SqlDbType.Int)
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
            str.Append("delete from ShopCard where CID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        public int UpdateState(int id, int state)
        {
            string str = "update ShopCard set State=" + state + " where CID=" + id;
            return SQLHelper.ExecuteNonQuery(CommandType.Text, str, null);
        }

        /// <summary>
        /// 获取id.用','分开.
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public string GetList(string sql)
        {
            string ids = "";
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    ids += HJConvert.ToInt32(dr["CID"]) + ",";
                }
            }
            ids = HJConvert.dellast(ids);

            return ids;
        }
    }
}
