using System;
using System.Collections.Generic;
namespace Hangjing.Model
{
    /// <summary>
    /// 实体类aboutClass 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class aboutClassInfo
    {
        private int _id;
        private string _name;
        private int _parentid;
        private int _fullid;

        private IList<aboutusInfo> _glist;

        /// <summary>
        /// 文章列表
        /// </summary>
        public IList<aboutusInfo> glist
        {
            set { _glist = value; }
            get { return _glist; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 排序（降序）
        /// </summary>
        public int FullId
        {
            set { _fullid = value; }
            get { return _fullid; }
        }
    }
}

