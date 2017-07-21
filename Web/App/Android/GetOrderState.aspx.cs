using System;
using System.Collections.Generic;
using System.Text;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class App_Android_GetOrderState : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new StateConfig().GetModel(2)));
        //Response.End();

        Response.Clear();
        int pagesize = HjNetHelper.GetPostParam("pagesize", 8);
        int pageindex = HjNetHelper.GetPostParam("pageindex", 1);
        string ordername = "Priority";

        int pagecount = 1;
        string sql = " 1=1 and Parentid=1 ";

        StateConfig dal = new StateConfig();
        IList<StateConfigInfo> list = dal.GetList(pagesize, pageindex, sql, ordername, 1);


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
        shoplistjson.Append("{\"page\":\"" + pageindex + "\",\"total\":\"" + pagecount + "\",\"record\":\"" + count + "\",\"datalist\":[");

        StateConfigInfo info = new StateConfigInfo();

        for (int i = 0; i < list.Count; i++)
        {
            info = list[i];

            shoplistjson.Append("{");
            shoplistjson.Append("\"ID\":\"" + info.ID + "\",");
            shoplistjson.Append("\"classname\":\"" + info.classname + "\",");
            shoplistjson.Append("\"Depth\":\"" + info.Depth + "\",");
            shoplistjson.Append("\"Status\":\"" + info.Status + "\",");
            shoplistjson.Append("\"Priority\":\"" + info.Priority + "\",");
            shoplistjson.Append("\"Parentid\":\"" + info.Parentid + "\",");
            shoplistjson.Append("\"isDel\":\"" + info.isDel + "\"");
            shoplistjson.Append("},");
        }

        shoplistjson.Append("]}");

        Response.Write(shoplistjson.ToString().Replace(",]}", "]}"));
        Response.End();

    }
}
