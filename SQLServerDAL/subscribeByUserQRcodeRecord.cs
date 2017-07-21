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
    /// 用户通过代参数的二维码扫描关注记录
    /// </summary>
    public partial class subscribeByUserQRcodeRecord
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(subscribeByUserQRcodeRecordInfo model)
        {
            SqlParameter[] parameters =
            {
               new SqlParameter("@openid", SqlDbType.VarChar,256) ,
               new SqlParameter("@nickname", SqlDbType.VarChar,256) ,
               new SqlParameter("@puserid", SqlDbType.Int,4) ,
               new SqlParameter("@addtime", SqlDbType.DateTime) ,
               new SqlParameter("@reveint1", SqlDbType.Int,4) ,
               new SqlParameter("@reveint2", SqlDbType.Int,4) ,
               new SqlParameter("@revevar1", SqlDbType.VarChar,256) ,
               new SqlParameter("@revevar2", SqlDbType.VarChar,256)
            };

            parameters[0].Value = model.openid;
            parameters[1].Value = model.nickname;
            parameters[2].Value = model.puserid;
            parameters[3].Value = model.addtime;
            parameters[4].Value = model.reveint1;
            parameters[5].Value = model.reveint2;
            parameters[6].Value = model.revevar1;
            parameters[7].Value = model.revevar2;

            SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "subscribeByUserQRcodeRecord_ADD", parameters);

            return 1;

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(subscribeByUserQRcodeRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update subscribeByUserQRcodeRecord set ");
            strSql.Append(" openid = @openid , ");
            strSql.Append(" nickname = @nickname , ");
            strSql.Append(" puserid = @puserid , ");
            strSql.Append(" addtime = @addtime , ");
            strSql.Append(" reveint1 = @reveint1 , ");
            strSql.Append(" reveint2 = @reveint2 , ");
            strSql.Append(" revevar1 = @revevar1 , ");
            strSql.Append(" revevar2 = @revevar2  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters =
            {
                 new SqlParameter("@Id", SqlDbType.Int,4) ,
                 new SqlParameter("@openid", SqlDbType.VarChar,256) ,
                 new SqlParameter("@nickname", SqlDbType.VarChar,256) ,
                 new SqlParameter("@puserid", SqlDbType.Int,4) ,
                 new SqlParameter("@addtime", SqlDbType.DateTime) ,
                 new SqlParameter("@reveint1", SqlDbType.Int,4) ,
                 new SqlParameter("@reveint2", SqlDbType.Int,4) ,
                 new SqlParameter("@revevar1", SqlDbType.VarChar,256) ,
                 new SqlParameter("@revevar2", SqlDbType.VarChar,256)
            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.openid;
            parameters[2].Value = model.nickname;
            parameters[3].Value = model.puserid;
            parameters[4].Value = model.addtime;
            parameters[5].Value = model.reveint1;
            parameters[6].Value = model.reveint2;
            parameters[7].Value = model.revevar1;
            parameters[8].Value = model.revevar2;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);


        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>Id</param>
        /// <returns>subscribeByUserQRcodeRecordInfo</returns>
        public subscribeByUserQRcodeRecordInfo GetModel(int Id)
        {
            string sql = "select Id,openid,nickname,puserid,addtime,reveint1,reveint2,revevar1,revevar2 from subscribeByUserQRcodeRecord where  Id = @Id";
            SqlParameter parameter = new SqlParameter("@Id", SqlDbType.Int, 4);
            parameter.Value = Id;
            subscribeByUserQRcodeRecordInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new subscribeByUserQRcodeRecordInfo();
                    model.Id = HJConvert.ToInt32(dr["Id"]);
                    model.openid = HJConvert.ToString(dr["openid"]);
                    model.nickname = HJConvert.ToString(dr["nickname"]);
                    model.puserid = HJConvert.ToInt32(dr["puserid"]);
                    model.addtime = HJConvert.ToDateTime(dr["addtime"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "subscribeByUserQRcodeRecord"), new SqlParameter("@strWhere", strWhere));
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
        public IList<subscribeByUserQRcodeRecordInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<subscribeByUserQRcodeRecordInfo> infos = new List<subscribeByUserQRcodeRecordInfo>();
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
            parameters[0].Value = "subscribeByUserQRcodeRecord";
            parameters[1].Value = "Id,openid,nickname,puserid,addtime,reveint1,reveint2,revevar1,revevar2";
            parameters[2].Value = "Id";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    subscribeByUserQRcodeRecordInfo model = new subscribeByUserQRcodeRecordInfo();
                    model.Id = HJConvert.ToInt32(dr["Id"]);
                    model.openid = HJConvert.ToString(dr["openid"]);
                    model.nickname = HJConvert.ToString(dr["nickname"]);
                    model.puserid = HJConvert.ToInt32(dr["puserid"]);
                    model.addtime = HJConvert.ToDateTime(dr["addtime"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
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
        public int DelsubscribeByUserQRcodeRecord(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from subscribeByUserQRcodeRecord where Id=@Id");
            SqlParameter[] Para =
                {
                new SqlParameter("@Id",SqlDbType.Int)
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
            str.Append("delete from subscribeByUserQRcodeRecord where Id in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

    }

}

