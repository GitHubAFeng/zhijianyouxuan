using System;
namespace Hangjing.Model
{
    /// <summary>
    /// 商家配送表
    /// </summary>
    [Serializable]
    public partial class shopdeliveryInfo
    {
        private int _rid;
        private int _tid;
        private DateTime _addtime;
        private decimal _distancestart;
        private decimal _distanceend;
        private int _minmoney;
        private decimal _sendmoney;
        private int _reveint1;
        private int _reveint2;
        private string _revevar1;
        private string _revevar2;
        private decimal _revefloat1;
        private decimal _revefloat2;
        /// <summary>
        /// 自增
        /// </summary>
        public int rId
        {
            set { _rid = value; }
            get { return _rid; }
        }
        /// <summary>
        /// 商家编号
        /// </summary>
        public int tid
        {
            set { _tid = value; }
            get { return _tid; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public DateTime AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 配送公里起点
        /// </summary>
        public decimal distancestart
        {
            set { _distancestart = value; }
            get { return _distancestart; }
        }
        /// <summary>
        /// 配送公里范围终点
        /// </summary>
        public decimal distanceend
        {
            set { _distanceend = value; }
            get { return _distanceend; }
        }
        /// <summary>
        /// 起送价格
        /// </summary>
        public int minmoney
        {
            set { _minmoney = value; }
            get { return _minmoney; }
        }
        /// <summary>
        /// 配送费用
        /// </summary>
        public decimal sendmoney
        {
            set { _sendmoney = value; }
            get { return _sendmoney; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public int ReveInt1
        {
            set { _reveint1 = value; }
            get { return _reveint1; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public int ReveInt2
        {
            set { _reveint2 = value; }
            get { return _reveint2; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public string ReveVar1
        {
            set { _revevar1 = value; }
            get { return _revevar1; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public string ReveVar2
        {
            set { _revevar2 = value; }
            get { return _revevar2; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public decimal ReveFloat1
        {
            set { _revefloat1 = value; }
            get { return _revefloat1; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public decimal ReveFloat2
        {
            set { _revefloat2 = value; }
            get { return _revefloat2; }
        }

    }
}

