using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Hangjing.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TogoInfo
    {
        private int _dataid;
        private string _picture;
        private string _togoname;
        private string _address;
        private string _linkman;
        private string _tel;
        private string _qq;
        private string _msn;
        private string _email;
        private string _url;
        private string _fetion;
        private string _introduce;
        private DateTime _intime;
        private DateTime _time1start;
        private DateTime _time1end;
        private string _ebuilding;
        private int _status;
        private string _remark;
        private int _grade;
        private int _isdelete;
        private int _sortnum;
        private int _star;
        private int _haveprinter;
        private int _point;
        private int _flavorgrade;
        private int _servicegrade;
        private int _speedgrade;
        private int _category;
        private int _viewtimes;
        private string _banner1;
        private string _banner2;
        private int _senttime;
        private int _minmoney;
        private int _sentmoney;
        private string _sentorg;
        private string _special;
        private int _reviewtimes;
        private decimal _money;
        private int _inve1;
        private string _inve2;

        private int _menunum;
        private int _showlocal;
        private int _showpicture;

        private TogoLoginInfo _logininfo = null;  //对应的管理在Togo中
        private ETogoOpinionInfo _togoOpinionInfo;//饭店列表中的评论.
        private int _optioncount;                 //饭店评论的条数.

        private DateTime _foodupdatetime;         //菜谱更新时间

        private DateTime _time2start;
        private DateTime _time2end;
        private DateTime _bisnessstart;
        private DateTime _bisnessend;

        private int _isbisness;

        /// <summary>
        /// 是否营业通过营业时间还判断的。1表示是，0表示没有营业
        /// </summary>
        public int isbisness
        {
            set { _isbisness = value; }
            get { return _isbisness; }
        }

        public DateTime Foodupdatetime
        {
            set { _foodupdatetime = value; }
            get { return _foodupdatetime; }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int DataID
        {
            set { _dataid = value; }
            get { return _dataid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Picture
        {
            set { _picture = value; }
            get { return _picture; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TogoName
        {
            set { _togoname = value; }
            get { return _togoname; }
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
        [JsonProperty]
        public string LinkMan
        {
            set { _linkman = value; }
            get { return _linkman; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Msn
        {
            set { _msn = value; }
            get { return _msn; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string EMail
        {
            set { _email = value; }
            get { return _email; }
        }

        /// <summary>
        /// 商家网址
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }

        /// <summary>
        /// 商家飞信号码
        /// </summary>
        public string Fetion
        {
            set { _fetion = value; }
            get { return _fetion; }
        }

        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduce
        {
            set { _introduce = value; }
            get { return _introduce; }
        }

        /// <summary>
        /// 加入系统的时间
        /// </summary>
        public DateTime InTime
        {
            set { _intime = value; }
            get { return _intime; }
        }

        /// <summary>
        /// 送餐时间1开始
        /// </summary>
        public DateTime Time1Start
        {
            set { _time1start = value; }
            get { return _time1start; }
        }

        /// <summary>
        /// 送餐时间1结束
        /// </summary>
        public DateTime Time1End
        {
            set { _time1end = value; }
            get { return _time1end; }
        }

        /// <summary>
        /// 配送范围
        /// </summary>
        public string EBuilding
        {
            set { _ebuilding = value; }
            get { return _ebuilding; }
        }

        /// <summary>
        /// 是否营业（1：营业 / 0：不营业）
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        /// <summary>
        /// 商家等级（管理员添加时手动添加）1.签约商家 , 2非签约商家
        /// </summary>
        public int Grade
        {
            set { _grade = value; }
            get { return _grade; }
        }

        /// <summary>
        /// 是否删除(1:删除/0:未删除)
        /// </summary>
        public int IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }

        /// <summary>
        /// 排序序号
        /// </summary>
        public int SortNum
        {
            set { _sortnum = value; }
            get { return _sortnum; }
        }

        /// <summary>
        ///  是否审核 0表示未审核，1表示审核
        /// </summary>
        public int Star
        {
            set { _star = value; }
            get { return _star; }
        }

        /// <summary>
        /// 是否有打印机(1：有 0：无),在给商家添加或者删除打印机时更新次字段
        /// </summary>
        public int HavePrinter
        {
            set { _haveprinter = value; }
            get { return _haveprinter; }
        }

        /// <summary>
        /// 佣金比例 计算商家佣金时使用 订单价格*佣金比例＝需要支付给的佣金
        /// </summary>
        public int Point
        {
            set { _point = value; }
            get { return _point; }
        }

        /// <summary>
        /// 用户评价时选择的口味分平均
        /// </summary>
        public int FlavorGrade
        {
            set { _flavorgrade = value; }
            get { return _flavorgrade; }
        }

        /// <summary>
        /// 用户评价时选择的服务分平均
        /// </summary>
        public int ServiceGrade
        {
            set { _servicegrade = value; }
            get { return _servicegrade; }
        }

        /// <summary>
        /// 用户评价时选择的速度平均
        /// </summary>
        public int SpeedGrade
        {
            set { _speedgrade = value; }
            get { return _speedgrade; }
        }

        /// <summary>
        /// 商家人气  该餐馆的订单数量×2+页面浏览量注意：页面浏览要改，下订单后要修改
        /// </summary>
        public int category
        {
            set { _category = value; }
            get { return _category; }
        }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ViewTimes
        {
            set { _viewtimes = value; }
            get { return _viewtimes; }
        }

        /// <summary>
        /// 商品金额
        /// </summary>
        [JsonProperty]
        public string banner1
        {
            set { _banner1 = value; }
            get { return _banner1; }
        }

        /// <summary>
        /// 店铺招牌广告2 (暂未使用)
        /// </summary>
        public string banner2
        {
            set { _banner2 = value; }
            get { return _banner2; }
        }

        /// <summary>
        /// 配送时间
        /// </summary>
        public int senttime
        {
            set { _senttime = value; }
            get { return _senttime; }
        }

        /// <summary>
        /// 最低起送价格
        /// </summary>
        public int minmoney
        {
            set { _minmoney = value; }
            get { return _minmoney; }
        }

        /// <summary>
        /// 配送费用
        /// </summary>
        public int sentmoney
        {
            set { _sentmoney = value; }
            get { return _sentmoney; }
        }

        /// <summary>
        /// 配送机构 统一配送/配送机构名称
        /// </summary>
        public string sentorg
        {
            set { _sentorg = value; }
            get { return _sentorg; }
        }

        /// <summary>
        /// 本店特色
        /// </summary>
        public string special
        {
            set { _special = value; }
            get { return _special; }
        }

        /// <summary>
        /// 评价次数（计算评价均分时有用）
        /// </summary>
        public int reviewtimes
        {
            set { _reviewtimes = value; }
            get { return _reviewtimes; }
        }

        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal money
        {
            set { _money = value; }
            get { return _money; }
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
        /// 商家分类，可多选
        /// </summary>
        public string Inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }

        /// <summary>
        /// 商家的登入信息
        /// </summary>
        public TogoLoginInfo LoginInfo
        {
            set { _logininfo = value; }
            get { return _logininfo; }
        }

        /// <summary>
        /// 饭店列表中的评论.
        /// </summary>
        public ETogoOpinionInfo TogoOpinionInfo
        {
            set
            {
                _togoOpinionInfo = value;
            }
            get
            {
                return _togoOpinionInfo;
            }
        }

        /// <summary>
        /// 饭店列表中的评论数.
        /// </summary>
        public int OpinionCount
        {
            set
            {
                _optioncount = value;
            }
            get
            {
                return _optioncount;
            }
        }

        /// <summary>
        /// 显示的菜单数量 0表示展示全部
        /// </summary>
        public int menunum
        {
            set { _menunum = value; }
            get { return _menunum; }
        }

        /// <summary>
        /// 是否显示餐馆位置(地图)
        /// </summary>
        public int showlocal
        {
            set { _showlocal = value; }
            get { return _showlocal; }
        }

        /// <summary>
        /// 是否显示图片
        /// </summary>
        public int showpicture
        {
            set { _showpicture = value; }
            get { return _showpicture; }
        }

        /// <summary>
        /// 送餐时间2开始
        /// </summary>
        public DateTime Time2Start
        {
            set { _time2start = value; }
            get { return _time2start; }
        }
        /// <summary>
        /// 送餐时间2结束
        /// </summary>
        public DateTime Time2End
        {
            set { _time2end = value; }
            get { return _time2end; }
        }
        /// <summary>
        /// 外卖订餐时间开始
        /// </summary>
        public DateTime bisnessStart
        {
            set { _bisnessstart = value; }
            get { return _bisnessstart; }
        }
        /// <summary>
        /// 外卖订餐时间结束
        /// </summary>
        public DateTime bisnessend
        {
            set { _bisnessend = value; }
            get { return _bisnessend; }
        }

        /// <summary>
        /// 订单量
        /// </summary>
        [JsonProperty]
        public int allcount
        {
            set;
            get;
        }

        /// <summary>
        /// 营业额
        /// </summary>
        [JsonProperty]
        public decimal allprice
        {
            set;
            get;
        }

        /// <summary>
        /// 支付商家的钱
        /// </summary>
        public decimal ShopHaveMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 收入
        /// </summary>
        public decimal Shopprofit
        {
            get;
            set;
        }

        /// <summary>
        ///  应收款 
        /// </summary>
        public decimal getmoney
        {
            get;
            set;
        }

        /// <summary>
        /// 促销优惠金额
        /// </summary>
        public decimal promotionmoney { get; set; }

        /// <summary>
        /// 在线支付金额
        /// </summary>
        public decimal paymoney { get; set; }

        /// <summary>
        /// 货到付款
        /// </summary>
        public decimal payamount { get; set; }

        /// <summary>
        /// 优惠卷支付
        /// </summary>
        public decimal cardpay { get; set; }

        /// <summary>
        /// 打包费
        /// </summary>
        public decimal packagefee { get; set; }

        /// <summary>
        /// 配送员应上缴款
        /// </summary>
        [JsonProperty]
        public decimal deliverpayweb
        {
            get;
            set;
        }

        /// <summary>
        /// 总工资
        /// </summary>
        [JsonProperty]
        public decimal allwage
        {
            get;
            set;
        }

        /// <summary>
        /// 基本工资
        /// </summary>
        [JsonProperty]
        public decimal basewage
        {
            get;
            set;
        }

        /// <summary>
        /// 提成比例
        /// </summary
        [JsonProperty]
        public decimal percentagewage
        {
            get;
            set;
        }


        /// <summary>
        /// 配送费
        /// </summary>
        public decimal SendFee
        {
            get;
            set;
        }
    }

}
