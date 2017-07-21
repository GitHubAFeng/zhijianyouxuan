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
using System.Reflection;
using System.Text;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;


/// <summary>
/// 根据坐标获取附近的外卖商家 传入坐标值 横坐标 纵坐标
/// </summary>
public partial class Ajax_MapFindShop : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        //获取用户点击的位置坐标信息
        string lat = HjNetHelper.GetPostParam("lat");
        string lng = HjNetHelper.GetPostParam("lng");

        string SqlWhere = "1=1 and IsDelete = 0 and Star = 1 and InUse = 'Y' and cityid = " + WebUtility.get_userCityid();
        string strDistance = " distance < Inve1 and Status=1 ";

        int count = new Points().GetCountWidthDistance(SqlWhere, "distance", 0, lat, lng, strDistance);

        string rs = "{\"msg\":\"成功\",\"data\":" + count + ",\"code\":0}";

        Response.Write(rs);
        Response.End();


    }


}
