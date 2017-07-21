using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 实体类WebBasic 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public class WebBasicInfo
	{
        private int _dataid;
		private string _key;
		private string _value;
		private string _inve1;
        private int _stype;

        /// <summary>
        /// 用来区域参数，1表示普通，2表示用textear编辑，3表示用fck编辑
        /// </summary>
        public int stype
        {
            set { _stype = value; }
            get { return _stype; }
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
		/// 标题
		/// </summary>
		public string Key
		{
			set{ _key=value;}
			get{return _key;}
		}

		/// <summary>
		/// 内容
		/// </summary>
		public string Value
		{
			set{ _value=value;}
			get{return _value;}
		}
		
        /// <summary>
		/// 
		/// </summary>
		public string Inve1
		{
			set{ _inve1=value;}
			get{return _inve1;}
		}
	}
}

