using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;
using Newtonsoft.Json;

/// <summary>
/// 当前余额，及可提现金额
/// </summary>
public partial class App_Android_Deliver_cashoutmoney : System.Web.UI.Page
{
    DeliverAddMoneyLog dal = new DeliverAddMoneyLog();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string shopid = WebUtility.InputText(Request["shopid"]);

        CanCashOutInfo model = dal.GetCanCashOutmoney(Convert.ToInt32(shopid));

        Response.Write(JsonConvert.SerializeObject(model));
        Response.End();

    }
}