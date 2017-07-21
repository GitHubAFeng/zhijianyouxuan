
// Itogoorder.css:订单实现类.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-03-12

using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;

using Hangjing.Model;

using Hangjing.DBUtility;//请先添加引用
using Hangjing.Common;
namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 跑腿订单操作
    /// </summary>
    public class ExpressOrder
    {

        /// <summary>
        /// 提交订单
        /// </summary>
        public IList<ROrderinfo> submitorder(ExpressOrderInfo model)
        {

            IList<ROrderinfo> my_list = new List<ROrderinfo>();
            ArrayList cmdtextlist = new ArrayList();
            ArrayList paraslist = new ArrayList();

            DateTime paytime = Convert.ToDateTime("1970/1/1 00:00:00");
            decimal myhavemoney = 0;//用户总共的金额
            decimal totalprice = 0;  //总支付金额
            decimal currentprice = 0; //用户还要支付的金额
            decimal acountpay = 0;//余额支付
            decimal paystate = 0;//支付状态

            totalprice = model.TotalPrice;
            //余额支付
            if (model.UserID > 0)
            {
                ECustomerInfo user = new ECustomer().GetModel(model.UserID);
                  if (user != null)
                  {
                      myhavemoney = user.Usermoney;
                      currentprice = totalprice;

                      if (model.PayMode == 2)
                      {
                          if (Convert.ToInt32(myhavemoney) > 0)
                          {
                              if (myhavemoney >= currentprice)
                              {
                                  acountpay = currentprice;
                                  myhavemoney = myhavemoney - currentprice;
                                  currentprice = 0;
                                  paystate = 1;
                                  paytime = DateTime.Now;
                              }
                              else
                              {
                                  if (myhavemoney > 0)
                                  {
                                      acountpay = myhavemoney;
                                      currentprice = currentprice - myhavemoney;
                                      myhavemoney = 0;
                                      paytime = DateTime.Now;
                                  }

                              }
                          }
                      }
                  }
            }
            else//商家提交
            {
                currentprice = model.Currentprice;//2015-12-2 
            }

            ROrderinfo m = new ROrderinfo();
            m.Orderid =model.OrderID;
            m.Currentprice = currentprice;
            m.allprice = totalprice;
            m.accountpay = acountpay;
           // m.Totalsendmoney = 0;
           // m.TogoPhone ="";
            m.togoid =0;
            m.PayOrderId = model.PayOrderId;
          //  m.saleprice = totalprice;

            my_list.Add(m);


            //账户减少
            if (acountpay > 0)
            {
                string accountpaysql = "UPDATE dbo.ECustomer SET Usermoney = Usermoney - " + acountpay + " WHERE DataID = " + model.UserID + ";";//减余额，加记录

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
                recordparameters[0].Value = model.UserID;
                recordparameters[1].Value = acountpay;
                recordparameters[2].Value = DateTime.Now;
                recordparameters[3].Value = "支付订单：" +model.OrderID;
                recordparameters[4].Value = 0;
                recordparameters[5].Value = "";

                cmdtextlist.Add(accountpaysql + strSqlrecord.ToString());
                paraslist.Add(recordparameters);

            }

            StringBuilder strSql = new StringBuilder();

            strSql.Append("insert into ExpressOrder(");
            strSql.Append("UserID,UserName,Tel,SentTime,Address,State,TogoId,orderTime,OrderID,TotalPrice,SetStateTime,Currentprice,Remark,Oorderid,callcount,callmsg,writer,paymode,paytime,paystate,paymoney,PayOrderId,Inve1,Inve2,sid,bid,CustomerName,tempcode,sendmoney,cityid,ordersource,isaddpoint,sendtype,ulat,ulng,shoplat,shoplng,sitelat,sitelng,ordertype,noaccess,validateCode,iscancel,ReveInt1,ReveInt2,ReveVar,ReveDate1,ReveDate2,IsTimeLimit,servename,acountpay,picktime,comtime");
            strSql.Append(") values (");
            strSql.Append("@UserID,@UserName,@Tel,@SentTime,@Address,@State,@TogoId,@orderTime,@OrderID,@TotalPrice,@SetStateTime,@Currentprice,@Remark,@Oorderid,@callcount,@callmsg,@writer,@paymode,@paytime,@paystate,@paymoney,@PayOrderId,@Inve1,@Inve2,@sid,@bid,@CustomerName,@tempcode,@sendmoney,@cityid,@ordersource,@isaddpoint,@sendtype,@ulat,@ulng,@shoplat,@shoplng,@sitelat,@sitelng,@ordertype,@noaccess,@validateCode,@iscancel,@ReveInt1,@ReveInt2,@ReveVar,@ReveDate1,@ReveDate2,@IsTimeLimit,@servename,@acountpay,@picktime,@comtime");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
			{
	            new SqlParameter("@UserID", SqlDbType.Int,4) ,            
                new SqlParameter("@UserName", SqlDbType.VarChar,20) ,            
                new SqlParameter("@Tel", SqlDbType.VarChar,50) ,            
                new SqlParameter("@SentTime", SqlDbType.VarChar,256) ,            
                new SqlParameter("@Address", SqlDbType.VarChar,256) ,            
                new SqlParameter("@State", SqlDbType.Int,4) ,            
                new SqlParameter("@TogoId", SqlDbType.Int,4) ,            
                new SqlParameter("@orderTime", SqlDbType.DateTime) ,            
                new SqlParameter("@OrderID", SqlDbType.VarChar,20) ,            
                new SqlParameter("@TotalPrice", SqlDbType.Decimal,5) ,            
                new SqlParameter("@SetStateTime", SqlDbType.DateTime) ,            
                new SqlParameter("@Currentprice", SqlDbType.Decimal,5) ,            
                new SqlParameter("@Remark", SqlDbType.VarChar,256) ,            
                new SqlParameter("@Oorderid", SqlDbType.VarChar,50) ,            
                new SqlParameter("@callcount", SqlDbType.Int,4) ,            
                new SqlParameter("@callmsg", SqlDbType.VarChar,256) ,            
                new SqlParameter("@writer", SqlDbType.VarChar,50) ,            
                new SqlParameter("@paymode", SqlDbType.Int,4) ,            
                new SqlParameter("@paytime", SqlDbType.DateTime) ,            
                new SqlParameter("@paystate", SqlDbType.Int,4) ,            
                new SqlParameter("@paymoney", SqlDbType.Decimal,5) ,            
                new SqlParameter("@PayOrderId", SqlDbType.VarChar,50) ,            
                new SqlParameter("@Inve1", SqlDbType.Int,4) ,            
                new SqlParameter("@Inve2", SqlDbType.VarChar,256) ,            
                new SqlParameter("@sid", SqlDbType.Int,4) ,            
                new SqlParameter("@bid", SqlDbType.Int,4) ,            
                new SqlParameter("@CustomerName", SqlDbType.VarChar,50) ,            
                new SqlParameter("@tempcode", SqlDbType.VarChar,50) ,            
                new SqlParameter("@sendmoney", SqlDbType.Decimal,5) ,            
                new SqlParameter("@cityid", SqlDbType.Int,4) ,            
                new SqlParameter("@ordersource", SqlDbType.Int,4) ,            
                new SqlParameter("@isaddpoint", SqlDbType.Int,4) ,            
                new SqlParameter("@sendtype", SqlDbType.Int,4) ,            
                new SqlParameter("@ulat", SqlDbType.VarChar,50) ,            
                new SqlParameter("@ulng", SqlDbType.VarChar,50) ,            
                new SqlParameter("@shoplat", SqlDbType.VarChar,50) ,            
                new SqlParameter("@shoplng", SqlDbType.VarChar,50) ,            
                new SqlParameter("@sitelat", SqlDbType.VarChar,256) ,            
                new SqlParameter("@sitelng", SqlDbType.VarChar,50) ,            
                new SqlParameter("@ordertype", SqlDbType.Int,4) ,            
                new SqlParameter("@noaccess", SqlDbType.Int,4) ,            
                new SqlParameter("@validateCode", SqlDbType.Int,4) ,            
                new SqlParameter("@iscancel", SqlDbType.Int,4) ,            
                new SqlParameter("@ReveInt1", SqlDbType.Int,4) ,            
                new SqlParameter("@ReveInt2", SqlDbType.Int,4) ,            
                new SqlParameter("@ReveVar", SqlDbType.VarChar,50) ,            
                new SqlParameter("@ReveDate1", SqlDbType.DateTime) ,            
                new SqlParameter("@ReveDate2", SqlDbType.DateTime) ,            
                new SqlParameter("@IsTimeLimit", SqlDbType.Int,4),             
                new SqlParameter("@servename", SqlDbType.VarChar,256),
                new SqlParameter("@acountpay", SqlDbType.Decimal,5),
                new SqlParameter("@picktime", SqlDbType.DateTime),
                new SqlParameter("@comtime", SqlDbType.DateTime)
                
            };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.Tel;
            parameters[3].Value = model.SentTime;
            parameters[4].Value = model.Address;
            parameters[5].Value = model.State;
            parameters[6].Value = model.TogoID;
            parameters[7].Value = model.orderTime;
            parameters[8].Value = model.OrderID;
            parameters[9].Value = model.TotalPrice;
            parameters[10].Value = model.SetStateTime;
            parameters[11].Value = currentprice; // model.Currentprice;
            parameters[12].Value = model.Remark;
            parameters[13].Value = model.Oorderid;
            parameters[14].Value = model.callcount;
            parameters[15].Value = model.callmsg;
            parameters[16].Value = model.writer;
            parameters[17].Value = model.PayMode;
            parameters[18].Value = paytime;
            parameters[19].Value = paystate;
            parameters[20].Value = model.paymoney;
            parameters[21].Value = model.PayOrderId;
            parameters[22].Value = model.Inve1;
            parameters[23].Value = model.Inve2;
            parameters[24].Value = model.sid;
            parameters[25].Value = model.bid;
            parameters[26].Value = model.CustomerName;
            parameters[27].Value = model.tempcode;
            parameters[28].Value = model.sendmoney;
            parameters[29].Value = model.Cityid;
            parameters[30].Value = model.ordersource;
            parameters[31].Value = model.isaddpoint;
            parameters[32].Value = model.sendtype;
            parameters[33].Value = model.ulat;
            parameters[34].Value = model.ulng;
            parameters[35].Value = model.shoplat;
            parameters[36].Value = model.shoplng;
            parameters[37].Value = model.sitelat;
            parameters[38].Value = model.sitelng;
            parameters[39].Value = model.ordertype;
            parameters[40].Value = model.noaccess;
            parameters[41].Value = model.validateCode;
            parameters[42].Value = model.iscancel;
            parameters[43].Value = model.ReveInt1;
            parameters[44].Value = model.ReveInt2;
            parameters[45].Value = model.ReveVar;
            parameters[46].Value = model.ReveDate1;
            parameters[47].Value = model.ReveDate2;
            parameters[48].Value = model.IsTimeLimit;
            parameters[49].Value = model.servename;
            parameters[50].Value = model.acountpay;
            parameters[51].Value = Convert.ToDateTime("1970-1-1");
            parameters[52].Value = Convert.ToDateTime("1970-1-1");

            cmdtextlist.Add(strSql.ToString());
            paraslist.Add(parameters);

            bool flag = SQLHelper.ExecuteSqlTran(CommandType.Text, cmdtextlist, paraslist);
            if (flag)
            {
                return my_list;
            }
            else
            {
                return null;
            }

            //return HJConvert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ExpressOrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ExpressOrder(");
            strSql.Append("UserID,UserName,Tel,SentTime,Address,State,TogoId,orderTime,OrderID,TotalPrice,SetStateTime,Currentprice,Remark,Oorderid,callcount,callmsg,writer,paymode,paytime,paystate,paymoney,PayOrderId,Inve1,Inve2,sid,bid,CustomerName,tempcode,sendmoney,cityid,ordersource,isaddpoint,sendtype,ulat,ulng,shoplat,shoplng,sitelat,sitelng,ordertype,noaccess,validateCode,iscancel,ReveInt1,ReveInt2,ReveVar,ReveDate1,ReveDate2,IsTimeLimit,servename");
            strSql.Append(") values (");
            strSql.Append("@UserID,@UserName,@Tel,@SentTime,@Address,@State,@TogoId,@orderTime,@OrderID,@TotalPrice,@SetStateTime,@Currentprice,@Remark,@Oorderid,@callcount,@callmsg,@writer,@paymode,@paytime,@paystate,@paymoney,@PayOrderId,@Inve1,@Inve2,@sid,@bid,@CustomerName,@tempcode,@sendmoney,@cityid,@ordersource,@isaddpoint,@sendtype,@ulat,@ulng,@shoplat,@shoplng,@sitelat,@sitelng,@ordertype,@noaccess,@validateCode,@iscancel,@ReveInt1,@ReveInt2,@ReveVar,@ReveDate1,@ReveDate2,@IsTimeLimit,@servename");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
			{
	            new SqlParameter("@UserID", SqlDbType.Int,4) ,            
                new SqlParameter("@UserName", SqlDbType.VarChar,20) ,            
                new SqlParameter("@Tel", SqlDbType.VarChar,50) ,            
                new SqlParameter("@SentTime", SqlDbType.VarChar,256) ,            
                new SqlParameter("@Address", SqlDbType.VarChar,256) ,            
                new SqlParameter("@State", SqlDbType.Int,4) ,            
                new SqlParameter("@TogoId", SqlDbType.Int,4) ,            
                new SqlParameter("@orderTime", SqlDbType.DateTime) ,            
                new SqlParameter("@OrderID", SqlDbType.VarChar,20) ,            
                new SqlParameter("@TotalPrice", SqlDbType.Decimal,5) ,            
                new SqlParameter("@SetStateTime", SqlDbType.DateTime) ,            
                new SqlParameter("@Currentprice", SqlDbType.Decimal,5) ,            
                new SqlParameter("@Remark", SqlDbType.VarChar,256) ,            
                new SqlParameter("@Oorderid", SqlDbType.VarChar,50) ,            
                new SqlParameter("@callcount", SqlDbType.Int,4) ,            
                new SqlParameter("@callmsg", SqlDbType.VarChar,256) ,            
                new SqlParameter("@writer", SqlDbType.VarChar,50) ,            
                new SqlParameter("@paymode", SqlDbType.Int,4) ,            
                new SqlParameter("@paytime", SqlDbType.DateTime) ,            
                new SqlParameter("@paystate", SqlDbType.Int,4) ,            
                new SqlParameter("@paymoney", SqlDbType.Decimal,5) ,            
                new SqlParameter("@PayOrderId", SqlDbType.VarChar,50) ,            
                new SqlParameter("@Inve1", SqlDbType.Int,4) ,            
                new SqlParameter("@Inve2", SqlDbType.VarChar,256) ,            
                new SqlParameter("@sid", SqlDbType.Int,4) ,            
                new SqlParameter("@bid", SqlDbType.Int,4) ,            
                new SqlParameter("@CustomerName", SqlDbType.VarChar,50) ,            
                new SqlParameter("@tempcode", SqlDbType.VarChar,50) ,            
                new SqlParameter("@sendmoney", SqlDbType.Decimal,5) ,            
                new SqlParameter("@cityid", SqlDbType.Int,4) ,            
                new SqlParameter("@ordersource", SqlDbType.Int,4) ,            
                new SqlParameter("@isaddpoint", SqlDbType.Int,4) ,            
                new SqlParameter("@sendtype", SqlDbType.Int,4) ,            
                new SqlParameter("@ulat", SqlDbType.VarChar,50) ,            
                new SqlParameter("@ulng", SqlDbType.VarChar,50) ,            
                new SqlParameter("@shoplat", SqlDbType.VarChar,50) ,            
                new SqlParameter("@shoplng", SqlDbType.VarChar,50) ,            
                new SqlParameter("@sitelat", SqlDbType.VarChar,256) ,//50
                new SqlParameter("@sitelng", SqlDbType.VarChar,50) ,            
                new SqlParameter("@ordertype", SqlDbType.Int,4) ,            
                new SqlParameter("@noaccess", SqlDbType.Int,4) ,            
                new SqlParameter("@validateCode", SqlDbType.Int,4) ,            
                new SqlParameter("@iscancel", SqlDbType.Int,4) ,            
                new SqlParameter("@ReveInt1", SqlDbType.Int,4) ,            
                new SqlParameter("@ReveInt2", SqlDbType.Int,4) ,            
                new SqlParameter("@ReveVar", SqlDbType.VarChar,50) ,            
                new SqlParameter("@ReveDate1", SqlDbType.DateTime) ,            
                new SqlParameter("@ReveDate2", SqlDbType.DateTime) ,            
                new SqlParameter("@IsTimeLimit", SqlDbType.Int,4),             
                new SqlParameter("@servename", SqlDbType.VarChar,256)    
            };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.Tel;
            parameters[3].Value = model.SentTime;
            parameters[4].Value = model.Address;
            parameters[5].Value = model.State;
            parameters[6].Value = model.TogoID;
            parameters[7].Value = model.orderTime;
            parameters[8].Value = model.OrderID;
            parameters[9].Value = model.TotalPrice;
            parameters[10].Value = model.SetStateTime;
            parameters[11].Value = model.Currentprice;
            parameters[12].Value = model.Remark;
            parameters[13].Value = model.Oorderid;
            parameters[14].Value = model.callcount;
            parameters[15].Value = model.callmsg;
            parameters[16].Value = model.writer;
            parameters[17].Value = model.PayMode;
            parameters[18].Value = model.paytime;
            parameters[19].Value = model.paystate;
            parameters[20].Value = model.paymoney;
            parameters[21].Value = model.PayOrderId;
            parameters[22].Value = model.Inve1;
            parameters[23].Value = model.Inve2;
            parameters[24].Value = model.sid;
            parameters[25].Value = model.bid;
            parameters[26].Value = model.CustomerName;
            parameters[27].Value = model.tempcode;
            parameters[28].Value = model.sendmoney;
            parameters[29].Value = model.Cityid;
            parameters[30].Value = model.ordersource;
            parameters[31].Value = model.isaddpoint;
            parameters[32].Value = model.sendtype;
            parameters[33].Value = model.ulat;
            parameters[34].Value = model.ulng;
            parameters[35].Value = model.shoplat;
            parameters[36].Value = model.shoplng;
            parameters[37].Value = model.sitelat;
            parameters[38].Value = model.sitelng;
            parameters[39].Value = model.ordertype;
            parameters[40].Value = model.noaccess;
            parameters[41].Value = model.validateCode;
            parameters[42].Value = model.iscancel;
            parameters[43].Value = model.ReveInt1;
            parameters[44].Value = model.ReveInt2;
            parameters[45].Value = model.ReveVar;
            parameters[46].Value = model.ReveDate1;
            parameters[47].Value = model.ReveDate2;
            parameters[48].Value = model.IsTimeLimit;
            parameters[49].Value = model.servename;

            return HJConvert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ExpressOrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ExpressOrder set ");
            strSql.Append(" UserID = @UserID , ");
            strSql.Append(" UserName = @UserName , ");
            strSql.Append(" Tel = @Tel , ");
            strSql.Append(" SentTime = @SentTime , ");
            strSql.Append(" Address = @Address , ");
            strSql.Append(" State = @State , ");
            strSql.Append(" TogoId = @TogoId , ");
            strSql.Append(" orderTime = @orderTime , ");
            strSql.Append(" OrderID = @OrderID , ");
            strSql.Append(" TotalPrice = @TotalPrice , ");
            strSql.Append(" SetStateTime = @SetStateTime , ");
            strSql.Append(" Currentprice = @Currentprice , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" Oorderid = @Oorderid , ");
            strSql.Append(" callcount = @callcount , ");
            strSql.Append(" callmsg = @callmsg , ");
            strSql.Append(" writer = @writer , ");
            strSql.Append(" paymode = @paymode , ");
            strSql.Append(" paytime = @paytime , ");
            strSql.Append(" paystate = @paystate , ");
            strSql.Append(" paymoney = @paymoney , ");
            strSql.Append(" PayOrderId = @PayOrderId , ");
            strSql.Append(" Inve1 = @Inve1 , ");
            strSql.Append(" Inve2 = @Inve2 , ");
            strSql.Append(" sid = @sid , ");
            strSql.Append(" bid = @bid , ");
            strSql.Append(" CustomerName = @CustomerName , ");
            strSql.Append(" tempcode = @tempcode , ");
            strSql.Append(" sendmoney = @sendmoney , ");
            strSql.Append(" cityid = @cityid , ");
            strSql.Append(" ordersource = @ordersource , ");
            strSql.Append(" isaddpoint = @isaddpoint , ");
            strSql.Append(" sendtype = @sendtype , ");
            strSql.Append(" ulat = @ulat , ");
            strSql.Append(" ulng = @ulng , ");
            strSql.Append(" shoplat = @shoplat , ");
            strSql.Append(" shoplng = @shoplng , ");
            strSql.Append(" sitelat = @sitelat , ");
            strSql.Append(" sitelng = @sitelng , ");
            strSql.Append(" ordertype = @ordertype , ");
            strSql.Append(" noaccess = @noaccess , ");
            strSql.Append(" validateCode = @validateCode , ");
            strSql.Append(" iscancel = @iscancel , ");
            strSql.Append(" ReveInt1 = @ReveInt1 , ");
            strSql.Append(" ReveInt2 = @ReveInt2 , ");
            strSql.Append(" ReveVar = @ReveVar , ");
            strSql.Append(" ReveDate1 = @ReveDate1 , ");
            strSql.Append(" ReveDate2 = @ReveDate2 , ");
            strSql.Append(" IsTimeLimit = @IsTimeLimit  ");
            strSql.Append(" where DataID=@DataID ");

            SqlParameter[] parameters = 
            {
	            new SqlParameter("@DataID", SqlDbType.Int,4) ,            
                new SqlParameter("@UserID", SqlDbType.Int,4) ,            
                new SqlParameter("@UserName", SqlDbType.VarChar,20) ,            
                new SqlParameter("@Tel", SqlDbType.VarChar,50) ,            
                new SqlParameter("@SentTime", SqlDbType.VarChar,256) ,            
                new SqlParameter("@Address", SqlDbType.VarChar,256) ,            
                new SqlParameter("@State", SqlDbType.Int,4) ,            
                new SqlParameter("@TogoId", SqlDbType.Int,4) ,            
                new SqlParameter("@orderTime", SqlDbType.DateTime) ,            
                new SqlParameter("@OrderID", SqlDbType.VarChar,20) ,            
                new SqlParameter("@TotalPrice", SqlDbType.Decimal,5) ,            
                new SqlParameter("@SetStateTime", SqlDbType.DateTime) ,            
                new SqlParameter("@Currentprice", SqlDbType.Decimal,5) ,            
                new SqlParameter("@Remark", SqlDbType.VarChar,256) ,            
                new SqlParameter("@Oorderid", SqlDbType.VarChar,50) ,            
                new SqlParameter("@callcount", SqlDbType.Int,4) ,            
                new SqlParameter("@callmsg", SqlDbType.VarChar,256) ,            
                new SqlParameter("@writer", SqlDbType.VarChar,50) ,            
                new SqlParameter("@paymode", SqlDbType.Int,4) ,            
                new SqlParameter("@paytime", SqlDbType.DateTime) ,            
                new SqlParameter("@paystate", SqlDbType.Int,4) ,            
                new SqlParameter("@paymoney", SqlDbType.Decimal,5) ,            
                new SqlParameter("@PayOrderId", SqlDbType.VarChar,50) ,            
                new SqlParameter("@Inve1", SqlDbType.Int,4) ,            
                new SqlParameter("@Inve2", SqlDbType.VarChar,256) ,            
                new SqlParameter("@sid", SqlDbType.Int,4) ,            
                new SqlParameter("@bid", SqlDbType.Int,4) ,            
                new SqlParameter("@CustomerName", SqlDbType.VarChar,50) ,            
                new SqlParameter("@tempcode", SqlDbType.VarChar,50) ,            
                new SqlParameter("@sendmoney", SqlDbType.Decimal,5) ,            
                new SqlParameter("@cityid", SqlDbType.Int,4) ,            
                new SqlParameter("@ordersource", SqlDbType.Int,4) ,            
                new SqlParameter("@isaddpoint", SqlDbType.Int,4) ,            
                new SqlParameter("@sendtype", SqlDbType.Int,4) ,            
                new SqlParameter("@ulat", SqlDbType.VarChar,50) ,            
                new SqlParameter("@ulng", SqlDbType.VarChar,50) ,            
                new SqlParameter("@shoplat", SqlDbType.VarChar,50) ,            
                new SqlParameter("@shoplng", SqlDbType.VarChar,50) ,            
                new SqlParameter("@sitelat", SqlDbType.VarChar,50) ,            
                new SqlParameter("@sitelng", SqlDbType.VarChar,50) ,            
                new SqlParameter("@ordertype", SqlDbType.Int,4) ,            
                new SqlParameter("@noaccess", SqlDbType.Int,4) ,            
                new SqlParameter("@validateCode", SqlDbType.Int,4) ,            
                new SqlParameter("@iscancel", SqlDbType.Int,4) ,            
                new SqlParameter("@ReveInt1", SqlDbType.Int,4) ,            
                new SqlParameter("@ReveInt2", SqlDbType.Int,4) ,            
                new SqlParameter("@ReveVar", SqlDbType.VarChar,50) ,            
                new SqlParameter("@ReveDate1", SqlDbType.DateTime) ,            
                new SqlParameter("@ReveDate2", SqlDbType.DateTime) ,            
                new SqlParameter("@IsTimeLimit", SqlDbType.Int,4)             
      
            };

            parameters[0].Value = model.DataID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.Tel;
            parameters[4].Value = model.SentTime;
            parameters[5].Value = model.Address;
            parameters[6].Value = model.State;
            parameters[7].Value = model.TogoID;
            parameters[8].Value = model.orderTime;
            parameters[9].Value = model.OrderID;
            parameters[10].Value = model.TotalPrice;
            parameters[11].Value = model.SetStateTime;
            parameters[12].Value = model.Currentprice;
            parameters[13].Value = model.Remark;
            parameters[14].Value = model.Oorderid;
            parameters[15].Value = model.callcount;
            parameters[16].Value = model.callmsg;
            parameters[17].Value = model.writer;
            parameters[18].Value = model.PayMode;
            parameters[19].Value = model.paytime;
            parameters[20].Value = model.paystate;
            parameters[21].Value = model.paymoney;
            parameters[22].Value = model.PayOrderId;
            parameters[23].Value = model.Inve1;
            parameters[24].Value = model.Inve2;
            parameters[25].Value = model.sid;
            parameters[26].Value = model.bid;
            parameters[27].Value = model.CustomerName;
            parameters[28].Value = model.tempcode;
            parameters[29].Value = model.sendmoney;
            parameters[30].Value = model.Cityid;
            parameters[31].Value = model.ordersource;
            parameters[32].Value = model.isaddpoint;
            parameters[33].Value = model.sendtype;
            parameters[34].Value = model.ulat;
            parameters[35].Value = model.ulng;
            parameters[36].Value = model.shoplat;
            parameters[37].Value = model.shoplng;
            parameters[38].Value = model.sitelat;
            parameters[39].Value = model.sitelng;
            parameters[40].Value = model.ordertype;
            parameters[41].Value = model.noaccess;
            parameters[42].Value = model.validateCode;
            parameters[43].Value = model.iscancel;
            parameters[44].Value = model.ReveInt1;
            parameters[45].Value = model.ReveInt2;
            parameters[46].Value = model.ReveVar;
            parameters[47].Value = model.ReveDate1;
            parameters[48].Value = model.ReveDate2;
            parameters[49].Value = model.IsTimeLimit;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataID</param>
        /// <returns>ExpressOrderInfo</returns>
        public ExpressOrderInfo GetModel(int DataID)
        {
            SqlParameter parameter = new SqlParameter("@DataID", SqlDbType.Int, 4);
            string sql = "select *,(select cname from City where cid = ExpressOrder.cityid ) as cityname,";

            sql += "(select name from deliver where dataid=ExpressOrder.Inve1) as delivername from ExpressOrder  where DataID=@DataID";
            parameter.Value = DataID;
            ExpressOrderInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new ExpressOrderInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.UserID = HJConvert.ToInt32(dr["UserID"]);
                    model.UserName = HJConvert.ToString(dr["UserName"]);
                    model.Tel = HJConvert.ToString(dr["Tel"]);
                    model.SentTime = HJConvert.ToString(dr["SentTime"]);
                    model.Address = HJConvert.ToString(dr["Address"]);
                    model.State = HJConvert.ToInt32(dr["State"]);
                    model.TogoID = HJConvert.ToInt32(dr["TogoId"]);
                    model.orderTime = HJConvert.ToDateTime(dr["orderTime"]);
                    model.OrderID = HJConvert.ToString(dr["OrderID"]);
                    model.TotalPrice = HJConvert.ToDecimal(dr["TotalPrice"]);
                    model.SetStateTime = HJConvert.ToDateTime(dr["SetStateTime"]);
                    model.Currentprice = HJConvert.ToDecimal(dr["Currentprice"]);
                    model.Remark = HJConvert.ToString(dr["Remark"]);
                    model.Oorderid = HJConvert.ToString(dr["Oorderid"]);
                    model.callcount = HJConvert.ToInt32(dr["callcount"]);
                    model.callmsg = HJConvert.ToString(dr["callmsg"]);
                    model.writer = HJConvert.ToString(dr["writer"]);
                    model.PayMode = HJConvert.ToInt32(dr["paymode"]);
                    model.paytime = HJConvert.ToDateTime(dr["paytime"]);
                    model.paystate = HJConvert.ToInt32(dr["paystate"]);
                    model.paymoney = HJConvert.ToDecimal(dr["paymoney"]);
                    model.PayOrderId = HJConvert.ToString(dr["PayOrderId"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.delivername = HJConvert.ToString(dr["delivername"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.sid = HJConvert.ToInt32(dr["sid"]);
                    model.bid = HJConvert.ToInt32(dr["bid"]);
                    model.CustomerName = HJConvert.ToString(dr["CustomerName"]);
                    model.tempcode = HJConvert.ToString(dr["tempcode"]);
                    model.sendmoney = HJConvert.ToDecimal(dr["sendmoney"]);
                    model.Cityid = HJConvert.ToInt32(dr["cityid"]);
                    model.ordersource = HJConvert.ToInt32(dr["ordersource"]);
                    model.isaddpoint = HJConvert.ToInt32(dr["isaddpoint"]);
                    model.sendtype = HJConvert.ToInt32(dr["sendtype"]);
                    model.ulat = HJConvert.ToString(dr["ulat"]);
                    model.ulng = HJConvert.ToString(dr["ulng"]);
                    model.shoplat = HJConvert.ToString(dr["shoplat"]);
                    model.shoplng = HJConvert.ToString(dr["shoplng"]);
                    model.sitelat = HJConvert.ToString(dr["sitelat"]);
                    model.sitelng = HJConvert.ToString(dr["sitelng"]);
                    model.ordertype = HJConvert.ToInt32(dr["ordertype"]);
                    model.noaccess = HJConvert.ToInt32(dr["noaccess"]);
                    model.validateCode = HJConvert.ToInt32(dr["validateCode"]);
                    model.iscancel = HJConvert.ToInt32(dr["iscancel"]);
                    model.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    model.ReveInt2 = HJConvert.ToInt32(dr["ReveInt2"]);
                    model.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    model.ReveDate1 = HJConvert.ToDateTime(dr["ReveDate1"]);
                    model.ReveDate2 = HJConvert.ToDateTime(dr["ReveDate2"]);
                    model.IsTimeLimit = HJConvert.ToInt32(dr["IsTimeLimit"]);
                    model.CityName = HJConvert.ToString(dr["CityName"]);

                }
            }
            return model;
        }
        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataID</param>
        /// <returns>ExpressOrderInfo</returns>
        public ExpressOrderInfo GetModel(string OrderID)
        {
            SqlParameter parameter = new SqlParameter("@OrderID", SqlDbType.VarChar, 50);
            string sql = "select *,(select cname from City where cid = ExpressOrder.cityid ) as cityname,(select name from deliver where dataid=ExpressOrder.Inve1) as delivername,(select Phone from deliver where dataid=ExpressOrder.Inve1) as delivertel from ExpressOrder  where OrderID=@OrderID";
            parameter.Value = OrderID;
            ExpressOrderInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new ExpressOrderInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.UserID = HJConvert.ToInt32(dr["UserID"]);
                    model.UserName = HJConvert.ToString(dr["UserName"]);
                    model.Tel = HJConvert.ToString(dr["Tel"]);
                    model.SentTime = HJConvert.ToString(dr["SentTime"]);
                    model.Address = HJConvert.ToString(dr["Address"]);
                    model.State = HJConvert.ToInt32(dr["State"]);
                    model.TogoID = HJConvert.ToInt32(dr["TogoId"]);
                    model.orderTime = HJConvert.ToDateTime(dr["orderTime"]);
                    model.OrderID = HJConvert.ToString(dr["OrderID"]);
                    model.TotalPrice = HJConvert.ToDecimal(dr["TotalPrice"]);
                    model.SetStateTime = HJConvert.ToDateTime(dr["SetStateTime"]);
                    model.Currentprice = HJConvert.ToDecimal(dr["Currentprice"]);
                    model.Remark = HJConvert.ToString(dr["Remark"]);
                    model.Oorderid = HJConvert.ToString(dr["Oorderid"]);
                    model.callcount = HJConvert.ToInt32(dr["callcount"]);
                    model.callmsg = HJConvert.ToString(dr["callmsg"]);
                    model.writer = HJConvert.ToString(dr["writer"]);
                    model.PayMode = HJConvert.ToInt32(dr["paymode"]);
                    model.paytime = HJConvert.ToDateTime(dr["paytime"]);
                    model.paystate = HJConvert.ToInt32(dr["paystate"]);
                    model.paymoney = HJConvert.ToDecimal(dr["paymoney"]);
                    model.PayOrderId = HJConvert.ToString(dr["PayOrderId"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.sid = HJConvert.ToInt32(dr["sid"]);
                    model.bid = HJConvert.ToInt32(dr["bid"]);
                    model.CustomerName = HJConvert.ToString(dr["CustomerName"]);
                    model.tempcode = HJConvert.ToString(dr["tempcode"]);
                    model.sendmoney = HJConvert.ToDecimal(dr["sendmoney"]);
                    model.Cityid = HJConvert.ToInt32(dr["cityid"]);
                    model.ordersource = HJConvert.ToInt32(dr["ordersource"]);
                    model.isaddpoint = HJConvert.ToInt32(dr["isaddpoint"]);
                    model.sendtype = HJConvert.ToInt32(dr["sendtype"]);
                    model.ulat = HJConvert.ToString(dr["ulat"]);
                    model.ulng = HJConvert.ToString(dr["ulng"]);
                    model.shoplat = HJConvert.ToString(dr["shoplat"]);
                    model.shoplng = HJConvert.ToString(dr["shoplng"]);
                    model.sitelat = HJConvert.ToString(dr["sitelat"]);
                    model.sitelng = HJConvert.ToString(dr["sitelng"]);
                    model.ordertype = HJConvert.ToInt32(dr["ordertype"]);
                    model.noaccess = HJConvert.ToInt32(dr["noaccess"]);
                    model.validateCode = HJConvert.ToInt32(dr["validateCode"]);
                    model.iscancel = HJConvert.ToInt32(dr["iscancel"]);
                    model.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    model.ReveInt2 = HJConvert.ToInt32(dr["ReveInt2"]);
                    model.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    model.ReveDate1 = HJConvert.ToDateTime(dr["ReveDate1"]);
                    model.ReveDate2 = HJConvert.ToDateTime(dr["ReveDate2"]);
                    model.IsTimeLimit = HJConvert.ToInt32(dr["IsTimeLimit"]);
                    model.CityName = HJConvert.ToString(dr["CityName"]);
                    model.delivername = HJConvert.ToString(dr["delivername"]);
                    model.delivertel = HJConvert.ToString(dr["delivertel"]);
                    //model.isaddDeliver = HJConvert.ToDecimal(dr["isaddDeliver"]);

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
                new SqlParameter("@tblName" ,SqlDbType.VarChar , 30),
                new SqlParameter("@strWhere" , SqlDbType.VarChar  , 300)
            };
            parameters[0].Value = "ExpressOrder";
            parameters[1].Value = strWhere;
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", parameters);
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
        public IList<ExpressOrderInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ExpressOrderInfo> infos = new List<ExpressOrderInfo>();
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
            parameters[0].Value = "ExpressOrder";
            parameters[1].Value = "*,(select Name from Deliver where dataid = ExpressOrder.Inve1) as delivername";
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
                    ExpressOrderInfo model = new ExpressOrderInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.UserID = HJConvert.ToInt32(dr["UserID"]);
                    model.UserName = HJConvert.ToString(dr["UserName"]);
                    model.Tel = HJConvert.ToString(dr["Tel"]);
                    model.SentTime = HJConvert.ToString(dr["SentTime"]);
                    model.Address = HJConvert.ToString(dr["Address"]);
                    model.State = HJConvert.ToInt32(dr["State"]);
                    model.TogoID = HJConvert.ToInt32(dr["TogoId"]);
                    model.orderTime = HJConvert.ToDateTime(dr["orderTime"]);
                    model.OrderID = HJConvert.ToString(dr["OrderID"]);
                    model.TotalPrice = HJConvert.ToDecimal(dr["TotalPrice"]);
                    model.SetStateTime = HJConvert.ToDateTime(dr["SetStateTime"]);
                    model.Currentprice = HJConvert.ToDecimal(dr["Currentprice"]);
                    model.Remark = HJConvert.ToString(dr["Remark"]);
                    model.Oorderid = HJConvert.ToString(dr["Oorderid"]);
                    model.callcount = HJConvert.ToInt32(dr["callcount"]);
                    model.callmsg = HJConvert.ToString(dr["callmsg"]);
                    model.writer = HJConvert.ToString(dr["writer"]);
                    model.PayMode = HJConvert.ToInt32(dr["paymode"]);
                    model.paytime = HJConvert.ToDateTime(dr["paytime"]);
                    model.paystate = HJConvert.ToInt32(dr["paystate"]);
                    model.paymoney = HJConvert.ToDecimal(dr["paymoney"]);
                    model.PayOrderId = HJConvert.ToString(dr["PayOrderId"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.sid = HJConvert.ToInt32(dr["sid"]);
                    model.bid = HJConvert.ToInt32(dr["bid"]);
                    model.CustomerName = HJConvert.ToString(dr["CustomerName"]);
                    model.tempcode = HJConvert.ToString(dr["tempcode"]);
                    model.sendmoney = HJConvert.ToInt32(dr["sendmoney"]);
                    model.Cityid = HJConvert.ToInt32(dr["cityid"]);
                    model.delivername = HJConvert.ToString(dr["delivername"]);
                    model.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    model.ReveInt2 = HJConvert.ToInt32(dr["ReveInt2"]);
                    infos.Add(model);
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
        public int DelETogoOrder(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ExpressOrder where DataID=@DataID");
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
            str.Append("delete from ExpressOrderDeliver where OrderID in ({0});");
            str.Append("delete from ExpressOrder where DataID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 当打印机获取完订单时，把这个值设置成当前时间。 （add by yangxioalong@ihangjing.com）
        /// 过一个时间段后，如果此订单的状态还没有更改成成功，则为失败。
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int UpdateOrderStateTime(string orderId)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update ExpressOrder set SetStateTime=" + DateTime.Now + " where OrderID=@OrderID");

            SqlParameter[] Para = 
            {
                new SqlParameter("@OrderID",SqlDbType.VarChar,20)
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
            str.Append("update ExpressOrder set State=@State,SetStateTime='" + DateTime.Now + "' where OrderID=@OrderID");
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
        /// <param name="ReveInt2">是否被接单，0否 1是</param>
        public int UpdateState(string OrderID, int State, int ReveInt2, int deliverid)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update ExpressOrder set State=@State,ReveInt2=@ReveInt2,");

            str.Append(" Inve1=@Inve1, SetStateTime='" + DateTime.Now + "' where OrderID=@OrderID");
            SqlParameter[] Para = 
            {
                new SqlParameter("@OrderID",SqlDbType.VarChar,100),
                new SqlParameter("@State",SqlDbType.Int),
                new SqlParameter("@ReveInt2",SqlDbType.Int),
                 new SqlParameter("@Inve1",SqlDbType.Int)
            };
            Para[0].Value = OrderID;
            Para[1].Value = State;
            Para[2].Value = ReveInt2;
            Para[3].Value = deliverid;
            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 修改订单的状态。
        /// </summary>
        /// <param name="OrderID">订单编号</param>
        /// <param name="State">要修改的订单状态</param>
        /// <param name="ReveInt2">是否被接单，0否 1是</param>
        public int UpdateState(string OrderID, int State, int ReveInt2)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update ExpressOrder set State=@State,ReveInt2=@ReveInt2,");

            str.Append(" SetStateTime='" + DateTime.Now + "' where OrderID=@OrderID");
            SqlParameter[] Para = 
            {
                new SqlParameter("@OrderID",SqlDbType.VarChar,100),
                new SqlParameter("@State",SqlDbType.Int),
                new SqlParameter("@ReveInt2",SqlDbType.Int)
            };
            Para[0].Value = OrderID;
            Para[1].Value = State;
            Para[2].Value = ReveInt2;
            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 订单加分,判断是不是一个订单
        /// </summary>
        /// <param name="orerid"></param>
        /// <returns></returns>
        public int AddPoint(int orerid)
        {
            ExpressOrderInfo model = new ExpressOrder().GetModel(orerid);
            WebBasicInfo myset = new WebBasic().GetModel(25); //, model.Cityid
            int rat = Convert.ToInt32(myset.Value);
            int point = Convert.ToInt32(model.Currentprice) * rat;
            string sql = "update ECustomer set point = point + " + point + "  where dataid = " + model.UserID + ";";
            sql += "insert into EPointRecord(userid , point , event , time)values(" + model.UserID + " , " + point + "  ,'点餐成功获得积分" + point + "个' , '" + DateTime.Now + "');";
            ECustomer daluser = new ECustomer();
            ECustomerInfo user = daluser.GetModel(model.UserID);
            if (user != null && user.RID.Trim() != "")
            {
                //推荐人加分
                ECustomerInfo tuser = daluser.GetModel(Convert.ToInt32(user.RID));
                if (tuser != null)
                {
                    string mysql = "UserID = " + user.DataID;
                    int count = GetCount(mysql);
                    if (count == 1)
                    {
                        myset = new WebBasic().GetModel(20); //, model.Cityid
                        int appfrientpoint = Convert.ToInt32(myset.Value);
                        sql += "update ECustomer set point = point + " + appfrientpoint + "  where dataid = " + tuser.DataID + ";";
                        sql += "insert into EPointRecord(userid , point , event , time)values(" + tuser.DataID + " , " + appfrientpoint + "  ,'你推荐的" + user.Name + "点外成功,获取积分" + appfrientpoint + "个' , '" + DateTime.Now + "');";
                    }
                }
            }
            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        /// <summary>
        /// 修改订单的处理人
        /// </summary>
        /// <param name="OrderID">订单编号</param>
        /// <param name="State">要修改的订单状态</param>
        public int UpdataState(string OrderID, string State)
        {

            StringBuilder str = new StringBuilder();
            str.Append("update ExpressOrder set writer=@writer where OrderID=@OrderID");
            SqlParameter[] Para = 
            {
                new SqlParameter("@OrderID",SqlDbType.VarChar,20),
                new SqlParameter("@writer",SqlDbType.VarChar , 20)
            };
            Para[0].Value = OrderID;
            Para[1].Value = State;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdatePayState(ExpressOrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ExpressOrder set ");
            strSql.Append("paymode=@paymode,");
            strSql.Append("paytime=@paytime,");
            strSql.Append("paystate=@paystate,");
            strSql.Append("paymoney=@paymoney");
            strSql.Append(" where OrderID=@OrderID ");
            SqlParameter[] parameters =
            {
				new SqlParameter("@OrderID", SqlDbType.VarChar,256),
                new SqlParameter("@paymode", SqlDbType.Int),
				new SqlParameter("@paytime", SqlDbType.DateTime),
				new SqlParameter("@paymoney", SqlDbType.Decimal,5),
				new SqlParameter("@paystate", SqlDbType.Int,4)
            };
            parameters[0].Value = model.OrderID;
            parameters[1].Value = model.PayMode;
            parameters[2].Value = model.paytime;
            parameters[3].Value = model.paymoney;
            parameters[4].Value = model.paystate;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdatePayStateWithPayOrderId(ExpressOrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ExpressOrder set ");
            strSql.Append("paymode=@paymode,");
            strSql.Append("paytime=@paytime,");
            strSql.Append("paystate=@paystate,");
            strSql.Append("paymoney=@paymoney");
            strSql.Append(" where PayOrderId=@PayOrderId ");
            SqlParameter[] parameters =
            {
                new SqlParameter("@paymode", SqlDbType.VarChar,256),
				new SqlParameter("@PayOrderId", SqlDbType.VarChar,256),
				new SqlParameter("@paytime", SqlDbType.DateTime),
				new SqlParameter("@paymoney", SqlDbType.Decimal,5),
				new SqlParameter("@paystate", SqlDbType.Int,4)
            };
            parameters[0].Value = model.PayMode;
            parameters[1].Value = model.PayOrderId;
            parameters[2].Value = model.paytime;
            parameters[3].Value = model.paymoney;
            parameters[4].Value = model.paystate;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 调度页面获取列表 包含配送信息 2012-5-9 zjf@ihangjing.com
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<ExpressOrderInfo> GetListForDelive(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ExpressOrderInfo> infos = new List<ExpressOrderInfo>();
            SqlParameter[] parameters = 
	        {   
                new SqlParameter("@pagesize", SqlDbType.Int),
		        new SqlParameter("@pageindex", SqlDbType.Int),
		        new SqlParameter("@orderfield", SqlDbType.VarChar,50),
		        new SqlParameter("@ordertype", SqlDbType.VarChar,5),
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

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "ExpressOrder_GetOrderList", parameters))
            {
                while (dr.Read())
                {
                    ExpressOrderInfo info = new ExpressOrderInfo();

                    info.DataID = HJConvert.ToInt32(dr["DataID"]);
                    info.UserID = HJConvert.ToInt32(dr["UserID"]);
                    info.UserName = HJConvert.ToString(dr["UserName"]);
                    info.Tel = HJConvert.ToString(dr["Tel"]);
                    info.SentTime = HJConvert.ToString(dr["SentTime"]);
                    info.Address = HJConvert.ToString(dr["Address"]);
                    info.State = HJConvert.ToInt32(dr["State"]);
                    info.orderTime = HJConvert.ToDateTime(dr["orderTime"]);
                    info.OrderID = HJConvert.ToString(dr["OrderID"]);
                    info.TotalPrice = HJConvert.ToDecimal(dr["totalprice"]);

                    info.SetStateTime = HJConvert.ToDateTime(dr["SetStateTime"]);
                    info.TogoID = HJConvert.ToInt32(dr["togoid"]);
                    info.Inve1 = HJConvert.ToInt32(dr["OInve1"]);
                    info.Inve2 = HJConvert.ToString(dr["OInve2"]);
                    info.Currentprice = HJConvert.ToDecimal(dr["Currentprice"]);
                    info.Remark = HJConvert.ToString(dr["Remark"]);
                    info.sendmoney = HJConvert.ToInt32(dr["sendmoney"]);
                    info.CustomerName = HJConvert.ToString(dr["CustomerName"]);

                    info.writer = HJConvert.ToString(dr["writer"]);
                    info.paystate = HJConvert.ToInt32(dr["paystate"]);

                    info.ordersource = HJConvert.ToInt32(dr["Ordersource"]);
                    info.ReveInt1 = HJConvert.ToInt32(dr["reveint1"]);

                    info.callmsg = HJConvert.ToString(dr["callmsg"]);
                    info.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    info.Oorderid = HJConvert.ToString(dr["Oorderid"]);

                    info.sitelat = HJConvert.ToString(dr["sitelat"]);


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

                    info.DeliveInfo = dinfo;

                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 获取报表记录总是和总金额(getmoney页面)
        /// </summary>
        /// <param name="togoNum"></param>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        public ETogoOrderInfo GetCountAndTotal1(string where)
        {
            string sql = "select sum(TotalPrice) as  OrderTotal,Count(*) as OrderCount from ExpressOrder where " + where;

            ETogoOrderInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (dr.Read())
                {
                    model = new ETogoOrderInfo();
                    model.OrderCount = HJConvert.ToInt32(dr["OrderCount"]);
                    model.OrderTotal = HJConvert.ToDecimal(dr["OrderTotal"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataID</param>
        /// <returns>ExpressOrderInfo</returns>
        public ExpressOrderInfo GetModelByOrderid(string orderid)
        {
            string sql = "select *,(select cname from City where cid=ExpressOrder.cityid) cityname from ExpressOrder  where orderid=@orderid";

            SqlParameter parameter = new SqlParameter("@orderid", SqlDbType.VarChar, 200);
            parameter.Value = orderid;
            ExpressOrderInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new ExpressOrderInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.UserID = HJConvert.ToInt32(dr["UserID"]);
                    model.UserName = HJConvert.ToString(dr["UserName"]);
                    model.Tel = HJConvert.ToString(dr["Tel"]);
                    model.SentTime = HJConvert.ToString(dr["SentTime"]);
                    model.Address = HJConvert.ToString(dr["Address"]);
                    model.State = HJConvert.ToInt32(dr["State"]);
                    model.TogoID = HJConvert.ToInt32(dr["TogoId"]);
                    model.orderTime = HJConvert.ToDateTime(dr["orderTime"]);
                    model.OrderID = HJConvert.ToString(dr["OrderID"]);
                    model.TotalPrice = HJConvert.ToDecimal(dr["TotalPrice"]);
                    model.SetStateTime = HJConvert.ToDateTime(dr["SetStateTime"]);
                    model.Currentprice = HJConvert.ToDecimal(dr["Currentprice"]);
                    model.Remark = HJConvert.ToString(dr["Remark"]);
                    model.Oorderid = HJConvert.ToString(dr["Oorderid"]);
                    model.callcount = HJConvert.ToInt32(dr["callcount"]);
                    model.callmsg = HJConvert.ToString(dr["callmsg"]);
                    model.writer = HJConvert.ToString(dr["writer"]);
                    model.PayMode = HJConvert.ToInt32(dr["paymode"]);
                    model.paytime = HJConvert.ToDateTime(dr["paytime"]);
                    model.paystate = HJConvert.ToInt32(dr["paystate"]);
                    model.paymoney = HJConvert.ToDecimal(dr["paymoney"]);
                    model.PayOrderId = HJConvert.ToString(dr["PayOrderId"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.sid = HJConvert.ToInt32(dr["sid"]);
                    model.bid = HJConvert.ToInt32(dr["bid"]);
                    model.CustomerName = HJConvert.ToString(dr["CustomerName"]);
                    model.tempcode = HJConvert.ToString(dr["tempcode"]);
                    model.sendmoney = HJConvert.ToDecimal(dr["sendmoney"]);
                    model.Cityid = HJConvert.ToInt32(dr["cityid"]);
                    model.ordersource = HJConvert.ToInt32(dr["ordersource"]);
                    model.isaddpoint = HJConvert.ToInt32(dr["isaddpoint"]);
                    model.sendtype = HJConvert.ToInt32(dr["sendtype"]);
                    model.ulat = HJConvert.ToString(dr["ulat"]);
                    model.ulng = HJConvert.ToString(dr["ulng"]);
                    model.shoplat = HJConvert.ToString(dr["shoplat"]);
                    model.shoplng = HJConvert.ToString(dr["shoplng"]);
                    model.sitelat = HJConvert.ToString(dr["sitelat"]);
                    model.sitelng = HJConvert.ToString(dr["sitelng"]);
                    model.ordertype = HJConvert.ToInt32(dr["ordertype"]);
                    model.noaccess = HJConvert.ToInt32(dr["noaccess"]);
                    model.validateCode = HJConvert.ToInt32(dr["validateCode"]);
                    model.iscancel = HJConvert.ToInt32(dr["iscancel"]);
                    model.ReveInt1 = HJConvert.ToInt32(dr["ReveInt1"]);
                    model.ReveInt2 = HJConvert.ToInt32(dr["ReveInt2"]);
                    model.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    model.ReveDate1 = HJConvert.ToDateTime(dr["ReveDate1"]);
                    model.ReveDate2 = HJConvert.ToDateTime(dr["ReveDate2"]);
                    model.IsTimeLimit = HJConvert.ToInt32(dr["IsTimeLimit"]);
                    model.CityName = HJConvert.ToString(dr["CityName"]);

                }
            }
            return model;
        }


        /// <summary>
        /// 要推给骑士的订单数量
        /// </summary>
        public OrderCountInfo SendDeliverOrderCount(string deliverid)
        {
            OrderCountInfo model = new OrderCountInfo();
            model.rat = 0;
            model.CountIntValue = 0;


            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "SendDeliverExpressOrderCount", new SqlParameter("@dataid", deliverid)))
            {
                while (dr.Read())
                {
                    model.CountIntValue = HJConvert.ToInt32(dr["ordercount"]);
                }
            }
            return model;
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

            return SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "ExpressOrder_AddOrderRecord", Para);
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
            str.Append("update ExpressOrder set State=3,SetStateTime='" + DateTime.Now + "' where OrderID=@OrderID");
            SqlParameter[] Para = 
            {
                new SqlParameter("@OrderID",SqlDbType.VarChar,20),
            };
            Para[0].Value = orderid;

            SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);

            return 1;
        }

        /// <summary>
        /// 订单加分,判断是不是一个订单  此方法需要改进以及测试 2011.6.10
        /// </summary>
        /// <param name="orerid"></param>
        /// <returns></returns>
        public int AddPoint(string orerid)
        {
            ExpressOrderInfo model = new ExpressOrder().GetModelByOrderid(orerid);

            WebBasicInfo mysetpoint = new WebBasic().GetModel(42);
            int ratpoint = Convert.ToInt32(mysetpoint.Value);
            //积分计算公式： 订单总金额*积分倍数
            int point = Convert.ToInt32(Convert.ToInt32(model.TotalPrice) * ratpoint);

            //int point = Convert.ToInt32(model.OrderSums);
            SqlParameter[] Para = 
            {
                new SqlParameter("@userid",SqlDbType.Int,5),
                new SqlParameter("@addpoint",SqlDbType.Int , 5),
                new SqlParameter("@orderid",SqlDbType.VarChar,20),
            };
            Para[0].Value = model.UserID;
            Para[1].Value = point;
            Para[2].Value = orerid;

            SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "addOrderPoint", Para);



            ECustomer daluser = new ECustomer();
            ECustomerInfo user = daluser.GetModel(model.UserID);
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
                        sql += "insert into EPointRecord(userid , point , event , time)values(" + tuser.DataID + " , " + rat + "  ,'跑腿订单成功,获取积分" + rat + "个' , '" + DateTime.Now + "');";
                        SQLHelper.ExecuteNonQuery(CommandType.Text, sql, null);
                    }
                }


            }

            return 1;
        }

    }
}

