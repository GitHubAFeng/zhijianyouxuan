using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Hangjing.Model
{
    ///<summary>
    ///套餐实体
    ///<summary>
    public class FoodPackagInfo
    {

        private int _pid;
        /// <summary>
        /// PID
        /// </summary>
        public int PID
        {
            get { return _pid; }
            set { _pid = value; }
        }

        private string _code;
        /// <summary>
        /// 未用
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private int _shopid;
        /// <summary>
        /// 商家编号
        /// </summary>
        public int ShopId
        {
            get { return _shopid; }
            set { _shopid = value; }
        }

        private string _foodids;
        /// <summary>
        /// 未用
        /// </summary>
        public string foodids
        {
            get { return _foodids; }
            set { _foodids = value; }
        }

        private int _num;
        /// <summary>
        /// 总数量
        /// </summary>
        public int Num
        {
            get { return _num; }
            set { _num = value; }
        }

        private int _cnum;
        /// <summary>
        /// 当前已经售，此数字大于num时不能再购买了
        /// </summary>
        public int cnum
        {
            get { return _cnum; }
            set { _cnum = value; }
        }

        private decimal _price;
        /// <summary>
        /// 套餐价格
        /// </summary>
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private string _unit;
        /// <summary>
        /// 未用
        /// </summary>
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        private int _tag;
        /// <summary>
        /// 未用
        /// </summary>
        public int Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        private int _sectionid;
        /// <summary>
        /// 未用
        /// </summary>
        public int SectionId
        {
            get { return _sectionid; }
            set { _sectionid = value; }
        }

        private int _inve1;
        /// <summary>
        /// 未用
        /// </summary>
        public int Inve1
        {
            get { return _inve1; }
            set { _inve1 = value; }
        }

        private string _inve2;
        /// <summary>
        /// 未用
        /// </summary>
        public string Inve2
        {
            get { return _inve2; }
            set { _inve2 = value; }
        }

        private int _state;
        /// <summary>
        /// 状态：0上架，1下架
        /// </summary>
        public int state
        {
            get { return _state; }
            set { _state = value; }
        }

        private string _title;
        /// <summary>
        /// 套餐名称
        /// </summary>
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }

        private int _sortnum;
        /// <summary>
        /// 排序(降序)
        /// </summary>
        public int sortnum
        {
            get { return _sortnum; }
            set { _sortnum = value; }
        }

        private DateTime _startdate;
        /// <summary>
        /// 启用时间
        /// </summary>
        public DateTime startdate
        {
            get { return _startdate; }
            set { _startdate = value; }
        }

        private DateTime _enddate;
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime enddate
        {
            get { return _enddate; }
            set { _enddate = value; }
        }

        private DateTime _starttime;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime starttime
        {
            get { return _starttime; }
            set { _starttime = value; }
        }

        private DateTime _endtime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endtime
        {
            get { return _endtime; }
            set { _endtime = value; }
        }

        private decimal _oldprice;
        /// <summary>
        /// 原价
        /// </summary>
        public decimal oldprice
        {
            get { return _oldprice; }
            set { _oldprice = value; }
        }

        private decimal _revefloat1;
        /// <summary>
        /// ReveFloat1
        /// </summary>
        public decimal ReveFloat1
        {
            get { return _revefloat1; }
            set { _revefloat1 = value; }
        }

        private decimal _revefloat2;
        /// <summary>
        /// ReveFloat2
        /// </summary>
        public decimal ReveFloat2
        {
            get { return _revefloat2; }
            set { _revefloat2 = value; }
        }

        private string _revevar1;
        /// <summary>
        /// ReveVar1
        /// </summary>
        public string ReveVar1
        {
            get { return _revevar1; }
            set { _revevar1 = value; }
        }

        private string _revevar2;
        /// <summary>
        /// ReveVar2
        /// </summary>
        public string ReveVar2
        {
            get { return _revevar2; }
            set { _revevar2 = value; }
        }

        private IList<PackFoodlistInfo> _ItemList;
        public IList<PackFoodlistInfo> ItemList
        {
            get { return _ItemList; }
            set { _ItemList = value; }
        }

    }
}

