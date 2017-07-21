#region license
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :产品属性表
// Created by wanghui at 2010-6-22 10:23:25.
// E-Mail: wanghui@ihangjing.com
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 实体类FoodStyleInfo(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class FoodStyleInfo
    {
        private int _dataid;
        private int _foodid;
        private string _title;
        private decimal _price;
        private decimal _markeyprice;
        private int _inuser;
        private int _salesum;
        private int _maxperday;
        private string _intro;
        private int _inve1;
        private string _inve2;
        private string _name;
        private string _pic;

        /// <summary>
        /// 产品图片
        /// </summary>
        public string Pic
        {
            get { return _pic; }
            set { _pic = value; }
        }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
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
        /// 商品编号
        /// </summary>
        public int FoodtId
        {
            set { _foodid = value; }
            get { return _foodid; }
        }
        /// <summary>
        /// 规格名称
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 现在价格
        /// </summary>
        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 市场价格
        /// </summary>
        public decimal MarkeyPrice
        {
            set { _markeyprice = value; }
            get { return _markeyprice; }
        }
        /// <summary>
        /// 是否在销售 0 未销售/1在销售
        /// </summary>
        public int InUser
        {
            set { _inuser = value; }
            get { return _inuser; }
        }
        /// <summary>
        /// 库存
        /// </summary>
        public int SaleSum
        {
            set { _salesum = value; }
            get { return _salesum; }
        }
        /// <summary>
        /// 每日最大供应量
        /// </summary>
        public int MaxPerDay
        {
            set { _maxperday = value; }
            get { return _maxperday; }
        }
        /// <summary>
        /// 简介
        /// </summary>
        public string Intro
        {
            set { _intro = value; }
            get { return _intro; }
        }
        /// <summary>
        /// 自增属性1
        /// </summary>
        public int Inve1
        {
            set { _inve1 = value; }
            get { return _inve1; }
        }
        /// <summary>
        /// 自增属性2
        /// </summary>
        public string Inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }
    }
}
