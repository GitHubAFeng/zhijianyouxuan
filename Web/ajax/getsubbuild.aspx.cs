#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-6-11 15:12:41.
* E-Mail: jijunjian@ihangjing.com
*********************************************** */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;
using System.Data;

public partial class ajax_getsubbuild : System.Web.UI.Page
//{
//    EBuilding dal = new EBuilding();
//    /*
//        select * from areas where AID like '_00000000000' 

//        select * from areas where AID like '___000000000' and AID not like '_00000000000' 

//        select * from areas where AID like '______000000' and AID not like '_00000000000' and aid not like '___000000000'

//        select * from areas where AID like '__________00' and adi not  like '______000000' and AID not like '_00000000000' and aid not like '___000000000'

//        select * from areas where AID not  like '__________00'  
//     */
//    // 1位/2位/3位/4位/2位 12位
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        Response.Clear();

//        string bid = Request["bid"];

//        string level = Request["level"];//级数
//        string sql = "";

//        //获取某一级的标识建筑物列表 最长的为4位数
//        switch (level)
//        {
//            case "1":
//                sql = " AID like '_00000000000' and inuse like '%Y%'"; break;
//            case "2":
//                sql = "AID like '" + bid.Substring(0, 1) + "__000000000' and AID not like '_00000000000' and inuse like '%Y%'"; break;
//            case "3":
//                sql = "AID like '" + bid.Substring(0, 3) + "___000000' and AID not like '___000000000' and inuse like '%Y%'"; break;
//            case "4":
//                sql = "AID like '" + bid.Substring(0, 6) + "____00' and AID not  like '______000000' and inuse like '%Y%'"; break;
//            case "5":
//                sql = "AID like '" + bid.Substring(0, 10) + "__' and AID not like '__________00' and inuse like '%Y%'"; break;
//            default:
//                Response.Write(1);
//                Response.End();
//                sql = ""; break;
//        };

//        IList<BuildingInfo> list = new List<BuildingInfo>();
//        list = dal.GetList(9999, 1, "SectionID=" + bid + "", "DataID", 1);
//        //if (Request["order"] == null)
//        //{
//            Response.Write(buildstr(list, level));
//        //}
//        //else
//        //{
//        //    Response.Write(buildstr_order(list, level));
//      //  }
//        Response.End();
//    }

//    /// <summary>
//    /// 生成html
//    /// </summary>
//    /// <param name="list"></param>
//    /// <param name="level"></param>
//    /// <returns></returns>
//    protected string buildstr(IList<BuildingInfo> list, string level)
//    {
//        StringBuilder sb = new StringBuilder();
//        int index = 0;
//        switch (level)
//        { 
//            case "1":
//                sb.Append("<ul>");

//                foreach (BuildingInfo m in list)
//                {
//                    string temp = index == 0 ? "hover" :"";
//                    sb.Append("<li class=\"sbuild " + temp + "\" id=\"build_" + m.AID + "\"><a href=\"javascript:getbuild(" + m.AID + "," + (Convert.ToInt32(level) + 1) + ",'divsecond')\">" + parsename(m.AName, level) + "</a></li>");
//                    index++;
//                }
//                sb.Append("</ul>");
//                break;
//            case "2":
//                sb.Append("<ul style=\" margin-top:10px;  clear:both\">");
//                foreach (BuildingInfo m in list)
//                {
//                    sb.Append("<li class=\"sbuild1 \" id=\"build_" + m.AID + "\"><a href=\"javascript:getbuild(" + m.AID + "," + (Convert.ToInt32(level) + 1) + ",'div_section')\">" + parsename(m.AName, level) + "</a></li>");
//                    index++;
//                }
//                sb.Append("</ul>");
//                break;
//            case "3":
//                string bid = Request["bid"];
//                string build = GetLeve3(bid, level, 1+"");
//                sb.Append(build);
//                break;
//        }
//        return sb.ToString();
//    }

//    /// <summary>
//    /// 解析名称
//    /// </summary>
//    /// <param name="oldname"></param>
//    /// <param name="level"></param>
//    /// <returns></returns>
//    protected string parsename(string oldname , string level)
//    {
//        string temp = oldname.Substring(1);
//        string[] names = temp.Split('/');

//        return names[Convert.ToInt32(level) -1];
//    }

