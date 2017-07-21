using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using System.Web.UI.HtmlControls;
using Hangjing.Control;
using Hangjing.DBUtility;
//using tenpayApp;

/// <summary>
/// 商家 跑腿订单
/// </summary>
public partial class shop_express : System.Web.UI.Page
{
    ExpressOrder dal = new ExpressOrder();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);

        if (!Page.IsPostBack)
        {
            //默认经纬度
            hidLat.Value = SectionProxyData.GetSetValue(4);
            hidLng.Value = SectionProxyData.GetSetValue(5);


            //得到商家经纬度
            ETogoLocalInfo infos = new ETogoLocal().GetInfoById(UserHelp.GetUser_Togo().Unid.ToString());
            this.hidflat.Value = infos.Lat;
            this.hidflng.Value = infos.Lng;


            List<string> times = new List<string>();
            DateTime now = DateTime.Now;
            now = now.AddHours(1);

            for (int hour = 0; hour <= 23; hour++)
            {
                times.Add(hour.ToString() + ":00");
                times.Add(hour.ToString() + ":30");
            }

            this.ddl_Time1.DataSource = times;
            this.ddl_Time1.DataBind();
            tbdate.Value = DateTime.Now.ToString("yyyy-MM-dd");

            //服务类型
            IList<STemplateInfo> list = SectionProxyData.GetExpressServeList();
            WebUtility.BindList("ID", "classname", list, ddlexpressserve);

        }
    }


    /// <summary>
    /// 提交跑腿订单
    /// </summary>
    public void btsave_Click(object sender, EventArgs e)
    {

        string usercode = WebUtility.FixgetCookie("shop_uc");
        if (usercode == null || usercode == "")
        {
            usercode = WebUtility.GetRandom(10);
            WebUtility.FixsetCookie("shop_uc", usercode, 1);
        }

        PointsInfo shop = UserHelp.GetUser_Togo();
        if (shop == null)
        {
            Response.Redirect("/tlogin.aspx");
        }


        ExpressOrderInfo info = new ExpressOrderInfo();
        info.TogoID = shop.Unid;
        info.CustomerName = "";

        //发件信息
        info.UserName = shop.CommPerson;
        info.Tel = shop.Comm; 
        info.Address = shop.Address;

        //收件信息
        info.callmsg = WebUtility.InputText(tbcallmsg.Value);
        info.ReveVar = WebUtility.InputText(tbReveVar.Value);
        info.Oorderid = WebUtility.InputText(tbOorderid.Value);

        //其他信息
        info.Inve2 = WebUtility.InputText(tbInve2.Value);//商品
        info.SentTime = WebUtility.InputText(tbdate.Value) + " " + ddl_Time1.SelectedValue;
        info.sendmoney = Convert.ToDecimal(WebUtility.InputText(hidsendfee.Value));
        info.PayMode = Convert.ToInt32(rbpaymode.SelectedValue);
        info.Remark = WebUtility.InputText(tbRemark.Value);

        info.ReveInt1 = Convert.ToInt32(ddlexpressserve.SelectedValue);
        info.servename = ddlexpressserve.SelectedItem.Text;


        info.tempcode = usercode;
        info.Cityid = shop.cityid;
        info.TotalPrice = info.sendmoney;
        info.Currentprice = info.sendmoney;

        info.ulat = hidtlat.Value;
        info.ulng = hidtlng.Value;
        //ETogoLocalInfo infos = new ETogoLocal().GetInfoById(shop.Unid.ToString());
        //info.shoplat = infos.Lat;
        //info.shoplng = infos.Lng;
        //这里获取最新的 商家可能拖动
        info.shoplat = hidflat.Value;
        info.shoplng = hidflng.Value;
        info.sitelat = "{'ulat':'" + info.ulat + "','ulng':'" + info.ulng + "','slat':'" + info.shoplat + "','slng':'" + info.shoplng + "'}";

        info.State = 0;
        info.orderTime = DateTime.Now;
        string orderid = DateTime.Now.ToString("yMMddHHmmss") + WebUtility.GetRandomOnlyNum(4) + info.Tel.Substring(info.Tel.Length - 2, 2);
        info.OrderID = orderid;
        info.SetStateTime = Convert.ToDateTime("1970-1-1");
        info.callcount = 0;
        info.writer = "";
        info.paytime = Convert.ToDateTime("1970-1-1");
        info.paystate = 0;
        info.paymoney = 0;
        string payorderid = "e" + DateTime.Now.ToString("yyMMddHHmmssff") + HJConvert.GetRandom(4);//生成支付订单编号
        info.PayOrderId = payorderid;
        info.Inve1 = 0;
        info.sid = 0;
        info.bid = 0;
        info.ordersource = 0;
        info.isaddpoint = 0;
        info.sendtype = 0;
        info.sitelng = "";
        info.ordertype = 0;
        info.noaccess = 0;
        info.validateCode = 0;
        info.iscancel = 0;
        info.ReveInt2 = 1;//后期修改 默认为已接收 2015-12-11 
        info.ReveDate1 = Convert.ToDateTime("1970-1-1");
        info.ReveDate2 = Convert.ToDateTime("1970-1-1");
        info.IsTimeLimit = 0;



        IList<ROrderinfo> mylist = dal.submitorder(info);
        if (mylist != null)
        {
            Session["expressorderinfo"] = mylist;

            if (info.PayMode == 1)
            {
                string show_url = WebUtility.GetConfigsite() + "/index.aspx";
                AliPayInfo alipa = new AliPayInfo(payorderid, SectionProxyData.GetSetValue(7), SectionProxyData.GetSetValue(7), mylist[0].Currentprice.ToString(), show_url, "", "");
                AliPay.Pay(alipa);
            }
            else if (info.PayMode == 5)
            {
                // TenpayUtil tenpay = new TenpayUtil();
                // Response.Redirect(TenpayUtil.tenpay_show + "?porderid=" + mylist[0].PayOrderId + "&price=" + mylist[0].Currentprice.ToString() + "&productname=" + SectionProxyData.GetSetValue(7));      
            }
            else
            {
                AlertScript.RegScript(this.Page,  "$.jBox.prompt('订单提交成功，请耐心等待!', '提示', 'info', { closed: function () {  } });hideload_super();");
            }
        }
        else
        {

            AlertScript.RegScript(this.Page, "tipsWindown('提示信息','text:对不起，服务器繁忙，请与我们客服联系!','250','150','true','2000','true','text');hideload_super();;");
        }


    }

}
