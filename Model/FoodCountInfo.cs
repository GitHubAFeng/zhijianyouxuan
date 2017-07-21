using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    public class FoodCountInfo
    {
        private int _foodunid;
        private int _count;
        private string _foodname;

        /// <summary>
        /// 商品编号
        /// </summary>
        public int FoodUnid
        {
            set { _foodunid = value; }
            get { return _foodunid; }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string FoodName
        {
            set { _foodname = value; }
            get { return _foodname; }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count
        {
            set { _count = value; }
            get { return _count; }
        }
    }
}
