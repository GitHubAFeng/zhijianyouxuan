using System;
using System.Collections;
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

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;
using Hangjing.Cache;

/// <summary>
/// 配送员单日配送轨迹
/// </summary>
public partial class EasyEatHome_MTogo_deliverpath : AdminPageBase
{
    Deliver dal = new Deliver();
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckRights("A");
        if (!Page.IsPostBack)
        {
            int tid = HjNetHelper.GetQueryInt("tid", 0);

            //控件绑定内容
            DeliverInfo deliver = dal.GetModel(tid);
            if (deliver != null)
            {
                diagram_tab_orders.HRef = "DeliverDetail.aspx?id=" + tid + "&cityid=" + deliver.Inve1;
            }

            int cid = HjNetHelper.GetQueryInt("cityid", 0);

            CityInfo citymodel = new City().GetModel(cid);
            if (citymodel != null)
            {
                hfcity.Value = citymodel.cname;
            }

            tbdate.Text = DateTime.Now.ToShortDateString();
        }
    }
}

