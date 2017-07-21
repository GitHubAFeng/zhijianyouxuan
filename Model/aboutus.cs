using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 实体类aboutus 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class aboutusInfo
	{
		private int _dataid;
		private int _sortid;
		private string _title;
		private string _helpcontent;
		private DateTime _addtime;
		private int _viewtimes;
		private int _ordernum;
		private string _keyword;
		private bool _isvisiableathome;
		private bool _isvisiablepictureathome;
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public int DataId
		{
			set{ _dataid=value;}
			get{return _dataid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SortId
		{
			set{ _sortid=value;}
			get{return _sortid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HelpContent
		{
			set{ _helpcontent=value;}
			get{return _helpcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ViewTimes
		{
			set{ _viewtimes=value;}
			get{return _viewtimes;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int OrderNum
		{
			set{ _ordernum=value;}
			get{return _ordernum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string KeyWord
		{
			set{ _keyword=value;}
			get{return _keyword;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsVisiableAtHome
		{
			set{ _isvisiableathome=value;}
			get{return _isvisiableathome;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsVisiablePictureAtHome
		{
			set{ _isvisiablepictureathome=value;}
			get{return _isvisiablepictureathome;}
		}
	}
}

