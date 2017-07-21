// TogoPrinter.cs:点餐商家数据访问层
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com 更新
// 2010-07-08
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;


using Hangjing.DBUtility;
using Hangjing.Model;

namespace Hangjing.SQLServerDAL
{
    public class TogoPrinter
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TogoPrinterInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ETogoPrinter(");
            strSql.Append("TogoId,TogoNum,PrinterSn,PrintPage,PrintTop,PrintFoot,IsUpdate,LastLoginDate)");
            strSql.Append(" values (");
            strSql.Append("@TogoId,@TogoNum,@PrinterSn,@PrintPage,@PrintTop,@PrintFoot,@IsUpdate,@LastLoginDate)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@TogoId", SqlDbType.Int,4),
				new SqlParameter("@TogoNum", SqlDbType.VarChar,20),
				new SqlParameter("@PrinterSn", SqlDbType.VarChar,20),
				new SqlParameter("@PrintPage", SqlDbType.Int,4),
				new SqlParameter("@PrintTop", SqlDbType.VarChar,256),
				new SqlParameter("@PrintFoot", SqlDbType.VarChar,256),
				new SqlParameter("@IsUpdate", SqlDbType.Int,4),
				new SqlParameter("@LastLoginDate", SqlDbType.DateTime)
            };
            parameters[0].Value = model.TogoId;
            parameters[1].Value = model.TogoNum;
            parameters[2].Value = model.PrinterSn;
            parameters[3].Value = model.PrintPage;
            parameters[4].Value = model.PrintTop;
            parameters[5].Value = model.PrintFoot;
            parameters[6].Value = model.IsUpdate;
            parameters[7].Value = model.LastLoginDate;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(TogoPrinterInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ETogoPrinter set ");
            strSql.Append("TogoId=@TogoId,");
            strSql.Append("TogoNum=@TogoNum,");
            strSql.Append("PrinterSn=@PrinterSn,");
            strSql.Append("PrintPage=@PrintPage,");
            strSql.Append("PrintTop=@PrintTop,");
            strSql.Append("PrintFoot=@PrintFoot,");
            strSql.Append("IsUpdate=@IsUpdate ");
            strSql.Append(" where DataId=@DataId ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataId", SqlDbType.Int,4),
				new SqlParameter("@TogoId", SqlDbType.Int,4),
				new SqlParameter("@TogoNum", SqlDbType.VarChar,20),
				new SqlParameter("@PrinterSn", SqlDbType.VarChar,20),
				new SqlParameter("@PrintPage", SqlDbType.Int,4),
				new SqlParameter("@PrintTop", SqlDbType.VarChar,256),
                new SqlParameter("@PrintFoot", SqlDbType.VarChar,256),
                new SqlParameter("@IsUpdate", SqlDbType.Int,4)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.TogoId;
            parameters[2].Value = model.TogoNum;
            parameters[3].Value = model.PrinterSn;
            parameters[4].Value = model.PrintPage;
            parameters[5].Value = model.PrintTop;
            parameters[6].Value = model.PrintFoot;
            parameters[7].Value = model.IsUpdate;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>ETogoPrinterInfo</returns>
        public TogoPrinterInfo GetModel(int DataId)
        {
            string sql = "select DataId,TogoId,TogoNum,PrinterSn,PrintPage,PrintTop,PrintFoot,IsUpdate,LastLoginDate from ETogoPrinter  where TogoId=@DataId";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            TogoPrinterInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new TogoPrinterInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    model.TogoNum = HJConvert.ToString(dr["TogoNum"]);
                    model.PrinterSn = HJConvert.ToString(dr["PrinterSn"]);
                    model.PrintPage = HJConvert.ToInt32(dr["PrintPage"]);
                    model.PrintTop = HJConvert.ToString(dr["PrintTop"]);
                    model.PrintFoot = HJConvert.ToString(dr["PrintFoot"]);
                    model.IsUpdate = HJConvert.ToInt32(dr["IsUpdate"]);
                    model.LastLoginDate = HJConvert.ToDateTime(dr["LastLoginDate"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 获取记录所有字段 根据商家的编号获取
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>ETogoPrinterInfo</returns>
        public TogoPrinterInfo GetModelByTogoId(int TogoId)
        {
            string sql = "select DataId,TogoId,TogoNum,PrinterSn,PrintPage,PrintTop,PrintFoot,IsUpdate,LastLoginDate from ETogoPrinter  where TogoId=@TogoId";
            SqlParameter parameter = new SqlParameter("@TogoId", SqlDbType.Int, 4);
            parameter.Value = TogoId;
            TogoPrinterInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new TogoPrinterInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    model.TogoNum = HJConvert.ToString(dr["TogoNum"]);
                    model.PrinterSn = HJConvert.ToString(dr["PrinterSn"]);
                    model.PrintPage = HJConvert.ToInt32(dr["PrintPage"]);
                    model.PrintTop = HJConvert.ToString(dr["PrintTop"]);
                    model.PrintFoot = HJConvert.ToString(dr["PrintFoot"]);
                    model.IsUpdate = HJConvert.ToInt32(dr["IsUpdate"]);
                    model.LastLoginDate = HJConvert.ToDateTime(dr["LastLoginDate"]);
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
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "ETogoPrinter"), new SqlParameter("@strWhere", strWhere)));
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
        public IList<TogoPrinterInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<TogoPrinterInfo> infos = new List<TogoPrinterInfo>();
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
            parameters[0].Value = "ETogoPrinter";
            parameters[1].Value = "DataId,TogoId,TogoNum,PrinterSn,PrintPage,PrintTop,PrintFoot,IsUpdate,LastLoginDate,(select name from Points where Points.unid=ETogoPrinter.togoid) togoname,(select CommPerson from Points where unid=ETogoPrinter.togoid) linkman,(select Comm from Points where unid=ETogoPrinter.togoid) linktel,(select address from Points where unid=ETogoPrinter.togoid) linkaddress";//, (select PrinterNum from EPrinterSecret where EPrinterSecret.PrinterNum)
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
                    TogoPrinterInfo info = new TogoPrinterInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    info.TogoNum = HJConvert.ToString(dr["TogoNum"]);
                    info.PrinterSn = HJConvert.ToString(dr["PrinterSn"]);
                    info.PrintPage = HJConvert.ToInt32(dr["PrintPage"]);
                    info.PrintTop = HJConvert.ToString(dr["PrintTop"]);
                    info.PrintFoot = HJConvert.ToString(dr["PrintFoot"]);
                    info.IsUpdate = HJConvert.ToInt32(dr["IsUpdate"]);
                    info.LastLoginDate = HJConvert.ToDateTime(dr["LastLoginDate"]);
                    info.TogoName = HJConvert.ToString(dr["togoname"]);
                    info.LinkMan = HJConvert.ToString(dr["LinkMan"]);
                    info.LinkTel = HJConvert.ToString(dr["LinkTel"]);
                    info.LinkAddress = HJConvert.ToString(dr["LinkAddress"]);
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
        public int DelTogoPrinter(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ETogoPrinter where DataId=@DataId");
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
            str.Append("delete from ETogoPrinter where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /*以下代码需要根据新的表结构重新编写 zjf@ihangjing.com */

        /// <summary>
        /// 根据打印机序列号获取打印机状态
        /// </summary>
        /// <param name="TogoSN"></param>
        /// <returns></returns>
        public TogoPrinterInfo IsPrintUse(string TogoNO)
        {
            TogoPrinterInfo model = null;
            SqlParameter[] Para = 
            {
                new SqlParameter("@TogoNO",SqlDbType.VarChar,20)
            };
            Para[0].Value = TogoNO;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "Togo_Printer_CheckIsUse", Para))
            {
                if (dr.Read())
                {
                    model = new TogoPrinterInfo();
                    //model.DataId = HJConvert.ToInt32(dr["dataid"]);
                    //model.TogoId = HJConvert.ToString(dr["togono"]);
                    //model.PrinterSn = HJConvert.ToString(dr["togosn"]);
                    //model.IsUse = HJConvert.ToInt32(dr["isuse"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 根据打印机序列号一获取一条打印机信息
        /// </summary>
        /// <param name="TogoNO"></param>
        /// <returns></returns>
        public TogoPrinterInfo GetModelByTogoNO(string TogoNO)
        {
            SqlParameter[] Para = 
            {
                new SqlParameter("@TogoNO",SqlDbType.VarChar,20)
            };
            Para[0].Value = TogoNO;

            TogoPrinterInfo model = null;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "Togo_Printer_GetModelByTogoNO", Para))
            {
                if (dr.Read())
                {
                    model = new TogoPrinterInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataID"]);
                    //model.TogoNO = HJConvert.ToString(dr["TogoNO"]);
                    //model.TogoSN = HJConvert.ToString(dr["TogoSN"]);
                    //model.Key = HJConvert.ToString(dr["Key"]);
                    //model.IsUse = HJConvert.ToInt32(dr["IsUse"]);
                    //model.TogoName = HJConvert.ToString(dr["TogoName"]);
                    //model.FirstNum = HJConvert.ToString(dr["FirstNum"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 根据序列号二更改一台打印机状态
        /// </summary>
        /// <param name="TogoSN"></param>
        /// <returns></returns>
        public int ChangeStateByTogoSN(string TogoSN, int State)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update Togo_Printer set IsUse=@State where TogoSN=@TogoSN");

            SqlParameter[] Para = 
            {
                new SqlParameter("@TogoSN",SqlDbType.VarChar,20),
                new SqlParameter("@State",SqlDbType.Int)
            };
            Para[0].Value = TogoSN;
            Para[1].Value = State;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 根据打印机背部编号获取商家密钥
        /// </summary>
        /// <param name="togoNo"></param>
        /// <returns></returns>
        public string GetPassKeyWithSN(string togoNo)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select [Key] from Togo_Printer where TogoNo=@TogoNo");

            SqlParameter[] Para = 
            {
                new SqlParameter("@TogoNo",SqlDbType.VarChar,20)
            };
            Para[0].Value = togoNo;

            return Convert.ToString(SQLHelper.ExecuteScalar(CommandType.Text, str.ToString(), Para));
        }

        /// <summary>
        /// 根据商家编号获取商家密钥
        /// </summary>
        /// <param name="togoNum"></param>
        /// <returns></returns>
        public string GetPasskeyWithCustID(string togoNum)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select [Key] from Togo_Printer where TogoSN=(select TogoSN from ETogo where TogoNum=@TogoNum)");

            SqlParameter[] Para = 
            {
                new SqlParameter("@TogoNum",SqlDbType.VarChar,20)
            };
            Para[0].Value = togoNum;

            return HJConvert.ToString(SQLHelper.ExecuteScalar(CommandType.Text, str.ToString(), Para));
        }

        /// <summary>
        /// 得到最后firstnum
        /// </summary>
        /// <returns></returns>
        public string GetMaxFirstNum()
        {
            string sql = " select top 1 * from Togo_Printer order by dataid desc";
            string firstnum = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (dr.Read())
                {
                    firstnum = HJConvert.ToString(dr["firstnum"]);
                }
            }
            return firstnum;
        }

        /// <summary>
        /// 根据订单编号获取对应商家的打印机编号,没有返回空
        /// </summary>
        /// <returns></returns>
        public string GetPrinterSNByOrderid(string orderid)
        {
            string sql = " SELECT TOP 1 PrinterSn FROM dbo.ETogoPrinter AS a  LEFT JOIN dbo.Custorder AS b ON a.TogoId = b.TogoId WHERE b.orderid = @orderid";

            SqlParameter[] Para = 
            {
                new SqlParameter("@orderid",SqlDbType.VarChar,50)
            };
            Para[0].Value = orderid;

            string PrinterSn = "";
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, Para))
            {
                if (dr.Read())
                {
                    PrinterSn = HJConvert.ToString(dr["PrinterSn"]);
                }
            }
            return PrinterSn;
        }

        /// <summary>
        /// 返回所有打印机商家及商家信息
        /// </summary>
        /// <returns></returns>
        public IList<PointsInfo> GetListWithSttus()
        {
            IList<PointsInfo> infos = new List<PointsInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "TogoPrinter_getAllShop", null))
            {
                while (dr.Read())
                {
                    PointsInfo info = new PointsInfo();
                    info.isbisness = HJConvert.ToInt32(dr["havenew"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.Name = HJConvert.ToString(dr["Name"]);
                    info.Comm = HJConvert.ToString(dr["Comm"]);
                    info.Picture = HJConvert.ToString(dr["Printersn"]);
                    infos.Add(info);
                }
            }
            return infos;
        }
    }
}
