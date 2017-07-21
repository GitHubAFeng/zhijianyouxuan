using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    ///优惠券
    /// </summary>
    public class ShopCardInfo
    {
        /// <summary>
        /// 绑定时间
        /// </summary>
        public DateTime usergettime
        {
            set;
            get;
        }


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
        /// 未用
        /// </summary>
        public string cardnum
        {
            set { _cardnum = value; }
            get { return _cardnum; }
        }
        /// <summary>
        /// 券号 12位
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
        ///  是否绑定 ： 0表示没有;1表示是
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        ///ReveInt=1时，表示优惠的现金;ReveInt=2时，表示享受的折扣（88折输入88）;ReveInt=3时，表示享受的积分倍数
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
        /// 管理员编号
        /// </summary>
        public int Inve1
        {
            set { _inve1 = value; }
            get { return _inve1; }
        }
        /// <summary>
        /// 是否激活：0表示没有，1表示激活；激活后才能使用
        /// </summary>
        public string Inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }

        /// <summary>
        /// 剩余金额
        /// </summary>
        public decimal cmoney
        {
            set { _cmoney = value; }
            get { return _cmoney; }
        }

        /// <summary>
        /// 类型：1积分兑换券 2电子优惠券
        /// 提示：积分兑换券表示用户用积分在网站兑换;电子优惠券表示是直接发送到用户用户手机或者邮箱里的券，不在前台显示，这种券需要通过上传excel生成券号
        /// </summary>
        public int ReveInt
        {
            set { _reveint = value; }
            get { return _reveint; }
        }

        /// <summary>
        /// 未用
        /// </summary>
        public string ReveVar
        {
            set { _revevar = value; }
            get { return _revevar; }
        }

        /// <summary>
        /// 是否被购买,0表示没有，1表示正在购买;2表示已经被购买
        /// </summary>
        public int isbuy
        {
            set { _isbuy = value; }
            get { return _isbuy; }
        }

        /// <summary>
        /// 购买用户编号(用户中心显示时要判断isbuy是否为1,0表示没有支付成功的，不算此用户的。)
        /// </summary>
        public int buyuid
        {
            set { _buyuid = value; }
            get { return _buyuid; }
        }

        private int _isused;
        /// <summary>
        /// 是否使用。1表示是。0表示没有
        /// </summary>
        public int isused
        {
            get { return _isused; }
            set { _isused = value; }
        }

        private int _timelimity;
        /// <summary>
        /// 未用
        /// </summary>
        public int timelimity
        {
            get { return _timelimity; }
            set { _timelimity = value; }
        }

        private DateTime _starttime;
        /// <summary>
        /// 未用
        /// </summary>
        public DateTime starttime
        {
            get { return _starttime; }
            set { _starttime = value; }
        }

        private DateTime _endtime;
        /// <summary>
        /// 未用
        /// </summary>
        public DateTime endtime
        {
            get { return _endtime; }
            set { _endtime = value; }
        }

        private int _moneylimity;
        /// <summary>
        /// 未用
        /// </summary>
        public int moneylimity
        {
            get { return _moneylimity; }
            set { _moneylimity = value; }
        }

        private decimal _moneyline;
        /// <summary>
        /// moneylimity 为0时，多少元才可用
        /// </summary>
        public decimal moneyline
        {
            get { return _moneyline; }
            set { _moneyline = value; }
        }

        private int _reveint1;
        /// <summary>
        /// 表示限制类型：1->现金折扣（满多少优惠多少）;2->百分比折扣（满多少享受折扣多少）;3->多倍积分（满多少享受）
        /// </summary>
        public int ReveInt1
        {
            get { return _reveint1; }
            set { _reveint1 = value; }
        }

        private string _revevar1;
        /// <summary>
        /// 未用
        /// </summary>
        public string ReveVar1
        {
            get { return _revevar1; }
            set { _revevar1 = value; }
        }
    }
}
