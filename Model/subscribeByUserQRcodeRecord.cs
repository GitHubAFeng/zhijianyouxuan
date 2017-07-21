using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Hangjing.Model
{
    /// <summary>
    /// 用户通过代参数的二维码扫描关注记录
    /// </summary>
    public class subscribeByUserQRcodeRecordInfo
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
        /// 用户openid
        /// </summary>
        public string openid
        {
            get;
            set;
        }

        /// <summary>
        /// 用户微信昵称
        /// </summary>
        public string nickname
        {
            get;
            set;
        }

        /// <summary>
        /// 上张用户编号
        /// </summary>
        public int puserid
        {
            get;
            set;
        }

        /// <summary>
        /// 关注时间
        /// </summary>
        public DateTime addtime
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

        /// <summary>
        /// revevar2
        /// </summary>
        public string revevar2
        {
            get;
            set;
        }

    }
}

