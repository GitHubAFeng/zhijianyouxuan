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
using System.Collections;

public partial class App_shop_Getgpsrecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        //Response.Write(JsonConvert.SerializeObject(new GPS_Records().GetModel(163)));
        //Response.End();

        Response.Clear();
        int pagesize = HjNetHelper.GetPostParam("pagesize", 8);
        int pageindex = HjNetHelper.GetPostParam("pageindex", 1);
        int did = HjNetHelper.GetPostParam("did", 0);
        string ordername = "id";

        int pagecount = 1;
        string sql = " 1=1 ";

        if (did > 0)
        {
            sql += "and JH1=" + did;
        }

        GPS_Records dal = new GPS_Records();
        IList<GPS_RecordsInfo> list = dal.GetList(pagesize, pageindex, sql, ordername, 1);


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

        GPS_RecordsInfo info = new GPS_RecordsInfo();

        for (int i = 0; i < list.Count; i++)
        {
            info = list[i];

            shoplistjson.Append("{");
            shoplistjson.Append("\"ID\": \"" + info.ID + "\",");
            shoplistjson.Append("\"JH1\": \"" + info.JH1 + "\",");
            shoplistjson.Append("\"JH2\": \"" + info.JH2 + "\",");
            shoplistjson.Append("\"JH3\": \"" + info.JH3 + "\",");
            shoplistjson.Append("\"JH4\": \"" + info.JH4 + "\",");
            shoplistjson.Append("\"JH5\": \"" + info.JH5 + "\",");
            shoplistjson.Append("\"AddTime\": \"" + info.AddTime + "\",");
            shoplistjson.Append("\"AddName\": \"" + info.AddName + "\",");
            shoplistjson.Append("\"UpTime\": \"" + info.UpTime + "\",");
            shoplistjson.Append("\"Remark\": \"" + info.Remark + "\",");
            shoplistjson.Append("\"Del\": \"" + info.Del + "\",");
            shoplistjson.Append("\"baidu\": \"" + info.baidu + "\"");
            shoplistjson.Append("},");

        }

        shoplistjson.Append("]}");

        Response.Write(shoplistjson.ToString().Replace(",]}", "]}"));
        Response.End();
    }
}