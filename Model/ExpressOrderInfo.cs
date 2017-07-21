using System;
using System.Collections.Generic;
namespace Hangjing.Model
{
    /// <summary>
    /// 跑腿订单
    /// </summary>
    [Serializable]
    public class ExpressOrderInfo
    {
        private int _dataid;
        private string _orderid;
        private int _userid;
        private string _username;
        private string _tel;
        private string _senttime;
        private string _address;
        private int _state;
        private int _togoid;
        private DateTime _ordertime;
        private decimal _totalprice;
        private DateTime _setstatetime;
        private decimal _currentprice;

        private int _foodcount;
        private int _ordercount;
        private decimal _ordertotal;

        private string _togoname;
        private string _customername;

        private string _Remark;
        private string _oorderid;
        private int _callcount;
        private string _callmsg;
        private string _writer;

        //支付部分
        private DateTime _paytime;
        private int _paystate;//0表示支付，1成功，-1失败
        private decimal _paymoney;
        private int _paymode;
        private string payorderid;
        private decimal _sendmoney;

        private int _Inve1;
        private int _sid;
        private string _inve2;
        private int _bid;
        private int _cityid;
        private string _tempcode;
        private string strPrintEnd;
        private int _addpoint;

        //后来新增的字段
        private int _ordersource;
        private int _isaddpoint;
        private int _sendtype;
        private string _ulat;
        private string _ulng;
        private string _shoplng;
        private string _shoplat;
        private string _sitelat;
        private string _sitelng;
        private int _ordertype;
        private int _noaccess;
        private int _iscancel;
        private int _validatecode;
        private int _reveint1;
        private int _reveint2;
        private string _revevar;
        private DateTime _revedate1;
        private DateTime _revedate2;
        private int _istimelimit;

        private string _payorderid;

        private string _servename;

        /// <summary>
        /// 服务类型名称
        /// </summary>		
        public string servename
        {
            get { return _servename; }
            set { _servename = value; }
        }


        private IList<FoodInOrderInfo> _foodlist;

        /// <summary>
        /// 菜品信息
        /// </summary>
        public IList<FoodInOrderInfo> foodlist
        {
            get { return _foodlist; }
            set { _foodlist = value; }
        }

        /// <summary>
        /// 支付编号
        /// </summary>		

        public string PayOrderId
        {
            get { return _payorderid; }
            set { _payorderid = value; }
        }

        /// <summary>
        /// 添加的积分
        /// </summary>
        public int addpoint
        {
            set { _addpoint = value; }
            get { return _addpoint; }
        }

        /// <summary>
        /// 打印结尾
        /// </summary>
        public string PrintEnd
        {
            get { return strPrintEnd; }
            set { strPrintEnd = value; }
        }

        /// <summary>
        /// 随机编码
        /// </summary>
        public string tempcode
        {
            set { _tempcode = value; }
            get { return _tempcode; }
        }

        /// <summary>
        /// 城市编号
        /// </summary>
        public int Cityid
        {
            set { _cityid = value; }
            get { return _cityid; }
        }

        /// <summary>
        /// 群主编号
        /// </summary>
        public int sid
        {
            set { _sid = value; }
            get { return _sid; }
        }

        /// <summary>
        /// 配送员编号
        /// </summary>
        public int Inve1
        {
            set { _Inve1 = value; }
            get { return _Inve1; }
        }


        /// <summary>
        /// 商品
        /// </summary>
        public string Inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }

        /// <summary>
        /// 配送点编号
        /// </summary>
        public int bid
        {
            set { _bid = value; }
            get { return _bid; }
        }

        /// <summary>
        /// 配送费
        /// </summary>
        public decimal sendmoney
        {
            set { _sendmoney = value; }
            get { return _sendmoney; }
        }


        /// <summary>
        /// 支付方式 1:支付宝支付 3:余额支付 2:发件人支付 4:收件人支付(货到付款) 5:财付通
        /// </summary>
        public int PayMode
        {
            set { _paymode = value; }
            get { return _paymode; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime paytime
        {
            set { _paytime = value; }
            get { return _paytime; }
        }

        /// <summary>
        /// 0表示未支付，1成功，-1失败
        /// </summary>
        public int paystate
        {
            set { _paystate = value; }
            get { return _paystate; }
        }

        /// <summary>
        /// 支付宝支付金额
        /// </summary>
        public decimal paymoney
        {
            set { _paymoney = value; }
            get { return _paymoney; }
        }

        /// <summary>
        /// 处理人
        /// </summary>
        public string writer
        {
            set { _writer = value; }
            get { return _writer; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _Remark = value; }
            get { return _Remark; }
        }


        /// <summary>
        /// 自增字段
        /// </summary>
        public int DataID
        {
            set { _dataid = value; }
            get { return _dataid; }
        }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }

        /// <summary>
        /// 发件联系人
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        /// <summary>
        /// 发件联系方式
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }

        /// <summary>
        /// 取件时间
        /// </summary>
        public string SentTime
        {
            set { _senttime = value; }
            get { return _senttime; }
        }

        /// <summary>
        ///发件联系人地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }

