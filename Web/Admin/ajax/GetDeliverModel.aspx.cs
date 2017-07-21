using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using System.Text;

/// <summary>
/// 返回配送员位置(json)
/// </summary>
public partial class qy_54tss_Admin_ajax_GetDeliverModel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = WebUtility.InputText(Request["id"]);
        Response.Clear();


        IList<DeliverInfo> list = new List<DeliverInfo>();

        Deliver dal = new Deliver();

        //按照订单数量排序
        DeliverInfo model = dal.GetOneDeliver(Convert.ToInt32(id));
        list.Add(model);

        string json = "{'lat':'','lng':''}";
        foreach (DeliverInfo info in list)
        {
            json = "{'lat':'" + info.Lat + "','lng':'" + info.Lng + "','d_name':'" + info.Name + "','ordernum':'" + info.OrderNum + "'";
            json += ",'carstate':'"+info.carstate+"'";
            json += ",'direction':'" + info.direction + "'";
            json += ",'Phone':'" + info.Phone + "'";
            json += ",'completeorder':'" + info.completeorder + "'";
            json += ",'timeoutorder':'" + info.timeoutorder + "'";
            json += ",'speed':'" + info.speed + "'";

        }

        IList<simpleorderInfo> orderlist = new Custorder().GetDliverList(id);

        string orderjson = ",orderlist:"+WebUtility.ObjectToJson(orderlist)+"";
        json += orderjson;

        json += "}";

        Response.Write(json);

        Response.End();
    }
}
