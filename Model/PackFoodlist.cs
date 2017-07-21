using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace Hangjing.Model
{
    ///<summary>
    ///套餐中的商品实体
    ///<summary>
    public class PackFoodlistInfo
    {

        private int _mid;
        /// <summary>
        /// mID
        /// </summary>
        public int mID
        {
            get { return _mid; }
            set { _mid = value; }
        }

        private int _shopid;
        /// <summary>
        /// 商家编号
        /// </summary>
        public int shopid
        {
            get { return _shopid; }
            set { _shopid = value; }
        }

        private int _pid;
        /// <summary>
        /// 套餐编号，
        /// </summary>
        public int pid
        {
            get { return _pid; }
            set { _pid = value; }
        }

        private string _foodname;
        /// <summary>
        /// 商品名称
        /// </summary>
        public string foodname
        {
            get { return _foodname; }
            set { _foodname = value; }
        }

        private int _fid;
        /// <summary>
        /// 菜品
        /// </summary>
        public int fid
        {
            get { return _fid; }
            set { _fid = value; }
        }

        private int _foodcount;
        /// <summary>
        /// 份数
        /// </summary>
        public int foodcount
        {
            get { return _foodcount; }
            set { _foodcount = value; }
        }

        private int _sortnum;
        /// <summary>
        /// 排序，现在未用
        /// </summary>
        public int sortnum
        {
            get { return _sortnum; }
            set { _sortnum = value; }
        }

        private int _sid;
        /// <summary>
        /// 未用
        /// </summary>
        public int sid
        {
            get { return _sid; }
            set { _sid = value; }
        }

        private string _revevar;
        /// <summary>
        /// 价格
        /// </summary>
        public string ReveVar
        {
            get { return _revevar; }
            set { _revevar = value; }
        }

        private string _revevar1;
        /// <summary>
        /// 未用
        /// </summary>
        public string ReveVar1
        {
            get { return _revevar1; }
            set { _revevar1 = value; }
        }

        private string _picture;
        /// <summary>
        /// 菜品图片
        /// </summary>
        public string picture
        {
            get { return _picture; }
            set { _picture = value; }
        }


    }
}

