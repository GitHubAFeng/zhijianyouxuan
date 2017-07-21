/*********************************************************************
 * CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved.
 * Function : 公告实体类
 * Created by jijunjian at 2010-8-5 17:52:08.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    public class NewsInfo
    {
        private int _dataid;
        private string _title;
        private DateTime _posttime;
        private int _sortnum;
        private string _econtent;
        private int _reve1;
        private string _reve2;
        /// <summary>
        /// 
        /// </summary>
        public int DataID
        {
            set { _dataid = value; }
            get { return _dataid; }
        }
        /// <summary>
        /// 公告标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime Posttime
        {
            set { _posttime = value; }
            get { return _posttime; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum
        {
            set { _sortnum = value; }
            get { return _sortnum; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string EContent
        {
            set { _econtent = value; }
            get { return _econtent; }
        }
        /// <summary>
        /// 备用
        /// </summary>
        public int reve1
        {
            set { _reve1 = value; }
            get { return _reve1; }
        }
        /// <summary>
        /// 备用
        /// </summary>
        public string Reve2
        {
            set { _reve2 = value; }
            get { return _reve2; }
        }
    }
}

