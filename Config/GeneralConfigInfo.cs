using System;

namespace Hangjing.Config
{
    /// <summary>
    /// 网站基本设置描述类, 加[Serializable]标记为可序列化
    /// </summary>
    [Serializable]
    public class GeneralConfigInfo : IConfigInfo
    {
        /// <summary>
        /// 网站域名
        /// </summary>
        public string URL
        {
            get;
            set;
        }

        /// <summary>
        /// 媒体资源
        /// </summary>
        public string HeadWord
        {
            get;
            set;
        }

        /// <summary>
        /// api 信息
        /// </summary>
        public string AppId
        {
            get;
            set;
        }

        /// <summary>
        /// api 信息
        /// </summary>
        public string AppSecret
        {
            get;
            set;
        }
        
        /// <summary>
        /// 电话订餐电话
        /// </summary>
        public string phone
        {
            get;
            set;
        }
                
        /// <summary>
        /// 催餐电话
        /// </summary>
        public string orderphone
        {
            get;
            set;
        }

        
        

    }
}
