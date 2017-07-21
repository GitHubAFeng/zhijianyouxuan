using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;

using System.Data.SqlClient;
using System.Data;

namespace Hangjing.SQLServerDAL
{
    public class TogoPicture
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.TogoPictureInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TogoPicture(");
            strSql.Append("TogoId,Picture,Pri,Inve1,Inve2)");
            strSql.Append(" values (");
            strSql.Append("@TogoId,@Picture,@Pri,@Inve1,@Inve2)");
            SqlParameter[] parameters = 
            {
			    new SqlParameter("@TogoId", SqlDbType.Int,4),
			    new SqlParameter("@Picture", SqlDbType.VarChar,256),
			    new SqlParameter("@Pri", SqlDbType.Int,4),
			    new SqlParameter("@Inve1",SqlDbType.Int,4),
			    new SqlParameter("@Inve2", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.TogoId;
            parameters[1].Value = model.Picture;
            parameters[2].Value = model.Pri;
            parameters[3].Value = model.Inve1;
            parameters[4].Value = model.Inve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.TogoPictureInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TogoPicture set ");
            strSql.Append("TogoId=@TogoId,");
            strSql.Append("Picture=@Picture,");
            strSql.Append("Pri=@Pri,");
            strSql.Append("Inve1=@Inve1,");
            strSql.Append("Inve2=@Inve2");
            strSql.Append(" where DataId=@DataId ");
            SqlParameter[] parameters =
            {
			    new SqlParameter("@DataId", SqlDbType.Int,4),
			    new SqlParameter("@TogoId", SqlDbType.Int,4),
			    new SqlParameter("@Picture", SqlDbType.VarChar,256),
			    new SqlParameter("@Pri",SqlDbType.Int,4),
			    new SqlParameter("@Inve1", SqlDbType.Int,4),
			    new SqlParameter("@Inve2", SqlDbType.VarChar,256)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.TogoId;
            parameters[2].Value = model.Picture;
            parameters[3].Value = model.Pri;
            parameters[4].Value = model.Inve1;
            parameters[5].Value = model.Inve2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>TogoPictureInfo</returns>
        public TogoPictureInfo GetModel(int DataId)
        {
            string sql = "select DataId,TogoId,Picture,Pri,Inve1,Inve2 , (select name from points where unid = TogoPicture.TogoId) as togoname from TogoPicture where  DataId = @DataId";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            TogoPictureInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new TogoPictureInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    model.Picture = HJConvert.ToString(dr["Picture"]);
                    model.Pri = HJConvert.ToInt32(dr["Pri"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.TogoName = HJConvert.ToString(dr["TogoName"]);
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
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "TogoPicture"), new SqlParameter("@strWhere", strWhere)));
        }

         //<summary>
         //获得某个商家幻灯片的个数
         //</summary>
         //此代码由杭景科技代码内部生成器自动生成
        public TogoPictureInfo GetPictureCount(int TogoId)
        {
            string sql = "select count(Picture) as PictureCount   from TogoPicture where  TogoId = @TogoId";
            SqlParameter parameter = new SqlParameter("@TogoId", SqlDbType.Int, 4);    
            parameter.Value = TogoId;
            TogoPictureInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new TogoPictureInfo();
                    model.PictureCount = HJConvert.ToInt32(dr["PictureCount"]);
               
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
        public IList<TogoPictureInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<TogoPictureInfo> infos = new List<TogoPictureInfo>();
            SqlParameter[] parameters = 
		    {
                new SqlParameter("@tblName", SqlDbType.VarChar,255),
                new SqlParameter("@strGetFields",SqlDbType.VarChar,1000),
                new SqlParameter("@primary", SqlDbType.VarChar,255),
                new SqlParameter("@orderName", SqlDbType.VarChar,255),
                new SqlParameter("@PageSize", SqlDbType.Int),
                new SqlParameter("@PageIndex", SqlDbType.Int),
                new SqlParameter("@OrderType", SqlDbType.Bit),
                new SqlParameter("@strWhere",SqlDbType.VarChar,1500)
			};
            parameters[0].Value = "TogoPicture";
            parameters[1].Value = "DataId,TogoId,Picture,Pri,Inve1,Inve2,(select Name from points where unid=TogoPicture.TogoId) as TogoName";
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
                    TogoPictureInfo info = new TogoPictureInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.TogoId = HJConvert.ToInt32(dr["TogoId"]);
                    info.Picture = HJConvert.ToString(dr["Picture"]);
                    info.Pri = HJConvert.ToInt32(dr["Pri"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.TogoName = HJConvert.ToString(dr["TogoName"]);
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
        public int DelTogoPicture(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from TogoPicture where DataId=@DataId");
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
            str.Append("delete from TogoPicture where DataId in ({0})");
            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}
