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

/// <summary>
/// 提出记录
/// </summary>
public partial class AndroidAPI_shop_GetCashOutList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        TogoAddMoneyLog dal = new TogoAddMoneyLog();

        int pagesize =  HjNetHelper.GetPostParam("pagesize", 8);
        int pageindex =  HjNetHelper.GetPostParam("pageindex", 1);
        int pagecount = 1;

        string order_state = WebUtility.InputText(Request["state"]);//订单状态
        string starttime = WebUtility.InputText(Request["starttime"]);//开始日期
        string endtime = WebUtility.InputText(Request["endtime"]);//结束日期
        string did = WebUtility.InputText(Request["did"]);//商家编号

        StringBuilder orderlistjson = new StringBuilder();

        string SqlWhere = " PayType = 7 and  userid=" + did;

        if (order_state != "" && order_state != "-1")
        {
            SqlWhere += " and PayState=" + order_state;
        }
        if (starttime != "")
        {
            SqlWhere += " and  AddDate > '" + starttime + "' ";
        }
        if (endtime != "")
        {
            SqlWhere = SqlWhere + " and AddDate <  '" + endtime + " 23:59:59'";
        }


        int count = dal.GetCount(SqlWhere);
        IList<TogoAddMoneyLogInfo> list = dal.GetList(pagesize, pageindex, SqlWhere, "dataid", 1);

        if (count % pagesize == 0)//整数倍
        {
            pagecount = count / pagesize;
        }
        else
        {
            pagecount = count / pagesize + 1;
        }

        orderlistjson.Append("{\"page\":\"" + pageindex.ToString() + "\",\"total\":\"" + pagecount + "\",\"record\":\"" + count + "\", \"orderlist\":[");

        for (int i = 0; i < list.Count; i++)
        {
            TogoAddMoneyLogInfo info = list[i];
            orderlistjson.Append("{\"addtime\":\"" + info.AddDate + "\"");
            orderlistjson.Append(",\"wantmoney\":\"" + info.AddMoney + "\"");
            orderlistjson.Append(",\"state\":\"" + info.PayState + "\"");
            orderlistjson.Append(",\"cid\":\"" + info.DataId + "\"");
            orderlistjson.Append(",\"remark\":\"" + WebUtility.FileterJson(info.Inve2) + "\"");
            orderlistjson.Append("},");
        }

        orderlistjson.Append("]}");

        Response.Write(orderlistjson.ToString().Replace("},]}", "}]}"));
        Response.End();

    }
}
