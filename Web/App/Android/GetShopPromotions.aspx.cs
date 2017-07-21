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
using Newtonsoft.Json;
using Hangjing.WebCommon;

/// <summary>
/// 商家促销
/// </summary>
public partial class AndroidAPI_GetShopPromotions : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        StringBuilder shoplistjson = new StringBuilder();
        int shopid = HjNetHelper.GetPostParam("shopid", 0);

        PointsInfo shop = new Points().GetModel(shopid);



        shoplistjson.Append("{");
        shoplistjson.Append("\"promotions\":" + WebUtility.NoHTML(JsonConvert.SerializeObject(PromotionTool.getPromotionsFormPicTagList(shop.Unid, shop.PType, shop.PEnd))) + "");//促销


        shoplistjson.Append("}");



        Response.Write(shoplistjson.ToString());
        Response.End();

    }
}
