using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Hangjing.Model
{

    /// <summary>
    /// 分销比例
    /// </summary>
    public class distributeRatioInfo
    {

        /// <summary>
        /// 编号
        /// </summary>
        public int drId
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
        /// 第一级百分比(自己相当于0，第一级就是自己的直接上线)
        /// </summary>
        public int onegraderatio
        {
            get;
            set;
        }

        /// <summary>
        /// 第二级百分比
        /// </summary>
        public int twograderatio
        {
            get;
            set;
        }

        /// <summary>
        /// 第三级百分比
        /// </summary>
        public int threegraderatio
        {
            get;
            set;
        }

        /// <summary>
        /// 未用
        /// </summary>
        public int reveint1
        {
            get;
            set;
        }

        /// <summary>
        /// 未用
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

        /// <summary>
        /// revevar2
        /// </summary>
        public string revevar2
        {
            get;
            set;
        }

        /// <summary>
        /// revevar3
        /// </summary>
        public string revevar3
        {
            get;
            set;
        }

        /// <summary>
        /// revevar4
        /// </summary>
        public string revevar4
        {
            get;
            set;
        }

    }
}

