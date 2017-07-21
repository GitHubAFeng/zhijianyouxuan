using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Hangjing.Model
{
    /// <summary>
    /// 配送点下商家-打印机关系
    /// </summary>
    public class shop_print_rInfo
    {

        private int _rid;
        /// <summary>
        /// rid
        /// </summary>
        public int rid
        {
            get { return _rid; }
            set { _rid = value; }
        }

        private int _shopid;
        /// <summary>
        /// 商家编号：points.unid
        /// </summary>
        public int shopid
        {
            get { return _shopid; }
            set { _shopid = value; }
        }

        private int _pid;
        /// <summary>
        /// ETogoPrinter.dataid
        /// </summary>
        public int pid
        {
            get { return _pid; }
            set { _pid = value; }
        }

        private int _reveint;
        /// <summary>
        /// 配送点编号
        /// </summary>
        public int ReveInt
        {
            get { return _reveint; }
            set { _reveint = value; }
        }

        private string _revevar;
        /// <summary>
        /// 打印机编号(对应：ETogoPrinter.PrinterSn)
        /// </summary>
        public string ReveVar
        {
            get { return _revevar; }
            set { _revevar = value; }
        }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string TogoName
        {
            set;
            get;
        }

    }
}

