using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Hangjing.Model;

// CacheProvider.cs:缓存提供者 实现ICacheProvider接口
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-04-07

public class CacheProvider : ICacheProvider
{

    //private IList<ETogoInfo> _togo_list;

    //private IList<EFoodInfo> _togofood_list;

    //private MetaInfo _meta_info;

    //private DataTable _building_datatable;            //标志建筑物

    //private Dictionary<int, ETogoInfo> _togoCache;    //点餐商家缓存

    //private Dictionary<int, int> _togoCacheUsage;    //点餐商家缓存命中个数 保存某一个商家的访问次数

    //private DataTable _togoNameId;       //商家名称 编号库  商家名称自动补全

    //private DataTable _marketNameId;     //超市名称 编号库 超市名称自动补全

    //private DataTable _customerDB;       //来电客户的信息 客服下订单时需要使用

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        //_togo_list = new List<ETogoInfo>(8);

        //_togofood_list = new List<EFoodInfo>(8);

        //_meta_info = new MetaInfo();

        //_building_datatable = new DataTable("EBuildingInfo");

        //_togoNameId = new DataTable("TogoNameId");

        //_customerDB = new DataTable("CustomerDB");

        //_marketNameId = new DataTable("MarketNameId");

        //// Initialize togo item cache and useage
        //_togoCache = new Dictionary<int, ETogoInfo>(200);

        //_togoCacheUsage = new Dictionary<int, int>(200);
    }

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


    #region ICacheProvider 成员

    public DataTable GetTogoLocalInfo()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region ICacheProvider 成员

    DataTable ICacheProvider.GetTogoLocalInfo()
    {
        throw new NotImplementedException();
    }

    #endregion
}