//    /// <summary>
//    /// 处理显示的页码 , 首页，订单页面
//    /// </summary>
//    /// <param name="pagecount"></param>
//    /// <param name="sb"></param>
//    /// <returns></returns>
//    private int handlerpageP(int pagecount, StringBuilder sb, int currentpage, string sectionid)
//    {
//        if (pagecount <= 10)
//        {
//            for (int x = 1; x < pagecount; x++)
//            {
//                if (x == currentpage)
//                {
//                    sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpage('" + x + "' , '" + sectionid + "')\"> " + x + " </span></strong>");
//                    sb.Append("<span>&nbsp;|&nbsp;</span>");
//                    continue;
//                }
//                sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + x + "' , '" + sectionid + "')\" > " + x + " </span>");
//                sb.Append("<span>&nbsp;|&nbsp;</span>");
//            }
//            if (currentpage == pagecount)
//            {
//                sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpage('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span></strong>");
//            }
//            else
//            {
//                sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span>");
//            }
//        }
//        else
//        {
//            if (currentpage <= 5)
//            {
//                for (int x = 1; x <= 10; x++)
//                {
//                    if (x == currentpage)
//                    {
//                        sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpage('" + x + "' , '" + sectionid + "')\"> " + x + " </span></strong>");
//                        sb.Append("<span>&nbsp;|&nbsp;</span>");
//                    }
//                    else
//                    {
//                        sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + x + "' , '" + sectionid + "')\"> " + x + " </span>");
//                        sb.Append("<span>&nbsp;|&nbsp;</span>");
//                    }
//                }
//                sb.Append(". . .");
//                sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span>");
//            }
//            else
//            {
//                if (currentpage <= pagecount - 5 + 1 + 1)
//                {
//                    sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + 1 + "' , '" + sectionid + "')\" > " + 1 + " </span>");
//                    sb.Append("<span>&nbsp;|&nbsp;</span>");
//                    sb.Append(". . .");
//                    for (int x = currentpage - 4; x < (currentpage - 4 + 1 + 9) && x < pagecount; x++)
//                    {
//                        if (x == currentpage)
//                        {
//                            sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpage('" + x + "' , '" + sectionid + "')\"> " + x + " </span></strong>");
//                            sb.Append("<span>&nbsp;|&nbsp;</span>");
//                        }
//                        else
//                        {
//                            sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpage('" + x + "' , '" + sectionid + "')\"> " + x + " </span>");
//                            sb.Append("<span>&nbsp;|&nbsp;</span>");
//                        }
//                    }
//                    sb.Append(". . .");
//                    sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageP('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span>");
//                }
//                else
//                {
//                    sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageP('" + 1 + "' , '" + sectionid + "')\" > " + 1 + " </span>");
//                    sb.Append("<span>&nbsp;|&nbsp;</span>");
//                    sb.Append(". . .");
//                    for (int x = currentpage - 9; x < pagecount; x++)
//                    {
//                        if (x == currentpage)
//                        {
//                            sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpageP('" + x + "' , '" + sectionid + "')\"> " + x + " </span></strong>");
//                            sb.Append("<span>&nbsp;|&nbsp;</span>");
//                        }
//                        else
//                        {
//                            sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageP('" + x + "' , '" + sectionid + "')\" > " + x + " </span>");
//                            sb.Append("<span>&nbsp;|&nbsp;</span>");
//                        }
//                    }
//                    if (currentpage == pagecount)
//                    {
//                        sb.Append("<strong> <span style=\"cursor: pointer;\" id=\"myfocus\" onclick=\"showpageP('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span></strong>");
//                    }
//                    else
//                    {
//                        sb.Append("<span style=\"cursor: pointer;\" onclick=\"showpageP('" + Convert.ToInt32(pagecount) + "', '" + sectionid + "')\"> " + Convert.ToInt32(pagecount) + " </span>");
//                    }
//                }
//            }
//        }

//        return 0;
//    }

//    /// <summary>
//    /// 区域获取 , 有分页
//    /// </summary>
//    /// <param name="s"></param>
//    /// <returns></returns>
//    private string GetLeve3(string bid, string level , string page)
//    {
//        int currentpage = Convert.ToInt32(page);
//        int pagesize = 27;
//        int startindex = pagesize * (currentpage - 1);
//        int endindex = pagesize * currentpage;
//        int xl = 0;
//        StringBuilder sb = new StringBuilder();
//        string sql = "AID like '" + bid.Substring(0, 3) + "___000000' and AID not like '___000000000' and inuse like '%Y%'";
//        IList<BuildingInfo> list = new List<BuildingInfo>();
//        list = dal.GetList(pagesize, Convert.ToInt32(page), sql, "Unid", 1);

