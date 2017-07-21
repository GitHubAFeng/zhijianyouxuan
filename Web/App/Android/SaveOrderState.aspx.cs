using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Common;
using Hangjing.SQLServerDAL;


/// <summary>
/// 用户确认收货 （更新订单状态为处理成功 并添加积分）
/// </summary>
public partial class App_Android_SaveOrderState : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string orderid = WebUtility.InputText(Request["orderid"]);//订单编号
        string userName = WebUtility.InputText(Request["userName"]);//用户名

        if (string.IsNullOrEmpty(orderid))
        {
            Response.Write("{\"orderid\":\"" + orderid + "\",\"state\":\"0\",\"msg\":\"请输入订单编号！\"}");
            Response.End();
            return;
        }

        Custorder dal = new Custorder();

        string sql = "update custorder set OrderStatus=3";
        sql += " where OrderID='" + orderid + "' and OrderStatus!=3 ";

        if (WebUtility.excutesql(sql) > 0)
        {
            dal.UserComplete(orderid, userName);//加积分和添加记录
            Response.Write("{\"orderid\":\"" + orderid + "\",\"state\":\"1\"}");
        }
        else
        {
            Response.Write("{\"orderid\":\"" + orderid + "\",\"state\":\"0\",\"msg\":\"您已经确认收货了！\"}");
        }


        Response.End();
    }


}