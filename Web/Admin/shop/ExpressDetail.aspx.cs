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
using Hangjing.Control;

/// <summary>
/// 跑腿订单信息管理
/// </summary>
public partial class qy_54tss_Admin_Sale_OrderDetailExpressDetail : System.Web.UI.Page
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
    /// <summary>
    /// 城市编号
    /// </summary>
    protected int cityid
    {
        get
        {
            object o = ViewState["cityid"];
            return (o == null) ? 0 : Convert.ToInt32(o);
        }
        set
        {
            ViewState["cityid"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!IsPostBack)
        {
            hidLat.Value = SectionProxyData.GetSetValue(4);
            hidLng.Value = SectionProxyData.GetSetValue(5);

            //编辑
            string OrderId = HjNetHelper.GetQueryString("id");
            BindData(OrderId);
            pageType.Text = "更新订单信息";
            tr_ordertime.Style["display"] = "";
        }
    }

    Hangjing.SQLServerDAL.ExpressOrder bll = new Hangjing.SQLServerDAL.ExpressOrder();


    protected void BindData(string OrderId)
    {
        ExpressOrderInfo info = new ExpressOrderInfo();
        info = bll.GetModel(Convert.ToInt32(OrderId));

        if (info != null)
        {
            // WebUtility.BindList("dataid", "title", SectionProxyData.GetDeliverSiteList().Where(a => a.cityid == info.Cityid).ToList(), ddlbid);
            WebUtility.BindList("dataid", "name", new Deliver().GetList(1000, 1, "Inve1=" + info.Cityid, "dataid", 1).ToList(), ddlInve1);

            WebUtility.SelectValue(ddlOrderState, info.State.ToString());
            flag = info.State + "";

            tbUserName.Text = info.UserName;
            tbTel.Text = info.Tel;
            tbSentTime.Text = info.SentTime;
            tbAddress.Text = info.Address;
            tborderTime.Text = info.orderTime.ToString();
            tbremark.Text = info.Remark;
            //tbOorderid.Text = info.Oorderid;
            //tbInve1.Value = info.Inve1.ToString();
            WebUtility.SelectValue(ddlInve1, info.Inve1.ToString());
            //WebUtility.SelectValue(ddlbid, info.bid.ToString());

            tbsendmoney.Text = Convert.ToInt32(info.sendmoney).ToString();
            //tbbid.Value = info.bid.ToString();
            tbInve2.Text = info.Inve2;
            tbcallmsg.Text = info.callmsg;
            tbReveVar.Text = info.ReveVar;
            tbOorderid.Text = info.Oorderid;
            tborderid.Text = info.OrderID;


            string cityname = "";
            cityid = info.Cityid;
            CityInfo cinfo = new City().GetModel(cityid);
            if (cinfo != null)
            {
                cityname = cinfo.cname;
            }
            else
            {
                cityname = SectionProxyData.GetSetValue(25);
            }

            hfcityname.Value = cityname;
            hfcityid.Value = cityid.ToString();

            hfState.Value = info.State.ToString();
            hflatlng.Value = info.sitelat;
            hfdeliverid.Value = info.Inve1.ToString();
            hforderid.Value = info.OrderID;
            hfusername.Value = info.callmsg;
            hfaddress.Value = info.Oorderid;
            hfshopname.Value = info.UserName;
            latlng.InnerHtml = info.sitelat;

        }
    }

    protected void BindFoodData()
    {

    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        ExpressOrderInfo info = bll.GetModel(HjNetHelper.GetQueryInt("id", 0));
        info = bll.GetModel(info.DataID);
        info.UserName = WebUtility.InputText(tbUserName.Text);
        info.Tel = WebUtility.InputText(tbTel.Text);
        info.SentTime = WebUtility.InputText(tbSentTime.Text);
        info.Address = WebUtility.InputText(tbAddress.Text);
        info.State = WebUtility.InputText(ddlOrderState.SelectedValue, true);
        info.TogoID = 0;
        //info.OrderID = "";151202150918966188
        info.Currentprice = 0;
        info.Remark = WebUtility.InputText(tbremark.Text);
        info.Oorderid = WebUtility.InputText(tbOorderid.Text);
        info.writer = UserHelp.GetAdmin().AdminName;
        info.PayMode = 0;
        info.paytime = Convert.ToDateTime("1970-1-1");
        info.paystate = 0;
        info.paymoney = 0;
        info.PayOrderId = "";
        info.Inve1 = Convert.ToInt32(ddlInve1.SelectedValue);  //WebUtility.InputText(tbInve1.Value, true);
        info.Inve2 = UserHelp.GetAdmin().ID.ToString();
        info.sid = 0;
        info.bid = 0;// Convert.ToInt32(ddlbid.SelectedValue);
        info.tempcode = "";
        info.sendmoney = WebUtility.InputText(tbsendmoney.Text, true);
        info.ordersource = 0;
        info.ulat = "";
        info.ulng = "";
        info.shoplat = "";
        info.shoplng = "";
        info.sitelat = "";
        info.sitelng = "";
        info.ordertype = 3;
        info.noaccess = 0;
        info.validateCode = 0;
        info.iscancel = 0;
        info.ReveInt1 = 0;
        info.ReveInt2 = 0;
        info.ReveVar = "";
        info.IsTimeLimit = 0;
        info.TotalPrice = info.sendmoney;

        info.Inve2 = WebUtility.InputText(tbInve2.Text);
        info.callmsg = WebUtility.InputText(tbcallmsg.Text);
        info.ReveVar = WebUtility.InputText(tbReveVar.Text);
        info.Oorderid = WebUtility.InputText(tbOorderid.Text);

  


        if (info.DataID == 0)
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);hideload_super();");
                return;
            }
            if (bll.Add(info) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('新增成功','success','true',5);");
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('新增失败','error','true',5);");
            }
        }
        else
        {
            //判断权限
            int _rs = WebUtility.checkOperator(3);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);");
                return;
            }
            if (bll.Update(info) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('编辑成功','success','true',5);");
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('编辑失败','error','true',5);");
            }
        }
    }

}
