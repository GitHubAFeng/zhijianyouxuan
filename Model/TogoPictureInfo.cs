using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
   public class TogoPictureInfo
    {
        private int _dataid;
        private int _togoid;
        private string _picture;
        private int _pri;
        private int _inve1;
        private string _inve2;
        private string _togoName;
        private int _pictureCount;

        /// <summary>
        /// 幻灯片的个数
        /// </summary>
        public int PictureCount
        {
            get { return _pictureCount; }
            set { _pictureCount = value; }
        }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string TogoName
        {
            get { return _togoName; }
            set { _togoName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DataId
        {
            set { _dataid = value; }
            get { return _dataid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TogoId
        {
            set { _togoid = value; }
            get { return _togoid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Picture
        {
            set { _picture = value; }
            get { return _picture; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Pri
        {
            set { _pri = value; }
            get { return _pri; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Inve1
        {
            set { _inve1 = value; }
            get { return _inve1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }
    }
}
