using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 计算配送费参数
    /// </summary>
    [Serializable]
    public class SendfeeInfo
    {
        /// <summary>
        /// 代收货款
        /// </summary>
        public decimal userpay
        {
            get;
            set;
        }

        /// <summary>
        ///城市编号
        /// </summary>
        public int ShopID
        {
            get;
            set;
        }

        /// <summary>
        ///城市名称
        /// </summary>
        public string cityname
        {
            get;
            set;
        }

        /// <summary>
        ///配送时间（与配送费有关）
        /// </summary>
        public DateTime sendtime
        {
            get;
            set;
        }

        /// <summary>
        ///商家，用户坐标信息
        /// </summary>
        public latlnginfo latlng
        {
            get;
            set;
        }

    }

}