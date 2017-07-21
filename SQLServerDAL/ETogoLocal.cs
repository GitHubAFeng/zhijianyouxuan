using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Hangjing.DBUtility;
using Hangjing.Model;

namespace Hangjing.SQLServerDAL
{
    public class ETogoLocal
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.ETogoLocalInfo model)
        {

            SqlParameter[] parameters = 
            {
                new SqlParameter("@TogoId",SqlDbType.Int,4),
                new SqlParameter("@lat", SqlDbType.VarChar,50),
                new SqlParameter("@lng", SqlDbType.VarChar,50),
                new SqlParameter("@polygon", SqlDbType.VarChar,4096),
                new SqlParameter("@radius", SqlDbType.Decimal,9),
            };

            parameters[0].Value = model.TogoId;
            parameters[1].Value = model.Lat;
            parameters[2].Value = model.Lng;
            parameters[3].Value = model.Polygon;
            parameters[4].Value = model.Radius;

            return SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "SaveTogoLocalInfo", parameters);
        }


        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public int Add(Hangjing.Model.ETogoLocalInfo model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("insert into ETogoLocalInfo(");
        //    strSql.Append("TogoId,lat,lng,polygon,radius)");
        //    strSql.Append(" values (");
        //    strSql.Append("@TogoId,@lat,@lng,@polygon,@radius)");
        //    SqlParameter[] parameters = 
        //    {
        //        new SqlParameter("@TogoId",SqlDbType.Int,4),
        //        new SqlParameter("@lat", SqlDbType.VarChar,50),
        //        new SqlParameter("@lng", SqlDbType.VarChar,50),
        //        new SqlParameter("@polygon", SqlDbType.VarChar,4096),
        //        new SqlParameter("@radius", SqlDbType.Decimal,9),
        //    };

        //    parameters[0].Value = model.TogoId;
        //    parameters[1].Value = model.Lat;
        //    parameters[2].Value = model.Lng;
        //    parameters[3].Value = model.Polygon;
        //    parameters[4].Value = model.Radius;

        //    return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
       // }



        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="Id"></param>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <returns></returns>
        public int DelETogoLocal(int TogoId)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ETogoLocalInfo TogoId=@TogoId");
            SqlParameter[] Para = 
	        {
		        new SqlParameter("@TogoId",SqlDbType.Int)
	        };
            Para[0].Value = TogoId;

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
            str.Append("delete from ETogoLocalInfo where TogoId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ETogoLocalInfo info)
        {
            string sql = "update ETogoLocalInfo set lat=@lat, lng=@lng, polygon=@polygon,radius=@radius where TogoId=@TogoId";
            SqlParameter[] parameters = 
            {
                new SqlParameter("@TogoId",SqlDbType.Int,4),
                new SqlParameter("@lat", SqlDbType.VarChar,50),
                new SqlParameter("@lng", SqlDbType.VarChar,50),
                new SqlParameter("@polygon", SqlDbType.VarChar,4096),
                new SqlParameter("@radius", SqlDbType.Decimal,9),
                
            };
            parameters[0].Value = info.TogoId;
            parameters[1].Value = info.Lat;
            parameters[2].Value = info.Lng;
            parameters[3].Value = info.Polygon;
            parameters[4].Value = info.Radius;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, parameters);
        }

        /// <summary>
        /// 根据ID获取商家定位信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ETogoLocalInfo GetInfoById(string id)
        {
            string sql = "select * from ETogoLocalInfo where TogoId=@TogoId";

            SqlParameter[] parameters = 
            {
                new SqlParameter("@TogoId", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            ETogoLocalInfo info = new ETogoLocalInfo();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameters))
            {
                if (dr.Read())
                {
                    info = new ETogoLocalInfo();
                    info.DataId = HJConvert.ToInt32(dr["dataid"]);
                    info.TogoId = HJConvert.ToInt32(dr["togoid"]);
                    info.Lat = HJConvert.ToString(dr["lat"]);
                    info.Lng = HJConvert.ToString(dr["lng"]);
                    info.Polygon = HJConvert.ToString(dr["polygon"]);
                    info.Radius = HJConvert.ToDecimal(dr["radius"]);
                }
            }

            return info;
        }

        /// <summary>
        /// 取得全部信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll()
        {

            IList<ETogoLocalInfo> infos = GetList();

            System.Data.DataTable togolocal_dt = new System.Data.DataTable("ETogoLocalInfo");

            //** System.Reflection.PropertyInfo[] 利用.Net反射机制实现Ilist和DateTable的转换。

            System.Reflection.PropertyInfo[] propertys = typeof(ETogoLocalInfo).GetProperties();
            foreach (System.Reflection.PropertyInfo pro in propertys)
            {
                togolocal_dt.Columns.Add(pro.Name, pro.PropertyType);
            }

            foreach (ETogoLocalInfo local in infos)
            {
                System.Data.DataRow dr = togolocal_dt.NewRow();
                dr["DataId"] = local.DataId.ToString();
                dr["TogoId"] = local.TogoId.ToString();
                dr["Lat"] = local.Lat.ToString();
                dr["Lng"] = local.Lng.ToString();
                dr["Polygon"] = local.Polygon.ToString();
                dr["Radius"] = local.Radius.ToString();

                togolocal_dt.Rows.Add(dr);
            }

            return togolocal_dt;
        }

        public IList<ETogoLocalInfo> GetList()
        {
            string sql = "select * from ETogoLocalInfo";
            IList<ETogoLocalInfo> list = new List<ETogoLocalInfo>();
            ETogoLocalInfo info = new ETogoLocalInfo();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    info = new ETogoLocalInfo();
                    info.DataId = HJConvert.ToInt32(dr["dataid"]);
                    info.TogoId = HJConvert.ToInt32(dr["togoid"]);
                    info.Lat = HJConvert.ToString(dr["lat"]);
                    info.Lng = HJConvert.ToString(dr["lng"]);
                    info.Polygon = HJConvert.ToString(dr["polygon"]);
                    info.Radius = HJConvert.ToDecimal(dr["radius"]);

                    list.Add(info);
                }
            }

            return list;
        }
    }
}
