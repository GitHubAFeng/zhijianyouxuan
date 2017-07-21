#region license
/*****************************************
*CopyRight (c) 2009-2013 HangJing Teconology. All Rights Reserved.
*Function :
*Created by jijunjian at 2013/11/21 22:21:58.
*E-Mail: jijunjian@ihangjing.com
*****************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

using Com.Alipay;
using System.Collections.Specialized;

/// <summary>
/// 手机网站支付同步回调界面
/// </summary>
public partial class Alipay_wappayreturn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Dictionary<string, string> sPara = GetRequestGet();

        if (sPara.Count > 0)//判断是否有带返回参数
        {
            Notify aliNotify = new Notify();
            bool verifyResult = aliNotify.VerifyReturn(sPara, Request.QueryString["sign"]);

            if (verifyResult)//验证成功
            {
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //请在这里加上商户的业务逻辑程序代码


                //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表

                //商户订单号
                string out_trade_no = Request.QueryString["out_trade_no"];

                //支付宝交易号
                string trade_no = Request.QueryString["trade_no"];

                //交易状态
                string result = Request.QueryString["result"];
                //支付金额
                string total_fee = Request.QueryString["total_fee"];
                    
                //判断是否在商户网站中已经做过了这次通知返回的处理
                //如果没有做过处理，那么执行商户的业务程序
                //如果有做过处理，那么不执行商户的业务程序

                string porderid = out_trade_no;

                Redirect(porderid, "", total_fee);

                //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            else//验证失败
            {
                Response.Write("支付失败:验证失败");
            }
        }
        else
        {
            Response.Write("支付失败:无返回参数");
        }


    }


    /// <summary>
    /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
    /// </summary>
    /// <returns>request回来的信息组成的数组</returns>
    public Dictionary<string, string> GetRequestGet()
    {
        int i = 0;
        Dictionary<string, string> sArray = new Dictionary<string, string>();
        NameValueCollection coll;
        //Load Form variables into NameValueCollection variable.
        coll = Request.QueryString;

        // Get names of all forms into a string array.
        String[] requestItem = coll.AllKeys;

        for (i = 0; i < requestItem.Length; i++)
        {
            sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
        }

        return sArray;
    }

    private void Redirect(string orderID, string subJect, string price)
    {
        string urlParam = "?id=" + orderID + "&price=" + price;
        string show_url = "/PaySuccess.aspx" + urlParam;
        HJlog.toLog("show_url=" + show_url);
        Response.Redirect(show_url);
    }
}
