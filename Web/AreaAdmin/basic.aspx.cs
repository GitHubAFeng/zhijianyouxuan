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
using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using System.Collections.Generic;

public partial class AreaAdmin_basic : AdminPageBase
{
    Custorder bll = new Custorder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            shwoTodayOrder();

            hfisshowupdate.Value = SectionProxyData.GetSetValue(60);
        }
    }

    /// <summary>
    /// 绑定指定的数据
    /// </summary>
    protected void shwoTodayOrder()
    {
        string orderWhere = "  DATEDIFF(day,OrderDateTime,GETDATE()) =0 ";
        orderWhere = WebUtility.GetCustorderSql(orderWhere);

        int[] hours = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
        IList<OrderCountInfo> shops = bll.GetOrderCount(7, "", "", "", orderWhere);
        string xjson = "";
        string yjson = "[";

        foreach (var hour in hours)
        {
            int ordercount = 0;
            decimal allprice = 0;
            foreach (var item in shops)
            {
                if (hour == Convert.ToInt32(item.CountKey))
                {
                    ordercount = item.CountIntValue;
                    allprice = item.CountDecimalValue;
                    break;
                }
            }
            yjson += "{'ordercount':" + ordercount + ",'allprice':" + allprice + "},";
        }


        xjson = WebUtility.dellast(xjson);
        yjson = WebUtility.dellast(yjson);

        yjson += "]";

        hfxjson.Value = xjson;
        hfyjson.Value = yjson;

    }



}
