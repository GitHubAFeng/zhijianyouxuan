using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
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
    //{"owername":"0.0","PId":"837","PPrice":"13.0","PName":"传统牛肉刀削面（大）","Foodcurrentprice":"13.0","PNum":"2"}
    [Serializable]
    public class AndroidFoodInOrderInfo
    {
        public AndroidFoodInOrderInfo()
        {

        }
        private int _pnum;
        private int _pid;
        private decimal _pprice;
        private string _pname;
        private string _owername;
        private string _foodcurrentprice;

        private string _togoid;
        /// <summary>
        /// 菜品里面保存的商家的id
        /// </summary>
        public string TogoId
        {
            set { _togoid = value; }
            get { return _togoid; }
        }

        public string Foodcurrentprice
        {
            set { _foodcurrentprice = value; }
            get { return _foodcurrentprice; }
        }

        public string owername
        {
            set { _owername = value; }
            get { return _owername; }
        }

        public int PNum
        {
            set { _pnum = value; }
            get { return _pnum; }
        }

        public int PId
        {
            set { _pid = value; }
            get { return _pid; }
        }

        public decimal PPrice
        {
            set { _pprice = value; }
            get { return _pprice; }
        }

        public string PName
        {
            set { _pname = value; }
            get { return _pname; }
        }
    }
}
