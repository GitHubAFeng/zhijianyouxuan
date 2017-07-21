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

public partial class App_AndriodV2_GetIntegralList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        int pageindex = HjNetHelper.GetQueryInt("pageindex", 1);
        int userid =  HjNetHelper.GetQueryInt("userid", 0);
        int pagesize = 8;
        int pagecount = 1;
        string sql = " 1=1 ";
        sql += " and custid='" + userid + "'";
        Integral dal = new Integral();
        IList<IntegralInfo> list = dal.GetList(pagesize, pageindex, sql, "IntegralId", 1);


        int count = dal.GetCount(sql);
        if (count % pagesize == 0)
        {
            pagecount = count / pagesize;
        }
        else
        {
            pagecount = count / pagesize + 1;
        }
        StringBuilder shoplistjson = new StringBuilder();
        shoplistjson.Append("{\"page\":\"" + pageindex + "\",\"total\":\"" + pagecount + "\",\"datalist\":[");

        IntegralInfo info = new IntegralInfo();

        for (int i = 0; i < list.Count; i++)
        {
            info = list[i];
            shoplistjson.Append("{\"IntegralId\":\"" + info.IntegralId + "\",\"GiftName\":\"" + info.GiftName + "\",\"PayIntegral\":\"" + info.PayIntegral + "\",\"Cdate\":\"" + info.Cdate + "\",\"State\":\"" + info.State + "\"},");
        }

        shoplistjson.Append("]}");

        Response.Write(shoplistjson.ToString().Replace(",]}", "]}"));
        Response.End();
    }
}
