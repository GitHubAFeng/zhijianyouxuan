using System;
using System.Collections;
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

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;

using System.Text;

// 商家名称自动补全 需要返回商家的名称以及商家的编号  此处商家的名称和编号保存在缓存中
// 缓存在系统商家管理中进行更新 即在Shop/ShopDetail.aspx 中根据情况更新缓存 
// Todo: 或者考虑一定时间段后更新 手动更新
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-07-15

public partial class Admin_ajax_TogoNameCommplit :AdminPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string content = Request["names"];
        if (content != null)
        {
            FillList(Server.UrlDecode(Request["names"]));
        }
    }

    protected void FillList(string s)
    {
        string sql = "Name like '%" + s + "%'";
        DataTable dt = new DataTable();
        //dt = EasyEatCache.GetTogoNameAndId();
        StringBuilder results = new StringBuilder();

        //if (!(dt.Rows.Count > 0))
        {
            //无缓存则加入缓存
           /// dt = new Points().GetAllTogeNameAndId();
            //EasyEatCache.SetTogoNameAndId(dt);

            //GlobalManage.AddSystemLog("TogoNameAndId no cache add cache", LogType.General, "System");
        }

        DataRow[] dr_list = dt.Select(sql, "DataId desc");

        if (dr_list.Length != 0)
        {
            int ncount = 0;
            foreach (DataRow info in dr_list)
            {
                ncount++;
                results.Append("<a onmouseover='mouseOverAddr(this)' onmouseout='mouseOutDiv(this)' href='javascript:void(0)' onclick=\"selectItem('" + info["TogoName"] + "|" + info["DataId"] + "')\">" + info["TogoName"] + "</a>");
                
                //同时保存商家名称和商家编号 使用 {|} 进行分割
                results.Append("<input name='" + info["TogoName"] + "|" + info["DataId"] + "' type='hidden'>");
                if (ncount >= 7)
                {
                    break;
                }
            }
            results.Append("<a class=\"off\" href='javascript:void(0)' onclick=\"SetDivDisplayType('address_drop','none')\" >关闭</a>");
        }
        else
        {
            //results.Append("<name>");
            //results.Append("没有此记录");
            //results.Append("</name>");
        }

        Response.AddHeader("Cache-Control", "no-cache");

        Response.Write(results.ToString());
        Response.End();
    }
}
