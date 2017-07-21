using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;

using System.Data.SqlClient;

namespace Hangjing.SQLServerDAL
{
    public class OldUser
    {
        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>Name</param>
        /// <returns>OldUserInfo</returns>
        public OldUserInfo GetModel(int Name)
        {
            string sql = "select uid Name,pass,Mail,UserName,UserPhone,UserBudding,Address from OldUser";
            SqlParameter parameter = new SqlParameter("@Name", SqlDbType.VarChar, 50);
            parameter.Value = Name;
            OldUserInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new OldUserInfo();
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.pass = HJConvert.ToString(dr["pass"]);
                    model.Mail = HJConvert.ToString(dr["Mail"]);
                    model.UserName = HJConvert.ToString(dr["UserName"]);
                    model.UserPhone = HJConvert.ToString(dr["UserPhone"]);
                    model.UserBudding = HJConvert.ToString(dr["UserBudding"]);
                    model.Address = HJConvert.ToString(dr["Address"]);
                    model.DataId = HJConvert.ToInt32(dr["uid"]);
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
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "users left join user_info on users.uid = user_info.userid"), new SqlParameter("@strWhere", strWhere)));
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
        public IList<OldUserInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<OldUserInfo> infos = new List<OldUserInfo>();
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
            parameters[0].Value = "users left join user_info on users.uid = user_info.userid";//
            parameters[1].Value = "users.uid,users.name,users.pass,users.mail,user_info.user_name,user_info.user_phone,user_info.user_mobile,user_info.user_budding,user_info.address_detail";
            parameters[2].Value = "uid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    OldUserInfo info = new OldUserInfo();
                    info.Name = HJConvert.ToString(dr["name"]);
                    info.pass = HJConvert.ToString(dr["pass"]);
                    info.Mail = HJConvert.ToString(dr["mail"]);
                    info.UserName = HJConvert.ToString(dr["user_name"]);
                    info.UserPhone = HJConvert.ToString(dr["user_mobile"]);
                    info.UserTell = HJConvert.ToString(dr["user_phone"]);
                    info.UserBudding = HJConvert.ToString(dr["user_budding"]);
                    info.Address = HJConvert.ToString(dr["address_detail"]);
                    info.DataId = HJConvert.ToInt32(dr["uid"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

    }
}
