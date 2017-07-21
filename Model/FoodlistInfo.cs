using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    public class FoodlistInfo
    {
        private int _unid;
        private string _inuse;
        private int _counid;
        private int _foodunid;
        private decimal _foodprice;
        private int _fcounts;
        private string _remark;
        private decimal _oldprice;
        private int _togoid;
        private DateTime _adddate;
        private string _foodname;
        private string _orderid;
        private string _sortName;
        /// <summary>
        /// 类型名
        /// </summary>
        public string SortName
        {
            get { return _sortName; }
            set { _sortName = value; }
        }

        /// <summary>
        /// 订单编号 随机生成
        /// 2010-03-15 8:47 by jijunjian
        /// </summary>
        public string orderid
        {
            set { _orderid = value; }
            get { return _orderid; }
        }

        public string FoodName
        {
            set { _foodname = value; }
            get { return _foodname; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Unid
        {
            set { _unid = value; }
            get { return _unid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InUse
        {
            set { _inuse = value; }
            get { return _inuse; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public int COUnid
        {
            set { _counid = value; }
            get { return _counid; }
        }
        /// <summary>
        /// 商品编号
        /// </summary>
        public int FoodUnid
        {
            set { _foodunid = value; }
            get { return _foodunid; }
        }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal FoodPrice
        {
            set { _foodprice = value; }
            get { return _foodprice; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int FCounts
        {
            set { _fcounts = value; }
            get { return _fcounts; }
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
        /// 原价格
        /// </summary>
        public decimal OldPrice
        {
            set { _oldprice = value; }
            get { return _oldprice; }
        }
        /// <summary>
        /// 商家编号
        /// </summary>
        public int TogoId
        {
            set { _togoid = value; }
            get { return _togoid; }
        }
        /// <summary>
        /// 增加时间
        /// </summary>
        public DateTime adddate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }

        private string _revevar1;
        private string _revevar2;


        /// <summary>
        /// 0表示没有点评，1表示已经点评
        /// </summary>
        public string ReveVar1
        {
            set { _revevar1 = value; }
            get { return _revevar1; }
        }
        /// <summary>
        ///  未用
        /// </summary>
        public string ReveVar2
        {
            set { _revevar2 = value; }
            get { return _revevar2; }
        }

        /// <summary>
        /// 未用
        /// </summary>
        public string material
        {
            get;
            set;
        }

        /// <summary>
        /// 规格编号
        /// </summary>
        public int sid
        {
            get;
            set;
        }

        /// <summary>
        /// 加价
        /// </summary>
        public decimal addprice
        {
            get;
            set;
        }

        /// <summary>
        /// 规格名称
        /// </summary>
        public string sname
        {
            get;
            set;
        }
    }
}
