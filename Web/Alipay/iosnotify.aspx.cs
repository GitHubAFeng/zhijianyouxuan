using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Xml;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Com.Alipay;
using Hangjing.WebCommon;

/// <summary>
/// 支付宝，支付成功后，更新支付状态，金额
/// </summary>
public partial class Alipayresult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SortedDictionary<string, string> sPara = GetRequestPost();

        if (sPara.Count > 0)//判断是否有带返回参数
        {

            #region 测试 2015-11-25 
            //string sp = "";
            //foreach (var item in sPara)
            //{
            //    sp += "key=" + item.Key + " value=" + item.Value;
            //}
            //Hangjing.AppLog.AppLog.Info("sPara：" + sp);
            //Hangjing.AppLog.AppLog.Info("notify_id=" + Request.Form["notify_id"] + "  sign=" + Request.Form["sign"]);
            //Hangjing.AppLog.AppLog.Info("out_trade_no=" + Request.Form["out_trade_no"] + "  trade_no=" + Request.Form["trade_no"]);
            //Hangjing.AppLog.AppLog.Info("trade_status=" + Request.Form["trade_status"] + "  total_fee=" + Request.Form["total_fee"]); 
            #endregion

            Notify aliNotify = new Notify();
            bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);
            if (verifyResult)//验证成功
            {

                //商户订单号
                string out_trade_no = Request.Form["out_trade_no"];

                //支付宝交易号
                string trade_no = Request.Form["trade_no"];

                //交易状态
                string trade_status = Request.Form["trade_status"];
                string strPrice = Request.Form["total_fee"];     //金额

                Hangjing.AppLog.AppLog.Info("app支付里面日志订单编号：" + trade_no);


                if (Request.Form["trade_status"] == "TRADE_FINISHED")
                {
                    Pay(out_trade_no, strPrice);
                }
                else if (Request.Form["trade_status"] == "TRADE_SUCCESS")
                {
                    Pay(out_trade_no, strPrice);
                }
                else
                {
                }

                //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——
                Hangjing.AppLog.AppLog.Info("APP支付宝支付：success");

                Response.Write("success");  //请不要修改或删除

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            else//验证失败
            {
                Hangjing.AppLog.AppLog.Info("APP支付宝支付：fail");
                Response.Write("fail");
            }
        }
        else
        {
            Hangjing.AppLog.AppLog.Info("APP支付宝支付：无通知参数");
            Response.Write("无通知参数");
        }
    }

    /// <summary>
    /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
    /// </summary>
    /// <returns>request回来的信息组成的数组</returns>
    public SortedDictionary<string, string> GetRequestPost()
    {
        int i = 0;
        SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
        NameValueCollection coll;
        //Load Form variables into NameValueCollection variable.
        coll = Request.Form;

        // Get names of all forms into a string array.
        String[] requestItem = coll.AllKeys;

        for (i = 0; i < requestItem.Length; i++)
        {
            sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
        }

        return sArray;
    }

    /// <summary>
    /// 支付
    /// </summary>
    /// <param name="orderid"></param>
    /// <param name="price"></param>
    protected void Pay(string payorderid, string price)
    {
        onlinepayCallback paycallback = new onlinepayCallback(payorderid, Convert.ToDecimal(price), (int)OrderPayModel.alipay);
        paycallback.Handle();
    }

}
