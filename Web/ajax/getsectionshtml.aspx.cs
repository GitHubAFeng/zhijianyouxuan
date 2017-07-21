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
/// 根据城市获取相识获取
/// </summary>
public partial class getsections_test : System.Web.UI.Page
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
                string sort = "-1";//城市编号
                if (Request["sort"] != null && Request["sort"].ToString() != "-1")
                {
                    sort = Request["sort"];
                }
                int uc = 0;
                if (HjNetHelper.GetQueryInt("uc", 0) != 0)
                {
                    uc = Convert.ToInt32(Request["uc"]);
                }
                int page = Convert.ToInt32(Request["page"]);
                ret = getSection(flag, sid, ch, key, page, sort, uc);
                Response.Write(ret);
                break;
            default:
                Response.Write("-1");
                break;
        }

        Response.End();
    }

    ESection bll = new ESection();

    /// <summary>
    /// 所有列表都有些获取。int sectionid, string ch, string key 这几个参数为-1表是空
    /// sort表示分类，只是加在链接后台的参数 -1表示没有 uc 1表示选择区域 ， 0表示选择楼宇
    /// </summary>
    /// <param name="flag">1表示全部</param>
    /// <param name="sectionid">区域编号</param>
    /// <param name="ch">首字母</param>
    /// <param name="key">关键字</param>
    /// <returns></returns>
    protected string getSection(int flag, string sectionid, string ch, string key, int page, string sort, int uc)
    {
        StringBuilder sb = new StringBuilder();

        string sql = "1=1 ";
        sql += " and cityid = " + sort;

        IList<SectionInfo> list = bll.GetAll_fix(Convert.ToInt32(sort));

        foreach (SectionInfo info in list)
        {
            sb.Append("<li id=\"one" + info.SectionID + "\" class=\"sbuild\">");
            sb.Append(" <a href='javascript:getbuild(0 , " + info.SectionID + " , -1 , -1 ,1 ," + sort + " , 0);'>" + info.SectionName + "</a></li>");
        }


        return sb.ToString();
    }

}