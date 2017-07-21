using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

// CacheProvider.cs:缓存提供者 实现ICacheProvider接口
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-04-07

namespace Hangjing.Common
{
    public class CacheProvider : ICacheProvider
    {

        //private IList<ETogoInfo> _togo_list;

        //private IList<EFoodInfo> _togofood_list;

        //private MetaInfo _meta_info;

        //private DataTable _building_datatable;            //标志建筑物

        //private Dictionary<int, ETogoInfo> _togoCache;    //点餐商家缓存

        //private Dictionary<int, int> _togoCacheUsage;    //点餐商家缓存命中个数 保存某一个商家的访问次数

        ///// <summary>
        ///// 初始化
        ///// </summary>
        //public void Init()
        //{
        //    _togo_list = new List<ETogoInfo>(8);

        //    _togofood_list = new List<EFoodInfo>(8);

        //    _meta_info = new MetaInfo();

        //    _building_datatable = new DataTable("EBuildingInfo");

        //    // Initialize togo item cache and useage
        //    _togoCache = new Dictionary<int, ETogoInfo>(200);

        //    _togoCacheUsage = new Dictionary<int, int>(200);
        //}

        ///// <summary>
        ///// 获取seo优化缓存
        ///// </summary>
        //public MetaInfo GetMetaInfo()
        //{
        //    MetaInfo value = new MetaInfo();

        //    lock (this)
        //    {
        //        value = _meta_info;
        //    }
        //    return value;
        //}

        ///// <summary>
        ///// 设置seo优化缓存
        ///// </summary>
        ///// <param name="Info"></param>
        //public void SetMetaInfo(MetaInfo Info)
        //{
        //    if (Info == null) throw new ArgumentNullException("MetaInfo");

        //    lock (_meta_info)
        //    {
        //        _meta_info = Info;
        //    }
        //}

        ///// <summary>
        ///// 获取首页商家的排行列表 推介
        ///// </summary>
        ///// <returns></returns>
        //public IList<ETogoInfo> GetTogoRank()
        //{
        //    IList<ETogoInfo> list = new List<ETogoInfo>(8);

        //    lock (_togo_list)
        //    {
        //        list = _togo_list;
        //    }

        //    return list;
        //}

        ///// <summary>
        ///// 设置推荐商家缓存
        ///// </summary>
        ///// <param name="list"></param>
        //public void SetTogoRank(IList<ETogoInfo> list)
        //{
        //    if (list == null) throw new ArgumentNullException("IList<ETogoInfo>");

        //    lock (_togo_list)
        //    {
        //        _togo_list = list;
        //    }
        //}

        ///// <summary>
        ///// 获取首页餐品排行列表 推介
        ///// </summary>
        ///// <returns></returns>
        //public IList<EFoodInfo> GetTogoFoodRank()
        //{
        //    IList<EFoodInfo> list = new List<EFoodInfo>(8);

        //    lock (_togofood_list)
        //    {
        //        list = _togofood_list;
        //    }

        //    return list;
        //}

        ///// <summary>
        ///// 设置推荐餐品缓存
        ///// </summary>
        ///// <param name="list"></param>
        //public void SetTogoFooRank(IList<EFoodInfo> list)
        //{
        //    if (list == null) throw new ArgumentNullException("IList<EFoodInfo>");

        //    lock (_togofood_list)
        //    {
        //        _togofood_list = list;
        //    }
        //}

        ///// <summary>
        ///// 清空商家排行列表
        ///// </summary>
        //public void ClearTogoRankCache()
        //{
        //    lock (_togo_list)
        //    {
        //        _togo_list.Clear();
        //    }
        //}

        ///// <summary>
        ///// 清空推荐餐品列表
        ///// </summary>
        //public void ClearTogoFoodRankCache()
        //{
        //    lock (_togofood_list)
        //    {
        //        _togofood_list.Clear();
        //    }
        //}

        ///// <summary>
        ///// 获取建筑物缓存
        ///// </summary>
        ///// <returns></returns>
        //public DataTable GetBuilding()
        //{
        //    DataTable dt = new DataTable("EBuildingInfo");

