using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ʵ����Links ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	public class LinksInfo
	{
		public LinksInfo()
		{}
		#region Model
		private int _linkid;
		private int _type;
		private string _url;
		private string _picture;
        private int _introduce;

        private string _title;

        ///<summary>
        ///���Ӽ��
        /// </summary>
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }

        ///<summary>
        ///����
        /// </summary>
        public int Introduce
        {
            get { return _introduce; }
            set { _introduce = value; }
        }

		/// <summary>
		/// �������
		/// </summary>
		public int LinkID
		{
			set{ _linkid=value;}
			get{return _linkid;}
		}
		/// <summary>
		/// ������������
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// ���ӵ�ַ
		/// </summary>
		public string Url
		{
			set{ _url=value;}
			get{return _url;}
		}
		/// <summary>
		/// LogoͼƬ
		/// </summary>
		public string Picture
		{
			set{ _picture=value;}
			get{return _picture;}
		}
		#endregion Model

	}
}

