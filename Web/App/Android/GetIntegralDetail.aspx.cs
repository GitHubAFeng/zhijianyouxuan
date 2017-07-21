using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class App_AndriodV2_GetIntegralDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        int id = HjNetHelper.GetQueryInt("id", 0);

        Integral dal = new Integral();
        IntegralInfo info = dal.GetModel(id);

        StringBuilder shoplistjson = new StringBuilder();

        if (info != null)
        {
            shoplistjson.Append("{\"CustId\":\"" + info.CustId + "\",\"UserName\":\"" + info.UserName + "\",\"GiftName\":\"" + info.GiftName + "\",\"PayIntegral\":\"" + info.PayIntegral + "\",\"Cdate\":\"" + info.Cdate + "\",\"Person\":\"" + info.Person + "\"");
            shoplistjson.Append(",\"Address\":\"" + info.Address + "\",\"Phone\":\"" + info.Phone + "\",\"Date\":\"" + info.Date + "\",\"remark\":\"" + info.Remark + "\",\"State\":\"" + info.State + "\"}");
        }

        Response.Write(shoplistjson.ToString());
        Response.End();
    }
}
