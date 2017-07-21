using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;


namespace Html5
{
    public partial class showtogoorder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetData();
            }
        }


        void GetData()
        {
            string id = HjNetHelper.GetQueryString("id");
            CustorderInfo model = new Custorder().GetModel(id);

            IList<OrderPromotionInfo> promotions = new OrderPromotion().GetList(10, 1, "revevar2 = '" + model.orderid + "'", "pid", 1);
            model.Promotions = promotions;

            IList<CustorderInfo> list = new List<CustorderInfo>();
            list.Add(model);
            WebUtility.BindRepeater(rptorder, list);
            WebUtility.BindRepeater(rptorder1, list);
            WebUtility.BindRepeater(rptorder2, list);

            WebUtility.BindRepeater(rptppt, new OrderStep().GetOrderSteps(model.orderid));

            IList<FoodlistInfo> foods = new Foodlist().GetAllByOrderID(id);

            WebUtility.BindRepeater(rptfood, foods);

            IList<PointsInfo> shop = new Points().GetList(1, 1, "unid=" + model.TogoId, "unid", 1);
            WebUtility.BindRepeater(rptshop, shop);

            if (model.Commentstate == 0)
            {
                dellink.HRef = "Commentshop.aspx?id=" + model.Unid;
            }
            else
            {
                dellink.Visible = false;
            }

            this.hState.Value = model.OrderStatus.ToString() ;

            if (model.OrderChecker == 1)
            {
                divconfirm.Style["display"] = "none";

            }
            hfshopid.Value = model.TogoId.ToString();
            string foodids = "";
            foreach (var item in foods)
            {
                foodids += item.FoodUnid + ",";
            }
            hffoodids.Value = WebUtility.dellast(foodids);

        }


        /// <summary>
        /// 确认收货
        /// </summary>
        protected void upStatus_Click(object sender, EventArgs e)
        {

            Custorder bll = new Custorder();
            string orderid = HjNetHelper.GetQueryString("id");
            string sql = "  UPDATE dbo.Custorder SET OrderChecker = 1 WHERE orderid =  '"+ orderid + "' ";

            WebUtility.excutesql(sql);

            CustorderInfo model = new Custorder().GetModel(orderid);

            new OrderStep().Add(new OrderStepInfo()
            {
                stepcode = 40,
                title = "用户确认收货",
                subtitle = "",
                deliverid = 0,
                addtime = DateTime.Now,
                orderid = model.orderid,
                revevar1 = "",
                revevar2 = ""

            });

            string url = "showtogoorder.aspx?id=" + orderid+"&msg=1";
            Response.Redirect(url);

        }


    }
}
