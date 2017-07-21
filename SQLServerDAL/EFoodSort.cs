// EFoodSort：菜品类别数据访问实体
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// add by yangxiaolong@ihangjing.com
// 2010-03-13

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
    public class EFoodSort
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(EFoodSortInfo Model)
        {
            StringBuilder str = new StringBuilder();
            str.Append("insert into EFoodSort(");
            str.Append("SortName,togonum , jorder)");
            str.Append(" values (");
            str.Append("@SortName,@TogoID , @jorder)");
            str.Append(";select @@IDENTITY");
            SqlParameter[] Para = 
            {
				new SqlParameter("@SortName", SqlDbType.VarChar,50),
				new SqlParameter("@TogoID", SqlDbType.Int,4),
                new SqlParameter("@jorder" , SqlDbType.Int ,4)
            };
            Para[0].Value = Model.SortName;
            Para[1].Value = Model.TogoNum;
            Para[2].Value = Model.Jorder;

            return HJConvert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, str.ToString(), Para));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sortid"></param>
        /// <returns></returns>
        public int Delete(int SortId)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from EFoodSort where SortID=@SortID");

            SqlParameter[] Para = 
            {
                new SqlParameter("@SortID",SqlDbType.Int,4)
            };
            Para[0].Value = SortId;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        public int DelList(string IdList)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from EFoodSort where SortID in (" + IdList + ")");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), null);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(EFoodSortInfo model)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update EFoodSort set ");
            str.Append("SortName=@SortName,");
            str.Append("TogoNum=@TogoNum , ");
            str.Append("jorder = @jorder ");
            str.Append(" where SortID=@SortID");

            SqlParameter[] Para = 
            {
                new SqlParameter("@SortID",SqlDbType.Int,4),
                new SqlParameter("@SortName",SqlDbType.VarChar,50),
                new SqlParameter("@TogoNum",SqlDbType.Int,4),
                new SqlParameter("@jorder" , SqlDbType.Int , 4)
            };
            Para[0].Value = model.SortID;
            Para[1].Value = model.SortName;
            Para[2].Value = model.TogoNum;
            Para[3].Value = model.Jorder;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 获取一条类别数据
        /// </summary>
        /// <param name="sortid"></param>
        /// <returns></returns>
        public EFoodSortInfo GetModel(int sortid)
        {
            SqlParameter[] Para = 
            {
                new SqlParameter("@SortID",SqlDbType.Int,4)
            };
            Para[0].Value = sortid;

            EFoodSortInfo model = null;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, "select *,(select Name from Points where Unid=EFoodSort.TogoNum) as TogoName from EFoodSort where SortID=@SortID", Para))
            {
                if (dr.Read())
                {
                    model = new EFoodSortInfo();
                    model.SortID = HJConvert.ToInt32(dr["SortID"]);
                    model.SortName = HJConvert.ToString(dr["SortName"]);
                    model.TogoNum = HJConvert.ToInt32(dr["TogoNum"]);
                    model.TogoName = HJConvert.ToString(dr["TogoName"]);
                    model.Jorder = HJConvert.ToInt32(dr["jorder"]);
                }
            }
            return model;
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
            Para[0].Value = "EFoodSort";
            Para[1].Value = strWhere;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", Para));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<EFoodSortInfo> GetList(int pageSize, int pageIndex, string where, string orderField, int orderType)
        {
            IList<EFoodSortInfo> DataList = new List<EFoodSortInfo>();

            SqlParameter[] Para = 
            {
				new SqlParameter("@tblName", SqlDbType.VarChar,255),
				new SqlParameter("@strGetFields", SqlDbType.VarChar,1000),
				new SqlParameter("@primary", SqlDbType.VarChar,255),
                new SqlParameter("@orderName",SqlDbType.VarChar,255),
				new SqlParameter("@PageSize", SqlDbType.Int),					
				new SqlParameter("@PageIndex", SqlDbType.Int),
				new SqlParameter("@OrderType", SqlDbType.Bit),
				new SqlParameter("@strWhere", SqlDbType.VarChar,1500)
            };

            Para[0].Value = "EFoodSort";
            Para[1].Value = "SortID,SortName,TogoNum,(select Name from points where unid=EFoodSort.TogoNum) TogoName , jorder";
            Para[2].Value = "SortID";
            Para[3].Value = orderField;
            Para[4].Value = pageSize;
            Para[5].Value = pageIndex;
            Para[6].Value = 1;
            Para[7].Value = where;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", Para))
            {
                while (dr.Read())
                {
                    EFoodSortInfo model = new EFoodSortInfo();
                    model.SortID = HJConvert.ToInt32(dr["SortID"]);
                    model.SortName = HJConvert.ToString(dr["SortName"]);
                    model.TogoNum = HJConvert.ToInt32(dr["TogoNum"]);
                    model.TogoName = HJConvert.ToString(dr["TogoName"]);
                    model.Jorder = HJConvert.ToInt32(dr["jorder"]);
                    DataList.Add(model);
                }
            }
            return DataList;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public IList<EFoodSortInfo> GetAll()
        {
            IList<EFoodSortInfo> DataList = new List<EFoodSortInfo>();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "GetAllFoodSort", null))
            {
                while (dr.Read())
                {
                    EFoodSortInfo model = new EFoodSortInfo();
                    model.SortID = HJConvert.ToInt32(dr["SortID"]);
                    model.SortName = HJConvert.ToString(dr["SortName"]);
                    model.TogoNum = HJConvert.ToInt32(dr["TogoNum"]);
                    DataList.Add(model);
                }
            }
            return DataList;
        }

        /// <summary>
        /// 获取某个商家的所有餐品类型
        /// </summary>
        /// <param name="TogoNum"></param>
        /// <returns></returns>
        public IList<EFoodSortInfo> GetListByTogoNum(int TogoNum)
        {
            SqlParameter[] Para = 
            {
                new SqlParameter("@TogoNum",SqlDbType.Int,4)
            };
            Para[0].Value = TogoNum;

            IList<EFoodSortInfo> DataList = new List<EFoodSortInfo>();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "GetFoodSortListByTogoNum", Para))
            {
                while (dr.Read())
                {
                    EFoodSortInfo model = new EFoodSortInfo();
                    model.SortID = HJConvert.ToInt32(dr["SortID"]);
                    model.SortName = HJConvert.ToString(dr["SortName"]);
                    model.TogoNum = HJConvert.ToInt32(dr["TogoNum"]);
                    //model.TogoName = HJConvert.ToString(dr["TogoName"]);
                    model.Jorder = HJConvert.ToInt32(dr["Jorder"]);
                    model.pic = HJConvert.ToString(dr["pic"]);
                    model.intro = HJConvert.ToString(dr["intro"]);

                    DataList.Add(model);
                }
            }
            return DataList;
        }

        /// <summary>
        /// 根据条件获取分类
        /// </summary>
        public IList<EFoodSortInfo> GetList1(string where)
        {
            IList<EFoodSortInfo> DataList = new List<EFoodSortInfo>();
            string sql = "select top 20 * from efoodsort where " + where;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    EFoodSortInfo model = new EFoodSortInfo();
                    model.SortID = HJConvert.ToInt32(dr["SortID"]);
                    model.SortName = HJConvert.ToString(dr["SortName"]);
                    model.Jorder = HJConvert.ToInt32(dr["jorder"]);
                    DataList.Add(model);
                }
            }
            return DataList;
        }
    }
}
