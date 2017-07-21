
using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ShopSurroundings:商家环境图
	/// </summary>
	[Serializable]
	public partial class ShopSurroundingsInfo
	{
		
		private int _id;
		private int _shopid;
		private string _title;
		private string _picture;
		private int _sort;
		private int _reveint1;
		private int _reveint2;
		private string _revevar1;
		private string _revevar2;
		/// <summary>
		/// 编号
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 商家编号
		/// </summary>
		public int Shopid
		{
			set{ _shopid=value;}
			get{return _shopid;}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 图片
		/// </summary>
		public string Picture
		{
			set{ _picture=value;}
			get{return _picture;}
		}
		/// <summary>
		/// 排序
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 未用
		/// </summary>
		public int ReveInt1
		{
			set{ _reveint1=value;}
			get{return _reveint1;}
		}
		/// <summary>
		/// 未用
		/// </summary>
		public int ReveInt2
		{
			set{ _reveint2=value;}
			get{return _reveint2;}
		}

		/// <summary>
		/// 缩略图
		/// </summary>
		public string ReveVar1
		{
			set{ _revevar1=value;}
			get{return _revevar1;}
		}
		/// <summary>
		/// 未用
		/// </summary>
		public string ReveVar2
		{
			set{ _revevar2=value;}
			get{return _revevar2;}
		}

	}
}

