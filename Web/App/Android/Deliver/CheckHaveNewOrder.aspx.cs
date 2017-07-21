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
using System.Text;

using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
/// 检查有没有新订单（外卖）,返回外卖订单多少个
/// </summary>
public partial class App_Android_Deliver_CheckHaveNewOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string did = Request["DId"];

        StringBuilder json = new StringBuilder();
        Custorder dal = new Custorder();

        // 1新增订单  2审核通过  3已经调度  4 正在配送  5处理成功  0已经取消
        //获取后台已经调度好的订单，配送员处理后则设置为 正在配送
        string sqlwhere = " OrderDeliver.DeliverId=" + WebUtility.InputText(did) + " and OrderStatus= 7 and sendstate = 2 ";
        int count = dal.DeliverGetOrderCount(sqlwhere);

        //跑腿订单
        sqlwhere = " State = 1 and Inve1 = " + did;
        int expresscount = new ExpressOrder().GetCount(sqlwhere);

        json.Append("{\"state\":\"0\",\"count\":\"" + count.ToString() + "\",\"expresscount\":\"" + expresscount.ToString() + "\"}");
        Response.Write(json.ToString());
        Response.End();
    }
}
