using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Hangjing.Model
{
    /// <summary>
    /// 做法实现
    /// </summary>
    [Serializable]
    public class PracticeInfo
    {

        private int _pid;
        private string _pnum;
        private string _pname;
        private string _namepy;
        private int _inve1;
        private string _inve2;
        private int _cityid;
        /// <summary>
        /// 
        /// </summary>
        public int pId
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public string pnum
        {
            set { _pnum = value; }
            get { return _pnum; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string pname
        {
            set { _pname = value; }
            get { return _pname; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public string namepy
        {
            set { _namepy = value; }
            get { return _namepy; }
        }
        /// <summary>
        /// 商家编号
        /// </summary>
        public int Inve1
        {
            set { _inve1 = value; }
            get { return _inve1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public int cityid
        {
            set { _cityid = value; }
            get { return _cityid; }
        }
    }
}

