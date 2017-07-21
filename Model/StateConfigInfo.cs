using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    public class StateConfigInfo
    {
        private int _id;
        private string _classname;
        private int _depth;
        private int _status;
        private int _priority;
        private int _parentid;
        private int _isdel;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
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
        /// 状态值(和程序对应)
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Priority
        {
            set { _priority = value; }
            get { return _priority; }
        }
        /// <summary>
        /// 父类 1表示订单状态，9表示支付方式，12表示支付状态
        /// </summary>
        public int Parentid
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 0表示可用，1表示不可用。主要是考虑到不用客户，显示不同的。
        /// </summary>
        public int isDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }
    }
}
