
// EFoodOpinionInfo.css:��Ʒ����ģ��.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-03-05

using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ʵ����EFoodOpinion
	/// </summary>
	[Serializable]
	public class EFoodOpinionInfo
	{

		private int _dataid;
		private int _userid;
		private int _foodid;
		private string _opinion;
		private DateTime _time;
		private int _point;
        private string _foodname;
        private string _togoname;
        private string _username;

		/// <summary>
		/// �������
		/// </summary>
		public int DataID
		{
            set { _dataid = value; }
            get { return _dataid; }
		}

		/// <summary>
		/// �û����
		/// </summary>
		public int UserID
		{
            set { _userid = value; }
            get { return _userid; }
		}

		/// <summary>
		/// ��Ʒ���
		/// </summary>
		public int FoodID
		{
            set { _foodid = value; }
            get { return _foodid; }
		}

		/// <summary>
		/// ��������
		/// </summary>
		public string Opinion
		{
            set { _opinion = value; }
            get { return _opinion; }
		}

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime Time
		{
            set { _time = value; }
            get { return _time; }
		}

		/// <summary>
		/// ��1.2.3.4.5���ȼ�ҳ������Ӧ�������Ǳ�ʾ
		/// </summary>
		public int Point
		{
            set { _point = value; }
            get { return _point; }
		}
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string FoodName
        {
            set { _foodname = value; }
            get { return _foodname; }
        }
        /// <summary>
        /// �̼�����
        /// </summary>
        public string TogoName
        {
            set { _togoname = value; }
            get { return _togoname; }
        }
        /// <summary>
        /// �û��ǳ�
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username;  }
        }
	}
}

