using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tenpayApp;
using System.Text;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Weixin;
using Hangjing.WebCommon;

namespace Html5.weixinpay
{
    /// <summary>
    /// 微信支付接口处理回调示例，商户按照此示例进行开发即可
    /// 注：通知参数是xml形式
    /// </summary>
    public partial class payNotifyUrl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            ResponseHandler resHandler = new ResponseHandler(Context);
            resHandler.init();

            //判断签名
            if (resHandler.isTenpaySign())
            {
                string openid = resHandler.getPostDataParameter("openid");

                //取结果参数做业务处理(注意，订单号)
                string out_trade_no = resHandler.getPostDataParameter("out_trade_no");
                //财付通订单号
                string transaction_id = resHandler.getPostDataParameter("transaction_id");
                //金额,以分为单位
                string total_fee = resHandler.getPostDataParameter("total_fee");
                //支付结果
                string return_code = resHandler.getPostDataParameter("return_code");
                //系统中的支付订单号
                string payorderid = out_trade_no;

                Hangjing.AppLog.AppLog.Info("支付通知接口： out_trade_no=" + out_trade_no + " \r\n transaction_id=" + transaction_id + " \r\n total_fee=" + total_fee + " \r\n trade_state=" + return_code + " \r\n discount=" + 0 + " \r\n payorderid=" + payorderid);

                //成功标志
                if ("SUCCESS".Equals(return_code))
                {
                    //------------------------------
                    //处理业务开始
                    //------------------------------


                    onlinepayCallback paycallback = new onlinepayCallback(out_trade_no, Convert.ToDecimal(total_fee) / 100, (int)OrderPayModel.WeChat);
                    paycallback.Handle();

                    //回复服务器处理成功
                    Response.Write("SUCCESS");
                    Response.End();
                }
                else
                {
                    Response.Write("支付失败");
                }
                //回复服务器处理成功
                Response.Write("success");
                //delivernotify(transaction_id, out_trade_no,openid);
            }

            else
            {//md5签名失败
                Response.Write("fail -md5 failed");
                Response.Write(resHandler.getDebugInfo());
            }
        }

        /// <summary>
        /// 发货通知:支付发布时，要至少一个订单调用发发货通知接口
        /// </summary>
        public void delivernotify(string transid, string out_trade_no, string openid)
        {
            string url = "https://api.weixin.qq.com/pay/delivernotify?access_token=" + WebUtility.GetConfigKey("weixin_access_token");

            TimeSpan toNow = DateTime.Now.AddHours(12).Subtract(new DateTime(1970, 1, 1));
            string deliver_timestamp = toNow.TotalSeconds.ToString("#0");

            // string openid = "oTi_Kt5WtEA1TphrktHCFCVKZIdU";

            //设置支付参数
            RequestHandler delivernotifyReqHandler = new RequestHandler(Context);
            delivernotifyReqHandler.setParameter("appid", TenpayUtil.appid);
            // delivernotifyReqHandler.setParameter("appkey", TenpayUtil.appkey);
            delivernotifyReqHandler.setParameter("openid", openid);
            delivernotifyReqHandler.setParameter("transid", transid);
            delivernotifyReqHandler.setParameter("out_trade_no", out_trade_no);
            delivernotifyReqHandler.setParameter("deliver_timestamp", deliver_timestamp);
            delivernotifyReqHandler.setParameter("deliver_status", "1");
            delivernotifyReqHandler.setParameter("deliver_msg", "ok");
            string app_signature = delivernotifyReqHandler.createSHA1Sign();

            Hangjing.AppLog.AppLog.Debug("发货通知签名：" + delivernotifyReqHandler.getDebugInfo());

            //发货通知接口post数据
            StringBuilder postdata = new StringBuilder("{");
            postdata.Append("\"appid\" : \"" + TenpayUtil.appid + "\",");
            postdata.Append("\"openid\" : \"" + openid + "\",");
            postdata.Append("\"transid\" : \"" + transid + "\",");
            postdata.Append("\"out_trade_no\" : \"" + out_trade_no + "\",");
            postdata.Append("\"deliver_timestamp\" : \"" + deliver_timestamp + "\",");
            postdata.Append("\"deliver_status\" : \"1\",");
            postdata.Append("\"deliver_msg\" : \"ok\",");
            postdata.Append("\"app_signature\" : \"" + app_signature + "\",");
            postdata.Append("\"sign_method\" : \"sha1\"");
            postdata.Append("}");

            Hangjing.Weixin.HttpHelper objhttp = new Hangjing.Weixin.HttpHelper();
            objhttp.isToLower = false;
            Hangjing.Weixin.HttpItem objHttpItem = new Hangjing.Weixin.HttpItem()
            {
                URL = url,
                Encoding = "utf-8",
                Method = "POST",
                UserAgent = Context.Request.UserAgent,
                Postdata = postdata.ToString(),
            };
            string ResultMsg = objhttp.GetHtml(objHttpItem);

            Hangjing.AppLog.AppLog.Info("发货通知：" + ResultMsg + "\r\n postdata=" + postdata);
        }
    }
}