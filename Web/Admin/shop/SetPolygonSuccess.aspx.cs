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

public partial class EasyEatHome_MTogo_SetPolygonSuccess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        litUrl.Text = "<a href='ShopLocal.aspx?tid=" + Hangjing.Common.HjNetHelper.GetQueryString("tid") + "&cid=" + Hangjing.Common.HjNetHelper.GetQueryString("cid") + "'>查看该商家定位</a><br /><a href='ShopDetail.aspx?id=" + Hangjing.Common.HjNetHelper.GetQueryString("tid") + "'>查看该商家信息</a><br /><a href='ShopList.aspx'>返回商家列表</a>";
    }
}
