using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class Admin_ajax_SelectUserPhone :AdminPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PointType = Request["pointtype"];
        string Point = Request["point"];
        string RegTime1 = Request["regtime1"];
        string RegTime2 = Request["regtime2"];

        string building = Request["building"];

        string UserIdList = "";
        string UserPhoneList = "";

        StringBuilder Json = new StringBuilder();
        ECustomer CBLL = new ECustomer();

        //筛选方式二
        if (!string.IsNullOrEmpty(building))
        {
            try
            {

                Response.AddHeader("Cache-Control", "no-cache");

                //获取用户编号列表 
                //查询Eaddress表，寻找字段BuildingId在building（页面传过来参数）内的用户编号
                EAddress bll = new EAddress();

                IList<EAddressInfo> AList = new List<EAddressInfo>();

                AList = bll.GetUserIdList(1000, 1, "BuildingId in( " + building + ")", "DataId", 1);

                if (AList != null)
                {
                    for (int i = 0; i < AList.Count; i++)
                    {
                        UserIdList += AList[i].UserID.ToString();
                        UserIdList += ",";
                    }
                }

                if (UserIdList.Length > 0)
                {
                    UserIdList = UserIdList.Substring(0, UserIdList.Length - 1);

                    //获取用户邮箱地址
                    //根据用户编号列表查询邮箱地址列表

                    DataTable dt = CBLL.GetPhoneList(UserIdList);

                    foreach (DataRow dr in dt.Rows)
                    {
                        UserPhoneList += dr["tell"].ToString();
                        UserPhoneList += ",";
                    }

                    if (UserPhoneList.Length > 0)
                    {
                        UserPhoneList = UserPhoneList.Substring(0, UserPhoneList.Length - 1);
                    }

                    //组织成json格式输出
                    Json.Append("{\"UserInfo\":[");

                    Json.Append("{");

                    Json.Append("\"UserIdList\":\"" + UserIdList + "\",");
                    Json.Append("\"UserPhoneList\":\"" + UserPhoneList + "\"");
                    Json.Append("}");

                    Json.Append("]}");
                    Json.ToString();

                    Response.Write(Json.ToString());
                    Response.End();
                }
                else
                {
                    Response.AddHeader("Cache-Control", "no-cache");
                    Response.Write("");
                    Response.End();
                }
            }
            catch
            {
                Response.AddHeader("Cache-Control", "no-cache");
                Response.Write("");
                Response.End();
            }
        }
        else
        {
            try
            {
                string SqlWhere = " 1=1 ";

                if (!string.IsNullOrEmpty(PointType))
                {
                    if (PointType == "-1")
                    {
                        SqlWhere += " AND Point < " + Point + " ";
                    }
                    else if (PointType == "0")
                    {
                        SqlWhere += " AND Point = " + Point + " ";
                    }
                    else
                    {
                        SqlWhere += " AND Point > " + Point + " ";
                    }
                }

                if (!string.IsNullOrEmpty(RegTime1))
                {
                    SqlWhere += " AND RegTime >= '" + RegTime1 + "' ";
                }
                if (!string.IsNullOrEmpty(RegTime2))
                {
                    SqlWhere += " AND RegTime <= '" + RegTime2 + "' ";
                }

                IList<ECustomerInfo> C1List = new List<ECustomerInfo>();

                C1List = CBLL.GetList(1000, 1, SqlWhere, "DataId", 1);

                if (C1List != null)
                {
                    for (int i = 0; i < C1List.Count; i++)
                    {
                        UserIdList += C1List[i].DataID.ToString();
                        UserIdList += ",";
                    }
                }

                if (UserIdList.Length > 0)
                {
                    UserIdList = UserIdList.Substring(0, UserIdList.Length - 1);

                    DataTable dt1 = CBLL.GetPhoneList(UserIdList);

                    foreach (DataRow dr in dt1.Rows)
                    {
                        UserPhoneList += dr["tell"].ToString();
                        UserPhoneList += ",";
                    }

                    if (UserPhoneList.Length > 0)
                    {
                        UserPhoneList = UserPhoneList.Substring(0, UserPhoneList.Length - 1);
                    }

                    Json.Append("{\"UserInfo\":[");

                    Json.Append("{");

                    Json.Append("\"UserIdList\":\"" + UserIdList + "\",");
                    Json.Append("\"UserPhoneList\":\"" + UserPhoneList + "\"");
                    Json.Append("}");

                    Json.Append("]}");
                    Json.ToString();

                    Response.Write(Json.ToString());

                    Response.End();
                }

                else
                {
                    Response.AddHeader("Cache-Control", "no-cache");
                    Response.Write("");
                    Response.End();
                }
                Response.Write(Json.ToString());
            }
            catch
            {
                Response.AddHeader("Cache-Control", "no-cache");
                Response.Write("");
                Response.End();
            }
        }
    }
}
