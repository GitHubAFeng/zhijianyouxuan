using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ��ʿλ����
	/// </summary>
    public class localInfo
	{
        private string _lng;
        private string _lat;
        private string _speed;
        private string _bear;
		

		/// <summary>
		/// ����
		/// </summary>
        public string lng
		{
            set { _lng = value; }
            get { return _lng; }
		}

		/// <summary>
		/// γ��
		/// </summary>
        public string lat
		{
            set { _lat = value; }
            get { return _lat; }
		}

		/// <summary>
		/// �ٶ�  ����/ʱ
		/// </summary>
        public string speed
		{
            set { _speed = value; }
            get { return _speed; }
		}

		/// <summary>
		/// ���� 0-360
		/// </summary>
        public string bear
		{
            set { _bear = value; }
            get { return _bear; }
		}
	}
}

