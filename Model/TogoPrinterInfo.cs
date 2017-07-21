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
    /// 商家打印机关系
    /// </summary>
    public class TogoPrinterInfo
    {
        private int _dataid;
        private int _togoid;
        private string _togonum;
        private string _printersn;
        private int _printpage;
        private string _printtop;
        private string _printfoot;
        private int _isupdate;
        private DateTime _lastlogindate;

        private string _togoname;
        private string _linkman;
        private string _linktel;
        private string _linkadddress;

        private string _printernum;

        /// <summary>
        /// 编号（打印机上贴的标签编号）
        /// </summary>
        public string PrinterNum
        {
            set { _printernum = value; }
            get { return _printernum; }
        }

        public string TogoName
        {
            set { _togoname = value; }
            get { return _togoname; }
        }

        /// <summary>
        /// 聯絡人
        /// </summary>
        public string LinkMan
        {
            set { _linkman = value; }
            get { return _linkman; }
        }

        /// <summary>
        /// 聯絡電話
        /// </summary>
        public string LinkTel
        {
            set { _linktel = value; }
            get { return _linktel; }
        }

        /// <summary>
        /// 聯係地址
        /// </summary>
        public string LinkAddress
        {
            set { _linkadddress = value; }
            get { return _linkadddress; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DataId
        {
            set { _dataid = value; }
            get { return _dataid; }
        }

        /// <summary>
        /// 商店编号
        /// </summary>
        public int TogoId
        {
            set { _togoid = value; }
            get { return _togoid; }
        }

        /// <summary>
        /// sim卡号码(打印机相关联)
        /// </summary>
        public string TogoNum
        {
            set { _togonum = value; }
            get { return _togonum; }
        }

        /// <summary>
        /// 编号（打印机上贴的标签编号） 此处命名一个bug 应该是  打印机表中的 PrinterNum
        /// </summary>
        public string PrinterSn
        {
            set { _printersn = value; }
            get { return _printersn; }
        }

        /// <summary>
        /// 打印联数
        /// </summary>
        public int PrintPage
        {
            set { _printpage = value; }
            get { return _printpage; }
        }

        /// <summary>
        /// 打印抬头
        /// </summary>
        public string PrintTop
        {
            set { _printtop = value; }
            get { return _printtop; }
        }

        /// <summary>
        /// 打印结尾
        /// </summary>
        public string PrintFoot
        {
            set { _printfoot = value; }
            get { return _printfoot; }
        }

        /// <summary>
        /// 是否更新(0 未更新 1 更新)
        /// </summary>
        public int IsUpdate
        {
            set { _isupdate = value; }
            get { return _isupdate; }
        }

        /// <summary>
        /// 最后心跳时间
        /// </summary>
        public DateTime LastLoginDate
        {
            set { _lastlogindate = value; }
            get { return _lastlogindate; }
        }

        /// <summary>
        /// 商家列表
        /// </summary>
        public IList<shop_print_rInfo> ShopList
        {
            set;
            get;
        }

        /// <summary>
        /// 是指打印机的连接状态，包括： 离线 ， 在线 。
        /// </summary>
        public string deviceStatus
        {
            set;
            get;
        }

        /// <summary>
        /// 是指打印纸张的状态，包括 正常 或 缺纸 。
        /// </summary>
        public string paperStatus
        {
            set;
            get;
        }

    }
}
