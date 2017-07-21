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

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Cache;

/// <summary>
/// 建筑物餐馆列表
/// TODO:使用缓存
/// </summary>
public partial class AreaAdmin_TogoBuilding : AdminPageBase
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


        try
        {
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
                        uc =  Convert.ToInt32( Request["uc"]);
                    }
                    int page = Convert.ToInt32(Request["page"]);
                    ret = getbuild(flag , sid , ch , key , page , sort , uc);
                    Response.Write(ret);
                    break;
                default:
                    Response.Write("-1");
                    break;
            }
        }
        catch
        {
            Response.Write("");
        }

        Response.End();
    }

    EBuilding bll = new EBuilding();


    protected string getbuild(int flag, string sectionid, string ch, string key, int page, string sort, int uc)
    {
        string siteurl = WebUtility.GetConfigsite();
        StringBuilder sb = new StringBuilder();
        string s = sectionid;
        int currentpage = Convert.ToInt32(page);
        int pagesize = 27;
        int startindex = pagesize * (currentpage - 1);
        int endindex = pagesize * currentpage;
        int xl = 0;

        DataTable dt = new DataTable();
        dt = (DataTable)EasyEatCache.GetCacheService().RetrieveObject("/building_datatable");

        if (dt == null || !(dt.Rows.Count > 0))
        {
            //无缓存则加入缓存
            dt = bll.GetBuildingTable();
            EasyEatCache.GetCacheService().AddObject("/building_datatable", dt);
        }
        string sql = "1=1 ";
        if (sort != "-1")
        {
            sql += " and  cityid=" + sort;
        }
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
                    sb.Append("<div class=\"building_list\"></span>");
                    sb.Append("<input type='checkbox' value='" + info["dataid"] + "' bname='" + info["name"] + "' class='mychck'  onclick=\"mychange('" + info["dataid"] + "',this , '" + info["name"] + "')\"  />" + info["name"] + "</div>");
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
            sb.Append("<div style=\"text-align:center; clear:both;margin-top:5px; position:relatively\"><img src=\"" + siteurl + "/images/first_p.gif\" id=\"first_p\" style=\"cursor:pointer;\" onclick=\"firstFix(" + flag + ",'" + s + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "' , '" + uc + "')\" ></img> ");
            sb.Append("<img src=\"" + siteurl + "/images/pre_p.gif\" id=\"_pre\"  style=\"cursor:pointer;\" onclick=\"preFix(" + flag + ",'" + s + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\"></img>");
            handlerpageFix(pagecount, sb, currentpage, s, ch, key, sort, flag, uc);
            //下一页,最后一页图标 
            sb.Append("<img src=\"" + siteurl + "/images/next_p.gif\" id=\"_next\"  style=\"cursor:pointer;\" onclick=\"nextFix(" + flag + ",'" + s + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\" ></img>");
            sb.Append("<img src=\"" + siteurl + "/images/last_p.gif\" id=\"last_p\"  style=\"cursor:pointer;\" onclick=\"lastFix(" + flag + ",'" + s + "','" + ch + "','" + key + "','" + sort + "' , '" + pagecount + "', '" + uc + "')\" ></img> ");
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
    private int handlerpageFix(int pagecount, StringBuilder sb, int currentpage, string sectionid , string ch , string key ,string sort , int flag , int uc)
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


    /// <summary>
    /// 区域获取
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private string GetByS(string s)
    {
        StringBuilder sb = new StringBuilder();
        //select cache

        //EasyEatCache.ClearBuilding();
        DataTable dt = new DataTable();
        //dt = EasyEatCache.GetBuilding();

        //if (!(dt.Rows.Count > 0))
        {
            //无缓存则加入缓存
            dt = bll.GetBuildingTable();
            //EasyEatCache.SetBuilding(dt);

            //GlobalManage.AddSystemLog("ETempBuildinginfo no cache add cache", LogType.General, "System");
        }

        DataRow[] dr_list = dt.Select("sectionid=" + s + " or dataid = 2", "dataid desc");

        if (dr_list.Length > 0)
        {
            foreach (DataRow info in dr_list)
            {
                sb.Append("<div class=\"building_list\">");
                sb.Append("<a href=\"EBuilding.aspx?id=" + info["dataid"] + "\">" + info["name"] + "</a></div>");
            }
        }
        else
        {
            sb.Append("您好，未搜索到相应标识建筑物，请选择其他区域。");
        }

        return sb.ToString();

    }

    /// <summary>
    /// 区域获取 , 有分页
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private string GetByS(string s , string page)
    {
        int currentpage = Convert.ToInt32(page);
        int pagesize = 27;
        int startindex = pagesize * (currentpage - 1);
        int endindex = pagesize * currentpage;
        int xl = 0;
        StringBuilder sb = new StringBuilder();
        DataTable dt = new DataTable();
        //select cache
        //dt = EasyEatCache.GetBuilding();

        //if (!(dt.Rows.Count > 0))
        {
            //无缓存则加入缓存
            dt = bll.GetBuildingTable();
            //EasyEatCache.SetBuilding(dt);

            //GlobalManage.AddSystemLog("EBuildingInfo no cache add cache", LogType.General, "System");
        }
        DataRow[] dr_list = dt.Select("sectionid = " + s, "dataid desc");
        if (dr_list.Length > 0)
        {
            foreach (DataRow info in dr_list)
            {
                if (xl >= startindex && xl < endindex)
                {
                    sb.Append("<div class=\"building_list\">");
                    sb.Append("<a href=\"EBuilding.aspx?id=" + info["dataid"] + "\">" + info["name"] + "</a></div>");
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
            sb.Append("<div style=\"text-align:center; clear:both;margin-top:5px; position:relatively\"><img src=\"../images/first_p.gif\" id=\"first_p\" style=\"cursor:pointer;\" onclick=\"firstP('" + s + "')\" ></img> ");
            sb.Append("<img src=\"../images/pre_p.gif\" id=\"_pre\"  style=\"cursor:pointer;\" onclick=\"preP('" + s + "')\"></img>");
            handlerpageP(pagecount, sb, currentpage, s);
            //下一页,最后一页图标 
            sb.Append("<img src=\"../images/next_p.gif\" id=\"_next\"  style=\"cursor:pointer;\" onclick=\"nextP('" + s + "' , '" + pagecount + "')\" ></img>");
            sb.Append("<img src=\"../images/last_p.gif\" id=\"last_p\"  style=\"cursor:pointer;\" onclick=\"lastP('" + s + "' ,'" + pagecount + "')\" ></img> ");
            sb.Append("</div>");
        }
        else
        {
            sb.Append("您好，未搜索到相应标识建筑物，请选择其他区域。");
        }

        return sb.ToString();

    }
    //字母获取
    private string GetByC(string s, string c)
    {
        StringBuilder sb = new StringBuilder();

        //select cache
        DataTable dt = new DataTable();
        //dt = EasyEatCache.GetBuilding();

        //if (!(dt.Rows.Count > 0))
        {
            //无缓存则加入缓存
           // dt = bll.TempGetBuildingTable();
            //EasyEatCache.SetBuilding(dt);

            //GlobalManage.AddSystemLog("EBuildingInfo no cache add cache", LogType.General, "System");
        }

        DataRow[] dr_list = dt.Select("sectionid=" + s + " and firstl='" + c + "'", "dataid desc");

        if (dr_list.Length > 0)
        {
            foreach (DataRow info in dr_list)
            {
                sb.Append("<div class=\"building_list\">");
                sb.Append("<a href=\"EBuilding.aspx?id=" + info["dataid"] + "\">" + info["name"] + "</a></div>");
            }
        }
        else
        {
            sb.Append("您好，未搜索到相应标识建筑物，请选择其他区域。");
        }

        return sb.ToString();

    }

    /// <summary>
    /// 区域获取
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private string GetBySUH(string s)
    {
        StringBuilder sb = new StringBuilder();

        //select cache
        DataTable dt = new DataTable();
        //dt = EasyEatCache.GetBuilding();

        //if (!(dt.Rows.Count > 0))
        {
            //无缓存则加入缓存
            dt = bll.GetBuildingTable();
            //EasyEatCache.SetBuilding(dt);

            //GlobalManage.AddSystemLog("EBuildingInfo no cache add cache", LogType.General, "System");
        }

        DataRow[] dr_list = dt.Select("sectionid=" + s + "", "dataid desc");

        if (dr_list.Length > 0)
        {
            int xl = 0;
            foreach (DataRow info in dr_list)
            {
                xl++;
                sb.Append("<div class=\"building_list\"></span>");
                sb.Append("<a href=\"javascript:InitAddress('" + info["Name"] + "')\">" + info["name"] + "</a></div>");
                //if (xl == 24)
                //{
                //    break;
                //}
            }
            //int pagecount = dr_list.Length / 24;
            /////分页
            //if (pagecount > 2)
            //{
            //    sb.Append("<div style=\"text-align:center\"><img src=\"images/first_p.gif\" id=\"first_p\" onclick=\"first()\" ></img> ");
            //    sb.Append("<img src=\"images/pre_p.gif\" id=\"_pre\" onclick=\"pre()\"></img>");
            //    handlerpage(pagecount, sb);
            //    sb.Append("<img src=\"images/next_p.gif\" id=\"_next\" onclick=\"next()\" ></img>");
            //    sb.Append("<img src=\"images/last_p.gif\" id=\"last_p\" onclick=\"last()\" ></img> ");
            //    sb.Append("</div>");
            //}

        }
        else
        {
            sb.Append("您好，未搜索到相应标识建筑物，请选择其他区域。");
        }

        return sb.ToString();

    }

    /// <summary>
    /// 区域获取 , 并且有页数
    /// </summary>
    /// <param name="s"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    private string GetBySUH(string s, string page)
    {
        int currentpage = Convert.ToInt32(page);
        int pagesize = 27;
        int startindex = pagesize * (currentpage - 1);
        int endindex = pagesize * currentpage;
        int xl = 0; 
        StringBuilder sb = new StringBuilder();
        DataTable dt = new DataTable();
        //select cache
        //dt = EasyEatCache.GetBuilding();

        //if (!(dt.Rows.Count > 0))
        {
            //无缓存则加入缓存
            dt = bll.GetBuildingTable();
            //EasyEatCache.SetBuilding(dt);
        }

        if (dt != null)
        {
            DataRow[] dr_list = dt.Select("sectionid = " + s, "dataid desc");
            //dataid in (select top 24 dataid from ETempBuildingInfo where sectionid = " + s + " and  dataid not in (select top " + Convert.ToString(24 * (requestpage - 1)) + " dataid from ETempBuildingInfo where  sectionid = " + s + "))
            if (dr_list.Length > 0)
            {
                foreach (DataRow info in dr_list)
                {
                    if (xl >= startindex && xl < endindex)
                    {
                        sb.Append("<div class=\"building_list\"></span>");
                        sb.Append("<a href=\"javascript:InitAddress('" + info["Name"] + "','" + info["dataid"] + "')\">" + info["name"] + "</a></div>");
                    }
                    xl++;
                    if (xl > endindex)
                    {
                        break;
                    }
                }
                int pagecount = dr_list.Length / pagesize;
                int lastpage = dr_list.Length % pagesize;
                if (lastpage > 0)
                {
                    pagecount = pagecount + 1;
                }
                //第一页 , 上一页图标
                sb.Append("<div style=\"text-align:center; clear:both\"><img src=\"../images/first_p.gif\" id=\"first_p\"  style=\"cursor:pointer;\" onclick=\"first('" + s + "')\" ></img> ");
                sb.Append("<img src=\"../images/pre_p.gif\" id=\"_pre\"  style=\"cursor:pointer;\" onclick=\"pre('" + s + "')\"></img>");
                handlerpage(pagecount, sb, currentpage, s);
                //下一页,最后一页图标 
                sb.Append("<img src=\"../images/next_p.gif\" id=\"_next\"  style=\"cursor:pointer;\" onclick=\"next('" + s + "' , '" + pagecount + "')\" ></img>");
                sb.Append("<img src=\"../images/last_p.gif\" id=\"last_p\"  style=\"cursor:pointer;\" onclick=\"last('" + s + "' ,'" + pagecount + "')\" ></img> ");
                sb.Append("</div>");
            }
            else
            {
                sb.Append("您好，未搜索到相应标识建筑物，请选择其他区域。");
            }

            return sb.ToString();
        }
        else
        {
            return "";
        }
    }

    //字母获取
    private string GetByCUH(string s, string c)
    {
        StringBuilder sb = new StringBuilder();

        //select cache
        DataTable dt = new DataTable();
        //dt = EasyEatCache.GetBuilding();

        //if (!(dt.Rows.Count > 0))
        {
            //无缓存则加入缓存
          //  dt = bll.TempGetBuildingTable();
            //EasyEatCache.SetBuilding(dt);

            //GlobalManage.AddSystemLog("EBuildingInfo no cache add cache", LogType.General, "System");
        }

        DataRow[] dr_list = dt.Select("sectionid=" + s + " and firstl='" + c + "'", "dataid desc");

        if (dr_list.Length > 0)
        {
            foreach (DataRow info in dr_list)
            {
                sb.Append("<div class=\"building_list\">");
                sb.Append("<a href=\"javascript:InitAddress('" + info["Name"] + "')\">" + info["name"] + "</a></div>");
            }
        }

        else
        {
            sb.Append("您好，未搜索到相应标识建筑物，请选择其他区域。");
        }

        return sb.ToString();
    }

    //字母获取 , 有分页
    private string GetByCUH(string s, string c , string page)
    {
        int currentpage = Convert.ToInt32(page);
        int pagesize = 24;
        int startindex = pagesize * (currentpage - 1);
        int endindex = pagesize * currentpage;
        int xl = 0; 
        StringBuilder sb = new StringBuilder();

        //select cache
        DataTable dt = new DataTable();
        //dt = EasyEatCache.GetBuilding();

        //if (!(dt.Rows.Count > 0))
        {
            //无缓存则加入缓存
          //  dt = bll.TempGetBuildingTable();
            //EasyEatCache.SetBuilding(dt);

            //GlobalManage.AddSystemLog("EBuildingInfo no cache add cache", LogType.General, "System");
        }
        DataRow[] dr_list = dt.Select("sectionid=" + s + " and firstl='" + c + "'", "dataid desc");
        if (dr_list.Length > 0)
        {
            foreach (DataRow info in dr_list)
            {
                if (xl > startindex && xl < endindex)
                {
                    sb.Append("<div class=\"building_list\"></span>");
                    sb.Append("<a href=\"javascript:InitAddress('" + info["Name"] + "')\">" + info["name"] + "</a></div>");
                }
                xl++;
            }
            int pagecount = dr_list.Length / pagesize;
            //第一页 , 上一页图标
            sb.Append("<div style=\"text-align:center; clear:both\"><img src=\"../images/first_p.gif\"  style=\"cursor:pointer;\" id=\"first_p\" onclick=\"first()\" ></img> ");
            sb.Append("<img src=\"../images/pre_p.gif\" id=\"_pre\"  style=\"cursor:pointer;\" onclick=\"pre()\"></img>");
            handlerpage(pagecount, sb, currentpage, s);
            //下一页,最后一页图标 
            sb.Append("<img src=\"../images/next_p.gif\" id=\"_next\"  style=\"cursor:pointer;\" onclick=\"next()\" ></img>");
            sb.Append("<img src=\"../images/last_p.gif\" id=\"last_p\"  style=\"cursor:pointer;\" onclick=\"last()\" ></img> ");
            sb.Append("</div>");
        }
        else
        {
            sb.Append("您好，未搜索到相应标识建筑物，请选择其他区域。");
        }

        return sb.ToString();
    }

    /// <summary>
    /// 处理显示的页码
    /// </summary>
    /// <param name="pagecount"></param>
    /// <param name="sb"></param>
    /// <returns></returns>
    private int handlerpage(int pagecount, StringBuilder sb)
    {
        if (pagecount <= 10)
        {
            sb.Append("<strong> <span style=\"cursor: pointer;\" onclick=\"showpage('"+ 1 +"')\"> " + 1 + " </span></strong>");
            sb.Append("<span>&nbsp;|&nbsp;</span>");
            for (int x = 2; x <= pagecount ; x++)
            {
                sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + x + "')\" > " + x + " </span>");
                sb.Append("<span>&nbsp;|&nbsp;</span>");
            }
            sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + Convert.ToInt32(pagecount + 1) + "')\"> " + Convert.ToInt32(pagecount + 1) + " </span>");
        }
        else//有点难度.
        {
            sb.Append("<strong> <span style=\"cursor: pointer;\" onclick=\"showpage('" + 1 + "')\"> " + 1 + " </span></strong>");
            sb.Append("<span>&nbsp;|&nbsp;</span>");
            for (int x = 2; x <= 10; x++)
            {
                sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + x + "')\" > " + x + " </span>");
                sb.Append("<span>&nbsp;|&nbsp;</span>");
            }
            sb.Append(". . .");
            sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + Convert.ToInt32(pagecount + 1) + "')\"> " + Convert.ToInt32(pagecount + 1) + " </span>");
        }
        return 0;
    }

    /// <summary>
    /// 处理显示的页码 , 用户中心
    /// </summary>
    /// <param name="pagecount"></param>
    /// <param name="sb"></param>
    /// <returns></returns>
    private int handlerpage(int pagecount, StringBuilder sb , int currentpage , string sectionid)
    {
        if (pagecount <= 10)
        {
            for (int x = 1; x < pagecount; x++)
            {
                if (x == currentpage)
                {
                    sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpage('" + x + "' , '"+sectionid+"')\"> " + x + " </span></strong>");
                    sb.Append("<span>&nbsp;|&nbsp;</span>");
                    continue;
                }
                sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + x + "' , '" + sectionid + "')\" > " + x + " </span>");
                sb.Append("<span>&nbsp;|&nbsp;</span>");
            }
            if (currentpage == pagecount )
            {
                sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpage('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span></strong>");
            }
            else
            {
                sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + Convert.ToInt32(pagecount ) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span>");
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
                        sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpage('" + x + "' , '" + sectionid + "')\"> " + x + " </span></strong>");
                        sb.Append("<span>&nbsp;|&nbsp;</span>");
                    }
                    else
                    {
                        sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + x + "' , '" + sectionid + "')\"> " + x + " </span>");
                        sb.Append("<span>&nbsp;|&nbsp;</span>");
                    }
                }
                sb.Append(". . .");
                sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span>");    
            }
            else
            {
                if (currentpage <= pagecount - 5 + 1+1 )
                {
                    sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + 1 + "' , '" + sectionid + "')\" > " + 1 + " </span>");
                    sb.Append("<span>&nbsp;|&nbsp;</span>");
                    sb.Append(". . .");
                    for (int x = currentpage - 4 ; x < (currentpage - 4 + 1  + 9) && x < pagecount; x++)
                    {
                        if (x == currentpage)
                        {
                            sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpage('" + x + "' , '" + sectionid + "')\"> " + x + " </span></strong>");
                            sb.Append("<span>&nbsp;|&nbsp;</span>");
                        }
                        else
                        {
                            sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + x + "' , '" + sectionid + "')\"> " + x + " </span>");
                            sb.Append("<span>&nbsp;|&nbsp;</span>");
                        }
                    }
                    sb.Append(". . .");
                    sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span>");
                }
                else
                {
                    sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + 1 + "' , '" + sectionid + "')\" > " + 1 + " </span>");
                    sb.Append("<span>&nbsp;|&nbsp;</span>");
                    sb.Append(". . .");
                    for (int x = currentpage - 9; x < pagecount; x++)
                    {
                        if (x == currentpage)
                        {
                            sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpage('" + x + "' , '" + sectionid + "')\"> " + x + " </span></strong>");
                            sb.Append("<span>&nbsp;|&nbsp;</span>");
                        }
                        else
                        {
                            sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + x + "' , '" + sectionid + "')\" > " + x + " </span>");
                            sb.Append("<span>&nbsp;|&nbsp;</span>");
                        }
                    }
                    if (currentpage == pagecount)
                    {
                        sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpage('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span></strong>");
                    }
                    else
                    {
                        sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span>");
                    }
                }
            }   
        }

        return 0;
    }

    /// <summary>
    /// 搜索时选择写字楼
    /// </summary>
    /// <param name="s"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    protected string Getbyss(string s, string page , string ids)
    {
        StringBuilder sb = new StringBuilder();

        //select cache
        DataTable dt = new DataTable();
       // dt = EasyEatCache.GetBuilding();

        //if (!(dt.Rows.Count > 0))
        {
            //无缓存则加入缓存
            dt = bll.GetBuildingTable();
            //EasyEatCache.SetBuilding(dt);

            //GlobalManage.AddSystemLog("EBuildingInfo no cache add cache", LogType.General, "System");
        }

        DataRow[] dr_list = dt.Select("sectionid=" + s + " and  dataid in ("+ids+") or dataid=2", "dataid desc");

        if (dr_list.Length > 0)
        {
            foreach (DataRow info in dr_list)
            {
                sb.Append("<div class=\"building_list\">");
                sb.Append("<a href=\"javascript:InitAddress('" + info["dataid"] + "')\">" + info["name"] + "</a></div>");
            }
        }

        else
        {
            sb.Append("您好，未搜索到相应标识建筑物，请选择其他区域。");
        }

        return sb.ToString();
    }

    /// <summary>
    /// 处理显示的页码 , 首页，订单页面
    /// </summary>
    /// <param name="pagecount"></param>
    /// <param name="sb"></param>
    /// <returns></returns>
    private int handlerpageP(int pagecount, StringBuilder sb, int currentpage, string sectionid)
    {
        if (pagecount <= 10)
        {
            for (int x = 1; x < pagecount; x++)
            {
                if (x == currentpage)
                {
                    sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpageP('" + x + "' , '" + sectionid + "')\"> " + x + " </span></strong>");
                    sb.Append("<span>&nbsp;|&nbsp;</span>");
                    continue;
                }
                sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageP('" + x + "' , '" + sectionid + "')\" > " + x + " </span>");
                sb.Append("<span>&nbsp;|&nbsp;</span>");
            }
            if (currentpage == pagecount)
            {
                sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpageP('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span></strong>");
            }
            else
            {
                sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageP('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span>");
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
                        sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpageP('" + x + "' , '" + sectionid + "')\"> " + x + " </span></strong>");
                        sb.Append("<span>&nbsp;|&nbsp;</span>");
                    }
                    else
                    {
                        sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageP('" + x + "' , '" + sectionid + "')\"> " + x + " </span>");
                        sb.Append("<span>&nbsp;|&nbsp;</span>");
                    }
                }
                sb.Append(". . .");
                sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageP('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span>");
            }
            else
            {
                if (currentpage <= pagecount - 5 + 1 + 1)
                {
                    sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageP('" + 1 + "' , '" + sectionid + "')\" > " + 1 + " </span>");
                    sb.Append("<span>&nbsp;|&nbsp;</span>");
                    sb.Append(". . .");
                    for (int x = currentpage - 4; x < (currentpage - 4 + 1 + 9) && x < pagecount; x++)
                    {
                        if (x == currentpage)
                        {
                            sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpageP('" + x + "' , '" + sectionid + "')\"> " + x + " </span></strong>");
                            sb.Append("<span>&nbsp;|&nbsp;</span>");
                        }
                        else
                        {
                            sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageP('" + x + "' , '" + sectionid + "')\"> " + x + " </span>");
                            sb.Append("<span>&nbsp;|&nbsp;</span>");
                        }
                    }
                    sb.Append(". . .");
                    sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageP('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span>");
                }
                else
                {
                    sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageP('" + 1 + "' , '" + sectionid + "')\" > " + 1 + " </span>");
                    sb.Append("<span>&nbsp;|&nbsp;</span>");
                    sb.Append(". . .");
                    for (int x = currentpage - 9; x < pagecount; x++)
                    {
                        if (x == currentpage)
                        {
                            sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpageP('" + x + "' , '" + sectionid + "')\"> " + x + " </span></strong>");
                            sb.Append("<span>&nbsp;|&nbsp;</span>");
                        }
                        else
                        {
                            sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageP('" + x + "' , '" + sectionid + "')\" > " + x + " </span>");
                            sb.Append("<span>&nbsp;|&nbsp;</span>");
                        }
                    }
                    if (currentpage == pagecount)
                    {
                        sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpageP('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span></strong>");
                    }
                    else
                    {
                        sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageP('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span>");
                    }
                }
            }
        }

        return 0;
    }
}
