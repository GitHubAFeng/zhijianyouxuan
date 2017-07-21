using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Weixin;
using System.Text.RegularExpressions;

namespace Html5
{
    /// <summary>
    /// 资质证照
    /// </summary>
    public partial class showshopicshopidcard : MasterPageBase
    {
        Points daltogo = new Points();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Points daltogo = new Points();
                int id = HjNetHelper.GetQueryInt("id", 0);

                PointsInfo info = daltogo.GetModel(id);


                IList<ShopSurroundingsInfo> pics = new List<ShopSurroundingsInfo>();
                if (info.isLicense == 1)
                {
                    pics.Add(new ShopSurroundingsInfo()
                    {
                        Picture = WebUtility.ShowPic(info.licensePic),
                        Title = "营业执照"
                    });
                }

                if (info.isCatering == 1)
                {
                    pics.Add(new ShopSurroundingsInfo()
                    {
                        Picture = WebUtility.ShowPic(info.cateringPic),
                        Title = "餐饮服务许可证"
                    });
                }




                WebUtility.BindRepeater(rptppt, pics);


            }
        }

    }
}
