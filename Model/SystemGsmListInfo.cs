using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 查看商家发送邮件、短信记录表
    /// </summary>
    [Serializable]
    public class SystemGsmListInfo
    {
        private int _dataid;
        private int _togoid;
        private DateTime _sentTime;
        private DateTime _adddate;
        private int _sum;
        private int _senttype;
        private string _content;
        private string _useridlist;
        private int _status;
        private int _inve1;
        private string _inve2;
        private string _TogoName;
        private decimal _delmoney;

        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal DelMoney
        {
            set { _delmoney = value; }
            get { return _delmoney; }
        }

        public string TogoName
        {
            get { return _TogoName; }
            set { _TogoName = value; }
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
        public int TogoId
        {
            set { _togoid = value; }
            get { return _togoid; }
        }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SentTime
        {
            set { _sentTime = value; }
            get { return _sentTime; }
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
        /// 发送数量
        /// </summary>
        public int Sum
        {
            set { _sum = value; }
            get { return _sum; }
        }

        /// <summary>
        /// 类型 1:短信 2：邮件
        /// </summary>
        public int SentType
        {
            set { _senttype = value; }
            get { return _senttype; }
        }

        /// <summary>
        /// 具体内容
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }

        /// <summary>
        /// 用户ID列表
        /// </summary>
        public string UserIdList
        {
            set { _useridlist = value; }
            get { return _useridlist; }
        }

        /// <summary>
        /// 状态 0：新增 1:进行中 2：完成 3：失败
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
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
