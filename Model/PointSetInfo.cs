// PointSetInfo.cs : 积分配置功能 （后台）实体
// CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.co
using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 实体类PointSet 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public class PointSetInfo
	{
		private int _dataid;
		private string _keyname;
		private int _keyvalue;
		private string _inuse;
		/// <summary>
		/// 
		/// </summary>
		public int DataId
		{
			set{ _dataid=value;}
			get{return _dataid;}
		}
		/// <summary>
        /// 积分项目
		/// </summary>
		public string KeyName
		{
            get { return _keyname;}
			set { _keyname=value;}
			
		}
		/// <summary>
        /// 积分值
		/// </summary>
		public int KeyValue
		{
			set{ _keyvalue=value;}
			get{return _keyvalue;}
		}
		/// <summary>
        /// 是否在使用
		/// </summary>
		public string InUse
		{
			set{ _inuse=value;}
			get{return _inuse;}
		}
	}
}

