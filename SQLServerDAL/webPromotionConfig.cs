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
    /// 系统促销配置
    /// </summary>
    public partial class webPromotionConfig
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(webPromotionConfigInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into webPromotionConfig(");
            strSql.Append("shopid,startdate,enddate,starttime,endtime,ptype,isopen,freeSendFee,overmoney,minusmoney,reveint1,reveint2,revevar1,revevar2,revefloat1,revefloat2");
            strSql.Append(") values (");
            strSql.Append("@shopid,@startdate,@enddate,@starttime,@endtime,@ptype,@isopen,@freeSendFee,@overmoney,@minusmoney,@reveint1,@reveint2,@revevar1,@revevar2,@revefloat1,@revefloat2");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters =
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

            parameters[0].Value = model.shopid;
            parameters[1].Value = model.startdate;
            parameters[2].Value = model.enddate;
            parameters[3].Value = model.starttime;
            parameters[4].Value = model.endtime;
            parameters[5].Value = model.ptype;
            parameters[6].Value = model.isopen;
            parameters[7].Value = model.freeSendFee;
            parameters[8].Value = model.overmoney;
            parameters[9].Value = model.minusmoney;
            parameters[10].Value = model.reveint1;
            parameters[11].Value = model.reveint2;
            parameters[12].Value = model.revevar1;
            parameters[13].Value = model.revevar2;
            parameters[14].Value = model.revefloat1;
            parameters[15].Value = model.revefloat2;
            return HJConvert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(webPromotionConfigInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update webPromotionConfig set ");

            strSql.Append(" shopid = @shopid , ");
            strSql.Append(" startdate = @startdate , ");
            strSql.Append(" enddate = @enddate , ");
            strSql.Append(" starttime = @starttime , ");
            strSql.Append(" endtime = @endtime , ");
            strSql.Append(" ptype = @ptype , ");
            strSql.Append(" isopen = @isopen , ");
            strSql.Append(" freeSendFee = @freeSendFee , ");
            strSql.Append(" overmoney = @overmoney , ");
            strSql.Append(" minusmoney = @minusmoney , ");
            strSql.Append(" reveint1 = @reveint1 , ");
            strSql.Append(" reveint2 = @reveint2 , ");
            strSql.Append(" revevar1 = @revevar1 , ");
            strSql.Append(" revevar2 = @revevar2 , ");
            strSql.Append(" revefloat1 = @revefloat1 , ");
            strSql.Append(" revefloat2 = @revefloat2  ");
            strSql.Append(" where pId=@pId ");

            SqlParameter[] parameters =
            {
                new SqlParameter("@pId", SqlDbType.Int,4) ,
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

            parameters[0].Value = model.pId;
            parameters[1].Value = model.shopid;
            parameters[2].Value = model.startdate;
            parameters[3].Value = model.enddate;
            parameters[4].Value = model.starttime;
            parameters[5].Value = model.endtime;
            parameters[6].Value = model.ptype;
            parameters[7].Value = model.isopen;
            parameters[8].Value = model.freeSendFee;
            parameters[9].Value = model.overmoney;
            parameters[10].Value = model.minusmoney;
            parameters[11].Value = model.reveint1;
            parameters[12].Value = model.reveint2;
            parameters[13].Value = model.revevar1;
            parameters[14].Value = model.revevar2;
            parameters[15].Value = model.revefloat1;
            parameters[16].Value = model.revefloat2;
            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>pId</param>
        /// <returns>webPromotionConfigInfo</returns>
        public webPromotionConfigInfo GetModel(int pId)
        {
            string sql = "select pId,shopid,startdate,enddate,starttime,endtime,ptype,isopen,freeSendFee,overmoney,minusmoney,reveint1,reveint2,revevar1,revevar2,revefloat1,revefloat2 from webPromotionConfig where  pId = @pId";
            SqlParameter parameter = new SqlParameter("@pId", SqlDbType.Int, 4);
            parameter.Value = pId;
            webPromotionConfigInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new webPromotionConfigInfo();
                    model.pId = HJConvert.ToInt32(dr["pId"]);
                    model.shopid = HJConvert.ToInt32(dr["shopid"]);
                    model.startdate = HJConvert.ToDateTime(dr["startdate"]);
                    model.enddate = HJConvert.ToDateTime(dr["enddate"]);
                    model.starttime = HJConvert.ToDateTime(dr["starttime"]);
                    model.endtime = HJConvert.ToDateTime(dr["endtime"]);
                    model.ptype = HJConvert.ToInt32(dr["ptype"]);
                    model.isopen = HJConvert.ToInt32(dr["isopen"]);
                    model.freeSendFee = HJConvert.ToDecimal(dr["freeSendFee"]);
                    model.overmoney = HJConvert.ToInt32(dr["overmoney"]);
                    model.minusmoney = HJConvert.ToInt32(dr["minusmoney"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    model.revefloat1 = HJConvert.ToDecimal(dr["revefloat1"]);
                    model.revefloat2 = HJConvert.ToDecimal(dr["revefloat2"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "webPromotionConfig"), new SqlParameter("@strWhere", strWhere));
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<webPromotionConfigInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType,int iswebtype)
        {
            IList<webPromotionConfigInfo> infos = new List<webPromotionConfigInfo>();
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
            parameters[0].Value = "webPromotionConfig";
            parameters[1].Value = "*,(select name from points where unid = webPromotionConfig.shopid ) as title ";
            parameters[2].Value = "pId";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    webPromotionConfigInfo model = new webPromotionConfigInfo();
                    model.pId = HJConvert.ToInt32(dr["pId"]);
                    model.shopid = HJConvert.ToInt32(dr["shopid"]);
                    model.startdate = HJConvert.ToDateTime(dr["startdate"]);
                    model.enddate = HJConvert.ToDateTime(dr["enddate"]);
                    model.starttime = HJConvert.ToDateTime(dr["starttime"]);
                    model.endtime = HJConvert.ToDateTime(dr["endtime"]);
                    model.ptype = HJConvert.ToInt32(dr["ptype"]);
                    model.isopen = HJConvert.ToInt32(dr["isopen"]);
                    model.freeSendFee = HJConvert.ToDecimal(dr["freeSendFee"]);
                    model.overmoney = HJConvert.ToInt32(dr["overmoney"]);
                    model.minusmoney = HJConvert.ToInt32(dr["minusmoney"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    model.revefloat1 = HJConvert.ToDecimal(dr["revefloat1"]);
                    model.revefloat2 = HJConvert.ToDecimal(dr["revefloat2"]);
                    model.title = HJConvert.ToString(dr["title"]);
                    if (model.shopid == 0)
                    {
                        model.title = "平台";
                    }
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
        public int DelwebPromotionConfig(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from webPromotionConfig where pId=@pId");
            SqlParameter[] Para =
                {
                new SqlParameter("@pId",SqlDbType.Int)
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
            str.Append("delete from webPromotionConfig where pId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }


    }
}

