/*********************************************************************
 * CopyRight (c) 2009-2014 HangJing Teconology. All Rights Reserved.
 * FileName : 
 * Function : 常量类，用于保存一些固定的参数
 * Created by jijunjian at 2014-06-13 16:33:21.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Hangjing.Common
{
    /// <summary>
    /// 常量类，用于保存一些固定的参数
    /// </summary>
    public class Constant
    {
        /// <summary>
        /// 商家排序方式
        /// </summary>
        public static string shopsortname = "Status desc, havenew desc, sortnum";

        /// <summary>
        /// 订单自动调度 给所有人，群编号变成8888，所有接口中获取订单时，除了要获取本组的，还要获取为8888的
        /// </summary>
        public static string biggid = "8888";


        /// <summary>
        /// StateConfig 表中支付方式数据对应的 PrrentID
        /// </summary>
        public static int PaymentMethodPrrentID = 10;

        /// <summary>
        /// 骑士app离线时间判断阀值（分钟）
        /// </summary>
        public static int OffLineDoor = 3;
    }
}
