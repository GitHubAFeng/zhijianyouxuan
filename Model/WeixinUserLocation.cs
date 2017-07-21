using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 微信用户位置记录表
	/// </summary>
	[Serializable]
	public partial class WeixinUserLocationInfo
	{
		private int _lid;
		private string _openid;
		private string _lat;
		private string _lng;
		private DateTime _addtime;
		private int _reveint1;
		private string _revevar1;
		/// <summary>
		/// 
		/// </summary>
		public int lid
		{
			set{ _lid=value;}
			get{return _lid;}
		}
		/// <summary>
		/// 微信用户对应公众号微一标示
		/// </summary>
		public string openid
		{
			set{ _openid=value;}
			get{return _openid;}
		}
		/// <summary>
		/// 用户纬度
		/// </summary>
		public string lat
		{
			set{ _lat=value;}
			get{return _lat;}
		}
		/// <summary>
		/// 用户经度
		/// </summary>
		public string lng
		{
			set{ _lng=value;}
			get{return _lng;}
		}
		/// <summary>
		/// 上传时间
		/// </summary>
		public DateTime addtime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 未用
		/// </summary>
		public int reveint1
		{
			set{ _reveint1=value;}
			get{return _reveint1;}
		}
		/// <summary>
		/// 未用
		/// </summary>
		public string revevar1
		{
			set{ _revevar1=value;}
			get{return _revevar1;}
		}
	}
}

