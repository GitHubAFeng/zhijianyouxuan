using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

using Hangjing.Common;
using Hangjing.DBUtility;
using System.Collections.Generic;
using Hangjing.Model;

namespace Hangjing.SQLServerDAL
{
    /// <summary>
    ///商家类
    /// </summary>
    public class Points
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PointsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Points(");
            strSql.Append("InUse,ID,Name,Comm,PType,RcvType,PosAddr,PosRoom,PosAttch,NamePy,PosSrvAd,CommPerson,StopAM,StopPM,SendLimit,LoginName,Password,SendFee,SN1,SN2,Sn2Key,PTimes,MgrCell,PHead,PEnd,OpenTime,IsCallCenter,Address,Introduce,Status,outnitice,InTime,Time1Start,Time1End,IsDelete,SortNum,FlavorGrade,ServiceGrade,SpeedGrade,Star,category,ViewTimes,senttime,sentorg,special,reviewtimes,money,Inve1,menunum,Picture,showpicture,foodupdatetime,Time2Start,Time2End,bisnessStart,bisnessend,bisnessStart2,bisnessend2,point,showlocal,Grade,Pop,email,EBuilding,Opentimes1,Opentimes2,Closetimes1,Closetimes2,cityid,BigPicture,RefreshTime)");
            strSql.Append(" values (");
            strSql.Append("@InUse,@ID,@Name,@Comm,@PType,@RcvType,@PosAddr,@PosRoom,@PosAttch,@NamePy,@PosSrvAd,@CommPerson,@StopAM,@StopPM,@SendLimit,@LoginName,@Password,@SendFee,@SN1,@SN2,@Sn2Key,@PTimes,@MgrCell,@PHead,@PEnd,@OpenTime,@IsCallCenter,@Address,@Introduce,@Status,@outnitice,@InTime,@Time1Start,@Time1End,@IsDelete,@SortNum,@FlavorGrade,@ServiceGrade,@SpeedGrade,@Star,@category,@ViewTimes,@senttime,@sentorg,@special,@reviewtimes,@money,@Inve1,@menunum,@Picture,@showpicture,@foodupdatetime,@Time2Start,@Time2End,@bisnessStart,@bisnessend,@bisnessStart2,@bisnessend2,@point,@showlocal,@Grade,@Pop,@email,@EBuilding,@Opentimes1,@Opentimes2,@Closetimes1,@Closetimes2,@cityid,@BigPicture,@RefreshTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@InUse", SqlDbType.VarChar,50),
					new SqlParameter("@ID", SqlDbType.VarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Comm", SqlDbType.VarChar,50),
					new SqlParameter("@PType", SqlDbType.Int,4),
					new SqlParameter("@RcvType", SqlDbType.Int,4),
					new SqlParameter("@PosAddr", SqlDbType.VarChar,50),
					new SqlParameter("@PosRoom", SqlDbType.VarChar,50),
					new SqlParameter("@PosAttch", SqlDbType.VarChar,50),
					new SqlParameter("@NamePy", SqlDbType.VarChar,50),
					new SqlParameter("@PosSrvAd", SqlDbType.VarChar,50),
					new SqlParameter("@CommPerson", SqlDbType.VarChar,200),
					new SqlParameter("@StopAM", SqlDbType.DateTime),
					new SqlParameter("@StopPM", SqlDbType.DateTime),
					new SqlParameter("@SendLimit", SqlDbType.Decimal,5),
					new SqlParameter("@LoginName", SqlDbType.VarChar,50),
					new SqlParameter("@Password", SqlDbType.VarChar,50),
					new SqlParameter("@SendFee", SqlDbType.Decimal,9),
					new SqlParameter("@SN1", SqlDbType.VarChar,50),
					new SqlParameter("@SN2", SqlDbType.VarChar,50),
					new SqlParameter("@Sn2Key", SqlDbType.VarChar,50),
					new SqlParameter("@PTimes", SqlDbType.Int,4),
					new SqlParameter("@MgrCell", SqlDbType.VarChar,256),
					new SqlParameter("@PHead", SqlDbType.VarChar,50),
					new SqlParameter("@PEnd", SqlDbType.VarChar,50),
					new SqlParameter("@OpenTime", SqlDbType.VarChar,256),
					new SqlParameter("@IsCallCenter", SqlDbType.Int,4),
					new SqlParameter("@Address", SqlDbType.VarChar,256),
					new SqlParameter("@Introduce", SqlDbType.Text),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@outnitice", SqlDbType.VarChar,256),
					new SqlParameter("@InTime", SqlDbType.DateTime),
					new SqlParameter("@Time1Start", SqlDbType.DateTime),
					new SqlParameter("@Time1End", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@SortNum", SqlDbType.Int,4),
					new SqlParameter("@FlavorGrade", SqlDbType.Int,4),
					new SqlParameter("@ServiceGrade", SqlDbType.Int,4),
					new SqlParameter("@SpeedGrade", SqlDbType.Int,4),
					new SqlParameter("@Star", SqlDbType.Int,4),
					new SqlParameter("@category", SqlDbType.VarChar,256),
					new SqlParameter("@ViewTimes", SqlDbType.Int,4),
					new SqlParameter("@senttime", SqlDbType.Int,4),
					new SqlParameter("@sentorg", SqlDbType.VarChar,256),
					new SqlParameter("@special", SqlDbType.VarChar,256),
					new SqlParameter("@reviewtimes", SqlDbType.Int,4),
					new SqlParameter("@money", SqlDbType.Decimal,5),
					new SqlParameter("@Inve1", SqlDbType.Int,4),
					new SqlParameter("@menunum", SqlDbType.Int,4),
					new SqlParameter("@Picture", SqlDbType.VarChar,256),
					new SqlParameter("@showpicture", SqlDbType.Int,4),
					new SqlParameter("@foodupdatetime", SqlDbType.DateTime),
					new SqlParameter("@Time2Start", SqlDbType.DateTime),
					new SqlParameter("@Time2End", SqlDbType.DateTime),
                    new SqlParameter("@bisnessStart", SqlDbType.DateTime),
				    new SqlParameter("@bisnessend", SqlDbType.DateTime),
                    new SqlParameter("@bisnessStart2", SqlDbType.DateTime),
				    new SqlParameter("@bisnessend2", SqlDbType.DateTime),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@showlocal", SqlDbType.Int,4),
					new SqlParameter("@Grade", SqlDbType.Int,4),
					new SqlParameter("@Pop", SqlDbType.Int,4),
					new SqlParameter("@email", SqlDbType.VarChar,50),
                    new SqlParameter("@EBuilding", SqlDbType.Text),
                    new SqlParameter("@Opentimes1", SqlDbType.DateTime),
				    new SqlParameter("@Opentimes2", SqlDbType.DateTime),
                    new SqlParameter("@Closetimes1", SqlDbType.DateTime),
				    new SqlParameter("@Closetimes2", SqlDbType.DateTime),
                    new SqlParameter("@cityid",SqlDbType.Int,4),
                    new SqlParameter("@BigPicture",SqlDbType.VarChar,256),
                    new SqlParameter("@RefreshTime",SqlDbType.VarChar,50)
                                      
            };
            parameters[0].Value = model.InUse;
            parameters[1].Value = model.ID;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Comm;
            parameters[4].Value = model.PType;
            parameters[5].Value = model.RcvType;
            parameters[6].Value = model.PosAddr;
            parameters[7].Value = model.PosRoom;
            parameters[8].Value = model.PosAttch;
            parameters[9].Value = model.NamePy;
            parameters[10].Value = model.PosSrvAd;
            parameters[11].Value = model.CommPerson;
            parameters[12].Value = model.StopAM;
            parameters[13].Value = model.StopPM;
            parameters[14].Value = model.SendLimit;
            parameters[15].Value = model.LoginName == null ? "" : model.LoginName;
            parameters[16].Value = model.Password == null ? "" : model.Password;
            parameters[17].Value = model.SendFee;
            parameters[18].Value = model.SN1;
            parameters[19].Value = model.SN2;
            parameters[20].Value = model.Sn2Key;
            parameters[21].Value = model.PTimes;
            parameters[22].Value = model.MgrCell;
            parameters[23].Value = model.PHead;
            parameters[24].Value = model.PEnd;
            parameters[25].Value = model.OpenTime;
            parameters[26].Value = model.IsCallCenter;
            parameters[27].Value = model.Address;
            parameters[28].Value = model.Introduce;
            parameters[29].Value = model.Status;
            parameters[30].Value = model.outnitice;
            parameters[31].Value = model.InTime;
            parameters[32].Value = model.Time1Start;
            parameters[33].Value = model.Time1End;
            parameters[34].Value = model.IsDelete;
            parameters[35].Value = model.SortNum;
            parameters[36].Value = model.FlavorGrade;
            parameters[37].Value = model.ServiceGrade;
            parameters[38].Value = model.SpeedGrade;
            parameters[39].Value = model.Star;
            parameters[40].Value = model.category;
            parameters[41].Value = model.ViewTimes;
            parameters[42].Value = model.senttime;
            parameters[43].Value = model.sentorg;
            parameters[44].Value = model.special;
            parameters[45].Value = model.reviewtimes;
            parameters[46].Value = model.money;
            parameters[47].Value = model.Inve1;
            parameters[48].Value = 0;
            parameters[49].Value = model.Picture;
            parameters[50].Value = model.showpicture;
            parameters[51].Value = model.foodupdatetime;
            parameters[52].Value = model.Time2Start;
            parameters[53].Value = model.Time2End;
            parameters[54].Value = model.bisnessStart;
            parameters[55].Value = model.bisnessend;
            parameters[56].Value = model.bisnessStart2;
            parameters[57].Value = model.bisnessend2;
            parameters[58].Value = model.point;
            parameters[59].Value = model.showlocal;
            parameters[60].Value = model.Grade;
            parameters[61].Value = model.pop;
            parameters[62].Value = model.email;
            parameters[63].Value = model.EBuilding == null ? "" : model.EBuilding;
            parameters[64].Value = model.Opentimes1;
            parameters[65].Value = model.Opentimes2;
            parameters[66].Value = model.Closetimes1;
            parameters[67].Value = model.Closetimes2;
            parameters[68].Value = model.cityid;
            parameters[69].Value = model.BigPicture;
            parameters[70].Value = model.RefreshTime;
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.PointsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Points set ");
            strSql.Append("InUse=@InUse,");
            strSql.Append("ID=@ID,");
            strSql.Append("Name=@Name,");
            strSql.Append("Comm=@Comm,");
            strSql.Append("PType=@PType,");
            strSql.Append("RcvType=@RcvType,");
            strSql.Append("PosAddr=@PosAddr,");
            strSql.Append("PosRoom=@PosRoom,");
            strSql.Append("PosAttch=@PosAttch,");
            strSql.Append("NamePy=@NamePy,");
            strSql.Append("PosSrvAd=@PosSrvAd,");
            strSql.Append("CommPerson=@CommPerson,");
            strSql.Append("StopAM=@StopAM,");
            strSql.Append("StopPM=@StopPM,");
            strSql.Append("SendLimit=@SendLimit,");
            strSql.Append("LoginName=@LoginName,");
            strSql.Append("Password=@Password,");
            strSql.Append("SendFee=@SendFee,");
            strSql.Append("SN1=@SN1,");
            strSql.Append("SN2=@SN2,");
            strSql.Append("Sn2Key=@Sn2Key,");
            strSql.Append("PTimes=@PTimes,");
            strSql.Append("MgrCell=@MgrCell,");
            strSql.Append("PHead=@PHead,");
            strSql.Append("PEnd=@PEnd,");
            strSql.Append("OpenTime=@OpenTime,");
            strSql.Append("IsCallCenter=@IsCallCenter,");
            strSql.Append("Address=@Address,");
            strSql.Append("Introduce=@Introduce,");
            strSql.Append("Status=@Status,");
            strSql.Append("outnitice=@outnitice,");
            strSql.Append("InTime=@InTime,");
            strSql.Append("Time1Start=@Time1Start,");
            strSql.Append("Time1End=@Time1End,");
            strSql.Append("IsDelete=@IsDelete,");
            strSql.Append("SortNum=@SortNum,");
            strSql.Append("FlavorGrade=@FlavorGrade,");
            strSql.Append("ServiceGrade=@ServiceGrade,");
            strSql.Append("SpeedGrade=@SpeedGrade,");
            strSql.Append("Star=@Star,");
            strSql.Append("category=@category,");
            strSql.Append("ViewTimes=@ViewTimes,");
            strSql.Append("senttime=@senttime,");
            strSql.Append("sentorg=@sentorg,");
            strSql.Append("special=@special,");
            strSql.Append("reviewtimes=@reviewtimes,");
            strSql.Append("money=@money,");
            strSql.Append("Inve1=@Inve1,");
            strSql.Append("menunum=@menunum,");
            strSql.Append("Picture=@Picture,");
            strSql.Append("showpicture=@showpicture,");
            strSql.Append("foodupdatetime=@foodupdatetime,");
            strSql.Append("Time2Start=@Time2Start,");
            strSql.Append("Time2End=@Time2End,");
            strSql.Append("bisnessStart=@bisnessStart,");
            strSql.Append("bisnessend=@bisnessend,");
            strSql.Append("bisnessStart2=@bisnessStart2,");
            strSql.Append("bisnessend2=@bisnessend2,");
            strSql.Append("point=@point,");
            strSql.Append("showlocal=@showlocal,");
            strSql.Append("Grade=@Grade,");
            strSql.Append("pop=@pop,");
            strSql.Append("email=@email,");
            strSql.Append("EBuilding=@EBuilding, ");
            strSql.Append("Opentimes1=@Opentimes1, ");
            strSql.Append("Opentimes2=@Opentimes2, ");
            strSql.Append("Closetimes1=@Closetimes1, ");
            strSql.Append("Closetimes2=@Closetimes2, ");
            strSql.Append("cityid=@cityid,BigPicture=@BigPicture ,");
            strSql.Append("licensePic=@licensePic,isLicense=@isLicense, ");
            strSql.Append("cateringPic=@cateringPic,");
            strSql.Append("isCatering=@isCatering,");
            strSql.Append("RefreshTime=@RefreshTime ");
            
            strSql.Append(" where Unid=@Unid ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@Unid", SqlDbType.Int,4),
				new SqlParameter("@InUse", SqlDbType.VarChar,1),
				new SqlParameter("@ID", SqlDbType.VarChar,3),
				new SqlParameter("@Name", SqlDbType.NVarChar,50),
				new SqlParameter("@Comm", SqlDbType.VarChar,20),
				new SqlParameter("@PType", SqlDbType.Int,4),
				new SqlParameter("@RcvType", SqlDbType.Int,4),
				new SqlParameter("@PosAddr", SqlDbType.VarChar,12),
				new SqlParameter("@PosRoom", SqlDbType.VarChar,15),
				new SqlParameter("@PosAttch", SqlDbType.VarChar,20),
				new SqlParameter("@NamePy", SqlDbType.VarChar,5),
				new SqlParameter("@PosSrvAd", SqlDbType.VarChar,12),
				new SqlParameter("@CommPerson", SqlDbType.VarChar,200),
				new SqlParameter("@StopAM", SqlDbType.DateTime),
				new SqlParameter("@StopPM", SqlDbType.DateTime),
				new SqlParameter("@SendLimit", SqlDbType.Decimal,5),
				new SqlParameter("@LoginName", SqlDbType.VarChar,10),
				new SqlParameter("@Password", SqlDbType.VarChar,50),
				new SqlParameter("@SendFee", SqlDbType.Decimal,9),
				new SqlParameter("@SN1", SqlDbType.VarChar,18),
				new SqlParameter("@SN2", SqlDbType.VarChar,18),
				new SqlParameter("@Sn2Key", SqlDbType.VarChar,18),
				new SqlParameter("@PTimes", SqlDbType.Int,4),
				new SqlParameter("@MgrCell", SqlDbType.VarChar,256),
				new SqlParameter("@PHead", SqlDbType.VarChar,32),
				new SqlParameter("@PEnd", SqlDbType.VarChar,32),
				new SqlParameter("@OpenTime", SqlDbType.VarChar,256),
				new SqlParameter("@IsCallCenter", SqlDbType.Int,4),
				new SqlParameter("@Address", SqlDbType.VarChar,256),
				new SqlParameter("@Introduce", SqlDbType.Text),
				new SqlParameter("@Status", SqlDbType.Int,4),
				new SqlParameter("@outnitice", SqlDbType.VarChar,256),
				new SqlParameter("@InTime", SqlDbType.DateTime),
				new SqlParameter("@Time1Start", SqlDbType.DateTime),
				new SqlParameter("@Time1End", SqlDbType.DateTime),
				new SqlParameter("@IsDelete", SqlDbType.Int,4),
				new SqlParameter("@SortNum", SqlDbType.Int,4),
				new SqlParameter("@FlavorGrade", SqlDbType.Int,4),
				new SqlParameter("@ServiceGrade", SqlDbType.Int,4),
				new SqlParameter("@SpeedGrade", SqlDbType.Int,4),
				new SqlParameter("@Star", SqlDbType.Int,4),
				new SqlParameter("@category", SqlDbType.VarChar,256),
				new SqlParameter("@ViewTimes", SqlDbType.Int,4),
				new SqlParameter("@senttime", SqlDbType.Int,4),
				new SqlParameter("@sentorg", SqlDbType.VarChar,256),
				new SqlParameter("@special", SqlDbType.VarChar,256),
				new SqlParameter("@reviewtimes", SqlDbType.Int,4),
				new SqlParameter("@money", SqlDbType.Decimal,5),
				new SqlParameter("@Inve1", SqlDbType.Int,4),
				new SqlParameter("@menunum", SqlDbType.Int,4),
				new SqlParameter("@Picture", SqlDbType.VarChar,256),
				new SqlParameter("@showpicture", SqlDbType.Int,4),
				new SqlParameter("@foodupdatetime", SqlDbType.DateTime),
				new SqlParameter("@Time2Start", SqlDbType.DateTime),
				new SqlParameter("@Time2End", SqlDbType.DateTime),
				new SqlParameter("@bisnessStart", SqlDbType.DateTime),
				new SqlParameter("@bisnessend", SqlDbType.DateTime),
                new SqlParameter("@bisnessStart2", SqlDbType.DateTime),
				new SqlParameter("@bisnessend2", SqlDbType.DateTime),
				new SqlParameter("@point", SqlDbType.Int,4),
				new SqlParameter("@showlocal", SqlDbType.Int,4),
                new SqlParameter("@Grade", SqlDbType.Int,4),
                new SqlParameter("@pop", SqlDbType.Int,4),
                new SqlParameter("@email", SqlDbType.VarChar,100),
                new SqlParameter("@EBuilding", SqlDbType.Text),
                new SqlParameter("@Opentimes1", SqlDbType.DateTime),
				new SqlParameter("@Opentimes2", SqlDbType.DateTime),
                new SqlParameter("@Closetimes1", SqlDbType.DateTime),
				new SqlParameter("@Closetimes2", SqlDbType.DateTime),
                new SqlParameter("@cityid", SqlDbType.Int,4),
                new SqlParameter("@BigPicture", SqlDbType.VarChar,256),

                new SqlParameter("@licensePic", SqlDbType.VarChar,256),
                new SqlParameter("@isLicense", SqlDbType.Int,4),
                new SqlParameter("@cateringPic", SqlDbType.VarChar,256),
                new SqlParameter("@isCatering", SqlDbType.Int,4),
                new SqlParameter("@RefreshTime", SqlDbType.VarChar,50)
                
            };
            parameters[0].Value = model.Unid;
            parameters[1].Value = model.InUse;
            parameters[2].Value = model.ID;
            parameters[3].Value = model.Name;
            parameters[4].Value = model.Comm;
            parameters[5].Value = model.PType;
            parameters[6].Value = model.RcvType;
            parameters[7].Value = model.PosAddr;
            parameters[8].Value = model.PosRoom;
            parameters[9].Value = model.PosAttch;
            parameters[10].Value = model.NamePy;
            parameters[11].Value = model.PosSrvAd;
            parameters[12].Value = model.CommPerson;
            parameters[13].Value = model.StopAM;
            parameters[14].Value = model.StopPM;
            parameters[15].Value = model.SendLimit;
            parameters[16].Value = model.LoginName;
            parameters[17].Value = model.Password;
            parameters[18].Value = model.SendFee;
            parameters[19].Value = model.SN1;
            parameters[20].Value = model.SN2;
            parameters[21].Value = model.Sn2Key;
            parameters[22].Value = model.PTimes;
            parameters[23].Value = model.MgrCell;
            parameters[24].Value = model.PHead;
            parameters[25].Value = model.PEnd;
            parameters[26].Value = model.OpenTime;
            parameters[27].Value = model.IsCallCenter;
            parameters[28].Value = model.Address;
            parameters[29].Value = model.Introduce;
            parameters[30].Value = model.Status;
            parameters[31].Value = model.outnitice;
            parameters[32].Value = model.InTime;
            parameters[33].Value = model.Time1Start;
            parameters[34].Value = model.Time1End;
            parameters[35].Value = model.IsDelete;
            parameters[36].Value = model.SortNum;
            parameters[37].Value = model.FlavorGrade;
            parameters[38].Value = model.ServiceGrade;
            parameters[39].Value = model.SpeedGrade;
            parameters[40].Value = model.Star;
            parameters[41].Value = model.category;
            parameters[42].Value = model.ViewTimes;
            parameters[43].Value = model.senttime;
            parameters[44].Value = model.sentorg;
            parameters[45].Value = model.special;
            parameters[46].Value = model.reviewtimes;
            parameters[47].Value = model.money;
            parameters[48].Value = model.Inve1;
            parameters[49].Value = model.menunum;
            parameters[50].Value = model.Picture;
            parameters[51].Value = model.showpicture;
            parameters[52].Value = model.foodupdatetime;
            parameters[53].Value = model.Time2Start;
            parameters[54].Value = model.Time2End;
            parameters[55].Value = model.bisnessStart;
            parameters[56].Value = model.bisnessend;
            parameters[57].Value = model.bisnessStart2;
            parameters[58].Value = model.bisnessend2;
            parameters[59].Value = model.point;
            parameters[60].Value = model.showlocal;
            parameters[61].Value = model.Grade;
            parameters[62].Value = model.pop;
            parameters[63].Value = model.email;

            parameters[64].Value = model.EBuilding;
            parameters[65].Value = model.Opentimes1;
            parameters[66].Value = model.Opentimes2;
            parameters[67].Value = model.Closetimes1;
            parameters[68].Value = model.Closetimes2;
            parameters[69].Value = model.cityid;
            parameters[70].Value = model.BigPicture;

            parameters[71].Value = model.licensePic;
            parameters[72].Value = model.isLicense;
            parameters[73].Value = model.cateringPic;
            parameters[74].Value = model.isCatering;
            parameters[75].Value = model.RefreshTime;


            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Updatel(int Inve1, int Unid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Points set ");
            strSql.Append("Inve1=@Inve1");
            strSql.Append(" where Unid=@Unid ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@Unid", SqlDbType.Int,4),
				new SqlParameter("@Inve1", SqlDbType.Int,4)
            };
            parameters[0].Value = Unid;
            parameters[1].Value = Inve1;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>Unid</param>
        /// <returns>PointsInfo</returns>
        public PointsInfo GetModel(int Unid)
        {
            string sql = "select *";
            sql += ", ";
            sql += " CASE WHEN( ( CONVERT(varchar(12) , Opentimes1, 114 ) < CONVERT(varchar(12) , getdate(), 114 )";
            sql += "and CONVERT(varchar(12) , Opentimes2, 114 ) > CONVERT(varchar(12) , getdate(), 114 )";
            sql += ") or  ( CONVERT(varchar(12) , Closetimes1, 114 ) < CONVERT(varchar(12) , getdate(), 114 )";
            sql += "and CONVERT(varchar(12) , Closetimes2, 114 ) > CONVERT(varchar(12) , getdate(), 114 )";
            sql += ") )THEN 1 ELSE 0 END AS havenew";
            sql += " from Points where  Unid = @Unid";
            SqlParameter parameter = new SqlParameter("@Unid", SqlDbType.Int, 4);
            parameter.Value = Unid;
            PointsInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    model = new PointsInfo();
                    model.Unid = HJConvert.ToInt32(dr["Unid"]);
                    model.InUse = HJConvert.ToString(dr["InUse"]);
                    model.ID = HJConvert.ToString(dr["ID"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.Comm = HJConvert.ToString(dr["Comm"]);
                    model.PType = HJConvert.ToInt32(dr["PType"]);
                    model.RcvType = HJConvert.ToInt32(dr["RcvType"]);
                    model.PosAddr = HJConvert.ToString(dr["PosAddr"]);
                    model.PosRoom = HJConvert.ToString(dr["PosRoom"]);
                    model.PosAttch = HJConvert.ToString(dr["PosAttch"]);
                    model.NamePy = HJConvert.ToString(dr["NamePy"]);
                    model.PosSrvAd = HJConvert.ToString(dr["PosSrvAd"]);
                    model.CommPerson = HJConvert.ToString(dr["CommPerson"]);
                    model.StopAM = HJConvert.ToDateTime(dr["StopAM"]);
                    model.StopPM = HJConvert.ToDateTime(dr["StopPM"]);
                    model.SendLimit = HJConvert.ToDecimal(dr["SendLimit"]);
                    model.LoginName = HJConvert.ToString(dr["LoginName"]);
                    model.Password = HJConvert.ToString(dr["Password"]);
                    model.SendFee = HJConvert.ToDecimal(dr["SendFee"]);
                    model.SN1 = HJConvert.ToString(dr["SN1"]);
                    model.SN2 = HJConvert.ToString(dr["SN2"]);
                    model.Sn2Key = HJConvert.ToString(dr["Sn2Key"]);
                    model.PTimes = HJConvert.ToInt32(dr["PTimes"]);
                    model.MgrCell = HJConvert.ToString(dr["MgrCell"]);
                    model.PHead = HJConvert.ToString(dr["PHead"]);
                    model.PEnd = HJConvert.ToString(dr["PEnd"]);
                    model.OpenTime = HJConvert.ToString(dr["OpenTime"]);
                    model.IsCallCenter = HJConvert.ToInt32(dr["IsCallCenter"]);
                    model.Address = HJConvert.ToString(dr["Address"]);
                    model.Introduce = HJConvert.ToString(dr["Introduce"]);
                    model.Status = HJConvert.ToInt32(dr["Status"]);
                    model.outnitice = HJConvert.ToString(dr["outnitice"]);
                    model.InTime = HJConvert.ToDateTime(dr["InTime"]);
                    model.Time1Start = HJConvert.ToDateTime(dr["Time1Start"]);
                    model.Time1End = HJConvert.ToDateTime(dr["Time1End"]);
                    model.IsDelete = HJConvert.ToInt32(dr["IsDelete"]);
                    model.SortNum = HJConvert.ToInt32(dr["SortNum"]);
                    model.FlavorGrade = HJConvert.ToInt32(dr["FlavorGrade"]);
                    model.ServiceGrade = HJConvert.ToInt32(dr["ServiceGrade"]);
                    model.SpeedGrade = HJConvert.ToInt32(dr["SpeedGrade"]);
                    model.Star = HJConvert.ToInt32(dr["Star"]);
                    model.category = HJConvert.ToString(dr["category"]);
                    model.ViewTimes = HJConvert.ToInt32(dr["ViewTimes"]);
                    model.senttime = HJConvert.ToInt32(dr["senttime"]);
                    model.sentorg = HJConvert.ToString(dr["sentorg"]);
                    model.special = HJConvert.ToString(dr["special"]);
                    model.reviewtimes = HJConvert.ToInt32(dr["reviewtimes"]);
                    model.money = HJConvert.ToDecimal(dr["money"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.menunum = HJConvert.ToInt32(dr["menunum"]);
                    model.Picture = HJConvert.ToString(dr["Picture"]);
                    model.showpicture = HJConvert.ToInt32(dr["showpicture"]);
                    model.foodupdatetime = HJConvert.ToDateTime(dr["foodupdatetime"]);
                    model.Time2Start = HJConvert.ToDateTime(dr["Time2Start"]);
                    model.Time2End = HJConvert.ToDateTime(dr["Time2End"]);
                    model.bisnessStart = HJConvert.ToDateTime(dr["bisnessStart"]);
                    model.bisnessend = HJConvert.ToDateTime(dr["bisnessend"]);
                    model.bisnessStart2 = HJConvert.ToDateTime(dr["bisnessStart2"]);
                    model.bisnessend2 = HJConvert.ToDateTime(dr["bisnessend2"]);
                    model.point = HJConvert.ToInt32(dr["point"]);
                    model.showlocal = HJConvert.ToInt32(dr["showlocal"]);
                    model.Grade = HJConvert.ToInt32(dr["Grade"]);
                    model.pop = HJConvert.ToInt32(dr["Pop"]);
                    model.email = HJConvert.ToString(dr["email"]);
                    model.EBuilding = HJConvert.ToString(dr["EBuilding"]);
                    model.Opentimes1 = HJConvert.ToDateTime(dr["Opentimes1"]);
                    model.Opentimes2 = HJConvert.ToDateTime(dr["Opentimes2"]);
                    model.Closetimes1 = HJConvert.ToDateTime(dr["Closetimes1"]);
                    model.Closetimes2 = HJConvert.ToDateTime(dr["Closetimes2"]);
                    model.cityid = HJConvert.ToInt32(dr["cityid"]);
                    model.BigPicture = HJConvert.ToString(dr["BigPicture"]);
                    model.isbisness = HJConvert.ToInt32(dr["havenew"]);

                    model.licensePic = HJConvert.ToString(dr["licensePic"]);
                    model.isLicense = HJConvert.ToInt32(dr["isLicense"]);
                    model.cateringPic = HJConvert.ToString(dr["cateringPic"]);
                    model.isCatering = HJConvert.ToInt32(dr["isCatering"]);
                    model.RefreshTime = HJConvert.ToString(dr["RefreshTime"]);
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
            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "Points"), new SqlParameter("@strWhere", strWhere));
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
        public IList<PointsInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<PointsInfo> infos = new List<PointsInfo>();
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
            string field = ", ";
            field += " CASE WHEN( ( CONVERT(varchar(12) , Opentimes1, 114 ) < CONVERT(varchar(12) , getdate(), 114 )";
            field += "and CONVERT(varchar(12) , Opentimes2, 114 ) > CONVERT(varchar(12) , getdate(), 114 )";
            field += ") or  ( CONVERT(varchar(12) , Closetimes1, 114 ) < CONVERT(varchar(12) , getdate(), 114 )";
            field += "and CONVERT(varchar(12) , Closetimes2, 114 ) > CONVERT(varchar(12) , getdate(), 114 )";
            field += ") )THEN 1 ELSE 0 END AS havenew";
            parameters[0].Value = "Points";
            parameters[1].Value = "*" + field + "";
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
                    PointsInfo info = new PointsInfo();
                    info.Unid = HJConvert.ToInt32(dr["Unid"]);
                    info.InUse = HJConvert.ToString(dr["InUse"]);
                    info.ID = HJConvert.ToString(dr["ID"]);
                    info.Name = HJConvert.ToString(dr["Name"]);
                    info.Comm = HJConvert.ToString(dr["Comm"]);
                    info.PType = HJConvert.ToInt32(dr["PType"]);
                    info.RcvType = HJConvert.ToInt32(dr["RcvType"]);
                    info.PosAddr = HJConvert.ToString(dr["PosAddr"]);
                    info.PosRoom = HJConvert.ToString(dr["PosRoom"]);
                    info.PosAttch = HJConvert.ToString(dr["PosAttch"]);
                    info.NamePy = HJConvert.ToString(dr["NamePy"]);
                    info.PosSrvAd = HJConvert.ToString(dr["PosSrvAd"]);
                    info.CommPerson = HJConvert.ToString(dr["CommPerson"]);
                    info.StopAM = HJConvert.ToDateTime(dr["StopAM"]);
                    info.StopPM = HJConvert.ToDateTime(dr["StopPM"]);
                    info.SendLimit = HJConvert.ToDecimal(dr["SendLimit"]);
                    info.LoginName = HJConvert.ToString(dr["LoginName"]);
                    info.Password = HJConvert.ToString(dr["Password"]);
                    info.SendFee = HJConvert.ToDecimal(dr["SendFee"]);
                    info.SN1 = HJConvert.ToString(dr["SN1"]);
                    info.SN2 = HJConvert.ToString(dr["SN2"]);
                    info.Sn2Key = HJConvert.ToString(dr["Sn2Key"]);
                    info.PTimes = HJConvert.ToInt32(dr["PTimes"]);
                    info.MgrCell = HJConvert.ToString(dr["MgrCell"]);
                    info.PHead = HJConvert.ToString(dr["PHead"]);
                    info.PEnd = HJConvert.ToString(dr["PEnd"]);
                    info.OpenTime = HJConvert.ToString(dr["OpenTime"]);
                    info.IsCallCenter = HJConvert.ToInt32(dr["IsCallCenter"]);
                    info.Address = HJConvert.ToString(dr["Address"]);
                    info.Introduce = HJConvert.ToString(dr["Introduce"]);
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.outnitice = HJConvert.ToString(dr["outnitice"]);
                    info.InTime = HJConvert.ToDateTime(dr["InTime"]);
                    info.Time1Start = HJConvert.ToDateTime(dr["Time1Start"]);
                    info.Time1End = HJConvert.ToDateTime(dr["Time1End"]);
                    info.IsDelete = HJConvert.ToInt32(dr["IsDelete"]);
                    info.SortNum = HJConvert.ToInt32(dr["SortNum"]);
                    info.FlavorGrade = HJConvert.ToInt32(dr["FlavorGrade"]);
                    info.ServiceGrade = HJConvert.ToInt32(dr["ServiceGrade"]);
                    info.SpeedGrade = HJConvert.ToInt32(dr["SpeedGrade"]);
                    info.Star = HJConvert.ToInt32(dr["Star"]);
                    info.category = HJConvert.ToString(dr["category"]);
                    info.ViewTimes = HJConvert.ToInt32(dr["ViewTimes"]);
                    info.senttime = HJConvert.ToInt32(dr["senttime"]);
                    info.sentorg = HJConvert.ToString(dr["sentorg"]);
                    info.special = HJConvert.ToString(dr["special"]);
                    info.reviewtimes = HJConvert.ToInt32(dr["reviewtimes"]);
                    info.money = HJConvert.ToDecimal(dr["money"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.menunum = HJConvert.ToInt32(dr["menunum"]);
                    info.Picture = HJConvert.ToString(dr["Picture"]);
                    info.showpicture = HJConvert.ToInt32(dr["showpicture"]);
                    info.foodupdatetime = HJConvert.ToDateTime(dr["foodupdatetime"]);
                    info.Time2Start = HJConvert.ToDateTime(dr["Time2Start"]);
                    info.Time2End = HJConvert.ToDateTime(dr["Time2End"]);
                    info.bisnessStart = HJConvert.ToDateTime(dr["bisnessStart"]);
                    info.bisnessend = HJConvert.ToDateTime(dr["bisnessend"]);
                    info.bisnessStart2 = HJConvert.ToDateTime(dr["bisnessStart2"]);
                    info.bisnessend2 = HJConvert.ToDateTime(dr["bisnessend2"]);
                    info.point = HJConvert.ToInt32(dr["point"]);
                    info.showlocal = HJConvert.ToInt32(dr["showlocal"]);
                    info.Grade = HJConvert.ToInt32(dr["Grade"]);
                    info.pop = HJConvert.ToInt32(dr["Pop"]);
                    info.email = HJConvert.ToString(dr["email"]);
                    info.isbisness = HJConvert.ToInt32(dr["havenew"]);
                    info.Opentimes1 = HJConvert.ToDateTime(dr["Opentimes1"]);
                    info.Opentimes2 = HJConvert.ToDateTime(dr["Opentimes2"]);
                    info.Closetimes1 = HJConvert.ToDateTime(dr["Closetimes1"]);
                    info.Closetimes2 = HJConvert.ToDateTime(dr["Closetimes2"]);
                    info.cityid = HJConvert.ToInt32(dr["cityid"]);
                    info.RefreshTime = HJConvert.ToString(dr["RefreshTime"]);

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
        public IList<PointsInfo> GetListtop(int top, string strWhere)
        {
            IList<PointsInfo> infos = new List<PointsInfo>();
            string field = ", ";
            field += " CASE WHEN( ( CONVERT(varchar(12) , Opentimes1, 114 ) < CONVERT(varchar(12) , getdate(), 114 )";
            field += "and CONVERT(varchar(12) , Opentimes2, 114 ) > CONVERT(varchar(12) , getdate(), 114 )";
            field += ") or  ( CONVERT(varchar(12) , Closetimes1, 114 ) < CONVERT(varchar(12) , getdate(), 114 )";
            field += "and CONVERT(varchar(12) , Closetimes2, 114 ) > CONVERT(varchar(12) , getdate(), 114 )";
            field += ") )THEN 1 ELSE 0 END AS havenew";
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, "select top(" + top + ") Unid,Name,Status,category,OpenTime,Picture " + field + " from Points where " + strWhere + "", null))
            {
                while (dr.Read())
                {
                    PointsInfo info = new PointsInfo();
                    info.Unid = HJConvert.ToInt32(dr["Unid"]);


                    info.Name = HJConvert.ToString(dr["Name"]);

                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.category = HJConvert.ToString(dr["category"]);
                    info.OpenTime = HJConvert.ToString(dr["OpenTime"]);
                    info.Picture = HJConvert.ToString(dr["Picture"]);
                    info.isbisness = HJConvert.ToInt32(dr["havenew"]);

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
        public int DelPoints(int Id)
        {
            return DelList(Id.ToString());
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

            //删除其他
            str.Append("update points set InUse='n',loginname='' where Unid in ({0});");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="TogoNum"></param>
        /// <param name="NewPwd"></param>
        /// <returns></returns>
        public int ResetPassword(int TogoNum, string NewPwd)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update points set Password=@NewPwd where Unid=@TogoNum");

            SqlParameter[] Parameters = {
                new SqlParameter("@NewPwd",SqlDbType.VarChar,50),
                 new SqlParameter("@TogoNum",SqlDbType.Int,4)
            };
            Parameters[0].Value = NewPwd;
            Parameters[1].Value = TogoNum;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Parameters);
        }
        /// <summary>
        /// 重置密码(一)
        /// </summary>
        /// <param name="TogoNum"></param>
        /// <param name="NewPwd"></param>
        /// <returns></returns>
        public int ResetPassword(string EMail, string NewPwd)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update points set Password=@NewPwd where EMail=@EMail");

            SqlParameter[] Para = 
            {
                new SqlParameter("@EMail",SqlDbType.VarChar,50),
                new SqlParameter("@NewPwd",SqlDbType.VarChar,50)
            };
            Para[0].Value = EMail;
            Para[1].Value = NewPwd;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 更新商家营业状态 商家中心设置使用
        /// </summary>
        /// <param name="ShopId">商家编号</param>
        /// <param name="Status">状态</param>
        /// <param name="Alert">提示信息</param>
        /// <returns></returns>
        public int UpateStatus(int ShopId, int Status, string Alert)
        {

            StringBuilder str = new StringBuilder();
            str.Append("update points set Status=@Status ,outnitice=@outnitice where unid=@unid");

            SqlParameter[] Parameters = {
                new SqlParameter("@unid",SqlDbType.Int,4),
                 new SqlParameter("@Status",SqlDbType.Int,4),
                 new SqlParameter("@outnitice",SqlDbType.VarChar,50)

            };
            Parameters[0].Value = ShopId;
            Parameters[1].Value = Status;
            Parameters[2].Value = Alert;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Parameters);
        }


        /// <summary>
        /// 更新一个DateTime字段的值 where是查询语句 需要带where 如：where DataId=1或者 where DataId in (1,2,3)
        /// </summary>
        public int UpdateValue(string param, DateTime Value, string Where)
        {
            return (int)SQLHelper.UpdateValue("Points", param, Value, Where);
        }

        /// <summary>
        /// 更新一个int字段的值 where是查询语句 需要带where 如：where DataId=1或者 where DataId in (1,2,3)
        /// </summary>
        public int UpdateValue(string param, int intValue, string Where)
        {
            return (int)SQLHelper.UpdateValue("Points", param, intValue, Where);
        }

        /// <summary>
        /// 更新一个string字段的值
        /// where是查询语句 需要带where 如：where DataId=1或者 where DataId in (1,2,3)
        ///</summary>
        public int UpdateValue(string param, string strValue, string Where)
        {
            return (int)SQLHelper.UpdateValue("Points", param, strValue, Where);
        }

        /// <summary>
        /// 更新一个string字段的值
        /// where是查询语句 需要带where 如：where DataId=1或者 where DataId in (1,2,3)
        ///</summary>
        public int UpdateValue(string param, decimal strValue, string Where)
        {
            return (int)SQLHelper.UpdateValue("Points", param, strValue, Where);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>Unid</param>
        /// <returns>PointsInfo</returns>
        public PointsInfo GetModel(string name, string pwd)
        {
            string sql = "select * from Points where  LoginName = @LoginName and Password=@Password";
            SqlParameter[] parameters = 
			{
				new SqlParameter("@LoginName", SqlDbType.VarChar,10),
				new SqlParameter("@Password", SqlDbType.VarChar,50)
			};
            parameters[0].Value = name;
            parameters[1].Value = pwd;
            PointsInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameters))
            {
                if (dr.Read())
                {
                    model = new PointsInfo();
                    model.Unid = HJConvert.ToInt32(dr["Unid"]);
                    model.InUse = HJConvert.ToString(dr["InUse"]);
                    model.ID = HJConvert.ToString(dr["ID"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.Comm = HJConvert.ToString(dr["Comm"]);
                    model.PType = HJConvert.ToInt32(dr["PType"]);
                    model.RcvType = HJConvert.ToInt32(dr["RcvType"]);
                    model.PosAddr = HJConvert.ToString(dr["PosAddr"]);
                    model.PosRoom = HJConvert.ToString(dr["PosRoom"]);
                    model.PosAttch = HJConvert.ToString(dr["PosAttch"]);
                    model.NamePy = HJConvert.ToString(dr["NamePy"]);
                    model.PosSrvAd = HJConvert.ToString(dr["PosSrvAd"]);
                    model.CommPerson = HJConvert.ToString(dr["CommPerson"]);
                    model.SendLimit = HJConvert.ToDecimal(dr["SendLimit"]);
                    model.LoginName = HJConvert.ToString(dr["LoginName"]);
                    model.Password = HJConvert.ToString(dr["Password"]);
                    model.SendFee = HJConvert.ToDecimal(dr["SendFee"]);
                    model.SN1 = HJConvert.ToString(dr["SN1"]);
                    model.SN2 = HJConvert.ToString(dr["SN2"]);
                    model.Sn2Key = HJConvert.ToString(dr["Sn2Key"]);
                    model.PTimes = HJConvert.ToInt32(dr["PTimes"]);
                    model.MgrCell = HJConvert.ToString(dr["MgrCell"]);
                    model.PHead = HJConvert.ToString(dr["PHead"]);
                    model.PEnd = HJConvert.ToString(dr["PEnd"]);
                    model.OpenTime = HJConvert.ToString(dr["OpenTime"]);
                    model.IsCallCenter = HJConvert.ToInt32(dr["IsCallCenter"]);
                    model.Address = HJConvert.ToString(dr["Address"]);
                    model.Introduce = HJConvert.ToString(dr["Introduce"]);
                    model.Status = HJConvert.ToInt32(dr["Status"]);
                    model.outnitice = HJConvert.ToString(dr["outnitice"]);
                    model.InTime = HJConvert.ToDateTime(dr["InTime"]);
                    model.Time1Start = HJConvert.ToDateTime(dr["Time1Start"]);
                    model.Time1End = HJConvert.ToDateTime(dr["Time1End"]);
                    model.IsDelete = HJConvert.ToInt32(dr["IsDelete"]);
                    model.SortNum = HJConvert.ToInt32(dr["SortNum"]);
                    model.FlavorGrade = HJConvert.ToInt32(dr["FlavorGrade"]);
                    model.ServiceGrade = HJConvert.ToInt32(dr["ServiceGrade"]);
                    model.SpeedGrade = HJConvert.ToInt32(dr["SpeedGrade"]);
                    model.Star = HJConvert.ToInt32(dr["Star"]);
                    model.category = HJConvert.ToString(dr["category"]);
                    model.ViewTimes = HJConvert.ToInt32(dr["ViewTimes"]);
                    model.senttime = HJConvert.ToInt32(dr["senttime"]);
                    model.sentorg = HJConvert.ToString(dr["sentorg"]);
                    model.special = HJConvert.ToString(dr["special"]);
                    model.reviewtimes = HJConvert.ToInt32(dr["reviewtimes"]);
                    model.money = HJConvert.ToDecimal(dr["money"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.menunum = HJConvert.ToInt32(dr["menunum"]);
                    model.Picture = HJConvert.ToString(dr["Picture"]);
                    model.showpicture = HJConvert.ToInt32(dr["showpicture"]);
                    model.foodupdatetime = HJConvert.ToDateTime(dr["foodupdatetime"]);
                    model.Time2Start = HJConvert.ToDateTime(dr["Time2Start"]);
                    model.Time2End = HJConvert.ToDateTime(dr["Time2End"]);
                    model.bisnessStart = HJConvert.ToDateTime(dr["bisnessStart"]);
                    model.bisnessend = HJConvert.ToDateTime(dr["bisnessend"]);
                    model.point = HJConvert.ToInt32(dr["point"]);
                    model.showlocal = HJConvert.ToInt32(dr["showlocal"]);
                    model.Grade = HJConvert.ToInt32(dr["Grade"]);
                    model.pop = HJConvert.ToInt32(dr["pop"]);
                    model.email = HJConvert.ToString(dr["email"]);
                    model.Opentimes1 = HJConvert.ToDateTime(dr["Opentimes1"]);
                    model.Opentimes2 = HJConvert.ToDateTime(dr["Opentimes2"]);
                    model.Closetimes1 = HJConvert.ToDateTime(dr["Closetimes1"]);
                    model.Closetimes2 = HJConvert.ToDateTime(dr["Closetimes2"]);
                    model.RefreshTime = HJConvert.ToString(dr["RefreshTime"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 更新商家账户金额 传入的参数 SetString: money = money -12 或者 money = money + 12 
        /// </summary>
        /// <param name="SetString"></param>
        /// <returns></returns>
        public int UpdateMoney(string SetString, int TogoId)
        {

            string sql = "update Points set " + SetString + " where unid=" + TogoId.ToString() + "";

            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        /// <summary>
        /// 区域搜索商家的列表
        /// </summary>
        /// <param name="pageindex">当前页</param>
        /// <param name="pagesize"></param>
        /// <param name="sqlwhere">条件</param>
        /// <param name="sortword">order by 后的</param>
        /// <returns></returns>
        public IList<PointsInfo> getsearchList(int PageIndex, int PageSize, string sqlwhere, string sortword)
        {
            IList<PointsInfo> list = new List<PointsInfo>();

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
            string field = "*, ";
            field += " CASE WHEN( ( CONVERT(varchar(12) , Opentimes1, 114 ) < CONVERT(varchar(12) , getdate(), 114 )";
            field += "and CONVERT(varchar(12) , Opentimes2, 114 ) > CONVERT(varchar(12) , getdate(), 114 )";
            field += ") or  ( CONVERT(varchar(12) , Closetimes1, 114 ) < CONVERT(varchar(12) , getdate(), 114 )";
            field += "and CONVERT(varchar(12) , Closetimes2, 114 ) > CONVERT(varchar(12) , getdate(), 114 )";
            field += ") )THEN 1 ELSE 0 END AS havenew";
            parameters[0].Value = "Points";
            parameters[1].Value = field;
            parameters[2].Value = "Unid";
            parameters[3].Value = sortword;
            parameters[4].Value = PageSize;
            parameters[5].Value = PageIndex;
            parameters[6].Value = 1;
            parameters[7].Value = sqlwhere;


            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectprii", parameters))
            {
                while (dr.Read())
                {
                    PointsInfo model = new PointsInfo();
                    model.Unid = HJConvert.ToInt32(dr["Unid"]);
                    model.InUse = HJConvert.ToString(dr["InUse"]);
                    model.ID = HJConvert.ToString(dr["ID"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.Comm = HJConvert.ToString(dr["Comm"]);
                    model.PType = HJConvert.ToInt32(dr["PType"]);
                    model.RcvType = HJConvert.ToInt32(dr["RcvType"]);
                    model.PosAddr = HJConvert.ToString(dr["PosAddr"]);
                    model.PosRoom = HJConvert.ToString(dr["PosRoom"]);
                    model.PosAttch = HJConvert.ToString(dr["PosAttch"]);
                    model.NamePy = HJConvert.ToString(dr["NamePy"]);
                    model.PosSrvAd = HJConvert.ToString(dr["PosSrvAd"]);
                    model.CommPerson = HJConvert.ToString(dr["CommPerson"]);
                    model.SendLimit = HJConvert.ToDecimal(dr["SendLimit"]);
                    model.LoginName = HJConvert.ToString(dr["LoginName"]);
                    model.Password = HJConvert.ToString(dr["Password"]);
                    model.SendFee = HJConvert.ToDecimal(dr["SendFee"]);
                    model.SN1 = HJConvert.ToString(dr["SN1"]);
                    model.SN2 = HJConvert.ToString(dr["SN2"]);
                    model.Sn2Key = HJConvert.ToString(dr["Sn2Key"]);
                    model.PTimes = HJConvert.ToInt32(dr["PTimes"]);
                    model.MgrCell = HJConvert.ToString(dr["MgrCell"]);
                    model.PHead = HJConvert.ToString(dr["PHead"]);
                    model.PEnd = HJConvert.ToString(dr["PEnd"]);
                    model.OpenTime = HJConvert.ToString(dr["OpenTime"]);
                    model.IsCallCenter = HJConvert.ToInt32(dr["IsCallCenter"]);
                    model.Address = HJConvert.ToString(dr["Address"]);
                    model.Introduce = HJConvert.ToString(dr["Introduce"]);
                    model.Status = HJConvert.ToInt32(dr["Status"]);
                    model.outnitice = HJConvert.ToString(dr["outnitice"]);
                    model.InTime = HJConvert.ToDateTime(dr["InTime"]);
                    model.Time1Start = HJConvert.ToDateTime(dr["Time1Start"]);
                    model.Time1End = HJConvert.ToDateTime(dr["Time1End"]);
                    model.IsDelete = HJConvert.ToInt32(dr["IsDelete"]);
                    model.SortNum = HJConvert.ToInt32(dr["SortNum"]);
                    model.FlavorGrade = HJConvert.ToInt32(dr["FlavorGrade"]);
                    model.ServiceGrade = HJConvert.ToInt32(dr["ServiceGrade"]);
                    model.SpeedGrade = HJConvert.ToInt32(dr["SpeedGrade"]);
                    model.Star = HJConvert.ToInt32(dr["Star"]);
                    model.category = HJConvert.ToString(dr["category"]);
                    model.ViewTimes = HJConvert.ToInt32(dr["ViewTimes"]);
                    model.senttime = HJConvert.ToInt32(dr["senttime"]);
                    model.sentorg = HJConvert.ToString(dr["sentorg"]);
                    model.special = HJConvert.ToString(dr["special"]);
                    model.reviewtimes = HJConvert.ToInt32(dr["reviewtimes"]);
                    model.money = HJConvert.ToDecimal(dr["money"]);
                    model.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    model.menunum = HJConvert.ToInt32(dr["menunum"]);
                    model.Picture = HJConvert.ToString(dr["Picture"]);
                    model.showpicture = HJConvert.ToInt32(dr["showpicture"]);
                    model.foodupdatetime = HJConvert.ToDateTime(dr["foodupdatetime"]);
                    model.Time2Start = HJConvert.ToDateTime(dr["Time2Start"]);
                    model.Time2End = HJConvert.ToDateTime(dr["Time2End"]);
                    model.bisnessStart = HJConvert.ToDateTime(dr["bisnessStart"]);
                    model.bisnessend = HJConvert.ToDateTime(dr["bisnessend"]);
                    model.bisnessStart2 = HJConvert.ToDateTime(dr["bisnessStart2"]);
                    model.bisnessend2 = HJConvert.ToDateTime(dr["bisnessend2"]);
                    model.point = HJConvert.ToInt32(dr["point"]);
                    model.showlocal = HJConvert.ToInt32(dr["showlocal"]);
                    model.Grade = HJConvert.ToInt32(dr["Grade"]);
                    model.pop = HJConvert.ToInt32(dr["pop"]);
                    model.EBuilding = HJConvert.ToString(dr["EBuilding"]);
                    // model.email = HJConvert.ToString(dr["email"]);
                    model.isbisness = HJConvert.ToInt32(dr["havenew"]);
                    model.Opentimes1 = HJConvert.ToDateTime(dr["Opentimes1"]);
                    model.Opentimes2 = HJConvert.ToDateTime(dr["Opentimes2"]);
                    model.Closetimes1 = HJConvert.ToDateTime(dr["Closetimes1"]);
                    model.Closetimes2 = HJConvert.ToDateTime(dr["Closetimes2"]);
                    model.BigPicture = HJConvert.ToString(dr["BigPicture"]);
                    model.RefreshTime = HJConvert.ToString(dr["RefreshTime"]);
                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 根据顺序返回
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IList<PointsInfo> GetList(string ids)
        {
            IList<PointsInfo> DataList = new List<PointsInfo>();
            string sql = "select unid, Picture ,Name , Time1Start ,Time1End from points where unid in (" + ids + ") and isdelete <> 1 order by charindex(','+rtrim(unid)+',', '," + ids + ",') ";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    PointsInfo info = new PointsInfo();
                    info.Unid = HJConvert.ToInt32(dr["unid"]);
                    info.Picture = HJConvert.ToString(dr["Picture"]);
                    info.Name = HJConvert.ToString(dr["Name"]);
                    info.Time1Start = HJConvert.ToDateTime(dr["Time1Start"]);
                    info.Time1End = HJConvert.ToDateTime(dr["Time1End"]);
                    DataList.Add(info);
                }
            }
            return DataList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public PointsInfo GetListff(int togoid)
        {
            PointsInfo info = new PointsInfo();
            string field = "select ";
            field += "*,(select avg(ServiceGrade) from ETogoOpinion where togoid = points.unid) as ServiceGrade1";
            field += ",(select avg(FlavorGrade) from ETogoOpinion where togoid = points.unid) as FlavorGrade1";
            field += ",(select avg(SpeedGrade) from ETogoOpinion where togoid = points.unid) as SpeedGrade1 from points where unid = " + togoid;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, field, null))
            {
                while (dr.Read())
                {
                    info.FlavorGrade = HJConvert.ToInt32(dr["FlavorGrade1"]);
                    info.ServiceGrade = HJConvert.ToInt32(dr["ServiceGrade1"]);
                    info.SpeedGrade = HJConvert.ToInt32(dr["SpeedGrade1"]);
                }
            }
            return info;
        }

        /// <summary>
        ///商家列表(坐标)
        /// </summary>
        /// <param name="pageindex">当前页</param>
        /// <param name="pagesize"></param>
        /// <param name="sqlwhere">条件</param>
        /// <param name="sortword">order by 后的</param>
        /// <returns></returns>
        public IList<PointsInfo> getshopList(string where)
        {
            IList<PointsInfo> list = new List<PointsInfo>();

            string sql = "select points.unid , etogolocalinfo.lat , etogolocalinfo.lng from points";
            sql += " left join etogolocalinfo  on ( points.unid=etogolocalinfo.togoid)   where  " + where;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    PointsInfo model = new PointsInfo();
                    model.Unid = HJConvert.ToInt32(dr["Unid"]);
                    model.Name = HJConvert.ToString(dr["lat"]);
                    model.NamePy = HJConvert.ToString(dr["lng"]);
                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 获得商家月统计
        /// </summary>
        public IList<PointsInfo> GetSummary(int pagesize, int pageindex, string strWhere, string orderName, int orderType, string orderwhere)
        {
            IList<PointsInfo> infos = new List<PointsInfo>();
            string ids = "";
            if (pageindex > 1)
            {
                ids = GetSummaryId(pagesize, pageindex, strWhere, orderName, orderwhere);
                if (ids == "")
                {
                    return infos;
                }
            }
            SqlParameter[] parameters = 
	        {
		        new SqlParameter("@tblName", SqlDbType.VarChar,255),
		        new SqlParameter("@strGetFields", SqlDbType.VarChar,1000),
		        new SqlParameter("@primary", SqlDbType.VarChar,255),
		        new SqlParameter("@orderName", SqlDbType.VarChar,255),
		        new SqlParameter("@PageSize", SqlDbType.Int),
		        new SqlParameter("@PageIndex", SqlDbType.Int),
		        new SqlParameter("@OrderType", SqlDbType.Bit),
		        new SqlParameter("@strWhere", SqlDbType.VarChar,1500),
                new SqlParameter("@ids", SqlDbType.VarChar,2000)
	        };
            parameters[0].Value = "Points";
            string field = "Unid, Name";
            field += ",(select count(*)  from Custorder where " + orderwhere + ") as allcount";
            field += ",(select sum(OrderSums)  from Custorder where " + orderwhere + ") as allprice";
            parameters[1].Value = field; ;
            parameters[2].Value = "Unid";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            parameters[8].Value = ids;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri_togo", parameters))
            {
                while (dr.Read())
                {
                    PointsInfo info = new PointsInfo();
                    info.Unid = HJConvert.ToInt32(dr["Unid"]);
                    info.Name = HJConvert.ToString(dr["Name"]);
                    info.allcount = HJConvert.ToInt32(dr["allcount"]);
                    info.allprice = HJConvert.ToDecimal(dr["allprice"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 获取所有商家的名称和对应的编号 
        /// </summary>
        /// <returns></returns>
        public string GetSummaryId(int pagesize, int pageindex, string sqlwhere, string sortname, string orderwhere)
        {
            string ids = "";
            int top = (pageindex - 1) * pagesize;
            string field = "select top " + top + " Unid";
            field += ",(select count(*)  from Custorder where " + orderwhere + ") as allcount";
            field += ",(select sum(OrderSums)  from Custorder where " + orderwhere + ") as allprice ";
            field += " from Points where " + sqlwhere + " order by " + sortname + " desc , Unid desc";
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, field, null))
            {
                while (dr.Read())
                {
                    ids += dr["Unid"] + ",";
                }
            }
            ids = System.Text.RegularExpressions.Regex.Replace(ids, @",$", "");
            return ids;
        }

        /// <summary>
        /// 搜索商品（配送范围内的商家的商品）
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="strWhere"></param>
        /// <param name="orderName"></param>
        /// <param name="orderType"></param>
        /// <param name="mylat"></param>
        /// <param name="mylng"></param>
        /// <param name="otherwhere"></param>
        /// <returns></returns>
        public IList<FoodinfoInfo> SearchFood(int pagesize, int pageindex, string strWhere, string orderName, int orderType, string mylat, string mylng, string otherwhere)
        {
            IList<FoodinfoInfo> infos = new List<FoodinfoInfo>();
            SqlParameter[] parameters =
            {
                new SqlParameter("@orderfield", SqlDbType.VarChar,255),
                new SqlParameter("@pagesize", SqlDbType.Int),
                new SqlParameter("@pageindex", SqlDbType.Int),
                new SqlParameter("@ordertype", SqlDbType.VarChar, 5),
                new SqlParameter("@where", SqlDbType.VarChar,1500),
                new SqlParameter("@lat", SqlDbType.VarChar,50),
                new SqlParameter("@lng", SqlDbType.VarChar,50),
               new SqlParameter("@otherwhere", SqlDbType.VarChar,1500),
            };
            parameters[0].Value = orderName;
            parameters[1].Value = pagesize; ;
            parameters[2].Value = pageindex;
            parameters[3].Value = orderType == 1 ? "desc" : "asc";
            parameters[4].Value = strWhere;
            parameters[5].Value = mylat;
            parameters[6].Value = mylng;
            parameters[7].Value = otherwhere;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "FoodInfo_Search", parameters))
            {
                while (dr.Read())
                {
                    FoodinfoInfo info = new FoodinfoInfo();
                    info.Unid = HJConvert.ToInt32(dr["Unid"]); 
                    info.FoodName = HJConvert.ToString(dr["FoodName"]);
                    info.FPrice = HJConvert.ToDecimal(dr["FPrice"]);
                    info.FPMaster = HJConvert.ToInt32(dr["FPMaster"]);

                    infos.Add(info);
                }
            }
            return infos;
        }


        /// <summary>
        /// 按距离获取商家[测试版本]
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="strWhere"></param>
        /// <param name="orderName"></param>
        /// <param name="orderType"></param>
        /// <param name="mylat"></param>
        /// <param name="mylng"></param>
        /// <param name="otherwhere">距离，及营业时间状态</param>
        /// <returns></returns>
        public IList<PointsInfo> GetDistanceListSuper(int pagesize, int pageindex, string strWhere, string orderName, int orderType, string mylat, string mylng, string otherwhere)
        {
            IList<PointsInfo> infos = new List<PointsInfo>();
            SqlParameter[] parameters = 
	        {
		        new SqlParameter("@orderfield", SqlDbType.VarChar,255),
		        new SqlParameter("@pagesize", SqlDbType.Int),
		        new SqlParameter("@pageindex", SqlDbType.Int),
		        new SqlParameter("@ordertype", SqlDbType.VarChar, 5),
		        new SqlParameter("@where", SqlDbType.VarChar,1500),
                new SqlParameter("@lat", SqlDbType.VarChar,50),
                new SqlParameter("@lng", SqlDbType.VarChar,50),
               new SqlParameter("@otherwhere", SqlDbType.VarChar,1500),
	        };
            parameters[0].Value = orderName;
            parameters[1].Value = pagesize; ;
            parameters[2].Value = pageindex;
            parameters[3].Value = orderType == 1 ? "desc" : "asc";
            parameters[4].Value = strWhere;
            parameters[5].Value = mylat;
            parameters[6].Value = mylng;
            parameters[7].Value = otherwhere;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "ETogo_GetShopListWithDistance", parameters))
            {
                while (dr.Read())
                {
                    PointsInfo info = new PointsInfo();
                    info.Unid = HJConvert.ToInt32(dr["Unid"]);
                    info.InUse = HJConvert.ToString(dr["InUse"]);
                    info.ID = HJConvert.ToString(dr["ID"]);
                    info.Name = HJConvert.ToString(dr["Name"]);
                    info.Comm = HJConvert.ToString(dr["Comm"]);
                    info.PType = HJConvert.ToInt32(dr["PType"]);
                    info.RcvType = HJConvert.ToInt32(dr["RcvType"]);
                    info.PosAddr = HJConvert.ToString(dr["PosAddr"]);
                    info.PosRoom = HJConvert.ToString(dr["PosRoom"]);
                    info.PosAttch = HJConvert.ToString(dr["PosAttch"]);
                    info.NamePy = HJConvert.ToString(dr["NamePy"]);
                    info.PosSrvAd = HJConvert.ToString(dr["PosSrvAd"]);
                    info.CommPerson = HJConvert.ToString(dr["CommPerson"]);
                    info.SendLimit = HJConvert.ToDecimal(dr["SendLimit"]);
                    info.LoginName = HJConvert.ToString(dr["LoginName"]);
                    info.Password = HJConvert.ToString(dr["Password"]);
                    info.SendFee = HJConvert.ToDecimal(dr["SendFee"]);
                    info.SN1 = HJConvert.ToString(dr["SN1"]);
                    info.SN2 = HJConvert.ToString(dr["SN2"]);
                    info.Sn2Key = HJConvert.ToString(dr["Sn2Key"]);
                    info.PTimes = HJConvert.ToInt32(dr["PTimes"]);
                    info.MgrCell = HJConvert.ToString(dr["MgrCell"]);
                    info.PHead = HJConvert.ToString(dr["PHead"]);
                    info.PEnd = HJConvert.ToString(dr["PEnd"]);
                    info.OpenTime = HJConvert.ToString(dr["OpenTime"]);
                    info.IsCallCenter = HJConvert.ToInt32(dr["IsCallCenter"]);
                    info.Address = HJConvert.ToString(dr["Address"]);
                    info.Introduce = "";
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.outnitice = HJConvert.ToString(dr["outnitice"]);
                    info.InTime = HJConvert.ToDateTime(dr["InTime"]);
                    info.Time1Start = HJConvert.ToDateTime(dr["Time1Start"]);
                    info.Time1End = HJConvert.ToDateTime(dr["Time1End"]);
                    info.IsDelete = HJConvert.ToInt32(dr["IsDelete"]);
                    info.SortNum = HJConvert.ToInt32(dr["SortNum"]);
                    info.FlavorGrade = HJConvert.ToInt32(dr["FlavorGrade"]);
                    info.ServiceGrade = HJConvert.ToInt32(dr["ServiceGrade"]);
                    info.SpeedGrade = HJConvert.ToInt32(dr["SpeedGrade"]);
                    info.Star = HJConvert.ToInt32(dr["Star"]);
                    info.category = HJConvert.ToString(dr["category"]);
                    info.ViewTimes = HJConvert.ToInt32(dr["ViewTimes"]);
                    info.senttime = HJConvert.ToInt32(dr["senttime"]);
                    info.sentorg = HJConvert.ToString(dr["sentorg"]);
                    info.special = HJConvert.ToString(dr["special"]);
                    info.reviewtimes = HJConvert.ToInt32(dr["reviewtimes"]);
                    info.money = HJConvert.ToDecimal(dr["money"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.menunum = HJConvert.ToInt32(dr["menunum"]);
                    info.Picture = HJConvert.ToString(dr["Picture"]);
                    info.showpicture = HJConvert.ToInt32(dr["showpicture"]);
                    info.foodupdatetime = HJConvert.ToDateTime(dr["foodupdatetime"]);
                    info.Time2Start = HJConvert.ToDateTime(dr["Time2Start"]);
                    info.Time2End = HJConvert.ToDateTime(dr["Time2End"]);
                    info.bisnessStart = HJConvert.ToDateTime(dr["bisnessStart"]);
                    info.bisnessend = HJConvert.ToDateTime(dr["bisnessend"]);
                    info.point = HJConvert.ToInt32(dr["point"]);
                    info.showlocal = HJConvert.ToInt32(dr["showlocal"]);
                    info.Grade = HJConvert.ToInt32(dr["Grade"]);
                    info.pop = HJConvert.ToInt32(dr["pop"]);
                    info.email = HJConvert.ToString(dr["email"]);
                    info.isbisness = HJConvert.ToInt32(dr["havenew"]);
                    info.Distance = HJConvert.ToDecimal(dr["Distance"]);
                    info.Introduce = HJConvert.ToString(dr["Introduce"]);
                    info.Opentimes1 = HJConvert.ToDateTime(dr["Opentimes1"]);
                    info.Opentimes2 = HJConvert.ToDateTime(dr["Opentimes2"]);
                    info.Closetimes1 = HJConvert.ToDateTime(dr["Closetimes1"]);
                    info.Closetimes2 = HJConvert.ToDateTime(dr["Closetimes2"]);
                    info.RefreshTime = HJConvert.ToString(dr["RefreshTime"]);

                    int togostatus = HJConvert.ToInt32(dr["Status"]);
                    int isonline = HJConvert.ToInt32(dr["havenew"]);
                    if (togostatus == 1 && isonline == 1)
                    {
                        info.isonline = 1;
                    }
                    else
                    {
                        info.isonline = 0;
                    }
                    info.Lat = HJConvert.ToString(dr["lat"]);
                    info.Lng = HJConvert.ToString(dr["Lng"]);

                    info.opentimestr = info.Opentimes1.ToShortTimeString() + "-" + info.Opentimes2.ToShortTimeString();
                    if (info.Opentimes1.ToShortTimeString() != info.Closetimes1.ToShortTimeString())
                    {
                        info.opentimestr += " | " + info.Closetimes1.ToShortTimeString() + "-" + info.Closetimes2.ToShortTimeString();
                    }

                    info.licensePic = HJConvert.ToString(dr["licensePic"]);
                    info.isLicense = HJConvert.ToInt32(dr["isLicense"]);
                    info.cateringPic = HJConvert.ToString(dr["cateringPic"]);
                    info.isCatering = HJConvert.ToInt32(dr["isCatering"]);

                    infos.Add(info);
                }
            }
            return infos;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderName"></param>
        /// <param name="orderType"></param>
        /// <param name="mylat"></param>
        /// <param name="mylng"></param>
        /// <param name="otherwhere"></param>
        /// <returns></returns>
        public int GetCountWidthDistance(string strWhere, string orderName, int orderType, string mylat, string mylng, string otherwhere)
        {
            SqlParameter[] parameters = 
	        {
		        new SqlParameter("@orderfield", SqlDbType.VarChar,255),
		        new SqlParameter("@ordertype", SqlDbType.VarChar, 5),
		        new SqlParameter("@where", SqlDbType.VarChar,1500),
                new SqlParameter("@lat", SqlDbType.VarChar,50),
                new SqlParameter("@lng", SqlDbType.VarChar,50),
               new SqlParameter("@otherwhere", SqlDbType.VarChar,1500),
	        };
            parameters[0].Value = orderName;
            parameters[1].Value = orderType == 1 ? "desc" : "asc";
            parameters[2].Value = strWhere;
            parameters[3].Value = mylat;
            parameters[4].Value = mylng;
            parameters[5].Value = otherwhere;

            int count = 0;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "ETogo_GetShopListCountWithDistance", parameters))
            {
                if (dr.Read())
                {
                    count = HJConvert.ToInt32(dr["recordcount"]);
                }
            }

            return count;
        }

        /// <summary>
        /// 取得记录数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetCollectCount(string strWhere)
        {
            SqlParameter[] Para = 
            {
                new SqlParameter("@tblName",SqlDbType.VarChar,255),
                new SqlParameter("@strWhere",SqlDbType.VarChar,1500)
            };
            Para[0].Value = "ETogoCollect Left Join Points On Points.Unid=ETogoCollect.togoid Left Join ETogoLocalInfo On Points.Unid=ETogoLocalInfo.togoid  ";
            Para[1].Value = strWhere;

            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", Para);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<PointsInfo> GetCollectList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<PointsInfo> infos = new List<PointsInfo>();
            SqlParameter[] Para = 
            {
                new SqlParameter("@pagesize", SqlDbType.Int),					
				new SqlParameter("@pageindex", SqlDbType.Int),
				new SqlParameter("@orderfield", SqlDbType.VarChar,255),
				new SqlParameter("@ordertype", SqlDbType.VarChar,5),
				new SqlParameter("@where", SqlDbType.VarChar,1000)
            };
            Para[0].Value = pagesize;
            Para[1].Value = pageindex;
            Para[2].Value = orderName;
            Para[3].Value = orderType == 1 ? "desc" : "asc";
            Para[4].Value = strWhere;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "GetCollectList", Para))
            {
                while (dr.Read())
                {
                    PointsInfo info = new PointsInfo();
                    info.Unid = HJConvert.ToInt32(dr["Unid"]);
                    info.InUse = HJConvert.ToString(dr["InUse"]);
                    info.ID = HJConvert.ToString(dr["ID"]);
                    info.Name = HJConvert.ToString(dr["Name"]);
                    info.Comm = HJConvert.ToString(dr["Comm"]);
                    info.PType = HJConvert.ToInt32(dr["PType"]);
                    info.RcvType = HJConvert.ToInt32(dr["RcvType"]);
                    info.PosAddr = HJConvert.ToString(dr["PosAddr"]);
                    info.PosRoom = HJConvert.ToString(dr["PosRoom"]);
                    info.PosAttch = HJConvert.ToString(dr["PosAttch"]);
                    info.NamePy = HJConvert.ToString(dr["NamePy"]);
                    info.PosSrvAd = HJConvert.ToString(dr["PosSrvAd"]);
                    info.CommPerson = HJConvert.ToString(dr["CommPerson"]);
                    info.SendLimit = HJConvert.ToDecimal(dr["SendLimit"]);
                    info.LoginName = HJConvert.ToString(dr["LoginName"]);
                    info.Password = HJConvert.ToString(dr["Password"]);
                    info.SendFee = HJConvert.ToDecimal(dr["SendFee"]);
                    info.SN1 = HJConvert.ToString(dr["SN1"]);
                    info.SN2 = HJConvert.ToString(dr["SN2"]);
                    info.Sn2Key = HJConvert.ToString(dr["Sn2Key"]);
                    info.PTimes = HJConvert.ToInt32(dr["PTimes"]);
                    info.MgrCell = HJConvert.ToString(dr["MgrCell"]);
                    info.PHead = HJConvert.ToString(dr["PHead"]);
                    info.PEnd = HJConvert.ToString(dr["PEnd"]);
                    info.OpenTime = HJConvert.ToString(dr["OpenTime"]);
                    info.IsCallCenter = HJConvert.ToInt32(dr["IsCallCenter"]);
                    info.Address = HJConvert.ToString(dr["Address"]);
                    info.Introduce = "";
                    info.Status = HJConvert.ToInt32(dr["Status"]);
                    info.outnitice = HJConvert.ToString(dr["outnitice"]);
                    info.InTime = HJConvert.ToDateTime(dr["InTime"]);
                    info.Time1Start = HJConvert.ToDateTime(dr["Time1Start"]);
                    info.Time1End = HJConvert.ToDateTime(dr["Time1End"]);
                    info.IsDelete = HJConvert.ToInt32(dr["IsDelete"]);
                    info.SortNum = HJConvert.ToInt32(dr["SortNum"]);
                    info.FlavorGrade = HJConvert.ToInt32(dr["FlavorGrade"]);
                    info.ServiceGrade = HJConvert.ToInt32(dr["ServiceGrade"]);
                    info.SpeedGrade = HJConvert.ToInt32(dr["SpeedGrade"]);
                    info.Star = HJConvert.ToInt32(dr["Star"]);
                    info.category = HJConvert.ToString(dr["category"]);
                    info.ViewTimes = HJConvert.ToInt32(dr["ViewTimes"]);
                    info.senttime = HJConvert.ToInt32(dr["senttime"]);
                    info.sentorg = HJConvert.ToString(dr["sentorg"]);
                    info.special = HJConvert.ToString(dr["special"]);
                    info.reviewtimes = HJConvert.ToInt32(dr["reviewtimes"]);
                    info.money = HJConvert.ToDecimal(dr["money"]);
                    info.Inve1 = HJConvert.ToInt32(dr["Inve1"]);
                    info.menunum = HJConvert.ToInt32(dr["menunum"]);
                    info.Picture = HJConvert.ToString(dr["Picture"]);
                    info.showpicture = HJConvert.ToInt32(dr["showpicture"]);
                    info.foodupdatetime = HJConvert.ToDateTime(dr["foodupdatetime"]);
                    info.Time2Start = HJConvert.ToDateTime(dr["Time2Start"]);
                    info.Time2End = HJConvert.ToDateTime(dr["Time2End"]);
                    info.bisnessStart = HJConvert.ToDateTime(dr["bisnessStart"]);
                    info.bisnessend = HJConvert.ToDateTime(dr["bisnessend"]);
                    info.point = HJConvert.ToInt32(dr["point"]);
                    info.showlocal = HJConvert.ToInt32(dr["showlocal"]);
                    info.Grade = HJConvert.ToInt32(dr["Grade"]);
                    info.pop = HJConvert.ToInt32(dr["pop"]);
                    info.email = HJConvert.ToString(dr["email"]);
                    info.RefreshTime = HJConvert.ToString(dr["RefreshTime"]);

                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 商家订单统计
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="strWhere"></param>
        /// <param name="orderwhere"></param>
        /// <returns></returns>
        public IList<TogoInfo> GetListWithOrderStatistics(int pagesize, int pageindex, string strWhere, string orderwhere, string ordername)
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

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "navlink_GetListWithOrderStatistics", parameters))
            {
                while (dr.Read())
                {
                    TogoInfo info = new TogoInfo();
                    info.Grade = HJConvert.ToInt32(dr["Unid"]);
                    info.TogoName = HJConvert.ToString(dr["classname"]);
                    info.allcount = HJConvert.ToInt32(dr["ordercount"]);
                    info.allprice = HJConvert.ToDecimal(dr["TotalPrice"]);
                    info.ShopHaveMoney = HJConvert.ToDecimal(dr["OrderTotal"]);
                    info.DataID = HJConvert.ToInt32(dr["recordtcount"]);
                    info.Shopprofit = HJConvert.ToDecimal(dr["Shopprofit"]);
                    info.banner1 = HJConvert.ToString(dr["foodprice"]);
                    info.sentmoney = HJConvert.ToInt32(dr["sendFee"]);
                    info.promotionmoney = HJConvert.ToDecimal(dr["promotionmoney"]);
                    info.paymoney = HJConvert.ToDecimal(dr["paymoney"]);
                    info.payamount = HJConvert.ToDecimal(dr["payamount"]);
                    info.cardpay = HJConvert.ToDecimal(dr["cardpay"]);

                    info.packagefee = HJConvert.ToDecimal(dr["packagefee"]);


                    info.LinkMan = HJConvert.ToString(dr["nopaycount"]);
                    info.Address = HJConvert.ToString(dr["nopaymoeny"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// 商家复制商家,返回新商家编号
        /// </summary>
        /// <param name="ShopId">商家编号</param>
        /// <param name="Status">状态</param>
        /// <param name="Alert">提示信息</param>
        /// <returns></returns>
        public int CopyShop(int ShopId)
        {

            SqlParameter[] Parameters = 
            {
                new SqlParameter("@shopid",SqlDbType.Int,4),
                new SqlParameter("@newunid",SqlDbType.Int,4),
            };
            Parameters[0].Value = ShopId;
            Parameters[1].Direction = ParameterDirection.Output;

            SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Points_CopyShop", Parameters);

            return HJConvert.ToInt32(Parameters[1].Value);
        }



        /// <summary>
        /// 根据商家id 和 搜索关键字 得到菜品名称 
        /// </summary>
        /// <param name="shopId">商家id</param>
        /// <param name="keyWord">搜索关键字</param>
        /// <returns></returns>
        public string GetFooNameByShopIdAndKeyWord(int shopId, string keyWord)
        {
            string sql = "SELECT TOP 1 FoodName FROM foodinfo WHERE InUse='y' AND FoodName LIKE '%" + keyWord + "%' AND FPMaster=" + shopId;//上架  商品名称 商家id

            return SQLHelper.ExecuteScalarr(CommandType.Text, sql);
        }




    }
}

