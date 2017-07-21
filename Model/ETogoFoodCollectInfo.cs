using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 餐品收藏
    /// </summary>
    public class ETogoFoodCollectInfo
    {
        #region Model
        private int _dataid;
        private int _foodid;
        private int _togoid;
        private int _userid;
        private DateTime _ctime;
        private int _inve1;
        private string _inve2;
        private string _togoname;
        private string _foodname;
        private decimal _foodprice;

        /// <summary>
        /// 编号
        /// </summary>
        public int dataid
        {
            set { _dataid = value; }
            get { return _dataid; }
        }

        /// <summary>
        /// 餐品ID
        /// </summary>
        public int foodid
        {
            set { _foodid = value; }
            get { return _foodid; }
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
        /// 餐馆名称
        /// </summary>
        public string inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }

        /// <summary>
        /// 餐馆名称
        /// </summary>
        public string togoname
        {
            set { _togoname = value; }
            get { return _togoname; }
        }

        /// <summary>
        /// 餐品名称
        /// </summary>
        public string foodname
        {
            set { _foodname = value; }
            get { return _foodname; }
        }

        public decimal Foodprice
        {
            set { _foodprice = value;  }
            get { return _foodprice;  }
        }
        #endregion Model
    }
}
