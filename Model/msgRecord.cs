using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 短信发送记录
    /// </summary>
    public class msgRecordInfo
    {
        private int _ranid;
        private string _orderid;
        private DateTime _adddate;
        private string _contents;
        private int _inve1;
        private string _inve2;
        private string _inve3;
        private string _inve4;

        /// <summary>
        /// 
        /// </summary>
        public int RanId
        {
            set { _ranid = value; }
            get { return _ranid; }
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
        }

        /// <summary>
        /// 0表示普通短信，1表示催单
        /// </summary>
        public int Inve1
        {
            set { _inve1 = value; }
            get { return _inve1; }
        }

        /// <summary>
        /// 短信接口返回内容;按窝窝提供的文档中的
        /// </summary>
        public string Inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }

        /// <summary>
        /// ip
        /// </summary>
        public string Inve3
        {
            set { _inve3 = value; }
            get { return _inve3; }
        }

        /// <summary>
        /// 催单表示订单编号，普通为空
        /// </summary>
        public string Inve4
        {
            set { _inve4 = value; }
            get { return _inve4; }
        }
    }
}
