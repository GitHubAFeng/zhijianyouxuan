using System;
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

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.SQLServerDAL;

public partial class OrderSuccess_weixinpay : System.Web.UI.Page
{

    Custorder dalorder = new Custorder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string orderid = HjNetHelper.GetQueryString("orderid");
            string price = HjNetHelper.GetQueryString("price");
            lborderid.InnerHtml = orderid;
            paymoney.InnerText = price;

            CustorderInfo model = dalorder.GetModel(orderid);
            qrtext.Value = model.PayOrderId;
        }
    }
   
}
