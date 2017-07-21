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
/// 花马币记录列表
/// </summary>
public partial class AndroidAPI_pointrecordlist : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        int userid = HjNetHelper.GetPostParam("userid", 0);//会员编号

        EPointRecord dal = new EPointRecord();

        int pagesize = HjNetHelper.GetPostParam("pagesize", 8);
        int pageindex = HjNetHelper.GetPostParam("pageindex", 1);
        int pagecount = 1;

        string sql = "  UserId= " + userid;


        IList<EPointRecordInfo> list = dal.GetList(pagesize, pageindex, sql, "DataID", 1);

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
        shoplistjson.Append("{\"page\":\"" + pageindex + "\",\"total\":\"" + pagecount + "\", \"datalist\":[");

        EPointRecordInfo info = new EPointRecordInfo();

        for (int i = 0; i < list.Count; i++)
        {
            info = new EPointRecordInfo();
            info = list[i];
            shoplistjson.Append("{\"UserID\":\"" + info.UserID + "\",\"Comment\":\"" + info.Event + "\",\"Point\":\"" + info.Point + "\",\"PostTime\":\"" + info.Time.ToString() + "\"");
            shoplistjson.Append("},");
        }
        shoplistjson.Append("]}");

        Response.Write(shoplistjson.ToString().Replace(",]}", "]}"));
        Response.End();


    }
}
