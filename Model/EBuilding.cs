using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ������
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
        /// ���б��
        /// </summary>
        public int cityid
        {
            set { _cityid = value; }
            get { return _cityid; }
        }

        /// <summary>
        /// �Ƿ������ţ�0��ʾ��1��ʾ��;
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
		/// ���
		/// </summary>
		public int DataID
		{
			set{ _dataid=value;}
			get{return _dataid;}
		}

		/// <summary>
		/// ����
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}

		/// <summary>
		/// ��ַ
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}

		/// <summary>
		/// ���򡣴���ǰ
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
		/// ��ͼ����
		/// </summary>
		public string XYUrl
		{
			set{ _xyurl=value;}
			get{return _xyurl;}
		}

		/// <summary>
		/// ��ע
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}

		/// <summary>
		/// ������
		/// </summary>
		public int SectionId
		{
			set{ _sectionid=value;}
            get { return _sectionid; }
		}

        /// <summary>
        /// ��������
        /// </summary>
        public string SectionName
        {
            set { _sectionname = value; }
            get { return _sectionname; }
        }

		/// <summary>
		/// ����ĸ
		/// </summary>
		public string FirstL
		{
			set{ _firstl=value;}
			get{return _firstl;}
		}

        /// <summary>
        /// ��������
        /// </summary>
        public string cityname
        {
            set;
            get;
        }

		#endregion Model

	}
}

