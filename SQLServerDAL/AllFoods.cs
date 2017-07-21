
// allfoods.cs : 菜名库
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-04-28
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
    public class AllFoods
    {
        public int Add(AllFoodsInfo model)
        {
            string sql = "insert into AllFoods(foodname) values (@foodname)";
            SqlParameter[] parameters = 
            {
                new SqlParameter("@foodname" , SqlDbType.VarChar , 50)
            };
            parameters[0].Value = model.Foodname;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, parameters);
        }

        public int IsExcit(string foodname)
        {
            string sql = "select count(*) from allfoods where foodname = @foodname";
            SqlParameter[] parameters = 
            {
                new SqlParameter("@foodname" , SqlDbType.VarChar , 50)
            };
            parameters[0].Value = foodname;

            return (int)SQLHelper.ExecuteScalar(CommandType.Text, sql, parameters);
        }

        /// <summary>
        /// 取得记录数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetCount(string strWhere)
        {
            SqlParameter[] Para = 
            {
                new SqlParameter("@tblName",SqlDbType.VarChar,255),
                new SqlParameter("@strWhere",SqlDbType.VarChar,1500)
            };
            Para[0].Value = "AllFoods";
            Para[1].Value = strWhere;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", Para));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<AllFoodsInfo> GetList(int pageSize, int pageIndex, string where, string orderField, int orderType)
        {
            IList<AllFoodsInfo> DataList = new List<AllFoodsInfo>();

            StringBuilder str = new StringBuilder();
            str.Append("FoodName");
           
            SqlParameter[] Para = 
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
            Para[0].Value = "AllFoods";
            Para[1].Value = str.ToString();
            Para[2].Value = "DataId";
            Para[3].Value = orderField;
            Para[4].Value = pageSize;
            Para[5].Value = pageIndex;
            Para[6].Value = 1;
            Para[7].Value = where;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", Para))
            {
                while (dr.Read())
                {
                    AllFoodsInfo model = new AllFoodsInfo();
                    model.Foodname = HJConvert.ToString(dr["FoodName"]);
                    DataList.Add(model);
                }
            }
            return DataList;
        }

    }
}