//        if (list.Count > 0)
//        {
//            foreach (AreasInfo info in list)
//            {
//                if (xl >= startindex && xl < endindex)
//                {
//                    sb.Append("<div class=\"building_list\" id>");
//                    sb.Append("<a href=\"shoplist.aspx?id=" + info.Unid + "\">" + parsename(info.AName, level) + "</a></div>");
//                }
//                xl++;
//            }
//            int pagecount = list.Count / pagesize;
//            int lastpage = list.Count % pagesize;
//            if (lastpage > 0)
//            {
//                pagecount = pagecount + 1;
//            }
//            sb.Append("<div style=\"clear:both\"></div>");
//            //第一页 , 上一页图标
//            sb.Append("<div style=\"text-align:center; clear:both;margin-top:5px; position:relatively\"><img src=\"images/first_p.gif\" id=\"first_p\" style=\"cursor:pointer;\" onclick=\"first('" + bid + "')\" ></img> ");
//            sb.Append("<img src=\"images/pre_p.gif\" id=\"_pre\"  style=\"cursor:pointer;\" onclick=\"pre('" + bid + "')\"></img>");
//            handlerpageP(pagecount, sb, currentpage, bid);
//            //下一页,最后一页图标 
//            sb.Append("<img src=\"images/next_p.gif\" id=\"_next\"  style=\"cursor:pointer;\" onclick=\"next('" + bid + "' , '" + pagecount + "')\" ></img>");
//            sb.Append("<img src=\"images/last_p.gif\" id=\"last_p\"  style=\"cursor:pointer;\" onclick=\"last('" + bid + "' ,'" + pagecount + "')\" ></img> ");
//            sb.Append("</div>");
//        }
//        else
//        {
//            sb.Append("您好，未搜索到相应标识建筑物，<a href='applybuilding.aspx' style='color:green' class='j_hide'>现在推荐</a>吧。");
//        }

//        return sb.ToString();

//    }

//    /// <summary>
//    /// 区域获取 , 有分页
//    /// </summary>
//    /// <param name="s"></param>
//    /// <returns></returns>
//    private string GetLeve3_order(string bid, string level, string page)
//    {
//        int currentpage = Convert.ToInt32(page);
//        int pagesize = 27;
//        int startindex = pagesize * (currentpage - 1);
//        int endindex = pagesize * currentpage;
//        int xl = 0;
//        StringBuilder sb = new StringBuilder();
//        string sql = "AID like '" + bid.Substring(0, 3) + "___000000' and AID not like '___000000000' and inuse like '%Y%'";
//        IList<BuildingInfo> list = new List<BuildingInfo>();
//        list = dal.GetList(pagesize, Convert.ToInt32(page), sql, "Unid", 1);

//        if (list.Count > 0)
//        {
//            foreach (AreasInfo info in list)
//            {
//                if (xl >= startindex && xl < endindex)
//                {
//                    sb.Append("<div class=\"building_list\">");
//                    sb.Append("<a href=\"#\" onclick=\"get4build('" + parsename(info.AName, level) + "' , " + info.Unid + " , '"+info.AID+"');return false;\" >" + parsename(info.AName, level) + "</a></div>");
//                }
//                xl++;
//            }
//            int pagecount = list.Count / pagesize;
//            int lastpage = list.Count % pagesize;
//            if (lastpage > 0)
//            {
//                pagecount = pagecount + 1;
//            }
//            sb.Append("<div style=\"clear:both\"></div>");
//            //第一页 , 上一页图标
//            sb.Append("<div style=\"text-align:center; clear:both;margin-top:5px; position:relatively\"><img src=\"images/first_p.gif\" id=\"first_p\" style=\"cursor:pointer;\" onclick=\"first('" + bid + "')\" ></img> ");
//            sb.Append("<img src=\"images/pre_p.gif\" id=\"_pre\"  style=\"cursor:pointer;\" onclick=\"pre('" + bid + "')\"></img>");
//            handlerpageP(pagecount, sb, currentpage, bid);
//            //下一页,最后一页图标 
//            sb.Append("<img src=\"images/next_p.gif\" id=\"_next\"  style=\"cursor:pointer;\" onclick=\"next('" + bid + "' , '" + pagecount + "')\" ></img>");
//            sb.Append("<img src=\"images/last_p.gif\" id=\"last_p\"  style=\"cursor:pointer;\" onclick=\"last('" + bid + "' ,'" + pagecount + "')\" ></img> ");
//            sb.Append("</div>");
//        }
//        else
//        {
//            sb.Append("您好，未搜索到相应标识建筑物，<a href='applybuilding.aspx' style='color:green' class='j_hide'>现在推荐</a>吧。");
//        }

