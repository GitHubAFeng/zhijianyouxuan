using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 属性选项
    /// </summary>
    [Serializable]
    public class attritem
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string name
        {
            set;
            get;
        }

        /// <summary>
        /// 价格
        /// </summary>
        public string price
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public int FoodtId
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int DataId
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set;
            get;
        }
    }
}
