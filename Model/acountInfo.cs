/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : $codebesideclassname$
 * Function : 
 * Created by jijunjian at 2010-11-16 21:46:36.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hangjing.Model;

namespace Hangjing.Model
{
    /// <summary>
    /// 在线支付实体
    /// </summary>
    public class acountInfo
    {
        private int _dataid;
        private string _ali_seller_mail;
        private string _ali_key;
        private string _ali_partner;
        private string _sxy_partner;
        private string _sxy_key;
        private string _ali_notify_url;
        private string _ali_return_url;
        private string _reve1;
        private string _reve2;
        private string _reve3;

        /// <summary>
        /// 1表示支付宝
        /// </summary>
        public int DataID
        {
            set { _dataid = value; }
            get { return _dataid; }
        }
        /// <summary>
        ///  支付宝表示：帐号
        /// </summary>
        public string Ali_Seller_Mail
        {
            set { _ali_seller_mail = value; }
            get { return _ali_seller_mail; }
        }
        /// <summary>
        /// 交易安全校验码（key）
        /// </summary>
        public string Ali_Key
        {
            set { _ali_key = value; }
            get { return _ali_key; }
        }
        /// <summary>
        /// 合作伙伴编号(Partnerid)
        /// </summary>
        public string Ali_Partner
        {
            set { _ali_partner = value; }
            get { return _ali_partner; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sxy_Partner
        {
            set { _sxy_partner = value; }
            get { return _sxy_partner; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sxy_Key
        {
            set { _sxy_key = value; }
            get { return _sxy_key; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ALI_NOTIFY_URL
        {
            set { _ali_notify_url = value; }
            get { return _ali_notify_url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ALI_RETURN_URL
        {
            set { _ali_return_url = value; }
            get { return _ali_return_url; }
        }

        /// <summary>
        /// 是否起用,0表示是，1表示不起用
        /// </summary>
        public string Reve1
        {
            set { _reve1 = value; }
            get { return _reve1; }
        }
        /// <summary>
        /// 名称：支付宝
        /// </summary>
        public string Reve2
        {
            set { _reve2 = value; }
            get { return _reve2; }
        }

        /// <summary>
        /// 商户的私钥
        /// </summary>
        public string Reve3
        {
            set { _reve3 = value; }
            get { return _reve3; }
        }
    }
}