//        return sb.ToString();

//    }

//    /// <summary>
//    /// 生成html
//    /// </summary>
//    /// <param name="list"></param>
//    /// <param name="level"></param>
//    /// <returns></returns>
//    protected string buildstr_order(IList<BuildingInfo> list, string level)
//    {
//        StringBuilder sb = new StringBuilder();
//        int index = 0;
//        switch (level)
//        {
//            case "1":
//                sb.Append("<ul>");

//                foreach (BuildingInfo m in list)
//                {
//                    string temp = index == 0 ? "hover" : "";
//                    sb.Append("<li class=\"sbuild " + temp + "\" id=\"build_" + m.AID + "\"><a href=\"javascript:getbuild_SHOP(" + m.AID + "," + (Convert.ToInt32(level) + 1) + ",'divsecond')\">" + parsename(m.AName, level) + "</a></li>");
//                    index++;
//                }
//                sb.Append("</ul>");
//                break;
//            case "2":
//                sb.Append("<ul style=\" margin-top:10px;  clear:both\">");
//                foreach (BuildingInfo m in list)
//                {
//                    sb.Append("<li class=\"sbuild1 \" id=\"build_" + m.AID + "\"><a href=\"javascript:getbuild_SHOP(" + m.AID + "," + (Convert.ToInt32(level) + 1) + ",'div_section')\">" + parsename(m.AName, level) + "</a></li>");
//                    index++;
//                }
//                sb.Append("</ul>");
//                break;
//            case "3":
//                string bid = Request["bid"];
//                string build = GetLeve3_order(bid, level, 1 + "");
//                sb.Append(build);
//                break;
//        }
//        return sb.ToString();
//    }
//}
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
        }
        catch
        {
            Response.Write("");
        }

        Response.End();
    }

    EBuilding bll = new EBuilding();

    ///// <summary>
    ///// 点击商家时选择楼宇
    ///// </summary>
    ///// <param name="s"></param>
    ///// <returns></returns>
    //private string SelectBuild(int togoid)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    //select cache
    //    TogoInfo model = new Custorder().GetModelByDataId(togoid);
    //    string build = model.EBuilding;
    //    if (WebUtility.dellast(build) != "")
    //    {
    //        string sql = "dataid in (" + WebUtility.dellast(build) + ")";
    //        IList<BuildingInfo> dr_list = bll.GetList(24, 1, sql, "type", 1);

    //        if (dr_list.Count > 0)
    //        {
    //            int xl = 0;
    //            foreach (BuildingInfo info in dr_list)
    //            {
    //                xl++;
    //                sb.Append("<div class=\"building_list\"></span>");
    //            sb.Append("<a href=\"javascript:InitAddress1('" + info.Name + "' , '" + info.DataID + "','" + togoid + "')\">" + info.Name + "</a></div>");
    //              //  sb.Append("<a href=\"..\\FindEat.aspx?id=" + togoid + "&bid='" + info.DataID + "'\">" + info.Name + "</a></div>");
    //            }
    //        }
    //   else
    //        {
    //            sb.Append("您好，未搜索到相应标识建筑物。");
    //        }
    //    }
    //    else
    //    {
    //        sb.Append("您好，未搜索到相应标识建筑物。");
    //    }
    //    return sb.ToString();

    //}


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
        dt = bll.GetBuildingTable();
        // dt = EasyEatCache.GetBuilding();
        if (!(dt.Rows.Count > 0))
        {
            //无缓存则加入缓存
            dt = bll.GetBuildingTable();
            // EasyEatCache.SetBuilding(dt);
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
                    if (uc == 1)//表示选择区域
                    {
                        sb.Append("<div class=\"building_list\">");
                        string url = "FindEat.aspx?id=" + info["dataid"];
                        if (sort != "-1")
                        {
                            url += "&sortid=" + sort;
                        }
                        sb.Append("<a href=\"" + url + "\">" + info["name"] + "</a></div>");
                    }
                    else//表示下订单，用户中心选择地址
                    {
                        sb.Append("<div class=\"building_list\"></span>");
                        sb.Append("<a href=\"javascript:InitAddress('" + info["Name"] + "' , " + info["dataid"] + ")\">" + info["name"] + "</a></div>");
                        // sb.Append("<a href=\"FindEat.aspx?id=" + info["dataid"] + "\">" + info["name"] + "</a></div>");

                        //                        function InitAddress1(name, bid, togoid) {

                        //    window.location = "ShowTogo.aspx?id=" + togoid + "&bid=" + bid;&bid='" + info.DataID 
                        //}

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

