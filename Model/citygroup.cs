using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 城市分组实体
    /// </summary>
    [Serializable]
    public class citygroup
    {
        /// <summary>
        /// 首字母
        /// </summary>
        public string firstletter
        {
            set;
            get;
        }

        /// <summary>
        /// 城市列表
        /// </summary>
        public IList<CityInfo> citylist
        {
            set;
            get;
        }

    }
}
