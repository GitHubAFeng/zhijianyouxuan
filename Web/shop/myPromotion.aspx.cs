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
using Hangjing.Common;
using System.IO;
using Hangjing.Model;

public partial class shop_myPromotion : System.Web.UI.Page
{
    Points togodal = new Points();
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!this.Page.IsPostBack)
        {
            UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);

            PointsInfo shop = togodal.GetModel(UserHelp.GetUser_Togo().Unid);

            WebUtility.BindList("pId", "revevar1", CacheHelper.GetWebPromotionConfig(), tbPEnd);
            WebUtility.SelectValue(rblshopptype, shop.PType.ToString());
            switch (shop.PType)
            {
                case 10:
                    webpromotionbox.Style["display"] = "none";

                    this.rptFoodList.DataSource = new webPromotionConfig().GetList(10, 1, "shopid=" + shop.Unid, "pid", 1, 0);
                    this.rptFoodList.DataBind();


                    break;
                case 20:
                    promotionbox.Style["display"] = "none";
                    WebUtility.CheckValueS(tbPEnd, shop.PEnd);


                    break;
                default:
                    webpromotionbox.Style["display"] = "none";
                    promotionbox.Style["display"] = "none";

                    break;
            }

        }
    }



    protected void btSave_Click(object sender, EventArgs e)
    {
        AlertScript.RegScript(this, UpdatePanel1, "tipsWindown('提示信息','text:修改成功!','250','150','true','3000','true','text');");

    }
}
