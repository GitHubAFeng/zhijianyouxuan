using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web.Script.Serialization;
using Hangjing.Common;
using Hangjing.WebCommon;

/// <summary>
/// 用户APP提交跑腿订单
/// </summary>
public partial class App_Android_express : UserAPPPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        string ret = " {\"orderstate\":\"2\" } ";
        //ordermodel={ "Inve2": "外卖", "callmsg": "测试姓名", "ReveVar": "15268170323", "Oorderid": "杭州市江干区学正街547号金沙居1239",  "SentTime": "2015-06-17 09:50:00","PayMode": 4, "Remark": "备注","ulat":"30.314395","ulng":"120.390334","shopid":"889","Tel":"15268170323","UserName":"zhu","Address":"金沙居","shoplat":"30.414395","shoplng":"120.490334","userid":"1" }
        string jsonstring = Request["ordermodel"];

        if (string.IsNullOrEmpty(jsonstring))
        {
            base.res.msg = "参数ordermodel不完整";
            res.state = 0;
            ShowJsonData();
            Response.End();
            return;
        }

        ExpressOrder dal = new ExpressOrder();

        ExpressOrderInfo infoaddress = new JavaScriptSerializer().Deserialize<ExpressOrderInfo>(jsonstring);

        SendfeeInfo snfo = new SendfeeInfo();
        snfo.sendtime = Convert.ToDateTime(infoaddress.SentTime);

        snfo.latlng = new latlnginfo();
        snfo.latlng.ulat = infoaddress.ulat;
        snfo.latlng.ulng = infoaddress.ulng;
        snfo.latlng.slat = infoaddress.shoplat;
        snfo.latlng.slng = infoaddress.shoplng;

        //ExpressSendFee fee = new ExpressSendFee(snfo);
        //SendInfo model = fee.getSendFee();




        ExpressOrderInfo info = new ExpressOrderInfo();


        info.UserName = infoaddress.UserName;
        info.Tel = infoaddress.Tel;
        info.Address = infoaddress.Address;
        info.UserID = 0;
        info.CustomerName = "";
        info.Inve2 = infoaddress.Inve2;
        info.callmsg = infoaddress.callmsg;
        info.ReveVar = infoaddress.ReveVar;
        info.Oorderid = infoaddress.Oorderid;
        info.SentTime = infoaddress.SentTime;
        info.Remark = infoaddress.Remark;
        info.PayMode = infoaddress.PayMode;
        info.paytime = Convert.ToDateTime("1970-1-1");
        info.paystate = 0;
        //info.Currentprice = model.Distance;
        //info.sendmoney = model.sendmoney;
        info.Currentprice = 0;
        info.sendmoney = 0;
        info.ulat = infoaddress.ulat;
        info.ulng = infoaddress.ulng;
        info.shoplat = infoaddress.shoplat;
        info.shoplng = infoaddress.shoplng;
        info.tempcode = "";
        string orderid = DateTime.Now.ToString("yMMddHHmmss") + WebUtility.GetRandomOnlyNum(4) + info.Tel.Substring(info.Tel.Length - 2, 2);
        info.OrderID = orderid;
        info.State = 0;
        info.orderTime = DateTime.Now;
        info.SetStateTime = Convert.ToDateTime("1970-1-1");
        info.Inve1 = 0;
        info.bid = 0;
        info.writer = "";
        info.TotalPrice = info.sendmoney;
        info.Cityid = infoaddress.Cityid;
        info.sid = 0;
        info.ordersource = infoaddress.ordersource;
        info.callcount = 0;
        info.paymoney = 0;
        info.TogoID = 0;
        info.isaddpoint = 0;
        info.sendtype = 0;
        info.sitelat = "{'ulat':'" + info.ulat + "','ulng':'" + info.ulng + "','slat':'" + info.shoplat + "','slng':'" + info.shoplng + "'}";
        info.sitelng = "";
        info.ordertype = 0;
        info.noaccess = 0;
        info.validateCode = 0;
        info.iscancel = 0;
        info.ReveInt2 = 0;
        info.ReveDate1 = Convert.ToDateTime("1970-1-1");
        info.ReveDate2 = Convert.ToDateTime("1970-1-1");
        info.IsTimeLimit = 0;
        info.ReveInt1 = 0;
        info.servename = "";
        info.Cityid = infoaddress.Cityid;
        info.PayOrderId = orderid;
        info.UserID = infoaddress.UserID;

        if (info.UserID > 0 && info.PayMode == 3)
        {
            ECustomerInfo einfo = new ECustomer().GetModel(info.UserID);
            if (einfo != null)
            {
                info.CustomerName = einfo.Name;
                decimal useroney = einfo.Usermoney;
                if (useroney == 0)
                {

                    base.res.msg = "您的账户余额为0，不能选择账户余额支付";
                    res.state = 0;
                    ShowJsonData();
                    Response.End();
                    return;


                }
                //可以支付部分金额
                if (useroney < info.sendmoney)
                {
                    base.res.msg = "余额不足，请选择其他支付方式";
                    res.state = 0;
                    ShowJsonData();
                    Response.End();
                    return;
                }

                if (WebUtility.GetMd5(infoaddress.PayPassword) != einfo.PayPassword)
                {
                    base.res.msg = "支付密码错误，请重新输入";
                    res.state = 0;
                    ShowJsonData();
                    Response.End();
                    return;
                }

            }


        }

        IList<ROrderinfo> result = dal.submitorder(info);
        if (result.Count > 0)
        {
            IList<DeliverInfo> Deliverlist = new Deliver().GetList(999, 1, "IsApproved=0 AND Inve1 = " + info.Cityid, "DataId", 1);
            NoticeHelper notice = new NoticeHelper(HttpContext.Current);
            notice.send2All(info.Cityid);

            base.res.msg = "提交订单成功";
            res.state = 1;
            ShowJsonData();
            Response.End();
            return;
        }
        else
        {

            base.res.msg = "提交订单失败，请联系管理员";
            res.state = 0;
            ShowJsonData();
            Response.End();
            return;
        }

    }
}