using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Hangjing.Model
{

    /// <summary>
    /// 订单步骤记录表
    /// </summary>
    public class OrderStepInfo
    {

        /// <summary>
        /// 编号
        /// </summary>
        public int sId
        {
            get;
            set;
        }

        /// <summary>
        /// 步骤编号:从0（下单），10（支付），20（商家接），30（配送员接），40（取货），50（送货），60（配送员完成），70(商家完成),80,商家拒绝，90，确认收货 100，用户取消 105 商家同意用户取消  108 商家拒绝用户取消
        /// </summary>
        public int stepcode
        {
            get;
            set;
        }

        /// <summary>
        /// orderid
        /// </summary>
        public string orderid
        {
            get;
            set;
        }

        /// <summary>
        /// 步骤标题
        /// </summary>
        public string title
        {
            get;
            set;
        }

        /// <summary>
        /// 步骤副标题
        /// </summary>
        public string subtitle
        {
            get;
            set;
        }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime addtime
        {
            get;
            set;
        }

        /// <summary>
        /// 配送员编号
        /// </summary>
        public int deliverid
        {
            get;
            set;
        }

        /// <summary>
        /// reveint1
        /// </summary>
        public int reveint1
        {
            get;
            set;
        }

        /// <summary>
        /// reveint2
        /// </summary>
        public int reveint2
        {
            get;
            set;
        }

        /// <summary>
        /// 骑士电话
        /// </summary>
        public string revevar1
        {
            get;
            set;
        }

        /// <summary>
        /// 经度
        /// </summary>
        public string revevar2
        {
            get;
            set;
        }
        /// <summary>
        /// 纬度
        /// </summary>
        public string revevar3
        {
            get;
            set;
        }

    }
}

