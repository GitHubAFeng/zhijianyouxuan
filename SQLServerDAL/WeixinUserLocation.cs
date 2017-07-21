using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;

namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 数据访问类:WeixinUserLocation
    /// </summary>
    public partial class WeixinUserLocation
    {
        /// <summary>
        /// 微信根据openid将经度纬度放入数据库，有则更新，无则添加，确保一个openid对应一个经度和一个纬度
        /// </summary>
        /// <param name="openid">用户openid</param>
        /// <param name="lat">纬度</param>
        /// <param name="lng">经度</param>
        ///此代码由杭景科技代码内部生成器自动生成
        public int Add(string openid, string lat, string lng)
        {
            SqlParameter[] parameters = 
            {
				new SqlParameter("@openid", SqlDbType.VarChar,256),
				new SqlParameter("@lat", SqlDbType.VarChar,50),
				new SqlParameter("@lng", SqlDbType.VarChar,50),
            };
            parameters[0].Value = openid;
            parameters[1].Value = lat;
            parameters[2].Value = lng;
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "WeixinUserLocation_uploadLoaction", parameters));
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.WeixinUserLocationInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeixinUserLocation set ");
            strSql.Append("lat=@lat,");
            strSql.Append("lng=@lng,");
            strSql.Append("addtime=@addtime,");
            strSql.Append("reveint1=@reveint1,");
            strSql.Append("revevar1=@revevar1");
            strSql.Append(" where lid=@lid");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@lat", SqlDbType.VarChar,50),
				new SqlParameter("@lng", SqlDbType.VarChar,50),
				new SqlParameter("@addtime", SqlDbType.DateTime),
				new SqlParameter("@reveint1", SqlDbType.Int,4),
				new SqlParameter("@revevar1", SqlDbType.VarChar,256),
				new SqlParameter("@lid", SqlDbType.Int,4),
				new SqlParameter("@openid", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.lat;
            parameters[1].Value = model.lng;
            parameters[2].Value = model.addtime;
            parameters[3].Value = model.reveint1;
            parameters[4].Value = model.revevar1;
            parameters[5].Value = model.lid;
            parameters[6].Value = model.openid;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }
        /// <summary>
        /// 获得总记录数
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        public int GetCount(string strWhere)
        {
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "WeixinUserLocation"), new SqlParameter("@strWhere", strWhere));
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
        public IList<WeixinUserLocationInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<WeixinUserLocationInfo> infos = new List<WeixinUserLocationInfo>();
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
            parameters[0].Value = "WeixinUserLocation";
            parameters[1].Value = "lid,openid,lat,lng,addtime,reveint1,revevar1";
            parameters[2].Value = "lid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    WeixinUserLocationInfo info = new WeixinUserLocationInfo();
                    info.lid = HJConvert.ToInt32(dr["lid"]);
                    info.openid = HJConvert.ToString(dr["openid"]);
                    info.lat = HJConvert.ToString(dr["lat"]);
                    info.lng = HJConvert.ToString(dr["lng"]);
                    info.addtime = HJConvert.ToDateTime(dr["addtime"]);
                    info.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    info.revevar1 = HJConvert.ToString(dr["revevar1"]);
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
        public int DelWeixinUserLocation(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from WeixinUserLocation where lid=@lid");
            SqlParameter[] Para = 
			{
				new SqlParameter("@lid",SqlDbType.Int)
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
            str.Append("delete from WeixinUserLocation where lid in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 根据openid获取信息
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>lid</param>
        /// <returns>WeixinUserLocationInfo</returns>
        public WeixinUserLocationInfo GetModel(string openid)
        {
            string sql = "select * from WeixinUserLocation where  openid = @openid";
            SqlParameter parameter = new SqlParameter("@openid", SqlDbType.VarChar, 256);
            parameter.Value = openid;
            WeixinUserLocationInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new WeixinUserLocationInfo();
                    model.lid = HJConvert.ToInt32(dr["lid"]);
                    model.openid = HJConvert.ToString(dr["openid"]);
                    model.lat = HJConvert.ToString(dr["lat"]);
                    model.lng = HJConvert.ToString(dr["lng"]);
                    model.addtime = HJConvert.ToDateTime(dr["addtime"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                }
            }
            return model;
        }

    }
}

