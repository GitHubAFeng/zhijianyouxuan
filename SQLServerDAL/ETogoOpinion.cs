using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;

using System.Data.SqlClient;

namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 数据访问类ETogoOpinion。
    /// </summary>
    public class ETogoOpinion
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ETogoOpinionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ETogoOpinion(");
            strSql.Append("UserID,TogoID,Point,Comment,PostTime,ServiceGrade,FlavorGrade,SpeedGrade,username,Rtime,Rcontent,rtype)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@TogoID,@Point,@Comment,@PostTime,@ServiceGrade,@FlavorGrade,@SpeedGrade,@username,@Rtime,@Rcontent,@rtype)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@UserID", SqlDbType.Int,4),
				new SqlParameter("@TogoID", SqlDbType.VarChar,20),
				new SqlParameter("@Point", SqlDbType.Int,4),
				new SqlParameter("@Comment", SqlDbType.VarChar,2000),
				new SqlParameter("@PostTime", SqlDbType.DateTime),
				new SqlParameter("@ServiceGrade", SqlDbType.Int,4),
				new SqlParameter("@FlavorGrade", SqlDbType.Int,4),
				new SqlParameter("@SpeedGrade", SqlDbType.Int,4),
				new SqlParameter("@username", SqlDbType.VarChar,256),
				new SqlParameter("@Rtime", SqlDbType.DateTime),
				new SqlParameter("@Rcontent", SqlDbType.Text),
				new SqlParameter("@rtype", SqlDbType.Int,4)
            };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.TogoID+"";
            parameters[2].Value = model.Point;
            parameters[3].Value = model.Comment;
            parameters[4].Value = model.PostTime;
            parameters[5].Value = model.ServiceGrade;
            parameters[6].Value = model.FlavorGrade;
            parameters[7].Value = model.SpeedGrade;
            parameters[8].Value = model.UserName;
            parameters[9].Value = model.Rtime;
            parameters[10].Value = model.Rcontent;
            parameters[11].Value = model.Rtype;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ETogoOpinionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ETogoOpinion set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("TogoID=@TogoID,");
            strSql.Append("Point=@Point,");
            strSql.Append("Comment=@Comment,");
            strSql.Append("PostTime=@PostTime,");
            strSql.Append("ServiceGrade=@ServiceGrade,");
            strSql.Append("FlavorGrade=@FlavorGrade,");
            strSql.Append("SpeedGrade=@SpeedGrade,");
            strSql.Append("username=@username,");
            strSql.Append("Rtime=@Rtime,");
            strSql.Append("Rcontent=@Rcontent,");
            strSql.Append("rtype=@rtype");
            strSql.Append(" where DataID=@DataID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataID", SqlDbType.Int,4),
				new SqlParameter("@UserID", SqlDbType.Int,4),
				new SqlParameter("@TogoID", SqlDbType.VarChar,20),
				new SqlParameter("@Point", SqlDbType.Int,4),
				new SqlParameter("@Comment", SqlDbType.VarChar,2000),
				new SqlParameter("@PostTime", SqlDbType.DateTime),
				new SqlParameter("@ServiceGrade", SqlDbType.Int,4),
				new SqlParameter("@FlavorGrade", SqlDbType.Int,4),
				new SqlParameter("@SpeedGrade", SqlDbType.Int,4),
				new SqlParameter("@username", SqlDbType.VarChar,256),
				new SqlParameter("@Rtime", SqlDbType.DateTime),
				new SqlParameter("@Rcontent", SqlDbType.Text),
				new SqlParameter("@rtype", SqlDbType.Int,4)
            };
            parameters[0].Value = model.DataID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.TogoID+"";
            parameters[3].Value = model.Point;
            parameters[4].Value = model.Comment;
            parameters[5].Value = model.PostTime;
            parameters[6].Value = model.ServiceGrade;
            parameters[7].Value = model.FlavorGrade;
            parameters[8].Value = model.SpeedGrade;
            parameters[9].Value = model.UserName;
            parameters[10].Value = model.Rtime;
            parameters[11].Value = model.Rcontent;
            parameters[12].Value = model.Rtype;


            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataID</param>
        /// <returns>ETogoOpinionInfo</returns>
        public ETogoOpinionInfo GetModel(int DataID)
        {
            string field = "select DataID,UserID,TogoID,Point,Comment,PostTime,ServiceGrade,FlavorGrade,SpeedGrade";
            field += ", (select name from points where unid = ETogoOpinion.togoid) as togoname , username,Rtime,Rcontent,rtype";
            field += ",(select Picture from ecustomer where dataid = ETogoOpinion.userid) as Picture from ETogoOpinion where DataID=@nDataID";

            SqlParameter parameter = new SqlParameter("@nDataID", SqlDbType.Int, 4);
            parameter.Value = DataID;
            ETogoOpinionInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, field, parameter))
            {
                if (dr.Read())
                {
                    model = new ETogoOpinionInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.UserID = HJConvert.ToInt32(dr["UserID"]);
                    model.TogoID = HJConvert.ToInt32(dr["TogoID"]);
                    model.Point = HJConvert.ToInt32(dr["Point"]);
                    model.Comment = HJConvert.ToString(dr["Comment"]);
                    model.PostTime = HJConvert.ToDateTime(dr["PostTime"]);
                    model.ServiceGrade = HJConvert.ToInt32(dr["ServiceGrade"]);
                    model.FlavorGrade = HJConvert.ToInt32(dr["FlavorGrade"]);
                    model.SpeedGrade = HJConvert.ToInt32(dr["SpeedGrade"]);
                    model.UserName = HJConvert.ToString(dr["username"]);
                    model.Rtime = HJConvert.ToDateTime(dr["Rtime"]);
                    model.Rcontent = HJConvert.ToString(dr["Rcontent"]);
                    model.Rtype = HJConvert.ToInt32(dr["rtype"]);
                    model.TogoName = HJConvert.ToString(dr["togoname"]);
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
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "ETogoOpinion"), new SqlParameter("@strWhere", strWhere)));
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
        public IList<ETogoOpinionInfo> GetOpinion(int top)
        {
            IList<ETogoOpinionInfo> infos = new List<ETogoOpinionInfo>();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, "select top(" + top + ") (select Name from ECustomer where DataID=ETogoOpinion.UserID)as username,(select TrueName from ECustomer where DataID=ETogoOpinion.UserID)as TrueName,(select name from points where unid = ETogoOpinion.togoid) as togoname,togoid,PostTime,Comment from ETogoOpinion  order by DataID desc", null))
            {
                while (dr.Read())
                {
                    ETogoOpinionInfo info = new ETogoOpinionInfo();
                    info.TogoID = HJConvert.ToInt32(dr["togoid"]);
                    info.UserName = HJConvert.ToString(dr["username"]);
                    info.TrueName = HJConvert.ToString(dr["TrueName"]);
                    info.Comment = HJConvert.ToString(dr["Comment"]);
                    info.PostTime = HJConvert.ToDateTime(dr["PostTime"]);
                    info.TogoName = HJConvert.ToString(dr["togoname"]);
                   
                  
                    infos.Add(info);
                }
            }
            return infos;
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
        public IList<ETogoOpinionInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ETogoOpinionInfo> infos = new List<ETogoOpinionInfo>();
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
            parameters[0].Value = "ETogoOpinion";
            string field = "DataID,UserID,TogoID,Point,Comment,PostTime,ServiceGrade,FlavorGrade,SpeedGrade";
            field += ", (select name from points where unid = ETogoOpinion.togoid) as togoname , username,Rtime,Rcontent,rtype";
            field += ",(select Picture from ecustomer where dataid = ETogoOpinion.userid) as Picture";
            parameters[1].Value = field;
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
                    ETogoOpinionInfo info = new ETogoOpinionInfo();
                    info.DataID = HJConvert.ToInt32(dr["DataID"]);
                    info.UserID = HJConvert.ToInt32(dr["UserID"]);
                    info.TogoID = HJConvert.ToInt32(dr["TogoID"]);
                    info.Point = HJConvert.ToInt32(dr["Point"]);
                    info.Comment = HJConvert.ToString(dr["Comment"]);
                    info.PostTime = HJConvert.ToDateTime(dr["PostTime"]);
                    info.ServiceGrade = HJConvert.ToInt32(dr["ServiceGrade"]);
                    info.FlavorGrade = HJConvert.ToInt32(dr["FlavorGrade"]);
                    info.SpeedGrade = HJConvert.ToInt32(dr["SpeedGrade"]);
                    info.TogoName = HJConvert.ToString(dr["togoname"]);
                    info.UserName = HJConvert.ToString(dr["username"]);
                    info.Rtime = HJConvert.ToDateTime(dr["Rtime"]);
                    info.Rcontent = HJConvert.ToString(dr["Rcontent"]);
                    info.Rtype= HJConvert.ToInt32(dr["rtype"]);
                    info.picture = HJConvert.ToString(dr["picture"]);
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
        public int DelETogoOpinion(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ETogoOpinion where DataID=@DataID");
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
            str.Append("delete from ETogoOpinion where DataID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 获取评论的平均分 type = 1; 速度 type =2 , 服务 type = 3 ;口味 type = 4
        /// </summary>
        /// <param name="marketid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int getAverage(int marketid, int type)
        {
            string sql = "";
            switch (type)
            {
                case 1:
                    sql = "SELECT AVG(point) AS point FROM ETogoOpinion where togoid = @marketid";
                    break;
                case 2:
                    sql = "SELECT AVG(SpeedGrade) AS point FROM ETogoOpinion where togoid = @marketid";
                    break;
                case 3:
                    sql = "SELECT AVG(ServiceGrade) AS point FROM ETogoOpinion where togoid = @marketid";
                    break;
                case 4:
                    sql = "SELECT AVG(FlavorGrade) AS point FROM ETogoOpinion where togoid = @marketid";
                    break;
            }
            SqlParameter[] parameter =
            {
                new SqlParameter("@marketid" , SqlDbType.Int , 4)
            };
            parameter[0].Value = marketid;

            return SQLHelper.ExecuteScalar(CommandType.Text, sql, parameter) == DBNull.Value ? 0 : Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, sql, parameter));
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
        public IList<ETogoOpinionInfo> GetList_nopic(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ETogoOpinionInfo> infos = new List<ETogoOpinionInfo>();
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
            parameters[0].Value = "ETogoOpinion";
            string field = "DataID,UserID,TogoID,Point,Comment,PostTime,ServiceGrade,FlavorGrade,SpeedGrade";
            field += ", (select name from points where unid = ETogoOpinion.togoid) as togoname , username,Rtime,Rcontent,rtype";
            parameters[1].Value = field;
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
                    ETogoOpinionInfo info = new ETogoOpinionInfo();
                    info.DataID = HJConvert.ToInt32(dr["DataID"]);
                    info.UserID = HJConvert.ToInt32(dr["UserID"]);
                    info.TogoID = HJConvert.ToInt32(dr["TogoID"]);
                    info.Point = HJConvert.ToInt32(dr["Point"]);
                    info.Comment = HJConvert.ToString(dr["Comment"]);
                    info.PostTime = HJConvert.ToDateTime(dr["PostTime"]);
                    info.ServiceGrade = HJConvert.ToInt32(dr["ServiceGrade"]);
                    info.FlavorGrade = HJConvert.ToInt32(dr["FlavorGrade"]);
                    info.SpeedGrade = HJConvert.ToInt32(dr["SpeedGrade"]);
                    info.TogoName = HJConvert.ToString(dr["togoname"]);
                    info.UserName = HJConvert.ToString(dr["username"]);
                    info.Rtime = HJConvert.ToDateTime(dr["Rtime"]);
                    info.Rcontent = HJConvert.ToString(dr["Rcontent"]);
                    info.Rtype = HJConvert.ToInt32(dr["rtype"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 用户评论时同步更新商家数据
        /// </summary>
        /// <param name="shopid">商家编号</param>
        /// <returns></returns>
        public void setReviewData(int shopid)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@shopid" , SqlDbType.Int , 4)
            };
            parameter[0].Value = shopid;

            SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "ETogo_setReviewData", parameter);
        }

    }
}

