using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

namespace Html5
{
    public partial class sharedetail : sharePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string sql = " reveint > 0 and id=" + HjNetHelper.GetQueryInt("id", 0);


            IList<userpacketInfo> orderlist = new userpacket().GetList(1, 1, sql, "id", 1);

            WebUtility.BindRepeater(rptorder, orderlist);


        }
    }
}
