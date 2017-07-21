/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * Function : ppt实体
 * Created by jijunjian at 2010-8-21 14:26:16.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    public class PPTInfo
    {
        private int _dataid;
        private string _picture;
        private string _title;
        private string _purl;
        private int _reve1;
        private string _reve2;
        private int _secid;

        /// <summary>
        /// 未用
        /// </summary>
        public int SecID
        {
            set { _secid = value; }
            get { return _secid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DataID
        {
            set { _dataid = value; }
            get { return _dataid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string picture
        {
            set { _picture = value; }
            get { return _picture; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PUrl
        {
            set { _purl = value; }
            get { return _purl; }
        }
        /// <summary>
        /// 排序(大在前)
        /// </summary>
        public int Reve1
        {
            set { _reve1 = value; }
            get { return _reve1; }
        }
        /// <summary>
        /// 显示位置：1：商家列表; 2:微信首页,3:app;
        /// </summary>
        public string Reve2
        {
            set { _reve2 = value; }
            get { return _reve2; }
        }
    }
}
