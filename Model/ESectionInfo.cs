
// ESectionInfo:������Ϣʵ����
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-03-05

using System;

namespace Hangjing.Model
{
	/// <summary>
	/// ����ʵ����
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
        /// ���б��
        /// </summary>
        public int cityid
        {
            set { _cityID = value; }
            get { return _cityID; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public int pri
        {
            set { _pri = value; }
            get { return _pri; }
        }

		/// <summary>
		/// ������
		/// </summary>
		public int SectionID
		{
            set { _sectionid = value; }
            get { return _sectionid; }
		}

		/// <summary>
		/// ��������
		/// </summary>
		public string SectionName
		{
            set { _sectionname = value; }
            get { return _sectionname; }
		}

        /// <summary>
        /// δ��
        /// </summary>
        public int Parentid
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
	}
}

