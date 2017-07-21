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
/// <summary>
/// 获取礼品详细
/// </summary>
public partial class Android_GetGiftDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        int id = HjNetHelper.GetPostParam("id", 0);

        Gifts dal = new Gifts();
        GiftsInfo info = dal.GetModel(id);

        StringBuilder shoplistjson = new StringBuilder();

        if (info != null)
        {
            shoplistjson.Append("{\"GiftsId\":\"" + info.GiftsId + "\",\"ClassId\":\"" + info.ClassId + "\",\"Gname\":\"" + info.Gname + "\",\"Content\":\"" + WebUtility.FileterJson(WebUtility.NoHTML(info.Content)) + "\",\"GiftsPrice\":\"" + info.GiftsPrice + "\",\"NeedIntegral\":\"" + info.NeedIntegral + "\",\"bigpicture\":\"" + info.bigpicture.Replace("~", WebUtility.GetConfigsite()) + "\",\"Picture\":\"" + info.Picture.Replace("~", WebUtility.GetConfigsite()) + "\",\"stocks\":\"" + info.Stocks + "\",\"sortnum\":\"" + info.sortnum + "\"}");
        }

        Response.Write(shoplistjson.ToString());
        Response.End();
    }
}
