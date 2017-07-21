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
/// 建筑物餐馆列表
/// TODO:使用缓存
/// </summary>
public partial class ajax_TogoBuilding : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        //fuc 是执行类型 
        //         类型代码          参数
        //getbys  根据区域获取      区域编号s
        //getbyl  根据字母获取      区域编号s 字母c
        //getbys_uh  用户中心根据区域获取      区域编号s
        //getbyl_uh  用户中心根据字母获取      区域编号s 字母c
        // page   用户中心请求的页数

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
        StringBuilder sb = new StringBuilder();
        string s = sectionid;
        int currentpage = Convert.ToInt32(page);
        int pagesize = 27;
        int startindex = pagesize * (currentpage - 1);
        int endindex = pagesize * currentpage;
        int xl = 0;

        DataTable dt = new DataTable();
        dt = (DataTable)EasyEatCache.GetCacheService().RetrieveObject("/building");

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
        if (dr_list.Length > 0)
        {
            foreach (DataRow info in dr_list)
            {
                if (xl >= startindex && xl < endindex)
                {
                    if (uc > 0)//表示选择区域
                    {
                        sb.Append("<div class=\"building_list\">");

                        string url = "shoplist.aspx?id=" + info["dataid"];
                        if (sort != "-1")
                        {
                            url += "&sortid=" + sort;
                        }
                        if (uc == 2)
                        {
                            url = "&a=1";
                        }
                        sb.Append("<a href=\"" + url + "\">" + info["name"] + "</a></div>");
                    }
                    else//表示下订单，用户中心选择地址
                    {
                        sb.Append("<div class=\"building_list\"></span>");
                        sb.Append("<a href=\"javascript:InitAddress('" + info["Name"] + "' , " + info["dataid"] + ")\">" + info["name"] + "</a></div>");
                       

                    }

                }
                xl++;
            }
            int pagecount = dr_list.Length / pagesize;
            int lastpage = dr_list.Length % pagesize;
            if (lastpage > 0)
            {
                pagecount = pagecount + 1;
            }
            sb.Append("<div style=\"clear:both\"></div>");
            //第一页 , 上一页图标
            sb.Append("<div style=\"text-align:center; clear:both;margin-top:5px; position:relatively\"><img src=\"images/first_p.gif\" id=\"first_p\" style=\"cursor:pointer;\" onclick=\"firstFix(" + flag + ",'" + s + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "' , '" + uc + "')\" ></img> ");
            sb.Append("<img src=\"images/pre_p.gif\" id=\"_pre\"  style=\"cursor:pointer;\" onclick=\"preFix(" + flag + ",'" + s + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\"></img>");
            handlerpageFix(pagecount, sb, currentpage, s, ch, key, sort, flag, uc);
            //下一页,最后一页图标 
            sb.Append("<img src=\"images/next_p.gif\" id=\"_next\"  style=\"cursor:pointer;\" onclick=\"nextFix(" + flag + ",'" + s + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\" ></img>");
            sb.Append("<img src=\"images/last_p.gif\" id=\"last_p\"  style=\"cursor:pointer;\" onclick=\"lastFix(" + flag + ",'" + s + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\" ></img> ");
            sb.Append("</div>");
        }
        else
        {
            sb.Append("您好，未搜索到相应标识建筑物。");
        }

        return sb.ToString();
    }


    /// <summary>
    /// 处理显示的页码 , 首页，订单页面
    /// </summary>
    /// <param name="pagecount"></param>
    /// <param name="sb"></param>
    /// <returns></returns>
    private int handlerpageFix(int pagecount, StringBuilder sb, int currentpage, string sectionid, string ch, string key, string sort, int flag, int uc)
    {
        if (pagecount <= 10)
        {
            for (int x = 1; x < pagecount; x++)
            {
                if (x == currentpage)
                {
                    sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpageFix('" + x + "' , " + flag + ",'" + sectionid + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\"> " + x + " </span></strong>");
                    sb.Append("<span>&nbsp;|&nbsp;</span>");
                    continue;
                }
                sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageFix('" + x + "' , " + flag + ",'" + sectionid + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\" > " + x + " </span>");
                sb.Append("<span>&nbsp;|&nbsp;</span>");
            }
            if (currentpage == pagecount)
            {
                sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpageFix('" + Convert.ToInt32(pagecount) + "', " + flag + ",'" + sectionid + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\"> " + Convert.ToInt32(pagecount) + " </span></strong>");
            }
            else
            {
                sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageFix('" + Convert.ToInt32(pagecount) + "', " + flag + ",'" + sectionid + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\"> " + Convert.ToInt32(pagecount) + " </span>");
            }
        }
        else
        {
            if (currentpage <= 5)
            {
                for (int x = 1; x <= 10; x++)
                {
                    if (x == currentpage)
                    {
                        sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpageFix('" + x + "' , " + flag + ",'" + sectionid + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\"> " + x + " </span></strong>");
                        sb.Append("<span>&nbsp;|&nbsp;</span>");
                    }
                    else
                    {
                        sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageFix('" + x + "' , " + flag + ",'" + sectionid + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\"> " + x + " </span>");
                        sb.Append("<span>&nbsp;|&nbsp;</span>");
                    }
                }
                sb.Append(". . .");
                sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageFix('" + Convert.ToInt32(pagecount) + "', " + flag + ",'" + sectionid + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\"> " + Convert.ToInt32(pagecount) + " </span>");
            }
            else
            {
                if (currentpage <= pagecount - 5 + 1 + 1)
                {
                    sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageFix('" + 1 + "' , " + flag + ",'" + sectionid + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\" > " + 1 + " </span>");
                    sb.Append("<span>&nbsp;|&nbsp;</span>");
                    sb.Append(". . .");
                    for (int x = currentpage - 4; x < (currentpage - 4 + 1 + 9) && x < pagecount; x++)
                    {
                        if (x == currentpage)
                        {
                            sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpageFix('" + x + "' , " + flag + ",'" + sectionid + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\"> " + x + " </span></strong>");
                            sb.Append("<span>&nbsp;|&nbsp;</span>");
                        }
                        else
                        {
                            sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageFix('" + x + "' , " + flag + ",'" + sectionid + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\"> " + x + " </span>");
                            sb.Append("<span>&nbsp;|&nbsp;</span>");
                        }
                    }
                    sb.Append(". . .");
                    sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageFix('" + Convert.ToInt32(pagecount) + "', " + flag + ",'" + sectionid + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\"> " + Convert.ToInt32(pagecount) + " </span>");
                }
                else
                {
                    sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageFix('" + 1 + "' , " + flag + ",'" + sectionid + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\" > " + 1 + " </span>");
                    sb.Append("<span>&nbsp;|&nbsp;</span>");
                    sb.Append(". . .");
                    for (int x = currentpage - 9; x < pagecount; x++)
                    {
                        if (x == currentpage)
                        {
                            sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpageFix('" + x + "' , " + flag + ",'" + sectionid + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\"> " + x + " </span></strong>");
                            sb.Append("<span>&nbsp;|&nbsp;</span>");
                        }
                        else
                        {
                            sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageFix('" + x + "', " + flag + ",'" + sectionid + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\" > " + x + " </span>");
                            sb.Append("<span>&nbsp;|&nbsp;</span>");
                        }
                    }
                    if (currentpage == pagecount)
                    {
                        sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpageFix('" + Convert.ToInt32(pagecount) + "', " + flag + ",'" + sectionid + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\"> " + Convert.ToInt32(pagecount) + " </span></strong>");
                    }
                    else
                    {
                        sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageFix('" + Convert.ToInt32(pagecount) + "', " + flag + ",'" + sectionid + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\"> " + Convert.ToInt32(pagecount) + " </span>");
                    }
                }
            }
        }

        return 0;
    }
}
