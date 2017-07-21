using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 餐馆收藏
    /// </summary>
    public class ETogoCollectInfo
    {
        #region Model
        private int _dataid;
        private int _togoid;
        private string _togoname;
        private int _userid;
        private DateTime _ctime;
        private int _inve1;
        private string _inve2;

        /// <summary>
        /// 编号
        /// </summary>
        public int dataid
        {
            set { _dataid = value; }
            get { return _dataid; }
        }

        /// <summary>
        /// 餐馆ID
        /// </summary>
        public int togoid
        {
            set { _togoid = value; }
            get { return _togoid; }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int userid
        {
            set { _userid = value; }
            get { return _userid; }
        }

        /// <summary>
        /// 收藏时间
        /// </summary>
        public DateTime ctime
        {
            set { _ctime = value; }
            get { return _ctime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int inve1
        {
            set { _inve1 = value; }
            get { return _inve1; }
        }

        /// <summary>
        /// 建筑物ids
        /// </summary>
        public string inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string Togoname
        {
            set { _togoname = value;  }
            get { return _togoname; }
        }
        #endregion Model
    }
}
