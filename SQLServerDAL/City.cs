using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hangjing.Model;
using Hangjing.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace Hangjing.SQLServerDAL
{
	/// <summary>
	/// 城市数据
	/// </summary>
	public class City
	{
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(CityInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into City(");
            strSql.Append("cname,site,url,adddate,ReveInt,ReveVar,lat,lng,ratio");
            strSql.Append(") values (");
            strSql.Append("@cname,@site,@url,@adddate,@ReveInt,@ReveVar,@lat,@lng,@ratio");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
			{
			        new SqlParameter("@cname", SqlDbType.VarChar,50) ,            
                    new SqlParameter("@site", SqlDbType.VarChar,50) ,            
                    new SqlParameter("@url", SqlDbType.VarChar,256) ,            
                    new SqlParameter("@adddate", SqlDbType.DateTime) ,            
                    new SqlParameter("@ReveInt", SqlDbType.Int,4) ,            
                    new SqlParameter("@ReveVar", SqlDbType.VarChar,256) ,            
                    new SqlParameter("@lat", SqlDbType.VarChar,50) ,            
                    new SqlParameter("@lng", SqlDbType.VarChar,50),
                    new SqlParameter("@ratio", SqlDbType.Decimal,9)
              
            };

            parameters[0].Value = model.cname;
            parameters[1].Value = model.site;
            parameters[2].Value = model.url;
            parameters[3].Value = model.adddate;
            parameters[4].Value = model.ReveInt;
            parameters[5].Value = model.ReveVar;
            parameters[6].Value = model.Lat;
            parameters[7].Value = model.Lng;
            parameters[8].Value = model.ratio;

            return HJConvert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(CityInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update City set ");
            strSql.Append(" cname = @cname , ");
            strSql.Append(" site = @site , ");
            strSql.Append(" url = @url , ");
            strSql.Append(" adddate = @adddate , ");
            strSql.Append(" ReveInt = @ReveInt , ");
            strSql.Append(" ReveVar = @ReveVar , ");
            strSql.Append(" lat = @lat , ");
            strSql.Append(" lng = @lng ,");
            strSql.Append(" ratio = @ratio  ");
            strSql.Append(" where cid=@cid ");

            SqlParameter[] parameters = 
			{
			    new SqlParameter("@cid", SqlDbType.Int,4) ,            
                new SqlParameter("@cname", SqlDbType.VarChar,50) ,            
                new SqlParameter("@site", SqlDbType.VarChar,50) ,            
                new SqlParameter("@url", SqlDbType.VarChar,256) ,            
                new SqlParameter("@adddate", SqlDbType.DateTime) ,            
                new SqlParameter("@ReveInt", SqlDbType.Int,4) ,            
                new SqlParameter("@ReveVar", SqlDbType.VarChar,256) ,            
                new SqlParameter("@lat", SqlDbType.VarChar,50) ,            
                new SqlParameter("@lng", SqlDbType.VarChar,50),
                new SqlParameter("@ratio", SqlDbType.Decimal,9)
              
            };

            parameters[0].Value = model.cid;
            parameters[1].Value = model.cname;
            parameters[2].Value = model.site;
            parameters[3].Value = model.url;
            parameters[4].Value = model.adddate;
            parameters[5].Value = model.ReveInt;
            parameters[6].Value = model.ReveVar;
            parameters[7].Value = model.Lat;
            parameters[8].Value = model.Lng;
            parameters[9].Value = model.ratio;
            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>cid</param>
        /// <returns>CityInfo</returns>
        public CityInfo GetModel(int cid)
        {
            string sql = "select * from City where  cid = @cid";
            SqlParameter parameter = new SqlParameter("@cid", SqlDbType.Int, 4);
            parameter.Value = cid;
            CityInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new CityInfo();
                    model.cid = HJConvert.ToInt32(dr["cid"]);
                    model.cname = HJConvert.ToString(dr["cname"]);
                    model.site = HJConvert.ToString(dr["site"]);
                    model.url = HJConvert.ToString(dr["url"]);
                    model.adddate = HJConvert.ToDateTime(dr["adddate"]);
                    model.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    model.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    model.Lat = HJConvert.ToString(dr["Lat"]);
                    model.Lng = HJConvert.ToString(dr["Lng"]);
                    model.ratio = HJConvert.ToString(dr["ratio"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "City"), new SqlParameter("@strWhere", strWhere));
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
        public IList<CityInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<CityInfo> infos = new List<CityInfo>();
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
            parameters[0].Value = "City";
            parameters[1].Value = "*";
            parameters[2].Value = "cid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    CityInfo info = new CityInfo();
                    info.cid = HJConvert.ToInt32(dr["cid"]);
                    info.cname = HJConvert.ToString(dr["cname"]);
                    info.site = HJConvert.ToString(dr["site"]);
                    info.url = HJConvert.ToString(dr["url"]);
                    info.adddate = HJConvert.ToDateTime(dr["adddate"]);
                    info.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    info.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    info.Lat = HJConvert.ToString(dr["Lat"]);
                    info.Lng = HJConvert.ToString(dr["Lng"]);
                    info.ratio = HJConvert.ToString(dr["ratio"]);
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
        public int DelCity(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from City where cid=@cid");
            SqlParameter[] Para = 
			{
				new SqlParameter("@cid",SqlDbType.Int)
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
            str.Append("delete from City where cid in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 以字母分组
        /// </summary>
        public IList<CityInfo> GetLeterList()
        {
            IList<CityInfo> infos = new List<CityInfo>();
            string sql = "select revevar from city group by revevar ";
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    CityInfo info = new CityInfo();
                    info.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        ///获取所有城市
        /// </summary>
        public IList<CityInfo> GetAllCity()
        {
            IList<CityInfo> infos = new List<CityInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "city_getAll", null))
            {
                while (dr.Read())
                {
                    CityInfo info = new CityInfo();
                    info.cid = HJConvert.ToInt32(dr["cid"]);
                    info.cname = HJConvert.ToString(dr["cname"]);
                    info.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    info.url = HJConvert.ToString(dr["url"]);
                    info.ReveInt = HJConvert.ToInt32(dr["ReveInt"]);
                    info.Lat = HJConvert.ToString(dr["Lat"]);
                    info.Lng = HJConvert.ToString(dr["Lng"]);
                    info.ratio = HJConvert.ToString(dr["ratio"]);
                    infos.Add(info);
                }
            }
            return infos;
        }


        /// <summary>
        /// 获取所有城市首字母
        /// </summary>
        /// <returns></returns>
        public IList<CityInfo> getCityPy()
        {
            IList<CityInfo> infos = new List<CityInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, "select distinct ReveVar from dbo.City order by ReveVar", null))
            {
                while (dr.Read())
                {
                    CityInfo info = new CityInfo();
                    info.ReveVar = HJConvert.ToString(dr["ReveVar"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 分区域交易额统计
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="strWhere"></param>
        /// <param name="orderwhere"></param>
        /// <returns></returns>
        public IList<TogoInfo> GetListWithOrderStatistics(int pagesize, int pageindex, string strWhere, string orderwhere, string ordername)
        {
            IList<TogoInfo> infos = new List<TogoInfo>();
            SqlParameter[] parameters = 
			{
				new SqlParameter("@PageSize", SqlDbType.Int),
				new SqlParameter("@PageIndex", SqlDbType.Int),
				new SqlParameter("@strWhere", SqlDbType.VarChar,1500),
                new SqlParameter("@orderwhere", SqlDbType.VarChar,1500),
                new SqlParameter("@ordername", SqlDbType.VarChar,100)
			};
            parameters[0].Value = pagesize;
            parameters[1].Value = pageindex;
            parameters[2].Value = strWhere;
            parameters[3].Value = orderwhere;
            parameters[4].Value = ordername;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "AreaAmount_getlist", parameters))
            {
                while (dr.Read())
                {
                    TogoInfo info = new TogoInfo();
                    info.TogoName = HJConvert.ToString(dr["classname"]);
                    info.packagefee = HJConvert.ToDecimal(dr["packagefee"]);
                    
                    info.allprice = HJConvert.ToDecimal(dr["TotalPrice"]);
                    
                    info.DataID = HJConvert.ToInt32(dr["recordtcount"]);
                    
                    info.paymoney = HJConvert.ToDecimal(dr["paymoney"]);
                    info.payamount = HJConvert.ToDecimal(dr["payamount"]);
                    
                    infos.Add(info);
                }
            }
            return infos;
        }
	}
}

