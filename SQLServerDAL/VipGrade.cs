/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * Function : 用户等级
 * Created by jijunjian at 2010-10-22 0:09:22.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;

using Hangjing.DBUtility;//请先添加引用
using Hangjing.Model;


namespace Hangjing.SQLServerDAL
{
    public class VipGrade
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VipGradeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into VipGrade(");
            strSql.Append("GradeName,MinPoint,MaxPoint,vRat,GaiPoint,Reve1,Reve2)");
            strSql.Append(" values (");
            strSql.Append("@GradeName,@MinPoint,@MaxPoint,@vRat,@GaiPoint,@Reve1,@Reve2)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@GradeName", SqlDbType.VarChar,50),
				new SqlParameter("@MinPoint", SqlDbType.Int,4),
				new SqlParameter("@MaxPoint", SqlDbType.Int,4),
				new SqlParameter("@vRat", SqlDbType.Decimal,5),
				new SqlParameter("@GaiPoint", SqlDbType.Int,4),
				new SqlParameter("@Reve1", SqlDbType.Int,4),
				new SqlParameter("@Reve2", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.GradeName;
            parameters[1].Value = model.MinPoint;
            parameters[2].Value = model.MaxPoint;
            parameters[3].Value = model.vRat;
            parameters[4].Value = model.GaiPoint;
            parameters[5].Value = model.Reve1;
            parameters[6].Value = model.Reve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(VipGradeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update VipGrade set ");
            strSql.Append("GradeName=@GradeName,");
            strSql.Append("MinPoint=@MinPoint,");
            strSql.Append("MaxPoint=@MaxPoint,");
            strSql.Append("vRat=@vRat,");
            strSql.Append("GaiPoint=@GaiPoint,");
            strSql.Append("Reve1=@Reve1,");
            strSql.Append("Reve2=@Reve2");
            strSql.Append(" where DataID=@DataID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataID", SqlDbType.Int,4),
				new SqlParameter("@GradeName", SqlDbType.VarChar,50),
				new SqlParameter("@MinPoint", SqlDbType.Int,4),
				new SqlParameter("@MaxPoint", SqlDbType.Int,4),
				new SqlParameter("@vRat", SqlDbType.Decimal,5),
				new SqlParameter("@GaiPoint", SqlDbType.Int,4),
				new SqlParameter("@Reve1", SqlDbType.Int,4),
				new SqlParameter("@Reve2", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.DataID;
            parameters[1].Value = model.GradeName;
            parameters[2].Value = model.MinPoint;
            parameters[3].Value = model.MaxPoint;
            parameters[4].Value = model.vRat;
            parameters[5].Value = model.GaiPoint;
            parameters[6].Value = model.Reve1;
            parameters[7].Value = model.Reve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }



        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdateName(VipGradeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update VipGrade set ");
            strSql.Append("GradeName=@GradeName,");
            strSql.Append("MinPoint=@MinPoint,");
            strSql.Append("MaxPoint=@MaxPoint,");
            strSql.Append("GaiPoint=@GaiPoint,");
            strSql.Append("Reve1=@Reve1");
            strSql.Append(" where DataID=@DataID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataID", SqlDbType.Int,4),
				new SqlParameter("@GradeName", SqlDbType.VarChar,50),
				new SqlParameter("@MinPoint", SqlDbType.Int,4),
				new SqlParameter("@MaxPoint", SqlDbType.Int,4),
                new SqlParameter("@GaiPoint", SqlDbType.Int,4),
				new SqlParameter("@Reve1", SqlDbType.Int,4),
            };
            parameters[0].Value = model.DataID;
            parameters[1].Value = model.GradeName;
            parameters[2].Value = model.MinPoint;
            parameters[3].Value = model.MaxPoint;
            parameters[4].Value = model.GaiPoint;
            parameters[5].Value = model.Reve1;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataID</param>
        /// <returns>VipGradeInfo</returns>
        public VipGradeInfo GetModel(int DataID)
        {
            string sql = "select DataID,GradeName,MinPoint,MaxPoint,vRat,GaiPoint,Reve1,Reve2 from VipGrade where dataid = @DataID";
            SqlParameter parameter = new SqlParameter("@DataID", SqlDbType.Int, 4);
            parameter.Value = DataID;
            VipGradeInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new VipGradeInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.GradeName = HJConvert.ToString(dr["GradeName"]);
                    model.MinPoint = HJConvert.ToInt32(dr["MinPoint"]);
                    model.MaxPoint = HJConvert.ToInt32(dr["MaxPoint"]);
                    model.vRat = HJConvert.ToDecimal(dr["vRat"]);
                    model.GaiPoint = HJConvert.ToInt32(dr["GaiPoint"]);
                    model.Reve1 = HJConvert.ToInt32(dr["Reve1"]);
                    model.Reve2 = HJConvert.ToString(dr["Reve2"]);
                }
            }
            return model;
        }


        /// <summary>
        /// 获取所有等级及
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>图书列表</returns>
        public IList<VipGradeInfo> GetAll()
        {
            IList<VipGradeInfo> infos = new List<VipGradeInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "getUserGradeAndTavourat", null))
            {
                while (dr.Read())
                {
                    VipGradeInfo info = new VipGradeInfo();
                    info.DataID = HJConvert.ToInt32(dr["DataID"]);
                    info.GradeName = HJConvert.ToString(dr["GradeName"]);
                    info.MinPoint = HJConvert.ToInt32(dr["MinPoint"]);
                    info.MaxPoint = HJConvert.ToInt32(dr["MaxPoint"]);
                    info.vRat = HJConvert.ToDecimal(dr["vRat"]);
                    info.GaiPoint = HJConvert.ToInt32(dr["GaiPoint"]);
                    info.Reve1 = HJConvert.ToInt32(dr["Reve1"]);
                    info.Reve2 = HJConvert.ToString(dr["Reve2"]);

                    User_Grade_RInfo model = new User_Grade_RInfo();
                    model.pid = HJConvert.ToInt32(dr["pid"]);
                    model.gid = HJConvert.ToInt32(dr["gid"]);
                    model.sendmoneyDiscount = HJConvert.ToDecimal(dr["sendmoneyDiscount"]);
                    model.foodmoneyDiscount = HJConvert.ToDecimal(dr["foodmoneyDiscount"]);
                    model.pointrat = HJConvert.ToDecimal(dr["pointrat"]);
                    model.sendprior = HJConvert.ToInt32(dr["sendprior"]);
                    model.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    model.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    model.ReveFlat = HJConvert.ToDecimal(dr["ReveFlat"]);

                    info.favourable = model;

                    infos.Add(info);
                }
            }
            return infos;
        }



        /// <summary>
        /// 获得总记录数
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int GetCount(string strWhere)
        {
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "VipGrade"), new SqlParameter("@strWhere", strWhere));
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
        public IList<VipGradeInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<VipGradeInfo> infos = new List<VipGradeInfo>();
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
            parameters[0].Value = "VipGrade";
            parameters[1].Value = "DataID,GradeName,MinPoint,MaxPoint,vRat,GaiPoint,Reve1,Reve2";
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
                    VipGradeInfo info = new VipGradeInfo();
                    info.DataID = HJConvert.ToInt32(dr["DataID"]);
                    info.GradeName = HJConvert.ToString(dr["GradeName"]);
                    info.MinPoint = HJConvert.ToInt32(dr["MinPoint"]);
                    info.MaxPoint = HJConvert.ToInt32(dr["MaxPoint"]);
                    info.vRat = HJConvert.ToDecimal(dr["vRat"]);
                    info.GaiPoint = HJConvert.ToInt32(dr["GaiPoint"]);
                    info.Reve1 = HJConvert.ToInt32(dr["Reve1"]);
                    info.Reve2 = HJConvert.ToString(dr["Reve2"]);
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
        public int DelVipGrade(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from VipGrade where DataID=@DataID");
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
            str.Append("delete from VipGrade where DataID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 通过分类获取筹等级
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public string GetGradeName(int point)
        {
            string rs = "";
            string sql = "select GradeName from VipGrade where MinPoint <= " + point + " and MaxPoint >= " + point;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    rs = HJConvert.ToString(dr["gradename"]);
                }
            }

            return rs;
        }
    }
}
