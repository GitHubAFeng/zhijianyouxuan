/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * Created by wanghui at 2011-5-12 9:03:30.
 * E-Mail   : wanghui@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;

using System.Data.SqlClient;

namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 数据访问类ETogoShoppingCart。
    /// </summary>
    public class ETogoShoppingCart
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ETogoShoppingCartInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ETogoShoppingCart(");
            strSql.Append("UId,TogoId,TogoName,PId,PName,PPrice,PNum,Currentprice,tempcode,Funit,material,sid,owername,addprice,sname,ReveInt1,ReveInt2,ReveVar1,ReveVar2)");
            strSql.Append(" values (");
            strSql.Append("@UId,@TogoId,@TogoName,@PId,@PName,@PPrice,@PNum,@Currentprice,@tempcode,@Funit,@material,@sid,@owername,@addprice,@sname,@ReveInt1,@ReveInt2,@ReveVar1,@ReveVar2)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@UId", SqlDbType.Int,4),
				new SqlParameter("@TogoId", SqlDbType.Int,4),
				new SqlParameter("@TogoName", SqlDbType.VarChar,256),
				new SqlParameter("@PId", SqlDbType.Int,4),
				new SqlParameter("@PName", SqlDbType.VarChar,256),
				new SqlParameter("@PPrice", SqlDbType.Decimal),
				new SqlParameter("@PNum", SqlDbType.Int,4),
				new SqlParameter("@Currentprice", SqlDbType.Decimal),
                new SqlParameter("@tempcode", SqlDbType.VarChar,256),
                new SqlParameter("@Funit", SqlDbType.VarChar,50) ,
                new SqlParameter("@material", SqlDbType.VarChar,200) ,
                new SqlParameter("@sid", SqlDbType.Int,4) ,
                new SqlParameter("@owername", SqlDbType.VarChar,256) ,
                new SqlParameter("@addprice", SqlDbType.Decimal,5) ,
                new SqlParameter("@sname", SqlDbType.VarChar,256) ,
                new SqlParameter("@ReveInt1", SqlDbType.Int,4) ,
                new SqlParameter("@ReveInt2", SqlDbType.Int,4) ,
                new SqlParameter("@ReveVar1", SqlDbType.VarChar,256) ,
                new SqlParameter("@ReveVar2", SqlDbType.VarChar,256) ,
            };
            parameters[0].Value = model.UId;
            parameters[1].Value = model.TogoId;
            parameters[2].Value = model.TogoName;
            parameters[3].Value = model.PId;
            parameters[4].Value = model.PName;
            parameters[5].Value = model.PPrice;
            parameters[6].Value = model.PNum;
            parameters[7].Value = model.Currentprice;
            parameters[8].Value = model.tempcode;
            parameters[9].Value = model.Funit;
            parameters[10].Value = model.material;
            parameters[11].Value = model.sid;
            parameters[12].Value = model.owername;
            parameters[13].Value = model.addprice;
            parameters[14].Value = model.sname;
            parameters[15].Value = model.ReveInt1;
            parameters[16].Value = model.ReveInt2;
            parameters[17].Value = model.ReveVar1;
            parameters[18].Value = model.ReveVar2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ETogoShoppingCartInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ETogoShoppingCart set ");
            strSql.Append("UId=@UId,");
            strSql.Append("TogoId=@TogoId,");
            strSql.Append("TogoName=@TogoName,");
            strSql.Append("PId=@PId,");
            strSql.Append("PName=@PName,");
            strSql.Append("PPrice=@PPrice,");
            strSql.Append("PNum=@PNum,");
            strSql.Append("Currentprice=@Currentprice");
            strSql.Append(" where DataId=@DataId ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataId", SqlDbType.Int,4),
				new SqlParameter("@UId", SqlDbType.Int,4),
				new SqlParameter("@TogoId", SqlDbType.Int,4),
				new SqlParameter("@TogoName", SqlDbType.VarChar,256),
				new SqlParameter("@PId", SqlDbType.Int,4),
				new SqlParameter("@PName", SqlDbType.VarChar,256),
				new SqlParameter("@PPrice", SqlDbType.Decimal,5),
				new SqlParameter("@PNum", SqlDbType.Int,4),
				new SqlParameter("@Currentprice", SqlDbType.Decimal,5)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.UId;
            parameters[2].Value = model.TogoId;
            parameters[3].Value = model.TogoName;
            parameters[4].Value = model.PId;
            parameters[5].Value = model.PName;
            parameters[6].Value = model.PPrice;
            parameters[7].Value = model.PNum;
            parameters[8].Value = model.Currentprice;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>ETogoShoppingCartInfo</returns>
        public ETogoShoppingCartInfo GetModel(int DataId)
        {
            string sql = "select DataId,UId,TogoId,TogoName,PId,PName,PPrice,PNum,Currentprice from ETogoShoppingCart where DataId=@DataId ";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            ETogoShoppingCartInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new ETogoShoppingCartInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.UId = HJConvert.ToInt32(dr["UId"]);
                    model.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    model.TogoName = HJConvert.ToString(dr["TogoName"]);
                    model.PId = HJConvert.ToInt32(dr["PId"]);
                    model.PName = HJConvert.ToString(dr["PName"]);
                    model.PPrice = HJConvert.ToDecimal(dr["PPrice"]);
                    model.PNum = HJConvert.ToInt32(dr["PNum"]);
                    model.Currentprice = HJConvert.ToDecimal(dr["Currentprice"]);
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
                new SqlParameter("@strWhere" , SqlDbType.VarChar ,500)
            };
            parameters[0].Value = "ETogoShoppingCart";
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
        public IList<ETogoShoppingCartInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ETogoShoppingCartInfo> infos = new List<ETogoShoppingCartInfo>();
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
            parameters[0].Value = "ETogoShoppingCart";
            parameters[1].Value = "DataId,UId,TogoId,TogoName,PId,PName,PPrice,PNum,Currentprice";
            parameters[2].Value = "DataId";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    ETogoShoppingCartInfo info = new ETogoShoppingCartInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.UId = HJConvert.ToInt32(dr["UId"]);
                    info.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    info.TogoName = HJConvert.ToString(dr["TogoName"]);
                    info.PId = HJConvert.ToInt32(dr["PId"]);
                    info.PName = HJConvert.ToString(dr["PName"]);
                    info.PPrice = HJConvert.ToDecimal(dr["PPrice"]);
                    info.PNum = HJConvert.ToInt32(dr["PNum"]);
                    info.Currentprice = HJConvert.ToDecimal(dr["Currentprice"]);
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
        public int DelETogoShoppingCart(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ETogoShoppingCart where DataId=@DataId");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@DataId",SqlDbType.Int)
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
            str.Append("delete from ETogoShoppingCart where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 检查购物车中是否已经存在商品 存在则返回商品  不存在则返回null
        /// </summary>
        /// <param name="Uid"></param>
        /// <param name="Pid"></param>
        /// <returns></returns>
        public Hangjing.Model.ETogoShoppingCartInfo ExistProduct(string tempcode, int Pid)
        {
            string sql = "select * from ETogoShoppingCart where tempCode=@Uid and Pid=@Pid";
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@Uid",SqlDbType.VarChar , 50),
                new SqlParameter("@Pid", SqlDbType.Int)
	        };
            Para[0].Value = tempcode;
            Para[1].Value = Pid;

            ETogoShoppingCartInfo model = null;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, Para))
            {
                if (dr.Read())
                {
                    model = new ETogoShoppingCartInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.UId = HJConvert.ToInt32(dr["UId"]);
                    model.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    model.TogoName = HJConvert.ToString(dr["TogoName"]);
                    model.PId = HJConvert.ToInt32(dr["PId"]);
                    model.PName = HJConvert.ToString(dr["PName"]);
                    model.PPrice = HJConvert.ToDecimal(dr["PPrice"]);
                    model.PNum = HJConvert.ToInt32(dr["PNum"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 修改购物车商品数量
        /// </summary>
        /// <param name="Uid"></param>
        /// <param name="Pid"></param>
        /// <param name="PNum"></param>
        /// <returns></returns>
        public int ModCart(int Uid, int Pid, int PNum, decimal PPrice)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ETogoShoppingCart set ");
            strSql.Append("PNum=@PNum");
            strSql.Append(" where  dataid=@PId");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@PId", SqlDbType.Int,4),
				new SqlParameter("@PNum", SqlDbType.Int,4),
            };
            parameters[0].Value = Pid;
            parameters[1].Value = PNum;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除购物车中的商品
        /// </summary>
        /// <param name="Uid"></param>
        /// <param name="Pid"></param>
        /// <returns></returns>
        public int DeleteCart(int Uid, int Pid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ETogoShoppingCart");
            strSql.Append(" where  dataid=@PId");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@UId", SqlDbType.Int,4),
				new SqlParameter("@PId", SqlDbType.Int,4)
            };
            parameters[0].Value = Uid;
            parameters[1].Value = Pid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取用户购物车信息 按照商家获取
        /// 添加togonum 字段 by jijunjian
        /// </summary>
        /// <param name="Uid"></param>
        /// <returns></returns>
        public IList<Hangjing.Model.ETogoShoppingCart> GetCart(string tempcode)
        {
            //1. 获取购物车中有几个商家的餐品
            IList<Hangjing.Model.ETogoShoppingCart> cart_list = new List<Hangjing.Model.ETogoShoppingCart>();
            int togo_num = 0;
            string strSql1 = "GetShoppingCart";
            SqlParameter[] Para1 = 
	        {
		        new SqlParameter("@Uid",SqlDbType.VarChar,50)
	        };
            Para1[0].Value = tempcode;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, strSql1.ToString(), Para1))
            {
                while (dr.Read())
                {
                    togo_num++;
                    Hangjing.Model.ETogoShoppingCart item = new Hangjing.Model.ETogoShoppingCart();
                    item.TogoName = HJConvert.ToString(dr["togoname"]);
                    item.TogoId = HJConvert.ToInt32(dr["togoid"]);
                    item.ItemList = new List<Hangjing.Model.ETogoShoppingCartInfo>();
                    item.Togoremark = 0; //起送价，配送费都是根据shopdeliver来生成
                    item.sendfree =0;
                    item.ptimes = HJConvert.ToInt32(dr["ptimes"]);
                    item.Lat = HJConvert.ToString(dr["Lat"]);
                    item.Lng = HJConvert.ToString(dr["Lng"]);
                    item.MaxDistance = HJConvert.ToInt32(dr["Inve1"]);

                    cart_list.Add(item);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    for (int j = 0; j < togo_num; j++)
                    {
                        if (cart_list[j].TogoId == HJConvert.ToInt32(dr["togoid"]))
                        {
                            Hangjing.Model.ETogoShoppingCartInfo info = new Hangjing.Model.ETogoShoppingCartInfo();
                            info.DataId = HJConvert.ToInt32(dr["DataId"]);
                            info.PId = HJConvert.ToInt32(dr["pid"]);
                            info.PName = HJConvert.ToString(dr["pname"]);
                            info.PNum = HJConvert.ToInt32(dr["pnum"]);
                            info.PPrice = HJConvert.ToDecimal(dr["pprice"]);
                            info.TogoId = HJConvert.ToInt32(dr["togoid"]);
                            info.TogoName = HJConvert.ToString(dr["togoname"]);
                            info.UId = HJConvert.ToInt32(dr["uid"]);
                            info.Foodcurrentprice = HJConvert.ToDecimal(dr["Currentprice"]);
                            info.Currentprice = HJConvert.ToDecimal(dr["pprice"]);
                            info.Remark = "";
                            info.Picture = HJConvert.ToString(dr["Picture"]);
                            info.addprice = HJConvert.ToDecimal(dr["addprice"]);
                            info.owername = HJConvert.ToDecimal(dr["owername"]);

                            info.sname = HJConvert.ToString(dr["sname"]);
                            info.sid = HJConvert.ToInt32(dr["sid"]);
                            info.material = HJConvert.ToString(dr["material"]);


                            cart_list[j].ItemList.Add(info);
                        }
                    }
                }
            }

            return cart_list;
        }

        /// <summary>
        /// 提交订单时删除一个商家
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="togoid"></param>
        /// <returns></returns>
        public int DeleteBytogo(string tempcode, int togoid)
        {
            string sql = "delete from ETogoShoppingCart where tempcode=@uid and togoid = @togoid";
            SqlParameter[] parameters = 
            {
				new SqlParameter("@uid", SqlDbType.VarChar,50),
				new SqlParameter("@togoid", SqlDbType.Int,4),
            };
            parameters[0].Value = tempcode;
            parameters[1].Value = togoid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, parameters);
        }

        /// <summary>
        /// 批量删除记录 根据用户ID删除
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        /// zjf@ihangjing.com 2010-03-23
        public int DelAllCartItem(string tempcode)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ETogoShoppingCart where tempCode = '" + tempcode + "'");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), null);
        }
    }
}
