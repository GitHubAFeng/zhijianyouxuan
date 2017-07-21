using System;
using System.Collections.Generic;
namespace Hangjing.Model
{
	/// <summary>
	/// 城市信息
	/// </summary>
	[Serializable]
	public class CityInfo
	{
        private int _cid;
        private string _cname;
        private string _site;
        private string _url;
        private DateTime? _adddate;
        private int _reveint;
        private string _revevar;
        private string _ratio;

        /// <summary>
        /// 区域抽成、比例
        /// </summary>
        public string ratio
        {
            set { _ratio = value; }
            get { return _ratio; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int cid
        {
            set { _cid = value; }
            get { return _cid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cname
        {
            set { _cname = value; }
            get { return _cname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string site
        {
            set { _site = value; }
            get { return _site; }
        }
        /// <summary>
        /// 城市图片
        /// </summary>
        public string url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? adddate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }
        /// <summary>
        ///  排序，降序
        /// </summary>
        public int ReveInt
        {
            set { _reveint = value; }
            get { return _reveint; }
        }
        /// <summary>
        /// 首字母
        /// </summary>
        public string ReveVar
        {
            set { _revevar = value; }
            get { return _revevar; }
        }

        public IList<ShopDataInfo> sublist
        {
            set;
            get;
        }

        /// <summary>
        /// 商家数量
        /// </summary>
        public int ShopCount
        {
            set;
            get;
        }


        /// <summary>
        /// 城市纬度
        /// </summary>
        public string Lat
        {
            set;
            get;
        }

        /// <summary>
        /// 城市经度
        /// </summary>
        public string Lng
        {
            set;
            get;
        }

        
        public IList<CityInfo> CityJuniorList { get; set; }


	}
}

