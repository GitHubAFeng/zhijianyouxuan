using System;
namespace Hangjing.Model
{
	/// <summary>
	/// �û��ߵ���¼
	/// </summary>
	[Serializable]
	public class hurryorderInfo
	{
		private int _cid;
		private string  _oid;
        private string _Name;
		private string _addtime;
		private int _ccount;
		private int _reveint;
		private string _revevar;

        private int _id;
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }

		/// <summary>
		/// auto_increment
		/// </summary>
		public int CID
		{
			set{ _cid=value;}
			get{return _cid;}
		}
		/// <summary>
		/// �������
		/// </summary>
		public string oid
		{
			set{ _oid=value;}
			get{return _oid;}
		}
		/// <summary>
		/// �û���
		/// </summary>
        public string Name
		{
            set { _Name = value; }
            get { return _Name; }
		}
		/// <summary>
		/// ���һ��ʱ��
		/// </summary>
		public string addtime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
        /// ��ǰ�����ߵ�����
		/// </summary>
		public int Ccount
		{
			set{ _ccount=value;}
			get{return _ccount;}
		}
		/// <summary>
		/// ״̬ 0 δ���� 1�Ѵ���
		/// </summary>
		public int ReveInt
		{
			set{ _reveint=value;}
			get{return _reveint;}
		}
		/// <summary>
        /// ����
		/// </summary>
		public string ReveVar
		{
			set{ _revevar=value;}
			get{return _revevar;}
		}

	}
}