        /// <summary>
        /// 订单状态： 0 新增（待接单）;1已调度; 2取货中; 4 配送中;3 成功 ;5 取消,6：失败
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }

        /// <summary>
        /// 下订单时间
        /// </summary>
        public DateTime orderTime
        {
            set { _ordertime = value; }
            get { return _ordertime; }
        }

        /// <summary>
        /// 总费用 = 配送费
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }

        /// <summary>
        /// 订单统计数目
        /// </summary>
        public int OrderCount
        {
            get { return _ordercount; }
            set { _ordercount = value; }
        }

        /// <summary>
        /// 订单金额统计  （连接字段）
        /// </summary>
        public Decimal OrderTotal
        {
            get { return _ordertotal; }
            set { _ordertotal = value; }
        }

        /// <summary>
        /// 读取订单的时间并且修改状态
        /// 订单被扫描的时间（判断订单是否失效）
        /// </summary>
        public DateTime SetStateTime
        {
            get { return _setstatetime; }
            set { _setstatetime = value; }
        }

        /// <summary>
        /// 未用
        /// </summary>
        public int TogoID
        {
            set
            {
                _togoid = value;
            }
            get
            {
                return _togoid;
            }
        }

        /// <summary>
        ///用户还需支付的金额
        /// </summary>
        public decimal Currentprice
        {
            set
            {
                _currentprice = value;
            }
            get
            {
                return _currentprice;
            }
        }

        /// <summary>
        /// 未用
        /// </summary>
        public string TogoName
        {
            set { _togoname = value; }
            get { return _togoname; }
        }

        /// <summary>
        /// 未用
        /// </summary>
        public int FoodCount
        {
            get { return _foodcount; }
            set { _foodcount = value; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string CustomerName
        {
            get { return _customername; }
            set { _customername = value; }
        }
        /// <summary>
        /// 收件人地址
        /// </summary>
        public string Oorderid
        {
            set { _oorderid = value; }
            get { return _oorderid; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public int callcount
        {
            set { _callcount = value; }
            get { return _callcount; }
        }
        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string callmsg
        {
            set { _callmsg = value; }
            get { return _callmsg; }
        }

        /// <summary>
        /// 订单来源：0：web ;1:wap;2:android;3:ios;
        /// </summary>		

        public int ordersource
        {
            get { return _ordersource; }
            set { _ordersource = value; }
        }
        /// <summary>
        /// 是否加积分。0表示没有，大于0表示此订单加的积分
        /// </summary>		
        public int isaddpoint
        {
            get { return _isaddpoint; }
            set { _isaddpoint = value; }
        }
        /// <summary>
        /// 未用
        /// </summary>		
        public int sendtype
        {
            get { return _sendtype; }
            set { _sendtype = value; }
        }
        /// <summary>
        /// 收件人纬度 
        /// </summary>		
        public string ulat
        {
            get { return _ulat; }
            set { _ulat = value; }
        }
        /// <summary>
        /// 收件人经度
        /// </summary>	
        public string ulng
        {
            get { return _ulng; }
            set { _ulng = value; }
        }
        /// <summary>
        /// 发件人纬度 
        /// </summary>		
        public string shoplat
        {
            get { return _shoplat; }
            set { _shoplat = value; }
        }
        /// <summary>
        /// 发件人经度
        /// </summary>		
        public string shoplng
        {
            get { return _shoplng; }
            set { _shoplng = value; }
        }
        /// <summary>
        /// 配送点纬度
        /// </summary>		
        public string sitelat
        {
            get { return _sitelat; }
            set { _sitelat = value; }
        }
        /// <summary>
        /// 配送点经度
        /// </summary>		
        public string sitelng
        {
            get { return _sitelng; }
            set { _sitelng = value; }
        }
        /// <summary>
        /// 未用
        /// </summary>		
        public int ordertype
        {
            get { return _ordertype; }
            set { _ordertype = value; }
        }
        /// <summary>
        /// 未用 
        /// </summary>		
        public int noaccess
        {
            get { return _noaccess; }
            set { _noaccess = value; }
        }
        /// <summary>
        /// 未用
        /// </summary>		
        public int validateCode
        {
            get { return _validatecode; }
            set { _validatecode = value; }
        }
        /// <summary>
        /// 未用
        /// </summary>	
        public int iscancel
        {
            get { return _iscancel; }
            set { _iscancel = value; }
        }
        /// <summary>
        /// 服务类型编号
        /// </summary>		
        public int ReveInt1
        {
            get { return _reveint1; }
            set { _reveint1 = value; }
        }
        /// <summary>
        /// 是否被接单： 0未被接单 1已被接单  5流单(用于接单按钮的变化)
        /// </summary>		
        public int ReveInt2
        {
            get { return _reveint2; }
            set { _reveint2 = value; }
        }
        /// <summary>
        /// 收件人联系方式
        /// </summary>		  
        public string ReveVar
        {
            get { return _revevar; }
            set { _revevar = value; }
        }
        /// <summary>
        /// 未用
        /// </summary>		
        public DateTime ReveDate1
        {
            get { return _revedate1; }
            set { _revedate1 = value; }
        }
        /// <summary>
        /// 未用
        /// </summary>		
        public DateTime ReveDate2
        {
            get { return _revedate2; }
            set { _revedate2 = value; }
        }
        /// <summary>
        /// 未用
        /// </summary>		
        public int IsTimeLimit
        {
            get { return _istimelimit; }
            set { _istimelimit = value; }
        }

        private string _delivername;
        /// <summary>
        /// 配送员名称 （临时变量）
        /// </summary>
        public string delivername
        {
            get { return _delivername; }
            set { _delivername = value; }
        }
        /// <summary>
        /// 配送员电话 （临时变量）
        /// </summary>
        public string delivertel
        {
            get;
            set;
        }


        //订单的配送情况表
        private OrderDeliverInfo _deliveinfo;

        public OrderDeliverInfo DeliveInfo
        {
            set { _deliveinfo = value; }
            get { return _deliveinfo; }
        }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName
        {
            get;
            set;
        }

        /// <summary>
        /// 余额支付的金额
        /// </summary>
        public decimal acountpay
        {
            get;
            set;
        }


        /// <summary>
        /// 商家id
        /// </summary>
        public int shopid
        {
            get;
            set;
        }
        /// <summary>
        /// 支付密码
        /// </summary>
        public string PayPassword
        {
            get;
            set;
        }


    }
}

