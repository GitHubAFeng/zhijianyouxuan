using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 实体类VipGrade 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class VipGradeInfo
	{
		private int _dataid;
		private string _gradename;
		private int _minpoint;
		private int _maxpoint;
		private decimal _vrat;
		private int _gaipoint;
		private int _reve1;
		private string _reve2;

        private User_Grade_RInfo _favourable;


        /// <summary>
        /// 等级对应优惠信息
        /// </summary>
        public User_Grade_RInfo favourable
        {
            set { _favourable = value; }
            get { return _favourable; }
        }

		/// <summary>
		/// 
		/// </summary>
		public int DataID
		{
			set{ _dataid=value;}
			get{return _dataid;}
		}
		/// <summary>
		/// 等级名称
		/// </summary>
		public string GradeName
		{
			set{ _gradename=value;}
			get{return _gradename;}
		}
		/// <summary>
		/// 此等级积分起点
		/// </summary>
		public int MinPoint
		{
			set{ _minpoint=value;}
			get{return _minpoint;}
		}
		/// <summary>
		/// 此等级积分终点
		/// </summary>
		public int MaxPoint
		{
			set{ _maxpoint=value;}
			get{return _maxpoint;}
		}
		/// <summary>
		/// 此等级积分增加倍数
		/// </summary>
		public decimal vRat
		{
			set{ _vrat=value;}
			get{return _vrat;}
		}
		/// <summary>
		/// 兑换该等级所需要积分
		/// </summary>
		public int GaiPoint
		{
			set{ _gaipoint=value;}
			get{return _gaipoint;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Reve1
		{
			set{ _reve1=value;}
			get{return _reve1;}
		}
		/// <summary>
		/// 会员的等级图像
		/// </summary>
		public string Reve2
		{
			set{ _reve2=value;}
			get{return _reve2;}
		}
	}
}

