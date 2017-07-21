#region license
/*****************************************
*CopyRight (c) 2009-2013 HangJing Teconology. All Rights Reserved.
*Function :
*Created by jijunjian at 2013/9/16 16:18:56.
*E-Mail: jijunjian@ihangjing.com
*****************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Hangjing.Common
{
    /// <summary>
    /// 订单来源（用户来源也用此类型）
    /// </summary>
    public enum OrderSource : int
    {
        [Description("网站")]
        web = 0,//网站

        [Description("android")]
        android = 2,

        [Description("iphone")]
        iphone = 3,

        [Description("客服")]
        CallCenter = 6,

        [Description("门店")]
        ShopCenter = 8,

        [Description("微信")]
        weixin = 7


    }



    /// <summary>
    /// 支付方式
    /// </summary>
    public enum OrderPayModel : int
    {
        [Description("余额支付")]
        Account = 3,
        [Description("支付宝")]
        alipay = 1,//支付宝
        [Description("货到付款")]
        PayonDelivery = 4,
        [Description("微信支付")]
        WeChat = 5
    }


}
