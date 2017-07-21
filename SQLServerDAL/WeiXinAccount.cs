using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Hangjing.DBUtility;
using Hangjing.Model;

namespace Hangjing.SQLServerDAL
{
    ///<summary>
    ///平台微信号信息
    ///<summary>
    public partial class WeiXinAccount
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(WeiXinAccountInfo model)
        {
            SqlParameter[] parameters = 
			{
			    new SqlParameter("@shopid", SqlDbType.Int,4) ,            
                new SqlParameter("@wxusername", SqlDbType.VarChar,256) ,            
                new SqlParameter("@wxpwd", SqlDbType.VarChar,256) ,            
                new SqlParameter("@AppId", SqlDbType.VarChar,256) ,            
                new SqlParameter("@AppSecret", SqlDbType.VarChar,256) ,            
                new SqlParameter("@reveint1", SqlDbType.Int,4) ,            
                new SqlParameter("@reveint2", SqlDbType.Int,4) ,            
                new SqlParameter("@revevar1", SqlDbType.VarChar,8000) ,            
                new SqlParameter("@revevar2", SqlDbType.VarChar,256),
                new SqlParameter("@partnerid", SqlDbType.VarChar,256) ,            
                new SqlParameter("@apikey", SqlDbType.VarChar,256) ,            
                new SqlParameter("@revevar3", SqlDbType.VarChar,256) ,            
                new SqlParameter("@revevar4", SqlDbType.VarChar,256)           
            };

            parameters[0].Value = model.shopid;
            parameters[1].Value = model.wxusername;
            parameters[2].Value = model.wxpwd;
            parameters[3].Value = model.AppId;
            parameters[4].Value = model.AppSecret;
            parameters[5].Value = model.reveint1;
            parameters[6].Value = model.reveint2;
            parameters[7].Value = model.revevar1;
            parameters[8].Value = model.revevar2;
            parameters[9].Value = model.partnerid;
            parameters[10].Value = model.apikey;
            parameters[11].Value = model.revevar3;
            parameters[12].Value = model.revevar4;        

            HJConvert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "WeiXinAccount_ADD", parameters));

            return 1;
        }


        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>dataId</param>
        /// <returns>WeiXinAccountInfo</returns>
        public WeiXinAccountInfo GetModel(int dataId)
        {
            string sql = "select dataId,shopid,wxusername,wxpwd,AppId,AppSecret,reveint1,reveint2,revevar1,revevar2,partnerid,apikey,revevar3,revevar4 from WeiXinAccount where  dataId = @dataId";
            SqlParameter parameter = new SqlParameter("@dataId", SqlDbType.Int, 4);
            parameter.Value = dataId;
            WeiXinAccountInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new WeiXinAccountInfo();
                    model.dataId = HJConvert.ToInt32(dr["dataId"]);
                    model.shopid = HJConvert.ToInt32(dr["shopid"]);
                    model.wxusername = HJConvert.ToString(dr["wxusername"]);
                    model.wxpwd = HJConvert.ToString(dr["wxpwd"]);
                    model.AppId = HJConvert.ToString(dr["AppId"]);
                    model.AppSecret = HJConvert.ToString(dr["AppSecret"]);
                    model.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    model.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    model.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    model.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    model.partnerid = HJConvert.ToString(dr["partnerid"]);
                    model.apikey = HJConvert.ToString(dr["apikey"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "WeiXinAccount"), new SqlParameter("@strWhere", strWhere));
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
        public IList<WeiXinAccountInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<WeiXinAccountInfo> infos = new List<WeiXinAccountInfo>();
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
            parameters[0].Value = "WeiXinAccount";
            parameters[1].Value = "dataId,shopid,wxusername,wxpwd,AppId,AppSecret,reveint1,reveint2,revevar1,revevar2,partnerid,apikey,revevar3,revevar4";
            parameters[2].Value = "dataId";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    WeiXinAccountInfo info = new WeiXinAccountInfo();
                    info.dataId = HJConvert.ToInt32(dr["dataId"]);
                    info.shopid = HJConvert.ToInt32(dr["shopid"]);
                    info.wxusername = HJConvert.ToString(dr["wxusername"]);
                    info.wxpwd = HJConvert.ToString(dr["wxpwd"]);
                    info.AppId = HJConvert.ToString(dr["AppId"]);
                    info.AppSecret = HJConvert.ToString(dr["AppSecret"]);
                    info.reveint1 = HJConvert.ToInt32(dr["reveint1"]);
                    info.reveint2 = HJConvert.ToInt32(dr["reveint2"]);
                    info.revevar1 = HJConvert.ToString(dr["revevar1"]);
                    info.revevar2 = HJConvert.ToString(dr["revevar2"]);
                    info.partnerid = HJConvert.ToString(dr["partnerid"]);
                    info.apikey = HJConvert.ToString(dr["apikey"]);
                    info.revevar3 = HJConvert.ToString(dr["revevar3"]);
                    info.revevar4 = HJConvert.ToString(dr["revevar4"]);
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
        public int DelWeiXinAccount(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from WeiXinAccount where dataId=@dataId");
            SqlParameter[] Para = 
			{
				new SqlParameter("@dataId",SqlDbType.Int)
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
            str.Append("delete from WeiXinAccount where dataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

    }
}

