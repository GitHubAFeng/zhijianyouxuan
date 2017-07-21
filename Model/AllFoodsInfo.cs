
// allfoodsinfo.cs : 菜名库实体
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-04-28
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    public class AllFoodsInfo
    {
        private int m__dataid;
        private string m__foodname;

        /// <summary>
        /// 获取或设置
        /// </summary>
        public int Dataid
        {
            get
            {
                return this.m__dataid;
            }

            set
            {
                this.m__dataid = value;
            }
        }

        /// <summary>
        /// 菜名
        /// </summary>
        public string Foodname
        {
            get
            {
                return this.m__foodname;
            }

            set
            {
                this.m__foodname = value;
            }
        }

    }
}
