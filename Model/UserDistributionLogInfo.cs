/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * Created by wanghui at 2011-5-12 9:03:30.
 * E-Mail   : wanghui@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 用户分销记录表
    /// </summary>
    [Serializable]
    public class UserDistributionLogInfo
    {
        private int _dataid;
        private int _userid;
        private decimal _addmoney;
        private int _state;
        private int _paytype;
        private DateTime _paydate;
        private int _paystate;
        private DateTime _adddate;
        private int _inve1;
        private string _inve2;
        private string _togoName;

        public string TogoName
        {
            get { return _togoName; }
            set { _togoName = value; }
        }

        private string _AdminName;

        public string AdminName
        {
            get { return _AdminName; }
            set { _AdminName = value; }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int DataId
        {
            set { _dataid = value; }
            get { return _dataid; }
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }

        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal AddMoney
        {
            set { _addmoney = value; }
            get { return _addmoney; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }

        /// <summary>
        /// 0 表示 正常，1表示区域代理商佣金
        /// </summary>
        public int PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayDate
        {
            set { _paydate = value; }
            get { return _paydate; }
        }

        /// <summary>
        /// 支付状态：0：新增充值 1：充值成功 -1：充值失败
        /// </summary>
        public int PayState
        {
            set { _paystate = value; }
            get { return _paystate; }
        }

        /// <summary>
        /// 新增时间
        /// </summary>
        public DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }

        /// <summary>
        /// 属性扩展字段
        /// </summary>
        public int Inve1
        {
            set { _inve1 = value; }
            get { return _inve1; }
        }

        /// <summary>
        /// 订单编号/流水号
        /// </summary>
        public string Inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }

        private string _UserName;
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            set { _UserName = value; }
            get { return _UserName; }
        }
    }
}
