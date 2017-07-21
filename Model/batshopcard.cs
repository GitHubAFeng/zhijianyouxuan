using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 优惠券批次实体
	/// </summary>
	[Serializable]
    public class batshopcardInfo
	{
        private int _dataid;
        private string _title;
        private string _batnum;
        private DateTime _adddate;
        private int _cantimes;
        private decimal _point;
        private int _inve1;
        private string _inve2;
        private int _cardcount;
        private string _contents;
        private string _adminname;
        private int _sortnum;
        private int _canusecount;
        private int _num;


        /// <summary>
        /// 转换json用到
        /// </summary>
        public int num
        {
            set { _num = value; }
            get { return _num; }
        }

        /// <summary>
        /// 能用张数
        /// </summary>
        public int canusecount
        {
            set { _canusecount = value; }
            get { return _canusecount; }
        }

        /// <summary>
        /// 排序（降序）
        /// </summary>
        public int sortnum
        {
            set { _sortnum = value; }
            get { return _sortnum; }
        }

        /// <summary>
        /// 管理员名称
        /// </summary>
        public string AdminName
        {
            set { _adminname = value; }
            get { return _adminname; }
        }

        /// <summary>
        /// 说明
        /// </summary>
        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DataID
        {
            set { _dataid = value; }
            get { return _dataid; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public string batnum
        {
            set { _batnum = value; }
            get { return _batnum; }
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
        /// 未用
        /// </summary>
        public int cantimes
        {
            set { _cantimes = value; }
            get { return _cantimes; }
        }
       
        /// <summary>
        /// 表示限制类型：1->现金折扣（满多少优惠多少）;2->百分比折扣（满多少享受折扣多少）;3->多倍积分（满多少享受）
        /// </summary>
        public int Inve1
        {
            set { _inve1 = value; }
            get { return _inve1; }
        }


        /// <summary>
        /// Inve1=1时，表示优惠的现金;Inve1=2时，表示享受的折扣（88折输入88）;Inve1=3时，表示享受的积分倍数
        /// </summary>
        public decimal point
        {
            set { _point = value; }
            get { return _point; }
        }

        /// <summary>
        /// 图片
        /// </summary>
        public string Inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }
        /// <summary>
        /// 生成充值卡数量
        /// </summary>
        public int CardCount
        {
            set { _cardcount = value; }
            get { return _cardcount; }
        }

        private DateTime _starttime;
        /// <summary>
        /// 当有时间限制时，开始时间
        /// </summary>
        public DateTime starttime
        {
            get { return _starttime; }
            set { _starttime = value; }
        }

        private DateTime _endtime;
        /// <summary>
        /// 当有时间限制时，结束时间
        /// </summary>
        public DateTime endtime
        {
            get { return _endtime; }
            set { _endtime = value; }
        }

        private int _mtype;
        /// <summary>
        /// 类型：1积分兑换券 2电子优惠券
        /// 提示：积分兑换券表示用户用积分在网站兑换;电子优惠券表示是直接发送到用户用户手机或者邮箱里的券，不在前台显示，这种券需要通过上传excel生成券号
        /// </summary>
        public int mtype
        {
            get { return _mtype; }
            set { _mtype = value; }
        }

        private int _mydiscount;
        /// <summary>
        /// mtype=1,为兑换所需积分.否则为无效
        /// </summary>
        public int mydiscount
        {
            get { return _mydiscount; }
            set { _mydiscount = value; }
        }

        private string _foossortids;
        /// <summary>
        ///未用
        /// </summary>
        public string foossortids
        {
            get { return _foossortids; }
            set { _foossortids = value; }
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

        private int _moneylimity;
        /// <summary>
        /// 未用
        /// </summary>
        public int moneylimity
        {
            get { return _moneylimity; }
            set { _moneylimity = value; }
        }

        private int _moneyline;
        /// <summary>
        /// moneylimity 为0时，多少元才可用
        /// </summary>
        public int moneyline
        {
            get { return _moneyline; }
            set { _moneyline = value; }
        }

        private string _togoname;
        /// <summary>
        /// 商家名称
        /// </summary>
        public string TogoName
        {
            set { _togoname = value; }
            get { return _togoname; }
        }
	}
}