        //    lock (_building_datatable)
        //    {
        //        dt = _building_datatable;
        //    }

        //    return dt;
        //}

        ///// <summary>
        ///// 设置建筑物缓存
        ///// </summary>
        //public void SetBuilding(DataTable dt)
        //{
        //    lock (_building_datatable)
        //    {
        //        _building_datatable = dt;
        //    }
        //}

        ///// <summary>
        ///// 清空建筑物缓存
        ///// </summary>
        //public void ClearBuilding()
        //{
        //    lock (_building_datatable)
        //    {
        //        _building_datatable.Clear();
        //    }
        //}

        ///// <summary>
        ///// 获取缓存中点餐商家
        ///// </summary>
        ///// <param name="TogoId"></param>
        ///// <returns></returns>
        //public ETogoInfo GetTogoItem(int togoId)
        //{

        //    if (togoId < 0) throw new ArgumentNullException("togoId");

        //    ETogoInfo value = null;
        //    lock (_togoCache)
        //    {
        //        if (_togoCache.TryGetValue(togoId, out value))
        //        {
        //            _togoCacheUsage[togoId]++;
        //        }
        //    }

        //    return value;
        //}

        ///// <summary>
        ///// 设置缓存中的点餐商家
        ///// </summary>
        ///// <param name="togoId"></param>
        ///// <param name="togoInfo"></param>
        //public void SetTogoItem(int togoId, ETogoInfo togoInfo)
        //{
        //    if (togoId < 0 ) throw new ArgumentNullException("togoId");
        //    if (togoInfo == null) throw new ArgumentNullException("togoInfo");

        //    lock (_togoCache)
        //    {
        //        _togoCache[togoId] = togoInfo;
        //        lock (_togoCacheUsage)
        //        {
        //            _togoCacheUsage[togoId] = 0;
        //        }
        //    }
        //}

        ///// <summary>
        ///// 清除点餐商家
        ///// </summary>
        //public void ClearTogoItem()
        //{
        //    lock (_togoCache)
        //    {
        //        _togoCache.Clear();
        //    }
        //    lock (_togoCacheUsage)
        //    {
        //        _togoCacheUsage.Clear();
        //    }
        //}

        ///// <summary>
        ///// 减小点餐商家的缓存大小，移除部分命中率低的商家信息
        ///// </summary>
        ///// <param name="cutSize">需要移除的商家个数</param>
        ///// <exception cref="ArgumentOutOfRangeException">If <b>cutSize</b> is less than or equal to zero.</exception>
        //public void CutTogoCache(int cutSize)
        //{
        //    if (cutSize <= 0) throw new ArgumentOutOfRangeException("cutSize", "Cut Size should be greater than zero");

        //    lock (_togoCache)
        //    {
        //        for (int i = 0; i < cutSize; i++)
        //        {
        //            int key = 0;
        //            int min = int.MaxValue;

        //            // 寻找访问次数最少的点餐商家
        //            foreach (int t in _togoCacheUsage.Keys)
        //            {
        //                if (_togoCacheUsage[t] < min)
        //                {
        //                    key = t;
        //                    min = _togoCacheUsage[t];
        //                }
        //            }

        //            if (key == 0)
        //            {
        //                break;
        //            }

        //            // 移除缓存中的点餐商家
        //            RemoveTogo(key);
        //        }
        //    }
        //}

        ///// <summary>
        ///// 移除一个商家的缓存
        ///// </summary>
        //public void RemoveTogo(int TogoId)
        //{
        //    if (TogoId == null || TogoId < 1) throw new ArgumentNullException("TogoId");

        //    lock (_togoCache)
        //    {
        //        _togoCache.Remove(TogoId);
        //    }
        //    lock (_togoCacheUsage)
        //    {
        //        _togoCacheUsage.Remove(TogoId);
        //    }
        //}

        ///// <summary>
        ///// 获取已经存在缓存中的点餐商家个数
        ///// </summary>
        //public int TogoCacheUsage
        //{
        //    get
        //    {
        //        lock (_togoCacheUsage)
        //        {
        //            return _togoCacheUsage.Count;
        //        }
        //    }
        //}

    }
}
