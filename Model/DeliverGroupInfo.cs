using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    public class DeliverGroupInfo
    {
        private int _id;
        private string _classname;
        private int _depth;
        private int _status;
        private int _priority;
        private int _parentid;
        private int _isdel;
        private string _pic;
        private string _hovepic;

        /// <summary>
        /// 备用字段
        /// </summary>
        public string hovepic
        {
            set { _hovepic = value; }
            get { return _hovepic; }
        }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string pic
        {
            set { _pic = value; }
            get { return _pic; }
        }
        /// <summary>
        ///
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 群组名称
        /// </summary>
        public string classname
        {
            set { _classname = value; }
            get { return _classname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Depth
        {
            set { _depth = value; }
            get { return _depth; }
        }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int Priority
        {
            set { _priority = value; }
            get { return _priority; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Parentid
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int isDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }

        /// <summary>
        /// 配送员列表 
        /// </summary>
        public IList<DeliverInfo> deliverlst
        {
            set;
            get;
        }

        /// <summary>
        /// 城市名称
        /// </string>
        public string cityname
        {
            get;
            set;
        }

    }
}
