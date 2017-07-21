using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ʵ����VipGrade ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// �ȼ���Ӧ�Ż���Ϣ
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
		/// �ȼ�����
		/// </summary>
		public string GradeName
		{
			set{ _gradename=value;}
			get{return _gradename;}
		}
		/// <summary>
		/// �˵ȼ��������
		/// </summary>
		public int MinPoint
		{
			set{ _minpoint=value;}
			get{return _minpoint;}
		}
		/// <summary>
		/// �˵ȼ������յ�
		/// </summary>
		public int MaxPoint
		{
			set{ _maxpoint=value;}
			get{return _maxpoint;}
		}
		/// <summary>
		/// �˵ȼ��������ӱ���
		/// </summary>
		public decimal vRat
		{
			set{ _vrat=value;}
			get{return _vrat;}
		}
		/// <summary>
		/// �һ��õȼ�����Ҫ����
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
		/// ��Ա�ĵȼ�ͼ��
		/// </summary>
		public string Reve2
		{
			set{ _reve2=value;}
			get{return _reve2;}
		}
	}
}

