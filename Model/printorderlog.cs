using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Hangjing.Model
{
    /// <summary>
    /// 订单打印记录表，用于查询状态
    /// </summary>
    public class printorderlogInfo
    {
        private int _pid;
        /// <summary>
        /// pid
        /// </summary>
        public int pid
        {
            get { return _pid; }
            set { _pid = value; }
        }

        private string _orderid;
        /// <summary>
        /// 订单编号
        /// </summary>
        public string orderid
        {
            get { return _orderid; }
            set { _orderid = value; }
        }

        private string _membercode;
        /// <summary>
        /// 商户编码
        /// </summary>
        public string memberCode
        {
            get { return _membercode; }
            set { _membercode = value; }
        }

        private string _securitykey;
        /// <summary>
        /// API 密钥
        /// </summary>
        public string securityKey
        {
            get { return _securitykey; }
            set { _securitykey = value; }
        }

        private int _printstate;
        /// <summary>
        /// 打印状态，根据网站上的状态，为0的状态要重新更是定时查询,222表示已经删除
        /// </summary>
        public int printstate
        {
            get { return _printstate; }
            set { _printstate = value; }
        }

        private DateTime _addtime;
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }

        private DateTime _updatetime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime updatetime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }

        private int _reveint1;
        /// <summary>
        /// 未用
        /// </summary>
        public int reveint1
        {
            get { return _reveint1; }
            set { _reveint1 = value; }
        }

        private int _reveint2;
        /// <summary>
        /// 未用
        /// </summary>
        public int reveint2
        {
            get { return _reveint2; }
            set { _reveint2 = value; }
        }

        private string _revevar1;
        /// <summary>
        /// 未用
        /// </summary>
        public string revevar1
        {
            get { return _revevar1; }
            set { _revevar1 = value; }
        }

        private string _revevar2;
        /// <summary>
        /// 未用
        /// </summary>
        public string revevar2
        {
            get { return _revevar2; }
            set { _revevar2 = value; }
        }

    }
}

