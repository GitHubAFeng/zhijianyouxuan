using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 分类分组实体
    /// </summary>
    [Serializable]
    public class sortgroup
    {
        /// <summary>
        /// 城市列表
        /// </summary>
        public IList<ShopDataInfo> sortlist
        {
            set;
            get;
        }

    }
}
