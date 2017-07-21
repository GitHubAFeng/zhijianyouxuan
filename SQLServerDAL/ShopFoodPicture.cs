using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Hangjing.Model;
using Hangjing.DBUtility;
namespace Hangjing.SQLServerDAL
{
    /// <summary>
    ///商家标签库管理
    /// </summary>
    public class ShopFoodPicture
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.ShopFoodPictureInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ShopFoodPicture(");
            strSql.Append("ShopId,Url,Picture,Title,Inve1,Inve2,cityid)");
            strSql.Append(" values (");
            strSql.Append("@ShopId,@Url,@Picture,@Title,@Inve1,@Inve2,@cityid)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@ShopId", SqlDbType.Int,4),
				new SqlParameter("@Url", SqlDbType.VarChar,256),
				new SqlParameter("@Picture", SqlDbType.VarChar,256),
				new SqlParameter("@Title", SqlDbType.VarChar,256),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,256),
				new SqlParameter("@cityid", SqlDbType.Int,4)
            };
            parameters[0].Value = model.ShopId;
            parameters[1].Value = model.Url;
            parameters[2].Value = model.Picture;
            parameters[3].Value = model.Title;
            parameters[4].Value = model.Inve1;
            parameters[5].Value = model.Inve2;
            parameters[6].Value = model.cityid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.ShopFoodPictureInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ShopFoodPicture set ");
            strSql.Append("ShopId=@ShopId,");
            strSql.Append("Url=@Url,");
            strSql.Append("Picture=@Picture,");
            strSql.Append("Title=@Title,");
            strSql.Append("Inve1=@Inve1,");
            strSql.Append("Inve2=@Inve2,");
            strSql.Append("cityid=@cityid");
            strSql.Append(" where IID=@IID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@IID", SqlDbType.Int,4),
				new SqlParameter("@ShopId", SqlDbType.Int,4),
				new SqlParameter("@Url", SqlDbType.VarChar,256),
				new SqlParameter("@Picture", SqlDbType.VarChar,256),
				new SqlParameter("@Title", SqlDbType.VarChar,256),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,256),
				new SqlParameter("@cityid", SqlDbType.Int,4)
            };
            parameters[0].Value = model.IID;
            parameters[1].Value = model.ShopId;
            parameters[2].Value = model.Url;
            parameters[3].Value = model.Picture;
            parameters[4].Value = model.Title;
            parameters[5].Value = model.Inve1;
            parameters[6].Value = model.Inve2;
            parameters[7].Value = model.cityid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>IID</param>
        /// <returns>ShopFoodPictureInfo</returns>
        public ShopFoodPictureInfo GetModel(int IID)
        {
            string sql = "select IID,ShopId,Url,Picture,Title,Inve1,Inve2,cityid from ShopFoodPicture where  IID = @IID";
            SqlParameter parameter = new SqlParameter("@IID", SqlDbType.Int, 4);
            parameter.Value = IID;
            ShopFoodPictureInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new ShopFoodPictureInfo();
                    model.IID = HJConvert.ToInt32(dr["IID"]);
                    model.ShopId = HJConvert.ToInt32(dr["ShopId"]);
                    model.Url = HJConvert.ToString(dr["Url"]);
                    model.Picture = HJConvert.ToString(dr["Picture"]);
                    model.Title = HJConvert.ToString(dr["Title"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.cityid = HJConvert.ToInt32(dr["cityid"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "ShopFoodPicture"), new SqlParameter("@strWhere", strWhere));
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
        public IList<ShopFoodPictureInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ShopFoodPictureInfo> infos = new List<ShopFoodPictureInfo>();
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
            parameters[0].Value = "ShopFoodPicture";
            parameters[1].Value = "IID,ShopId,Url,Picture,Title,Inve1,Inve2,cityid";
            parameters[2].Value = "IID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    ShopFoodPictureInfo info = new ShopFoodPictureInfo();
                    info.IID = HJConvert.ToInt32(dr["IID"]);
                    info.ShopId = HJConvert.ToInt32(dr["ShopId"]);
                    info.Url = HJConvert.ToString(dr["Url"]);
                    info.Picture = HJConvert.ToString(dr["Picture"]);
                    info.Title = HJConvert.ToString(dr["Title"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.cityid = HJConvert.ToInt32(dr["cityid"]);
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
        public int DelShopFoodPicture(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ShopFoodPicture where IID=@IID");
            SqlParameter[] Para = 
			{
				new SqlParameter("@IID",SqlDbType.Int)
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
            str.Append("delete from ShopFoodPicture where IID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }
}

