using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    [Serializable]
    public class AndroidOrderModelInfo
    {
        /*
        http://www.dianyifen.com/AndroidAPI/SubmitOrder.aspx
        ?ordermodel=
        {"totalmoney":"74.0","phone":"13750821675","eattime":"0","state":"1","userid":"","foodinorderString":"",
        "list":
        [
        "{\"count\":1,\"id\":\"8009\",\"price\":16,\"name\":\"猴头菇鸡汤\"}",
        "{\"count\":1,\"id\":\"8008\",\"price\":12,\"name\":\"黑眉豆龙骨汤\"}",
        "{\"count\":1,\"id\":\"8007\",\"price\":18,\"name\":\"元气乌鸡汤\"}",
        "{\"count\":1,\"id\":\"8006\",\"price\":28,\"name\":\"鼎香肥牛饭配黑眉豆\"}"
        ],
        "shopname":"Dianyifen","people":1,"addtime":"2011-11-23 18:12:38",
        "username":"郑","shopid":"1","address":"杭州","sentmoney":"5","realname":"郑","note":""}
        */
        //{'totalmoney':'74.0','phone':'13750821675','eattime':'0','state':'1','userid':'','foodinorderString':'','list':'qqqq','shopname':'Dianyifen',
        //'people':1,'addtime':'2011-11-23 18:12:38','username':'郑','shopid':'1','address':'杭州','sentmoney':'5','realname':'郑','note':''}

        //[{"ShopCardIDs":"","Phone":"","Mobilephone":"15356645670","UserID":"1","CustomerName":"jijunjian","sendfree":"0.0","CID":"","sendtype":"1","Oorderid":"0","PayPassword":"","tempCode":"","Receiver":"ceshi","OrderType":"0","Address":"ceshi","TogoId":"805","payType":"4","GainTime":"2013-05-13 14:56","Remark":"","bid":"0","cartlist":[{"owername":"0.0","PId":"837","PPrice":"13.0","PName":"传统牛肉刀削面（大）","Foodcurrentprice":"13.0","PNum":"2"}],"ordersource":"1"}]//

        private decimal _totalmoney;
        private string _phone;
        private string _mobilephone;
        private string _sendfree;
        private string _sendtype;
        private int _state;
        private int _userid;
        private string _foodinorderString;
        private List<AndroidFoodInOrderInfo> _list;
        private string _shopcardids;
        private string _shopname;
        private int _people;
        private string _addtime;
        private string _customername;//imei
        private int _togoId;
        private string _address;
        private decimal _sentmoney;
        private string _receiver;
        private string _ordertype;


        public string OrderType
        {
            set { _ordertype = value; }
            get { return _ordertype; }
        }

        private string _paytype;

        public string payType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        private string _ordersource;

        public string ordersource
        {
            set { _ordersource = value; }
            get { return _ordersource; }
        }

        private string _cid;

        public string CID
        {
            set { _cid = value; }
            get { return _cid; }
        }

        private string _bid;

        public string bid
        {
            set { _bid = value; }
            get { return _bid; }
        }


        private string _remark;

        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        private string _gaintime;
        public string GainTime
        {
            set { _gaintime = value; }
            get { return _gaintime; }
        }

        private string _tempcode;

        public string tempCode
        {
            set { _tempcode = value; }
            get { return _tempcode; }
        }

        private string _paypassword;

        public string PayPassword
        {
            set { _paypassword = value; }
            get { return _paypassword; }
        }

        private string _oorderid;

        public string Oorderid
        {
            set { _oorderid = value; }
            get { return _oorderid; }
        }

        public decimal totalmoney
        {
            set { _totalmoney = value; }
            get { return _totalmoney; }
        }

        public string ShopCardIDs
        {
            set { _shopcardids = value; }
            get { return _shopcardids; }
        }

        public string Mobilephone
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }

        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }

        public string sendtype
        {
            set { _sendtype = value; }
            get { return _sendtype; }
        }

        public string sendfree
        {
            set { _sendfree = value; }
            get { return _sendfree; }
        }

        public int state
        {
            set { _state = value; }
            get { return _state; }
        }

        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }

        public string foodinorderString
        {
            set { _foodinorderString = value; }
            get { return _foodinorderString; }
        }

        public string addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }

        public List<AndroidFoodInOrderInfo> cartlist
        {
            set { _list = value; }
            get { return _list; }
        }

        public string shopname
        {
            set { _shopname = value; }
            get { return _shopname; }
        }

        public int people
        {
            set { _people = value; }
            get { return _people; }
        }

        public string CustomerName
        {
            set { _customername = value; }
            get { return _customername; }
        }

        public int TogoId
        {
            set { _togoId = value; }
            get { return _togoId; }
        }

        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }

        public decimal sentmoney
        {
            set { _sentmoney = value; }
            get { return _sentmoney; }
        }

        public string Receiver
        {
            set { _receiver = value; }
            get { return _receiver; }
        }

      
    }
}
