using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;


namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 数据访问类EUser。
    /// </summary>
    public class ECustomer
    {

        /// <summary>
        /// 增减余额
        /// </summary>
        /// <returns></returns>
        public int addAccountMoney(int uid, string msg, decimal addpoint, int PayType)
        {
            SqlParameter[] Para =
            {
                new SqlParameter("@userid",SqlDbType.Int,5),
                new SqlParameter("@AddMoney",SqlDbType.Decimal , 5),
                new SqlParameter("@Inve2",SqlDbType.VarChar,200),
                new SqlParameter("@PayType",SqlDbType.Int,5),
                new SqlParameter("@dataid",SqlDbType.Int,5),
            };
            Para[0].Value = uid;
            Para[1].Value = addpoint;
            Para[2].Value = msg;
            Para[3].Value = PayType;
            Para[4].Direction = ParameterDirection.Output; ;

            SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Ecustomer_addmoney", Para);



            return HJConvert.ToInt32(Para[4].Value);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hangjing.Model.ECustomerInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ECustomer(");
            strSql.Append("Name,TrueName,Sex,Tell,Phone,QQ,MSN,RegTime,Point,Picture,State,EMAIL ,password ,isActivate ,ActivateCode ,groupid , WebSite ,rid,userMoney,PhoneActivate,UC_ID)");
            strSql.Append(" values (");
            strSql.Append("@Name,@TrueName,@Sex,@Tell,@Phone,@QQ,@MSN,@RegTime,@Point,@Picture,@State,@EMAIL,@password ,@isActivate ,@ActivateCode , @groupid , @WebSite ,@rid,@userMoney,@PhoneActivate,@UC_ID);");
            strSql.Append("SELECT @@IDENTITY;");
            SqlParameter[] parameters = 
            {			
				new SqlParameter("@Name", SqlDbType.VarChar,256),
				new SqlParameter("@TrueName", SqlDbType.VarChar,256),
				new SqlParameter("@Sex", SqlDbType.VarChar ,1),
				new SqlParameter("@Tell", SqlDbType.VarChar,20),
				new SqlParameter("@Phone", SqlDbType.VarChar,20),
				new SqlParameter("@QQ", SqlDbType.VarChar,20),
				new SqlParameter("@MSN", SqlDbType.VarChar,256),
				new SqlParameter("@RegTime", SqlDbType.DateTime),
				new SqlParameter("@Point", SqlDbType.Int,4),
				new SqlParameter("@Picture", SqlDbType.VarChar,256),
				new SqlParameter("@State", SqlDbType.VarChar,10),
				new SqlParameter("@EMAIL", SqlDbType.VarChar,30),
                new SqlParameter("@password" , SqlDbType.VarChar , 50),
                new SqlParameter("@isActivate" , SqlDbType.Int),
                new SqlParameter("@ActivateCode" , SqlDbType.VarChar , 200),
                new SqlParameter("@groupid" , SqlDbType.Int , 5),
                new SqlParameter("@WebSite" , SqlDbType.VarChar , 50),
                new SqlParameter("@rid" , SqlDbType.VarChar , 50),
                new SqlParameter("@userMoney" , SqlDbType.Decimal),
                new SqlParameter("@PhoneActivate" , SqlDbType.Int),
                new SqlParameter("@UC_ID" , SqlDbType.Int),
            };
            parameters[0].Value = model.Name;
            parameters[1].Value = model.TrueName;
            parameters[2].Value = model.Sex;
            parameters[3].Value = model.Tell != null ? model.Tell : "";
            parameters[4].Value = "";
            parameters[5].Value = "";
            parameters[6].Value = model.MSN;
            parameters[7].Value = model.RegTime;
            parameters[8].Value = model.Point;
            parameters[9].Value = "";
            parameters[10].Value = "0";
            parameters[11].Value = model.EMAIL;
            parameters[12].Value = model.Password;
            parameters[13].Value = model.IsActivate;
            parameters[14].Value = model.ActivateCode;
            parameters[15].Value = model.GroupID;
            parameters[16].Value = model.WebSite;
            parameters[17].Value = model.RID;
            parameters[18].Value = model.Usermoney;
            parameters[19].Value = model.PhoneActivate;
            parameters[20].Value = model.UC_ID;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Hangjing.Model.ECustomerInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ECustomer set ");
            strSql.Append("TrueName=@TrueName,");
            strSql.Append("Tell=@Tell,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("QQ=@QQ,");
            strSql.Append("MSN=@MSN, ");
            strSql.Append("userMoney=@userMoney,PhoneActivate=@PhoneActivate,");
            strSql.Append("name=@name , Picture = @Picture,sex=@sex");
            strSql.Append(",EMAIL=@EMAIL ");
            strSql.Append(" where DataID=@DataID ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@DataID", SqlDbType.Int , 4),
				new SqlParameter("@TrueName", SqlDbType.VarChar,256),
				new SqlParameter("@Tell", SqlDbType.VarChar,20),
				new SqlParameter("@Phone", SqlDbType.VarChar,20),
				new SqlParameter("@QQ", SqlDbType.VarChar,20),
				new SqlParameter("@MSN", SqlDbType.VarChar,256),
                new SqlParameter("@userMoney" , SqlDbType.Decimal , 20),
                new SqlParameter("@PhoneActivate" , SqlDbType.Int , 20),
                new SqlParameter("@name" , SqlDbType.VarChar ,30),
                new SqlParameter("@Picture" , SqlDbType.VarChar ,256),
                new SqlParameter("@sex" , SqlDbType.VarChar ,1),
				new SqlParameter("@EMAIL", SqlDbType.VarChar,256),
            };
            parameters[0].Value = model.DataID;
            parameters[1].Value = model.TrueName;
            parameters[2].Value = model.Tell;
            parameters[3].Value = model.Phone;
            parameters[4].Value = model.QQ;
            parameters[5].Value = model.MSN;
            parameters[6].Value = model.Usermoney;
            parameters[7].Value = model.PhoneActivate;
            parameters[8].Value = model.Name;
            parameters[9].Value = model.Picture;
            parameters[10].Value = model.Sex;
            parameters[11].Value = model.EMAIL;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录所有字段
        /// </summary>
        /// 此代码由杭景科技代码内部生成器自动生成
        /// <param>DataID</param>
        /// <returns>ECustomerInfo</returns>
        public ECustomerInfo GetModel(int DataID)
        {
            SqlParameter parameter = new SqlParameter("@nDataID", SqlDbType.Int, 4);
            parameter.Value = DataID;
            ECustomerInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, "select * from ECustomer where DataID=@nDataID", parameter))
            {
                if (dr.Read())
                {
                    model = new ECustomerInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.TrueName = HJConvert.ToString(dr["TrueName"]);
                    model.Sex = HJConvert.ToString(dr["Sex"]);
                    model.Tell = HJConvert.ToString(dr["Tell"]);
                    model.Phone = HJConvert.ToString(dr["Phone"]);
                    model.QQ = HJConvert.ToString(dr["QQ"]);
                    model.MSN = HJConvert.ToString(dr["MSN"]);
                    model.RegTime = HJConvert.ToDateTime(dr["RegTime"]);
                    model.Point = HJConvert.ToInt32(dr["Point"]);
                    model.Picture = HJConvert.ToString(dr["Picture"]);
                    model.State = HJConvert.ToString(dr["State"]);
                    model.EMAIL = HJConvert.ToString(dr["Email"]);
                    model.Password = HJConvert.ToString(dr["Password"]);
                    model.IsActivate = HJConvert.ToInt32(dr["isactivate"]);
                    model.ActivateCode = HJConvert.ToString(dr["activatecode"]);
                    model.GroupID = HJConvert.ToDecimal(dr["GroupID"]);
                    model.WebSite = HJConvert.ToString(dr["WebSite"]);
                    model.RID = HJConvert.ToString(dr["rid"]);
                    model.Usermoney = HJConvert.ToDecimal(dr["userMoney"]);
                    model.PayPassword = HJConvert.ToString(dr["PayPassword"]);
                    model.PayPWDQuestion = HJConvert.ToString(dr["PayPWDQuestion"]);
                    model.distributemoney = HJConvert.ToDecimal(dr["distributemoney"]);
                    model.PayPWDAnswer = HJConvert.ToString(dr["PayPWDAnswer"]);
                   
                    model.qrcodeurl = HJConvert.ToString(dr["qrcodeurl"]);


                }
            }
            return model;
        }

        /// <summary>
        /// 该email是否被除数使用
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool exists(string email)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from ECustomer where email = @email");
            SqlParameter[] parameters = 
            {
                new SqlParameter("@email" , SqlDbType.VarChar , 30)
            };
            parameters[0].Value = email;

            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters)) > 0 ? true : false;
        }

        /// <summary>
        /// 获得记录数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetCount(string strWhere)
        {
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "pagecount", new SqlParameter("@tblName", "ECustomer"), new SqlParameter("@strWhere", strWhere)));
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int id)
        {
            string sql = "delete from Ecustomer  where dataID=@ID ";
            SqlParameter parameter = new SqlParameter("@ID", SqlDbType.Int, 4);
            parameter.Value = id;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
        }

        /// <summary>
        /// 删除不定条数记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int Delete(string ids)
        {
            string sql = "delete from Ecustomer  where dataID in ({0}) ";
            return SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(sql, ids), null);
        }

        /// <summary>
        /// 分页获得新闻列表
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页码</param>
        /// <param name="where">查询条件</param>
        /// <returns>IList<NewsInfo></returns>
        public IList<Hangjing.Model.ECustomerInfo> GetList(int pagesize, int pageindex, string where)
        {
            IList<Hangjing.Model.ECustomerInfo> infos = new List<Hangjing.Model.ECustomerInfo>();

            SqlParameter[] parameters = 
            {
				new SqlParameter("@tblName", SqlDbType.VarChar,255),
				new SqlParameter("@strGetFields", SqlDbType.VarChar,1000),
				new SqlParameter("@orderName", SqlDbType.VarChar,255),
				new SqlParameter("@PageSize", SqlDbType.Int),					
				new SqlParameter("@PageIndex", SqlDbType.Int),
				new SqlParameter("@OrderType", SqlDbType.Bit),
				new SqlParameter("@strWhere", SqlDbType.VarChar,1500)
            };
            parameters[0].Value = "ECustomer";
            parameters[1].Value = "*";
            parameters[2].Value = "dataID";
            parameters[3].Value = pagesize;
            parameters[4].Value = pageindex;
            parameters[5].Value = 1;
            parameters[6].Value = where;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    Hangjing.Model.ECustomerInfo model = new Hangjing.Model.ECustomerInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.TrueName = HJConvert.ToString(dr["TrueName"]);
                    model.Sex = HJConvert.ToString(dr["Sex"]);
                    model.Tell = HJConvert.ToString(dr["Tell"]);
                    model.Phone = HJConvert.ToString(dr["Phone"]);
                    model.QQ = HJConvert.ToString(dr["QQ"]);
                    model.MSN = HJConvert.ToString(dr["MSN"]);
                    model.RegTime = HJConvert.ToDateTime(dr["RegTime"]);
                    model.Point = HJConvert.ToInt32(dr["Point"]);
                    model.Picture = HJConvert.ToString(dr["Picture"]);
                    model.State = HJConvert.ToString(dr["State"]);
                    model.EMAIL = HJConvert.ToString(dr["Email"]);
                    model.Password = HJConvert.ToString(dr["Password"]);
                    model.IsActivate = HJConvert.ToInt32(dr["isactivate"]);
                    model.ActivateCode = HJConvert.ToString(dr["activatecode"]);
                   model.GroupID = HJConvert.ToDecimal(dr["GroupID"]);
                    model.WebSite = HJConvert.ToString(dr["WebSite"]);
                    infos.Add(model);
                }
            }
            return infos;
        }

        /// <summary>
        /// 通过email, password取得实例.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Hangjing.Model.ECustomerInfo GetModelByEmail(string email, string password)
        {
            Hangjing.Model.ECustomerInfo model = null;
            SqlParameter[] parameters = 
            {
                new SqlParameter("@email" , SqlDbType.VarChar , 50),
                new SqlParameter("@password" , SqlDbType.VarChar, 50)
            };
            parameters[0].Value = email;
            parameters[1].Value = password;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, "select * from ECustomer where email=@email and password=@password", parameters))
            {

                while (dr.Read())
                {
                    model = new ECustomerInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.TrueName = HJConvert.ToString(dr["TrueName"]);
                    model.Tell = HJConvert.ToString(dr["Tell"]);
                    model.Phone = HJConvert.ToString(dr["Phone"]);
                    model.EMAIL = HJConvert.ToString(dr["Email"]);
                    model.Point = HJConvert.ToInt32(dr["Point"]);
                    model.RegTime = HJConvert.ToDateTime(dr["RegTime"]);
                    model.QQ = HJConvert.ToString(dr["QQ"]);
                    model.MSN = HJConvert.ToString(dr["MSN"]);
                    model.Picture = HJConvert.ToString(dr["Picture"]);
                    model.Password = HJConvert.ToString(dr["Password"]);
                    model.IsActivate = HJConvert.ToInt32(dr["isactivate"]);
                    model.ActivateCode = HJConvert.ToString(dr["activatecode"]);
                   model.GroupID = HJConvert.ToDecimal(dr["GroupID"]);
                    model.WebSite = HJConvert.ToString(dr["WebSite"]);
                    model.RID = HJConvert.ToString(dr["rid"]);
                    model.PayPassword = HJConvert.ToString(dr["PayPassword"]);
                    model.Usermoney = HJConvert.ToDecimal(dr["userMoney"]);
                    model.PhoneActivate = HJConvert.ToInt32(dr["PhoneActivate"]);
                    model.UC_ID = HJConvert.ToInt32(dr["UC_ID"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 更新用户照片
        /// </summary>
        /// <param name="dataid"></param>
        /// <param name="type"></param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public int UpdateUser(int dataid, string type, string filepath)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update ECustomer set picture ");
            str.Append("=@filepath");
            str.Append(" where dataid=@uid");

            SqlParameter[] parameters = 
            {
                new SqlParameter("@filepath", SqlDbType.VarChar , 50),
                new SqlParameter("@uid", SqlDbType.Int,4)
            };
            parameters[0].Value = filepath;
            parameters[1].Value = dataid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), parameters);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="dataid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int ChangePassword(int dataid, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Ecustomer set password=@password where dataid=@dataid");
            SqlParameter[] parameters = 
            {
                new SqlParameter("@password" ,SqlDbType.VarChar , 50),
                new SqlParameter("@dataid" , SqlDbType.Int , 4)
            };
            parameters[0].Value = password;
            parameters[1].Value = dataid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据邮箱来修改密码
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public int UpdatePwdByEmail(string email, string newPwd)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update eCustomer set Password = @Password where Email= @Email");

            SqlParameter[] Para = 
            {
                new SqlParameter("@Password",SqlDbType.VarChar,60),
                new SqlParameter("@Email",SqlDbType.VarChar,200)
            };
            Para[0].Value = newPwd;
            Para[1].Value = email;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        /// <summary>
        /// 验证昵称是否可用。
        /// </summary>
        /// <param name="nikename"></param>
        /// <returns></returns>
        public bool IsExistNike(string nikename)
        {
            string sql = "select count(*) from ecustomer where name=@nikename";
            SqlParameter[] parameters = 
            {
                new SqlParameter("@nikename" ,SqlDbType.VarChar , 256)
            };
            parameters[0].Value = nikename;
            return Convert.ToInt32(SQLHelper.ExecuteScalar(CommandType.Text, sql, parameters)) > 0 ? true : false;
        }

        /// <summary>
        /// 通过昵称和密码登录
        /// </summary>
        /// <returns></returns>
        public ECustomerInfo GetModelByNameP(string nikename, string password)
        {
            ECustomerInfo model = null;
            //string sql = "select * from ecustomter where name=@nikename and password =@password";
            SqlParameter[] paramters = 
            {
                new SqlParameter("@nikename" , SqlDbType.VarChar , 256),
                new SqlParameter("@password" , SqlDbType.VarChar , 50)
            };
            paramters[0].Value = nikename;
            paramters[1].Value = password;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "select * from ECustomer where name=@nikename and password=@password", paramters))
            {

                while (dr.Read())
                {
                    model = new ECustomerInfo();
                    model.DataID = HJConvert.ToInt32(dr["dataid"]);
                    model.Name = HJConvert.ToString(dr["name"]);
                    model.TrueName = HJConvert.ToString(dr["truename"]);
                    model.Tell = HJConvert.ToString(dr["tell"]);
                    model.Phone = HJConvert.ToString(dr["phone"]);
                    model.EMAIL = HJConvert.ToString(dr["email"]);
                    model.Point = HJConvert.ToInt32(dr["point"]);
                    model.RegTime = HJConvert.ToDateTime(dr["regtime"]);
                    model.QQ = HJConvert.ToString(dr["QQ"]);
                    model.MSN = HJConvert.ToString(dr["msn"]);
                    model.Picture = HJConvert.ToString(dr["picture"]);
                    model.Password = HJConvert.ToString(dr["password"]);
                    model.IsActivate = HJConvert.ToInt32(dr["isactivate"]);
                    model.ActivateCode = HJConvert.ToString(dr["activatecode"]);
                   model.GroupID = HJConvert.ToDecimal(dr["GroupID"]);
                    model.WebSite = HJConvert.ToString(dr["WebSite"]);
                    model.RID = HJConvert.ToString(dr["rid"]);
                    //model.Usermoney = HJConvert.ToDecimal(dr["userMoney"]);
                    //model.PhoneActivate = HJConvert.ToInt32(dr["PhoneActivate"]);
                }
            }
            return model;

        }

        public int UpdateUserValue(int uid, string param, DateTime Value)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update ecustomer set ");
            str.Append(param);
            str.Append("=@value");
            str.Append(" where dataid=@uid");
            SqlParameter[] Para = 
            {
                new SqlParameter("@value", SqlDbType.DateTime),
                new SqlParameter("@uid", SqlDbType.Int,4)
            };
            Para[0].Value = Value;
            Para[1].Value = uid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        public int UpdateUserValue(int uid, string param, int intValue)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update ecustomer set ");
            str.Append(param);
            str.Append("=@value");
            str.Append(" where dataid=@uid");
            SqlParameter[] Para = 
            {
                new SqlParameter("@value", SqlDbType.Int),
                new SqlParameter("@uid", SqlDbType.Int,4)
            };
            Para[0].Value = intValue;
            Para[1].Value = uid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }

        public int UpdateUserValue(int uid, string param, string strValue)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update ecustomer set ");
            str.Append(param);
            str.Append("=@value");
            str.Append(" where dataid=@uid");
            SqlParameter[] Para = 
            {
                new SqlParameter("@value", SqlDbType.VarChar,4096),
                new SqlParameter("@uid", SqlDbType.Int,4)
            };
            Para[0].Value = strValue;
            Para[1].Value = uid;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), Para);
        }


        public ECustomerInfo GetModel(string username)
        {
            ECustomerInfo model = null;
            SqlParameter[] paramter = 
            {
                new SqlParameter("@nikename" , SqlDbType.VarChar ,256)
            };
            paramter[0].Value = username;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, "select * from ecustomer where name=@nikename", paramter))
            {

                while (dr.Read())
                {
                    model = new ECustomerInfo();
                    model.DataID = HJConvert.ToInt32(dr["dataid"]);
                    model.Name = HJConvert.ToString(dr["name"]);
                    model.TrueName = HJConvert.ToString(dr["truename"]);
                    model.Tell = HJConvert.ToString(dr["tell"]);
                    model.Phone = HJConvert.ToString(dr["phone"]);
                    model.EMAIL = HJConvert.ToString(dr["email"]);
                    model.Point = HJConvert.ToInt32(dr["point"]);
                    model.RegTime = HJConvert.ToDateTime(dr["regtime"]);
                    model.QQ = HJConvert.ToString(dr["QQ"]);
                    model.MSN = HJConvert.ToString(dr["msn"]);
                    model.Picture = HJConvert.ToString(dr["picture"]);
                    model.Password = HJConvert.ToString(dr["password"]);
                    model.IsActivate = HJConvert.ToInt32(dr["isactivate"]);
                    model.ActivateCode = HJConvert.ToString(dr["activatecode"]);
                   model.GroupID = HJConvert.ToDecimal(dr["GroupID"]);
                    model.WebSite = HJConvert.ToString(dr["WebSite"]);
                    model.RID = HJConvert.ToString(dr["rid"]);
                    //model.Usermoney = HJConvert.ToDecimal(dr["userMoney"]);
                    //model.PhoneActivate = HJConvert.ToInt32(dr["PhoneActivate"]);
                }
            }
            return model;
        }


        public ECustomerInfo GetModelByNameAPassword(string nikename, string password)
        {
            ECustomerInfo model = null;
            SqlParameter[] parameters =
            {
                new SqlParameter("@name" , SqlDbType.VarChar , 50),
                new SqlParameter("@password" , SqlDbType.VarChar , 50)
            };
            parameters[0].Value = nikename;
            parameters[1].Value = password;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "ECustomer_GetModelbynameandpassword", parameters))
            {
                while (dr.Read())
                {
                    model = new ECustomerInfo();
                    model.DataID = HJConvert.ToInt32(dr["dataid"]);
                    model.Name = HJConvert.ToString(dr["name"]);
                    model.TrueName = HJConvert.ToString(dr["truename"]);
                    model.Tell = HJConvert.ToString(dr["tell"]);
                    model.Phone = HJConvert.ToString(dr["phone"]);
                    model.EMAIL = HJConvert.ToString(dr["email"]);
                    model.Point = HJConvert.ToInt32(dr["point"]);
                    model.RegTime = HJConvert.ToDateTime(dr["regtime"]);
                    model.QQ = HJConvert.ToString(dr["QQ"]);
                    model.MSN = HJConvert.ToString(dr["msn"]);
                    model.Picture = HJConvert.ToString(dr["picture"]);
                    model.Password = HJConvert.ToString(dr["password"]);
                    model.IsActivate = HJConvert.ToInt32(dr["isactivate"]);
                    model.ActivateCode = HJConvert.ToString(dr["activatecode"]);
                   model.GroupID = HJConvert.ToDecimal(dr["GroupID"]);
                    model.WebSite = HJConvert.ToString(dr["WebSite"]);
                    model.RID = HJConvert.ToString(dr["rid"]);
                    model.Usermoney = HJConvert.ToDecimal(dr["userMoney"]);
                    model.PhoneActivate = HJConvert.ToInt32(dr["PhoneActivate"]);
                    model.PayPassword = HJConvert.ToString(dr["PayPassword"]);
                    model.Sex = HJConvert.ToString(dr["Sex"]);
                    model.UC_ID = HJConvert.ToInt32(dr["UC_ID"]);
                    model.PayPWDAnswer = HJConvert.ToString(dr["PayPWDAnswer"]);
                    model.distributemoney = HJConvert.ToDecimal(dr["distributemoney"]);
                    model.qrcodeurl = HJConvert.ToString(dr["qrcodeurl"]);

                }
            }
            return model;
        }


        /// <summary>
        /// 根据手机号码和密码得到用户信息 2015-11-20 
        /// </summary>
        /// <param name="Tell">手机号码</param>
        /// <param name="password">MD5加密后的密码</param>
        /// <returns></returns>
        public ECustomerInfo GetModelByTellAPassword(string Tell, string password)
        {
            ECustomerInfo model = null;
            SqlParameter[] parameters =
            {
                new SqlParameter("@Tell" , SqlDbType.VarChar , 20),
                new SqlParameter("@password" , SqlDbType.VarChar , 50)
            };
            parameters[0].Value = Tell;
            parameters[1].Value = password;

            string sql = "SELECT TOP 1 * FROM dbo.ECustomer WHERE Tell=@Tell AND Password=@password";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, parameters))
            {
                while (dr.Read())
                {
                    model = new ECustomerInfo();
                    model.DataID = HJConvert.ToInt32(dr["dataid"]);
                    model.Name = HJConvert.ToString(dr["name"]);
                    model.TrueName = HJConvert.ToString(dr["truename"]);
                    model.Tell = HJConvert.ToString(dr["tell"]);
                    model.Phone = HJConvert.ToString(dr["phone"]);
                    model.EMAIL = HJConvert.ToString(dr["email"]);
                    model.Point = HJConvert.ToInt32(dr["point"]);
                    model.RegTime = HJConvert.ToDateTime(dr["regtime"]);
                    model.QQ = HJConvert.ToString(dr["QQ"]);
                    model.MSN = HJConvert.ToString(dr["msn"]);
                    model.Picture = HJConvert.ToString(dr["picture"]);
                    model.Password = HJConvert.ToString(dr["password"]);
                    model.IsActivate = HJConvert.ToInt32(dr["isactivate"]);
                    model.ActivateCode = HJConvert.ToString(dr["activatecode"]);
                   model.GroupID = HJConvert.ToDecimal(dr["GroupID"]);
                    model.WebSite = HJConvert.ToString(dr["WebSite"]);
                    model.RID = HJConvert.ToString(dr["rid"]);
                    model.Usermoney = HJConvert.ToDecimal(dr["userMoney"]);
                    model.PhoneActivate = HJConvert.ToInt32(dr["PhoneActivate"]);
                    model.PayPassword = HJConvert.ToString(dr["PayPassword"]);
                    model.Sex = HJConvert.ToString(dr["Sex"]);
                    model.UC_ID = HJConvert.ToInt32(dr["UC_ID"]);
                }
            }
            return model;
        }


        public int UpateActivate(string email, string code)
        {
            string sql = "update ecustomer set isactivate = 1 where email = @email and activatecode = @code";
            SqlParameter[] parameter =
            {
                new SqlParameter("@email", SqlDbType.VarChar, 30),
                new SqlParameter("@code" , SqlDbType.VarChar , 200)
            };
            parameter[0].Value = email;
            parameter[1].Value = code;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
        }

        /// <summary>
        /// 更新用户账户金额 传入的参数 SetString: money = money -12 或者 money = money + 12 
        /// </summary>
        /// <param name="SetString"></param>
        /// <returns></returns>
        public int UpdateMoney(string SetString, int DataId)
        {

            string sql = "update ecustomer set " + SetString + " where DataId=" + DataId.ToString() + "";

            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        /// <summary>
        /// method to udpate activatecode;
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public int UpdateCode(string email, string code)
        {
            string sql = "update ecustomer set activatecode = @code where email = @email";
            SqlParameter[] parameter =
            {
                new SqlParameter("@email", SqlDbType.VarChar, 30),
                new SqlParameter("@code" , SqlDbType.VarChar , 200)
            };
            parameter[0].Value = email;
            parameter[1].Value = code;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
        }

        /*--月统计
        select month(ordertime),count(orderid) from etogoorder where year(ordertime) = '2010' group by month(ordertime)

        --日统计
        select day(ordertime),count(orderid) from etogoorder where year(ordertime) = '2010' and month(ordertime)='7'  group by day(ordertime)

        --小时统计
        select datepart(hh,ordertime),count(orderid) from etogoorder where year(ordertime) = '2010' and month(ordertime)='7' and day(ordertime)='16'  group by datepart(hh,ordertime)
        */

        /// <summary>
        /// Type:1 小时统计 2 日统计 3 周统计 4 月统计 5 年统计
        /// Year 需要统计的年份 Month需要统计的月份 Day需要统计的日 
        /// 小时统计需要Year Month Day
        /// 日统计需要Year Month 
        /// 月统计需要 Year
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public IList<OrderCountInfo> GetUserCount(int Type, string Year, string Month, string Day)
        {
            IList<OrderCountInfo> list = new List<OrderCountInfo>();
            string strSql = "";
            switch (Type)
            {
                case 1: strSql = "select datepart(hh,RegTime) as CountKey,count(dataid) as CountIntValue,Sum(dataid) as CountDecimalPrice from ECustomer where year(RegTime) = '" + Year + "' and month(RegTime)='" + Month + "'  and day(RegTime)='" + Day + "'  group by datepart(hh,RegTime)"; break;
                case 2: strSql = "select day(RegTime) as CountKey,count(dataid) as CountIntValue,Sum(dataid) as CountDecimalPrice from ECustomer where year(RegTime) = '" + Year + "' and month(RegTime)='" + Month + "'  group by day(RegTime)"; break;
                case 3: strSql = ""; break;
                case 4: strSql = "select month(RegTime) as CountKey,count(dataid) as CountIntValue,Sum(dataid) as CountDecimalPrice from ECustomer where year(RegTime) = '" + Year + "' group by month(RegTime)"; break;
                case 5: strSql = ""; break;
            }

            OrderCountInfo info = new OrderCountInfo();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, strSql, null))
            {
                while (dr.Read())
                {
                    info = new OrderCountInfo();

                    info.CountKey = HJConvert.ToString(dr["CountKey"]);
                    info.CountIntValue = HJConvert.ToInt32(dr["CountIntValue"]);
                    info.CountDecimalValue = HJConvert.ToDecimal(dr["CountDecimalPrice"]);

                    list.Add(info);
                }
            }

            return list;
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
        public IList<ECustomerInfo> giveCardUsers(int pagesize, int pageindex, string strWhere, string orderwhere,string otherwhere)
        {
            IList<ECustomerInfo> infos = new List<ECustomerInfo>();
            SqlParameter[] parameters =
            {
                new SqlParameter("@PageSize", SqlDbType.Int),
                new SqlParameter("@PageIndex", SqlDbType.Int),
                new SqlParameter("@strWhere", SqlDbType.VarChar,1500),
                new SqlParameter("@orderwhere", SqlDbType.VarChar,1500),
                new SqlParameter("@otherwhere", SqlDbType.VarChar,1500)
            };
            parameters[0].Value = pagesize;
            parameters[1].Value = pageindex;
            parameters[2].Value = strWhere;
            parameters[3].Value = orderwhere;
            parameters[4].Value = otherwhere;


            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "ECustomer_giveCardUsers", parameters))
            {
                while (dr.Read())
                {
                    ECustomerInfo info = new ECustomerInfo();
                    info.DataID = HJConvert.ToInt32(dr["unid"]);
                    info.Name = HJConvert.ToString(dr["classname"]);
                    info.UC_ID = HJConvert.ToInt32(dr["ordercount"]);
                    info.num = HJConvert.ToInt32(dr["recordtcount"]);
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
        public IList<ECustomerInfo> GetList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ECustomerInfo> infos = new List<ECustomerInfo>();
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
            parameters[0].Value = "ECustomer";
            parameters[1].Value = "*,(select max(OrderDateTime) from Custorder where UserId = ecustomer.dataid) as lastordertime ";
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
                    ECustomerInfo info = new ECustomerInfo();
                    info.DataID = HJConvert.ToInt32(dr["DataID"]);
                    info.Name = HJConvert.ToString(dr["Name"]);
                    info.TrueName = HJConvert.ToString(dr["TrueName"]);
                    info.Sex = HJConvert.ToString(dr["Sex"]);
                    info.Tell = HJConvert.ToString(dr["Tell"]);
                    info.Phone = HJConvert.ToString(dr["Phone"]);
                    info.QQ = HJConvert.ToString(dr["QQ"]);
                    info.MSN = HJConvert.ToString(dr["MSN"]);
                    info.RegTime = HJConvert.ToDateTime(dr["RegTime"]);
                    info.Point = HJConvert.ToInt32(dr["Point"]);
                    info.Picture = HJConvert.ToString(dr["Picture"]);
                    info.State = HJConvert.ToString(dr["State"]);
                    info.EMAIL = HJConvert.ToString(dr["Email"]);
                    info.Password = HJConvert.ToString(dr["Password"]);
                    info.IsActivate = HJConvert.ToInt32(dr["isActivate"]);
                    info.ActivateCode = HJConvert.ToString(dr["ActivateCode"]);
                    info.GroupID = HJConvert.ToDecimal(dr["groupid"]);
                    info.WebSite = HJConvert.ToString(dr["WebSite"]);
                    info.Usermoney = HJConvert.ToDecimal(dr["userMoney"]);
                    info.PhoneActivate = HJConvert.ToInt32(dr["PhoneActivate"]);
                    info.PayPassword = HJConvert.ToString(dr["PayPassword"]);
                    info.lastordertime = HJConvert.ToString(dr["lastordertime"]);
                    info.UC_ID = HJConvert.ToInt32(dr["UC_ID"]);
                    infos.Add(info);
                }
            }
            return infos;
        }



        /// <summary>
        /// 获取分销商列表（包含下线人数）
        /// </summary>
        /// <param name="pagesize">页尺寸</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderType">排序类型，1为降序，0为升序</param>
        /// <returns>图书列表</returns>
        ///此代码由杭景科技代码内部生成器自动生成
        public IList<ECustomerInfo> getdistributor(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {
            IList<ECustomerInfo> infos = new List<ECustomerInfo>();
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
            parameters[0].Value = "ECustomer";
            parameters[1].Value = "*,(select count(1) from distributor where (onegradeID = ecustomer.dataid or twogradeID = ecustomer.dataid or thressgradeID = ecustomer.dataid)) as juniorcount ";
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
                    ECustomerInfo info = new ECustomerInfo();
                    info.DataID = HJConvert.ToInt32(dr["DataID"]);
                    info.Name = HJConvert.ToString(dr["Name"]);
                    info.TrueName = HJConvert.ToString(dr["TrueName"]);
                    info.Sex = HJConvert.ToString(dr["Sex"]);
                    info.Tell = HJConvert.ToString(dr["Tell"]);
                    info.Phone = HJConvert.ToString(dr["Phone"]);
                    info.QQ = HJConvert.ToString(dr["QQ"]);
                    info.MSN = HJConvert.ToString(dr["MSN"]);
                    info.RegTime = HJConvert.ToDateTime(dr["RegTime"]);
                    info.Point = HJConvert.ToInt32(dr["Point"]);
                    info.Picture = HJConvert.ToString(dr["Picture"]);
                    info.State = HJConvert.ToString(dr["State"]);
                    info.EMAIL = HJConvert.ToString(dr["Email"]);
                    info.Password = HJConvert.ToString(dr["Password"]);
                    info.IsActivate = HJConvert.ToInt32(dr["isActivate"]);
                    info.ActivateCode = HJConvert.ToString(dr["ActivateCode"]);
                    info.GroupID = HJConvert.ToDecimal(dr["groupid"]);
                    info.WebSite = HJConvert.ToString(dr["WebSite"]);
                    info.Usermoney = HJConvert.ToDecimal(dr["userMoney"]);
                    info.PhoneActivate = HJConvert.ToInt32(dr["PhoneActivate"]);
                    info.PayPassword = HJConvert.ToString(dr["PayPassword"]);
                    info.UC_ID = HJConvert.ToInt32(dr["juniorcount"]);
                    info.distributemoney = HJConvert.ToDecimal(dr["distributemoney"]);





                    infos.Add(info);
                }
            }
            return infos;
        }


        /// <summary>
        /// 获取用户地址
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public EAddressInfo GetaddressModel(int userid)
        {
            EAddressInfo model = null;
            string sql = "select a.* , b.Name ,c.aname from EAddress a left join EBuilding b on a.BuildingID = b.DataID left join areas c on b.SectionID = c.unid  where userid = @userid order by pri desc";

            SqlParameter[] paramter = 
            {
                new SqlParameter("@userid" , SqlDbType.Int ,5)
            };
            paramter[0].Value = userid;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, paramter))
            {

                while (dr.Read())
                {
                    model = new EAddressInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.BuildingID = HJConvert.ToInt32(dr["BuildingID"]);
                    model.Address = HJConvert.ToString(dr["Address"]);
                    model.Phone = HJConvert.ToString(dr["phone"]);
                    model.Mobilephone = HJConvert.ToString(dr["mobilephone"]);
                    model.Receiver = HJConvert.ToString(dr["aname"]);
                    model.BuildingName = HJConvert.ToString(dr["Name"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 根据用户编号获取用户名和邮件地址
        /// </summary>
        /// <param name="UserIdList"></param>
        /// <returns></returns>
        public DataTable GetEmailAddressList(string UserIdList)
        {
            string sql = "select name, email from ecustomer where dataid in (" + UserIdList + ")";

            DataTable dt = new DataTable();
            DataSet ds = SQLHelper.Query(CommandType.Text, sql, null);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据用户编号获取用户名和邮件地址
        /// </summary>
        /// <param name="UserIdList"></param>
        /// <returns></returns>
        public DataTable GetPhoneList(string UserIdList)
        {
            string sql = "select name, tell from ecustomer where dataid in (" + UserIdList + ")";
            DataTable dt = new DataTable();
            DataSet ds = SQLHelper.Query(CommandType.Text, sql, null);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 通过来电获取用户信息(查看name和Tell两个字段)
        /// </summary>
        /// <returns></returns>
        public ECustomerInfo GetListByTel(string Tell)
        {
            ECustomerInfo model = null;
            string sql = "select * from ECustomer where 1=1 and ( Name = '" + Tell + "' or Tell ='" + Tell + "' )";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    model = new ECustomerInfo();
                    model.DataID = HJConvert.ToInt32(dr["dataid"]);
                    model.Name = HJConvert.ToString(dr["name"]);
                    model.TrueName = HJConvert.ToString(dr["truename"]);
                    model.Tell = HJConvert.ToString(dr["tell"]);
                    model.Phone = HJConvert.ToString(dr["phone"]);
                    model.EMAIL = HJConvert.ToString(dr["email"]);
                    model.Point = HJConvert.ToInt32(dr["point"]);
                    model.RegTime = HJConvert.ToDateTime(dr["regtime"]);
                    model.QQ = HJConvert.ToString(dr["QQ"]);
                    model.MSN = HJConvert.ToString(dr["msn"]);
                    model.Picture = HJConvert.ToString(dr["picture"]);
                    model.Password = HJConvert.ToString(dr["password"]);
                    model.IsActivate = HJConvert.ToInt32(dr["isactivate"]);
                    model.ActivateCode = HJConvert.ToString(dr["activatecode"]);
                   model.GroupID = HJConvert.ToDecimal(dr["GroupID"]);
                    model.WebSite = HJConvert.ToString(dr["WebSite"]);
                    model.RID = HJConvert.ToString(dr["rid"]);
                    model.State = HJConvert.ToString(dr["State"]);
                }
            }
            return model;

        }

        /// <summary>
        /// 验证手机是否可用。
        /// </summary>
        /// <param name="nikename"></param>
        /// <returns></returns>
        public bool IsExistPhone(string Tell)
        {
            string sql = "select count(*) from ecustomer where Tell=@nikename";
            SqlParameter[] parameters = 
            {
                new SqlParameter("@nikename" ,SqlDbType.VarChar , 30)
            };
            parameters[0].Value = Tell;
            return ((int)SQLHelper.ExecuteScalar(CommandType.Text, sql, parameters)) > 0 ? false : true;
        }


        /**********************************第三方登陆相关方法******************************************************/
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add_Third(Hangjing.Model.ECustomerInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ECustomer(");
            strSql.Append("Name,TrueName,Sex,Tell,Phone,QQ,MSN,RegTime,Point,Picture,State,EMAIL ,password ,isActivate ,ActivateCode ,groupid , WebSite ,rid,openid,wtype,Usermoney)");
            strSql.Append(" values (");
            strSql.Append("@Name,@TrueName,@Sex,@Tell,@Phone,@QQ,@MSN,@RegTime,@Point,@Picture,@State,@EMAIL ,@password ,@isActivate ,@ActivateCode , @groupid , @WebSite ,@rid,@openid,@wtype,@Usermoney)");
            SqlParameter[] parameters = 
            {			
				new SqlParameter("@Name", SqlDbType.VarChar,256),
				new SqlParameter("@TrueName", SqlDbType.VarChar,256),
				new SqlParameter("@Sex", SqlDbType.VarChar ,1),
				new SqlParameter("@Tell", SqlDbType.VarChar,20),
				new SqlParameter("@Phone", SqlDbType.VarChar,20),
				new SqlParameter("@QQ", SqlDbType.VarChar,20),
				new SqlParameter("@MSN", SqlDbType.VarChar,256),
				new SqlParameter("@RegTime", SqlDbType.DateTime),
				new SqlParameter("@Point", SqlDbType.Int,4),
				new SqlParameter("@Picture", SqlDbType.VarChar,256),
				new SqlParameter("@State", SqlDbType.VarChar,10),
				new SqlParameter("@EMAIL", SqlDbType.VarChar,30),
                new SqlParameter("@password" , SqlDbType.VarChar , 50),
                new SqlParameter("@isActivate" , SqlDbType.Int),
                new SqlParameter("@ActivateCode" , SqlDbType.VarChar , 200),
                new SqlParameter("@groupid" , SqlDbType.Int , 5),
                new SqlParameter("@WebSite" , SqlDbType.VarChar , 50),
                new SqlParameter("@rid" , SqlDbType.VarChar , 50),
                new SqlParameter("@openid" , SqlDbType.VarChar , 50),
                new SqlParameter("@wtype" , SqlDbType.VarChar , 50),
                new SqlParameter("@Usermoney" , SqlDbType.Decimal, 9)
            };
            parameters[0].Value = model.Name != null ? model.Name : "";
            parameters[1].Value = model.TrueName != null ? model.TrueName : "";
            parameters[2].Value = model.Sex != null ? model.Sex : "";
            parameters[3].Value = model.Tell != null ? model.Tell : "";
            parameters[4].Value = model.Phone != null ? model.Phone : "";
            parameters[5].Value = model.QQ;
            parameters[6].Value = model.MSN != null ? model.MSN : "";
            parameters[7].Value = model.RegTime;
            parameters[8].Value = model.Point;
            parameters[9].Value = model.Picture != null ? model.Picture : "";
            parameters[10].Value = 0;
            parameters[11].Value = model.EMAIL;
            parameters[12].Value = model.Password;
            parameters[13].Value = model.IsActivate;
            parameters[14].Value = model.ActivateCode;
            parameters[15].Value = model.GroupID;
            parameters[16].Value = model.WebSite;
            parameters[17].Value = model.RID;
            parameters[18].Value = model.openid;
            parameters[19].Value = model.wtype;
            parameters[20].Value = model.Usermoney;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 通过第三方登陆接口信息获取用户信息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="wtype"></param>
        /// <returns></returns>
        public ECustomerInfo GetModelByThird(string openid, string wtype)
        {
            ECustomerInfo model = null;
            SqlParameter[] parameters = 
            {
                new SqlParameter("@openid" , SqlDbType.VarChar , 256),
                new SqlParameter("@wtype" , SqlDbType.VarChar , 50),
            };
            parameters[0].Value = openid;
            parameters[1].Value = wtype;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, "select * from ECustomer where openid=@openid AND wtype=@wtype ", parameters))
            {
                while (dr.Read())
                {
                    model = new ECustomerInfo();
                    model.DataID = HJConvert.ToInt32(dr["dataid"]);
                    model.Name = HJConvert.ToString(dr["name"]);
                    model.TrueName = HJConvert.ToString(dr["truename"]);
                    model.Tell = HJConvert.ToString(dr["tell"]);
                    model.Phone = HJConvert.ToString(dr["phone"]);
                    model.EMAIL = HJConvert.ToString(dr["email"]);
                    model.Point = HJConvert.ToInt32(dr["point"]);
                    model.RegTime = HJConvert.ToDateTime(dr["regtime"]);
                    model.QQ = HJConvert.ToString(dr["QQ"]);
                    model.MSN = HJConvert.ToString(dr["msn"]);
                    model.Picture = HJConvert.ToString(dr["picture"]);
                    model.Password = HJConvert.ToString(dr["password"]);
                    model.IsActivate = HJConvert.ToInt32(dr["isactivate"]);
                    model.ActivateCode = HJConvert.ToString(dr["activatecode"]);
                   model.GroupID = HJConvert.ToDecimal(dr["GroupID"]);
                    model.WebSite = HJConvert.ToString(dr["WebSite"]);
                    model.RID = HJConvert.ToString(dr["rid"]);
                    model.Usermoney = HJConvert.ToDecimal(dr["userMoney"]);
                    model.PhoneActivate = HJConvert.ToInt32(dr["PhoneActivate"]);
                    model.PayPassword = HJConvert.ToString(dr["PayPassword"]);
                    model.Sex = HJConvert.ToString(dr["Sex"]);
                    model.wtype = HJConvert.ToString(dr["wtype"]);
                    model.openid = HJConvert.ToString(dr["openid"]);
                }
            }
            return model;
        }

        /// <summary>
        /// 传用户应加米粒，描述,用户编号
        /// </summary>
        /// <returns></returns>
        public int addpoint(int uid, string msg, int addpoint)
        {
            SqlParameter[] Para = 
            {
                new SqlParameter("@userid",SqlDbType.Int,5),
                new SqlParameter("@addpoint",SqlDbType.Int , 5),
                new SqlParameter("@msg",SqlDbType.VarChar,200),
            };
            Para[0].Value = uid;
            Para[1].Value = addpoint;
            Para[2].Value = msg;

            SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "ECustomer_AddPoint", Para);

            return 1;
        }

        /// <summary>
        /// 会员订餐Top10
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public IList<FoodlistInfo> UserSaleTOP10(string orderwhere, string ordername)
        {
            SqlParameter[] Para = 
            {
                new SqlParameter("@orderwhere",SqlDbType.VarChar,2000),
                new SqlParameter("@ordername",SqlDbType.VarChar,200)
            };
            Para[0].Value = orderwhere;
            Para[1].Value = ordername;

            IList<FoodlistInfo> DataList = new List<FoodlistInfo>();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "UserSaleTOP10", Para))
            {
                while (dr.Read())
                {
                    FoodlistInfo info = new FoodlistInfo();
                    info.FoodPrice = HJConvert.ToDecimal(dr["TotalPrice"]);
                    info.FCounts = HJConvert.ToInt32(dr["ordercount"]);
                    info.FoodName = HJConvert.ToString(dr["Name"]);
                    DataList.Add(info);
                }
            }
            return DataList;
        }


        /// <summary>
        /// 同行活跃度统计
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public IList<ECustomerInfo> getuser_ActivityStatis(int day)
        {
            IList<ECustomerInfo> list = new List<ECustomerInfo>();
            SqlParameter[] Para = 
            {
                new SqlParameter("@day",SqlDbType.Int)
            };
            Para[0].Value = day;
            ECustomerInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "user_ActivityStatis", Para))
            {
                while (dr.Read())
                {
                    model = new ECustomerInfo();
                    model.DataID = HJConvert.ToInt32(dr["UserId"]);
                    model.Name = HJConvert.ToString(dr["username"]);
                    list.Add(model);
                }
            }
            return list;

        }

        /// <summary>
        /// 用户订单排行榜 (orderSums：金额排序，num数量排序)
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public IList<ECustomerInfo> getuser_orderStatis(string where, string order)
        {
            string sql = @"select DataID,Name,count(orderid) as num,sum(OrderSums) as orderSums from ECustomer left join Custorder on Custorder.UserId = ECustomer.DataID where 1 = 1 " + where + " group by DataID,Name order by " + order + " desc ";
            IList<ECustomerInfo> list = new List<ECustomerInfo>();
            ECustomerInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    model = new ECustomerInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.num = HJConvert.ToInt32(dr["num"]);
                    model.orderSums = HJConvert.ToInt32(dr["orderSums"]);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        ///注册以来（7天前注册）只定过一次的用户
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public IList<ECustomerInfo> getuser_workOnceOrderStatis()
        {
            string sql = @"select DataID,Name,count(orderid) as num from ECustomer left join Custorder on Custorder.UserId = ECustomer.DataID where  RegTime < convert(datetime,convert(char(20),dateadd(day,-6,getdate()),102)) group by DataID,Name having count(orderid) = 1 ";
            IList<ECustomerInfo> list = new List<ECustomerInfo>();
            ECustomerInfo model = null;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    model = new ECustomerInfo();
                    model.DataID = HJConvert.ToInt32(dr["DataID"]);
                    model.Name = HJConvert.ToString(dr["Name"]);
                    model.num = HJConvert.ToInt32(dr["num"]);
                    list.Add(model);
                }
            }
            return list;
        }
    }
}
