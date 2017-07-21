using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 实体类ShopData 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    [Serializable]
	public class ShopDataInfo
	{
		private int _id;
		private string _classname;
        public string _value;
		private int _depth;
		private int _status;
		private int _priority;
		private int _parentid;
		private int _isdel;

		/// <summary>
        /// 1商家类别2餐厅氛围3地区管理4区域管理5人均消费6经营项目7菜系管理8美食快讯
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string classname
		{
			set{ _classname=value;}
			get{return _classname;}
		}
        /// <summary>
        /// 新增，
        /// </summary>
        public string value
        {
            set { _value = value; }
            get { return _value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public int Depth
		{
			set{ _depth=value;}
			get{return _depth;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Priority
		{
			set{ _priority=value;}
			get{return _priority;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int isDel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
        /// <summary>
		/// 
		/// </summary>
        public string Pic
        {
            set;
            get;
        }
        
	}
}

