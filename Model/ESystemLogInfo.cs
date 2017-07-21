using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 实体类ESystemLog 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ESystemLogInfo
    {
        private int? _id;
        private DateTime? _datatime;
        private string _logtype;
        private string _usser;
        private string _message;
        /// <summary>
        /// 
        /// </summary>
        public int? Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DataTime
        {
            set { _datatime = value; }
            get { return _datatime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LogType
        {
            set { _logtype = value; }
            get { return _logtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Usser
        {
            set { _usser = value; }
            get { return _usser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            set { _message = value; }
            get { return _message; }
        }
    }
}
