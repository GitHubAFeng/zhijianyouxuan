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

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using System.Text;

/// <summary>
/// 返回和商家距离最近的配送员(3个)
/// </summary>
public partial class qy_54tss_Admin_ajax_GetDeliverListbySpan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        IList<DeliverInfo> list = new List<DeliverInfo>();

        Deliver dal = new Deliver();
        string where = " 1=1 and Status > -1 and IsApproved=0 and IsWorking=1";//并且审核状态为 已审核
        //区域名称
        string sname = Server.UrlDecode(Request["sname"]);
        if (sname != null && sname != "所有区域" && sname != "0" && sname != "")
        {
            where += " and Section = '" + sname + "'";
        }
        if (Request["dids"] != null)
        {
            where += " and DataId in (" + Request["dids"] + ")";
        }
        int cid = HjNetHelper.GetQueryInt("cid", 0);
        if (cid > 0)
        {
            where += " and inve1 = " + cid + "";
        }

        //按照订单数量排序
        list = dal.GetListWithOrderNum(1000, 1, where, "TOrderNum", 1);

        string lat = Request["lat"];
        string lng = Request["lng"];

        foreach (DeliverInfo info in list)
        {
            string mydistance = WebUtility.getDistance(lat , lng , info.Lat , info.Lng);
            if (mydistance == "--")
            {
                info.distance = 99999999;//不能计算就显示很远
            }
            else
            {
                info.distance = Convert.ToInt32( Convert.ToDecimal(mydistance));
            }
            info.Inve2 = "";
        }

        IList<DeliverInfo> listfix = new IListSort<DeliverInfo>(list, "distance", true).Sort();

        Response.Write(WebUtility.ObjectToJson(listfix));

        Response.End();
    }
}
