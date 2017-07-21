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
using System.Collections.Generic;

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.DBUtility;
using System.Text.RegularExpressions;

// 订单信息管理
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com 
// 2011-06-10

public partial class Admin_shop_OrderDetail : AdminPageBase
{
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

    private int oid
    {
        get
        {
            object o = ViewState["oid"];
            return (o == null) ? 0 : Convert.ToInt32(o.ToString());
        }
        set
        {
            ViewState["oid"] = value;
        }
    }

    /// <summary>
    /// 管理员编号
    /// </summary>
    private int uid
    {
        get
        {
            object o = ViewState["uid"];
            return (o == null) ? 0 : Convert.ToInt32(o.ToString());
        }
        set
        {
            ViewState["uid"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        CheckRights("C");

        if (!IsPostBack)
        {
            new OrderState().BindOrderState(ddlOrderState);
            //编辑
            string OrderId = HjNetHelper.GetQueryString("id");

            if (!string.IsNullOrEmpty(OrderId))
            {
                BindData(OrderId);
                hidDataId.Value = OrderId.ToString();

                string msg = "更新订单信息";
                pageType.Text = msg;
                oid = Convert.ToInt32(OrderId);
                uid = UserHelp.GetAdmin().ID;

                lbcallmsg.InnerHtml = gethurry(OrderId);
            }
            else
            {
                pageType.Text = "新增订单";
                hidDataId.Value = "0";
            }
        }
    }

    Custorder bll = new Custorder();
    Foodlist orderfood_bll = new Foodlist();

    protected void BindData(string OrderId)
    {
        EnumHelper.OrderSourceToDropDownList(ddlsource);
        CustorderInfo info = bll.GetModel(Convert.ToInt32(OrderId));

        if (info != null)
        {

            hidDataId.Value = info.Unid.ToString();
            hidTogoId.Value = info.TogoId.ToString();
            hidUserId.Value = info.UserId.ToString();

            tbOrderId.Text = info.orderid.ToString();
            tbTogoName.Text = info.TogoName;
            tbCustomerName.Text = info.CustomerName;
            tbUserName.Text = info.OrderRcver;
            tbTel.Text = info.OrderComm;
            tbAddress.Text = info.AddressText;

            tborderTime.Text = info.OrderDateTime.ToString();
            tbTotalPrice.Text = (info.OrderSums - info.SendFee).ToString();

            tbremark.Text = info.OrderAttach;
            tbpromotion.Text = info.promotionmoney.ToString();
            this.lblpaymoney.Text = info.paymoney.ToString();

            tbUserId.Text = info.UserId.ToString();
            Textstatr.Text = info.SendTime.ToShortTimeString() + "";
            flag = info.OrderStatus.ToString() + "";
            tbsendfree.Text = info.SendFee.ToString();

            WebUtility.SelectValue(ddlPaymode, info.paymode.ToString());
            WebUtility.SelectValue(ddlPaystate, info.paystate.ToString());


            //
            if (info.OrderAddress != "")
            {
                tbadminname.Text = info.OrderAddress;

            }
            else
            {
                tradminname.Style.Add("display", "none");
            }

            tbsendstate.Text = WebUtility.TurnOrderSendState(info.sendstate.ToString(), info.IsShopSet.ToString());

            SqlWhere = "OrderComm ='" + info.OrderComm.ToString() + "' and custorder.unid <> " + OrderId + " ";

            this.rtpOrderlist.DataSource = bll.GetListFix(5, 1, SqlWhere, "OrderDateTime", 1);

            this.rtpOrderlist.DataBind();
            lbstatus.InnerHtml = "订单操作<font color='red'>(当前状态：" + WebUtility.TurnOrderState(info.OrderStatus.ToString()) + ")</font>";

            WebUtility.SelectValue(ddlOrderState, info.OrderStatus.ToString());

            tbReveInt1.Text = info.ReveInt1.ToString();

            TextBoxto.Text = info.OrderSums.ToString();
            WebUtility.SelectValue(ddleatytpe, info.ReveInt2.ToString());
            WebUtility.SelectValue(ddlsource, info.fromweb);

            IList<FoodlistInfo> food_list = new List<FoodlistInfo>();
            food_list = orderfood_bll.GetAllByOrderID(info.orderid);
            rptFoodlist.DataSource = food_list;
            rptFoodlist.DataBind();

            string discountmsg = "无折扣";
            if (info.OldStatus < 100)
            {
                discountmsg = "会员折扣：" + info.OldStatus + "折";
            }
            lbdiscount.InnerHtml = discountmsg;

            tboldprice.Text = info.OldPrice.ToString();
            tbshopdiscountmoney.Text = info.shopdiscountmoney.ToString();
            WebUtility.SelectValue(ddlIsShopSet, info.IsShopSet.ToString());
            lbshopnotice.InnerText = "商家说明：" + info.OrderAddrEx;
            tbcardpay.Text = info.cardpay.ToString();

            IList<CustorderInfo> orders = new List<CustorderInfo>();
            orders.Add(info);

            WebUtility.BindRepeater(rptorder, orders);
            tbpakcage.Text = info.Packagefee.ToString();

            if (info.paymode == 1 || info.paymode == 5)
            {
                IList<PayOrderLogInfo> payrecord = new PayOrderLog().GetList(5, 1, "OrderId = '" + info.orderid + "' and state=2", "AddTime", 1);
                if (payrecord.Count > 0)
                {
                    string payids = "";
                    foreach (var item in payrecord)
                    {
                        payids += item.Batch + ",";
                    }
                    lbpayorderid.InnerHtml = "支付流水号：" + WebUtility.dellast(payids);
                }
            }

        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        CustorderInfo info = bll.GetModel(HjNetHelper.GetQueryInt("id", 0));

        info.OrderAttach = delmark(tbremark.Text);
        info.AddressText = WebUtility.InputText(tbAddress.Text);
        info.OrderStatus = Convert.ToInt32(ddlOrderState.SelectedValue);

        info.SendFee = Convert.ToDecimal(this.tbsendfree.Text);
        info.OrderSums = info.OldPrice + info.SendFee;
        info.paystate = Convert.ToInt32(ddlPaystate.SelectedValue);

        //判断权限
        int _rs = WebUtility.checkOperator(3);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }

        bll.AddOrderRecord(info.orderid, info.OrderStatus, UserHelp.GetAdmin().AdminName, "管理员修改订单信息:订单详细界面");

        if (bll.Update(info) > 0)
        {
            bll.UpdataState(HjNetHelper.GetQueryInt("id", 0), UserHelp.GetAdmin().AdminName);
            if (info.OrderStatus == 3 && flag != "3")
            {
                bll.AddPoint(info.orderid);

            }
            if (info.OrderStatus == 5 && flag != "5")
            {
                int ret = bll.UpdataState(info.orderid, info.OrderStatus);
                if (ret > 0)
                {
                    new OrderStep().Add(new OrderStepInfo()
                    {
                        stepcode = 100,
                        title = "平台取消订单",
                        subtitle = "",
                        addtime = DateTime.Now,
                        orderid = info.orderid,
                        revevar1 = "",
                        revevar2 = "0",
                        revevar3 = "0"

                    });
                }

            }
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('更新订单','text:更新订单信息成功','250','150','true','','true','text')");
            BindData(info.Unid.ToString());

            {
                NoticeHelper notice = new NoticeHelper(Context, info.deliverid.ToString());
                notice.sendOrderByDeliveryid();
            }
            {
                NoticeHelper notice = new NoticeHelper(Context, info.TogoId.ToString());
                notice.send2ShopByShopid();
            }
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('更新订单','text:更新订单信息失败','250','150','true','','true','text')");
        }
    }

    public string gethurry(string oid)
    {
        hurryorderInfo model = new Hangjing.SQLServerDAL.hurryorder().GetModel(oid);
        string rs = "";
        if (model.oid != null)
        {
            rs = "<font color='red'>" + model.Ccount + "(" + model.addtime + ")</font>";
        }
        else
        {
            rs = "无";
        }
        return rs;
    }

    /// <summary>
    /// 过滤标点
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    protected string delmark(string str)
    {
        string rs = Regex.Replace(str, @"[,.!@#$%^&\|~\?<>()_+';*\(\)_+\]\[]", "");
        return rs;
    }

    protected IList<FoodlistInfo> getproduct(object oid)
    {
        return orderfood_bll.GetAllByOrderID(oid.ToString());
    }
}

