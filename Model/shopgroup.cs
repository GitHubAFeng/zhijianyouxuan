using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 商家分组实体
    /// </summary>
    [Serializable]
    public class shopgroup
    {
        /// <summary>
        /// 头部html
        /// </summary>
        public string headhtml
        {
            set;
            get;
        }

        /// <summary>
        /// 城市列表
        /// </summary>
        public IList<PointsInfo> shops
        {
            set;
            get;
        }

    }
}
