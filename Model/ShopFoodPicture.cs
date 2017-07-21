using System;
namespace Hangjing.Model
{
	/// <summary>
    /// �̼ұ�ǩ�����
	/// </summary>
	[Serializable]
	public class ShopFoodPictureInfo
	{
		private int _iid;
		private int _shopid;
		private string _url;
		private string _picture;
		private string _title;
		private int _inve1;
		private string _inve2;
		private int _cityid;
        private string _togoname;

        public string togoname
        {
            set { _togoname = value; }
            get { return _togoname; }
        }
		/// <summary>
		/// 
		/// </summary>
		public int IID
		{
			set{ _iid=value;}
			get{return _iid;}
		}
		/// <summary>
		/// Ϊ0 ����Ϊϵͳ��ͼƬ�κ��̼ҿ���ʹ��
		/// </summary>
		public int ShopId
		{
			set{ _shopid=value;}
			get{return _shopid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Url
		{
			set{ _url=value;}
			get{return _url;}
		}
		/// <summary>
		/// ����ͼ(290*218)
		/// </summary>
		public string Picture
		{
			set{ _picture=value;}
			get{return _picture;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
        /// δ��
		/// </summary>
		public int Inve1
		{
			set{ _inve1=value;}
			get{return _inve1;}
		}
		/// <summary>
		///δ��
		/// </summary>
		public string Inve2
		{
			set{ _inve2=value;}
			get{return _inve2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int cityid
		{
			set{ _cityid=value;}
			get{return _cityid;}
		}

	}
}

