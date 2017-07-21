using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;

using Com.Alipay;
using System.Text;
using Hangjing.Common;

namespace Html5
{
    /// <summary>
    /// 支付成功界面
    /// </summary>
    public partial class PaySuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                lborderid.InnerHtml = Request["id"];
                lbprice.InnerHtml = Request["price"];

            }

        }


    }
}
