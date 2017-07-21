
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
    /// <summary>
    /// 数据访问类ETogo。
    /// </summary>
    public class Togo 
    {
        /// <summary>
        /// togo
        /// </summary>
        /// <param name="togono"></param>
        /// <returns></returns>
        public string GetPassKeyWithSN(string sn1)
        {
            string sql = "select PrinterKey from EPrinterSecret where PrinterNum = @togono";
            SqlParameter parameter = new SqlParameter("@togono", SqlDbType.VarChar, 20);
            parameter.Value = sn1;
            string pkey = "";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                while (dr.Read())
                {
                    pkey = HJConvert.ToString(dr["PrinterKey"]);
                }
            }
            return pkey;
        }

        /// <summary>
        /// 根据打印机编号获取dataid,做为获取订单的编号
        /// </summary>
        /// <param name="sn2"></param>
        /// <returns></returns>
        public TogoPrinterInfo GetModelByTogoSN(string num)
        {
            string sql = "select *  from ETogoPrinter where PrinterSn = @togosn";
            SqlParameter parameter = new SqlParameter("@togosn", SqlDbType.VarChar, 20);
            parameter.Value = num;
            TogoPrinterInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                while (dr.Read())
                {
                    model = new TogoPrinterInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.PrintTop = HJConvert.ToString(dr["PrintTop"]);
                    model.TogoId = HJConvert.ToInt32(dr["togoid"]);
                    model.PrintPage = HJConvert.ToInt32(dr["PrintPage"]);
                    model.PrintFoot = HJConvert.ToString(dr["PrintFoot"]);
                }
            }

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="togonum">商家编号</param>
        /// <returns></returns>
        public string GetPasskeyWithCustID(string togoid)
        {
            string sql = "select b.PrinterKey from ETogoPrinter a left join  EPrinterSecret b on  a.PrinterSn = b.PrinterNum where a.togoid = @togoid";
       
            SqlParameter parameter = new SqlParameter("@togoid", SqlDbType.VarChar, 20);
            parameter.Value = togoid;
            string pkey = "";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                while (dr.Read())
                {
                    pkey = HJConvert.ToString(dr["PrinterKey"]);
                }
            }
            System.Diagnostics.Debug.WriteLine(pkey);
            return pkey;
        }

        /// <summary>
        /// 根据打印机传过来的参数，同步更改商家信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdatePrintInfo(TogoPrinterInfo model)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update ETogoPrinter set PrintTop=@Name,");
            str.Append("PrintPage=@Interlock,");
            str.Append("PrintFoot=@pEnd");
            str.Append(" where togoid=@togoid");

            SqlParameter[] Para = 
            {
                new SqlParameter("@Name",SqlDbType.VarChar,256),
                new SqlParameter("@Interlock",SqlDbType.Int),
                new SqlParameter("@pEnd",SqlDbType.VarChar,400),
                new SqlParameter("@togoid",SqlDbType.VarChar,20)
            };
            Para[0].Value = model.PrintTop;
            Para[1].Value = model.PrintPage;
            Para[2].Value = model.PrintFoot;
            Para[3].Value = model.TogoId+"";

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 更改（商家名，联数，打印结尾是否更改）状态。
        /// </summary>
        /// <param name="TogoNum">商家编号</param>
        /// <param name="isUpdate"></param>
        /// <returns></returns>
        public int UpdateIsUpdate(string togoid, int isUpdate)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update ETogoPrinter set IsUpdate=@IsUpdate where togoid=@togoid");

            SqlParameter[] Para = 
            {
                new SqlParameter("@togoid",SqlDbType.VarChar,20),
                new SqlParameter("@IsUpdate",SqlDbType.Int)
            };
            Para[0].Value = togoid;
            Para[1].Value = isUpdate;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 根据商家编号获取商家名称、联数和打印结尾。（商家管理中心在修改名称联数或者打印结尾时使用）
        /// </summary>
        /// <param name="togoid"></param>
        /// <returns></returns>
        public TogoPrinterInfo GetModelByTogoNum(string togoid)
        {
            string sql = "select * from ETogoPrinter where togoid = @togoid";
            SqlParameter[] Para = 
            {
                new SqlParameter("@togoid",SqlDbType.Int,4)
            };
            Para[0].Value = Convert.ToInt32(togoid); ;

            TogoPrinterInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, Para))
            {
                if (dr.Read())
                {
                    model = new TogoPrinterInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataID"]);
                    model.PrintTop = HJConvert.ToString(dr["etogoNPrintTopame"]);
                    model.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    model.PrintPage = HJConvert.ToInt32(dr["PrintPage"]);
                    model.PrintFoot = HJConvert.ToString(dr["PrintFoot"]);
                    model.IsUpdate = HJConvert.ToInt32(dr["IsUpdate"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 修改商家的在线状态和最后登录时间
        /// </summary>
        /// <param name="togoNum"></param>
        /// <param name="Status"></param>
        /// <param name="_lastlogindate"></param>
        /// <returns></returns>
        public int UpdateState(string togoid, int Status, DateTime Lastlogindate)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" update ETogoPrinter set  LastLoginDate=@LastLoginDate where togoid = @togoid");

            SqlParameter[] Para = 
            {
                new SqlParameter("@togoid",SqlDbType.Int,4),
                new SqlParameter("@LastLoginDate",SqlDbType.DateTime),
            };

            Para[0].Value = togoid;
            Para[1].Value = Lastlogindate;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }
        /// <summary>
        /// 获取所有在线商家
        /// </summary>
        /// <returns></returns>
        public IList<TogoPrinterInfo> GetAllToCheck()
        {
            string sql = "select TogoId , LastLoginDate from ETogoPrinter";
            IList<TogoPrinterInfo> list = new List<TogoPrinterInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    TogoPrinterInfo model = new TogoPrinterInfo();
                    model.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    model.LastLoginDate = HJConvert.ToDateTime(dr["LastLoginDate"]);
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

