// EBuilding��������.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// update by yangxiaolong@ihangjing.com
// 2010-03-15
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

using Hangjing.DBUtility;
using Hangjing.Model;

namespace Hangjing.SQLServerDAL
{
	/// <summary>
	/// ���ݷ�����EBuilding��
	/// </summary>
    public class EBuilding 
	{
		#region  ��Ա����


        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(BuildingInfo model)
        {
            StringBuilder str = new StringBuilder();
            str.Append("insert into EBuilding(");
            str.Append("[Name],[Address],[Type],Picture,Remark,SectionID,FirstL, XYUrl,Lat,Lng, IsShow,cityid)");
            str.Append(" values (");
            str.Append("@Name,@Address,@Type,@Picture,@Remark,@SectionID,@FirstL,@XYUrl,@Lat,@Lng, @IsShow,@cityid);");
            str.Append(";select @@IDENTITY");

            SqlParameter[] Para = 
            {
				new SqlParameter("@Name", SqlDbType.VarChar,256),
				new SqlParameter("@Address", SqlDbType.VarChar,256),
				new SqlParameter("@Type", SqlDbType.Int),
				new SqlParameter("@Picture", SqlDbType.VarChar,80),
				new SqlParameter("@Remark", SqlDbType.VarChar,512),
				new SqlParameter("@SectionID", SqlDbType.Int),
				new SqlParameter("@FirstL", SqlDbType.VarChar,1),
                new SqlParameter("@XYUrl", SqlDbType.VarChar, 256),
                new SqlParameter("@Lat", SqlDbType.VarChar,50),
                new SqlParameter("@Lng", SqlDbType.VarChar,50),
                new SqlParameter("@IsShow", SqlDbType.Int),
                new SqlParameter("@cityid", SqlDbType.Int)
            };
            Para[0].Value = model.Name;
            Para[1].Value = model.Address;
            Para[2].Value = model.Type;
            Para[3].Value = model.Picture == null ? "" : model.Picture;
            Para[4].Value = model.Remark;
            Para[5].Value = model.SectionId;
            Para[6].Value = model.FirstL;
            Para[7].Value = model.XYUrl;
            Para[8].Value = model.Lat;
            Para[9].Value = model.Lng;
            Para[10].Value = model.IsShow;
            Para[11].Value = model.cityid;

            //return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, str.ToString(), Para));
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Update(BuildingInfo model)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update EBuilding set ");
            str.Append("Name=@Name,");
            str.Append("Address=@Address,");
            str.Append("Type=@Type,");
            str.Append("Picture=@Picture,");
            str.Append("Remark=@Remark,");
            str.Append("SectionID=@SectionID,");
            str.Append("FirstL=@FirstL,");
            str.Append("XYUrl=@XYUrl,");
            str.Append("Lat=@Lat,");
            str.Append("Lng=@Lng, ");
            str.Append("IsShow=@IsShow,");
            str.Append("cityid=@cityid ");
            str.Append(" where DataID=@DataID ");

            SqlParameter[] Para =
            {
				new SqlParameter("@DataID", SqlDbType.VarChar,20),
				new SqlParameter("@Name", SqlDbType.VarChar,256),
				new SqlParameter("@Address", SqlDbType.VarChar,256),
				new SqlParameter("@Type", SqlDbType.Int,4),
				new SqlParameter("@Picture", SqlDbType.VarChar,80),
				new SqlParameter("@Remark", SqlDbType.VarChar,512),
				new SqlParameter("@SectionID", SqlDbType.Int),
				new SqlParameter("@FirstL", SqlDbType.VarChar,1),
                new SqlParameter("@XYUrl", SqlDbType.VarChar, 256),
                new SqlParameter("@Lat", SqlDbType.VarChar,50),
                new SqlParameter("@Lng", SqlDbType.VarChar,50),
                new SqlParameter("@IsShow", SqlDbType.Int),
                new SqlParameter("@cityid", SqlDbType.Int)
            };
            Para[0].Value = model.DataID;
            Para[1].Value = model.Name;
            Para[2].Value = model.Address;
            Para[3].Value = model.Type;
            Para[4].Value = model.Picture == null ? "" : model.Picture;
            Para[5].Value = model.Remark;
            Para[6].Value = model.SectionId;
            Para[7].Value = model.FirstL;
            Para[8].Value = model.XYUrl;
            Para[9].Value = model.Lat;
            Para[10].Value = model.Lng;
            Para[11].Value = model.IsShow;
            Para[12].Value = model.cityid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public int Delete(int DataID)
		{
            StringBuilder str = new StringBuilder();
            str.Append("delete from EBuilding where DataID=@DataID");

			SqlParameter[] Para =
            {
			    new SqlParameter("@DataID", SqlDbType.Int)
            };
			Para[0].Value = DataID;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
		}

        /// <summary>
        /// ɾ����������
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        public int DelList(string IdList)
        {
            SqlParameter[] Para = 
            {
                new SqlParameter("@IdList",SqlDbType.VarChar,200)
            };
            Para[0].Value = IdList;

            return SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "DelEBuildingList", Para);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public BuildingInfo GetModel(int DataID)
        {
            SqlParameter[] Para = 
            {
                new SqlParameter("@DataID",SqlDbType.Int)
            };
            Para[0].Value = DataID;

            BuildingInfo model = null;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "GetEBuildingModel", Para))//2011.6.30 ���´洢����
            {
                if (dr.Read())
                {
                    model = new BuildingInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.Address = HJConvert.ToString(dr["Address"]);
                    model.Type = HJConvert.ToInt32(dr["Type"]);
                    model.Picture = HJConvert.ToString(dr["Picture"]);
                    model.Remark = HJConvert.ToString(dr["Remark"]);
                    model.SectionId = HJConvert.ToInt32(dr["SectionID"]);
                    model.FirstL = HJConvert.ToString(dr["FirstL"]);
                    model.SectionName = HJConvert.ToString(dr["SectionName"]);
                    model.XYUrl = HJConvert.ToString(dr["XYUrl"]);
                    model.Lat = HJConvert.ToString(dr["Lat"]);
                    model.Lng = HJConvert.ToString(dr["Lng"]);
                    model.IsShow = HJConvert.ToInt32(dr["isshow"]);
                    model.cityid = HJConvert.ToInt32(dr["cityid"]);
                }
            }
            return model;
        }


