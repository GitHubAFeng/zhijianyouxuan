using System;
namespace Hangjing.Model
{
	/// <summary>
	/// 商家用户坐标实体
	/// </summary>
    public class latlnginfo
	{
        /// <summary>
        /// 用户纬度
        /// </summary>
        public string ulat
        {
            set;
            get;
        }

		/// <summary>
		/// 用户经度
		/// </summary>
        public string ulng
        {
            set;
            get;
        }

        /// <summary>
        /// 商家纬度
        /// </summary>
        public string slat
        {
            set;
            get;
        }

        /// <summary>
        /// 商家经度
        /// </summary>
        public string slng
        {
            set;
            get;
        }

	}
}

