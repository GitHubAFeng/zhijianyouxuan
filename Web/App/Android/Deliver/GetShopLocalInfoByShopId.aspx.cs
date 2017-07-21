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
using System.Text;

using Hangjing.Model;
using Hangjing.SQLServerDAL;

// CopyRight (c) 2009-2012 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2012-05-7
// 获取商家坐标信息

public partial class App_Android_Deliver_GetShopLocalInfoByShopId : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string shopid = Hangjing.Common.HjNetHelper.GetPostParam("shopid");
        ETogoLocal dal = new ETogoLocal();

        ETogoLocalInfo model = new ETogoLocalInfo();

        model = dal.GetInfoById(shopid);

        StringBuilder json = new StringBuilder();

        if (model != null)
        {
            json.Append("{\"shopid\":\""+model.TogoId.ToString()+"\",\"lat\":\""+model.Lat+"\",\"lng\":\""+model.Lng+"\"}");
        }
        else
        {
            json.Append("{\"shopid\":\"0\",\"lat\":\"\",\"lng\":\"\"}");
        }

        Response.Write(json.ToString());
        Response.End();
    }
}
