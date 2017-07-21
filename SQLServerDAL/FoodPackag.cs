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
    /// 套餐类
    /// </summary>
    public partial class FoodPackag
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(FoodPackagInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FoodPackag(");
            strSql.Append("Code,ShopId,foodids,Num,cnum,Price,Unit,Tag,SectionId,Inve1,Inve2,state,title,sortnum,startdate,enddate,starttime,endtime,oldprice,ReveFloat1,ReveFloat2,ReveVar1,ReveVar2");
            strSql.Append(") values (");
            strSql.Append("@Code,@ShopId,@foodids,@Num,@cnum,@Price,@Unit,@Tag,@SectionId,@Inve1,@Inve2,@state,@title,@sortnum,@startdate,@enddate,@starttime,@endtime,@oldprice,@ReveFloat1,@ReveFloat2,@ReveVar1,@ReveVar2");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
			{
			    new SqlParameter("@Code", SqlDbType.VarChar,50) ,            
                new SqlParameter("@ShopId", SqlDbType.Int,4) ,            
                new SqlParameter("@foodids", SqlDbType.VarChar,512) ,            
                new SqlParameter("@Num", SqlDbType.Int,4) ,            
                new SqlParameter("@cnum", SqlDbType.Int,4) ,            
                new SqlParameter("@Price", SqlDbType.Decimal,5) ,            
                new SqlParameter("@Unit", SqlDbType.VarChar,50) ,            
                new SqlParameter("@Tag", SqlDbType.Int,4) ,            
                new SqlParameter("@SectionId", SqlDbType.Int,4) ,            
                new SqlParameter("@Inve1", SqlDbType.Int,4) ,            
                new SqlParameter("@Inve2", SqlDbType.VarChar,256) ,            
                new SqlParameter("@state", SqlDbType.Int,4) ,            
                new SqlParameter("@title", SqlDbType.VarChar,256) ,            
                new SqlParameter("@sortnum", SqlDbType.Int,4) ,            
                new SqlParameter("@startdate", SqlDbType.DateTime) ,            
                new SqlParameter("@enddate", SqlDbType.DateTime) ,            
                new SqlParameter("@starttime", SqlDbType.DateTime) ,            
                new SqlParameter("@endtime", SqlDbType.DateTime) ,            
                new SqlParameter("@oldprice", SqlDbType.Decimal,5) ,            
                new SqlParameter("@ReveFloat1", SqlDbType.Decimal,5) ,            
                new SqlParameter("@ReveFloat2", SqlDbType.Decimal,5) ,            
                new SqlParameter("@ReveVar1", SqlDbType.VarChar,256) ,            
                new SqlParameter("@ReveVar2", SqlDbType.VarChar,256)             
              
            };

            parameters[0].Value = model.Code;
            parameters[1].Value = model.ShopId;
            parameters[2].Value = model.foodids;
            parameters[3].Value = model.Num;
            parameters[4].Value = model.cnum;
            parameters[5].Value = model.Price;
            parameters[6].Value = model.Unit;
            parameters[7].Value = model.Tag;
            parameters[8].Value = model.SectionId;
            parameters[9].Value = model.Inve1;
            parameters[10].Value = model.Inve2;
            parameters[11].Value = model.state;
            parameters[12].Value = model.title;
            parameters[13].Value = model.sortnum;
            parameters[14].Value = model.startdate;
            parameters[15].Value = model.enddate;
            parameters[16].Value = model.starttime;
            parameters[17].Value = model.endtime;
            parameters[18].Value = model.oldprice;
            parameters[19].Value = model.ReveFloat1;
            parameters[20].Value = model.ReveFloat2;
            parameters[21].Value = model.ReveVar1;
            parameters[22].Value = model.ReveVar2;
            return HJConvert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(FoodPackagInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FoodPackag set ");
            strSql.Append(" Code = @Code , ");
            strSql.Append(" ShopId = @ShopId , ");
            strSql.Append(" foodids = @foodids , ");
            strSql.Append(" Num = @Num , ");
            strSql.Append(" cnum = @cnum , ");
            strSql.Append(" Price = @Price , ");
            strSql.Append(" Unit = @Unit , ");
            strSql.Append(" Tag = @Tag , ");
            strSql.Append(" SectionId = @SectionId , ");
            strSql.Append(" Inve1 = @Inve1 , ");
            strSql.Append(" Inve2 = @Inve2 , ");
            strSql.Append(" state = @state , ");
            strSql.Append(" title = @title , ");
            strSql.Append(" sortnum = @sortnum , ");
            strSql.Append(" startdate = @startdate , ");
            strSql.Append(" enddate = @enddate , ");
            strSql.Append(" starttime = @starttime , ");
            strSql.Append(" endtime = @endtime , ");
            strSql.Append(" oldprice = @oldprice , ");
            strSql.Append(" ReveFloat1 = @ReveFloat1 , ");
            strSql.Append(" ReveFloat2 = @ReveFloat2 , ");
            strSql.Append(" ReveVar1 = @ReveVar1 , ");
            strSql.Append(" ReveVar2 = @ReveVar2  ");
            strSql.Append(" where PID=@PID ");

            SqlParameter[] parameters = 
			{
			    new SqlParameter("@PID", SqlDbType.Int,4) ,            
                new SqlParameter("@Code", SqlDbType.VarChar,50) ,            
                new SqlParameter("@ShopId", SqlDbType.Int,4) ,            
                new SqlParameter("@foodids", SqlDbType.VarChar,512) ,            
                new SqlParameter("@Num", SqlDbType.Int,4) ,            
                new SqlParameter("@cnum", SqlDbType.Int,4) ,            
                new SqlParameter("@Price", SqlDbType.Decimal,5) ,            
                new SqlParameter("@Unit", SqlDbType.VarChar,50) ,            
                new SqlParameter("@Tag", SqlDbType.Int,4) ,            
                new SqlParameter("@SectionId", SqlDbType.Int,4) ,            
                new SqlParameter("@Inve1", SqlDbType.Int,4) ,            
                new SqlParameter("@Inve2", SqlDbType.VarChar,256) ,            
                new SqlParameter("@state", SqlDbType.Int,4) ,            
                new SqlParameter("@title", SqlDbType.VarChar,256) ,            
                new SqlParameter("@sortnum", SqlDbType.Int,4) ,            
                new SqlParameter("@startdate", SqlDbType.DateTime) ,            
                new SqlParameter("@enddate", SqlDbType.DateTime) ,            
                new SqlParameter("@starttime", SqlDbType.DateTime) ,            
                new SqlParameter("@endtime", SqlDbType.DateTime) ,            
                new SqlParameter("@oldprice", SqlDbType.Decimal,5) ,            
                new SqlParameter("@ReveFloat1", SqlDbType.Decimal,5) ,            
                new SqlParameter("@ReveFloat2", SqlDbType.Decimal,5) ,            
                new SqlParameter("@ReveVar1", SqlDbType.VarChar,256) ,            
                new SqlParameter("@ReveVar2", SqlDbType.VarChar,256)             
              
            };

            parameters[0].Value = model.PID;
            parameters[1].Value = model.Code;
            parameters[2].Value = model.ShopId;
            parameters[3].Value = model.foodids;
            parameters[4].Value = model.Num;
            parameters[5].Value = model.cnum;
            parameters[6].Value = model.Price;
            parameters[7].Value = model.Unit;
            parameters[8].Value = model.Tag;
            parameters[9].Value = model.SectionId;
            parameters[10].Value = model.Inve1;
            parameters[11].Value = model.Inve2;
            parameters[12].Value = model.state;
            parameters[13].Value = model.title;
            parameters[14].Value = model.sortnum;
            parameters[15].Value = model.startdate;
            parameters[16].Value = model.enddate;
            parameters[17].Value = model.starttime;
            parameters[18].Value = model.endtime;
            parameters[19].Value = model.oldprice;
            parameters[20].Value = model.ReveFloat1;
            parameters[21].Value = model.ReveFloat2;
            parameters[22].Value = model.ReveVar1;
            parameters[23].Value = model.ReveVar2;
            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>PID</param>
        /// <returns>FoodPackagInfo</returns>
        public FoodPackagInfo GetModel(int PID)
        {
            string sql = "select PID,Code,ShopId,foodids,Num,cnum,Price,Unit,Tag,SectionId,Inve1,Inve2,state,title,sortnum,startdate,enddate,starttime,endtime,oldprice,ReveFloat1,ReveFloat2,ReveVar1,ReveVar2 from FoodPackag where  PID = @PID";
            SqlParameter parameter = new SqlParameter("@PID", SqlDbType.Int, 4);
            parameter.Value = PID;
            FoodPackagInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new FoodPackagInfo();
                    model.PID = HJConvert.ToInt32(dr["PID"]);
                    model.Code = HJConvert.ToString(dr["Code"]);
                    model.ShopId = HJConvert.ToInt32(dr["ShopId"]);
                    model.foodids = HJConvert.ToString(dr["foodids"]);
                    model.Num = HJConvert.ToInt32(dr["Num"]);
                    model.cnum = HJConvert.ToInt32(dr["cnum"]);
                    model.Price = HJConvert.ToDecimal(dr["Price"]);
                    model.Unit = HJConvert.ToString(dr["Unit"]);
                    model.Tag = HJConvert.ToInt32(dr["Tag"]);
                    model.SectionId = HJConvert.ToInt32(dr["SectionId"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.state = HJConvert.ToInt32(dr["state"]);
                    model.title = HJConvert.ToString(dr["title"]);
                    model.sortnum = HJConvert.ToInt32(dr["sortnum"]);
                    model.startdate = HJConvert.ToDateTime(dr["startdate"]);
                    model.enddate = HJConvert.ToDateTime(dr["enddate"]);
                    model.starttime = HJConvert.ToDateTime(dr["starttime"]);
                    model.endtime = HJConvert.ToDateTime(dr["endtime"]);
                    model.oldprice = HJConvert.ToDecimal(dr["oldprice"]);
                    model.ReveFloat1 = HJConvert.ToDecimal(dr["ReveFloat1"]);
                    model.ReveFloat2 = HJConvert.ToDecimal(dr["ReveFloat2"]);
                    model.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
                    model.ReveVar2 = HJConvert.ToString(dr["ReveVar2"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "FoodPackag"), new SqlParameter("@strWhere", strWhere));
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
        public IList<FoodPackagInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<FoodPackagInfo> infos = new List<FoodPackagInfo>();
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
            parameters[0].Value = "FoodPackag";
            parameters[1].Value = "PID,Code,ShopId,foodids,Num,cnum,Price,Unit,Tag,SectionId,Inve1,Inve2,state,title,sortnum,startdate,enddate,starttime,endtime,oldprice,ReveFloat1,ReveFloat2,ReveVar1,ReveVar2";
            parameters[2].Value = "PID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    FoodPackagInfo info = new FoodPackagInfo();
                    info.PID = HJConvert.ToInt32(dr["PID"]);
                    info.Code = HJConvert.ToString(dr["Code"]);
                    info.ShopId = HJConvert.ToInt32(dr["ShopId"]);
                    info.foodids = HJConvert.ToString(dr["foodids"]);
                    info.Num = HJConvert.ToInt32(dr["Num"]);
                    info.cnum = HJConvert.ToInt32(dr["cnum"]);
                    info.Price = HJConvert.ToDecimal(dr["Price"]);
                    info.Unit = HJConvert.ToString(dr["Unit"]);
                    info.Tag = HJConvert.ToInt32(dr["Tag"]);
                    info.SectionId = HJConvert.ToInt32(dr["SectionId"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.state = HJConvert.ToInt32(dr["state"]);
                    info.title = HJConvert.ToString(dr["title"]);
                    info.sortnum = HJConvert.ToInt32(dr["sortnum"]);
                    info.startdate = HJConvert.ToDateTime(dr["startdate"]);
                    info.enddate = HJConvert.ToDateTime(dr["enddate"]);
                    info.starttime = HJConvert.ToDateTime(dr["starttime"]);
                    info.endtime = HJConvert.ToDateTime(dr["endtime"]);
                    info.oldprice = HJConvert.ToDecimal(dr["oldprice"]);
                    info.ReveFloat1 = HJConvert.ToDecimal(dr["ReveFloat1"]);
                    info.ReveFloat2 = HJConvert.ToDecimal(dr["ReveFloat2"]);
                    info.ReveVar1 = HJConvert.ToString(dr["ReveVar1"]);
                    info.ReveVar2 = HJConvert.ToString(dr["ReveVar2"]);
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
        public int DelFoodPackag(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from FoodPackag where PID=@PID");
            SqlParameter[] Para = 
			{
				new SqlParameter("@PID",SqlDbType.Int)
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
            str.Append("delete from FoodPackag where PID in ({0})");
            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 更新一个int字段的值 where是查询语句 需要带where 如：where DataId=1或者 where DataId in (1,2,3)
        /// </summary>
        public int UpdateValue(string param, int intValue, string Where)
        {
            return (int)SQLHelper.UpdateValue("FoodPackag", param, intValue, Where);
        }

        /// <summary>
        /// 获取精品套餐信息
        /// </summary>
        /// <param name="Uid"></param>
        /// <returns></returns>
        public IList<Hangjing.Model.FoodPackagInfo> GetFoodPackage(int togoID, string sqlWhere)
        {
            //1. 获取精品套餐
            IList<FoodPackagInfo> packagesList = new List<FoodPackagInfo>();
            string strsql1 = "GetFoodPackageAndDetail";

            int count = 0;
            SqlParameter parameter = new SqlParameter("@togoID", SqlDbType.Int, 4);
            parameter.Value = togoID;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, strsql1.ToString(), parameter))
            {
                while (dr.Read())
                {
                    count++;
                    FoodPackagInfo info = new FoodPackagInfo();
                    info.PID = HJConvert.ToInt32(dr["PID"]);
                    info.title = HJConvert.ToString(dr["title"]);
                    info.Price = HJConvert.ToDecimal(dr["Price"]);
                    info.oldprice = HJConvert.ToDecimal(dr["oldprice"]);
                    info.ItemList = new List<PackFoodlistInfo>();
                    packagesList.Add(info);
                }

                dr.NextResult();
                while (dr.Read())
                {
                    for (int j = 0; j < count; j++)
                    {
                        if (packagesList[j].title == HJConvert.ToString(dr["title"]))
                        {
                            Hangjing.Model.PackFoodlistInfo info = new Hangjing.Model.PackFoodlistInfo();
                            info.pid = HJConvert.ToInt32(dr["pid"]);
                            info.shopid = HJConvert.ToInt32(dr["shopid"]);
                            info.fid = HJConvert.ToInt32(dr["fid"]);
                            info.foodname = HJConvert.ToString(dr["foodname"]);
                            info.picture = HJConvert.ToString(dr["picture"]);

                            packagesList[j].ItemList.Add(info);
                        }
                    }
                }



            }
            return packagesList;
        }
    }
}

