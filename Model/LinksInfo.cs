using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 实体类Links 。(属性说明自动提取数据库字段的描述信息)
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
        ///链接简介
        /// </summary>
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }

        ///<summary>
        ///排序
        /// </summary>
        public int Introduce
        {
            get { return _introduce; }
            set { _introduce = value; }
        }

		/// <summary>
		/// 自增编号
		/// </summary>
		public int LinkID
		{
			set{ _linkid=value;}
			get{return _linkid;}
		}
		/// <summary>
		/// 友情链接名称
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 链接地址
		/// </summary>
		public string Url
		{
			set{ _url=value;}
			get{return _url;}
		}
		/// <summary>
		/// Logo图片
		/// </summary>
		public string Picture
		{
			set{ _picture=value;}
			get{return _picture;}
		}
		#endregion Model

	}
}

