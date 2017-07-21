using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Common
{
    ///zjf@ihangjing.com 添加注释
    ///.net容器类 
    ///键/值对结构 和 SortedDictionary<> (相当于 Key 能自动排序 Dictionary<>)功能相似, 但内部算法不同, 其 Keys、Values 可通过索引访问
    /// <summary>
    /// ShopTypeList泛型类 保存商家分类信息 格式：分类编号 分类名称
    /// 赋值
    /// topictypeList = new ShopTypeList<int, string>();
    /// topictypeList.Add(TypeConverter.ObjectToInt(dr["typeid"]), dr["name"].ToString());
    /// 使用
    /// ShopTypeList<int, string> topictypearray = GetShopTypeArray();
    /// typictypename = topictypearray[Int32.Parse(dr["typeid"].ToString())];
    /// </summary>
    /// <typeparam name="T">占位符(下同)</typeparam>
    [Serializable]
    public class ShopTypeSortedList<TKey, TValue> : System.Collections.Generic.SortedList<TKey, TValue>, IHangjingCollection<KeyValuePair<TKey, TValue>>
    {
        public ShopTypeSortedList()
            : base()
        { }

        public ShopTypeSortedList(IComparer<TKey> comparer)
            : base(comparer)
        { }

        public ShopTypeSortedList(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        { }

        public ShopTypeSortedList(int capacity)
            : base(capacity)
        { }

        public ShopTypeSortedList(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
            : base(dictionary, comparer)
        { }

        public ShopTypeSortedList(int capacity, IComparer<TKey> comparer)
            : base(capacity, comparer)
        { }

        public object SyncRoot
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this.Count == 0;
            }
        }

        private int _fixedsize = default(int);

        /// <summary>
        /// 固定大小属性
        /// </summary>
        public int FixedSize
        {
            get
            {
                return _fixedsize;
            }
            set
            {
                _fixedsize = value;
            }
        }

        /// <summary>
        /// 是否已满
        /// </summary>
        public bool IsFull
        {
            get
            {
                if ((FixedSize != default(int)) && (this.Count >= FixedSize))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get
            {
                return "1.0";
            }
        }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author
        {
            get
            {
                return "IHangjing";
            }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }


        /// <summary>
        /// 追加元素
        /// </summary>
        /// <param name="value"></param>
        public new void Add(TKey tkey, TValue tvalue)
        {
            if (!this.IsFull)
            {
                base.Add(tkey, tvalue);
            }
        }


        /// <summary>
        /// 比较对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("当前数据为空");
            }

            if (obj.GetType() == this.GetType())
            {
                ShopTypeSortedList<TKey, TValue> list = obj as ShopTypeSortedList<TKey, TValue>;
                return this.Count.CompareTo(list.Count);
            }
            else
            {
                return this.GetType().FullName.CompareTo(obj.GetType().FullName);
            }
        }
    }
}
