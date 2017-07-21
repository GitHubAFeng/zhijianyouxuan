using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class UserHome_orderdetail : System.Web.UI.Page
{
    public string ImagePath = WebUtility.GetMasterPicturePath();

    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? string.Empty : o.ToString();
        }
        set
        {
            ViewState["SqlWhere"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin(Request.Url.PathAndQuery);
        if (!this.Page.IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                string cityid = WebUtility.get_userCityid();
                if (cityid == "0")
                {
                    string url = "/citys.aspx";
                    Response.Redirect(url);
                }
                hfcityid.Value = cityid;
                hfcityname.Value = WebUtility.get_userCityName();
                hidLat.Value = Map.DefautLocal.getlocalInfo().Lat;
                hidLng.Value = Map.DefautLocal.getlocalInfo().Lng;

                GetData();
            }
        }
    }


    Foodlist eorderfooddal = new Foodlist();
    Custorder etogoorderdal = new Custorder();
    public string OrderState = null;

    void GetData()
    {
        CustorderInfo model = etogoorderdal.GetModel(HjNetHelper.GetQueryInt("id", 0));

        if (model != null)
        {
            this.lbOrderId.Text = model.orderid;
            this.lbOrderdate.Text = model.OrderDateTime.ToString();
            this.togoname.Text = model.TogoName;
            this.lbUName.Text = model.OrderRcver;
            this.lbTel.Text = model.OrderComm;
            this.lbAddress.Text = model.AddressText;
            lbmsg.Text = model.SendTime.ToShortTimeString() + "";
            lbremark.Text = model.OrderAttach;

            this.rtpBooks.DataSource = eorderfooddal.GetList(100, 1, "orderid ='" + model.orderid + "'", "unid", 1);
            this.rtpBooks.DataBind();

            OrderState = WebUtility.TurnOrderState(model.OrderStatus.ToString());

            OrderStateProcess myprocess = new OrderStateProcess(model.OrderStatus);
            process.InnerHtml = myprocess.Processhtml;

            hfstate.Value = model.OrderStatus.ToString();
            hflatlng.Value = model.ReveVar2;
            hfdeliverid.Value = model.deliverid.ToString();
            hforderid.Value = model.orderid;
            hfusername.Value = model.OrderRcver;
            hfaddress.Value = model.AddressText;
            hfshopname.Value = model.TogoName;

            IList<CustorderInfo> orders = new List<CustorderInfo>();
            orders.Add(model);

            IList<OrderPromotionInfo> promotions = new OrderPromotion().GetList(10, 1, "revevar2 = '" + model.orderid + "'", "pid", 1);
            model.Promotions = promotions;

            WebUtility.BindRepeater(rptorder, orders);

            WebUtility.BindRepeater(rptppt, new OrderStep().GetOrderSteps(model.orderid));

            if (model.OrderChecker == 1)
            {
                upStatus.Style["display"] = "none";

            }

        }
    }


    protected string GetFoodName(object foodid, int type)
    {
        Foodinfo dal = new Foodinfo();
        return dal.GetModel(Convert.ToInt32(foodid)).FoodName;
    }


    /// <summary>
    /// 返回
    /// </summary>
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/user/MyOrderList.aspx");
    }


    /// <summary>
    /// 确认收货
    /// </summary>
    protected void upStatus_Click(object sender, EventArgs e)
    {
        Custorder bll = new Custorder();
        int orderid = HjNetHelper.GetQueryInt("id", 0);//订单编号
        string sql = "  UPDATE dbo.Custorder SET OrderChecker = 1 WHERE Unid =  " + orderid;

        WebUtility.excutesql(sql);

        CustorderInfo model = etogoorderdal.GetModel(orderid);

        new OrderStep().Add(new OrderStepInfo()
        {
            stepcode = 40,
            title = "用户确认收货",
            subtitle = "",
            deliverid = 0,
            addtime = DateTime.Now,
            orderid = model.orderid,
            revevar1 = "",
            revevar2 = "0",
            revevar3 = "0"

        });

        string url = "orderdetail.aspx?id=" +orderid;

        AlertScript.RegScript(this.Page,  "alert('操作成功','success','true',5);gourl('" + url + "');");


    }
}
