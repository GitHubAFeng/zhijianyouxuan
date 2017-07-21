using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 优惠券使用记录
    /// </summary>
    public class ShopCardUserRecordInfo
    {
        private int _cid;
        private string _cardnum;
        private string _ckey;
        private DateTime _adddate;
        private int _state;
        private decimal _point;
        private int _batid;
        private int _canday;
        private int _inve1;
        private string _inve2;
        private string _title;
        private string _username;
        private int _userid;
        private decimal _cmoney;
        private int _reveint;
        private string _revevar;
        private int _isbuy;
        private int _buyuid;
        private DateTime _usergettime;
        private DateTime _buytime;

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }

        public string username
        {
            set { _username = value; }
            get { return _username; }
        }

        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CID
        {
            set { _cid = value; }
            get { return _cid; }
        }
        /// <summary>
        /// shopcard.cid
        /// </summary>
        public string cardnum
        {
            set { _cardnum = value; }
            get { return _cardnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ckey
        {
            set { _ckey = value; }
            get { return _ckey; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }
        /// <summary>
        ///  未用
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }


        /// <summary>
        /// 表示限制类型：1->现金折扣（满多少优惠多少）;2->百分比折扣（满多少享受折扣多少）;3->多倍积分（满多少享受）
        /// </summary>
        public int ReveInt
        {
            set { _reveint = value; }
            get { return _reveint; }
        }

        /// <summary>
        /// ReveInt=1时，表示优惠的现金;ReveInt=2时，表示享受的折扣（88折输入88）;ReveInt=3时，表示享受的积分倍数
        /// </summary>
        public decimal Point
        {
            set { _point = value; }
            get { return _point; }
        }



        /// <summary>
        /// 
        /// </summary>
        public int batid
        {
            set { _batid = value; }
            get { return _batid; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public int canday
        {
            set { _canday = value; }
            get { return _canday; }
        }
        /// <summary>
        /// 未用
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

        /// <summary>
        /// 未用
        /// </summary>
        public decimal cmoney
        {
            set { _cmoney = value; }
            get { return _cmoney; }
        }
        
        /// <summary>
        /// 订单编号
        /// </summary>
        public string ReveVar
        {
            set { _revevar = value; }
            get { return _revevar; }
        }

        /// <summary>
        /// 未用
        /// </summary>
        public int isbuy
        {
            set { _isbuy = value; }
            get { return _isbuy; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public int buyuid
        {
            set { _buyuid = value; }
            get { return _buyuid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime usergettime
        {
            set { _usergettime = value; }
            get { return _usergettime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime buytime
        {
            set { _buytime = value; }
            get { return _buytime; }
        }
    }
}
