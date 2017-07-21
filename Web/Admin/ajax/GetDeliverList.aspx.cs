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

//zjf@ihangjing.com 2012-5-10
public partial class qy_54tss_Admin_ajax_GetDeliverList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        StringBuilder deliver = new StringBuilder("<ul>");

        IList<DeliverInfo> list = new List<DeliverInfo>();

        Deliver dal = new Deliver();
        string where = " 1=1 and IsApproved=0 ";//审核状态为 已审核
        //区域名称
        string sname = WebUtility.InputText(Server.UrlDecode(Request["sname"]));
        if (sname != null && sname != "所有区域" && sname != "0" && sname != "")
        {
            where += " and Section = '" + sname + "'";
        }
        int cid = HjNetHelper.GetQueryInt("cid", 0);
        if (cid > 0)
        {
            where += " and inve1 = " + cid + "";
        }


        //按照订单数量排序
        list = dal.GetListWithOrderNum(1000, 1, where, "TOrderNum", 1);
        string jsonstr = "[";

        foreach (DeliverInfo info in list)
        {
            string workstate = "";
            //有订单的则为配送 状态
            if (info.OrderNum > 0)
            {
                workstate = "peisong";
               
            }
            else
            {
                workstate = WebUtility.GetDeliverStatus(info.Status.ToString());
            }
            deliver.Append("<li class=\""+ workstate + "\">");

            deliver.Append("<a href=\"javascript:void(0)\" onclick=\"showme(" + info.OrderNum.ToString() + " ,'" + info.Name + "' , '" + info.Lat + "','" + info.Lng + "','" + info.DataId + "','" + info.GpsIMEI + "','" + info.Phone + "')\">");
            deliver.Append("<p>"+info.OrderNum.ToString()+"</p>");
            deliver.Append("<p>"+info.Name+"</p>");
            deliver.Append("</a></li>");




            jsonstr += "{'Name':'" + info.Name + "','OrderNum':'" + info.OrderNum + "','Lat':'" + info.Lat + "','Lng':'" + info.Lng + "','DataId':'" + info.DataId + "','GpsIMEI':'" + info.GpsIMEI + "','Phone':'" + info.Phone + "','workstate':'" + workstate + "'},";
        }
        jsonstr = WebUtility.dellast(jsonstr);
        jsonstr += "]";

        deliver.Append("</ul>");
        string hidejson = "<input type='hidden' id='deliverlistjson' value=\"" + jsonstr + "\">";
        deliver.Append(hidejson);

        Response.Write(deliver.ToString());

        Response.End();
    }
}
