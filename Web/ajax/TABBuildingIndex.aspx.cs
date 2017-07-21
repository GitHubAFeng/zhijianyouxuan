using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;
using Hangjing.Cache;

/// <summary>
/// 获取建筑物-只用在首页;
/// </summary>
public partial class TABBuildingIndex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        string type = Request["fuc"];


        string ret = "";

        switch (type)
        {
            case "getbuild"://所有都用个获取
                int flag = 0;
                if (Request["flag"] != null && Request["flag"].ToString() == "1")
                {
                    flag = 1;
                }
                string sid = "-1";
                if (Request["s"] != null && Request["s"].ToString() != "-1")
                {
                    sid = Request["s"];
                }
                string ch = "-1";
                if (Request["c"] != null && Request["c"].ToString() != "-1")
                {
                    ch = Request["c"];
                }
                string key = "-1";
                if (Request["key"] != null && Request["key"].ToString() != "-1")
                {
                    key = Server.UrlDecode(Request["key"]);
                }
                string sort = "-1";
                if (Request["sort"] != null && Request["sort"].ToString() != "-1")
                {
                    sort = Request["sort"];
                }
                int uc = 0;
                if (Request["uc"] != null && Request["uc"].ToString() != "0")
                {
                    uc = Convert.ToInt32(Request["uc"]);
                }
                int page = Convert.ToInt32(Request["page"]);
                ret = getbuild(flag, sid, ch, key, page, sort, uc);
                Response.Write(ret);
                break;
            default:
                Response.Write("-1");
                break;
        }


        Response.End();
    }

    EBuilding bll = new EBuilding();


    /// <summary>
    /// 所有列表都有些获取。int sectionid, string ch, string key 这几个参数为-1表是空
    /// sort表示分类，只是加在链接后台的参数 -1表示没有 uc 1表示选择区域 ， 0表示选择楼宇
    /// </summary>
    /// <param name="flag">1表示全部</param>
    /// <param name="sectionid">区域编号</param>
    /// <param name="ch">首字母</param>
    /// <param name="key">关键字</param>
    /// <returns></returns>
    protected string getbuild(int flag, string sectionid, string ch, string key, int page, string sort, int uc)
    {

        string s = sectionid;
        int currentpage = Convert.ToInt32(page);
        int pagesize = 30;
        int startindex = pagesize * (currentpage - 1);
        int endindex = pagesize * currentpage;
        int xl = 0;
        int pagecount = 0;


        DataTable dt = (DataTable)EasyEatCache.GetCacheService().RetrieveObject("/building");

        if (dt == null || !(dt.Rows.Count > 0))
        {
            dt = bll.GetBuildingTable();
            EasyEatCache.GetCacheService().AddObject("/building", dt);
        }
        string sql = "1=1 ";
        if (flag == 0)
        {
            if (sectionid != "-1")
            {
                sql += " and  sectionid=" + sectionid;
            }
            if (ch != "-1")
            {
                sql += " and  FirstL like '" + ch.Trim() + "'";//不区分大小写
            }
            if (key != "-1")
            {
                sql += " and Name like '%" + key + "%'";
            }
        }
        DataRow[] dr_list = dt.Select(sql, "type desc");

        pagecount = dr_list.Length / pagesize;
        int lastpage = dr_list.Length % pagesize;
        if (lastpage > 0)
        {
            pagecount = pagecount + 1;
        }

        StringBuilder sb = new StringBuilder("<span class=\"left_ico\"><a id=\"tab_pre_page\" href=\"javascript:preFix(" + flag + ",'" + s + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\">&nbsp;</a></span><div class=\"building_cen_con\" ><ul>");

        if (dr_list.Length > 0)
        {
            foreach (DataRow info in dr_list)
            {
                if (xl >= startindex && xl < endindex)
                {
                    string url = "shoplist.aspx?id=" + info["dataid"];
                    sb.Append("<li><a href=\"" + url + "\" title='" + info["name"] + "'>" + WebUtility.Left(info["name"], 6) + "</a></li>");
                }
                xl++;
            }
           
        }
        else
        {
            sb.Append("<div style='color:#fff'>您好，未搜索到相应标识建筑物。</div>");
        }
        sb.Append("<input type=\"hidden\" id=\"hfpagecount\" value=\"" + pagecount + "\" />");
        sb.Append("<input type=\"hidden\" id=\"hfcpage\" value=\"" + page + "\" />");
        sb.Append("</ul></div><span class=\"right_ico\"><a id=\"tab_next_page\" href=\"javascript:nextFix(" + flag + ",'" + s + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\">&nbsp;</a></span>");

        return sb.ToString();
    }
}
