using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;

using System.Text;
using Hangjing.Common;

namespace Html5
{
    /// <summary>
    /// 跑腿订单成功
    /// </summary>
    public partial class expressOrderSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string orderid = HjNetHelper.GetQueryString("orderid");
                lborderid.InnerHtml = orderid;
                ExpressOrder dalorder = new ExpressOrder();
                ExpressOrderInfo infoorder = dalorder.GetModel(orderid);
                lbprice.InnerHtml = (infoorder.sendmoney+infoorder.TotalPrice).ToString();
            }

        }
    }
}
