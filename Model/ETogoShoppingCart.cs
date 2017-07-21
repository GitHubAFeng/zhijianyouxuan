using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ETogoShoppingCart:在线点餐购物车数据 购物车中单个商家的餐品信息
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-03-22
namespace Hangjing.Model
{
    [Serializable]
    public class ETogoShoppingCart
    {
        private int _TogoId;
        private string _TogoName;
        private string _TogoNum;//by jijunjian;
        private IList<ETogoShoppingCartInfo> _ItemList;
        private int _cartstatus;
        private int _sendfree;
        private int _MaxDistance;

        /// <summary>
        /// 送餐费
        /// </summary>
        public int sendfree
        {
            set { _sendfree = value; }
            get { return _sendfree; }
        }


        /// <summary>
        /// 状态主要是，有打印机的商家，如果没开打印机就写-1,其他是0;
        /// </summary>
        public int cartstatus
        {
            set { _cartstatus = value; }
            get { return _cartstatus; }
        }

        /// <summary>
        /// 店铺编号
        /// </summary>
        public int TogoId
        {
            set { _TogoId = value; }
            get { return _TogoId; }
        }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string TogoName
        {
            set { _TogoName = value;}
            get { return _TogoName;}
        }
        /// <summary>
        /// 商家编号下订单时用此.
        /// </summary>
        public string TogoNum
        {
            set
            {
                _TogoNum = value;
            }
            get
            {
                return _TogoNum;
            }
        }
        /// <summary>
        /// 店铺的餐品列表
        /// </summary>
        public IList<ETogoShoppingCartInfo> ItemList
        {
            set { _ItemList = value; }
            get { return _ItemList; }
        }

        /// <summary>
        /// 商家起送价
        /// </summary>
        public int Togoremark
        {
            set;
            get;
        }

        private int _ptimes;

        /// <summary>
        /// 满多少免配送费(0表示不启用 )
        /// </summary>
        public int ptimes
        {
            set { _ptimes = value; }
            get { return _ptimes; }
        }

        private string _lat;
        /// <summary>
        /// 店铺坐标 横坐标
        /// </summary>
        public string Lat
        {
            set { _lat = value; }
            get { return _lat; }
        }

        private string _lng;
        /// <summary>
        /// 店铺坐标 纵坐标
        /// </summary>
        public string Lng
        {
            set { _lng = value; }
            get { return _lng; }
        }

         /// <summary>
        /// 用户，商家坐标
        /// </summary>
        public string latlng
        {
            set;
            get;
        }

        /// <summary>
        /// 配送距离
        /// </summary>
        public int MaxDistance
        {
            set { _MaxDistance = value; }
            get { return _MaxDistance; }
        }

        /// <summary>
        /// 送餐费
        /// </summary>
        public int oldsendfree
        {
            set;
            get;
        }
    }
}
