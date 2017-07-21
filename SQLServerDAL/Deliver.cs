using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Hangjing.Model;
using Hangjing.DBUtility;
using System.Collections;
//using Hangjing.IDAL;


namespace Hangjing.SQLServerDAL
{
    public class Deliver
    {
        /// <summary>
        /// 后台获取列表
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<DeliverInfo> GetDeliverList(int pagesize, int pageindex, string strWhere, string orderName, int orderType, string otherwhere)
        {
            IList<DeliverInfo> infos = new List<DeliverInfo>();
            SqlParameter[] parameters =
            {
                new SqlParameter("@pagesize", SqlDbType.Int),
                new SqlParameter("@pageindex", SqlDbType.Int),
                new SqlParameter("@orderfield", SqlDbType.VarChar,255),
                new SqlParameter("@ordertype", SqlDbType.VarChar,5),
                new SqlParameter("@where", SqlDbType.VarChar,1500),
                new SqlParameter("@otherwhere", SqlDbType.VarChar,1500)
            };
            parameters[0].Value = pagesize;
            parameters[1].Value = pageindex;
            parameters[2].Value = orderName;
            parameters[3].Value = orderType == 1 ? "desc" : "asc"; ;
            parameters[4].Value = strWhere;
            parameters[5].Value = otherwhere;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "deliver_getlistWithState", parameters))
            {
                while (dr.Read())
                {
                    DeliverInfo info = new DeliverInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.CodeId = HJConvert.ToString(dr["CodeId"]);
                    info.Name = HJConvert.ToString(dr["Name"]);
                    info.Phone = HJConvert.ToString(dr["Phone"]);
                    info.Section = HJConvert.ToString(dr["Section"]);

                    DateTime lasttime = HJConvert.ToDateTime(dr["lasttime"]);
                    if ((DateTime.Now - lasttime).TotalMinutes < Hangjing.Common.Constant.OffLineDoor)
                    {
                        info.Status = 1;
                    }

                    info.GpsIMEI = HJConvert.ToString(dr["GpsIMEI"]);
                    info.OrderNum = HJConvert.ToInt32(dr["OrderNum"]);
                    info.UserName = HJConvert.ToString(dr["UserName"]);
                    info.Password = HJConvert.ToString(dr["Password"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);

                    info.recordtcount = HJConvert.ToInt32(dr["recordtcount"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    info.cityname = HJConvert.ToString(dr["cityname"]);
                    info.Groupname = HJConvert.ToString(dr["Groupname"]);


                    infos.Add(info);
                }
            }
            return infos;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.DeliverInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Deliver(");
            strSql.Append("CodeId,Name,Phone,Section,Status,GpsIMEI,OrderNum,UserName,Password,Inve1,Inve2,AddDate,IsApproved,pic1,havemoney,IsWorking)");
            strSql.Append(" values (");
            strSql.Append("@CodeId,@Name,@Phone,@Section,@Status,@GpsIMEI,@OrderNum,@UserName,@Password,@Inve1,@Inve2,@AddDate,@IsApproved,@pic1,@havemoney,@IsWorking)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@CodeId", SqlDbType.VarChar,10),
				new SqlParameter("@Name", SqlDbType.VarChar,50),
				new SqlParameter("@Phone", SqlDbType.VarChar,50),
				new SqlParameter("@Section", SqlDbType.VarChar,256),
				new SqlParameter("@Status", SqlDbType.Int,4),
				new SqlParameter("@GpsIMEI", SqlDbType.VarChar,50),
				new SqlParameter("@OrderNum", SqlDbType.Int,4),
				new SqlParameter("@UserName", SqlDbType.NVarChar,50),
				new SqlParameter("@Password", SqlDbType.NVarChar,50),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,256),
				new SqlParameter("@AddDate", SqlDbType.DateTime),
				new SqlParameter("@IsApproved", SqlDbType.Int),
				new SqlParameter("@pic1", SqlDbType.VarChar,256),
                new SqlParameter("@havemoney", SqlDbType.Decimal,9),
                new SqlParameter("@IsWorking", SqlDbType.Int),
            };
            parameters[0].Value = model.CodeId;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Phone;
            parameters[3].Value = model.Section;
            parameters[4].Value = model.Status;
            parameters[5].Value = model.GpsIMEI;
            parameters[6].Value = model.OrderNum;
            parameters[7].Value = model.UserName;
            parameters[8].Value = model.Password;
            parameters[9].Value = model.Inve1;
            parameters[10].Value = model.Inve2;
            parameters[11].Value = model.AddDate;
            parameters[12].Value = model.IsApproved;

            parameters[13].Value = model.pic1 == null ? "" : model.pic1;
            parameters[14].Value = model.havemoney;
            parameters[15].Value = model.IsWorking;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.DeliverInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Deliver set ");
            strSql.Append("CodeId=@CodeId,");
            strSql.Append("Name=@Name,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("Section=@Section,");
            strSql.Append("Status=@Status,");
            strSql.Append("GpsIMEI=@GpsIMEI,");
            strSql.Append("OrderNum=@OrderNum,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Password=@Password,");
            strSql.Append("Inve1=@Inve1,");
            strSql.Append("Inve2=@Inve2,");
            strSql.Append("AddDate=@AddDate");
            strSql.Append(",IsApproved=@IsApproved");
            strSql.Append(",pic1=@pic1");
            strSql.Append(",IsWorking=@IsWorking");
            strSql.Append(" where DataId=@DataId");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataId", SqlDbType.Int,4),
				new SqlParameter("@CodeId", SqlDbType.VarChar,10),
				new SqlParameter("@Name", SqlDbType.VarChar,50),
				new SqlParameter("@Phone", SqlDbType.VarChar,50),
				new SqlParameter("@Section", SqlDbType.VarChar,256),
				new SqlParameter("@Status", SqlDbType.Int,4),
				new SqlParameter("@GpsIMEI", SqlDbType.VarChar,50),
				new SqlParameter("@OrderNum", SqlDbType.Int,4),
				new SqlParameter("@UserName", SqlDbType.NVarChar,50),
				new SqlParameter("@Password", SqlDbType.NVarChar,50),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@Inve2", SqlDbType.VarChar,256),
				new SqlParameter("@AddDate", SqlDbType.DateTime),
				new SqlParameter("@IsApproved", SqlDbType.Int,4),
				new SqlParameter("@pic1", SqlDbType.VarChar,256),
                new SqlParameter("@IsWorking", SqlDbType.Int,4),
            };
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.CodeId;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Phone;
            parameters[4].Value = model.Section;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.GpsIMEI;
            parameters[7].Value = model.OrderNum;
            parameters[8].Value = model.UserName;
            parameters[9].Value = model.Password;
            parameters[10].Value = model.Inve1;
            parameters[11].Value = model.Inve2;
            parameters[12].Value = model.AddDate;
            parameters[13].Value = model.IsApproved;

            parameters[14].Value = model.pic1;
            parameters[15].Value = model.IsWorking;


            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>DeliverInfo</returns>
        public DeliverInfo GetModel(int DataId)
        {
            string sql = "select DataId,CodeId,Name,Phone,Section,Status,GpsIMEI,OrderNum,UserName,Password,Inve1,Inve2,AddDate,IsApproved ,pic1,havemoney,IsWorking  from Deliver where  DataId = @DataId";
            SqlParameter parameter = new SqlParameter("@DataId", SqlDbType.Int, 4);
            parameter.Value = DataId;
            DeliverInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new DeliverInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.CodeId = HJConvert.ToString(dr["CodeId"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.Phone = HJConvert.ToString(dr["Phone"]);
                    model.Section = HJConvert.ToString(dr["Section"]);
                    model.Status = HJConvert.ToInt32(dr["Status"]);
                    model.GpsIMEI = HJConvert.ToString(dr["GpsIMEI"]);
                    model.OrderNum = HJConvert.ToInt32(dr["OrderNum"]);
                    model.UserName = HJConvert.ToString(dr["UserName"]);
                    model.Password = HJConvert.ToString(dr["Password"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.AddDate = HJConvert.ToDateTime(dr["AddDate"]);

                    model.IsApproved = HJConvert.ToInt32(dr["IsApproved"]);
                    model.pic1 = HJConvert.ToString(dr["pic1"]);
                    model.havemoney = HJConvert.ToDecimal(dr["havemoney"]);
                    model.IsWorking = HJConvert.ToInt32(dr["IsWorking"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "Deliver"), new SqlParameter("@strWhere", strWhere));
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
        public IList<DeliverInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<DeliverInfo> infos = new List<DeliverInfo>();
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
            parameters[0].Value = "Deliver";
            parameters[1].Value = "*,(select cname from City where cid=Deliver.Inve1) cityname,(select classname from DeliverGroup where id=Deliver.GpsIMEI) Groupname";
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
                    DeliverInfo info = new DeliverInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.CodeId = HJConvert.ToString(dr["CodeId"]);
                    info.Name = HJConvert.ToString(dr["Name"]);
                    info.Phone = HJConvert.ToString(dr["Phone"]);
                    info.Section = HJConvert.ToString(dr["Section"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.GpsIMEI = HJConvert.ToString(dr["GpsIMEI"]);
                    info.OrderNum = HJConvert.ToInt32(dr["OrderNum"]);
                    info.UserName = HJConvert.ToString(dr["UserName"]);
                    info.Password = HJConvert.ToString(dr["Password"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    info.cityname = HJConvert.ToString(dr["cityname"]);
                    info.Groupname = HJConvert.ToString(dr["Groupname"]);
                    info.IsApproved = HJConvert.ToInt32(dr["IsApproved"]);
                    info.IsWorking = HJConvert.ToInt32(dr["IsWorking"]);
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
        public int DelDeliver(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from Deliver where DataId=@DataId");
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
            str.Append("delete from Deliver where DataId in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>DeliverInfo</returns>
        public DeliverInfo GetModelByCodeId(string CodeId)
        {
            string sql = "selectDataId,CodeId,Name,Phone,Section,Status,GpsIMEI,OrderNum,UserName,Password,Inve1,Inve2,AddDate from Deliver where  CodeId = @CodeId";
            SqlParameter parameter = new SqlParameter("@CodeId", SqlDbType.VarChar, 10);
            parameter.Value = CodeId;

            DeliverInfo model = null;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new DeliverInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.CodeId = HJConvert.ToString(dr["CodeId"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.Phone = HJConvert.ToString(dr["Phone"]);
                    model.Section = HJConvert.ToString(dr["Section"]);
                    model.Status = HJConvert.ToInt32(dr["Status"]);
                    model.GpsIMEI = HJConvert.ToString(dr["GpsIMEI"]);
                    model.OrderNum = HJConvert.ToInt32(dr["OrderNum"]);
                    model.UserName = HJConvert.ToString(dr["UserName"]);
                    model.Password = HJConvert.ToString(dr["Password"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataId</param>
        /// <returns>DeliverInfo</returns>
        public DeliverInfo GetModelByIMEI(string IMEI)
        {
            string sql = "select DataId,CodeId,Name,Phone,Section,Status,GpsIMEI,OrderNum,UserName,Password,Inve1,Inve2,AddDate,IsApproved from Deliver where  GpsIMEI = @GpsIMEI";
            SqlParameter parameter = new SqlParameter("@GpsIMEI", SqlDbType.VarChar, 10);
            parameter.Value = IMEI;

            DeliverInfo model = null;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new DeliverInfo();
                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.CodeId = HJConvert.ToString(dr["CodeId"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.Phone = HJConvert.ToString(dr["Phone"]);
                    model.Section = HJConvert.ToString(dr["Section"]);
                    model.Status = HJConvert.ToInt32(dr["Status"]);
                    model.GpsIMEI = HJConvert.ToString(dr["GpsIMEI"]);
                    model.OrderNum = HJConvert.ToInt32(dr["OrderNum"]);
                    model.UserName = HJConvert.ToString(dr["UserName"]);
                    model.Password = HJConvert.ToString(dr["Password"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.AddDate = HJConvert.ToDateTime(dr["AddDate"]);

                    model.IsApproved = HJConvert.ToInt32(dr["IsApproved"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 配送员登录 zjf@ihangjing.com 2012.5.5 add
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public DeliverInfo GetModelByUserNameAndPassword(string UserName, string Password)
        {
            SqlParameter[] parameters = 
            {
                new SqlParameter("@UserName" , SqlDbType.VarChar , 50),
                new SqlParameter("@Password" , SqlDbType.VarChar ,50)
            };
            parameters[0].Value = UserName;
            parameters[1].Value = Password;

            DeliverInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "GetDeliverModelByUserNameAndPassword", parameters))
            {
                if (dr.Read())
                {
                    model = new DeliverInfo();

                    model.DataId = HJConvert.ToInt32(dr["DataId"]);
                    model.CodeId = HJConvert.ToString(dr["CodeId"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.Phone = HJConvert.ToString(dr["Phone"]);
                    model.Section = HJConvert.ToString(dr["Section"]);
                    model.Status = HJConvert.ToInt32(dr["Status"]);
                    model.GpsIMEI = HJConvert.ToString(dr["GpsIMEI"]);
                    model.OrderNum = HJConvert.ToInt32(dr["OrderNum"]);
                    model.UserName = HJConvert.ToString(dr["UserName"]);
                    model.Password = HJConvert.ToString(dr["Password"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    model.AddDate = HJConvert.ToDateTime(dr["AddDate"]);

                }
            }

            return model;
        }

        /// <summary>
        /// 修改用户密码 zjf@ihangjing.com 2012.5.7
        /// </summary>
        /// <param name="dataid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int ChangePassword(int Dataid, string Password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Deliver set Password=@Password where dataid=@dataid");
            SqlParameter[] parameters = 
            {
                new SqlParameter("@Password" ,SqlDbType.VarChar , 50),
                new SqlParameter("@dataid" , SqlDbType.Int , 4)
            };
            parameters[0].Value = Password;
            parameters[1].Value = Dataid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取列表 包含配送员有多少订单在配送中
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<DeliverInfo> GetListWithOrderNum(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<DeliverInfo> infos = new List<DeliverInfo>();
            SqlParameter[] parameters = 
			{
				new SqlParameter("@where", SqlDbType.VarChar,1500)
			};
            parameters[0].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "deliver_ListWithOrderNum", parameters))
            {
                while (dr.Read())
                {
                    DeliverInfo info = new DeliverInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.CodeId = HJConvert.ToString(dr["CodeId"]);
                    info.Name = HJConvert.ToString(dr["Name"]);
                    info.Phone = HJConvert.ToString(dr["Phone"]);
                    info.Section = HJConvert.ToString(dr["Section"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.GpsIMEI = HJConvert.ToString(dr["GpsIMEI"]);
                    info.OrderNum = HJConvert.ToInt32(dr["OrderNum"]);
                    info.UserName = HJConvert.ToString(dr["UserName"]);
                    info.Password = HJConvert.ToString(dr["Password"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    //获取配送员正在配送的订单个数
                    info.OrderNum = HJConvert.ToInt32(dr["TOrderNum"]);
                    info.Lat = HJConvert.ToString(dr["lat"]);
                    info.Lng = HJConvert.ToString(dr["Lng"]);

                    infos.Add(info);
                }
            }
            return infos;
        }


        /// <summary>
        /// 获取一个配送员
        /// </summary>
        ///此代码由杭景科技代码内部生成器自动生成
        public DeliverInfo GetOneDeliver(int did)
        {

            DeliverInfo info = null;
            string sql = " select top 1 a.*,b.*,(select count(*) ordernum from Custorder where  deliverid=a.DataId and OrderStatus=7 and sendstate < 3 ) as TOrderNum from Deliver a left join GPS_Records b on a.dataid = b.jh1 where a.dataid = @did order by ID desc ";

            SqlParameter[] parameters = 
			{
				new SqlParameter("@did", SqlDbType.Int)
			};
            parameters[0].Value = did;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameters))
            {
                while (dr.Read())
                {
                    info = new DeliverInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.CodeId = HJConvert.ToString(dr["CodeId"]);
                    info.Name = HJConvert.ToString(dr["Name"]);
                    info.Phone = HJConvert.ToString(dr["Phone"]);
                    info.Section = HJConvert.ToString(dr["Section"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.GpsIMEI = HJConvert.ToString(dr["GpsIMEI"]);
                    info.OrderNum = HJConvert.ToInt32(dr["OrderNum"]);
                    info.UserName = HJConvert.ToString(dr["UserName"]);
                    info.Password = HJConvert.ToString(dr["Password"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.Inve2 = HJConvert.ToString(dr["Inve2"]);
                    info.AddDate = HJConvert.ToDateTime(dr["AddDate"]);
                    //获取配送员正在配送的订单个数
                    info.OrderNum = HJConvert.ToInt32(dr["TOrderNum"]);

                    info.completeorder = new Custorder().GetCount("deliverid=" + did + " and OrderStatus=3 and DATEDIFF(day,OrderDateTime,GETDATE()) =0 ");
                    info.timeoutorder = new Custorder().GetCount("deliverid=" + did + " and OrderStatus=7 and DATEDIFF(day,OrderDateTime,GETDATE()) =0 and SendTime < GETDATE() ");


                    //坐标信息要修正
                    string Lat = HJConvert.ToString(dr["jh3"]);
                    string Lng = HJConvert.ToString(dr["jh2"]);
                    if (Lat != "" && Lng != "")
                    {
                        //3.15修改
                        //info.Lat = (Convert.ToDouble(Lat) + 0.003574).ToString();
                        //info.Lng = (Convert.ToDouble(Lng) + 0.011292).ToString();


                        info.Lat = Lat;
                        info.Lng = Lng;

                        info.speed = HJConvert.ToInt32(Convert.ToDecimal(dr["JH4"]));
                        info.direction = HJConvert.ToInt32(Convert.ToDecimal(dr["JH5"]));
                        info.carstate = "";

                        DateTime recordAddTime = HJConvert.ToDateTime(dr["AddTime"]);

                        //大于5分没上传数据，表示离线
                        if (DateTime.Now > recordAddTime.AddMinutes(5))
                        {
                            info.carstate = "离线";
                        }
                        else
                        {
                            if (info.speed == 0)
                            {
                                info.carstate = "停车";
                            }
                            else
                            {
                                info.carstate = "行驶(" + info.speed + "公里/小时)";
                            }
                        }

                    }
                    else
                    {
                        info.Lat = "";
                        info.Lng = "";
                        info.speed = 0;
                        info.direction = 0;
                        info.carstate = "离线";
                    }

                }
            }
            return info;
        }

        /// <summary>
        ///  配送员重新分配后保存[群编号未修改的，不编辑]
        /// </summary>
        /// <param name="list">有修改的列表</param>
        /// <param name="nosetlist">设置为未分配的，群编号设置为0</param>
        /// <returns></returns>
        public bool SaveGroupDeliverR(IList<Hangjing.Model.DeliverGroupInfo> list, IList<Hangjing.Model.DeliverInfo> nosetlist)
        {
            IList<ROrderinfo> my_list = new List<ROrderinfo>();
            ArrayList cmdtextlist = new ArrayList();
            ArrayList paraslist = new ArrayList();

            DeliverGroupInfo model = new DeliverGroupInfo();
            model.ID = 0; 
            model.deliverlst = nosetlist;
            list.Add(model);

            foreach (var item in list)
            {
                foreach (var shop in item.deliverlst)
                {
                    if (shop.GpsIMEI != item.ID.ToString())
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update Deliver set ");

                        strSql.Append("GpsIMEI=@GpsIMEI");
                        strSql.Append(" where DataId=@DataId");
                        SqlParameter[] parameters = 
		                {
                            new SqlParameter("@GpsIMEI", SqlDbType.Int,4) ,            
                            new SqlParameter("@DataId", SqlDbType.Int,4) ,                      
                        };
                        parameters[0].Value = item.ID;
                        parameters[1].Value = shop.DataId;

                        cmdtextlist.Add(strSql.ToString());
                        paraslist.Add(parameters);
                    }

                }
            }



            return SQLHelper.ExecuteSqlTran(CommandType.Text, cmdtextlist, paraslist);

        }


        /// <summary>
        /// 更新app登入信息,登录时设置1，退出设置0
        /// </summary>
        /// <param name="TogoNum"></param>
        /// <param name="NewPwd"></param>
        /// <returns></returns>
        public int UpdateLoginState(int TogoNum, int State)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update Deliver set Status=@State where DataID=@TogoNum");

            SqlParameter[] Para = 
            {
                new SqlParameter("@TogoNum",SqlDbType.Int),
                new SqlParameter("@State",SqlDbType.Int)
            };
            Para[0].Value = TogoNum;
            Para[1].Value = State;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 查询附近几公里内的商家
        /// </summary>
        /// <param name="distance">范围</param>
        /// <param name="lat">纬度</param>
        /// <param name="lng">经度</param>
        /// <returns></returns>
        public IList<DeliverInfo> GetNearbyList(decimal distance, string lat, string lng)
        {
            IList<DeliverInfo> infos = new List<DeliverInfo>();
            SqlParameter[] parameters = 
			{
				new SqlParameter("@distance", SqlDbType.Decimal),
				new SqlParameter("@lat", SqlDbType.VarChar,50),
				new SqlParameter("@lng", SqlDbType.VarChar,50),
			};
            parameters[0].Value = distance;
            parameters[1].Value = lat;
            parameters[2].Value = lng;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "deliver_Nearby", parameters))
            {
                while (dr.Read())
                {
                    DeliverInfo info = new DeliverInfo();
                    info.DataId = HJConvert.ToInt32(dr["DataId"]);
                    info.Name = HJConvert.ToString(dr["Name"]);
                    info.Lat = HJConvert.ToString(dr["lat"]);
                    info.Lng = HJConvert.ToString(dr["lng"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 订单统计
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="strWhere"></param>
        /// <param name="orderwhere"></param>
        /// <returns></returns>
        public IList<TogoInfo> SendOrderStatistics(int pagesize, int pageindex, string strWhere, string orderwhere, string ordername)
        {
            IList<TogoInfo> infos = new List<TogoInfo>();
            SqlParameter[] parameters = 
			{
				new SqlParameter("@PageSize", SqlDbType.Int),
				new SqlParameter("@PageIndex", SqlDbType.Int),
				new SqlParameter("@strWhere", SqlDbType.VarChar,1500),
                new SqlParameter("@orderwhere", SqlDbType.VarChar,1500),
                new SqlParameter("@ordername", SqlDbType.VarChar,100)
			};
            parameters[0].Value = pagesize;
            parameters[1].Value = pageindex;
            parameters[2].Value = strWhere;
            parameters[3].Value = orderwhere;
            parameters[4].Value = ordername;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "deliver_SendOrderStatistics", parameters))
            {
                while (dr.Read())
                {
                    TogoInfo info = new TogoInfo();
                    info.TogoName = HJConvert.ToString(dr["classname"]);
                    info.allcount = HJConvert.ToInt32(dr["ordercount"]);
                    info.allprice = HJConvert.ToDecimal(dr["TotalPrice"]);
                    info.ShopHaveMoney = HJConvert.ToDecimal(dr["OrderTotal"]);
                    info.DataID = HJConvert.ToInt32(dr["recordtcount"]);
                    info.Shopprofit = HJConvert.ToDecimal(dr["Shopprofit"]);
                    info.getmoney = HJConvert.ToDecimal(dr["getmoney"]);

                    info.SendFee = HJConvert.ToDecimal(dr["SendFee"]);

                    info.basewage = info.allcount * HJConvert.ToDecimal(dr["CodeId"]);
                    info.percentagewage = info.SendFee * HJConvert.ToDecimal(dr["Section"])/100;
                    info.allwage = info.basewage + info.percentagewage;

                    info.deliverpayweb = info.getmoney - info.ShopHaveMoney;

                    info.IsDelete = HJConvert.ToInt32(dr["Unid"]);

                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 更新一个int字段的值 where是查询语句 需要带where 如：where DataId=1或者 where DataId in (1,2,3)
        /// </summary>
        public int UpdateValue(string param, int intValue, string Where)
        {
            return (int)SQLHelper.UpdateValue("Deliver", param, intValue, Where);
        }




    }
}
