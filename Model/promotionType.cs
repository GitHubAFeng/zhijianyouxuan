using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Hangjing.Model
{
    /// <summary>
    /// 促销类型
    /// </summary>
    public class promotionTypeInfo
    {

        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string title
        {
            get;
            set;
        }

        /// <summary>
        /// 状态值，固定
        /// </summary>
        public int state
        {
            get;
            set;
        }

        /// <summary>
        /// 排序值
        /// </summary>
        public int sortnum
        {
            get;
            set;
        }

        /// <summary>
        /// 0表示是系统促销，1表示是商家促销分类
        /// </summary>
        public int iswebtype
        {
            get;
            set;
        }

        /// <summary>
        /// reveint1
        /// </summary>
        public int reveint1
        {
            get;
            set;
        }

        /// <summary>
        /// reveint2
        /// </summary>
        public int reveint2
        {
            get;
            set;
        }

        /// <summary>
        /// revevar1
        /// </summary>
        public string revevar1
        {
            get;
            set;
        }

    }
}

