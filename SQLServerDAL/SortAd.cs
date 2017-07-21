using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Hangjing.Model;

using Hangjing.DBUtility;

namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 分类对应的文字广告
    /// </summary>
    public class SortAd
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SortAdInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SortAd(");
            strSql.Append("Picture,Link,Introduce,state,title,Width,Height,isLink,stype,sortid,Servicefee,AdStartDate,AdEndDate,defautpic,cityid)");
            strSql.Append(" values (");
            strSql.Append("@Picture,@Link,@Introduce,@state,@title,@Width,@Height,@isLink,@stype,@sortid,@Servicefee,@AdStartDate,@AdEndDate,@defautpic,@cityid)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@Picture", SqlDbType.VarChar,256),
				new SqlParameter("@Link", SqlDbType.VarChar,256),
				new SqlParameter("@Introduce", SqlDbType.VarChar,4096),
				new SqlParameter("@state", SqlDbType.VarChar,256),
				new SqlParameter("@title", SqlDbType.VarChar,256),
				new SqlParameter("@Width", SqlDbType.Int,4),
				new SqlParameter("@Height", SqlDbType.Int,4),
                new SqlParameter("@isLink",SqlDbType.Int,4),
                new SqlParameter("@stype", SqlDbType.Int,4),
				new SqlParameter("@sortid", SqlDbType.Int,4),
				new SqlParameter("@Servicefee", SqlDbType.Decimal,5),
				new SqlParameter("@AdStartDate", SqlDbType.DateTime),
				new SqlParameter("@AdEndDate", SqlDbType.DateTime),
				new SqlParameter("@defautpic", SqlDbType.VarChar,256),
                new SqlParameter("@cityid",SqlDbType.Int)
            };
            parameters[0].Value = model.Picture;
            parameters[1].Value = model.Link;
            parameters[2].Value = model.Introduce;
            parameters[3].Value = model.state;
            parameters[4].Value = model.title;
            parameters[5].Value = 0;
            parameters[6].Value = 0;
            parameters[7].Value = model.isLink;
            parameters[8].Value = 0;
            parameters[9].Value = model.sortid;
            parameters[10].Value = model.Servicefee;
            parameters[11].Value = model.AdStartDate;
            parameters[12].Value = model.AdEndDate;
            parameters[13].Value = model.defautpic;
            parameters[14].Value = model.cityid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(SortAdInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SortAd set ");
            strSql.Append("Picture=@Picture,");
            strSql.Append("Link=@Link,");
            strSql.Append("Introduce=@Introduce,");
            strSql.Append("state=@state,title=@title,Width=@Width,Height=@Height,isLink=@isLink,");
            strSql.Append("stype=@stype,");
            strSql.Append("sortid=@sortid,");
            strSql.Append("Servicefee=@Servicefee,");
            strSql.Append("AdStartDate=@AdStartDate,");
            strSql.Append("AdEndDate=@AdEndDate,");
            strSql.Append("defautpic=@defautpic,cityid=@cityid ");
            strSql.Append(" where DataId=@DataId ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataId", SqlDbType.VarChar,256),
				new SqlParameter("@Picture", SqlDbType.VarChar,256),
				new SqlParameter("@Link", SqlDbType.VarChar,256),
				new SqlParameter("@Introduce", SqlDbType.VarChar,4096),
				new SqlParameter("@state", SqlDbType.VarChar,256),
                new SqlParameter("@title", SqlDbType.VarChar,256),
                new SqlParameter("@Width", SqlDbType.Int,4),
                new SqlParameter("@Height", SqlDbType.Int,4),
                new SqlParameter("@isLink",SqlDbType.Int,4),
                new SqlParameter("@stype", SqlDbType.Int,4),
				new SqlParameter("@sortid", SqlDbType.Int,4),
				new SqlParameter("@Servicefee", SqlDbType.Decimal,5),
				new SqlParameter("@AdStartDate", SqlDbType.DateTime),
				new SqlParameter("@AdEndDate", SqlDbType.DateTime),
				new SqlParameter("@defautpic", SqlDbType.VarChar,256),
                new SqlParameter("@cityid",SqlDbType.Int)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.Picture;
            parameters[2].Value = model.Link;
            parameters[3].Value = model.Introduce;
            parameters[4].Value = model.state;
            parameters[5].Value = model.title;
            parameters[6].Value = model.Width;
            parameters[7].Value = model.Height;
            parameters[8].Value = model.isLink;
            parameters[9].Value = model.stype;
            parameters[10].Value = model.sortid;
            parameters[11].Value = model.Servicefee;
            parameters[12].Value = model.AdStartDate;
            parameters[13].Value = model.AdEndDate;
            parameters[14].Value = model.defautpic;
            parameters[15].Value = model.cityid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>SortAdInfo</returns>
        public SortAdInfo GetModel(int DataId)
        {
            string sql = "select DataId,Picture,Link,Introduce,state,title,Width,Height,isLink,stype,sortid,Servicefee,AdStartDate,AdEndDate,defautpic from SortAd where  DataId = @DataId";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            SortAdInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new SortAdInfo();
                    model.DataId = HJConvert.ToString(dr["DataId"]);
                    model.Picture = HJConvert.ToString(dr["Picture"]);
                    model.Link = HJConvert.ToString(dr["Link"]);
                    model.Introduce = HJConvert.ToString(dr["Introduce"]);
                    model.state = HJConvert.ToString(dr["state"]);
                    model.title = HJConvert.ToString(dr["title"]);
                    model.Width = HJConvert.ToInt32(dr["Width"]);
                    model.Height = HJConvert.ToInt32(dr["Height"]);
                    model.isLink = HJConvert.ToInt32(dr["isLink"]);
                    model.stype = HJConvert.ToInt32(dr["stype"]);
                    model.sortid = HJConvert.ToInt32(dr["sortid"]);
                    model.Servicefee = HJConvert.ToDecimal(dr["Servicefee"]);
                    model.AdStartDate = HJConvert.ToDateTime(dr["AdStartDate"]);
                    model.AdEndDate = HJConvert.ToDateTime(dr["AdEndDate"]);
                    model.defautpic = HJConvert.ToString(dr["defautpic"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "SortAd"), new SqlParameter("@strWhere", strWhere));
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
        public IList<SortAdInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<SortAdInfo> infos = new List<SortAdInfo>();
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
            parameters[0].Value = "SortAd";
            parameters[1].Value = "*";
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
                    SortAdInfo info = new SortAdInfo();
                    info.DataId = HJConvert.ToString(dr["DataId"]);
                    info.Picture = HJConvert.ToString(dr["Picture"]);
                    info.Link = HJConvert.ToString(dr["Link"]);
                    info.Introduce = HJConvert.ToString(dr["Introduce"]);
                    info.state = HJConvert.ToString(dr["state"]);
                    info.title = HJConvert.ToString(dr["title"]);
                    info.Width = HJConvert.ToInt32(dr["Width"]);
                    info.Height = HJConvert.ToInt32(dr["Height"]);
                    info.isLink = HJConvert.ToInt32(dr["isLink"]);
                    info.cityid = HJConvert.ToInt32(dr["cityid"]);
                    info.stype = HJConvert.ToInt32(dr["stype"]);
                    info.sortid = HJConvert.ToInt32(dr["sortid"]);
                    info.Servicefee = HJConvert.ToDecimal(dr["Servicefee"]);
                    info.AdStartDate = HJConvert.ToDateTime(dr["AdStartDate"]);
                    info.AdEndDate = HJConvert.ToDateTime(dr["AdEndDate"]);
                    info.defautpic = HJConvert.ToString(dr["defautpic"]);
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
        public int DelAdInfo(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from SortAd where DataId=@DataId");
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
            str.Append("delete from SortAd where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>SortAdInfo</returns>
        public SortAdInfo GetModel(int DataId, int secid)
        {
            string sql = "select * from SortAd where  DataId = " + DataId + " and cityid=" + secid;
            SortAdInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (dr.Read())
                {
                    model = new SortAdInfo();
                    model.DataId = HJConvert.ToString(dr["DataId"]);
                    model.Picture = HJConvert.ToString(dr["Picture"]);
                    model.Link = HJConvert.ToString(dr["Link"]);
                    model.Introduce = HJConvert.ToString(dr["Introduce"]);
                    model.state = HJConvert.ToString(dr["state"]);
                    model.title = HJConvert.ToString(dr["title"]);
                    model.Width = HJConvert.ToInt32(dr["Width"]);
                    model.Height = HJConvert.ToInt32(dr["Height"]);
                    model.isLink = HJConvert.ToInt32(dr["isLink"]);
                    model.cityid = HJConvert.ToInt32(dr["cityid"]);
                    model.stype = HJConvert.ToInt32(dr["stype"]);
                    model.sortid = HJConvert.ToInt32(dr["sortid"]);
                    model.Servicefee = HJConvert.ToDecimal(dr["Servicefee"]);
                    model.AdStartDate = HJConvert.ToDateTime(dr["AdStartDate"]);
                    model.AdEndDate = HJConvert.ToDateTime(dr["AdEndDate"]);
                    model.defautpic = HJConvert.ToString(dr["defautpic"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add_fix(SortAdInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SortAd(");
            strSql.Append("dataid,Picture,Link,Introduce,state,title,Width,Height,isLink,cityid)");
            strSql.Append(" values (");
            strSql.Append("@dataid,@Picture,@Link,@Introduce,@state,@title,@Width,@Height,@isLink,@cityid)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
            {
                new SqlParameter("@DataId", SqlDbType.VarChar,256),
				new SqlParameter("@Picture", SqlDbType.VarChar,256),
				new SqlParameter("@Link", SqlDbType.VarChar,256),
				new SqlParameter("@Introduce", SqlDbType.VarChar,4096),
				new SqlParameter("@state", SqlDbType.VarChar,256),
				new SqlParameter("@title", SqlDbType.VarChar,256),
				new SqlParameter("@Width", SqlDbType.Int,4),
				new SqlParameter("@Height", SqlDbType.Int,4),
                new SqlParameter("@isLink",SqlDbType.Int,4),
                new SqlParameter("@cityid", SqlDbType.Int,4)
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.Picture;
            parameters[2].Value = model.Link;
            parameters[3].Value = model.Introduce;
            parameters[4].Value = model.state;
            parameters[5].Value = model.title;
            parameters[6].Value = model.Width;
            parameters[7].Value = model.Height;
            parameters[8].Value = model.isLink;
            parameters[9].Value = model.cityid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取所有广告
        /// </summary>
        public IList<SortAdInfo> GetAll()
        {
            IList<SortAdInfo> infos = new List<SortAdInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "getAllSortAd", null))
            {
                while (dr.Read())
                {
                    SortAdInfo info = new SortAdInfo();
                    info.DataId = HJConvert.ToString(dr["DataId"]);
                    info.Picture = HJConvert.ToString(dr["Picture"]);
                    info.Link = HJConvert.ToString(dr["Link"]);
                    info.Introduce = HJConvert.ToString(dr["Introduce"]);
                    info.state = HJConvert.ToString(dr["state"]);
                    info.title = HJConvert.ToString(dr["title"]);
                    info.Width = HJConvert.ToInt32(dr["Width"]);
                    info.Height = HJConvert.ToInt32(dr["Height"]);
                    info.isLink = HJConvert.ToInt32(dr["isLink"]);
                    info.cityid = HJConvert.ToInt32(dr["cityid"]);
                    info.stype = HJConvert.ToInt32(dr["stype"]);
                    info.sortid = HJConvert.ToInt32(dr["sortid"]);
                    info.Servicefee = HJConvert.ToDecimal(dr["Servicefee"]);
                    info.AdStartDate = HJConvert.ToDateTime(dr["AdStartDate"]);
                    info.AdEndDate = HJConvert.ToDateTime(dr["AdEndDate"]);
                    info.defautpic = HJConvert.ToString(dr["defautpic"]);

                    infos.Add(info);
                }
            }
            return infos;
        }

    }
}

