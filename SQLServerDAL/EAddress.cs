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
    /// ���ݷ�����EAddress��
    /// </summary>
    public class EAddress
    {
        /// <summary>
        /// �����ֻ�����ַ��Ϣ������û������ھͲ���ӣ����͵�ַ, �����û���� 
        /// </summary>
        public int SaveUserAndAddress(Hangjing.Model.EAddressInfo model)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", SqlDbType.Int, 4),
                new SqlParameter("@BuildingID", SqlDbType.Int,5),
                new SqlParameter("@Address", SqlDbType.VarChar,256),
                new SqlParameter("@Pri", SqlDbType.Int,4) ,
                new SqlParameter("@AddTime"  , SqlDbType.DateTime),
                new SqlParameter("@phone" , SqlDbType.VarChar , 50 ),
                new SqlParameter("@mobilephone" , SqlDbType.VarChar , 50),
                new SqlParameter("@receiver" , SqlDbType.VarChar , 30),
                new SqlParameter("@lat",SqlDbType.VarChar,50),
                new SqlParameter("@lng",SqlDbType.VarChar,50)
            };

            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.BuildingID;
            parameters[2].Value = model.Address;
            parameters[3].Value = model.Pri;
            parameters[4].Value = model.AddTime;
            parameters[5].Value = model.Phone;
            parameters[6].Value = model.Mobilephone;
            parameters[7].Value = model.Receiver;
            parameters[8].Value = model.Lat;
            parameters[9].Value = model.Lng;

            SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "SaveUserAndAddress", parameters);

            return HJConvert.ToInt32(parameters[0].Value);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Hangjing.Model.EAddressInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into EAddress(");
            strSql.Append("UserID,BuildingID,Address,Pri , addtime , phone ,mobilephone , receiver,lat,lng)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@BuildingID,@Address,@Pri ,@AddTime , @phone , @mobilephone ,@receiver, @lat, @lng)");
            strSql.Append(";SELECT @@IDENTITY;");
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", SqlDbType.Int, 4),
                new SqlParameter("@BuildingID", SqlDbType.Int,5),
                new SqlParameter("@Address", SqlDbType.VarChar,512),
                new SqlParameter("@Pri", SqlDbType.Int,4) ,
                new SqlParameter("@AddTime"  , SqlDbType.DateTime),
                new SqlParameter("@phone" , SqlDbType.VarChar , 512 ),
                new SqlParameter("@mobilephone" , SqlDbType.VarChar , 50),
                new SqlParameter("@receiver" , SqlDbType.VarChar , 30),
                new SqlParameter("@lat",SqlDbType.VarChar,50),
                new SqlParameter("@lng",SqlDbType.VarChar,50)
            };

            parameters[0].Value = model.UserID;
            parameters[1].Value = model.BuildingID;
            parameters[2].Value = model.Address;
            parameters[3].Value = model.Pri;
            parameters[4].Value = model.AddTime;
            parameters[5].Value = model.Phone;
            parameters[6].Value = model.Mobilephone;
            parameters[7].Value = model.Receiver;
            parameters[8].Value = model.Lat;
            parameters[9].Value = model.Lng;

            return HJConvert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Update(Hangjing.Model.EAddressInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update EAddress set ");
            strSql.Append("BuildingID=@BuildingID,");
            strSql.Append("Address=@Address,");
            strSql.Append("Pri=@Pri,addtime=@AddTime , phone=@phone , mobilephone=@mobilephone ,receiver=@receiver,lat=@lat,lng=@lng");
            strSql.Append(" where UserID=@UserID and dataid=@dataid");
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", SqlDbType.Int, 4),
                new SqlParameter("@BuildingID", SqlDbType.Int,4),
                new SqlParameter("@Address", SqlDbType.VarChar,512),
                new SqlParameter("@Pri", SqlDbType.Int,4) ,
                new SqlParameter("@AddTime"  , SqlDbType.DateTime),
                new SqlParameter("@phone" , SqlDbType.VarChar , 512 ),
                new SqlParameter("@mobilephone" , SqlDbType.VarChar , 50),
                new SqlParameter("@receiver" , SqlDbType.VarChar , 30),
                new SqlParameter("@dataid" , SqlDbType.Int,4),
                new SqlParameter("@lat",SqlDbType.VarChar,50),
                new SqlParameter("@lng",SqlDbType.VarChar,50)
            };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.BuildingID;
            parameters[2].Value = model.Address;
            parameters[3].Value = model.Pri;
            parameters[4].Value = model.AddTime;
            parameters[5].Value = model.Phone;
            parameters[6].Value = model.Mobilephone;
            parameters[7].Value = model.Receiver;
            parameters[8].Value = model.DataID;
            parameters[9].Value = model.Lat;
            parameters[10].Value = model.Lng;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// ɾ��һ����¼
        /// </summary>
        /// <param name="Id"></param>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        /// <returns></returns>
        public int DelEAddress(int Id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from EAddress where DataID=@DataID");
            SqlParameter[] Para =
            {
                new SqlParameter("@DataID",SqlDbType.Int)
            };
            Para[0].Value = Id;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// ����ɾ����¼
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        public int DelList(string IdList)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from EAddress where DataID in ({0})");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), IdList), null);
        }

        /// <summary>
        /// ��ȡ��¼�����ֶ�
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        /// <param>DataID</param>
        /// <returns>EAddressInfo</returns>
        public EAddressInfo GetModel(int DataID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@nDataID", SqlDbType.Int, 4)
            };
            parameters[0].Value = DataID;
            EAddressInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, "select * ,(select Name from EBuilding  where DataID=EAddress.BuildingID) as BuildingName from EAddress where DataID=@nDataID", parameters))
            {
                if (dr.Read())
                {
                    model = new EAddressInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.UserID = HJConvert.ToInt32(dr["UserID"]);
                    model.BuildingID = HJConvert.ToInt32(dr["BuildingID"]);

                    model.Address = HJConvert.ToString(dr["Address"]);
                    model.Pri = HJConvert.ToInt32(dr["Pri"]);
                    model.AddTime = HJConvert.ToDateTime(dr["addtime"]);
                    model.Phone = HJConvert.ToString(dr["phone"]);
                    model.Mobilephone = HJConvert.ToString(dr["mobilephone"]);
                    model.Receiver = HJConvert.ToString(dr["receiver"]);
                    model.BuildingName = HJConvert.ToString(dr["BuildingName"]);
                    model.Lat = HJConvert.ToString(dr["lat"]);
                    model.Lng = HJConvert.ToString(dr["lng"]);
                }
            }
            return model;
        }

        /// <summary>
        /// ����Ĭ�ϵ�ַ
        /// </summary>
        /// <param name="dataid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int UpdateDefaut(int dataid, int userid)
        {
            string sql = "update eaddress set pri = 0 where userid= " + userid + ";update eaddress set pri = 1 where  dataid=" + dataid + "";

            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        /// <summary>
        /// ����ܼ�¼��
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        public int GetCount(string strWhere)
        {
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "EAddress"), new SqlParameter("@strWhere", strWhere)));
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagesize">ҳ�ߴ�</param>
        /// <param name="pageindex">ҳ����</param>
        /// <param name="strWhere">��ѯ����</param>
        /// <param name="orderName">�����ֶ�</param>
        /// <param name="orderType">�������ͣ�1Ϊ����0Ϊ����</param>
        /// <returns>ͼ���б�</returns>
        ///�˴����ɺ����Ƽ������ڲ��������Զ�����
        public IList<EAddressInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<EAddressInfo> infos = new List<EAddressInfo>();
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
            parameters[0].Value = "EAddress";
            parameters[1].Value = "* ,(select Name from EBuilding where DataID = eaddress.BuildingID) as buildingname";
            parameters[2].Value = "DataID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    EAddressInfo info = new EAddressInfo();
                    info.DataID = HJConvert.ToInt32(dr["DataID"]);
                    info.UserID = HJConvert.ToInt32(dr["UserID"]);
                    info.BuildingID = HJConvert.ToInt32(dr["BuildingID"]);
                    info.Address = HJConvert.ToString(dr["Address"]);
                    info.Pri = HJConvert.ToInt32(dr["Pri"]);
                    info.AddTime = HJConvert.ToDateTime(dr["AddTime"]);
                    info.Phone = HJConvert.ToString(dr["phone"]);
                    info.Mobilephone = HJConvert.ToString(dr["mobilephone"]);
                    info.Receiver = HJConvert.ToString(dr["receiver"]);
                    info.BuildingName = HJConvert.ToString(dr["buildingname"]);
                    info.Lat = HJConvert.ToString(dr["lat"]);
                    info.Lng = HJConvert.ToString(dr["lng"]);
                    infos.Add(info);
                }
            }
            return infos;
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagesize">ҳ�ߴ�</param>
        /// <param name="pageindex">ҳ����</param>
        /// <param name="strWhere">��ѯ����</param>
        /// <param name="orderName">�����ֶ�</param>
        /// <param name="orderType">�������ͣ�1Ϊ����0Ϊ����</param>
        /// <returns>ͼ���б�</returns>
        ///�˴����ɺ����Ƽ������ڲ��������Զ�����
        public IList<EAddressInfo> GetUserIdList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<EAddressInfo> infos = new List<EAddressInfo>();
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
            parameters[0].Value = "EAddress";
            parameters[1].Value = "DataID,UserID";
            parameters[2].Value = "DataID";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    EAddressInfo info = new EAddressInfo();
                    info.UserID = HJConvert.ToInt32(dr["UserID"]);
                    infos.Add(info);
                }
            }
            return infos;
        }


        /// <summary>
        /// ��ַ�绰
        /// </summary>
        public int Update_tem(Hangjing.Model.EAddressInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update EAddress set ");
            strSql.Append(" Address=@Address,");
            strSql.Append(" mobilephone=@mobilephone");
            strSql.Append(" where dataid=@dataid");
            SqlParameter[] parameters =
            {
                new SqlParameter("@Address", SqlDbType.VarChar,256),
                new SqlParameter("@mobilephone" , SqlDbType.VarChar , 50),
                new SqlParameter("@dataid" , SqlDbType.Int,4)
            };
            parameters[0].Value = model.Address;
            parameters[1].Value = model.Mobilephone;
            parameters[2].Value = model.DataID;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// ��ȡ��¼�����ֶ�
        /// </summary>
        /// �˴����ɺ����Ƽ������ڲ��������Զ�����
        /// <param>DataID</param>
        /// <returns>EAddressInfo</returns>
        public EAddressInfo GetaddressModel(int uid)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@nDataID", SqlDbType.Int, 4)
            };
            parameters[0].Value = uid;
            EAddressInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, "select * ,(select Name from EBuilding  where DataID=EAddress.BuildingID) as BuildingName from EAddress where UserID=@nDataID", parameters))
            {
                if (dr.Read())
                {
                    model = new EAddressInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.UserID = HJConvert.ToInt32(dr["UserID"]);
                    model.BuildingID = HJConvert.ToInt32(dr["BuildingID"]);

                    model.Address = HJConvert.ToString(dr["Address"]);
                    model.Pri = HJConvert.ToInt32(dr["Pri"]);
                    model.AddTime = HJConvert.ToDateTime(dr["addtime"]);
                    model.Phone = HJConvert.ToString(dr["phone"]);
                    model.Mobilephone = HJConvert.ToString(dr["mobilephone"]);
                    model.Receiver = HJConvert.ToString(dr["receiver"]);
                    model.BuildingName = HJConvert.ToString(dr["BuildingName"]);
                    model.Lat = HJConvert.ToString(dr["lat"]);
                    model.Lng = HJConvert.ToString(dr["lng"]);
                }
            }
            return model;
        }
        /// <summary>
        /// ɾ����Ĭ�ϵ�ַ
        /// </summary>
        /// <param>dataid</param>
        public int DelEAdd(int dataid)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@dataid", SqlDbType.Int),
                new SqlParameter("@msg", SqlDbType.Int)
            };
            parameters[0].Value = dataid;
            parameters[1].Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "delete_msg", parameters);
            return Convert.ToInt32(parameters[1].Value);
        }

    }
}

