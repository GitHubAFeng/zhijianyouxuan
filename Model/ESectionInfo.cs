
// ESectionInfo:区域信息实体类
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-03-05

using System;

namespace Hangjing.Model
{
	/// <summary>
	/// 区域实体类
	/// </summary>
	[Serializable]
	public class SectionInfo
	{
		private int _sectionid;
		private string _sectionname;
        private int _pri;
        private int _cityID;
        private int _parentid;

        /// <summary>
        /// 城市编号
        /// </summary>
        public int cityid
        {
            set { _cityID = value; }
            get { return _cityID; }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int pri
        {
            set { _pri = value; }
            get { return _pri; }
        }

		/// <summary>
		/// 区域编号
		/// </summary>
		public int SectionID
		{
            set { _sectionid = value; }
            get { return _sectionid; }
		}

		/// <summary>
		/// 区域名称
		/// </summary>
		public string SectionName
		{
            set { _sectionname = value; }
            get { return _sectionname; }
		}

        /// <summary>
        /// 未用
        /// </summary>
        public int Parentid
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
	}
}

