using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Weixin;
using tenpayApp;

namespace Html5.weixinpay
{
    /// <summary>
    /// 原生态支付
    /// </summary>
    public partial class nativepay : System.Web.UI.Page
    {
        public String appId = TenpayUtil.appid;
        public String timeStamp = "";//时间戳
        public String nonceStr = "";//随机字符串
        public String packageValue = "";//订单详情扩展字符串
        public String paySign = "";//签名

        protected void Page_Load(object sender, EventArgs e)
        {
            string orderid = HjNetHelper.GetQueryString("orderid");
            decimal allmoney = Convert.ToDecimal(HjNetHelper.GetQueryString("price"));
            weixinPay(orderid, allmoney);
        }

        /// <summary>
        /// 微信支付
        /// </summary>
        /// <param name="payorderid"></param>
        protected void weixinPay(string orderid, decimal price)
        {
            //添加支付记录，主要用来在支付过程中，失败后，重新支付
            PayOrderLog dalpaylog = new PayOrderLog();
            string payorderid = dalpaylog.GetPayBatch(orderid); ;

            /*********************准备去支付 添加支付日志********************************/
            PayOrderLogInfo info = new PayOrderLogInfo();
            info.OrderId = orderid;
            info.AddTime = DateTime.Now;
            info.Batch = payorderid;
            info.Price = price;

            info.PayType = 0;
            info.PayTime = Convert.ToDateTime("1900-1-1");
            info.State = 0;
            info.PayCallTime = Convert.ToDateTime("1900-1-1");
            info.Remark = "";
            info.Reve1 = "5";
            info.Reve2 = "";
            dalpaylog.Add(info);
            /*********************添加支付日志 over********************************/


            timeStamp = TenpayUtil.getTimestamp();
            nonceStr = TenpayUtil.getNoncestr();

            //步骤2：使用统一支付接口，获取prepay_id
            UnifiedOrder_pub uopay = new UnifiedOrder_pub(Context);
            uopay.rh.setParameter("appid", appId);
            uopay.rh.setParameter("mch_id", TenpayUtil.partner);
            uopay.rh.setParameter("nonce_str", nonceStr);
            uopay.rh.setParameter("body", "buyfood");
            uopay.rh.setParameter("out_trade_no", payorderid);
            uopay.rh.setParameter("total_fee", (price * 100).ToString("#0"));	
            uopay.rh.setParameter("notify_url", TenpayUtil.tenpay_notify);//接收财付通通知的URL
            uopay.rh.setParameter("trade_type", "NATIVE");//
            uopay.rh.setParameter("spbill_create_ip", UserHelp.GetUserIP());

            uopay.rh.setKey(TenpayUtil.key);
            uopay.getPrepayId();

            string code_url = uopay.code_url;

            string sql = "UPDATE dbo.Custorder SET PayOrderId = '"+code_url+"' WHERE orderid = '"+orderid+"'";
            WebUtility.excutesql(sql);

            string url = WebUtility.GetMasterUrl() + "/weixinpay.aspx?orderid=" + orderid + "&price=" + price;
            Response.Redirect(url);
           
        }
    }
}