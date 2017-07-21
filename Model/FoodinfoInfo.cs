using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    [Serializable]
    public class FoodinfoInfo
    {
        private int _unid;
        private string _inuse;
        private string _foodno;
        private decimal _fprice;
        private DateTime _fpindate;
        private DateTime _fpactivedate;
        private int  _fpmaster;
        private string _foodname;
        private string _foodnamepy;
        private decimal _fullprice;
        private int _remains;
        private int _maxperday;
        private string _taste;
        private string _picture;
        private int _foodtype;
        private string _sortName;
        private string _togoName;
        private int _ordernum;
        private int _isrecommend;
        private int _isspecial;
        private string _opentime;
        private int _isauth;
        
        /// <summary>
        /// 10:00:00-12:00:00/14:00:00-23:00:00;
        /// </summary>
        public string OpenTime
        {
            set { _opentime = value; }
            get { return _opentime; }
        }
        /// <summary>
        /// 属性数量
        /// </summary>
        public int isauth
        {
            get { return _isauth; }
            set { _isauth = value; }
        }

        /// <summary>
        /// 统计是否热门菜
        /// </summary>
        public int IsRecommend
        {
            get { return _isrecommend; }
            set { _isrecommend = value; }
        }

        /// <summary>
        /// 规格数量：
        /// </summary>
        public int IsSpecial
        {
            get { return _isspecial; }
            set { _isspecial = value; }
        }

        /// <summary>
        /// 商品排序
        /// </summary>
        public int OrderNum
        {
            get { return _ordernum; }
            set { _ordernum = value; }
        }

        /// <summary>
        /// 商家名
        /// </summary>
        public string TogoName
        {
            get { return _togoName; }
            set { _togoName = value; }
        }

        /// <summary>
        /// 类型名
        /// </summary>
        public string SortName
        {
            get { return _sortName; }
            set { _sortName = value; }
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
        /// y 上架，n 下架，d  删除
        /// </summary>
        public string InUse
        {
            set { _inuse = value; }
            get { return _inuse; }
        }

        /// <summary>
        /// 商家推荐 1是 0否
        /// </summary>
        public string FoodNo
        {
            set { _foodno = value; }
            get { return _foodno; }
        }
        /// <summary>
        /// 今日特价 1是 0否
        /// </summary>
        public string Special
        {
            set;
            get;
        }


        /// <summary>
        /// 规格中最底价格
        /// </summary>
        public decimal FPrice
        {
            set { _fprice = value; }
            get { return _fprice; }
        }

        /// <summary>
        /// 价格录入日期
        /// </summary>
        public DateTime FPInDate
        {
            set { _fpindate = value; }
            get { return _fpindate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime FPActiveDate
        {
            set { _fpactivedate = value; }
            get { return _fpactivedate; }
        }

        /// <summary>
        /// 菜品隶属餐馆编号
        /// </summary>
        public int  FPMaster
        {
            set { _fpmaster = value; }
            get { return _fpmaster; }
        }

        /// <summary>
        /// 菜品名
        /// </summary>
        public string FoodName
        {
            set { _foodname = value; }
            get { return _foodname; }
        }

        /// <summary>
        /// 拼音
        /// </summary>
        public string FoodNamePy
        {
            set { _foodnamepy = value; }
            get { return _foodnamepy; }
        }

        /// <summary>
        /// 打包费
        /// </summary>
        public decimal FullPrice
        {
            set { _fullprice = value; }
            get { return _fullprice; }
        }
        /// <summary>
        ///  月销量
        /// </summary>
        public int Remains
        {
            set { _remains = value; }
            get { return _remains; }
        }

        /// <summary>
        /// 每日最大供应量 库存
        /// </summary>
        public int MaxPerDay
        {
            set { _maxperday = value; }
            get { return _maxperday; }
        }

        /// <summary>
        /// 口味
        /// </summary>
        public string Taste
        {
            set { _taste = value; }
            get { return _taste; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string  Picture
        {
            set { _picture = value; }
            get { return _picture; }
        }

        /// <summary>
        /// 类型
        /// </summary>
        public int FoodType
        {
            set { _foodtype = value; }
            get { return _foodtype; }
        }
        /// <summary>
        /// 原价（今日特价用到）
        /// </summary>
        public decimal Oldprice
        {
            set;
            get;
        }
        /// <summary>
        /// 库存类型 0 无限库存 1手动库存
        /// </summary>
        public int DpPerDay
        {
            set;
            get;
        }


    }
}
