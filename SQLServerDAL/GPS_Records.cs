using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

using Hangjing.Model;
using Hangjing.DBUtility;

namespace Hangjing.SQLServerDAL
{
	/// <summary>
	/// 类GPS_Records。
	/// </summary>
	public class GPS_Records
	{
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(GPS_RecordsInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into GPS_Records(");
			strSql.Append("JH1,JH2,JH3,JH4,JH5,AddTime,AddName,UpTime,Remark,Del,baidu)");
			strSql.Append(" values (");
			strSql.Append("@JH1,@JH2,@JH3,@JH4,@JH5,@AddTime,@AddName,@UpTime,@Remark,@Del,@baidu)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@JH1", SqlDbType.VarChar,20),
				new SqlParameter("@JH2", SqlDbType.VarChar,20),
				new SqlParameter("@JH3", SqlDbType.VarChar,20),
				new SqlParameter("@JH4", SqlDbType.VarChar,20),
				new SqlParameter("@JH5", SqlDbType.VarChar,20),
				new SqlParameter("@AddTime", SqlDbType.DateTime),
				new SqlParameter("@AddName", SqlDbType.VarChar,50),
				new SqlParameter("@UpTime", SqlDbType.DateTime),
				new SqlParameter("@Remark", SqlDbType.Text),
				new SqlParameter("@Del", SqlDbType.Int,4),
				new SqlParameter("@baidu", SqlDbType.Int,4)
            };
            parameters[0].Value = model.JH1;
            parameters[1].Value = model.JH2;
            parameters[2].Value = model.JH3;
            parameters[3].Value = model.JH4;
            parameters[4].Value = model.JH5;
            parameters[5].Value = model.AddTime;
            parameters[6].Value = model.AddName;
            parameters[7].Value = model.UpTime;
            parameters[8].Value = model.Remark;
            parameters[9].Value = model.Del;
            parameters[10].Value = model.baidu;

			return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(GPS_RecordsInfo mode)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update GPS_Records set ");
			strSql.Append("JH1=@JH1,");
			strSql.Append("JH2=@JH2,");
			strSql.Append("JH3=@JH3,");
			strSql.Append("JH4=@JH4,");
			strSql.Append("JH5=@JH5,");
			strSql.Append("AddTime=@AddTime,");
			strSql.Append("AddName=@AddName,");
			strSql.Append("UpTime=@UpTime,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("Del=@Del,");
			strSql.Append("baidu=@baidu");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@ID", SqlDbType.Int,4),
				new SqlParameter("@JH1", SqlDbType.VarChar,20),
				new SqlParameter("@JH2", SqlDbType.VarChar,20),
				new SqlParameter("@JH3", SqlDbType.VarChar,20),
				new SqlParameter("@JH4", SqlDbType.VarChar,20),
				new SqlParameter("@JH5", SqlDbType.VarChar,20),
				new SqlParameter("@AddTime", SqlDbType.DateTime),
				new SqlParameter("@AddName", SqlDbType.VarChar,50),
				new SqlParameter("@UpTime", SqlDbType.DateTime),
				new SqlParameter("@Remark", SqlDbType.Text),
				new SqlParameter("@Del", SqlDbType.Int,4),
				new SqlParameter("@baidu", SqlDbType.Int,4)
            };
            parameters[0].Value = mode.ID;
            parameters[1].Value = mode.JH1;
            parameters[2].Value = mode.JH2;
            parameters[3].Value = mode.JH3;
            parameters[4].Value = mode.JH4;
            parameters[5].Value = mode.JH5;
            parameters[6].Value = mode.AddTime;
            parameters[7].Value = mode.AddName;
            parameters[8].Value = mode.UpTime;
            parameters[9].Value = mode.Remark;
            parameters[10].Value = mode.Del;
            parameters[11].Value = mode.baidu;

		    return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
		}

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>ID</param>
        /// <returns>GPS_RecordsInfo</returns>
        public GPS_RecordsInfo GetModel(int ID)
        {
            string sql = "select ID,JH1,JH2,JH3,JH4,JH5,AddTime,AddName,UpTime,Remark,Del,baidu from GPS_Records where  ID = @ID";
            SqlParameter parameter = new SqlParameter("@ID", SqlDbType.Int, 4);
            parameter.Value = ID;
            GPS_RecordsInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new GPS_RecordsInfo();
                    model.ID = HJConvert.ToInt32(dr["ID"]);
                    model.JH1 = HJConvert.ToString(dr["JH1"]);
                    model.JH2 = HJConvert.ToString(dr["JH2"]);
                    model.JH3 = HJConvert.ToString(dr["JH3"]);
                    model.JH4 = HJConvert.ToString(dr["JH4"]);
                    model.JH5 = HJConvert.ToString(dr["JH5"]);
                    model.AddTime = HJConvert.ToDateTime(dr["AddTime"]);
                    model.AddName = HJConvert.ToString(dr["AddName"]);
                    model.UpTime = HJConvert.ToDateTime(dr["UpTime"]);
                    model.Remark = HJConvert.ToString(dr["Remark"]);
                    model.Del = HJConvert.ToInt32(dr["Del"]);
                    model.baidu = HJConvert.ToInt32(dr["baidu"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>ID</param>
        /// <returns>GPS_RecordsInfo</returns>
        public GPS_RecordsInfo GetModelByDid(int ID)
        {
            string sql = "select TOP 1 ID,JH2,JH3 from GPS_Records where  JH1 = @ID order by ID desc";
            SqlParameter parameter = new SqlParameter("@ID", SqlDbType.Int, 4);
            parameter.Value = ID;
            GPS_RecordsInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new GPS_RecordsInfo();
                    model.JH2 = HJConvert.ToString(dr["JH2"]);
                    model.JH3 = HJConvert.ToString(dr["JH3"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "GPS_Records"), new SqlParameter("@strWhere", strWhere));
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
        public IList<GPS_RecordsInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<GPS_RecordsInfo> infos = new List<GPS_RecordsInfo>();
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
            parameters[0].Value = "GPS_Records";
            parameters[1].Value = "ID,JH1,JH2,JH3,JH4,JH5,AddTime,AddName,UpTime,Remark,Del,baidu";
            parameters[2].Value = "ID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    GPS_RecordsInfo info = new GPS_RecordsInfo();
                    info.ID = HJConvert.ToInt32(dr["ID"]);
                    info.JH1 = HJConvert.ToString(dr["JH1"]);
                    info.JH2 = HJConvert.ToString(dr["JH2"]);
                    info.JH3 = HJConvert.ToString(dr["JH3"]);
                    info.JH4 = HJConvert.ToString(dr["JH4"]);
                    info.JH5 = HJConvert.ToString(dr["JH5"]);
                    info.AddTime = HJConvert.ToDateTime(dr["AddTime"]);
                    info.AddName = HJConvert.ToString(dr["AddName"]);
                    info.UpTime = HJConvert.ToDateTime(dr["UpTime"]);
                    info.Remark = HJConvert.ToString(dr["Remark"]);
                    info.Del = HJConvert.ToInt32(dr["Del"]);
                    info.baidu = HJConvert.ToInt32(dr["baidu"]);
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
        public int DelGPS_Records(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from GPS_Records where ID=@ID");
            SqlParameter[] Para = 
			{
				new SqlParameter("@ID",SqlDbType.Int)
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
            str.Append("delete from GPS_Records where ID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 获取IMEI号对应最新的一条位置信息
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>ID</param>
        /// <returns>GPS_RecordsInfo</returns>
        public GPS_RecordsInfo GetModelByImei(int Imei)
        {
            //select top 1 ID,JH1,JH2,JH3,JH4,JH5,AddTime,AddName,UpTime,Remark,Del,baidu from GPS_Records where  JH1 = '353419035495394' order by id desc

            string sql = "select top 1 ID,JH1,JH2,JH3,JH4,JH5,AddTime,AddName,UpTime,Remark,Del,baidu from GPS_Records where  JH1 = @JH1 order by id desc";
            SqlParameter parameter = new SqlParameter("@JH1", SqlDbType.VarChar, 50);
            parameter.Value = Imei;

            GPS_RecordsInfo model = null;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new GPS_RecordsInfo();
                    model.ID = HJConvert.ToInt32(dr["ID"]);
                    model.JH1 = HJConvert.ToString(dr["JH1"]);
                    model.JH2 = HJConvert.ToString(dr["JH2"]);
                    model.JH3 = HJConvert.ToString(dr["JH3"]);
                    model.JH4 = HJConvert.ToString(dr["JH4"]);
                    model.JH5 = HJConvert.ToString(dr["JH5"]);
                    model.AddTime = HJConvert.ToDateTime(dr["AddTime"]);
                    model.AddName = HJConvert.ToString(dr["AddName"]);
                    model.UpTime = HJConvert.ToDateTime(dr["UpTime"]);
                    model.Remark = HJConvert.ToString(dr["Remark"]);
                    model.Del = HJConvert.ToInt32(dr["Del"]);
                    model.baidu = HJConvert.ToInt32(dr["baidu"]);
                }
            }
            return model;
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
        public IList<localInfo> GetDeliverPath(int did, string date)
        {
            IList<localInfo> infos = new List<localInfo>();
            SqlParameter[] parameters =
            {
                new SqlParameter("@did", SqlDbType.Int,5),
                new SqlParameter("@date", SqlDbType.VarChar,20),
            };
            parameters[0].Value = did;
            parameters[1].Value = date;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "GPS_Records_GetDeliverPath", parameters))
            {
                while (dr.Read())
                {
                    localInfo info = new localInfo();
                    info.lat = HJConvert.ToString(dr["lat"]);
                    info.lng = HJConvert.ToString(dr["lng"]);
                    info.bear = HJConvert.ToString(dr["AddTime"]);
                    
                    infos.Add(info);
                }
            }
            return infos;
        }
    }
}

