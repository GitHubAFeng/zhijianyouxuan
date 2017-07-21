using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;


using Hangjing.DBUtility;
using Hangjing.Model;


// ETogo.cs:点餐商家数据访问层
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com 更新
// 2010-07-08

namespace Hangjing.SQLServerDAL
{
    public class PrinterSecret
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PrinterSecretInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into EPrinterSecret(");
            strSql.Append("PrinterNum,PrinterSn,PrinterKey,FirstSn,IsUse)");
            strSql.Append(" values (");
            strSql.Append("@PrinterNum,@PrinterSn,@PrinterKey,@FirstSn,@IsUse)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@PrinterNum", SqlDbType.VarChar,20),
				new SqlParameter("@PrinterSn", SqlDbType.VarChar,20),
				new SqlParameter("@PrinterKey", SqlDbType.VarChar,20),
				new SqlParameter("@FirstSn", SqlDbType.VarChar,20),
				new SqlParameter("@IsUse", SqlDbType.Int,4)
            };
            parameters[0].Value = model.PrinterNum;
            parameters[1].Value = model.PrinterSn;
            parameters[2].Value = model.PrinterKey;
            parameters[3].Value = model.FirstSn;
            parameters[4].Value = model.IsUse;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(PrinterSecretInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update EPrinterSecret set ");
            strSql.Append("PrinterNum=@PrinterNum,");
            strSql.Append("PrinterSn=@PrinterSn,");
            strSql.Append("PrinterKey=@PrinterKey,");
            strSql.Append("FirstSn=@FirstSn,");
            strSql.Append("IsUse=@IsUse");
            strSql.Append(" where DataId=@DataId ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataId", SqlDbType.Int,4),
				new SqlParameter("@PrinterNum", SqlDbType.VarChar,20),
				new SqlParameter("@PrinterSn", SqlDbType.VarChar,20),
				new SqlParameter("@PrinterKey", SqlDbType.VarChar,20),
				new SqlParameter("@FirstSn", SqlDbType.VarChar,20),
				new SqlParameter("@IsUse", SqlDbType.Int,4)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.PrinterNum;
            parameters[2].Value = model.PrinterSn;
            parameters[3].Value = model.PrinterKey;
            parameters[4].Value = model.FirstSn;
            parameters[5].Value = model.IsUse;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>EPrinterSecretInfo</returns>
        public PrinterSecretInfo GetModel(int DataId)
        {
            string sql = "select DataId,PrinterNum,PrinterSn,PrinterKey,FirstSn,IsUse from EPrinterSecret where DataId=@DataId";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            PrinterSecretInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new PrinterSecretInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.PrinterNum = HJConvert.ToString(dr["PrinterNum"]);
                    model.PrinterSn = HJConvert.ToString(dr["PrinterSn"]);
                    model.PrinterKey = HJConvert.ToString(dr["PrinterKey"]);
                    model.FirstSn = HJConvert.ToString(dr["FirstSn"]);
                    model.IsUse = HJConvert.ToInt32(dr["IsUse"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 根据打印机编号获取打印机序列号SN
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>EPrinterSecretInfo</returns>
        public PrinterSecretInfo GetModel(string PrinterNum)
        {
            string sql = "select DataId,PrinterNum,PrinterSn,PrinterKey,FirstSn,IsUse from EPrinterSecret where PrinterNum=@PrinterNum";
            SqlParameter parameter = new SqlParameter("@PrinterNum", SqlDbType.VarChar, 128);
            parameter.Value = PrinterNum;
            PrinterSecretInfo model = null;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new PrinterSecretInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.PrinterNum = HJConvert.ToString(dr["PrinterNum"]);
                    model.PrinterSn = HJConvert.ToString(dr["PrinterSn"]);
                    model.PrinterKey = HJConvert.ToString(dr["PrinterKey"]);
                    model.IsUse = HJConvert.ToInt32(dr["IsUse"]);
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
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "EPrinterSecret"), new SqlParameter("@strWhere", strWhere)));
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
        public IList<PrinterSecretInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<PrinterSecretInfo> infos = new List<PrinterSecretInfo>();
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
            parameters[0].Value = "EPrinterSecret";
            parameters[1].Value = "DataId,PrinterNum,PrinterSn,PrinterKey,FirstSn,IsUse";
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
                    PrinterSecretInfo info = new PrinterSecretInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.PrinterNum = HJConvert.ToString(dr["PrinterNum"]);
                    info.PrinterSn = HJConvert.ToString(dr["PrinterSn"]);
                    info.PrinterKey = HJConvert.ToString(dr["PrinterKey"]);
                    info.FirstSn = HJConvert.ToString(dr["FirstSn"]);
                    info.IsUse = HJConvert.ToInt32(dr["IsUse"]);
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
        public int DelPrinterSecret(int Id)
        {
            return DelList(Id.ToString());
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
            str.Append("DELETE FROM dbo.ETogoPrinter WHERE PrinterSn IN (SELECT PrinterNum FROM dbo.EPrinterSecret WHERE DataId IN (" + IdList + ") );");
            str.Append("delete from EPrinterSecret where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 更新一个int字段的值 where是查询语句 需要带where 如：where DataId=1或者 where DataId in (1,2,3)
        /// </summary>
        public int UpdateValue(string param, int intValue, string Where)
        {
            return (int)SQLHelper.UpdateValue("EPrinterSecret", param, intValue, Where);
        }

        /// <summary>
        /// 更新一个string字段的值
        /// where是查询语句 需要带where 如：where DataId=1或者 where DataId in (1,2,3)
        ///</summary>
        public int UpdateValue(string param, string strValue, string Where)
        {
            return (int)SQLHelper.UpdateValue("EPrinterSecret", param, strValue, Where);
        }
    }
}
