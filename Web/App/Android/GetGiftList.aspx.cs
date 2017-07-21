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

public partial class Android_GetGiftList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        int pagesize = HjNetHelper.GetPostParam("pagesize", 8);
        int pageindex = HjNetHelper.GetPostParam("pageindex", 1);
        int sortid = HjNetHelper.GetPostParam("sortid", 0);
        string ordername = "sortnum";

        int pagecount = 1;
        string sql = " 1=1 ";

        if (sortid > 0)
        {
            sql += "and ClassId=" + sortid.ToString() + "";
        }

        Gifts dal = new Gifts();
        IList<GiftsInfo> list = dal.GetList(pagesize, pageindex, sql, ordername, 1);


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

        GiftsInfo info = new GiftsInfo();

        for (int i = 0; i < list.Count; i++)
        {
            info = list[i];
            shoplistjson.Append("{\"GiftsId\":\"" + info.GiftsId + "\",\"Gname\":\"" + info.Gname + "\",\"NeedIntegral\":\"" + info.NeedIntegral + "\",\"GiftsPrice\":\"" + info.GiftsPrice.ToString() + "\",\"bigpicture\":\"" + info.bigpicture.Replace("~", WebUtility.GetConfigsite()) + "\",\"Picture\":\"" + info.Picture.Replace("~", WebUtility.GetConfigsite()) + "\",\"Stocks\":\"" + info.Stocks + "\",\"Content\":\"" + WebUtility.FileterJson(WebUtility.NoHTML(info.Content)) + "\",\"ClassId\":\"" + info.ClassId + "\"},");
        }

        shoplistjson.Append("]}");

        Response.Write(shoplistjson.ToString().Replace(",]}", "]}"));
        Response.End();

    }
}
