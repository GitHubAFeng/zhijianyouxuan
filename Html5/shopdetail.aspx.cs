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
    public partial class shopdetail : System.Web.UI.Page
    {
        ETogoOpinion dal = new ETogoOpinion();
        Points daltogo = new Points();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = HjNetHelper.GetQueryInt("id", 0);
                tid.Value = id.ToString();


                string SqlWhere = String.Format("Unid={0}", id);

                string strDistance = " 1=1 ";

                IList<PointsInfo> shoplist = daltogo.GetDistanceListSuper(1, 1, SqlWhere, "unid", 1, "0", "0", strDistance);

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

                    try
                    {
                        int count = dal.GetCount("TogoID=" + id);
                        IList<ETogoOpinionInfo> infolist = dal.GetList(1000, 1, "TogoID=" + id, "PostTime", 1);
                        int ServiceGrade = 0;
                        int FlavorGrade = 0;
                        int SpeedGrade = 0;
                        int all = 0;
                        foreach (var items in infolist)
                        {
                            ServiceGrade += item.ServiceGrade;
                            FlavorGrade += item.FlavorGrade;
                            SpeedGrade += item.SpeedGrade;
                            all += 15;
                        }

                        if (count > 0)
                        {
                            int rev = ((ServiceGrade + FlavorGrade + SpeedGrade) * 100 / all);
                            item.review = rev + "%";
                            item.reviewtimes = count;
                        }
                    }
                    catch (Exception ex)
                    {
                        HJlog.toLog(ex.ToString());
                    }

                }
                WebUtility.BindRepeater(rptshop, shoplist);

                PointsInfo togomodel = shoplist[0];

                int iscollect = 0;//是否收藏，0表示没有，1表示已经收藏
                ECustomerInfo user = UserHelp.GetUser();
                if (user != null)
                {
                    hidUid.Value = user.DataID.ToString();
                    ETogoCollect dal_c = new ETogoCollect();
                    string sql = " 1= 1 and inve1=0 and togoid=" + id + " and userid=" + user.DataID;
                    if (dal_c.GetTogoCount(sql) > 0)
                    {
                        iscollect = 1;
                        collect.Value = "1";
                    }
                }
            }

        }
    }
}