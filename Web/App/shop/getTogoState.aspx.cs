using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;

// CopyRight (c) 2009-2012 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2012-05-25
// 更新订单状态
// 传入参数：订单编号 订单状态 
public partial class App_Android_shop_getTogoState : System.Web.UI.Page
{

    Points dal = new Points();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string shopid = WebUtility.InputText(Request["shopid"]);
        //string state = WebUtility.InputText(Request["state"]);
        //Status=1 营业，0，休息

        PointsInfo model = dal.GetModel(Convert.ToInt32(shopid));

        if (model !=null)
        {
            Response.Write("{\"togoid\":\"" + shopid.Trim() + "\",\"state\":\"" + model.Status + "\"}");
        }
        else
        {
            Response.Write("{\"togoid\":\"" + shopid.Trim() + "\",\"state\":\"" + model.Status + "\"}");
        }

        Response.End();
    }
}
