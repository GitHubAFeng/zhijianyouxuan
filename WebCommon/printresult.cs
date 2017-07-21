using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.WebCommon
{
    /// <summary>
    /// 打印请求结果
    /// </summary>
    public class printresult
    {
        /// <summary>
        /// 状态，0表示正常，其他表示失败
        /// </summary>
        public int responseCode
        {
            set;
            get;
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string msg
        {
            set;
            get;
        }

        /// <summary>
        /// 数据信息
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string orderindex
        {
            set;
            get;
        }
    }
}
