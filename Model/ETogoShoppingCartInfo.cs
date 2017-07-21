using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ETogoShoppingCartInfo:在线点餐购物车数据
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-03-22

namespace Hangjing.Model
{
    [Serializable]
    public class ETogoShoppingCartInfo
    {
        private int _dataid;
        private int _uid;
        private int _togoid;
        private string _togoname;
        private int _pid;
        private string _pname;
        private decimal _pprice;
        private int _pnum;
        private string _remark;
        private decimal _foodcurrentpric;
        private decimal _currentprice;

        private decimal _allprice;
        private string _picture;

        /// <summary>
        /// 编号
        /// </summary>
        public int DataId
        {
            set { _dataid = value; }
            get { return _dataid; }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UId
        {
            set { _uid = value; }
            get { return _uid; }
        }

        /// <summary>
        /// 商家ID
        /// </summary>
        public int TogoId
        {
            set { _togoid = value; }
            get { return _togoid; }
        }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string TogoName
        {
            set { _togoname = value; }
            get { return _togoname; }
        }

        /// <summary>
        /// 餐品编号
        /// </summary>
        public int PId
        {
            set { _pid = value; }
            get { return _pid; }
        }

        /// <summary>
        /// 餐品名称
        /// </summary>
        public string PName
        {
            set { _pname = value; }
            get { return _pname; }
        }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal PPrice
        {
            set { _pprice = value; }
            get { return _pprice; }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int PNum
        {
            set { _pnum = value; }
            get { return _pnum; }
        }
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        public decimal Foodcurrentprice
        {
            set { _foodcurrentpric = value; }
            get { return _foodcurrentpric; }
        }

        public decimal Currentprice
        {
            get { return _currentprice; }
            set { _currentprice = value; }
        }


        //商品总价
        public decimal AllPrice
        {
            get { return (PPrice+addprice) * PNum; }
        }

        //菜品图片
        public string Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }

        /// <summary>
        /// 临时编码 guid
        /// </summary>
        public string tempcode
        {
            set;
            get;
        }


        /// <summary>
		/// 未用
        /// </summary>
        public string Funit
        {
            get;
            set;
        }

        /// <summary>
        /// 未用
        /// </summary>
        public string material
        {
            get;
            set;
        }

        /// <summary>
        /// 规格编号
        /// </summary>
        public int sid
        {
            get;
            set;
        }

        /// <summary>
        /// 打包费
        /// </summary>
        public decimal owername
        {
            get;
            set;
        }

        /// <summary>
        /// 加价
        /// </summary>
        public decimal addprice
        {
            get;
            set;
        }

        /// <summary>
        /// 规格名称
        /// </summary>
        public string sname
        {
            get;
            set;
        }

        /// <summary>
        /// 未用
        /// </summary>
        public int ReveInt1
        {
            get;
            set;
        }

        /// <summary>
        /// 未用
        /// </summary>
        public int ReveInt2
        {
            get;
            set;
        }

        /// <summary>
        /// 未用
        /// </summary>
        public string ReveVar1
        {
            get;
            set;
        }

        /// <summary>
        /// 未用
        /// </summary>
        public string ReveVar2
        {
            get;
            set;
        }

    }
}
