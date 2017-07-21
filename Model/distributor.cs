using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Hangjing.Model
{

    /// <summary>
    /// 用户分销关系表
    /// </summary>
    public class distributorInfo
    {

        /// <summary>
        /// 编号
        /// </summary>
        public int dId
        {
            get;
            set;
        }

        /// <summary>
        /// 会员编号
        /// </summary>
        public int userid
        {
            get;
            set;
        }

        /// <summary>
        /// 上级编号(直接上线)
        /// </summary>
        public int onegradeID
        {
            get;
            set;
        }

        /// <summary>
        /// 上级openid(直接上线)
        /// </summary>
        public string oneopenid
        {
            get;
            set;
        }

        /// <summary>
        /// 上级名称(直接上线)
        /// </summary>
        public string oneName
        {
            get;
            set;
        }


        /// <summary>
        /// 上级状态(直接上线)
        /// </summary>
        public int onestate
        {
            get;
            set;
        }


        /// <summary>
        /// 上上级编号
        /// </summary>
        public int twogradeID
        {
            get;
            set;
        }

        /// <summary>
        /// 上上openid(直接上线)
        /// </summary>
        public string twoopenid
        {
            get;
            set;
        }

        /// <summary>
        /// 上上级名称(直接上线)
        /// </summary>
        public string twoName
        {
            get;
            set;
        }


        /// <summary>
        /// 上上级状态(直接上线)
        /// </summary>
        public int twostate
        {
            get;
            set;
        }

        /// <summary>
        /// 上上上级编号
        /// </summary>
        public int thressgradeID
        {
            get;
            set;
        }

        /// <summary>
        /// 上上上级openid(直接上线)
        /// </summary>
        public string thressopenid
        {
            get;
            set;
        }


        /// <summary>
        /// 上上上级名称(直接上线)
        /// </summary>
        public string thressName
        {
            get;
            set;
        }


        /// <summary>
        /// 上上上级状态(直接上线)
        /// </summary>
        public int thressstate
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
        /// 未用
        /// </summary>
        public int reveint3
        {
            get;
            set;
        }

        /// <summary>
        /// 未用
        /// </summary>
        public int reveint4
        {
            get;
            set;
        }

        /// <summary>
        /// 未用
        /// </summary>
        public string revevar1
        {
            get;
            set;
        }

        /// <summary>
        /// 未用
        /// </summary>
        public string revevar2
        {
            get;
            set;
        }

        /// <summary>
        /// 未用
        /// </summary>
        public string revevar3
        {
            get;
            set;
        }

        /// <summary>
        /// 未用
        /// </summary>
        public string revevar4
        {
            get;
            set;
        }

    }
}

