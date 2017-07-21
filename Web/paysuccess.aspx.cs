using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class PaySuccess : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string orderid = HjNetHelper.GetQueryString("id");
            string price = HjNetHelper.GetQueryString("price");

            spanorderid.InnerText = orderid;
            paymoney.InnerText = price;

            if (orderid.StartsWith("r"))
            {
                divmsg.InnerHtml = "恭喜，您的帐户充值成功！";
            }
            else
            {
                divmsg.InnerHtml = "恭喜，您的订单已支付成功！";
            }
        }
        

    }

    protected void Userhome_click(object sender, EventArgs e)
    {
        Response.Redirect("User/myindex.aspx");
    }
}
