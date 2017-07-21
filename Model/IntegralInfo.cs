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
    /// 实体类Integral 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class IntegralInfo
    {
        private int _integralid;
        private string _custid;
        private string _payintegral;
        private int _detailid;
        private DateTime _cdate;
        private string _state;
        private int _giftsid;
        private string _address;
        private string _phone;
        private string _person;
        private string _date;
        private string _username;
        private string _giftname;
        private string _remark;

        /// <summary>
        /// 
        /// </summary>
        public int IntegralId
        {
            set { _integralid = value; }
            get { return _integralid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CustId
        {
            set { _custid = value; }
            get { return _custid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PayIntegral
        {
            set { _payintegral = value; }
            get { return _payintegral; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DetailId
        {
            set { _detailid = value; }
            get { return _detailid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Cdate
        {
            set { _cdate = value; }
            get { return _cdate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int GiftsId
        {
            set { _giftsid = value; }
            get { return _giftsid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Person
        {
            set { _person = value; }
            get { return _person; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Date
        {
            set { _date = value; }
            get { return _date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string GiftName
        {
            get { return _giftname; }
            set { _giftname = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

    }
}
