using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 用户充值记录表
    /// </summary>
    [Serializable]
    class UserCancellogInfo
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
        /// 
        /// </summary>
        public decimal AddMoney
        {
            set { _addmoney = value; }
            get { return _addmoney; }
        }

        /// <summary>
        /// 商家状态0同意1拒绝
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }

        /// <summary>
        /// 管理员处理状态0未处理1已处理
        /// </summary>
        public int PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PayDate
        {
            set { _paydate = value; }
            get { return _paydate; }
        }

        /// <summary>
        /// 
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
        /// 操作管理员编号
        /// </summary>
        public int Inve1
        {
            set { _inve1 = value; }
            get { return _inve1; }
        }

        /// <summary>
        /// 未用
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
