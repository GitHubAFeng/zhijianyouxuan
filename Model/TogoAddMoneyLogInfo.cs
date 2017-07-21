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
    /// 商家充值记录表
    /// </summary>
    [Serializable]
    public class TogoAddMoneyLogInfo
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

        /// <summary>
        /// 编号
        /// </summary>
        public int DataId
        {
            set { _dataid = value; }
            get { return _dataid; }
        }

        /// <summary>
        /// 商家编号
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
        /// 管理员编号
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }

        /// <summary>
        /// 支付类型：0后台转入/7订单结算/1:提现记录
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
        /// 支付状态 1：充值成功 0：进行中,2：拒绝（只针对提现）,3：商家取消（只针对提现）
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
        /// 备注
        /// </summary>
        public string Inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }

        /// <summary>
        /// 管理员
        /// </summary>
        public string admin
        {
            set;
            get;
        }

    }
}
