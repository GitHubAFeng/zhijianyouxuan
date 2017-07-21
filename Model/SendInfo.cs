using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 根据最短路径生成信息
    /// </summary>
    [Serializable]
    public class SendInfo
    {
        /// <summary>
        /// 距离
        /// </summary>
        public decimal Distance
        {
            get;
            set;
        }


        /// <summary>
        /// 配送费
        /// </summary>
        public decimal sendmoney
        {
            get;
            set;
        }


        /// <summary>
        ///未用
        /// </summary>
        public int ShopID
        {
            get;
            set;
        }

    }

}