using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 建筑物
	/// </summary>
	[Serializable]
	public class BuildingInfo
	{
        public BuildingInfo()
		{}
		#region Model
		private int  _dataid;
		private string _name;
		private string _address;
		private int _type;
		private string _picture;
		private string _xyurl;  
		private string _remark;
		private int _sectionid;
		private string _firstl;
        private string _sectionname;

        private string _lat;
        private string _lng;
        private int _isshow;
        private int _cityid;

        /// <summary>
        /// 城市编号
        /// </summary>
        public int cityid
        {
            set { _cityid = value; }
            get { return _cityid; }
        }

        /// <summary>
        /// 是否在热门：0表示否，1表示是;
        /// </summary>
        public int IsShow
        {
            set { _isshow = value; }
            get { return _isshow; }
        }

        public string Lat
        {
            set { _lat = value; }
            get { return _lat; }
        }

        public string Lng
        {
            set { _lng = value; }
            get { return _lng; }
        }

		/// <summary>
		/// 编号
		/// </summary>
		public int DataID
		{
			set{ _dataid=value;}
			get{return _dataid;}
		}

		/// <summary>
		/// 名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}

		/// <summary>
		/// 地址
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}

		/// <summary>
		/// 排序。大在前
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}

		/// <summary>
		/// 
		/// </summary>
		public string Picture
		{
			set{ _picture=value;}
			get{return _picture;}
		}

		/// <summary>
		/// 地图坐标
		/// </summary>
		public string XYUrl
		{
			set{ _xyurl=value;}
			get{return _xyurl;}
		}

		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}

		/// <summary>
		/// 区域编号
		/// </summary>
		public int SectionId
		{
			set{ _sectionid=value;}
            get { return _sectionid; }
		}

        /// <summary>
        /// 区域名称
        /// </summary>
        public string SectionName
        {
            set { _sectionname = value; }
            get { return _sectionname; }
        }

		/// <summary>
		/// 首字母
		/// </summary>
		public string FirstL
		{
			set{ _firstl=value;}
			get{return _firstl;}
		}

        /// <summary>
        /// 城市名称
        /// </summary>
        public string cityname
        {
            set;
            get;
        }

		#endregion Model

	}
}

