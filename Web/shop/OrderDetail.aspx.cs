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

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using System.Collections.Generic;

public partial class shop_OrderDetail : System.Web.UI.Page
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

    private string flag
    {
        get
        {
            object o = ViewState["flag"];
            return (o == null) ? "" : Convert.ToString(o);
        }
        set
        {
            ViewState["flag"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
        if (!this.Page.IsPostBack)
        {
            new OrderState().BindOrderState(ddlFunction);
            if (Request.QueryString["id"] != null)
            {
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
            this.tbcardpay.Text = (model.cardpay).ToString();
            this.TextBoxtop.Text = "" + model.OrderSums;
            this.lbOrderdate.Text = model.OrderDateTime.ToString();
            this.togoname.Text = model.TogoName;
            this.lbUName.Text = model.OrderRcver;
            this.lbTel.Text = model.OrderComm;
            this.lbAddress.Text = model.AddressText;
            lbmsg.Text = model.SendTime.ToShortTimeString() + "";
            lbremark.Text = model.OrderAttach;
            ddlFunction.SelectedValue = model.OrderStatus.ToString();
            this.rtpBooks.DataSource = eorderfooddal.GetAllByOrderID(model.orderid);
            this.rtpBooks.DataBind();
            OrderState = WebUtility.TurnOrderState(model.OrderStatus);
            this.lbcprice.Text = model.SendFee.ToString();
            flag = model.OrderStatus.ToString();

            lboldprice.Text = model.OldPrice.ToString();
            lbshopdiscountmoney.Text = model.shopdiscountmoney.ToString();

            lbpaytstate.Text = WebUtility.TurnPayState(model.paystate.ToString());
            this.lblpaymoney.Text = model.paymoney.ToString();
            this.lblpaymode.Text = WebUtility.TurnPayModel(model.paymode);

            lbpromotion.Text = model.promotionmoney.ToString();

            string discountmsg = "无折扣";
            if (model.OldStatus < 100)
            {
                discountmsg = "会员折扣：" + model.OldStatus + "折";
            }
            lbdiscount.Text = discountmsg;
            lbeattype.Text = model.ReveInt2 == 0 ? "外卖" : "堂食";
            lbpeople.Text = model.ReveInt1.ToString() + "人";
            //统一配送的，商家不能修改订单状态
            if (model.ReveVar1.Trim() == "0")
            {
                ddlFunction.Enabled = false;
                btSearch.Style["display"] = "none";
            }

            string opmsg = "";
            if (model.IsShopSet == 1)
            {
                opmsg += "于" + model.ReveDate1 + "接收，";
            }
            if (model.IsShopSet == 2)
            {
                opmsg += "于" + model.ReveDate1 + "拒绝，";
            }

            opmsgbox.InnerHtml = opmsg;
            tbshopremark.Text = model.OrderAddrEx;

            IList<CustorderInfo> orders = new List<CustorderInfo>();
            orders.Add(model);

            WebUtility.BindRepeater(rptorder, orders);
            lbpackage.Text = model.Packagefee.ToString();
        }
    }
    /// <summary>
    /// 返回
    /// </summary>
    /// <param name="foodid"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    protected string GetFoodName(object foodid, int type)
    {
        Foodinfo dal = new Foodinfo();
        return dal.GetModel(Convert.ToInt32(foodid)).FoodName;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/shop/OrderList.aspx");
    }


    protected void btSaveState_Click(object sender, EventArgs e)
    {
        int dataid = HjNetHelper.GetQueryInt("id", 0); //custorder的Unid主键
        CustorderInfo model = etogoorderdal.GetModel(dataid);

        etogoorderdal.AddOrderRecord(model.orderid, Convert.ToInt32(ddlFunction.SelectedValue), "商家", "订单详细界面");

        if (etogoorderdal.UpdataState(dataid, Convert.ToInt32(ddlFunction.SelectedValue)) > 0)
        {
            AlertScript.RegScript(this.Page, "tipsWindown('提示信息','text:更新状态成功!','250','150','true','1000','true','text');");
            if (ddlFunction.SelectedValue == "3")//避免多次加分
            {
                etogoorderdal.AddPoint(model.orderid.ToString());
            }

            GetData();
        }
        else
        {
            AlertScript.RegScript(this.Page, "tipsWindown('提示信息','text:更新状态失败!','250','150','true','1000','true','text');");
        }
    }

    /// <summary>
    /// 操作按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void set_Click(object sender, EventArgs e)
    {
        int shopid = UserHelp.GetUser_Togo().Unid;
        string orderid = HjNetHelper.GetQueryString("id");
        Button opbt = (Button)sender;



        string resign = WebUtility.InputText(tbshopremark.Text);
        int shopset = 0;
        switch (opbt.ID)
        {
            case "tbreceive":
                shopset = 1;
                break;
            case "tbrefuse":
                shopset = 2;
                break;
            case "tbdy":
                Response.Redirect("printorder.aspx?id=" + orderid);
                break;
        }

        shopSetOrder set = new shopSetOrder(Context, shopid, orderid, shopset, resign);
        int rs = set.HandleOrder();

        string url = "OrderDetail.aspx?id=" + orderid;

        if (rs == 0)
        {
            AlertScript.RegScript(this.Page, "alert('此订单已经处理过了');");
        }
        else
        {
            AlertScript.RegScript(this.Page, "alert('操作成功');gourl('" + url + "');");
        }

    }

    protected IList<FoodlistInfo> getproduct(object oid)
    {
        return eorderfooddal.GetAllByOrderID(oid.ToString());
    }
}
