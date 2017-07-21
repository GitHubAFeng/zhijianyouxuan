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
    /// 跑腿配送费
    /// </summary>
    public partial class expressFeeConfig
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(expressFeeConfigInfo model)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@cityid", SqlDbType.Int,4) ,
                new SqlParameter("@basedistance", SqlDbType.Int,4) ,
                new SqlParameter("@basedistanceprice", SqlDbType.Float,8) ,
                new SqlParameter("@seconddistance", SqlDbType.Int,4) ,
                new SqlParameter("@seconddistancePerPrice", SqlDbType.Float,8) ,
                new SqlParameter("@thirdDistancePerPrice", SqlDbType.Float,8) ,
                new SqlParameter("@starttime1", SqlDbType.DateTime) ,
                new SqlParameter("@endtime1", SqlDbType.VarChar,256) ,
                new SqlParameter("@starttime2", SqlDbType.DateTime) ,
                new SqlParameter("@endtime2", SqlDbType.VarChar,256) ,
                new SqlParameter("@reveint1", SqlDbType.Int,4) ,
                new SqlParameter("@reveint2", SqlDbType.Int,4) ,
                new SqlParameter("@reveint3", SqlDbType.Int,4) ,
                new SqlParameter("@reveint4", SqlDbType.Int,4) ,
                new SqlParameter("@revevar1", SqlDbType.VarChar,256) ,
                new SqlParameter("@revevar2", SqlDbType.VarChar,256) ,
                new SqlParameter("@revevar3", SqlDbType.VarChar,256) ,
                new SqlParameter("@revefloat1", SqlDbType.Float,8) ,
                new SqlParameter("@revefloat2", SqlDbType.Float,8) ,
                new SqlParameter("@revedate1", SqlDbType.DateTime) ,
                new SqlParameter("@revedate2", SqlDbType.DateTime)

            };

            parameters[0].Value = model.cityid;
            parameters[1].Value = model.basedistance;
            parameters[2].Value = model.basedistanceprice;
            parameters[3].Value = model.seconddistance;
            parameters[4].Value = model.seconddistancePerPrice;
            parameters[5].Value = model.thirdDistancePerPrice;
            parameters[6].Value = model.starttime1;
            parameters[7].Value = model.endtime1;
            parameters[8].Value = model.starttime2;
            parameters[9].Value = model.endtime2;
            parameters[10].Value = model.reveint1;
            parameters[11].Value = model.reveint2;
            parameters[12].Value = model.reveint3;
            parameters[13].Value = model.reveint4;
            parameters[14].Value = model.revevar1;
            parameters[15].Value = model.revevar2;
            parameters[16].Value = model.revevar3;
            parameters[17].Value = model.revefloat1;
            parameters[18].Value = model.revefloat2;
            parameters[19].Value = model.revedate1;
            parameters[20].Value = model.revedate2;

            return HJConvert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "expressFeeConfig_ADD", parameters));
        }


        /// <summary>
        /// 根据城市编号获取信息
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>fid</param>
        /// <returns>expressFeeConfigInfo</returns>
        public expressFeeConfigInfo GetModel(int cityid)
        {
            string sql = "select * from expressFeeConfig where  cityid = @fid";
            SqlParameter parameter = new SqlParameter("@fid", SqlDbType.Int, 4);
            parameter.Value = cityid;
            expressFeeConfigInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new expressFeeConfigInfo();
                    model.fid = HJConvert.ToInt32(dr["fid"]);
                    model.cityid = HJConvert.ToInt32(dr["cityid"]);
                    model.basedistance = HJConvert.ToInt32(dr["basedistance"]);
                    model.basedistanceprice = HJConvert.ToDecimal(dr["basedistanceprice"]);
                    model.seconddistance = HJConvert.ToInt32(dr["seconddistance"]);
                    model.seconddistancePerPrice = HJConvert.ToDecimal(dr["seconddistancePerPrice"]);
                    model.thirdDistancePerPrice = HJConvert.ToDecimal(dr["thirdDistancePerPrice"]);
                    model.starttime1 = HJConvert.ToDateTime(dr["starttime1"]);
                    model.endtime1 = HJConvert.ToString(dr["endtime1"]);
                    model.starttime2 = HJConvert.ToDateTime(dr["starttime2"]);
                    model.endtime2 = HJConvert.ToString(dr["endtime2"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.reveint3 = HJConvert.ToInt32(dr["reveint3"]);
                    model.reveint4 = HJConvert.ToInt32(dr["reveint4"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    model.revevar3 = HJConvert.ToString(dr["revevar3"]);
                    model.revefloat1 = HJConvert.ToDecimal(dr["revefloat1"]);
                    model.revefloat2 = HJConvert.ToDecimal(dr["revefloat2"]);
                    model.revedate1 = HJConvert.ToDateTime(dr["revedate1"]);
                    model.revedate2 = HJConvert.ToDateTime(dr["revedate2"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "expressFeeConfig"), new SqlParameter("@strWhere", strWhere));
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
        public IList<expressFeeConfigInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<expressFeeConfigInfo> infos = new List<expressFeeConfigInfo>();
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
            parameters[0].Value = "expressFeeConfig";
            parameters[1].Value = "fid,cityid,basedistance,basedistanceprice,seconddistance,seconddistancePerPrice,thirdDistancePerPrice,starttime1,endtime1,starttime2,endtime2,reveint1,reveint2,reveint3,reveint4,revevar1,revevar2,revevar3,revefloat1,revefloat2,revedate1,revedate2";
            parameters[2].Value = "fid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    expressFeeConfigInfo model = new expressFeeConfigInfo();
                    model.fid = HJConvert.ToInt32(dr["fid"]);
                    model.cityid = HJConvert.ToInt32(dr["cityid"]);
                    model.basedistance = HJConvert.ToInt32(dr["basedistance"]);
                    model.basedistanceprice = HJConvert.ToDecimal(dr["basedistanceprice"]);
                    model.seconddistance = HJConvert.ToInt32(dr["seconddistance"]);
                    model.seconddistancePerPrice = HJConvert.ToDecimal(dr["seconddistancePerPrice"]);
                    model.thirdDistancePerPrice = HJConvert.ToDecimal(dr["thirdDistancePerPrice"]);
                    model.starttime1 = HJConvert.ToDateTime(dr["starttime1"]);
                    model.endtime1 = HJConvert.ToString(dr["endtime1"]);
                    model.starttime2 = HJConvert.ToDateTime(dr["starttime2"]);
                    model.endtime2 = HJConvert.ToString(dr["endtime2"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.reveint3 = HJConvert.ToInt32(dr["reveint3"]);
                    model.reveint4 = HJConvert.ToInt32(dr["reveint4"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    model.revevar3 = HJConvert.ToString(dr["revevar3"]);
                    model.revefloat1 = HJConvert.ToDecimal(dr["revefloat1"]);
                    model.revefloat2 = HJConvert.ToDecimal(dr["revefloat2"]);
                    model.revedate1 = HJConvert.ToDateTime(dr["revedate1"]);
                    model.revedate2 = HJConvert.ToDateTime(dr["revedate2"]);
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
        public int DelexpressFeeConfig(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from expressFeeConfig where fid=@fid");
            SqlParameter[] Para =
                {
                new SqlParameter("@fid",SqlDbType.Int)
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
            str.Append("delete from expressFeeConfig where fid in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }




    }
}

