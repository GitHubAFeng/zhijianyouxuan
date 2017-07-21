using System;
namespace Hangjing.Model
{
    /// <summary>
    /// 分类对应的文字广告
    /// </summary>
    [Serializable]
    public class SortAdInfo
    {
        private string _dataid;
        private string _picture;
        private string _link;
        private string _introduce;
        private string _state;
        private string _title;
        private int _width;
        private int _height;
        private int _islink;
        private int _cityID;
        private int _stype;
        private int _sortid;
        private decimal _Servicefee;
        private DateTime _AdStartDate;
        private DateTime _AdEndDate;
        private string _defautpic;
        private int _reveint1;
        private int _reveint2;
        private string _revevar1;
        private string _revevar2;
        /// <summary>
        /// 当不在展示时间时，显示此图片，且没有链接
        /// </summary>
        public string defautpic
        {
            set { _defautpic = value; }
            get { return _defautpic; }
        }

        /// <summary>
        /// 展示结束时间
        /// </summary>
        public DateTime AdEndDate
        {
            set { _AdEndDate = value; }
            get { return _AdEndDate; }
        }

        /// <summary>
        /// 展示开始时间
        /// </summary>
        public DateTime AdStartDate
        {
            set { _AdStartDate = value; }
            get { return _AdStartDate; }
        }

        /// <summary>
        /// 未用
        /// </summary>
        public decimal Servicefee
        {
            set { _Servicefee = value; }
            get { return _Servicefee; }
        }

        /// <summary>
        /// 未用
        /// </summary>
        public int sortid
        {
            set { _sortid = value; }
            get { return _sortid; }
        }

        /// <summary>
        /// 广告类型(0表示图片，1表示文字)
        /// </summary>
        public int stype
        {
            set { _stype = value; }
            get { return _stype; }
        }

        /// <summary>
        /// 未用
        /// </summary>
        public int cityid
        {
            set { _cityID = value; }
            get { return _cityID; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DataId
        {
            set { _dataid = value; }
            get { return _dataid; }
        }
        /// <summary>
        /// 图片
        /// </summary>
        public string Picture
        {
            set { _picture = value; }
            get { return _picture; }
        }
        /// <summary>
        /// 广告链接
        /// </summary>
        public string Link
        {
            set { _link = value; }
            get { return _link; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public string Introduce
        {
            set { _introduce = value; }
            get { return _introduce; }
        }
        /// <summary>
        /// 排序【降序】
        /// </summary>
        public string state
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 广告标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public int Width
        {
            set { _width = value; }
            get { return _width; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public int Height
        {
            set { _height = value; }
            get { return _height; }
        }

        /// <summary>
        /// 是否可连接，0代表是，1代表否
        /// </summary>
        public int isLink
        {
            set { _islink = value; }
            get { return _islink; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ReveInt1
        {
            set { _reveint1 = value; }
            get { return _reveint1; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ReveInt2
        {
            set { _reveint2 = value; }
            get { return _reveint2; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ReveVar1
        {
            set { _revevar1 = value; }
            get { return _revevar1; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ReveVar2
        {
            set { _revevar2 = value; }
            get { return _revevar2; }
        }
    }
}

