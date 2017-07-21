using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 骑士位置类
	/// </summary>
    public class localInfo
	{
        private string _lng;
        private string _lat;
        private string _speed;
        private string _bear;
		

		/// <summary>
		/// 经度
		/// </summary>
        public string lng
		{
            set { _lng = value; }
            get { return _lng; }
		}

		/// <summary>
		/// 纬度
		/// </summary>
        public string lat
		{
            set { _lat = value; }
            get { return _lat; }
		}

		/// <summary>
		/// 速度  公里/时
		/// </summary>
        public string speed
		{
            set { _speed = value; }
            get { return _speed; }
		}

		/// <summary>
		/// 航向 0-360
		/// </summary>
        public string bear
		{
            set { _bear = value; }
            get { return _bear; }
		}
	}
}

