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
    /// 用户消费记录表
    /// </summary>
    [Serializable]
    public class UserDelMoneyLogInfo
    {
        private int _dataid;
        private int _userid;
        private decimal _delmoney;
        private DateTime _adddate;
        private string _buyitem;
        private int _inve1;
        private string _inve2;
        private string _userName;
        private string _togoName;

        public string TogoName
        {
            get { return _togoName; }
            set { _togoName = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
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
        /// 消费金额
        /// </summary>
        public decimal DelMoney
        {
            set { _delmoney = value; }
            get { return _delmoney; }
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
        /// 消费名目
        /// </summary>
        public string BuyItem
        {
            set { _buyitem = value; }
            get { return _buyitem; }
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
        /// 属性扩展字段
        /// </summary>
        public string Inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }
    }
}
