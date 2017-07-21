
// EFoodOpinionInfo.css:餐品评论模型.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-03-05

using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 实体类EFoodOpinion
	/// </summary>
	[Serializable]
	public class EFoodOpinionInfo
	{

		private int _dataid;
		private int _userid;
		private int _foodid;
		private string _opinion;
		private DateTime _time;
		private int _point;
        private string _foodname;
        private string _togoname;
        private string _username;

		/// <summary>
		/// 自增编号
		/// </summary>
		public int DataID
		{
            set { _dataid = value; }
            get { return _dataid; }
		}

		/// <summary>
		/// 用户编号
		/// </summary>
		public int UserID
		{
            set { _userid = value; }
            get { return _userid; }
		}

		/// <summary>
		/// 餐品编号
		/// </summary>
		public int FoodID
		{
            set { _foodid = value; }
            get { return _foodid; }
		}

		/// <summary>
		/// 评论内容
		/// </summary>
		public string Opinion
		{
            set { _opinion = value; }
            get { return _opinion; }
		}

		/// <summary>
		/// 评论时间
		/// </summary>
		public DateTime Time
		{
            set { _time = value; }
            get { return _time; }
		}

		/// <summary>
		/// 分1.2.3.4.5个等级页面用相应个数的星表示
		/// </summary>
		public int Point
		{
            set { _point = value; }
            get { return _point; }
		}
        /// <summary>
        /// 商品名称
        /// </summary>
        public string FoodName
        {
            set { _foodname = value; }
            get { return _foodname; }
        }
        /// <summary>
        /// 商家名称
        /// </summary>
        public string TogoName
        {
            set { _togoname = value; }
            get { return _togoname; }
        }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username;  }
        }
	}
}

