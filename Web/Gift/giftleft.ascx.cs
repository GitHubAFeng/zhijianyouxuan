using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hangjing.SQLServerDAL;

public partial class Gift_giftleft : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //订单动态
        Integral ibll = new Integral();
        rptGetGiftRecord.DataSource = ibll.GetList(4, 1, "state = 1", "IntegralId", 1);
        rptGetGiftRecord.DataBind();
    }
}
