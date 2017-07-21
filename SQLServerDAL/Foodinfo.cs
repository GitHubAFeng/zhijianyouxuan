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
    /// 菜品
    /// </summary>
    public class Foodinfo
    {
        /// <summary>
        /// 获取商家所有商品规格,属性
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public StyleAndAttr getAllByShopid(int shopid)
        {
            StyleAndAttr model = new StyleAndAttr();

            SqlParameter[] Para1 =
            {
                new SqlParameter("@shopid",SqlDbType.Int,5)
            };
            Para1[0].Value = shopid;

            model.styles = new List<FoodStyleInfo>();
            model.attrs = new List<FoodAttributesInfo>();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "ProductStyle_getAllByShopid", Para1))
            {
                while (dr.Read())
                {
                    FoodStyleInfo info = new FoodStyleInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.FoodtId = HJConvert.ToInt32(dr["FoodtId"]);
                    info.Title = HJConvert.ToString(dr["Title"]);
                    info.Price = HJConvert.ToDecimal(dr["Price"]);
                    info.MarkeyPrice = HJConvert.ToDecimal(dr["MarkeyPrice"]);
                    info.InUser = HJConvert.ToInt32(dr["InUser"]);
                    info.SaleSum = HJConvert.ToInt32(dr["SaleSum"]);
                    info.MaxPerDay = HJConvert.ToInt32(dr["MaxPerDay"]);
                    info.Intro = HJConvert.ToString(dr["Intro"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);


                    model.styles.Add(info);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    FoodAttributesInfo info = new FoodAttributesInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.FoodtId = HJConvert.ToInt32(dr["FoodtId"]);
                    info.Title = HJConvert.ToString(dr["Title"]);
                    info.SelectType = HJConvert.ToInt32(dr["SelectType"]);
                    info.Attributes = HJConvert.ToString(dr["Attributes"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.attritems = new List<attritem>();

                    string[] items = info.Attributes.Split('#');
                    foreach (var item in items)
                    {
                        string[] msg = item.Split('?');
                        info.attritems.Add(new attritem() { name = msg[0], price = msg[1], Title = info.Title, FoodtId = info.FoodtId, DataId = info.DataId });
                    }


                    model.attrs.Add(info);
                }
            }


            return model;

        }


        /// <summary>
        /// 根据规格设置价格及规格数
        /// </summary>
        public int UpdateFooodPrice(int foodid)
        {
            string sql = "UPDATE dbo.Foodinfo SET isspecial = (SELECT COUNT(1) FROM dbo.ProductStyle WHERE FoodtId = " + foodid + ") WHERE Unid = " + foodid;
            sql += ";UPDATE dbo.Foodinfo SET FPrice =  (SELECT TOP 1 Price FROM dbo.ProductStyle WHERE FoodtId = " + foodid + ") WHERE Unid = " + foodid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }


        /// <summary>
        /// 根据属性设置商品是否有属性信息
        /// </summary>
        public int UpdateFooodAttr(int foodid)
        {
            string sql = "UPDATE dbo.Foodinfo SET isauth = (SELECT COUNT(1) FROM dbo.ProductAttributes WHERE FoodtId = " + foodid + ") WHERE Unid = " + foodid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(FoodinfoInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Foodinfo(");
            strSql.Append("InUse,FoodNo,FPrice,FPInDate,FPActiveDate,FPMaster,FoodName,FoodNamePy,FullPrice,Remains,MaxPerDay,Taste,Picture,FoodType,OrderNum,isrecommend,isspecial,isauth,opentime,Oldnum,Oldprice,Special,DpPerDay)");
            strSql.Append(" values (");
            strSql.Append("@InUse,@FoodNo,@FPrice,@FPInDate,@FPActiveDate,@FPMaster,@FoodName,@FoodNamePy,@FullPrice,@Remains,@MaxPerDay,@Taste,@Picture,@FoodType,@OrderNum,@isrecommend,@isspecial,@isauth,@opentime,@Oldnum,@Oldprice,@Special,@DpPerDay)");
            strSql.Append(";select @@IDENTITY");

            SqlParameter[] parameters =
            {
                new SqlParameter("@InUse", SqlDbType.VarChar,10),
                new SqlParameter("@FoodNo", SqlDbType.VarChar,10),
                new SqlParameter("@FPrice", SqlDbType.Decimal),
                new SqlParameter("@FPInDate", SqlDbType.DateTime),
                new SqlParameter("@FPActiveDate", SqlDbType.DateTime),
                new SqlParameter("@FPMaster", SqlDbType.Int,8),
                new SqlParameter("@FoodName", SqlDbType.VarChar,256),
                new SqlParameter("@FoodNamePy", SqlDbType.VarChar,256),
                new SqlParameter("@FullPrice", SqlDbType.Decimal),
                new SqlParameter("@Remains", SqlDbType.Int,4),
                new SqlParameter("@MaxPerDay", SqlDbType.Int,4),
                new SqlParameter("@Taste", SqlDbType.Text),
                new SqlParameter("@Picture", SqlDbType.VarChar,50),
                new SqlParameter("@FoodType", SqlDbType.Int,4),
                new SqlParameter("@OrderNum", SqlDbType.Int,4),
                new SqlParameter("@isrecommend", SqlDbType.Int,4),
                new SqlParameter("@isspecial", SqlDbType.Int,4),
                new SqlParameter("@isauth", SqlDbType.Int,4),
                new SqlParameter("@opentime", SqlDbType.VarChar,255),
                new SqlParameter("@Oldnum", SqlDbType.Int,4),
                new SqlParameter("@Oldprice", SqlDbType.Decimal),
                new SqlParameter("@Special", SqlDbType.VarChar,10),
                new SqlParameter("@DpPerDay", SqlDbType.Int,4) 
              };
            parameters[0].Value = model.InUse;
            parameters[1].Value = model.FoodNo;
            parameters[2].Value = model.FPrice;
            parameters[3].Value = model.FPInDate;
            parameters[4].Value = model.FPActiveDate;
            parameters[5].Value = model.FPMaster;
            parameters[6].Value = model.FoodName;
            parameters[7].Value = model.FoodNamePy;
            parameters[8].Value = model.FullPrice;
            parameters[9].Value = model.Remains;
            parameters[10].Value = model.MaxPerDay;
            parameters[11].Value = model.Taste;
            parameters[12].Value = model.Picture;
            parameters[13].Value = model.FoodType;
            parameters[14].Value = model.OrderNum;
            parameters[15].Value = model.IsRecommend;
            parameters[16].Value = model.IsSpecial;
            parameters[17].Value = model.isauth;
            parameters[18].Value = model.OpenTime;
            parameters[19].Value = model.Remains;
            parameters[20].Value = model.Oldprice;
            parameters[21].Value = model.Special;
            parameters[22].Value = model.DpPerDay;

            int foodid = Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters)); 

            FoodStyleInfo info = new FoodStyleInfo();
            info.DataId = 0;
            info.Title = "";
            info.FoodtId = foodid;
            info.SaleSum = 0;
            info.Price = model.FPrice;
            info.MaxPerDay = 0;
            info.InUser = 0;
            info.Intro = "";// this.tbIntro.Value;
            info.MarkeyPrice = 0;
            info.Inve1 = 0;
            info.Inve2 = "";

            new FoodStyle().Add(info);


            return foodid;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(FoodinfoInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Foodinfo set ");
            strSql.Append("InUse=@InUse,");
            strSql.Append("FoodNo=@FoodNo,");
            strSql.Append("FPrice=@FPrice,");
            strSql.Append("FPInDate=@FPInDate,");
            strSql.Append("FPActiveDate=@FPActiveDate,");
            strSql.Append("FPMaster=@FPMaster,");
            strSql.Append("FoodName=@FoodName,");
            strSql.Append("FoodNamePy=@FoodNamePy,");
            strSql.Append("FullPrice=@FullPrice,");
            strSql.Append("Remains=@Remains,");
            strSql.Append("MaxPerDay=@MaxPerDay,");
            strSql.Append("Taste=@Taste,");
            strSql.Append("Picture=@Picture,");
            strSql.Append("FoodType=@FoodType,");
            strSql.Append("OrderNum=@OrderNum,");
            strSql.Append("isrecommend=@isrecommend,");
            strSql.Append("isauth=@isauth,");
            strSql.Append("isspecial=@isspecial, ");
            strSql.Append("opentime=@opentime ,Oldnum=@Oldnum,");
            strSql.Append("Oldprice=@Oldprice,");
            strSql.Append("Special=@Special,");
            strSql.Append("DpPerDay=@DpPerDay");
            strSql.Append(" where Unid=@Unid");
            SqlParameter[] parameters =
            {
                new SqlParameter("@Unid", SqlDbType.Int,4),
                new SqlParameter("@InUse", SqlDbType.VarChar,10),
                new SqlParameter("@FoodNo", SqlDbType.VarChar,10),
                new SqlParameter("@FPrice", SqlDbType.Decimal,9),
                new SqlParameter("@FPInDate", SqlDbType.DateTime),
                new SqlParameter("@FPActiveDate", SqlDbType.DateTime),
                new SqlParameter("@FPMaster", SqlDbType.Int,8),
                new SqlParameter("@FoodName", SqlDbType.VarChar,256),
                new SqlParameter("@FoodNamePy", SqlDbType.VarChar,256),
                new SqlParameter("@FullPrice", SqlDbType.Decimal,9),
                new SqlParameter("@Remains", SqlDbType.Int,4),
                new SqlParameter("@MaxPerDay", SqlDbType.Int,4),
                new SqlParameter("@Taste", SqlDbType.Text),
                new SqlParameter("@Picture", SqlDbType.VarChar,50),
                new SqlParameter("@FoodType", SqlDbType.Int,4),
                new SqlParameter("@OrderNum", SqlDbType.Int,11),
                new SqlParameter("@isrecommend", SqlDbType.Int,11),
                new SqlParameter("@isspecial", SqlDbType.Int,11),
                new SqlParameter("@isauth", SqlDbType.Int,4),
                new SqlParameter("@opentime", SqlDbType.VarChar,255),
                new SqlParameter("@Oldnum", SqlDbType.Int,4),
                new SqlParameter("@Oldprice", SqlDbType.Decimal),
                new SqlParameter("@Special", SqlDbType.VarChar,10),
                new SqlParameter("@DpPerDay", SqlDbType.Int,4),
                
                
            };
            parameters[0].Value = model.Unid;
            parameters[1].Value = model.InUse;
            parameters[2].Value = model.FoodNo;
            parameters[3].Value = model.FPrice;
            parameters[4].Value = model.FPInDate;
            parameters[5].Value = model.FPActiveDate;
            parameters[6].Value = model.FPMaster;
            parameters[7].Value = model.FoodName;
            parameters[8].Value = model.FoodNamePy;
            parameters[9].Value = model.FullPrice;
            parameters[10].Value = model.Remains;
            parameters[11].Value = model.MaxPerDay;
            parameters[12].Value = model.Taste;
            parameters[13].Value = model.Picture;
            parameters[14].Value = model.FoodType;
            parameters[15].Value = model.OrderNum;
            parameters[16].Value = model.IsRecommend;
            parameters[17].Value = model.IsSpecial;
            parameters[18].Value = model.isauth;
            parameters[19].Value = model.OpenTime;
            parameters[20].Value = model.Remains;
            parameters[21].Value = model.Oldprice;
            parameters[22].Value = model.Special;
            parameters[23].Value = model.DpPerDay;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        //热门菜品
        public IList<FoodinfoInfo> GetListt(int tid, int Unid)
        {

            IList<FoodinfoInfo> infos = new List<FoodinfoInfo>();

            string sql = "select top 10  foodinfo.Unid,count(*) as num, Foodinfo.FoodName,Foodinfo.FPrice,Foodinfo.FullPrice from Foodlist left join Foodinfo on Foodlist.FoodUnid = Foodinfo.Unid  where Foodlist.togoid =" + Unid + " group by foodinfo.Unid,Foodinfo.FoodName,Foodinfo.FPrice,Foodinfo.FullPrice order by num desc";
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (dr.Read())
                {

                    FoodinfoInfo model = new FoodinfoInfo();
                    model.Unid = HJConvert.ToInt32(dr["Unid"]);
                    model.FoodName = HJConvert.ToString(dr["FoodName"]);
                    model.FPrice = HJConvert.ToDecimal(dr["FPrice"]);
                    model.FullPrice = HJConvert.ToDecimal(dr["FullPrice"]);
                    infos.Add(model);
                }
            }
            return infos;
        }
        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>Unid</param>
        /// <returns>FoodinfoInfo</returns>
        public FoodinfoInfo GetModel(int Unid)
        {
            string sql = "select foodinfo.*,points.Name as togoname ,efoodsort.SortName  from foodinfo  left join efoodsort on foodinfo.FoodType=efoodsort.SortID   left join points on foodinfo.FPMaster =points.unid  where foodinfo.Unid =@nUnid";
            SqlParameter parameter = new SqlParameter("@nUnid", SqlDbType.Int, 4);
            parameter.Value = Unid;
            FoodinfoInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new FoodinfoInfo();
                    model.Unid = HJConvert.ToInt32(dr["Unid"]);
                    model.InUse = HJConvert.ToString(dr["InUse"]);
                    model.FoodNo = HJConvert.ToString(dr["FoodNo"]);
                    model.FPrice = HJConvert.ToDecimal(dr["FPrice"]);
                    model.FPInDate = HJConvert.ToDateTime(dr["FPInDate"]);
                    model.FPActiveDate = HJConvert.ToDateTime(dr["FPActiveDate"]);
                    model.FPMaster = HJConvert.ToInt32(dr["FPMaster"]);
                    model.FoodName = HJConvert.ToString(dr["FoodName"]);
                    model.FoodNamePy = HJConvert.ToString(dr["FoodNamePy"]);
                    model.FullPrice = HJConvert.ToDecimal(dr["FullPrice"]);
                    model.Remains = HJConvert.ToInt32(dr["Remains"]);
                    model.MaxPerDay = HJConvert.ToInt32(dr["MaxPerDay"]);
                    model.Taste = HJConvert.ToString(dr["Taste"]);
                    model.Picture = HJConvert.ToString(dr["Picture"]);
                    model.FoodType = HJConvert.ToInt32(dr["FoodType"]);
                    model.TogoName = HJConvert.ToString(dr["TogoName"]);
                    model.SortName = HJConvert.ToString(dr["SortName"]);
                    model.OrderNum = HJConvert.ToInt32(dr["OrderNum"]);
                    model.IsRecommend = HJConvert.ToInt32(dr["isrecommend"]);
                    model.IsSpecial = HJConvert.ToInt32(dr["isspecial"]);
                    model.isauth = HJConvert.ToInt32(dr["isauth"]);
                    model.OpenTime = HJConvert.ToString(dr["OpenTime"]);
                    model.Oldprice = HJConvert.ToDecimal(dr["Oldprice"]);
                    model.Special = HJConvert.ToString(dr["Special"]);
                    model.DpPerDay = HJConvert.ToInt32(dr["DpPerDay"]);
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
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "Foodinfo"), new SqlParameter("@strWhere", strWhere)));
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
        public IList<FoodinfoInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<FoodinfoInfo> infos = new List<FoodinfoInfo>();
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
            parameters[0].Value = "Foodinfo";
            parameters[1].Value = "*,(select SortName from EFoodSort where SortID=Foodinfo.FoodType) as SortName,(select Name from points where unid=Foodinfo.FPMaster) as TogoName";
            parameters[2].Value = "Unid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    FoodinfoInfo info = new FoodinfoInfo();
                    info.Unid = HJConvert.ToInt32(dr["Unid"]);
                    info.InUse = HJConvert.ToString(dr["InUse"]);
                    info.FoodNo = HJConvert.ToString(dr["FoodNo"]);
                    info.FPrice = HJConvert.ToDecimal(dr["FPrice"]);

                    info.FPMaster = HJConvert.ToInt32(dr["FPMaster"]);
                    info.FoodName = HJConvert.ToString(dr["FoodName"]);
                    info.FoodNamePy = HJConvert.ToString(dr["FoodNamePy"]);
                    info.FullPrice = HJConvert.ToDecimal(dr["FullPrice"]);
                    info.Remains = HJConvert.ToInt32(dr["Remains"]);
                    info.MaxPerDay = HJConvert.ToInt32(dr["MaxPerDay"]);
                    info.Taste = HJConvert.ToString(dr["Taste"]);
                    info.Picture = HJConvert.ToString(dr["Picture"]);
                    info.FoodType = HJConvert.ToInt32(dr["FoodType"]);
                    info.SortName = HJConvert.ToString(dr["SortName"]);
                    info.TogoName = HJConvert.ToString(dr["TogoName"]);
                    info.OrderNum = HJConvert.ToInt32(dr["OrderNum"]);
                    info.IsRecommend = HJConvert.ToInt32(dr["isrecommend"]);
                    info.IsSpecial = HJConvert.ToInt32(dr["isspecial"]);
                    info.isauth = HJConvert.ToInt32(dr["isauth"]);
                    info.Oldprice = HJConvert.ToDecimal(dr["Oldprice"]);
                    info.Special = HJConvert.ToString(dr["Special"]);
                    info.DpPerDay = HJConvert.ToInt32(dr["DpPerDay"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 大家都吃的商品（前50个商品）
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        public IList<FoodinfoInfo> getHotFoods(string lat,string lng)
        {
            IList<FoodinfoInfo> infos = new List<FoodinfoInfo>();
            SqlParameter[] parameters =
            {
                new SqlParameter("@lat", SqlDbType.VarChar,50),
                new SqlParameter("@lng", SqlDbType.VarChar,50),
            };
            parameters[0].Value = lat;
            parameters[1].Value = lng;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "Foodinfo_getHotFoods", parameters))
            {
                while (dr.Read())
                {
                    FoodinfoInfo info = new FoodinfoInfo();
                    info.Unid = HJConvert.ToInt32(dr["Unid"]);
                    info.InUse = HJConvert.ToString(dr["InUse"]);
                    info.FoodNo = HJConvert.ToString(dr["FoodNo"]);
                    info.FPrice = HJConvert.ToDecimal(dr["FPrice"]);
                    info.FPMaster = HJConvert.ToInt32(dr["FPMaster"]);
                    info.FoodName = HJConvert.ToString(dr["FoodName"]);
                    info.FoodNamePy = HJConvert.ToString(dr["FoodNamePy"]);
                    info.FullPrice = HJConvert.ToDecimal(dr["FullPrice"]);
                    info.Remains = HJConvert.ToInt32(dr["Remains"]);
                    info.MaxPerDay = HJConvert.ToInt32(dr["MaxPerDay"]);
                    info.Taste = HJConvert.ToString(dr["Taste"]);
                    info.Picture = HJConvert.ToString(dr["Picture"]);
                    info.FoodType = HJConvert.ToInt32(dr["FoodType"]);
                    info.TogoName = HJConvert.ToString(dr["togoname"]);
                    info.OrderNum = HJConvert.ToInt32(dr["OrderNum"]);
                    info.IsRecommend = HJConvert.ToInt32(dr["isrecommend"]);
                    info.IsSpecial = HJConvert.ToInt32(dr["isspecial"]);
                    info.isauth = HJConvert.ToInt32(dr["isauth"]);
                    info.Oldprice = HJConvert.ToDecimal(dr["Oldprice"]);
                    info.Special = HJConvert.ToString(dr["Special"]);
                    info.DpPerDay = HJConvert.ToInt32(dr["DpPerDay"]);
                    info.SortName = HJConvert.ToString(dr["salecount"]);


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
        public int DelFoodinfo(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from Foodinfo where Unid=@Unid");
            SqlParameter[] Para =
            {
                new SqlParameter("@Unid",SqlDbType.Int)
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
            str.Append("delete from Foodinfo where Unid in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 修改上下线（inuse）
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        public int UpdateValue(string param, string charValue, string Where)
        {
            return Convert.ToInt32(SQLHelper.UpdateValue("Foodinfo", param, charValue, Where));
        }

        /// <summary>
        /// 获取首页推荐商家,餐品信息
        /// </summary>
        /// <returns></returns>
        public HotTogoandFoodInfo GetHot()
        {
            HotTogoandFoodInfo model = null;
            string sql = "select top 1 * from HotTogoandFood";
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    model = new HotTogoandFoodInfo();
                    model.Dataid = HJConvert.ToInt32(dr["dataid"]);
                    model.Togoids = HJConvert.ToString(dr["togoids"]);
                    model.Foodids = HJConvert.ToString(dr["foodids"]);
                    model.Otherids = HJConvert.ToString(dr["otherids"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 获得推荐信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateHot(HotTogoandFoodInfo model)
        {
            string sql = "update hottogoandfood set togoids = @togoids ,foodids = @foodids where dataid=1";
            SqlParameter[] parameters =
            {
                new SqlParameter("@togoids" , SqlDbType.VarChar ,50),
                new SqlParameter("@foodids" , SqlDbType.VarChar , 50)
            };
            parameters[0].Value = model.Togoids;
            parameters[1].Value = model.Foodids;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, parameters);
        }

        /// <summary>
        /// 修改图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdatePicture(FoodinfoInfo model)
        {
            string sql = "update Foodinfo set Picture = @Picture where FoodNo = @FoodNo";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Picture" , SqlDbType.VarChar , 200),
                new SqlParameter("@FoodNo" , SqlDbType.Int)
            };
            parameters[0].Value = model.Picture;
            parameters[1].Value = model.FoodNo;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, parameters);
        }


        /// <summary>
        /// 更新一个DateTime字段的值 where是查询语句 需要带where 如：where DataId=1或者 where DataId in (1,2,3)
        /// </summary>
        public int UpdateValue(string param, DateTime Value, string Where)
        {
            return (int)SQLHelper.UpdateValue("Foodinfo", param, Value, Where);
        }

        /// <summary>
        /// 更新一个int字段的值 where是查询语句 需要带where 如：where DataId=1或者 where DataId in (1,2,3)
        /// </summary>
        public int UpdateValue(string param, int intValue, string Where)
        {
            return (int)SQLHelper.UpdateValue("Foodinfo", param, intValue, Where);
        }

        /// <summary>
        /// 获取列表热点,卖得多的
        /// </summary>
        /// <param name="togoid">商家</param>
        public IList<FoodinfoInfo> GethotList(int togoid)
        {
            IList<FoodinfoInfo> infos = new List<FoodinfoInfo>();
            SqlParameter[] parameters =
            {
                new SqlParameter("@ntogoid", SqlDbType.Int,6)
            };
            parameters[0].Value = togoid;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, "select unid,FoodName,FPrice from Foodinfo ", parameters))
            {
                while (dr.Read())
                {
                    FoodinfoInfo info = new FoodinfoInfo();
                    info.Unid = HJConvert.ToInt32(dr["unid"]);
                    info.FoodName = HJConvert.ToString(dr["FoodName"]);
                    info.FPrice = HJConvert.ToInt32(dr["FPrice"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 更新库存
        /// </summary>
        public int UpdateStock(FoodinfoInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Foodinfo set ");
            strSql.Append("Remains=@Remains,");
            strSql.Append("MaxPerDay=@MaxPerDay");
            strSql.Append(" where Unid=@Unid");
            SqlParameter[] parameters =
            {
                new SqlParameter("@Unid", SqlDbType.Int,4),
                new SqlParameter("@Remains", SqlDbType.Int,4),
                new SqlParameter("@MaxPerDay", SqlDbType.Int,4)
            };
            parameters[0].Value = model.Unid;
            parameters[1].Value = model.Remains;
            parameters[2].Value = model.MaxPerDay;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新库存  是否推荐 是否特价
        /// </summary>
        public int UpdateSubInfo(FoodinfoInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Foodinfo set ");
            strSql.Append("Remains=@Remains,");
            strSql.Append("MaxPerDay=@MaxPerDay,");
            strSql.Append("isrecommend=@isrecommend,");
            strSql.Append("isspecial=@isspecial ");
            strSql.Append(" where Unid=@Unid");
            SqlParameter[] parameters =
            {
                new SqlParameter("@Unid", SqlDbType.Int,4),
                new SqlParameter("@Remains", SqlDbType.Int,4),
                new SqlParameter("@MaxPerDay", SqlDbType.Int,4),
                new SqlParameter("@isrecommend", SqlDbType.Int,11),
                new SqlParameter("@isspecial", SqlDbType.Int,11)
            };
            parameters[0].Value = model.Unid;
            parameters[1].Value = model.Remains;
            parameters[2].Value = model.MaxPerDay;
            parameters[3].Value = model.IsRecommend;
            parameters[4].Value = model.IsSpecial;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 设置所有商品的默认库存
        /// </summary>
        public int UpdateDefultStock(int Stock)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Foodinfo set ");
            strSql.Append("Remains=@Remains,");
            strSql.Append("MaxPerDay=@MaxPerDay");
            SqlParameter[] parameters =
            {
                new SqlParameter("@Remains", SqlDbType.Int,4),
                new SqlParameter("@MaxPerDay", SqlDbType.Int,4),
            };
            parameters[0].Value = Stock;
            parameters[1].Value = Stock;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 设置所有商品的默认库存
        /// </summary>
        public int UpdatePy(FoodinfoInfo m)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Foodinfo set ");
            strSql.Append("FoodNamePy=@FoodNamePy where ");
            strSql.Append("Unid=@Unid");
            SqlParameter[] parameters =
            {
                new SqlParameter("@FoodNamePy", SqlDbType.VarChar,50),
                new SqlParameter("@Unid", SqlDbType.Int,5),
            };
            parameters[0].Value = m.FoodNamePy;
            parameters[1].Value = m.Unid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
    }

}

