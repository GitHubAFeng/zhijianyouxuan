// PointSetInfo.cs : �������ù��� ����̨��ʵ��
// CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.co
using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ʵ����PointSet ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ������Ŀ
		/// </summary>
		public string KeyName
		{
            get { return _keyname;}
			set { _keyname=value;}
			
		}
		/// <summary>
        /// ����ֵ
		/// </summary>
		public int KeyValue
		{
			set{ _keyvalue=value;}
			get{return _keyvalue;}
		}
		/// <summary>
        /// �Ƿ���ʹ��
		/// </summary>
		public string InUse
		{
			set{ _inuse=value;}
			get{return _inuse;}
		}
	}
}

