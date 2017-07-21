using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 支付日志表
    /// </summary>
    public class PayOrderLogInfo
    {
        private Guid _dataid;
        private string _orderid;
        private DateTime _addtime;
        private string _batch;
        private decimal _price;
        private int _paytype;
        private DateTime _paytime;
        private int _state;
        private DateTime _paycalltime;
        private string _remark;
        private string _reve1;
        private string _reve2;

        /// <summary>
        /// 唯一编号
        /// </summary>
        public Guid DataId
        {
            set { _dataid = value; }
            get { return _dataid; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 交易流水号(获取当前的支付流水号（订单编号+001）：订单+(000-999)的)
        /// </summary>
        public string Batch
        {
            set { _batch = value; }
            get { return _batch; }
        }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 是否进行退款：0表示没有，1开始退款，2退款成功
        /// </summary>
        public int PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        /// <summary>
        /// 支付时间(从支付宝，返回时再保存)
        /// </summary>
        public DateTime PayTime
        {
            set { _paytime = value; }
            get { return _paytime; }
        }
        /// <summary>
        /// 支付状态：0表示未支付（最早的提交的状态），1 提交后返回成功（AliReturn.aspx修改），2支付成功（GoAliNotify.aspx这个页面处理），-1提交后返回失败（AliReturn.aspx修改），-2支付失败（GoAliNotify.aspx这个页面处理）
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 支付回调时间
        /// </summary>
        public DateTime PayCallTime
        {
            set { _paycalltime = value; }
            get { return _paycalltime; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string Reve1
        {
            set { _reve1 = value; }
            get { return _reve1; }
        }
        /// <summary>
        /// 支付宝交易号（AliReturn.aspx修改）
        /// </summary>
        public string Reve2
        {
            set { _reve2 = value; }
            get { return _reve2; }
        }

        private string _RefundRecordNo;
        /// <summary>
        /// 退款支付宝流水号
        /// </summary>
        public string RefundRecordNo
        {
            set { _RefundRecordNo = value; }
            get { return _RefundRecordNo; }
        }
    }
}
