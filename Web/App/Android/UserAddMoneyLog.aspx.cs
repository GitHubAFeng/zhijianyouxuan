using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Newtonsoft.Json;

public partial class App_Android_UserAddMoneyLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        UserAddMoneyLog dal = new UserAddMoneyLog();

        int pagesize = HjNetHelper.GetPostParam("pagesize", 8);
        int pageindex = HjNetHelper.GetPostParam("pageindex", 1);
        int pagecount = 1;

        string PayType = WebUtility.InputText(Request["PayType"]);//订单状态
        string did = WebUtility.InputText(Request["userid"]);//用户编号


        StringBuilder orderlistjson = new StringBuilder();

        string SqlWhere = " PayState=1  and  UserId=" + did;

        if (PayType != "" && PayType != "-1")
        {
            SqlWhere += " and PayType=" + PayType;
        }


        int count = 0;

        count = dal.GetCount(SqlWhere);
        IList<UserAddMoneyLogInfo> list = dal.GetList(pagesize, pageindex, SqlWhere, "DataId", 1);

        if (count % pagesize == 0)//整数倍
        {
            pagecount = count / pagesize;
        }
        else
        {
            pagecount = count / pagesize + 1;
        }

        orderlistjson.Append("{\"page\":\"" + pageindex.ToString() + "\",\"total\":\"" + pagecount + "\", \"datalist\":[");

        UserAddMoneyLogInfo info = new UserAddMoneyLogInfo();

        for (int i = 0; i < list.Count; i++)
        {
            info = list[i];

            orderlistjson.Append("{\"TogoName\":\"" + info.TogoName + "\",");

            orderlistjson.Append("\"AdminName\":\"" + info.AdminName + "\",");
            orderlistjson.Append("\"DataId\":\"" + info.DataId + "\",");
            orderlistjson.Append("\"UserId\":\"" + info.UserId + "\",");
            orderlistjson.Append("\"AddMoney\":\"" + info.AddMoney + "\",");
            orderlistjson.Append("\"State\":\"" + info.State + "\",");
            orderlistjson.Append("\"PayType\":\"" + info.PayType + "\",");
            orderlistjson.Append("\"PayDate\":\"" + info.PayDate.ToString("yyyy-MM-dd HH:mm:dd") + "\",");
            orderlistjson.Append("\"PayState\":\"" + info.PayState + "\",");
            orderlistjson.Append("\"Inve1\":\"" + info.Inve1 + "\",");
            orderlistjson.Append("\"Inve2\":\"" + info.Inve2 + "\",");
            orderlistjson.Append("\"UserName\":\"" + info.UserName + "\",");
            orderlistjson.Append("\"AddDate\":\"" + info.AddDate.ToString("yyyy-MM-dd HH:mm:dd") + "\"},");
        }

        orderlistjson.Append("]}");

        Response.Write(orderlistjson.ToString().Replace("},]}", "}]}"));
        Response.End();

    }
}
