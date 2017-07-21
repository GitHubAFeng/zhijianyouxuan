using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
/// 取消提现
/// </summary>
public partial class App_Android_Deliver_cancelcashout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        int cid = HjNetHelper.GetQueryInt("cid", 0);


        string sql = "UPDATE TogoAddMoneyLog SET PayState = 3 WHERE PayState = 0 and   dataid = " + cid;

        if (WebUtility.excutesql(sql) > 0)
        {
            Response.Write("{\"msg\":\"取消成功\",\"state\":\"1\"}");
            Response.End();
        }
        else
        {
            Response.Write("{\"msg\":\"此申请，当前不能取消\",\"state\":\"0\"}");
            Response.End();
        }


        Response.End();
    }
}
