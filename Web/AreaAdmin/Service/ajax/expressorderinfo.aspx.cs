﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using System.Text;

/// <summary>
/// 返回订单内容
/// </summary>
public partial class qy_54tss_AreaAdmin_Service_ajax_expressorderinfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string orderid = Request["orderid"];

        StringBuilder deliver = new StringBuilder("");

        ExpressOrderInfo model = new ExpressOrder().GetModelByOrderid(orderid);  //.GetModel(orderid);
        string messagebody = WebUtility.getExpressOrderInfo(model);

        string rs = "{\"messagebody\":\"" + messagebody + "\",\"record\":\"" + deliver.ToString() + "\"}";

        Response.Write(rs);

        Response.End();
    }
}
