using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ʵ����WebBasic ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	public class WebBasicInfo
	{
        private int _dataid;
		private string _key;
		private string _value;
		private string _inve1;
        private int _stype;

        /// <summary>
        /// �������������1��ʾ��ͨ��2��ʾ��textear�༭��3��ʾ��fck�༭
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
		/// ����
		/// </summary>
		public string Key
		{
			set{ _key=value;}
			get{return _key;}
		}

		/// <summary>
		/// ����
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

