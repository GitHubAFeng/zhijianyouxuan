#region license
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :礼品类别信息
// Created by tuhui at 2010-6-22 10:47:48.
// E-Mail: tuhui@ihangjing.com
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 礼品类别信息 Model
    /// </summary>
    [Serializable]
    public class GiftsClassInfo
    {
        private int _classid;
        private string _classname;
        private string _parentid;
        private int _classorder;

        public GiftsClassInfo()
        { }

        public GiftsClassInfo(int classid,string classname,string parentid,int classorder)
        {
            this._classid = classid;
            this._classname = classname;
            this._classorder = classorder;
            this._parentid = parentid;
        }

        /// <summary>
        /// 礼品类别编号
        /// </summary>
        public int ClassId
        {
            set { _classid = value; }
            get { return _classid; }
        }

        /// <summary>
        /// 礼品类别名称
        /// </summary>
        public string ClassName
        {
            set { _classname = value; }
            get { return _classname; }
        }

        /// <summary>
        /// 父类别编号
        /// </summary>
        public string ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int ClassOrder
        {
            set { _classorder = value; }
            get { return _classorder; }
        }
    }
}
