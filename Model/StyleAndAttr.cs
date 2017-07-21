using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 规格，属性实体
    /// </summary>
    [Serializable]
    public class StyleAndAttr
    {
        /// <summary>
        /// 规格
        /// </summary>
        public IList<FoodStyleInfo> styles
        {
            set;
            get;
        }

        /// <summary>
        /// 属性
        /// </summary>
        public IList<FoodAttributesInfo> attrs
        {
            set;
            get;
        }

    }
}
