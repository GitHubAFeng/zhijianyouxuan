using System;
using System.Collections.Generic;
namespace Hangjing.Model
{
    /// <summary>
    /// 实体类ETogoOrder 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class simpleorderInfo
    {
        private string _orderid;
        private string _username;
        private string _address;
        private string _togoname;
        private string _tel;

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }

        /// <summary>
        /// 订单编号(当前时间自动生成)
        /// </summary>
        public string OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }

        /// <summary>
        /// 用户真实名称(收件人)
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        /// <summary>
        /// 送餐地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }

        /// <summary>
        /// 商家名称
        /// 2010-03-15 8:47 by jijunjian
        /// </summary>
        public string TogoName
        {
            set { _togoname = value; }
            get { return _togoname; }
        }
        
    }
}