        /// <summary>
        /// ����ܼ�¼��
        /// </summary>
        public int GetCount(string strWhere)
        {
            SqlParameter[] Para = 
            {
                new SqlParameter("@tblName",SqlDbType.VarChar,255),
                new SqlParameter("@strWhere",SqlDbType.VarChar,1500)
            };
            Para[0].Value = "EBuilding";
            Para[1].Value = strWhere;

            return (int)SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", Para);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public IList<BuildingInfo> GetList(int pageSize, int pageIndex, string where, string orderField, int orderType)
        {
            IList<BuildingInfo> DataList = new List<BuildingInfo>();

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
            Para[0].Value = "EBuilding";
            Para[1].Value = "*,(select SectionName from SectionInfo where SectionID=EBuilding.SectionID) SectionName,(select cname from city where cid=EBuilding.cityid) cityname";
            Para[2].Value = "DataID";
            Para[3].Value = orderField;
            Para[4].Value = pageSize;
            Para[5].Value = pageIndex;
            Para[6].Value = orderType;
            Para[7].Value = where;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", Para))
            {
                while (dr.Read())
                {
                    BuildingInfo model = new BuildingInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.Address = HJConvert.ToString(dr["Address"]);
                    model.Type = HJConvert.ToInt32(dr["Type"]);
                    model.Picture = HJConvert.ToString(dr["Picture"]);
                    model.Remark = HJConvert.ToString(dr["Remark"]);
                    model.SectionId = HJConvert.ToInt32(dr["SectionID"]);
                    model.FirstL = HJConvert.ToString(dr["FirstL"]);
                    model.SectionName = HJConvert.ToString(dr["SectionName"]);
                    model.XYUrl = HJConvert.ToString(dr["XYUrl"]);
                    model.Lat = HJConvert.ToString(dr["Lat"]);
                    model.Lng = HJConvert.ToString(dr["Lng"]);
                    model.IsShow = HJConvert.ToInt32(dr["isshow"]);
                    model.cityname = HJConvert.ToString(dr["cityname"]);
                    DataList.Add(model);
                }
            }
            return DataList;
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public IList<BuildingInfo> GetAll()
        {
            IList<BuildingInfo> DataList = new List<BuildingInfo>();

            //�����ȡ���˴������ȡ���е��ֶΣ����ٻ�ȡ���ֶμ��������Լ��������ݿ�ĸ���
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "GetBuildingAll", null))
            {
                while (dr.Read())
                {
                    BuildingInfo model = new BuildingInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.Type = HJConvert.ToInt32(dr["Type"]);
                    model.SectionId = HJConvert.ToInt32(dr["SectionID"]);
                    model.FirstL = HJConvert.ToString(dr["FirstL"]);
                    model.Lat = HJConvert.ToString(dr["Lat"]);
                    model.Lng = HJConvert.ToString(dr["Lng"]);
                    model.IsShow = HJConvert.ToInt32(dr["isshow"]);
                    model.cityid = HJConvert.ToInt32(dr["cityid"]);
                    DataList.Add(model);
                }
            }
            return DataList;
        }

