using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    public class TogoPointSetInfo
    {
        /// <summary>
        /// 实体类TogoPointSet 。(属性说明自动提取数据库字段的描述信息)
        /// </summary>
        private int _dataid;
        private int? _togoid;
        private decimal? _multiple;
        private DateTime? _starttime;
        private DateTime? _endtime;
        private int? _inve1;
        private int? _inve2;
        /// <summary>
        /// 
        /// </summary>
        public int DataId
        {
            set { _dataid = value; }
            get { return _dataid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TogoId
        {
            set { _togoid = value; }
            get { return _togoid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Multiple
        {
            set { _multiple = value; }
            get { return _multiple; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Inve1
        {
            set { _inve1 = value; }
            get { return _inve1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }
    }
}
