using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Hangjing.SQLServerDAL
{
    public class Custorder
    {
        /// <summary>
        /// 根据订单自增id 或者 订单编号  获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>Unid</param>
        /// <returns>CustorderInfo</returns>
        public CustorderInfo GetOpenidAndDeliver(string orderid)
        {
            string sql = "SELECT P2Sign,Name,Phone FROM  dbo.Custorder LEFT JOIN dbo.Deliver ON dbo.Custorder.deliverid = dbo.Deliver.DataId WHERE orderid = '" + orderid + "' ";
            CustorderInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (dr.Read())
                {
                    model = new CustorderInfo();

                    model.Sender = HJConvert.ToString(dr["Name"]);
                    model.CallPhoneNo = HJConvert.ToString(dr["Phone"]);
                    model.P2Sign = HJConvert.ToString(dr["P2Sign"]);

                }
            }
            return model;
        }



        /// <summary>
        /// 订单取消时，如果用支付宝，余额支付的返回到余额
        /// </summary>
        /// <param name="orerid"></param>
        /// <returns></returns>
        public int OrderCancelRefund(string orerid)
        {
            CustorderInfo model = new Custorder().GetModel(orerid);
            string msg = "退款记录：orderid=" + orerid;

            if (model.paystate == 1 && model.paymoney > 0 && (model.OrderStatus == 5 || model.OrderStatus == 6 || model.OrderStatus == 4) && model.UserId > 0)
            {
                msg += "-》进入退款流程，搜索存储过程";
                SqlParameter[] Para =
                {
                    new SqlParameter("@userid",SqlDbType.Int,5),
                    new SqlParameter("@orderid",SqlDbType.VarChar,20)
                };
                Para[0].Value = model.UserId;
                Para[1].Value = model.orderid;

                SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "OrderCancelRefund", Para);
            }

            HJlog.toLog(msg);

            return 1;
        }

        /// <summary>
        /// 按天查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IList<CustorderInfo> GetSumGroupByDay(string where)
        {
            IList<CustorderInfo> list = new List<CustorderInfo>();

            string sql = "select sum(OrderSums) as  OrderTotal,Count(*) as OrderCount,sum(shopdiscountmoney) as shopdiscountmoney,";
            sql += " CONVERT(varchar(100), orderdateTime, 23) AS mydate";
            sql += " from Custorder where " + where + " GROUP BY CONVERT(varchar(100), orderdateTime, 23) order by  mydate asc ";


            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    CustorderInfo model = new CustorderInfo();
                    model.OrderCount = HJConvert.ToInt32(dr["OrderCount"]);
                    model.OrderTotal = HJConvert.ToDecimal(dr["OrderTotal"]);
                    model.shopdiscountmoney = HJConvert.ToDecimal(dr["shopdiscountmoney"]);


                    model.tempcode = HJConvert.ToString(dr["mydate"]);

                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.CustorderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into custorder(");
            strSql.Append("Unid,InUse,OrderDateTime,OrderChecker,OrderStatus,OrderRcver,OrderComm,OrderAddress,AddressText,OrderAddrEx,OrderAttach,OrderSums,Sender,SendTime,CallPhoneNo,P2Sign,SendFee,paymode,paytime,paymoney,paystate,SetStateTime, UserId, TogoId)");
            strSql.Append(" values (");
            strSql.Append("@Unid,@InUse,@OrderDateTime,@OrderChecker,@OrderStatus,@OrderRcver,@OrderComm,@OrderAddress,@AddressText,@OrderAddrEx,@OrderAttach,@OrderSums,@Sender,@SendTime,@CallPhoneNo,@P2Sign,@SendFee,@paymode,@paytime,@paymoney,@paystate,@SetStateTime,@UserId, @TogoId)");
            SqlParameter[] parameters =
            {
                new SqlParameter("@Unid", SqlDbType.Int,4),
                new SqlParameter("@InUse", SqlDbType.VarChar,10),
                new SqlParameter("@OrderDateTime", SqlDbType.DateTime),
                new SqlParameter("@OrderChecker", SqlDbType.Int,4),
                new SqlParameter("@OrderStatus", SqlDbType.Int,4),
                new SqlParameter("@OrderRcver", SqlDbType.VarChar,50),
                new SqlParameter("@OrderComm", SqlDbType.VarChar,50),
                new SqlParameter("@OrderAddress", SqlDbType.VarChar,50),
                new SqlParameter("@AddressText", SqlDbType.VarChar,100),
                new SqlParameter("@OrderAddrEx", SqlDbType.VarChar,300),
                new SqlParameter("@OrderAttach", SqlDbType.VarChar,200),
                new SqlParameter("@OrderSums", SqlDbType.Decimal,9),
                new SqlParameter("@Sender", SqlDbType.VarChar,256),
                new SqlParameter("@SendTime", SqlDbType.DateTime),
                new SqlParameter("@CallPhoneNo", SqlDbType.VarChar,256),
                new SqlParameter("@P2Sign", SqlDbType.VarChar,256),
                new SqlParameter("@SendFee", SqlDbType.Decimal,9),
                new SqlParameter("@paymode", SqlDbType.Int,4),
                new SqlParameter("@paytime", SqlDbType.DateTime),
                new SqlParameter("@paymoney", SqlDbType.Decimal,9),
                new SqlParameter("@paystate", SqlDbType.Int,4),
                new SqlParameter("@SetStateTime", SqlDbType.DateTime),
                new SqlParameter("@UserId", SqlDbType.Int,11),
                new SqlParameter("@TogoId", SqlDbType.Int,11)
            };
            parameters[0].Value = model.Unid;
            parameters[1].Value = model.InUse;
            parameters[2].Value = model.OrderDateTime;
            parameters[3].Value = model.OrderChecker;
            parameters[4].Value = model.OrderStatus;
            parameters[5].Value = model.OrderRcver;
            parameters[6].Value = model.OrderComm;
            parameters[7].Value = model.OrderAddress;
            parameters[8].Value = model.AddressText;
            parameters[9].Value = model.OrderAddrEx;
            parameters[10].Value = model.OrderAttach;
            parameters[11].Value = model.OrderSums;
            parameters[12].Value = model.Sender;
            parameters[13].Value = model.SendTime;
            parameters[14].Value = model.CallPhoneNo;
            parameters[15].Value = model.P2Sign;
            parameters[16].Value = model.SendFee;
            parameters[17].Value = model.paymode;
            parameters[18].Value = model.paytime;
            parameters[19].Value = model.paymoney;
            parameters[20].Value = model.paystate;
            parameters[21].Value = model.SetStateTime;
            parameters[22].Value = model.UserId;
            parameters[23].Value = model.TogoId;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.CustorderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update custorder set ");
            strSql.Append("InUse=@InUse,");
            strSql.Append("OrderDateTime=@OrderDateTime,");
            strSql.Append("OrderChecker=@OrderChecker,");
            strSql.Append("OrderStatus=@OrderStatus,");
            strSql.Append("OrderRcver=@OrderRcver,");
            strSql.Append("OrderComm=@OrderComm,");
            strSql.Append("OrderAddress=@OrderAddress,");
            strSql.Append("AddressText=@AddressText,");
            strSql.Append("OrderAddrEx=@OrderAddrEx,");
            strSql.Append("OrderAttach=@OrderAttach,");
            strSql.Append("OrderSums=@OrderSums,");
            strSql.Append("Sender=@Sender,");
            strSql.Append("SendTime=@SendTime,");
            strSql.Append("CallPhoneNo=@CallPhoneNo,");
            strSql.Append("P2Sign=@P2Sign,");
            strSql.Append("SendFee=@SendFee,");
            strSql.Append("paymode=@paymode,");
            strSql.Append("paytime=@paytime,");
            strSql.Append("paymoney=@paymoney,");
            strSql.Append("paystate=@paystate,");
            strSql.Append("SetStateTime=@SetStateTime,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("TogoId=@TogoId ");
            strSql.Append(" where Unid=@Unid ");
            SqlParameter[] parameters =
            {
                new SqlParameter("@Unid", SqlDbType.Int,4),
                new SqlParameter("@InUse", SqlDbType.VarChar,10),
                new SqlParameter("@OrderDateTime", SqlDbType.DateTime),
                new SqlParameter("@OrderChecker", SqlDbType.Int,4),
                new SqlParameter("@OrderStatus", SqlDbType.Int,4),
                new SqlParameter("@OrderRcver", SqlDbType.VarChar,50),
                new SqlParameter("@OrderComm", SqlDbType.VarChar,50),
                new SqlParameter("@OrderAddress", SqlDbType.VarChar,50),
                new SqlParameter("@AddressText", SqlDbType.VarChar,100),
                new SqlParameter("@OrderAddrEx", SqlDbType.VarChar,300),
                new SqlParameter("@OrderAttach", SqlDbType.VarChar,200),
                new SqlParameter("@OrderSums", SqlDbType.Decimal,9),
                new SqlParameter("@Sender", SqlDbType.VarChar,256),
                new SqlParameter("@SendTime", SqlDbType.DateTime),
                new SqlParameter("@CallPhoneNo", SqlDbType.VarChar,256),
                new SqlParameter("@P2Sign", SqlDbType.VarChar,256),
                new SqlParameter("@SendFee", SqlDbType.Decimal,9),
                new SqlParameter("@paymode", SqlDbType.Int,4),
                new SqlParameter("@paytime", SqlDbType.DateTime),
                new SqlParameter("@paymoney", SqlDbType.Decimal,9),
                new SqlParameter("@paystate", SqlDbType.Int,4),
                new SqlParameter("@SetStateTime", SqlDbType.DateTime),
                new SqlParameter("@UserId", SqlDbType.Int,11),
                new SqlParameter("@TogoId", SqlDbType.Int,11)
            };
            parameters[0].Value = model.Unid;
            parameters[1].Value = model.InUse;
            parameters[2].Value = model.OrderDateTime;
            parameters[3].Value = model.OrderChecker;
            parameters[4].Value = model.OrderStatus;
            parameters[5].Value = model.OrderRcver;
            parameters[6].Value = model.OrderComm;
            parameters[7].Value = model.OrderAddress;
            parameters[8].Value = model.AddressText;
            parameters[9].Value = model.OrderAddrEx;
            parameters[10].Value = model.OrderAttach;
            parameters[11].Value = model.OrderSums;
            parameters[12].Value = model.Sender;
            parameters[13].Value = model.SendTime;
            parameters[14].Value = model.CallPhoneNo;
            parameters[15].Value = model.P2Sign;
            parameters[16].Value = model.SendFee;
            parameters[17].Value = model.paymode;
            parameters[18].Value = model.paytime;
            parameters[19].Value = model.paymoney;
            parameters[20].Value = model.paystate;
            parameters[21].Value = model.SetStateTime;
            parameters[22].Value = model.UserId;
            parameters[23].Value = model.TogoId;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        public IList<CustorderInfo> GetInderModel()
        {
            IList<CustorderInfo> infos = new List<CustorderInfo>();


            string sql = "select top(10) Unid,OrderDateTime,OrderRcver,OrderSums,TogoId,(select Name from Points where Unid=custorder.TogoId) as TogoName,(select top(1)  FoodUnid from Foodlist where orderid=custorder.orderid) as Foodid   from custorder   where  OrderStatus=@OrderStatus and TogoId<>1 order by Unid desc ";

            SqlParameter parameter = new SqlParameter("@OrderStatus", SqlDbType.Int, 4);
            parameter.Value = 3;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                while (dr.Read())
                {
                    CustorderInfo info = new CustorderInfo();
                    info.Unid = HJConvert.ToInt32(dr["Unid"]);
                    info.OrderDateTime = HJConvert.ToDateTime(dr["OrderDateTime"]);
                    info.OrderRcver = HJConvert.ToString(dr["OrderRcver"]);
                    info.TogoName = HJConvert.ToString(dr["TogoName"]);
                    info.OrderSums = HJConvert.ToDecimal(dr["OrderSums"]);
                    info.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    info.FoodName = (string)SQLHelper.ExecuteScalar(CommandType.Text, "select FoodName from Foodinfo where Unid=" + HJConvert.ToInt32(dr["Foodid"]) + "  ", null);//
                    infos.Add(info);
                }
            }
            return infos;
        }
        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>Unid</param>
        /// <returns>CustorderInfo</returns>
        public CustorderInfo GetModel(int Unid)
        {

            return GetModel(Unid.ToString());
        }


        /// <summary>
        /// 根据订单自增id 或者 订单编号  获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>Unid</param>
        /// <returns>CustorderInfo</returns>
        public CustorderInfo GetModel(string orderid)
        {
            string sql = "select a.*,b.name as TogoName,b.Comm as TogoTel,b.senttime as SentTime, c.name as CustomerName from custorder as a left join points as b on a.togoid = b.unid left join ecustomer as c on a.userid =c.dataid where ";
            if (orderid.Length >= 10)
            {
                sql += " a.orderid= '" + orderid.Trim() + "'";
            }
            else
            {
                sql += " a.Unid=" + orderid;
            }

            CustorderInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (dr.Read())
                {
                    model = new CustorderInfo();
                    model.Unid = HJConvert.ToInt32(dr["Unid"]);
                    model.InUse = HJConvert.ToString(dr["InUse"]);
                    model.OrderDateTime = HJConvert.ToDateTime(dr["OrderDateTime"]);
                    model.OrderChecker = HJConvert.ToInt32(dr["OrderChecker"]);
                    model.OrderStatus = HJConvert.ToInt32(dr["OrderStatus"]);
                    model.OrderRcver = HJConvert.ToString(dr["OrderRcver"]);
                    model.OrderComm = HJConvert.ToString(dr["OrderComm"]);
                    model.OrderAddress = HJConvert.ToString(dr["OrderAddress"]);
                    model.AddressText = HJConvert.ToString(dr["AddressText"]);
                    model.OrderAddrEx = HJConvert.ToString(dr["OrderAddrEx"]);
                    model.OrderAttach = HJConvert.ToString(dr["OrderAttach"]);
                    model.OrderSums = HJConvert.ToDecimal(dr["OrderSums"]);
                    model.Sender = HJConvert.ToString(dr["Sender"]);
                    model.SendTime = HJConvert.ToDateTime(dr["SendTime"]);
                    model.CallPhoneNo = HJConvert.ToString(dr["CallPhoneNo"]);
                    model.P2Sign = HJConvert.ToString(dr["P2Sign"]);
                    model.SendFee = HJConvert.ToDecimal(dr["SendFee"]);
                    model.paymode = HJConvert.ToInt32(dr["paymode"]);
                    model.paytime = HJConvert.ToDateTime(dr["paytime"]);
                    model.paymoney = HJConvert.ToDecimal(dr["paymoney"]);
                    model.paystate = HJConvert.ToInt32(dr["paystate"]);
                    model.SetStateTime = HJConvert.ToDateTime(dr["SetStateTime"]);
                    model.UserId = HJConvert.ToInt32(dr["UserId"]);
                    model.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    model.TogoName = HJConvert.ToString(dr["TogoName"]);
                    model.orderid = HJConvert.ToString(dr["orderid"]);
                    model.SystemUserId = HJConvert.ToInt32(dr["SystemUserId"]);
                    model.OldStatus = HJConvert.ToInt32(dr["OldStatus"]);
                    model.IsShopSet = HJConvert.ToInt32(dr["IsShopSet"]);
                    model.deliversiteid = HJConvert.ToInt32(dr["deliversiteid"]);
                    model.deliverheaderid = HJConvert.ToInt32(dr["deliverheaderid"]);
                    model.deliverid = HJConvert.ToInt32(dr["deliverid"]);
                    model.sendstate = HJConvert.ToInt32(dr["sendstate"]);
                    model.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    model.ReveInt2 = HJConvert.ToInt32(dr["ReveInt2"]);
                    model.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
                    model.ReveVar2 = HJConvert.ToString(dr["ReveVar2"]);
                    model.ReveDate1 = HJConvert.ToDateTime(dr["ReveDate1"]);
                    model.ReveDate2 = HJConvert.ToDateTime(dr["ReveDate2"]);
                    model.OldPrice = HJConvert.ToDecimal(dr["OldPrice"]);
                    model.fromweb = HJConvert.ToString(dr["fromweb"]);
                    model.TogoTel = HJConvert.ToString(dr["TogoTel"]);
                    model.OldPrice = HJConvert.ToDecimal(dr["OldPrice"]);
                    model.shopdiscountmoney = HJConvert.ToDecimal(dr["shopdiscountmoney"]);
                    model.cardpay = HJConvert.ToDecimal(dr["cardpay"]);
                    model.PayOrderId = HJConvert.ToString(dr["PayOrderId"]);
                    model.CustomerName = HJConvert.ToString(dr["CustomerName"]);
                    model.SentTime = HJConvert.ToInt32(dr["SentTime"]);
                    model.shopCancel = HJConvert.ToInt32(dr["shopCancel"]);
                    model.Cancelreason = HJConvert.ToString(dr["Cancelreason"]);
                    model.iscount = HJConvert.ToInt32(dr["iscount"]);

                    model.Commentstate = HJConvert.ToInt32(dr["Commentstate"]);//后期添加 无法判断是否评论 2015-12-10

                    model.promotionmoney = HJConvert.ToDecimal(dr["webpromotionmoney"]) + HJConvert.ToDecimal(dr["shoppromotionmoney"]);
                    model.needpaymoney = model.OrderSums - model.cardpay - model.promotionmoney;
                    model.Packagefee = HJConvert.ToDecimal(dr["packagefee"]);


                }
            }
            return model;
        }


        /// <summary>
        /// 根据订单自增id 或者 订单编号  获取记录所有字段
        /// </summary>
        /// <param>Unid</param>
        /// <returns>CustorderInfo</returns>
        public CustorderInfo GetSqlWhere(string where)
        {
            string sql = "SELECT TOP 1 Unid FROM dbo.Custorder WHERE " + where;

            CustorderInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (dr.Read())
                {
                    model = new CustorderInfo();
                    model.Unid = HJConvert.ToInt32(dr["Unid"]);
                }
            }
            return model;
        }


        /// <summary>
        /// 是否可评论
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int OrderDataID(int UserID, int id)
        {
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, "select  Unid from Custorder where Commentstate=0 and UserId=" + UserID + "and TogoId=" + id + "and OrderDateTime between '" + DateTime.Now.ToShortDateString() + "' and  '" + DateTime.Now.ToString() + "'", null));
        }
        public int OrderDataIDstate(int UserID, int id)
        {
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, "select  Unid from Custorder where Commentstate=1 and UserId=" + UserID + " and TogoId=" + id + " ", null));
        }
        //评论更新订单已评论了
        public void OrderUnid(int Unid)
        {
            int i = Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, "update Custorder set Commentstate=1 where Unid=" + Unid + "", null));
        }

        /// <summary>
        /// 获得总记录数
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int GetCount(string strWhere)
        {
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "custorder"), new SqlParameter("@strWhere", strWhere)));
        }

        public int GetCountFix(string strWhere)
        {
            string sql = "SELECT COUNT(*) FROM custorder INNER JOIN ETogo ON TogoId = ETogo.DataId INNER JOIN ECustomer ON ECustomer.DataId = UserId";

            if (string.IsNullOrEmpty(strWhere) == false)
            {
                sql += " WHERE " + strWhere;
            }

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, sql, null));
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
        public IList<CustorderInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<CustorderInfo> infos = new List<CustorderInfo>();
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
            parameters[0].Value = "custorder";
            parameters[1].Value = "*,(select Name from Points where Unid=custorder.TogoId) as TogoName,(select senttime from Points where Unid=custorder.TogoId) as SentTime,(select Picture from Points where Unid=custorder.TogoId) as TogoPic";
            parameters[2].Value = "Unid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    CustorderInfo info = new CustorderInfo();
                    info.Unid = HJConvert.ToInt32(dr["Unid"]);
                    info.InUse = HJConvert.ToString(dr["InUse"]);
                    info.OrderDateTime = HJConvert.ToDateTime(dr["OrderDateTime"]);
                    info.OrderChecker = HJConvert.ToInt32(dr["OrderChecker"]);
                    info.OrderStatus = HJConvert.ToInt32(dr["OrderStatus"]);
                    info.OrderRcver = HJConvert.ToString(dr["OrderRcver"]);
                    info.OrderComm = HJConvert.ToString(dr["OrderComm"]);
                    info.OrderAddress = HJConvert.ToString(dr["OrderAddress"]);
                    info.AddressText = HJConvert.ToString(dr["AddressText"]);
                    info.OrderAddrEx = HJConvert.ToString(dr["OrderAddrEx"]);
                    info.OrderAttach = HJConvert.ToString(dr["OrderAttach"]);
                    info.OrderSums = HJConvert.ToDecimal(dr["OrderSums"]);
                    info.Sender = HJConvert.ToString(dr["Sender"]);
                    info.SendTime = HJConvert.ToDateTime(dr["SendTime"]);
                    info.CallPhoneNo = HJConvert.ToString(dr["CallPhoneNo"]);
                    info.P2Sign = HJConvert.ToString(dr["P2Sign"]);
                    info.SendFee = HJConvert.ToDecimal(dr["SendFee"]);
                    info.paymode = HJConvert.ToInt32(dr["paymode"]);
                    info.paytime = HJConvert.ToDateTime(dr["paytime"]);
                    info.paymoney = HJConvert.ToDecimal(dr["paymoney"]);
                    info.paystate = HJConvert.ToInt32(dr["paystate"]);
                    info.SetStateTime = HJConvert.ToDateTime(dr["SetStateTime"]);
                    info.UserId = HJConvert.ToInt32(dr["UserId"]);
                    info.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    info.cityid = HJConvert.ToInt32(dr["cityid"]);
                    info.TogoName = HJConvert.ToString(dr["TogoName"]);
                    info.orderid = HJConvert.ToString(dr["orderid"]);
                    info.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    info.ReveInt2 = HJConvert.ToInt32(dr["ReveInt2"]);
                    info.IsShopSet = HJConvert.ToInt32(dr["IsShopSet"]);
                    info.sendstate = HJConvert.ToInt32(dr["sendstate"]);
                    info.ReveVar2 = HJConvert.ToString(dr["ReveVar2"]);
                    info.cardpay = HJConvert.ToDecimal(dr["cardpay"]);
                    info.TogoPic = HJConvert.ToString(dr["TogoPic"]);
                    info.SentTime = HJConvert.ToInt32(dr["SentTime"]);
                    info.shopCancel = HJConvert.ToInt32(dr["shopCancel"]);
                    info.Cancelreason = HJConvert.ToString(dr["Cancelreason"]);
                    info.promotionmoney = HJConvert.ToDecimal(dr["webpromotionmoney"]) + HJConvert.ToDecimal(dr["shoppromotionmoney"]);

                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 查询打印
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<CustorderInfo> GetListt(string strWhere)
        {
            return GetListFix(100, 1, "custorder.unid in (" + strWhere + ")", "OrderDateTime", 1);
        }
        /// <summary>
        /// 获取列表 此方法在翻页最后一页时似乎读取数据失败了 使用的时候注意测试
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<CustorderInfo> GetListFix(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<CustorderInfo> infos = new List<CustorderInfo>();
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
            parameters[0].Value = "custorder left JOIN points ON custorder.TogoId = points.unid left JOIN ECustomer ON ECustomer.DataId = custorder.UserId left JOIN hurryorder ON hurryorder.oid = custorder.orderid";
            parameters[1].Value = "custorder.*,points.name as TogoName,ECustomer.Name AS CustomerName,points.comm As TogoTel, hurryorder.ReveInt as hurhav ";
            parameters[2].Value = "custorder.Unid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "wg_pageselectprifix", parameters))
            {
                while (dr.Read())
                {
                    CustorderInfo info = new CustorderInfo();
                    info.Unid = HJConvert.ToInt32(dr["Unid"]);
                    info.InUse = HJConvert.ToString(dr["InUse"]);
                    info.OrderDateTime = HJConvert.ToDateTime(dr["OrderDateTime"]);
                    info.OrderChecker = HJConvert.ToInt32(dr["OrderChecker"]);
                    info.OrderStatus = HJConvert.ToInt32(dr["OrderStatus"]);
                    info.OrderRcver = HJConvert.ToString(dr["OrderRcver"]);
                    info.OrderComm = HJConvert.ToString(dr["OrderComm"]);
                    info.OrderAddress = HJConvert.ToString(dr["OrderAddress"]);
                    info.AddressText = HJConvert.ToString(dr["AddressText"]);
                    info.OrderAddrEx = HJConvert.ToString(dr["OrderAddrEx"]);
                    info.OrderAttach = HJConvert.ToString(dr["OrderAttach"]);
                    info.OrderSums = HJConvert.ToDecimal(dr["OrderSums"]);

                    info.cardpay = HJConvert.ToDecimal(dr["cardpay"]);
                    info.Packagefee = HJConvert.ToDecimal(dr["Packagefee"]);

                    info.promotionmoney = HJConvert.ToDecimal(dr["webpromotionmoney"]) + HJConvert.ToDecimal(dr["shoppromotionmoney"]);
                    info.needpaymoney = info.OrderSums - info.cardpay - info.promotionmoney;

                    info.Sender = HJConvert.ToString(dr["Sender"]);
                    info.SendTime = HJConvert.ToDateTime(dr["SendTime"]);
                    info.CallPhoneNo = HJConvert.ToString(dr["CallPhoneNo"]);
                    info.P2Sign = HJConvert.ToString(dr["P2Sign"]);
                    info.SendFee = HJConvert.ToDecimal(dr["SendFee"]);
                    info.paymode = HJConvert.ToInt32(dr["paymode"]);
                    info.paytime = HJConvert.ToDateTime(dr["paytime"]);
                    info.paymoney = HJConvert.ToDecimal(dr["paymoney"]);
                    info.paystate = HJConvert.ToInt32(dr["paystate"]);
                    info.SetStateTime = HJConvert.ToDateTime(dr["SetStateTime"]);
                    info.UserId = HJConvert.ToInt32(dr["UserId"]);
                    info.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    info.TogoName = HJConvert.ToString(dr["TogoName"]);
                    info.CustomerName = HJConvert.ToString(dr["CustomerName"]);
                    info.TogoTel = HJConvert.ToString(dr["TogoTel"]);
                    info.orderid = HJConvert.ToString(dr["orderid"]);
                    info.writer = HJConvert.ToString(dr["writer"]);
                    info.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
                    info.shopdiscountmoney = HJConvert.ToDecimal(dr["shopdiscountmoney"]);
                    info.OldPrice = HJConvert.ToDecimal(dr["OldPrice"]);
                    info.sendstate = HJConvert.ToInt32(dr["sendstate"]);
                    info.IsShopSet = HJConvert.ToInt32(dr["IsShopSet"]);
                    info.deliversiteid = HJConvert.ToInt32(dr["deliversiteid"]);
                    info.shopCancel = HJConvert.ToInt32(dr["shopCancel"]);
                    info.Cancelreason = HJConvert.ToString(dr["Cancelreason"]);
                    info.hurhav = HJConvert.ToInt32(dr["hurhav"]);



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
        public int DelCustorder(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from custorder where Unid=@Unid");
            SqlParameter[] Para =
            {
                new SqlParameter("@Unid",SqlDbType.Int,20)
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
            str.Append("delete from custorder where Unid in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }


        /// <summary>
        /// 批量设置订单为处理成功
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        public int SetOkList(string IdList)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update custorder set orderstatus=3 where Unid in ({0})");
            IList<CustorderInfo> orders = GetList(50, 1, "unid in (" + IdList + ") and orderstatus <> 3", "unid", 0);
            foreach (var item in orders)
            {
                AddPoint(item.orderid);
            }

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 根据订单编号更改打印机状态为成功（add by yangxioalong@ihangjing.com）
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int UpdateOrderState(int orderId)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update custorder set orderstatus=3 where Unid=@Unid;");

            SqlParameter[] Para =
            {
                new SqlParameter("@Unid",SqlDbType.Int,20)
            };
            Para[0].Value = orderId;
            //AddPoint(orderId);

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 根据订单编号更改打印机状态为成功（add by yangxioalong@ihangjing.com）
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int UpdateOrderState(string orderId)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update custorder set orderstatus=3 where orderId=@orderId;");

            SqlParameter[] Para =
            {
                new SqlParameter("@orderId",SqlDbType.VarChar,50)
            };
            Para[0].Value = orderId;
            AddPoint(orderId);

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }


        /// <summary>
        /// 当打印机获取完订单时，把这个值设置成当前时间。 （add by yangxioalong@ihangjing.com）
        /// 过一个时间段后，如果此订单的状态还没有更改成成功，则为失败。
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int UpdateOrderStateTime(int orderId)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update custorder set SetStateTime=" + DateTime.Now + " where Unid=@Unid");

            SqlParameter[] Para =
            {
                new SqlParameter("@Unid",SqlDbType.Int,20)
            };
            Para[0].Value = orderId;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }


        /// <summary>
        /// 修改订单的状态。
        /// </summary>
        /// <param name="OrderID">订单编号</param>
        /// <param name="State">要修改的订单状态</param>
        public int UpdataState(string OrderID, int State)
        {
            StringBuilder str = new StringBuilder();

            if (State == 2)
            {
                str.Append("update custorder set orderstatus=@orderstatus,SetStateTime='" + DateTime.Now + "' where OrderID=@OrderID");
            }
            else if (State == 5)
            {
                str.Append("update custorder set orderstatus=@orderstatus,IsShopSet=2 where OrderID=@OrderID");
            }
            else
            {
                str.Append("update custorder set orderstatus=@orderstatus where OrderID=@OrderID");
            }

            SqlParameter[] Para =
            {
                new SqlParameter("@OrderID",SqlDbType.VarChar,50),
                new SqlParameter("@orderstatus",SqlDbType.Int)
            };
            Para[0].Value = OrderID;
            Para[1].Value = State;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 修改订单的状态。
        /// </summary>
        /// <param name="OrderID">订单编号</param>
        /// <param name="State">要修改的订单状态</param>
        public int UpdataState(int OrderID, int State)
        {

            StringBuilder str = new StringBuilder();

            if (State == 2)
            {
                str.Append("update custorder set orderstatus=@orderstatus,SetStateTime='" + DateTime.Now + "' where Unid=@Unid");
            }
            else
            {
                str.Append("update custorder set orderstatus=@orderstatus where Unid=@Unid");
            }

            SqlParameter[] Para =
            {
                new SqlParameter("@Unid",SqlDbType.Int,20),
                new SqlParameter("@orderstatus",SqlDbType.Int)
            };
            Para[0].Value = OrderID;
            Para[1].Value = State;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 修改订单的处理人
        /// </summary>
        /// <param name="OrderID">订单编号</param>
        /// <param name="State">处理人</param>
        public int UpdataState(int OrderID, string writer)
        {

            StringBuilder str = new StringBuilder();
            str.Append("update custorder set writer=@writer where Unid=@OrderID");
            SqlParameter[] Para =
            {
                new SqlParameter("@OrderID",SqlDbType.VarChar,20),
                new SqlParameter("@writer",SqlDbType.VarChar , 20)
            };
            Para[0].Value = OrderID;
            Para[1].Value = writer;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }



        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdatePayState(Hangjing.Model.CustorderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update custorder set ");
            strSql.Append("paymode=@paymode,");
            strSql.Append("paytime=@paytime,");
            strSql.Append("paymoney=@paymoney,");
            strSql.Append("paystate=@paystate");
            strSql.Append(" where Unid=@Unid ");
            SqlParameter[] parameters =
            {
                new SqlParameter("@Unid", SqlDbType.Int,20),
                new SqlParameter("@paymode", SqlDbType.Int,4),
                new SqlParameter("@paytime", SqlDbType.DateTime),
                new SqlParameter("@paymoney", SqlDbType.Decimal,5),
                new SqlParameter("@paystate", SqlDbType.Int,4)
            };
            parameters[0].Value = model.Unid;
            parameters[1].Value = model.paymode;
            parameters[2].Value = model.paytime;
            parameters[3].Value = model.paymoney;
            parameters[4].Value = model.paystate;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /*--月统计
        select month(ordertime),count(orderid) from etogoorder where year(ordertime) = '2010' group by month(ordertime)

        --日统计
        select day(ordertime),count(orderid) from etogoorder where year(ordertime) = '2010' and month(ordertime)='7'  group by day(ordertime)

        --小时统计
        select datepart(hh,ordertime),count(orderid) from etogoorder where year(ordertime) = '2010' and month(ordertime)='7' and day(ordertime)='16'  group by datepart(hh,ordertime)
        */

        /// <summary>
        /// Type:1 小时统计 2 日统计 3 周统计 4 月统计 5 年统计
        /// Year 需要统计的年份 Month需要统计的月份 Day需要统计的日 
        /// 小时统计需要Year Month Day
        /// 日统计需要Year Month 
        /// 月统计需要 Year
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public IList<OrderCountInfo> GetOrderCount(int Type, string Year, string Month, string Day)
        {
            IList<OrderCountInfo> list = new List<OrderCountInfo>();

            string strSql = "";

            switch (Type)
            {
                case 1: strSql = "select datepart(hh,OrderDateTime) as CountKey,count(orderid) as CountIntValue,Sum(OrderSums) as CountDecimalPrice from custorder where year(OrderDateTime) = '" + Year + "' and month(OrderDateTime)='" + Month + "'  and day(OrderDateTime)='" + Day + "'  group by datepart(hh,OrderDateTime)"; break;
                case 2: strSql = "select day(OrderDateTime) as CountKey,count(orderid) as CountIntValue,Sum(OrderSums) as CountDecimalPrice from custorder where year(OrderDateTime) = '" + Year + "' and month(OrderDateTime)='" + Month + "'  group by day(OrderDateTime)"; break;
                case 3: strSql = ""; break;
                case 4: strSql = "select month(OrderDateTime) as CountKey,count(orderid) as CountIntValue,Sum(OrderSums) as CountDecimalPrice from custorder where year(OrderDateTime) = '" + Year + "' group by month(OrderDateTime)"; break;
                case 5: strSql = ""; break;
            }

            OrderCountInfo info = new OrderCountInfo();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, strSql, null))
            {
                while (dr.Read())
                {
                    info = new OrderCountInfo();

                    info.CountKey = HJConvert.ToString(dr["CountKey"]);
                    info.CountIntValue = HJConvert.ToInt32(dr["CountIntValue"]);
                    info.CountDecimalValue = HJConvert.ToDecimal(dr["CountDecimalPrice"]);

                    list.Add(info);
                }
            }

            return list;
        }


        /// <summary>
        /// Type:1 小时统计 2 日统计 3 周统计 4 月统计 5 年统计
        /// Year 需要统计的年份 Month需要统计的月份 Day需要统计的日 
        /// 小时统计需要Year Month Day
        /// 日统计需要Year Month 
        /// 月统计需要 Year
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public IList<OrderCountInfo> GetOrderCount(int Type, string Year, string Month, string Day, string where)
        {
            IList<OrderCountInfo> list = new List<OrderCountInfo>();

            string strSql = "";

            switch (Type)
            {
                case 1: strSql = "select datepart(hh,OrderDateTime) as CountKey,count(orderid) as CountIntValue,Sum(OrderSums) as CountDecimalPrice,Sum(SendFee) as CountSendFee,(select sum(OrderSums) from custorder where togoid = 1 and year(OrderDateTime) = '" + Year + "' and month(OrderDateTime)='" + Month + "'  and day(OrderDateTime)='" + Day + "'  and " + where + "    group by datepart(hh,OrderDateTime)) as CountDrinkPrice from custorder where year(OrderDateTime) = '" + Year + "' and month(OrderDateTime)='" + Month + "'  and day(OrderDateTime)='" + Day + "'  and " + where + "    group by datepart(hh,OrderDateTime)"; break;
                case 2: strSql = "select day(OrderDateTime) as CountKey,count(orderid) as CountIntValue,Sum(OrderSums) as CountDecimalPrice,Sum(SendFee) as CountSendFee,(select top 1 sum(OrderSums) from custorder where togoid=1 and year(OrderDateTime) = '" + Year + "' and month(OrderDateTime)='" + Month + "'  and " + where + "    group by day(OrderDateTime)) as CountDrinkPrice from custorder where year(OrderDateTime) = '" + Year + "' and month(OrderDateTime)='" + Month + "'  and " + where + "    group by day(OrderDateTime)"; break;
                case 3: strSql = ""; break;
                case 4: strSql = "select month(OrderDateTime) as CountKey,count(orderid) as CountIntValue,Sum(OrderSums) as CountDecimalPrice,Sum(SendFee) as CountSendFee,(select top 1 sum(OrderSums) from custorder where togoid = 1 and year(OrderDateTime) = '" + Year + "'  and " + where + "  group by month(OrderDateTime)) as CountDrinkPrice from custorder where year(OrderDateTime) = '" + Year + "'  and " + where + "   group by month(OrderDateTime)"; break;
                case 5: strSql = ""; break;
                case 7: strSql = "select datepart(hh,OrderDateTime) as CountKey,count(orderid) as CountIntValue,Sum(OrderSums) as CountDecimalPrice,Sum(SendFee) as CountSendFee from custorder where " + where + "    group by datepart(hh,OrderDateTime)"; break;

            }


            OrderCountInfo info = new OrderCountInfo();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, strSql, null))
            {
                while (dr.Read())
                {
                    info = new OrderCountInfo();

                    info.CountKey = HJConvert.ToString(dr["CountKey"]);
                    info.CountIntValue = HJConvert.ToInt32(dr["CountIntValue"]);
                    info.CountDecimalValue = HJConvert.ToDecimal(dr["CountDecimalPrice"]);
                    info.CountSendFee = HJConvert.ToInt32(dr["CountSendFee"]);

                    list.Add(info);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取报表记录总是和总金额(getmoney页面)
        /// </summary>
        /// <param name="togoNum"></param>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        public CustorderInfo GetCountAndTotal1(string where)
        {
            string sql = "select sum(OrderSums) as  OrderTotal,Count(*) as OrderCount,sum(shopdiscountmoney) as shopdiscountmoney,sum (case when deliversiteid =0  then (shopdiscountmoney) else 0   end)  as nopaymoeny,SUM(SendFee) as SendFee, SUM(cardpay) as cardpay from Custorder where " + where;

            CustorderInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (dr.Read())
                {
                    model = new CustorderInfo();
                    model.OrderCount = HJConvert.ToInt32(dr["OrderCount"]);
                    model.OrderTotal = HJConvert.ToDecimal(dr["OrderTotal"]);
                    model.shopdiscountmoney = HJConvert.ToDecimal(dr["shopdiscountmoney"]);
                    model.SendFee = HJConvert.ToDecimal(dr["SendFee"]);  //配送费
                    model.cardpay = HJConvert.ToDecimal(dr["cardpay"]);  //优惠卷

                    model.CallPhoneNo = HJConvert.ToDecimal(dr["nopaymoeny"]).ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 每日销售金额统计
        /// </summary>
        /// <param name="SDate"></param>
        /// <param name="EDate"></param>
        /// <returns></returns>
        public IList<ShopOrderCountInfo> GetShopDayOrderCount(string TogoId, DateTime SDate, DateTime EDate)
        {
            //如查询日期是2011-5-30 日 则 两个参数分别是 2011-5-30  2011-5-31
            //如查询日期是2011-5-25 -- 2011-5-30 则 两个参数分别是 2011-5-25  2011-5-31
            //select sum(OrderSums) as OrderSum, OrderDateTime from Custorder where TogoId=1 and OrderDateTime < '2011-5-28 00:00:00' and  OrderDateTime > '2011-5-24 00:00:00' group by day(OrderDateTime)

            string strSql = "select sum(OrderSums) as CountDecimalValue,count(Unid) as OrderCount,  OrderDateTime from custorder where TogoId=" + TogoId + " and OrderDateTime < '" + EDate.AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00' and  OrderDateTime > '" + SDate.ToString("yyyy-MM-dd") + " 00:00:00' group by day(OrderDateTime)";

            IList<ShopOrderCountInfo> list = new List<ShopOrderCountInfo>();

            ShopOrderCountInfo info = new ShopOrderCountInfo();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, strSql, null))
            {
                while (dr.Read())
                {
                    info = new ShopOrderCountInfo();

                    info.CountDecimalValue = HJConvert.ToDecimal(dr["CountDecimalValue"]);
                    info.OrderCount = HJConvert.ToInt32(dr["OrderCount"]);
                    info.OrderDate = HJConvert.ToDateTime(dr["OrderDateTime"]);

                    list.Add(info);
                }
            }

            return list;
        }

        /// <summary>
        /// 订单加分,判断是不是一个订单  此方法需要改进以及测试 2011.6.10
        /// </summary>
        /// <param name="orerid"></param>
        /// <returns></returns>
        public int AddPoint(string orerid)
        {
            CustorderInfo model = new Custorder().GetModel(orerid);

            WebBasicInfo mysetpoint = new WebBasic().GetModel(42);
            int ratpoint = Convert.ToInt32(mysetpoint.Value);
            //积分计算公式： 订单总金额*积分倍数
            int point = Convert.ToInt32(Convert.ToInt32(model.OrderSums) * ratpoint);

            //int point = Convert.ToInt32(model.OrderSums);
            SqlParameter[] Para =
            {
                new SqlParameter("@userid",SqlDbType.Int,5),
                new SqlParameter("@addpoint",SqlDbType.Int , 5),
                new SqlParameter("@orderid",SqlDbType.VarChar,20),
            };
            Para[0].Value = model.UserId;
            Para[1].Value = point;
            Para[2].Value = model.orderid;

            SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "addOrderPoint", Para);



            ECustomer daluser = new ECustomer();
            ECustomerInfo user = daluser.GetModel(model.UserId);
            if (user != null && user.RID.Trim() != "")
            {
                //推荐人加分
                ECustomerInfo tuser = daluser.GetModel(Convert.ToInt32(user.RID));
                if (tuser != null)
                {
                    string mysql = "UserID = " + user.DataID;
                    string sql = "";
                    int count = GetCount(mysql);
                    if (count == 1)
                    {
                        WebBasicInfo myset = new WebBasic().GetModel(30);
                        int rat = Convert.ToInt32(myset.Value);
                        sql += "update ECustomer set point = point + " + rat + "  where dataid = " + tuser.DataID + ";";
                        sql += "insert into EPointRecord(userid , point , event , time)values(" + tuser.DataID + " , " + rat + "  ,'你推荐的" + user.Name + "点外成功,获取积分" + rat + "个' , '" + DateTime.Now + "');";
                        SQLHelper.ExecuteNonQuery(CommandType.Text, sql, null);
                    }
                }


            }

            return 1;
        }


        /// <summary>
        /// 下订单
        /// </summary>
        /// <param name="list"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public IList<ROrderinfo> SubmitOrder(IList<Hangjing.Model.ETogoShoppingCart> list, EAddressInfo address)
        {
            IList<ROrderinfo> my_list = new List<ROrderinfo>();
            ArrayList cmdtextlist = new ArrayList();

            ArrayList paraslist = new ArrayList();
            string weixinopenid = "";
            decimal myhavemoney = 0;//用户总共的金额
            int pastta = 0; //余额支付状态
            if (address.UserID > 0)
            {
                ECustomerInfo user = new ECustomer().GetModel(address.UserID);
                if (user != null)
                {
                    myhavemoney = user.Usermoney;
                    weixinopenid = user.PayPWDQuestion;
                }
            }
            string sign = HjNetHelper.GetTime();

            decimal cardpay = 0;//优惠券支付金额(只是支付商品金额)
            string cids = "";
            int pointrat = 1;//积分倍数。

            //优惠券相关.
            if (address.UserID > 0 && address.isuercard == 1 && address.shopcardjson != "")
            {
                //店铺券使用记录
                IList<ShopCardInfo> userShopCardlist = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<IList<ShopCardInfo>>(address.shopcardjson);
                foreach (ShopCardInfo item in userShopCardlist)
                {
                    cids += item.CID + ",";

                    StringBuilder strSqlcard = new StringBuilder("update ShopCard set isused =1  where cid =" + item.CID + ";");
                    strSqlcard.Append("insert into ShopCardUserRecord(");
                    strSqlcard.Append("cardnum,ckey,AddDate,State,Point,batid,canday,Inve1,Inve2,userid,username,cmoney,ReveInt,ReveVar,usergettime,isbuy,buyuid,buytime)");
                    strSqlcard.Append(" values (");
                    strSqlcard.Append("@cardnum,@ckey,@AddDate,@State,@Point,@batid,@canday,@Inve1,@Inve2,@userid,@username,@cmoney,@ReveInt,@ReveVar,@usergettime,@isbuy,@buyuid,@buytime)");

                    SqlParameter[] parameterscard =
                    {
                        new SqlParameter("@cardnum", SqlDbType.VarChar,50),
                        new SqlParameter("@ckey", SqlDbType.VarChar,50),
                        new SqlParameter("@AddDate", SqlDbType.DateTime),
                        new SqlParameter("@State", SqlDbType.Int,4),
                        new SqlParameter("@point", SqlDbType.Decimal) ,
                        new SqlParameter("@batid", SqlDbType.Int,4),
                        new SqlParameter("@canday", SqlDbType.Int,4),
                        new SqlParameter("@Inve1", SqlDbType.Int,4),
                        new SqlParameter("@Inve2", SqlDbType.VarChar,256),
                        new SqlParameter("@userid", SqlDbType.Int,4),
                        new SqlParameter("@username", SqlDbType.VarChar,256),
                        new SqlParameter("@cmoney", SqlDbType.Decimal,5),
                        new SqlParameter("@ReveInt", SqlDbType.Int,4),
                        new SqlParameter("@ReveVar", SqlDbType.VarChar,256),
                        new SqlParameter("@usergettime", SqlDbType.DateTime),
                        new SqlParameter("@isbuy", SqlDbType.Int,4),
                        new SqlParameter("@buyuid", SqlDbType.Int,4),
                        new SqlParameter("@buytime", SqlDbType.DateTime)
                    };
                    parameterscard[0].Value = "";
                    parameterscard[1].Value = item.ckey;
                    parameterscard[2].Value = DateTime.Now;
                    parameterscard[3].Value = 0;
                    parameterscard[4].Value = item.Point;
                    parameterscard[5].Value = 0;
                    parameterscard[6].Value = 0;
                    parameterscard[7].Value = 0;
                    parameterscard[8].Value = "";
                    parameterscard[9].Value = address.UserID;
                    parameterscard[10].Value = address.CustomerName;
                    parameterscard[11].Value = 0;
                    parameterscard[12].Value = item.ReveInt1;
                    parameterscard[13].Value = "";
                    parameterscard[14].Value = DateTime.Now;
                    parameterscard[15].Value = 0;
                    parameterscard[16].Value = 0;
                    parameterscard[17].Value = DateTime.Now;
                    cmdtextlist.Add(strSqlcard.ToString());
                    paraslist.Add(parameterscard);

                    switch (item.ReveInt1)
                    {
                        case 1:
                        case 2:
                            cardpay += item.Point;
                            break;
                        case 3:
                            pointrat *= Convert.ToInt32(item.Point);
                            break;
                        default:
                            break;
                    }

                }
            }

            if (address.redpackage != null && address.redpackage.id > 0)
            {
                StringBuilder strSqlmsg = new StringBuilder("update msgpacket set num =1  where id =" + address.redpackage.id + ";");
                strSqlmsg.Append("insert into msgpacketrecord(");
                strSqlmsg.Append("pid,pulltime,pullmoney,pulltel,reveint,revevar)");
                strSqlmsg.Append(" values (");
                strSqlmsg.Append("@pid,@pulltime,@pullmoney,@pulltel,@reveint,@revevar)");
                strSqlmsg.Append(";select @@IDENTITY");
                SqlParameter[] parametersmsg =
                {
                        new SqlParameter("@pid", SqlDbType.Int,4),
                        new SqlParameter("@pulltime", SqlDbType.DateTime),
                        new SqlParameter("@pullmoney", SqlDbType.Decimal,9),
                        new SqlParameter("@reveint", SqlDbType.Int,4),
                        new SqlParameter("@revevar", SqlDbType.VarChar,50),
                        new SqlParameter("@pulltel", SqlDbType.VarChar,50)
                };
                parametersmsg[0].Value = address.redpackage.id;
                parametersmsg[1].Value = DateTime.Now;
                parametersmsg[2].Value = address.redpackage.alltotal;
                parametersmsg[3].Value = 0;
                parametersmsg[4].Value = "0";
                parametersmsg[5].Value = address.redpackage.ReveVar;
                cmdtextlist.Add(strSqlmsg.ToString());
                paraslist.Add(parametersmsg);

                cardpay += address.redpackage.alltotal;

            }


            cids = HJConvert.dellast(cids);

            string updatefoodsql = "";
            int i = 0, j = 0;
            for (i = 0; i < list.Count; i++)//点了几个商家.就会有多少个订单.
            {
                string orderid = HjNetHelper.GetTime() + address.Mobilephone.Substring(address.Mobilephone.Length - 4) + i;

                int orderstatus = 2;//审核通过
                decimal currentprice = 0; //现菜品费+现送餐费-acountpay =用户还要支付的金额
                decimal foodmoney = 0;//菜品金额【折扣后的】
                decimal acountpay = 0;//余额支付
                decimal totalprice = 0; //现菜品费+现送餐费
                decimal oldprice = 0;//商品原价
                int foodmoneydiscount = 100;//商品折扣，100表示没有折扣，会员有等级会有折扣，88折，保存88
                decimal payshopmoney = 0;//要支付给商家的，负数表示商家要给平台的（自送，送到付款的，因为这个钱商家收了）;
                decimal PromotionMoney = 0;//促销优惠商品
                bool iswebpromotion = false;//是否平台促销，
                int IsShopSet = 0;//接单设置
                decimal Commission = 0;

                decimal packagefee = 0;

                for (int x = 0; x < list[i].ItemList.Count; x++)
                {
                    totalprice += (list[i].ItemList[x].PPrice + list[i].ItemList[j].addprice) * list[i].ItemList[x].PNum;
                    packagefee += (list[i].ItemList[x].owername) * list[i].ItemList[x].PNum;
                }
                oldprice = totalprice;

                //根据折扣信息，来计算总价
                Points togo = new Points();
                PointsInfo togomodel = togo.GetModel(list[i].TogoId);
                bool iscash = false; //是否货到付款
                if (address.paymode == 4)
                {
                    iscash = true;
                }

                if (togomodel.RcvType == 1 && (address.paymode == 3 || address.paymode == 4))
                {
                    IsShopSet = 1;
                }

                payshopmoney = getPayShopMoney(togomodel, oldprice, iscash);
                payshopmoney += packagefee;

                switch (togomodel.SN1.Trim())
                {
                    case "0": //提成
                        Commission = oldprice * togomodel.point / 100;
                        break;
                    case "1":
                        Commission = togomodel.point;
                        break;
                }

                if (list[i].ptimes > 0 && totalprice >= list[i].ptimes)
                {
                    list[i].sendfree = 0;
                }
                address.cityid = togomodel.cityid;

                //要审核订单的最低金额
                int moneydoor = Convert.ToInt32(CacheHelper.GetSetValue(34));


                totalprice += list[i].sendfree;
                totalprice += packagefee;
                currentprice = totalprice;
                currentprice -= cardpay;

                if (address.Promotions != null && address.Promotions.Count > 0)
                {
                    foreach (var item in address.Promotions)
                    {
                        PromotionMoney += item.freeSendFee;
                        if (item.shopid == 0)
                        {
                            iswebpromotion = true;
                        }

                        StringBuilder promotionSql = new StringBuilder();
                        promotionSql.Append("insert into OrderPromotion(");
                        promotionSql.Append("shopid,startdate,enddate,starttime,endtime,ptype,isopen,freeSendFee,overmoney,minusmoney,reveint1,reveint2,revevar1,revevar2,revefloat1,revefloat2");
                        promotionSql.Append(") values (");
                        promotionSql.Append("@shopid,@startdate,@enddate,@starttime,@endtime,@ptype,@isopen,@freeSendFee,@overmoney,@minusmoney,@reveint1,@reveint2,@revevar1,@revevar2,@revefloat1,@revefloat2");
                        promotionSql.Append(") ");

                        SqlParameter[] promotionparameters =
                        {
                            new SqlParameter("@shopid", SqlDbType.Int,4) ,
                            new SqlParameter("@startdate", SqlDbType.DateTime) ,
                            new SqlParameter("@enddate", SqlDbType.DateTime) ,
                            new SqlParameter("@starttime", SqlDbType.DateTime) ,
                            new SqlParameter("@endtime", SqlDbType.DateTime) ,
                            new SqlParameter("@ptype", SqlDbType.Int,4) ,
                            new SqlParameter("@isopen", SqlDbType.Int,4) ,
                            new SqlParameter("@freeSendFee", SqlDbType.Float,8) ,
                            new SqlParameter("@overmoney", SqlDbType.Int,4) ,
                            new SqlParameter("@minusmoney", SqlDbType.Int,4) ,
                            new SqlParameter("@reveint1", SqlDbType.Int,4) ,
                            new SqlParameter("@reveint2", SqlDbType.Int,4) ,
                            new SqlParameter("@revevar1", SqlDbType.VarChar,256) ,
                            new SqlParameter("@revevar2", SqlDbType.VarChar,256) ,
                            new SqlParameter("@revefloat1", SqlDbType.Float,8) ,
                            new SqlParameter("@revefloat2", SqlDbType.Float,8)

                        };

                        promotionparameters[0].Value = item.shopid;
                        promotionparameters[1].Value = item.startdate;
                        promotionparameters[2].Value = item.enddate;
                        promotionparameters[3].Value = DateTime.Now;
                        promotionparameters[4].Value = DateTime.Now;
                        promotionparameters[5].Value = item.ptype;
                        promotionparameters[6].Value = 0;
                        promotionparameters[7].Value = item.freeSendFee;
                        promotionparameters[8].Value = 0;
                        promotionparameters[9].Value = 0;
                        promotionparameters[10].Value = 0;
                        promotionparameters[11].Value = item.reveint2;
                        promotionparameters[12].Value = item.revevar1;
                        promotionparameters[13].Value = orderid;
                        promotionparameters[14].Value = 0;
                        promotionparameters[15].Value = 0;

                        cmdtextlist.Add(promotionSql.ToString());
                        paraslist.Add(promotionparameters);
                    }
                }

                currentprice -= PromotionMoney;
                if (currentprice <= 0)
                {
                    currentprice = 0;
                    pastta = 1;
                }

                int firstAutoAuth = Convert.ToInt32(CacheHelper.GetSetValue(33));//首单是否自动审核（0不是，1表示是）
                string sql = " OrderComm = '" + address.Mobilephone + "'";
                if (firstAutoAuth == 0 && GetCount(sql) == 0)
                {
                    orderstatus = 1;
                }
                else
                {
                    orderstatus = 2;
                }


                //计算余额支付
                if (address.UserID > 0)
                {
                    address.pointrat = 1;
                    address.foodmoneydiscount = 10;
                    foodmoney = address.foodmoneydiscount / 10 * totalprice;

                    foodmoneydiscount = Convert.ToInt32(address.foodmoneydiscount * 10);

                    if (address.paymode == 3 && currentprice > 0)
                    {
                        if (Convert.ToInt32(myhavemoney) > 0)
                        {
                            if (myhavemoney >= currentprice)
                            {
                                acountpay = currentprice;
                                myhavemoney = myhavemoney - currentprice;
                                currentprice = 0;
                                pastta = 1;
                            }
                            else
                            {
                                if (myhavemoney > 0)
                                {
                                    acountpay = myhavemoney;
                                    currentprice = currentprice - myhavemoney;
                                    myhavemoney = 0;
                                }

                            }
                        }
                    }

                }
                else
                {
                    acountpay = 0;
                }


                ROrderinfo m = new ROrderinfo();

                m.Orderid = orderid;
                m.Currentprice = currentprice;
                m.allprice = totalprice;
                m.accountpay = acountpay;
                m.sendfee = list[i].sendfree;
                m.togoid = list[i].TogoId;
                m.WeiXxinOpenID = togomodel.PosAddr;
                m.cardpay = cardpay;
                m.latlng = list[i].latlng;
                m.sentorg = togomodel.sentorg;
                m.paymode = address.paymode;
                m.paystate = pastta;
                m.cityid = togomodel.cityid;
                m.promotionmoney = PromotionMoney;
                m.isAutoReceiveOrder = IsShopSet;

                if (moneydoor < totalprice)
                {
                    orderstatus = 1;
                }

                my_list.Add(m);

                //账户减少
                if (acountpay > 0)
                {
                    string accountpaysql = "UPDATE dbo.ECustomer SET userMoney = userMoney - " + acountpay + " WHERE DataID = " + address.UserID + ";";//减余额，加记录

                    StringBuilder strSqlrecord = new StringBuilder();
                    strSqlrecord.Append("insert into UserDelMoneyLog(");
                    strSqlrecord.Append("UserId,DelMoney,AddDate,BuyItem,Inve1,Inve2)");
                    strSqlrecord.Append(" values (");
                    strSqlrecord.Append("@UserId,@DelMoney,@AddDate,@BuyItem,@Inve1,@Inve2)");
                    SqlParameter[] recordparameters =
                    {
                        new SqlParameter("@UserId", SqlDbType.Int,4),
                        new SqlParameter("@DelMoney", SqlDbType.Decimal,5),
                        new SqlParameter("@AddDate", SqlDbType.DateTime),
                        new SqlParameter("@BuyItem", SqlDbType.VarChar,50),
                        new SqlParameter("@Inve1", SqlDbType.Int,4),
                        new SqlParameter("@Inve2", SqlDbType.VarChar,256)
                    };
                    recordparameters[0].Value = address.UserID;
                    recordparameters[1].Value = acountpay;
                    recordparameters[2].Value = DateTime.Now;
                    recordparameters[3].Value = "支付订单：" + orderid;
                    recordparameters[4].Value = 0;
                    recordparameters[5].Value = "";

                    cmdtextlist.Add(accountpaysql + strSqlrecord.ToString());
                    paraslist.Add(recordparameters);

                }

                StringBuilder strSql = new StringBuilder();

                strSql.Append("insert into OrderStep(");
                strSql.Append("stepcode,orderid,title,subtitle,addtime,deliverid,reveint1,reveint2,revevar1,revevar2");
                strSql.Append(") values (");
                strSql.Append("0,@orderid,'订单已经提交','',getdate(),0,0,0,'',''");
                strSql.Append(");");

                if (pastta == 1)
                {
                    strSql.Append("insert into OrderStep(");
                    strSql.Append("stepcode,orderid,title,subtitle,addtime,deliverid,reveint1,reveint2,revevar1,revevar2");
                    strSql.Append(") values (");
                    strSql.Append("10,@orderid,'支付成功','',getdate(),0,0,0,'',''");
                    strSql.Append(");");
                }

                if (IsShopSet == 1)
                {
                    strSql.Append("insert into OrderStep(");
                    strSql.Append("stepcode,orderid,title,subtitle,addtime,deliverid,reveint1,reveint2,revevar1,revevar2");
                    strSql.Append(") values (");
                    strSql.Append("20,@orderid,'商家已经接单','',getdate(),0,0,0,'',''");
                    strSql.Append(");");


                }


                strSql.Append("insert into custorder(");
                strSql.Append("InUse,OrderDateTime,OrderChecker,OrderStatus,OrderRcver,OrderComm,OrderAddress,AddressText,OrderAddrEx,OrderAttach,OrderSums,Sender,SendTime,CallPhoneNo,P2Sign,SendFee,paymode,paytime,paymoney,paystate,SetStateTime, UserId, TogoId,orderid,fromweb,CityID,deliversiteid,ReveVar1,ReveVar2,OldPrice,ReveInt1,ReveInt2,oldstatus,shopdiscountmoney,tempcode,cardpay,cardids,pointrat,webpromotionmoney,shoppromotionmoney,IsShopSet,ReveDate1,Packagefee,Commission,picktime,comtime,iscount)");
                strSql.Append(" values (");
                strSql.Append("@InUse,@OrderDateTime,@OrderChecker,@OrderStatus,@OrderRcver,@OrderComm,@OrderAddress,@AddressText,@OrderAddrEx,@OrderAttach,@OrderSums,@Sender,@SendTime,@CallPhoneNo,@P2Sign,@SendFee,@paymode,@paytime,@paymoney,@paystate,@SetStateTime,@UserId, @TogoId,@orderid,@fromweb,@CityID,@deliversiteid,@ReveVar1,@ReveVar2,@OldPrice,@ReveInt1,@ReveInt2,@oldstatus,@shopdiscountmoney,@tempcode,@cardpay,@cardids,@pointrat,@webpromotionmoney,@shoppromotionmoney,@IsShopSet,@ReveDate1,@Packagefee,@Commission,@picktime,@comtime,@iscount);");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters =
                {
                    new SqlParameter("@InUse", SqlDbType.VarChar,10),
                    new SqlParameter("@OrderDateTime", SqlDbType.DateTime),
                    new SqlParameter("@OrderChecker", SqlDbType.Int,4),
                    new SqlParameter("@OrderStatus", SqlDbType.Int,4),
                    new SqlParameter("@OrderRcver", SqlDbType.VarChar,50),
                    new SqlParameter("@OrderComm", SqlDbType.VarChar,50),
                    new SqlParameter("@OrderAddress", SqlDbType.VarChar,50),
                    new SqlParameter("@AddressText", SqlDbType.VarChar,100),
                    new SqlParameter("@OrderAddrEx", SqlDbType.VarChar,300),
                    new SqlParameter("@OrderAttach", SqlDbType.VarChar,200),
                    new SqlParameter("@OrderSums", SqlDbType.Decimal,9),
                    new SqlParameter("@Sender", SqlDbType.VarChar,256),
                    new SqlParameter("@SendTime", SqlDbType.DateTime),
                    new SqlParameter("@CallPhoneNo", SqlDbType.VarChar,256),
                    new SqlParameter("@P2Sign", SqlDbType.VarChar,256),
                    new SqlParameter("@SendFee", SqlDbType.Decimal,9),
                    new SqlParameter("@paymode", SqlDbType.Int,4),
                    new SqlParameter("@paytime", SqlDbType.DateTime),
                    new SqlParameter("@paymoney", SqlDbType.Decimal,9),
                    new SqlParameter("@paystate", SqlDbType.Int,4),
                    new SqlParameter("@SetStateTime", SqlDbType.DateTime),
                    new SqlParameter("@UserId", SqlDbType.Int,11),
                    new SqlParameter("@TogoId", SqlDbType.Int,11),
                    new SqlParameter("@orderid", SqlDbType.VarChar,50),
                    new SqlParameter("@fromweb", SqlDbType.VarChar,50),
                    new SqlParameter("@CityID",SqlDbType.Int,4),
                    new SqlParameter("@deliversiteid",SqlDbType.Int,4),
                    new SqlParameter("@ReveVar1",SqlDbType.VarChar,256),
                    new SqlParameter("@ReveVar2",SqlDbType.VarChar,256),
                    new SqlParameter("@OldPrice",SqlDbType.Decimal,9),
                    new SqlParameter("@ReveInt1", SqlDbType.Int,11),
                    new SqlParameter("@ReveInt2", SqlDbType.Int,11),
                    new SqlParameter("@oldstatus", SqlDbType.Int,4),
                    new SqlParameter("@shopdiscountmoney", SqlDbType.Decimal,9),
                    new SqlParameter("@tempcode",SqlDbType.VarChar,256),
                    new SqlParameter("@cardpay",SqlDbType.Decimal,9),
                    new SqlParameter("@cardids",SqlDbType.VarChar,256),
                    new SqlParameter("@pointrat", SqlDbType.Int,11),

                    new SqlParameter("@webpromotionmoney",SqlDbType.Decimal,9),
                    new SqlParameter("@shoppromotionmoney",SqlDbType.Decimal,9),

                    new SqlParameter("@IsShopSet",SqlDbType.Int),
                    new SqlParameter("@ReveDate1",SqlDbType.DateTime),
                    new SqlParameter("@Packagefee",SqlDbType.Decimal,9),
                    new SqlParameter("@Commission",SqlDbType.Decimal,9),
                    new SqlParameter("@picktime",SqlDbType.DateTime),
                    new SqlParameter("@comtime",SqlDbType.DateTime),
                    new SqlParameter("@iscount",SqlDbType.Int),
               };
                parameters[0].Value = "Y";
                parameters[1].Value = DateTime.Now;
                parameters[2].Value = 0;//前台
                parameters[3].Value = orderstatus;
                parameters[4].Value = address.Receiver;
                parameters[5].Value = address.Mobilephone;
                parameters[6].Value = address.kefuid;
                parameters[7].Value = address.Address;
                parameters[8].Value = "";
                parameters[9].Value = address.Remark;
                parameters[10].Value = totalprice;
                parameters[11].Value = "";
                parameters[12].Value = Convert.ToDateTime(address.sendtime);
                parameters[13].Value = address.Phone;
                parameters[14].Value = weixinopenid;
                parameters[15].Value = list[i].sendfree;
                parameters[16].Value = address.paymode;
                parameters[17].Value = pastta == 1 ? DateTime.Now : Convert.ToDateTime("1900-01-01");
                parameters[18].Value = acountpay;
                parameters[19].Value = pastta;//支付状态
                parameters[20].Value = DateTime.Now;
                parameters[21].Value = address.UserID;
                parameters[22].Value = togomodel.Unid;
                parameters[23].Value = orderid;
                parameters[24].Value = address.fromweb;
                parameters[25].Value = togomodel.cityid;
                parameters[26].Value = 0;
                parameters[27].Value = togomodel.sentorg;
                parameters[28].Value = list[i].latlng;
                parameters[29].Value = oldprice;
                parameters[30].Value = address.ReveInt1;
                parameters[31].Value = address.ReveInt2;
                parameters[32].Value = foodmoneydiscount;
                parameters[33].Value = payshopmoney;
                parameters[34].Value = address.tempcode;
                parameters[35].Value = cardpay;
                parameters[36].Value = cids;
                parameters[37].Value = pointrat;
                parameters[38].Value = iswebpromotion == true ? PromotionMoney : 0;
                parameters[39].Value = iswebpromotion == false ? PromotionMoney : 0; ;

                parameters[40].Value = IsShopSet;
                parameters[41].Value = IsShopSet == 0 ? Convert.ToDateTime("1970-1-1") : DateTime.Now;
                parameters[42].Value = packagefee;
                parameters[43].Value = Commission;
                parameters[44].Value = Convert.ToDateTime("1970-1-1");
                parameters[45].Value = Convert.ToDateTime("1970-1-1");
                parameters[46].Value = 0;




                //订单中编辑
                int unid = Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));

                //修改商家表里的POP（销售量）字段
                StringBuilder strSql3 = new StringBuilder();
                strSql3.Append("update points set pop=pop+1 where unid=@Unid");
                SqlParameter[] param = {
                  new SqlParameter("@Unid", SqlDbType.Int,4)
                };
                param[0].Value = list[i].TogoId;

                cmdtextlist.Add(strSql3.ToString());
                paraslist.Add(param);

                for (j = 0; j < list[i].ItemList.Count; j++)//每个订单中的餐品
                {
                    StringBuilder strSql2 = new StringBuilder();
                    strSql2.Append("insert into Foodlist(");
                    strSql2.Append("InUse,COUnid,FoodUnid,FoodPrice,FCounts,Remark,OldPrice,TogoId,adddate,orderid,fooname,material,sid,addprice,sname,packagefee)");
                    strSql2.Append(" values (");
                    strSql2.Append("@InUse,@COUnid,@FoodUnid,@FoodPrice,@FCounts,@Remark,@OldPrice,@TogoId,@adddate,@orderid,@fooname,@material,@sid,@addprice,@sname,@packagefee);");
                    SqlParameter[] parameters2 =
                    {
                        new SqlParameter("@InUse", SqlDbType.VarChar,10),
                        new SqlParameter("@COUnid", SqlDbType.Int,4),
                        new SqlParameter("@FoodUnid", SqlDbType.Int,4),
                        new SqlParameter("@FoodPrice", SqlDbType.Decimal,9),
                        new SqlParameter("@FCounts", SqlDbType.Int,4),
                        new SqlParameter("@Remark", SqlDbType.VarChar,50),
                        new SqlParameter("@OldPrice", SqlDbType.Decimal,9),
                        new SqlParameter("@TogoId", SqlDbType.Int,4),
                        new SqlParameter("@adddate", SqlDbType.DateTime),
                        new SqlParameter("@orderid", SqlDbType.VarChar,50),
                        new SqlParameter("@fooname", SqlDbType.VarChar,200),

                        new SqlParameter("@material", SqlDbType.VarChar,200) ,
                        new SqlParameter("@sid", SqlDbType.Int,4) ,
                        new SqlParameter("@addprice", SqlDbType.Decimal,5) ,
                        new SqlParameter("@sname", SqlDbType.VarChar,256) ,
                         new SqlParameter("@packagefee", SqlDbType.Decimal,9),


                        
                    };
                    parameters2[0].Value = "Y";
                    parameters2[1].Value = unid;
                    parameters2[2].Value = list[i].ItemList[j].PId;
                    parameters2[3].Value = list[i].ItemList[j].PPrice + list[i].ItemList[j].addprice;
                    parameters2[4].Value = list[i].ItemList[j].PNum;
                    parameters2[5].Value = list[i].ItemList[j].Remark;
                    parameters2[6].Value = list[i].ItemList[j].PPrice;
                    parameters2[7].Value = togomodel.Unid;
                    parameters2[8].Value = DateTime.Now;
                    parameters2[9].Value = orderid;
                    parameters2[10].Value = list[i].ItemList[j].PName;

                    parameters2[11].Value = list[i].ItemList[j].material;
                    parameters2[12].Value = list[i].ItemList[j].sid;
                    parameters2[13].Value = list[i].ItemList[j].addprice;
                    parameters2[14].Value = list[i].ItemList[j].sname;
                    parameters2[15].Value = list[i].ItemList[j].owername;

                    cmdtextlist.Add(strSql2.ToString());
                    paraslist.Add(parameters2);

                    if (address.paymode == 4 || address.paymode == 3)
                    {
                        //更新库存
                        int pnum = list[i].ItemList[j].PNum;
                        int pid = list[i].ItemList[j].PId;
                        updatefoodsql += " update Foodinfo set MaxPerDay =(case when maxperday=100000 then 100000 when maxperday-" + pnum + "<0  then 0 else maxperday-" + pnum + " end) where unid =" + list[i].ItemList[j].PId + ";";

                        updatefoodsql += "update Foodinfo set Remains =(SELECT COUNT(1) FROM dbo.Foodlist WHERE  FoodUnid=" + pid + " AND datediff(month,adddate,getdate())=0)+" + pnum + " where unid =" + pid + ";";

                    }
                }


            }
            bool flag = SQLHelper.ExecuteSqlTran(CommandType.Text, cmdtextlist, paraslist);
            if (flag)
            {
                AutoDispatch(my_list);
                if (address.paymode == 4 || address.paymode == 3)
                {
                    SQLHelper.excutesql(updatefoodsql);
                }
                return my_list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 计算要支付商家的金额
        /// </summary>
        /// <param name="shop">商家对像</param>
        /// <param name="foodprice">商品金额</param>
        /// <param name="isCash">是否货到付款</param>
        /// <returns></returns>
        public decimal getPayShopMoney(PointsInfo shop, decimal foodprice, bool isCash)
        {
            decimal payshopmoney = 0;
            switch (shop.SN1.Trim())
            {
                case "":
                case "0": //提成
                    payshopmoney = foodprice - foodprice * shop.point / 100;
                    break;
                case "1":
                    payshopmoney = foodprice - shop.point;
                    break;
                default:
                    break;
            }

            if (shop.sentorg.Trim() == "1" && isCash)//货到付款的，商家要给平台钱为负数
            {
                payshopmoney -= foodprice;
            }

            return payshopmoney;
        }

        /// <summary>
        /// 下订单
        /// </summary>
        /// <param name="list"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public int AddTBOrder(CustorderInfo model)
        {
            IList<ROrderinfo> my_list = new List<ROrderinfo>();
            string orderid = HjNetHelper.GetTime() + model.OrderComm.Substring(model.OrderComm.Length - 4);

            ArrayList paraslist = new ArrayList();

            int orderstatus = 2;//审核通过
            int foodmoneydiscount = 100;//商品折扣，100表示没有折扣，会员有等级会有折扣，88折，保存88;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into custorder(");
            strSql.Append("InUse,OrderDateTime,OrderChecker,OrderStatus,OrderRcver,OrderComm,OrderAddress,AddressText,OrderAddrEx,OrderAttach,OrderSums,Sender,SendTime,CallPhoneNo,P2Sign,SendFee,paymode,paytime,paymoney,paystate,SetStateTime, UserId, TogoId,orderid,fromweb,CityID,deliversiteid,ReveVar1,ReveVar2,OldPrice,ReveInt1,ReveInt2,oldstatus,shopdiscountmoney,tempcode,cardpay,cardids,pointrat,IsShopSet,ReveDate1)");
            strSql.Append(" values (");
            strSql.Append("@InUse,@OrderDateTime,@OrderChecker,@OrderStatus,@OrderRcver,@OrderComm,@OrderAddress,@AddressText,@OrderAddrEx,@OrderAttach,@OrderSums,@Sender,@SendTime,@CallPhoneNo,@P2Sign,@SendFee,@paymode,@paytime,@paymoney,@paystate,@SetStateTime,@UserId, @TogoId,@orderid,@fromweb,@CityID,@deliversiteid,@ReveVar1,@ReveVar2,@OldPrice,@ReveInt1,@ReveInt2,@oldstatus,@shopdiscountmoney,@tempcode,@cardpay,@cardids,@pointrat,@IsShopSet,@ReveDate1);");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters =
            {
                new SqlParameter("@InUse", SqlDbType.VarChar,10),
                new SqlParameter("@OrderDateTime", SqlDbType.DateTime),
                new SqlParameter("@OrderChecker", SqlDbType.Int,4),
                new SqlParameter("@OrderStatus", SqlDbType.Int,4),
                new SqlParameter("@OrderRcver", SqlDbType.VarChar,50),
                new SqlParameter("@OrderComm", SqlDbType.VarChar,50),
                new SqlParameter("@OrderAddress", SqlDbType.VarChar,50),
                new SqlParameter("@AddressText", SqlDbType.VarChar,100),
                new SqlParameter("@OrderAddrEx", SqlDbType.VarChar,300),
                new SqlParameter("@OrderAttach", SqlDbType.VarChar,200),
                new SqlParameter("@OrderSums", SqlDbType.Decimal,9),
                new SqlParameter("@Sender", SqlDbType.VarChar,256),
                new SqlParameter("@SendTime", SqlDbType.DateTime),
                new SqlParameter("@CallPhoneNo", SqlDbType.VarChar,256),
                new SqlParameter("@P2Sign", SqlDbType.VarChar,256),
                new SqlParameter("@SendFee", SqlDbType.Decimal,9),
                new SqlParameter("@paymode", SqlDbType.Int,4),
                new SqlParameter("@paytime", SqlDbType.DateTime),
                new SqlParameter("@paymoney", SqlDbType.Decimal,9),
                new SqlParameter("@paystate", SqlDbType.Int,4),
                new SqlParameter("@SetStateTime", SqlDbType.DateTime),
                new SqlParameter("@UserId", SqlDbType.Int,11),
                new SqlParameter("@TogoId", SqlDbType.Int,11),
                new SqlParameter("@orderid", SqlDbType.VarChar,50),
                new SqlParameter("@fromweb", SqlDbType.VarChar,50),
                new SqlParameter("@CityID",SqlDbType.Int,4),
                new SqlParameter("@deliversiteid",SqlDbType.Int,4),
                new SqlParameter("@ReveVar1",SqlDbType.VarChar,256),
                new SqlParameter("@ReveVar2",SqlDbType.VarChar,256),
                new SqlParameter("@OldPrice",SqlDbType.Decimal,9),
                new SqlParameter("@ReveInt1", SqlDbType.Int,11),
                new SqlParameter("@ReveInt2", SqlDbType.Int,11),
                new SqlParameter("@oldstatus", SqlDbType.Int,4),
                new SqlParameter("@shopdiscountmoney", SqlDbType.Decimal,9),
                new SqlParameter("@tempcode",SqlDbType.VarChar,256),
                new SqlParameter("@cardpay",SqlDbType.Decimal,9),
                new SqlParameter("@cardids",SqlDbType.VarChar,256),
                new SqlParameter("@pointrat", SqlDbType.Int,11),
                new SqlParameter("@IsShopSet", SqlDbType.Int),
                new SqlParameter("@ReveDate1", SqlDbType.DateTime),

            };
            parameters[0].Value = "Y";
            parameters[1].Value = DateTime.Now;
            parameters[2].Value = 0;
            parameters[3].Value = orderstatus;
            parameters[4].Value = model.OrderRcver;
            parameters[5].Value = model.OrderComm;
            parameters[6].Value = "";
            parameters[7].Value = model.AddressText;
            parameters[8].Value = "";
            parameters[9].Value = model.OrderAttach;
            parameters[10].Value = model.OrderSums;
            parameters[11].Value = "";
            parameters[12].Value = Convert.ToDateTime(model.SendTime);
            parameters[13].Value = model.OrderComm;
            parameters[14].Value = model.P2Sign;
            parameters[15].Value = model.SendFee;
            parameters[16].Value = model.paymode;
            parameters[17].Value = model.paytime;
            parameters[18].Value = model.paymoney;
            parameters[19].Value = model.paystate;
            parameters[20].Value = DateTime.Now;
            parameters[21].Value = 0;
            parameters[22].Value = model.TogoId;
            parameters[23].Value = orderid;
            parameters[24].Value = model.fromweb;
            parameters[25].Value = model.cityid;
            parameters[26].Value = 0;
            parameters[27].Value = model.ReveVar1;
            parameters[28].Value = model.ReveVar2;
            parameters[29].Value = model.OldPrice;
            parameters[30].Value = 0;
            parameters[31].Value = 0;
            parameters[32].Value = foodmoneydiscount;
            parameters[33].Value = model.shopdiscountmoney;
            parameters[34].Value = "";
            parameters[35].Value = 0;
            parameters[36].Value = "";
            parameters[37].Value = 1;
            parameters[38].Value = model.IsShopSet;
            parameters[39].Value = model.ReveDate1;


            int id = Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
            ROrderinfo mm = new ROrderinfo();
            mm.latlng = model.ReveVar2;
            mm.Orderid = orderid;
            mm.cityid = model.cityid;
            mm.sentorg = model.ReveVar1;
            mm.paystate = model.paystate;
            mm.paymode = model.paymode;
            my_list.Add(mm);

            AutoDispatch(my_list);

            return id;

        }

        /// <summary>
        /// 自动调度 传入订单编号
        /// </summary>
        /// <param name="my_list"></param>
        public void AutoDispatch(string orderid)
        {

            Custorder order = new Custorder();
            CustorderInfo orderinfo = new CustorderInfo();

            orderinfo = order.GetModel(orderid);
            IList<ROrderinfo> my_list = new List<ROrderinfo>();

            ROrderinfo mm = new ROrderinfo();
            mm.latlng = orderinfo.ReveVar2;//坐标"{'ulat':'" + ulat + "','ulng':'" + ulng + "','slat':'" + info.Lat + "','slng':'" + info.Lng + "'}";
            mm.Orderid = orderid;
            mm.cityid = orderinfo.cityid;
            mm.sentorg = orderinfo.ReveVar1;//配送方式
            mm.paystate = orderinfo.paystate;
            mm.paymode = orderinfo.paymode;
            my_list.Add(mm);

            AutoDispatch(my_list);
        }

        /// <summary>
        /// 自动调度
        /// </summary>
        /// <param name="my_list"></param>
        public void AutoDispatch(IList<ROrderinfo> my_list)
        {
            autodispatchconfigInfo config = CacheHelper.Getautodispatchconfig().Where(a => a.reveint2 == my_list[0].cityid).FirstOrDefault();
            if (config != null && config.isopen == 1)
            {
                latlnginfo latlng = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<latlnginfo>(my_list[0].latlng);

                IList<DeliverInfo> Nearbydelivers = null;
                if (config.autotype == 2)
                {
                    Nearbydelivers = new Deliver().GetNearbyList(config.distance, latlng.slat, latlng.slng);
                }

                foreach (var item in my_list)
                {
                    if (item.sentorg == "0" && (item.paymode == 4 || item.paystate == 1) && item.isAutoReceiveOrder==1) //只有统一配送的才调度
                    {
                        switch (config.autotype)
                        {
                            case 1: //表示发给所有人
                                {
                                    //订单表中也保存配送员编号，便于统计,修改订单状态:
                                    string sql = "update Custorder set deliverheaderid=" + Constant.biggid + ",OrderStatus=7,deliverid=0,sendstate=0,ReveDate2=getdate()  where OrderID='" + item.Orderid + "'";
                                    SQLHelper.excutesql(sql);

                                    NoticeHelper notice = new NoticeHelper(null);
                                    notice.send2All(item.cityid);

                                }
                                break;
                            case 2:
                                {
                                    string ids = "";
                                    int delivercount = 0;
                                    if (Nearbydelivers != null)
                                    {
                                        foreach (var deliver in Nearbydelivers)
                                        {
                                            delivercount++;
                                            ids += deliver.DataId + ",";
                                            if (delivercount >= config.reveint1)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    ids = HJConvert.dellast(ids);
                                    if (ids != "")
                                    {
                                        //订单表中也保存配送员编号，便于统计,修改订单状态:
                                        string sql = "update Custorder set deliverheaderid=" + Constant.biggid + ",OrderStatus=7,deliverid=0,sendstate=0,ReveDate2=getdate()  where OrderID='" + item.Orderid + "'";
                                        SQLHelper.excutesql(sql);

                                        NoticeHelper notice = new NoticeHelper(null);
                                        notice.send2IDs(ids);
                                    }
                                }
                                break;
                            case 3:
                                {

                                    int did = OutsetOrderDeliver(item.Orderid, latlng.slat, latlng.slng, item.cityid);

                                    if (did > 0)
                                    {
                                        NoticeHelper notice = new NoticeHelper(null, did.ToString());
                                        notice.sendOrderByDeliveryidNoData();

                                    }

                                }

                                break;
                            default:
                                break;
                        }
                    }

                }

            }
        }

        /// <summary>
        /// 自动调度订单
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="slat"></param>
        /// <param name="slng"></param>
        /// <returns>返回 配送员编号</returns>
        public int OutsetOrderDeliver(string orderid, string slat, string slng, int cityid)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@orderid", SqlDbType.VarChar,50),
                new SqlParameter("@lat", SqlDbType.VarChar,50),
                new SqlParameter("@lng", SqlDbType.VarChar,256),
                new SqlParameter("@did", SqlDbType.Int,4),
                    new SqlParameter("@cityid", SqlDbType.Int,4)
            };
            parameters[0].Value = orderid;
            parameters[1].Value = slat;
            parameters[2].Value = slng;
            parameters[3].Direction = ParameterDirection.Output;
            parameters[4].Value = cityid;


            SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "deliver_latest", parameters);
            int did = HJConvert.ToInt32(parameters[3].Value);

            return did;
        }
        /// <summary>
        /// 自动调度订单
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="slat"></param>
        /// <param name="slng"></param>
        /// <returns>返回 配送员编号</returns>
        public int OutsetOrderDeliver(string orderid, string slat, string slng)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@orderid", SqlDbType.VarChar,50),
                new SqlParameter("@lat", SqlDbType.VarChar,50),
                new SqlParameter("@lng", SqlDbType.VarChar,256),
                new SqlParameter("@did", SqlDbType.Int,4),
            };
            parameters[0].Value = orderid;
            parameters[1].Value = slat;
            parameters[2].Value = slng;
            parameters[3].Direction = ParameterDirection.Output;

            SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "deliver_latest", parameters);
            int did = HJConvert.ToInt32(parameters[3].Value);

            return did;
        }

        /// <summary>
        /// 取消订单
        /// 查询表，看本次处理的订单是否已经处理过，如果存在则更新对应业务系统中订单状态为“未传送取消订单”，
        /// 要更新的表iocache的Status字段值为-10 ，custorder的字段值OrderStatus为-10，且要更新本订单中餐品的库存，
        /// 不存在则只对网站更新本订单中对应业务系统中餐品的库存
        /// </summary>
        /// <returns></returns>
        public bool UpdateCallCenterOrder(string orderid, int iocachestatus, int custorderstatus)
        {
            ArrayList cmdtextlist = new ArrayList();

            ArrayList paraslist = new ArrayList();

            StringBuilder strSql = new StringBuilder();

            StringBuilder strSql2 = new StringBuilder();

            strSql.Append("update custorder set OrderStatus=" + custorderstatus.ToString() + " where orderid=@orderid");

            cmdtextlist.Add(strSql.ToString());

            SqlParameter[] parameters =
            {
                new SqlParameter("@orderid", SqlDbType.VarChar,50),
            };
            paraslist.Add(parameters);

            strSql2.Append("update iocache set Status=" + iocachestatus.ToString() + " where orderid=@orderid");

            SqlParameter[] parameters2 =
            {
                new SqlParameter("@orderid", SqlDbType.VarChar,50),
            };

            cmdtextlist.Add(strSql2.ToString());
            paraslist.Add(parameters2);

            bool flag = SQLHelper.ExecuteSqlTran(CommandType.Text, cmdtextlist, paraslist);
            //bool flag = CallCenterMySqlHelper.ExecuteSqlTran(CommandType.Text, cmdtextlist, paraslist);

            return flag;
        }

        /// <summary>
        /// 调度页面获取列表 包含配送信息
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<CustorderInfo> GetListForDelive(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<CustorderInfo> infos = new List<CustorderInfo>();
            SqlParameter[] parameters =
            {
                new SqlParameter("@pagesize", SqlDbType.Int),
                new SqlParameter("@pageindex", SqlDbType.Int),
                new SqlParameter("@orderfield", SqlDbType.VarChar,200),
                new SqlParameter("@ordertype", SqlDbType.VarChar,50),
                new SqlParameter("@where", SqlDbType.VarChar,4500)
            };
            parameters[0].Value = pagesize;
            parameters[1].Value = pageindex;
            parameters[2].Value = orderName;
            if (orderType == 1)
            {
                parameters[3].Value = "desc";
            }
            else
            {
                parameters[3].Value = "asc";
            }
            parameters[4].Value = strWhere;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "DeliverGetOrderListWithAll", parameters))
            {
                while (dr.Read())
                {
                    CustorderInfo model = new CustorderInfo();
                    model.Unid = HJConvert.ToInt32(dr["Unid"]);
                    model.InUse = HJConvert.ToString(dr["InUse"]);
                    model.OrderDateTime = HJConvert.ToDateTime(dr["OrderDateTime"]);
                    model.OrderChecker = HJConvert.ToInt32(dr["OrderChecker"]);
                    model.OrderStatus = HJConvert.ToInt32(dr["OrderStatus"]);
                    model.OrderRcver = HJConvert.ToString(dr["OrderRcver"]).Replace(",", "").Replace("'", "").Trim();
                    model.OrderComm = HJConvert.ToString(dr["OrderComm"]);
                    model.OrderAddress = HJConvert.ToString(dr["OrderAddress"]);
                    model.AddressText = HJConvert.ToString(dr["AddressText"]).Replace(",", "").Replace("'", "").Trim();
                    model.OrderAddrEx = HJConvert.ToString(dr["OrderAddrEx"]);
                    model.OrderAttach = HJConvert.ToString(dr["OrderAttach"]);
                    model.OrderSums = HJConvert.ToDecimal(dr["OrderSums"]);
                    model.Sender = HJConvert.ToString(dr["Sender"]);
                    model.SendTime = HJConvert.ToDateTime(dr["SendTime"]);
                    model.CallPhoneNo = HJConvert.ToString(dr["CallPhoneNo"]);
                    model.P2Sign = HJConvert.ToString(dr["P2Sign"]);
                    model.SendFee = HJConvert.ToDecimal(dr["SendFee"]);
                    model.paymode = HJConvert.ToInt32(dr["paymode"]);
                    model.paytime = HJConvert.ToDateTime(dr["paytime"]);
                    model.paymoney = HJConvert.ToDecimal(dr["paymoney"]);
                    model.paystate = HJConvert.ToInt32(dr["paystate"]);
                    model.SetStateTime = HJConvert.ToDateTime(dr["SetStateTime"]);
                    model.UserId = HJConvert.ToInt32(dr["UserId"]);
                    model.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    model.TogoName = HJConvert.ToString(dr["TogoName"]);
                    model.orderid = HJConvert.ToString(dr["orderid"]);
                    model.SystemUserId = HJConvert.ToInt32(dr["SystemUserId"]);
                    model.OldStatus = HJConvert.ToInt32(dr["OldStatus"]);
                    model.IsShopSet = HJConvert.ToInt32(dr["IsShopSet"]);
                    model.deliversiteid = HJConvert.ToInt32(dr["deliversiteid"]);
                    model.deliverheaderid = HJConvert.ToInt32(dr["deliverheaderid"]);
                    model.deliverid = HJConvert.ToInt32(dr["deliverid"]);
                    model.sendstate = HJConvert.ToInt32(dr["sendstate"]);
                    model.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    model.ReveInt2 = HJConvert.ToInt32(dr["ReveInt2"]);
                    model.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
                    model.ReveVar2 = HJConvert.ToString(dr["ReveVar2"]);
                    model.ReveDate1 = HJConvert.ToDateTime(dr["ReveDate1"]);
                    model.ReveDate2 = HJConvert.ToDateTime(dr["ReveDate2"]);
                    model.fromweb = HJConvert.ToString(dr["fromweb"]);

                    //配送信息
                    OrderDeliverInfo dinfo = new OrderDeliverInfo();
                    dinfo.Orderid = HJConvert.ToString(dr["OrderId"]);
                    dinfo.DeliverId = HJConvert.ToInt32(dr["DeliverId"]);
                    dinfo.DeliverName = HJConvert.ToString(dr["DeliverName"]);
                    dinfo.Dispatcher = HJConvert.ToString(dr["Dispatcher"]);
                    dinfo.DispatchTime = HJConvert.ToDateTime(dr["DispatchTime"]);
                    dinfo.DeliveryTime = HJConvert.ToDateTime(dr["DeliveryTime"]);
                    dinfo.Section = HJConvert.ToString(dr["Section"]);
                    dinfo.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    dinfo.Inve2 = HJConvert.ToString(dr["Inve2"]);

                    model.DeliveInfo = dinfo;

                    infos.Add(model);
                }
            }
            return infos;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddOrderRecord(string orderid, int state, string adminname, string Reve2)
        {
            SqlParameter[] Para =
            {
                new SqlParameter("@orderid",SqlDbType.VarChar,20),
                new SqlParameter("@newstate",SqlDbType.Int,4),
                new SqlParameter("@adminname",SqlDbType.VarChar,50),
                new SqlParameter("@Reve2",SqlDbType.VarChar,256),
            };
            Para[0].Value = orderid;
            Para[1].Value = state;
            Para[2].Value = adminname;
            Para[3].Value = Reve2;

            return SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Etogoorder_AddOrderRecord", Para);
        }


        /// <summary>
        /// 骑士客户端(Android)获取获取订单总数 2012.5.4 zjf@ihangjing.com 新增
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int DeliverGetOrderCount(string strWhere)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@tblName" ,SqlDbType.VarChar , 300),
                new SqlParameter("@strWhere" , SqlDbType.VarChar  , 300)
            };
            parameters[0].Value = " Custorder LEFT JOIN dbo.OrderDeliver ON Custorder.OrderID=OrderDeliver.orderid ";
            parameters[1].Value = strWhere;

            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount_leftjoin", parameters);
        }


        /// <summary>
        /// 获取时间最早并且没有被打印的订单
        /// </summary>
        /// <param name="togoNum"></param>
        /// <returns></returns>
        public CustorderInfo GetOldOrder(string togoNum)
        {
            SqlParameter[] Para =
            {
                new SqlParameter("@TogoNum",SqlDbType.VarChar,20)
            };
            Para[0].Value = togoNum;

            CustorderInfo model = null;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "ETogoOrder_GetOldOrder", Para))
            {
                if (dr.Read())
                {
                    model = new CustorderInfo();
                    model.Unid = HJConvert.ToInt32(dr["Unid"]);
                    model.InUse = HJConvert.ToString(dr["InUse"]);
                    model.OrderDateTime = HJConvert.ToDateTime(dr["OrderDateTime"]);
                    model.OrderChecker = HJConvert.ToInt32(dr["OrderChecker"]);
                    model.OrderStatus = HJConvert.ToInt32(dr["OrderStatus"]);
                    model.OrderRcver = HJConvert.ToString(dr["OrderRcver"]);
                    model.OrderComm = HJConvert.ToString(dr["OrderComm"]);
                    model.OrderAddress = HJConvert.ToString(dr["OrderAddress"]);
                    model.AddressText = HJConvert.ToString(dr["AddressText"]);
                    model.OrderAddrEx = HJConvert.ToString(dr["OrderAddrEx"]);
                    model.OrderAttach = HJConvert.ToString(dr["OrderAttach"]);
                    model.OrderSums = HJConvert.ToDecimal(dr["OrderSums"]);
                    model.Sender = HJConvert.ToString(dr["Sender"]);
                    model.SendTime = HJConvert.ToDateTime(dr["SendTime"]);
                    model.CallPhoneNo = HJConvert.ToString(dr["CallPhoneNo"]);
                    model.P2Sign = HJConvert.ToString(dr["P2Sign"]);
                    model.SendFee = HJConvert.ToDecimal(dr["SendFee"]);
                    model.paymode = HJConvert.ToInt32(dr["paymode"]);
                    model.paytime = HJConvert.ToDateTime(dr["paytime"]);
                    model.paymoney = HJConvert.ToDecimal(dr["paymoney"]);
                    model.paystate = HJConvert.ToInt32(dr["paystate"]);
                    model.SetStateTime = HJConvert.ToDateTime(dr["SetStateTime"]);
                    model.UserId = HJConvert.ToInt32(dr["UserId"]);
                    model.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    model.orderid = HJConvert.ToString(dr["orderid"]);
                    model.SystemUserId = HJConvert.ToInt32(dr["SystemUserId"]);
                    model.OldStatus = HJConvert.ToInt32(dr["OldStatus"]);
                    model.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 修改订单的状态。
        /// </summary>
        /// <param name="OrderID">订单编号</param>
        /// <param name="State">要修改的订单状态</param>
        public int UpdataStatebyorderid(string OrderID, int State)
        {

            StringBuilder str = new StringBuilder();
            str.Append("update custorder set orderstatus=@State,SetStateTime='" + DateTime.Now + "' where OrderID=@OrderID");
            SqlParameter[] Para =
            {
                new SqlParameter("@OrderID",SqlDbType.VarChar,20),
                new SqlParameter("@State",SqlDbType.Int)
            };
            Para[0].Value = OrderID;
            Para[1].Value = State;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 修改订单的状态。
        /// </summary>
        /// <param name="OrderID">订单编号</param>
        /// <param name="State">要修改的订单状态</param>
        public int UpdataStatebyorderidfix(string OrderID, int State)
        {

            StringBuilder str = new StringBuilder();

            str.Append("update custorder set sendstate=@State where OrderID=@OrderID");

            SqlParameter[] Para =
            {
                new SqlParameter("@OrderID",SqlDbType.VarChar,20),
                new SqlParameter("@State",SqlDbType.Int)
            };
            Para[0].Value = OrderID;
            Para[1].Value = State;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 骑士客户端(Android)获取获取订单列表 2012.5.4 zjf@ihangjing.com 新增
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序desc，0为升序asc</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<CustorderInfo> DeliverGetOrderList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            //SELECT * FROM  etogoorder LEFT JOIN dbo.OrderDeliver ON dbo.ETogoOrder.OrderID=dbo.OrderDeliver.orderid WHERE dbo.OrderDeliver.DeliverId=2 AND dbo.ETogoOrder.State=1

            /*
            @pagesize = 3,
		    @pageindex = 1,
		    @orderfield = N'EtogoOrder.dataid',
		    @ordertype = N'desc',
		    @where = N'DeliverId=2 and state=4'
             */
            IList<CustorderInfo> infos = new List<CustorderInfo>();
            SqlParameter[] parameters =
            {
                new SqlParameter("@pagesize", SqlDbType.Int),
                new SqlParameter("@pageindex", SqlDbType.Int),
                new SqlParameter("@orderfield", SqlDbType.VarChar,50),
                new SqlParameter("@ordertype", SqlDbType.VarChar,5),
                new SqlParameter("@where", SqlDbType.VarChar,1500)
            };
            parameters[0].Value = pagesize;
            parameters[1].Value = pageindex;
            parameters[2].Value = orderName;
            if (orderType == 1)
            {
                parameters[3].Value = "desc";
            }
            else
            {
                parameters[3].Value = "asc";
            }
            parameters[4].Value = strWhere;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "DeliverGetOrderListWithAll", parameters))
            {
                while (dr.Read())
                {
                    CustorderInfo info = new CustorderInfo();
                    info.Unid = HJConvert.ToInt32(dr["Unid"]);
                    info.UserId = HJConvert.ToInt32(dr["UserId"]);
                    info.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    info.orderid = HJConvert.ToString(dr["orderid"]);
                    info.OrderDateTime = HJConvert.ToDateTime(dr["OrderDateTime"]);
                    info.OrderAddress = HJConvert.ToString(dr["OrderAddress"]);
                    info.AddressText = HJConvert.ToString(dr["AddressText"]);
                    info.OrderAddrEx = HJConvert.ToString(dr["OrderAddrEx"]);
                    info.OrderAttach = HJConvert.ToString(dr["OrderAttach"]);
                    info.OrderSums = HJConvert.ToDecimal(dr["OrderSums"]);
                    info.Sender = HJConvert.ToString(dr["Sender"]);
                    info.SendTime = HJConvert.ToDateTime(dr["SendTime"]);
                    info.TogoName = HJConvert.ToString(dr["TogoName"]);
                    info.OldStatus = HJConvert.ToInt32(dr["OldStatus"]);

                    info.OrderStatus = HJConvert.ToInt32(dr["OrderStatus"]);

                    info.paymode = HJConvert.ToInt32(dr["paymode"]);
                    info.paystate = HJConvert.ToInt32(dr["paystate"]);
                    info.SendFee = HJConvert.ToInt32(dr["SendFee"]);
                    info.sendstate = HJConvert.ToInt32(dr["sendstate"]);
                    info.IsShopSet = HJConvert.ToInt32(dr["IsShopSet"]);

                    //info.mypaymoney = info.Currentprice + info.sendmoney + info.Packagefree;

                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 骑士客户端通过获取列表的方式得到订单的model
        /// </summary>
        public IList<CustorderInfo> DeliverGetOrderList4GetModel(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<CustorderInfo> infos = new List<CustorderInfo>();
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
            parameters[0].Value = "Custorder";
            parameters[1].Value = "* ,(select Name from Points where Unid = Custorder.togoid) as togoname,(select senttime from Points where Unid = Custorder.togoid) as SentTime,(select address from Points where Unid = Custorder.togoid) as togoaddress,(select Comm from Points where Unid = Custorder.togoid) as togotel ";
            parameters[2].Value = "Unid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    CustorderInfo model = new CustorderInfo();

                    model.Unid = HJConvert.ToInt32(dr["Unid"]);
                    model.InUse = HJConvert.ToString(dr["InUse"]);
                    model.OrderDateTime = HJConvert.ToDateTime(dr["OrderDateTime"]);
                    model.OrderChecker = HJConvert.ToInt32(dr["OrderChecker"]);
                    model.OrderStatus = HJConvert.ToInt32(dr["OrderStatus"]);
                    model.OrderRcver = HJConvert.ToString(dr["OrderRcver"]);
                    model.OrderComm = HJConvert.ToString(dr["OrderComm"]);
                    model.OrderAddress = HJConvert.ToString(dr["OrderAddress"]);
                    model.AddressText = HJConvert.ToString(dr["AddressText"]);
                    model.OrderAddrEx = HJConvert.ToString(dr["OrderAddrEx"]);
                    model.OrderAttach = HJConvert.ToString(dr["OrderAttach"]);
                    model.OrderSums = HJConvert.ToDecimal(dr["OrderSums"]);
                    model.Sender = HJConvert.ToString(dr["Sender"]);
                    model.SendTime = HJConvert.ToDateTime(dr["SendTime"]);
                    model.CallPhoneNo = HJConvert.ToString(dr["CallPhoneNo"]);
                    model.P2Sign = HJConvert.ToString(dr["P2Sign"]);
                    model.SendFee = HJConvert.ToDecimal(dr["SendFee"]);
                    model.paymode = HJConvert.ToInt32(dr["paymode"]);
                    model.paytime = HJConvert.ToDateTime(dr["paytime"]);
                    model.paymoney = HJConvert.ToDecimal(dr["paymoney"]);
                    model.paystate = HJConvert.ToInt32(dr["paystate"]);
                    model.SetStateTime = HJConvert.ToDateTime(dr["SetStateTime"]);
                    model.UserId = HJConvert.ToInt32(dr["UserId"]);
                    model.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    model.orderid = HJConvert.ToString(dr["orderid"]);
                    model.SystemUserId = HJConvert.ToInt32(dr["SystemUserId"]);
                    model.OldStatus = HJConvert.ToInt32(dr["OldStatus"]);
                    model.TogoTel = HJConvert.ToString(dr["togotel"]);
                    model.TogoName = HJConvert.ToString(dr["togoname"]);
                    model.TogoAddress = HJConvert.ToString(dr["togoaddress"]);
                    model.sendstate = HJConvert.ToInt32(dr["sendstate"]);
                    model.OldPrice = HJConvert.ToDecimal(dr["OldPrice"]);
                    model.ReveVar2 = HJConvert.ToString(dr["ReveVar2"]);
                    model.IsShopSet = HJConvert.ToInt32(dr["IsShopSet"]);
                    model.SentTime = HJConvert.ToInt32(dr["SentTime"]);
                    model.shopdiscountmoney = HJConvert.ToDecimal(dr["shopdiscountmoney"]);
                    model.promotionmoney = HJConvert.ToDecimal(dr["webpromotionmoney"]) + HJConvert.ToDecimal(dr["shoppromotionmoney"]);
                    model.cardpay = HJConvert.ToDecimal(dr["cardpay"]);
                    model.Packagefee = HJConvert.ToDecimal(dr["Packagefee"]);
                    model.picktime = HJConvert.ToDateTime(dr["picktime"]);
                    model.comtime = HJConvert.ToDateTime(dr["comtime"]);
                    model.shopCancel = HJConvert.ToInt32(dr["shopCancel"]);
                    model.Cancelreason = HJConvert.ToString(dr["Cancelreason"]);


                    infos.Add(model);
                }
            }
            return infos;
        }

        /// <summary>
        /// 获取配送员的订单
        /// </summary>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<simpleorderInfo> GetDliverList(string did)
        {
            IList<simpleorderInfo> infos = new List<simpleorderInfo>();

            string sql = "select a.OrderRcver,a.AddressText,a.orderid,a.OrderComm ,b.Name from Custorder a left join points b on a.togoid = b.unid where deliverid =" + did + " and OrderStatus = 7 and sendstate < 3 ";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    simpleorderInfo info = new simpleorderInfo();
                    info.UserName = HJConvert.ToString(dr["OrderRcver"]);
                    info.Address = HJConvert.ToString(dr["AddressText"]);
                    info.TogoName = HJConvert.ToString(dr["Name"]);
                    info.OrderID = HJConvert.ToString(dr["OrderID"]);
                    info.Tel = HJConvert.ToString(dr["OrderComm"]);

                    infos.Add(info);
                }
            }
            return infos;
        }
        /// <summary>
        /// 获得所有状态为新增的订单
        /// </summary>
        /// <returns></returns>
        public IList<CustorderInfo> GetNewOrder()
        {
            IList<CustorderInfo> DataList = new List<CustorderInfo>();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "ETogoOrder_GetNewOrder", null))
            {
                while (dr.Read())
                {
                    CustorderInfo model = new CustorderInfo();
                    model.orderid = HJConvert.ToString(dr["OrderID"]);
                    model.OrderStatus = HJConvert.ToInt32(dr["OrderStatus"]);
                    model.OrderDateTime = HJConvert.ToDateTime(dr["OrderDateTime"]);
                    DataList.Add(model);
                }
            }
            return DataList;
        }

        /// <summary>
        /// 要推给商家的订单数量
        /// </summary>
        public int SendShopOrderCount(string shopid)
        {
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "SendShopOrderCount", new SqlParameter("@shopid", shopid)));
        }

        /// <summary>
        /// 要推给骑士的订单数量
        /// </summary>
        public OrderCountInfo SendDeliverOrderCount(string deliverid)
        {
            OrderCountInfo model = new OrderCountInfo();
            model.rat = 0;
            model.CountIntValue = 0;


            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "SendDeliverOrderCount", new SqlParameter("@dataid", deliverid)))
            {
                while (dr.Read())
                {
                    model.CountIntValue = HJConvert.ToInt32(dr["ordercount"]);

                }
            }

            return model;
        }


        /// <summary>
        /// 配送完成相关操作
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="did"></param>
        /// <returns></returns>
        public int deliverComplete(string orderid, int did)
        {
            AddOrderRecord(orderid, 3, "骑士", "骑士修改订单状态 SaveOrderState.aspx");
            AddPoint(orderid);

            StringBuilder str = new StringBuilder();
            str.Append("update OrderDeliver set OverTime=getdate() where OrderID=@OrderID");

            SqlParameter[] Para =
            {
                new SqlParameter("@OrderID",SqlDbType.VarChar,20),
            };
            Para[0].Value = orderid;

            SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);

            return 1;
        }

        /// <summary>
        /// 会员确认收货相关操作（加积分和添加操作记录） 2015-11-25 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="did"></param>
        /// <returns></returns>
        public int UserComplete(string orderid, string userName)
        {
            userName = string.IsNullOrEmpty(userName) ? "会员" : userName;

            AddPoint(orderid);//添加积分

            AddOrderRecord(GetModel(orderid).orderid, 3, userName, "用户修改订单状态为3 确认收货 SaveOrderState.aspx");

            return 1;
        }



        /// <summary>
        /// 营业收入订单总数，和金额
        /// </summary>
        /// <param name="togoNum"></param>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        public CustorderInfo SiteIncomeStatistics(string where)
        {
            string sql = "SELECT COUNT(1) AS	OrderCount , SUM(OrderSums - shopdiscountmoney) AS OrderTotal, SUM(shopdiscountmoney) as shopdiscountmoney, SUM(OrderSums) as CountDecimalPrice,SUM(SendFee) as SendFee, SUM(cardpay) as cardpay FROM dbo.Custorder   where " + where;

            CustorderInfo model = new CustorderInfo();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (dr.Read())
                {

                    model.OrderCount = HJConvert.ToInt32(dr["OrderCount"]);
                    model.OrderTotal = HJConvert.ToDecimal(dr["OrderTotal"]); //收入
                    model.OrderSums = HJConvert.ToDecimal(dr["CountDecimalPrice"]); //总的
                    model.shopdiscountmoney = HJConvert.ToDecimal(dr["shopdiscountmoney"]); //支付商家的
                    model.SendFee = HJConvert.ToDecimal(dr["SendFee"]);  //配送费
                    model.cardpay = HJConvert.ToDecimal(dr["cardpay"]);  //优惠卷
                }
            }
            return model;
        }

        /// <summary>
        /// 订单来源分布
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IList<OrderCountInfo> GetOrderByOrderSource(string where)
        {
            IList<OrderCountInfo> list = new List<OrderCountInfo>();
            string strSql = " SELECT fromweb ,COUNT(1) AS ordercount , SUM(OrderSums) AS TotalPrice FROM dbo.Custorder where  " + where + "    group by fromweb";

            OrderCountInfo info = new OrderCountInfo();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, strSql, null))
            {
                while (dr.Read())
                {
                    info = new OrderCountInfo();

                    info.CountKey = HJConvert.ToString(dr["fromweb"]);
                    info.CountIntValue = HJConvert.ToInt32(dr["ordercount"]);
                    info.CountDecimalValue = HJConvert.ToDecimal(dr["TotalPrice"]);
                    list.Add(info);
                }
            }

            return list;
        }



        /// <summary>
        /// 获取外卖和跑腿订单，按调度时间降序 2015-12-2 
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="waimaiWhere"></param>
        /// <param name="paotuiWhere"></param>
        /// <returns></returns>
        public IList<CustorderInfo> getOrderListWithWMAndPT(int pagesize, int pageindex, string waimaiWhere, string paotuiWhere, string orderstr)
        {
            IList<CustorderInfo> infos = new List<CustorderInfo>();
            SqlParameter[] parameters =
            {
                new SqlParameter("@pagesize", SqlDbType.Int),
                new SqlParameter("@pageindex", SqlDbType.Int),
                new SqlParameter("@paotuiorderwhere", SqlDbType.VarChar,1500),
                new SqlParameter("@waimaiorderwhere", SqlDbType.VarChar,1500),
                new SqlParameter("@orderstr", SqlDbType.VarChar,100),
                
            };
            parameters[0].Value = pagesize;
            parameters[1].Value = pageindex;
            parameters[2].Value = paotuiWhere;
            parameters[3].Value = waimaiWhere;
            parameters[4].Value = orderstr;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "deliver_getOrderList", parameters))
            {
                while (dr.Read())
                {
                    CustorderInfo info = new CustorderInfo();

                    info.orderid = HJConvert.ToString(dr["orderid"]);
                    info.OrderDateTime = HJConvert.ToDateTime(dr["OrderDateTime"]);
                    info.AddressText = HJConvert.ToString(dr["AddressText"]);
                    info.OrderSums = HJConvert.ToDecimal(dr["OrderSums"]);
                    info.TogoName = HJConvert.ToString(dr["TogoName"]);
                    info.OrderStatus = HJConvert.ToInt32(dr["OrderStatus"]);
                    info.Unid = HJConvert.ToInt32(dr["ordertype"]);//1：外卖，2表示跑腿
                    info.sendstate = HJConvert.ToInt32(dr["sendstate"]);
                    info.OrderCount = HJConvert.ToInt32(dr["recordtcount"]);
                    info.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    info.OrderComm = HJConvert.ToString(dr["OrderComm"]);
                    info.OrderRcver = HJConvert.ToString(dr["OrderRcver"]);
                    info.deliverid = HJConvert.ToInt32(dr["deliverid"]);
                    info.delivertel = HJConvert.ToString(dr["delivertel"]);
                    info.SentTime = HJConvert.ToInt32(dr["SentTime"]);
                    info.picktime = HJConvert.ToDateTime(dr["picktime"]);
                    info.comtime = HJConvert.ToDateTime(dr["comtime"]);

                    if (dr["OverTime"] != null && HJConvert.ToString(dr["OverTime"]) != "")
                    {
                        info.tempcode = HJConvert.ToString(dr["OverTime"]);
                    }
                    else
                    {
                        info.tempcode = "1900-01-01 00:00:00.000";
                    }

                    info.IsShopSet = HJConvert.ToInt32(dr["IsShopSet"]);

                    info.ReveVar2 = HJConvert.ToString(dr["ReveVar2"]);
                    info.SendFee = HJConvert.ToDecimal(dr["SendFee"]);
                    info.Sender = HJConvert.ToString(dr["Sender"]);
                    info.SendTime = HJConvert.ToDateTime(dr["SendTime"]);
                    info.Packagefee = HJConvert.ToDecimal(dr["Packagefee"]);
                    info.promotionmoney = HJConvert.ToDecimal(dr["webpromotionmoney"]) + HJConvert.ToDecimal(dr["shoppromotionmoney"]);
                    info.cardpay = HJConvert.ToDecimal(dr["cardpay"]);
                    info.paymode = HJConvert.ToInt32(dr["paymode"]);
                    info.paystate = HJConvert.ToInt32(dr["paystate"]);
                    info.OrderAttach = HJConvert.ToString(dr["OrderAttach"]);
                    info.shopdiscountmoney = HJConvert.ToDecimal(dr["shopdiscountmoney"]);
                    info.OldPrice = HJConvert.ToDecimal(dr["OldPrice"]);
                    info.paymoney = HJConvert.ToDecimal(dr["paymoney"]);
                    info.TogoAddress = HJConvert.ToString(dr["TogoAddress"]);
                    info.shopCancel = HJConvert.ToInt32(dr["shopCancel"]);
                    info.Cancelreason = HJConvert.ToString(dr["Cancelreason"]);

                    infos.Add(info);
                }
            }
            return infos;
        }



    }
}
