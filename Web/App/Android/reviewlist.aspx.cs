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
/// 商家评论列表
/// </summary>
public partial class AndroidAPI_reviewlist : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        int shopid = HjNetHelper.GetPostParam("shopid", 0);//商家编号

        ETogoOpinion dal = new ETogoOpinion();

        int pagesize = HjNetHelper.GetPostParam("pagesize", 8);
        int pageindex = HjNetHelper.GetPostParam("pageindex", 1);
        int pagecount = 1;

        string sql = "  1=1 ";

        if (shopid > 0)
        {
            sql += " and togoid=" + shopid + "";
        }

        IList<ETogoOpinionInfo> list = dal.GetList(pagesize, pageindex, sql, "posttime", 1);

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

        ETogoOpinionInfo info = new ETogoOpinionInfo();


        for (int i = 0; i < list.Count; i++)
        {
            info = new ETogoOpinionInfo();
            info = list[i];
            shoplistjson.Append("{\"UserID\":\"" + info.UserID + "\",\"TogoID\":\"" + info.TogoID + "\",\"Comment\":\"" + WebUtility.FileterJson(info.Comment) + "\",\"Point\":\"" + info.Point + "\",\"PostTime\":\"" + info.PostTime.ToString() + "\",");
            shoplistjson.Append("\"ServiceGrade\":\"" + info.ServiceGrade + "\",");
            shoplistjson.Append("\"FlavorGrade\":\"" + info.FlavorGrade + "\",");
            shoplistjson.Append("\"SpeedGrade\":\"" + info.SpeedGrade + "\",");
            shoplistjson.Append("\"userpic\":\"" +  ""  + "\",");
            shoplistjson.Append("\"Rcontent\":\"" + WebUtility.FileterJson(info.Rcontent) + "\",");
            shoplistjson.Append("\"Rtime\":\"" + info.Rtime + "\",");

            shoplistjson.Append("\"UserName\":\"" + info.UserName + "\"");
            shoplistjson.Append("},");
        }
        shoplistjson.Append("]}");

        Response.Write(shoplistjson.ToString().Replace(",]}", "]}"));
        Response.End();


    }
}