        public BuildingInfo GetModelbyName(string buildingname)
        {
            BuildingInfo model = null;
            SqlParameter[] parameters = 
            {
                new SqlParameter("@buildingname" , SqlDbType.VarChar , 100)
            };
            parameters[0].Value = buildingname;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "EBuilding_GetModelByName", parameters))
            {
                while (dr.Read())
                {
                    model = new BuildingInfo();
                    model.DataID = HJConvert.ToInt32(dr["dataid"]);
                }
            }
            return model;
        }

        /// <summary>
        /// ��ȡ��־������ ����DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetBuildingTable()
        {
            IList<BuildingInfo> infos = GetAll();

            System.Data.DataTable building_dt = new System.Data.DataTable("BuildingInfo");
            //** System.Reflection.PropertyInfo[] ����.Net�������ʵ��Ilist��DateTable��ת����
            System.Reflection.PropertyInfo[] propertys = typeof(BuildingInfo).GetProperties();
            foreach (System.Reflection.PropertyInfo pro in propertys)
            {
                building_dt.Columns.Add(pro.Name, pro.PropertyType);
            }

            foreach (BuildingInfo building in infos)
            {
                System.Data.DataRow dr = building_dt.NewRow();
                dr["DataId"] = building.DataID;
                dr["Name"] = building.Name;
                //dr["Address"] = building.Address;
                dr["Type"] = building.Type;
                //dr["Picture"] = building.Picture;
                //dr["Remark"] = building.Remark;
                dr["SectionID"] = building.SectionId;
                dr["FirstL"] = building.FirstL;
                dr["SectionName"] = building.SectionName;
                dr["Lat"] = building.Lat;
                dr["Lng"] = building.Lng;
                dr["cityid"] = building.cityid;
                building_dt.Rows.Add(dr);
            }

            return building_dt;
        }

        //**********************************************************ע��ʱ������

        /// <summary>
        /// ���ݽ��������б��ȡ��Ӧ�Ľ����������б� zjf@ihangjing.com 2010-07-13
        /// </summary>
        public string GetNameList(string IdList)
        {
            string s = IdList.Trim();
            if (s == "")
            {
                return "";
            }
            string strSql = "select name from ebuilding where dataid in(" + IdList + ")";
            string NameList = "";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, strSql, null))
            {
                while (dr.Read())
                {
                    NameList += HJConvert.ToString(dr["Name"]);
                    NameList += ",";
                }
            }
            return System.Text.RegularExpressions.Regex.Replace(NameList, @",$", " ");//ȥ�����һ������
        }

        /// <summary>
        /// ���ݽ������Ų�������
        /// </summary>
        public string GetOrderNameList(string IdList)
        {
            string s = IdList.Trim();
            if (s == "")
            {
                return "";
            }

            string strSql = "select name from ebuilding where dataid in(" + IdList + ") order by charindex(','+rtrim(dataid)+',', ',"+IdList+",') ";
            string NameList = "";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, strSql, null))
            {
                while (dr.Read())
                {
                    NameList += HJConvert.ToString(dr["Name"]);
                    NameList += ",";
                }
            }
            return System.Text.RegularExpressions.Regex.Replace(NameList, @",$", " ");//ȥ�����һ������
        }
        /// <summary>
        /// ʹ�ô洢���̻�������б������������ zjf@ihangjing.com 2010-07-14
        /// </summary>
        public IList<BuildingInfo> GetFixList(int pageSize, int pageIndex, string where, string orderField, int orderType)
        {
            IList<BuildingInfo> DataList = new List<BuildingInfo>();

            SqlParameter[] Para = 
            {
                new SqlParameter("@pagesize", SqlDbType.Int),					
                new SqlParameter("@pageindex", SqlDbType.Int),
                new SqlParameter("@orderfield", SqlDbType.VarChar,20),
                new SqlParameter("@ordertype", SqlDbType.VarChar,5),
                new SqlParameter("@where", SqlDbType.VarChar,1500)
            };
            
            Para[0].Value = pageSize;
            Para[1].Value = pageIndex;
            Para[2].Value = orderField;
            Para[3].Value = orderType == 1?"desc":"asc";
            Para[4].Value = where;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "EGetBuildingList", Para))
            {
                while (dr.Read())
                {
                    BuildingInfo model = new BuildingInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.Address = HJConvert.ToString(dr["Address"]);
                    model.Type = HJConvert.ToInt32(dr["Type"]);
                    //model.Picture = HJConvert.ToString(dr["Picture"]);
                    //model.Remark = HJConvert.ToString(dr["Remark"]);
                    model.SectionId = HJConvert.ToInt32(dr["SectionID"]);
                    model.FirstL = HJConvert.ToString(dr["FirstL"]);
                    model.SectionName = HJConvert.ToString(dr["SectionName"]);
                    model.XYUrl = HJConvert.ToString(dr["XYUrl"]);
                    model.Lat = HJConvert.ToString(dr["Lat"]);
                    model.Lng = HJConvert.ToString(dr["Lng"]);
                    model.IsShow = HJConvert.ToInt32(dr["isshow"]);

                    DataList.Add(model);
                }
            }
            return DataList;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public IList<BuildingInfo> MapGetList(string Lat, string Lng,decimal Radius)
        {
            IList<BuildingInfo> DataList = new List<BuildingInfo>();

            SqlParameter[] Para = 
            {
				new SqlParameter("@Lat", SqlDbType.VarChar,50),
				new SqlParameter("@Lng", SqlDbType.VarChar,50),
				new SqlParameter("@Radius", SqlDbType.Decimal)
            };
            Para[0].Value = Lat;
            Para[1].Value = Lng;
            Para[2].Value = Radius;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "MapGetBuildingByLatLng", Para))
            {
                while (dr.Read())
                {
                    BuildingInfo model = new BuildingInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    //model.Address = HJConvert.ToString(dr["Address"]);
                    //model.Type = HJConvert.ToInt32(dr["Type"]);
                    //model.Picture = HJConvert.ToString(dr["Picture"]);
                    //model.Remark = HJConvert.ToString(dr["Remark"]);
                    //model.SectionID = HJConvert.ToInt32(dr["SectionID"]);
                    model.FirstL = HJConvert.ToString(dr["FirstL"]);
                    model.SectionName = HJConvert.ToString(dr["SectionName"]);
                    model.Lat = HJConvert.ToString(dr["Lat"]);
                    model.Lng = HJConvert.ToString(dr["Lng"]);
                    model.IsShow = HJConvert.ToInt32(dr["isshow"]);
                    model.cityid = HJConvert.ToInt32(dr["cityid"]);
                    DataList.Add(model);
                }
            }
            return DataList;
        }
		#endregion  ��Ա����
	}
}

