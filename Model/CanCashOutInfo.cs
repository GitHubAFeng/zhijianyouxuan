/*********************************************************************
 * CopyRight (c) 2009-2014 HangJing Teconology. All Rights Reserved.
 * FileName : 
 * Function : 
 * Created by jijunjian at 2015-06-02 14:57:28.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Hangjing.Model;

namespace Hangjing.Model
{
    /// <summary>
    /// 可提现实体
    /// </summary>
    public class CanCashOutInfo
    {
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal AllMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 冻结金额
        /// </summary>
        public decimal nousemoney
        {
            get;
            set;
        }

        /// <summary>
        /// 可提现金额
        /// </summary>
        public decimal usemoney
        {
            get;
            set;
        }
    }
}
