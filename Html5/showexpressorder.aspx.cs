using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;


namespace Html5
{
    /// <summary>
    /// 跑腿订单详情
    /// </summary>
    public partial class showexpressorder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ExpressOrderInfo model = new ExpressOrder().GetModel(HjNetHelper.GetQueryInt("id",0));

            IList<ExpressOrderInfo> list = new List<ExpressOrderInfo>();
            list.Add(model);

            WebUtility.BindRepeater(rptorder, list);
        }
    }
}
