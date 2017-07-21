using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// PrinterSecretInfo.cs:打印机库实体.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-07-13

namespace Hangjing.Model
{
    /// <summary>
    /// 打印机信息实体
    /// </summary>
    public class PrinterSecretInfo
    {
        private int _dataid;
        private string _printernum;
        private string _printersn;
        private string _printerkey;
        private string _firstsn;
        private int _isuse;

        /// <summary>
        /// 
        /// </summary>
        public int DataId
        {
            set { _dataid = value; }
            get { return _dataid; }
        }

        /// <summary>
        /// 编号（打印机上贴的标签编号）
        /// </summary>
        public string PrinterNum
        {
            set { _printernum = value; }
            get { return _printernum; }
        }

        /// <summary>
        /// 序列号
        /// </summary>
        public string PrinterSn
        {
            set { _printersn = value; }
            get { return _printersn; }
        }

        /// <summary>
        /// 密钥
        /// </summary>
        public string PrinterKey
        {
            set { _printerkey = value; }
            get { return _printerkey; }
        }

        /// <summary>
        /// 最初序列号(追踪打印机用)
        /// </summary>
        public string FirstSn
        {
            set { _firstsn = value; }
            get { return _firstsn; }
        }

        /// <summary>
        /// 是否在使用
        /// </summary>
        public int IsUse
        {
            set { _isuse = value; }
            get { return _isuse; }
        }
    }
}
