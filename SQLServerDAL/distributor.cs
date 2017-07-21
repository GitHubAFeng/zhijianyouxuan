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
    /// 用户分销关系表
    /// </summary>
    public partial class distributor
    {
        /// <summary>
        /// 根据openid获取用户 1，2，3级用户编号
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public distributorInfo GetSuperiors(string openid)
        {
            string sql = "SELECT a.Id, nickname,puserid AS onegradeID, onegradeID AS  twogradeID, twogradeID AS thressgradeID FROM subscribeByUserQRcodeRecord AS a LEFT JOIN distributor AS b ON a.puserid = b.userid WHERE openid = '" + openid+"'";
            distributorInfo  model = new distributorInfo();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (dr.Read())
                {
                    model.dId = HJConvert.ToInt32(dr["Id"]);
                    model.onegradeID = HJConvert.ToInt32(dr["onegradeID"]);
                    model.twogradeID = HJConvert.ToInt32(dr["twogradeID"]);
                    model.thressgradeID = HJConvert.ToInt32(dr["thressgradeID"]);
                    model.revevar4 = HJConvert.ToString(dr["nickname"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 根据用户编号获取用户 1，2，3级用户编号，及openid
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public distributorInfo GetSuperiors(int userid)
        {
            SqlParameter parameter = new SqlParameter("@userid", SqlDbType.Int, 4);
            parameter.Value = userid;

       
            distributorInfo model = new distributorInfo();
            model.oneName = "";
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "distributor_getAllSuperiors", parameter))
            {
                if (dr.Read())
                {
                    model.dId = HJConvert.ToInt32(dr["dId"]);
                    model.onegradeID = HJConvert.ToInt32(dr["onegradeID"]);
                    model.twogradeID = HJConvert.ToInt32(dr["twogradeID"]);
                    model.thressgradeID = HJConvert.ToInt32(dr["thressgradeID"]);

                    model.oneopenid = HJConvert.ToString(dr["oneopenid"]);
                    model.twoopenid = HJConvert.ToString(dr["twoopenid"]);
                    model.thressopenid = HJConvert.ToString(dr["thressopenid"]);

                    model.oneName = HJConvert.ToString(dr["oneName"]);
                    model.twoName = HJConvert.ToString(dr["twoName"]);
                    model.thressName = HJConvert.ToString(dr["thressName"]);

                    model.onestate = HJConvert.ToInt32(dr["onestate"]);
                    model.twostate = HJConvert.ToInt32(dr["twostate"]);
                    model.thressstate = HJConvert.ToInt32(dr["thressstate"]);

                }
            }
            return model;
        }

        /// <summary>
        /// 获取各级下线人数,注，可能只有一级分类，或者只有1，2级下线
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public IList<ShopDataInfo> getChildCount(int userid)
        {
            SqlParameter parameter = new SqlParameter("@userid", SqlDbType.Int, 4);
            parameter.Value = userid;
            IList<ShopDataInfo> childlist = new List<ShopDataInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "distributor_getChildCount", parameter))
            {
                while (dr.Read())
                {
                    ShopDataInfo model = new ShopDataInfo();
                    model.ID = HJConvert.ToInt32(dr["childcount"]);
                    childlist.Add(model);

                }
            }
            return childlist;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(distributorInfo model)
        {
            SqlParameter[] parameters =
            {
               new SqlParameter("@userid", SqlDbType.Int,4) ,
               new SqlParameter("@onegradeID", SqlDbType.Int,4) ,
               new SqlParameter("@twogradeID", SqlDbType.Int,4) ,
               new SqlParameter("@thressgradeID", SqlDbType.Int,4) ,
               new SqlParameter("@reveint1", SqlDbType.Int,4) ,
               new SqlParameter("@reveint2", SqlDbType.Int,4) ,
               new SqlParameter("@reveint3", SqlDbType.Int,4) ,
               new SqlParameter("@reveint4", SqlDbType.Int,4) ,
               new SqlParameter("@revevar1", SqlDbType.VarChar,256) ,
               new SqlParameter("@revevar2", SqlDbType.VarChar,256) ,
               new SqlParameter("@revevar3", SqlDbType.VarChar,256) ,
               new SqlParameter("@revevar4", SqlDbType.VarChar,4000)

            };

            parameters[0].Value = model.userid;
            parameters[1].Value = model.onegradeID;
            parameters[2].Value = model.twogradeID;
            parameters[3].Value = model.thressgradeID;
            parameters[4].Value = model.reveint1;
            parameters[5].Value = model.reveint2;
            parameters[6].Value = model.reveint3;
            parameters[7].Value = model.reveint4;
            parameters[8].Value = model.revevar1;
            parameters[9].Value = model.revevar2;
            parameters[10].Value = model.revevar3;
            parameters[11].Value = model.revevar4;

            SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "distributor_ADD", parameters);

            return 1;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(distributorInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update distributor set ");
            strSql.Append(" userid = @userid , ");
            strSql.Append(" onegradeID = @onegradeID , ");
            strSql.Append(" twogradeID = @twogradeID , ");
            strSql.Append(" thressgradeID = @thressgradeID , ");
            strSql.Append(" reveint1 = @reveint1 , ");
            strSql.Append(" reveint2 = @reveint2 , ");
            strSql.Append(" reveint3 = @reveint3 , ");
            strSql.Append(" reveint4 = @reveint4 , ");
            strSql.Append(" revevar1 = @revevar1 , ");
            strSql.Append(" revevar2 = @revevar2 , ");
            strSql.Append(" revevar3 = @revevar3 , ");
            strSql.Append(" revevar4 = @revevar4  ");
            strSql.Append(" where dId=@dId ");

            SqlParameter[] parameters =
            {
                 new SqlParameter("@dId", SqlDbType.Int,4) ,
                 new SqlParameter("@userid", SqlDbType.Int,4) ,
                 new SqlParameter("@onegradeID", SqlDbType.Int,4) ,
                 new SqlParameter("@twogradeID", SqlDbType.Int,4) ,
                 new SqlParameter("@thressgradeID", SqlDbType.Int,4) ,
                 new SqlParameter("@reveint1", SqlDbType.Int,4) ,
                 new SqlParameter("@reveint2", SqlDbType.Int,4) ,
                 new SqlParameter("@reveint3", SqlDbType.Int,4) ,
                 new SqlParameter("@reveint4", SqlDbType.Int,4) ,
                 new SqlParameter("@revevar1", SqlDbType.VarChar,256) ,
                 new SqlParameter("@revevar2", SqlDbType.VarChar,256) ,
                 new SqlParameter("@revevar3", SqlDbType.VarChar,256) ,
                 new SqlParameter("@revevar4", SqlDbType.VarChar,4000)

            };

            parameters[0].Value = model.dId;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.onegradeID;
            parameters[3].Value = model.twogradeID;
            parameters[4].Value = model.thressgradeID;
            parameters[5].Value = model.reveint1;
            parameters[6].Value = model.reveint2;
            parameters[7].Value = model.reveint3;
            parameters[8].Value = model.reveint4;
            parameters[9].Value = model.revevar1;
            parameters[10].Value = model.revevar2;
            parameters[11].Value = model.revevar3;
            parameters[12].Value = model.revevar4;
            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>dId</param>
        /// <returns>distributorInfo</returns>
        public distributorInfo GetModel(int dId)
        {
            string sql = "select dId,userid,onegradeID,twogradeID,thressgradeID,reveint1,reveint2,reveint3,reveint4,revevar1,revevar2,revevar3,revevar4 from distributor where  dId = @dId";
            SqlParameter parameter = new SqlParameter("@dId", SqlDbType.Int, 4);
            parameter.Value = dId;
            distributorInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new distributorInfo();
                    model.dId = HJConvert.ToInt32(dr["dId"]);
                    model.userid = HJConvert.ToInt32(dr["userid"]);
                    model.onegradeID = HJConvert.ToInt32(dr["onegradeID"]);
                    model.twogradeID = HJConvert.ToInt32(dr["twogradeID"]);
                    model.thressgradeID = HJConvert.ToInt32(dr["thressgradeID"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.reveint3 = HJConvert.ToInt32(dr["reveint3"]);
                    model.reveint4 = HJConvert.ToInt32(dr["reveint4"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    model.revevar3 = HJConvert.ToString(dr["revevar3"]);
                    model.revevar4 = HJConvert.ToString(dr["revevar4"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "distributor"), new SqlParameter("@strWhere", strWhere));
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
        public IList<distributorInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<distributorInfo> infos = new List<distributorInfo>();
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
            parameters[0].Value = "distributor";
            parameters[1].Value = "dId,userid,onegradeID,twogradeID,thressgradeID,reveint1,reveint2,reveint3,reveint4,revevar1,revevar2,revevar3,revevar4";
            parameters[2].Value = "dId";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    distributorInfo model = new distributorInfo();
                    model.dId = HJConvert.ToInt32(dr["dId"]);
                    model.userid = HJConvert.ToInt32(dr["userid"]);
                    model.onegradeID = HJConvert.ToInt32(dr["onegradeID"]);
                    model.twogradeID = HJConvert.ToInt32(dr["twogradeID"]);
                    model.thressgradeID = HJConvert.ToInt32(dr["thressgradeID"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.reveint3 = HJConvert.ToInt32(dr["reveint3"]);
                    model.reveint4 = HJConvert.ToInt32(dr["reveint4"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    model.revevar3 = HJConvert.ToString(dr["revevar3"]);
                    model.revevar4 = HJConvert.ToString(dr["revevar4"]);
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
        public int Deldistributor(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from distributor where dId=@dId");
            SqlParameter[] Para =
                {
                new SqlParameter("@dId",SqlDbType.Int)
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
            str.Append("delete from distributor where dId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }
    }

}

