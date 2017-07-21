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
    /// 实体类ProductAttributes(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class FoodAttributesInfo
    {
        private int _dataid;
        private int _foodtid;
        private string _title;
        private int _selecttype;
        private string _attributes;
        private int _inve1;
        private string _inve2;
        private string _name;

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
            set { _foodtid = value; }
            get { return _foodtid; }
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
        /// 选择类型 0 单选 1多选
        /// </summary>
        public int SelectType
        {
            set { _selecttype = value; }
            get { return _selecttype; }
        }
        /// <summary>
        /// 属性(使用分隔符进行分割）
        /// </summary>
        public string Attributes
        {
            set { _attributes = value; }
            get { return _attributes; }
        }
        /// <summary>
        /// 是否必选0表示不是，1表示是
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

        /// <summary>
        /// 属性选项
        /// </summary>
        public IList<attritem> attritems
        {
            set;
            get;
        }
    }
}
