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
    public partial class cartdetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Points daltogo = new Points();
                int id = HjNetHelper.GetQueryInt("id", 0);
                string SqlWhere = String.Format("Unid={0}", id);

                string strDistance = " 1=1 ";
                string lat = WebUtility.FixgetCookie("mylat");
                string lng = WebUtility.FixgetCookie("mylng");

                IList<shopdeliveryInfo> deliveryrecord = new shopdelivery().GetList(" tid = " + id);

                IList<PointsInfo> shoplist = daltogo.GetDistanceListSuper(1, 1, SqlWhere, "unid", 1, lat, lng, strDistance);

                IList<ShopFoodPictureInfo> alltags = CacheHelper.GetShopPicTag();
                foreach (var item in shoplist)
                {
                    item.pictags = new List<ShopFoodPictureInfo>();
                    string[] tagids = WebUtility.delBrackets(item.OpenTime).Split(',');
                    foreach (var ids in tagids)
                    {
                        foreach (var tag in alltags)
                        {
                            if (ids.Trim() == tag.IID.ToString().Trim())
                            {
                                item.pictags.Add(tag);
                            }
                        }
                    }

                    foreach (var record in deliveryrecord)
                    {
                        if (record.distancestart <= item.Distance && record.distanceend > item.Distance)
                        {
                            item.SendFee = record.sendmoney;
                            item.SendLimit = record.minmoney;
                            break;
                        }
                    }
                }
                PointsInfo togomodel = shoplist[0];
                if (togomodel == null)
                {
                    return;
                }

                hnTogoStatus.Value = togomodel.Status + "";     //状态
                hfsendfree.Value = togomodel.SendFee.ToString();//配送费
                hidTogoName.Value = togomodel.Name;
                hnTogoBusiness.Value = togomodel.isbisness + ""; //时间
                hfminimoney.Value = togomodel.SendLimit.ToString();
                hffreemoney.Value = togomodel.PTimes.ToString();//满多少免配送费，0表示未启用
            }

        }
    }
}