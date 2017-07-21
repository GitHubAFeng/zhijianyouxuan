#region license
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :
// Created by jijunjian at 2010-4-30 14:39:50.
// E-Mail: jijunjian@ihangjing.com
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    [Serializable]
    public class ROrderinfo
    {
        private string orderid;
        private decimal currentprice;
        private int _togoid;

        public int togoid
        {
            set
            {
                _togoid = value;
            }
            get
            {
                return _togoid;
            }
        }

        public string Orderid
        {
            set
            {
                orderid = value;
            }
            get
            {
                return orderid;
            }
        }
        /// <summary>
        /// 当前用户还要支付的金额
        /// </summary>
        public decimal Currentprice
        {
            set
            {
                currentprice = value;
            }
            get
            {
                return currentprice;
            }
        }

        private decimal _allprice;
        /// <summary>
        /// 总金额=原菜品费+原送餐费
        /// </summary>
        public decimal allprice
        {
            set
            {
                _allprice = value;
            }
            get
            {
                return _allprice;
            }
        }

        private string payorderid;
        /// <summary>
        /// 支付中提交給支付接口的订单编号（同时多个订单的情况）
        /// </summary>
        public string PayOrderId
        {
            set { payorderid = value; }
            get { return payorderid; }
        }

        private decimal _accountpay;
        /// <summary>
        /// 余额支付金额
        /// </summary>
        public decimal accountpay
        {
            set
            {
                _accountpay = value;
            }
            get
            {
                return _accountpay;
            }
        }

        /// <summary>
        /// 送餐费
        /// </summary>
        public decimal sendfee
        {
            set;
            get;
        }

        /// <summary>
        /// 商家对应微信中的openid
        /// </summary>
        public string WeiXxinOpenID
        {
            set;
            get;
        }

        /// <summary>
        /// 优惠支付金额
        /// </summary>
        public decimal cardpay
        {
            set;
            get;
        }


        /// <summary>
        /// 促销优惠金额
        /// </summary>
        public decimal promotionmoney
        {
            set;
            get;
        }

        /// <summary>
        ///  配送方式，根据商家设置的生成  配送方式,0表示统一配送，1表示商家自送
        /// </summary>
        public string sentorg
        {
            set;
            get;
        }

        /// <summary>
        ///  用户，商家坐标
        /// </summary>
        public string latlng
        {
            set;
            get;
        }

        /// <summary>
        ///  城市编号
        /// </summary>
        public int cityid
        {
            set;
            get;
        }

        /// <summary>
        /// 支付类型 （1支付宝/2银联/3账户余额/4货到付款/5微信支付）
        /// </summary>
        public int paymode { get; set; }

        /// <summary>
        /// 支付结果  0 未支付 1 成功
        /// </summary>
        public int paystate { get; set; }

        /// <summary>
        /// 自动接单  0：否， 1：是
        /// </summary>
        public int isAutoReceiveOrder { get; set; }
    }
}
