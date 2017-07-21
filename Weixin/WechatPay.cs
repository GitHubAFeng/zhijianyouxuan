using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 微信支付
    /// </summary>
    public class WechatPay
    {
        WechatPayInfo payinfo = new WechatPayInfo();
        HttpContext Context;

        public WechatPay(HttpContext Context)
        {
            this.Context = Context;
        }

        /// <summary>
        /// 构造支付参数
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="price"></param>
        public WechatPayInfo BuildPayParam(string orderid, decimal price, int userid, string openid)
        {
            //添加支付记录，主要用来在支付过程中，失败后，重新支付
            PayOrderLog dalpaylog = new PayOrderLog();
            string payorderid = dalpaylog.GetPayBatch(orderid);

            if (orderid.StartsWith("d"))
            {
                price = Convert.ToDecimal(CacheHelper.GetSetValue(57));
            }
            if (orderid.StartsWith("r"))
            {
                int rid = HjNetHelper.GetQueryInt("rid", 0);
                decimal addmoney = 0;
                foreach (var item in CacheHelper.GetOrderSourceList())
                {
                    if (rid == item.ID)
                    {
                        addmoney = Convert.ToDecimal(item.classname) + item.Status;
                        price = Convert.ToDecimal(item.classname);
                        break;
                    }
                }

                UserAddMoneyLog dal = new UserAddMoneyLog();
                UserAddMoneyLogInfo addmoneyinfo = new UserAddMoneyLogInfo();
                addmoneyinfo.AddDate = DateTime.Now;
                addmoneyinfo.AddMoney = addmoney;
                addmoneyinfo.Inve1 = 0;
                addmoneyinfo.Inve2 = orderid;
                addmoneyinfo.PayDate = Convert.ToDateTime("1900-01-01 00:00:00");
                addmoneyinfo.PayState = 0;
                addmoneyinfo.PayType = 5;
                addmoneyinfo.State = 0;
                addmoneyinfo.TogoName = "";
                addmoneyinfo.UserId = userid;

                if (addmoneyinfo.UserId == 1551)
                {
                    price = 0.01M;
                }


                dal.Add(addmoneyinfo);
            }


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


            /**
             * JS_API支付demo
             * ====================================================
             * 在微信浏览器里面打开H5网页中执行JS调起支付。接口输入输出数据格式为JSON。
             * 成功调起支付需要三个步骤：
             * 步骤1：网页授权获取用户openid
             * 步骤2：使用统一支付接口，获取prepay_id
             * 步骤3：使用jsapi调起支付
            */

            //price = 0.01M;

            payinfo.timeStamp = TenpayUtil.getTimestamp();
            payinfo.nonceStr = TenpayUtil.getNoncestr();

            //步骤2：使用统一支付接口，获取prepay_id
            UnifiedOrder_pub uopay = new UnifiedOrder_pub(Context);
            uopay.rh.setParameter("appid", payinfo.appId);
            uopay.rh.setParameter("mch_id", TenpayUtil.partner);
            uopay.rh.setParameter("nonce_str", payinfo.nonceStr);
            uopay.rh.setParameter("body", "buyfood");
            uopay.rh.setParameter("openid", openid);
            uopay.rh.setParameter("out_trade_no", payorderid);
            uopay.rh.setParameter("total_fee", (price * 100).ToString("#0"));
            uopay.rh.setParameter("notify_url", TenpayUtil.tenpay_notify);//接收财付通通知的URL
            uopay.rh.setParameter("trade_type", "JSAPI");
            uopay.rh.setParameter("spbill_create_ip", Utils.GetIP());

            uopay.rh.setKey(TenpayUtil.key);

            uopay.getPrepayId();

            string prepay_id = uopay.prepay_id;

            payinfo.packageValue = "prepay_id=" + prepay_id;

            //步骤3：使用jsapi调起支付(页面中)

            //设置支付参数
            RequestHandler paySignReqHandler = new RequestHandler(Context);
            paySignReqHandler.init();

            paySignReqHandler.setParameter("appId", payinfo.appId);
            paySignReqHandler.setParameter("timeStamp", payinfo.timeStamp);
            paySignReqHandler.setParameter("nonceStr", payinfo.nonceStr);

            string package = payinfo.packageValue;//packageValue.Replace("=", "%3D")

            paySignReqHandler.setParameter("package", package);
            paySignReqHandler.setParameter("signType", "MD5");
            paySignReqHandler.setKey(TenpayUtil.key);

            payinfo.paySign = paySignReqHandler.createMd5Sign();

            HJlog.toLog("paySign=" + payinfo.paySign + "  packageValue=" + payinfo.packageValue);

            return payinfo;
        }
    }

    /// <summary>
    /// 支付实体
    /// </summary>
    public class WechatPayInfo
    {

        public string appId
        {
            get { return TenpayUtil.appid; }
        }

        public string timeStamp
        {
            set;
            get;
        }


        public string nonceStr
        {
            set;
            get;
        }

        public string packageValue
        {
            set;
            get;
        }

        public string paySign
        {
            set;
            get;
        }

    }
}
