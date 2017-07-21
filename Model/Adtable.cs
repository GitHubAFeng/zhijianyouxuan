// AdTableInfo.cs:广告图.
// CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-05-12

using System;
namespace Hangjing.Model
{
    [Serializable]
    /// <summary>
    /// 实体类AdTable 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class AdTableInfo
    {
        private int _tid;
        private string _adtype;
        private string _adname;
        private string _adpage;
        private int _adheight;
        private int _adwidth;
        private string _adimageadrees;
        private string _adadrees;
        private int _daymode;
        private decimal _daymoney;
        private int _userid;
        private int _mid;
        private DateTime _adstartdate;
        private DateTime _adadddate;
        private string _userName;

        /// <summary>
        ///username（用户名： sysuser（表）.FormalName（字段））
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TID
        {
            set { _tid = value; }
            get { return _tid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AdType
        {
            set { _adtype = value; }
            get { return _adtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AdName
        {
            set { _adname = value; }
            get { return _adname; }
        }
        /// <summary>
        /// 广告说明
        /// </summary>
        public string AdPage
        {
            set { _adpage = value; }
            get { return _adpage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int AdHeight
        {
            set { _adheight = value; }
            get { return _adheight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int AdWidth
        {
            set { _adwidth = value; }
            get { return _adwidth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AdImageAdrees
        {
            set { _adimageadrees = value; }
            get { return _adimageadrees; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AdAdrees
        {
            set { _adadrees = value; }
            get { return _adadrees; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public int DayMode
        {
            set { _daymode = value; }
            get { return _daymode; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public decimal DayMoney
        {
            set { _daymoney = value; }
            get { return _daymoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int MID
        {
            set { _mid = value; }
            get { return _mid; }
        }
        /// <summary> 
        /// 
        /// </summary>
        public DateTime AdStartDate
        {
            set { _adstartdate = value; }
            get { return _adstartdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AdAddDate
        {
            set { _adadddate = value; }
            get { return _adadddate; }
        }
    }
}


