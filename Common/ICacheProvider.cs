using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

// ICacheProvider.cs:缓存提供者接口  可以实现这个接口的方法来提供不同的缓存方式 留待以后扩展
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-04-07

namespace Hangjing.Common
{
    public interface ICacheProvider
    {

        ///// <summary>
        ///// 获取seo优化缓存
        ///// </summary>
        //MetaInfo GetMetaInfo();

        ///// <summary>
        ///// 设置seo优化缓存
        ///// </summary>
        ///// <param name="Info"></param>
        //void SetMetaInfo(MetaInfo Info);

        ///// <summary>
        ///// 获取首页商家的排行列表 推介
        ///// </summary>
        ///// <returns></returns>
        //IList<ETogoInfo> GetTogoRank();

        ///// <summary>
        ///// 设置推荐商家缓存
        ///// </summary>
        ///// <param name="list"></param>
        //void SetTogoRank(IList<ETogoInfo> list);

        ///// <summary>
        ///// 获取首页餐品排行列表 推介
        ///// </summary>
        ///// <returns></returns>
        //IList<EFoodInfo> GetTogoFoodRank();

        ///// <summary>
        ///// 设置推荐餐品缓存
        ///// </summary>
        ///// <param name="list"></param>
        //void SetTogoFooRank(IList<EFoodInfo> list);

        ///// <summary>
        ///// 清空商家排行列表
        ///// </summary>
        //void ClearTogoRankCache();

        ///// <summary>
        ///// 清空推荐餐品缓存
        ///// </summary>
        //void ClearTogoFoodRankCache();
        ///// <summary>
        ///// 获取建筑物缓存
        ///// </summary>
        ///// <returns></returns>
        //DataTable GetBuilding();

        ///// <summary>
        ///// 设置建筑物缓存
        ///// </summary>
        //void SetBuilding(DataTable dt);

        ///// <summary>
        ///// 清空建筑物缓存
        ///// </summary>
        //void ClearBuilding();

        ///// <summary>
        ///// 获取缓存中点餐商家
        ///// </summary>
        ///// <param name="TogoId"></param>
        ///// <returns></returns>
        //ETogoInfo GetTogoItem(int togoId);

        ///// <summary>
        ///// 设置缓存中的点餐商家
        ///// </summary>
        ///// <param name="togoId"></param>
        ///// <param name="togoInfo"></param>
        //void SetTogoItem(int togoId, ETogoInfo togoInfo);

        ///// <summary>
        ///// 清除点餐商家
        ///// </summary>
        //void ClearTogoItem();

        ///// <summary>
        ///// 对点餐商家的缓存进行裁剪
        ///// </summary>
        ///// <param name="cutSize"></param>
        //void CutTogoCache(int cutSize);

        ///// <summary>
        ///// 移除点餐商家的缓存
        ///// </summary>
        ///// <param name="TogoId"></param>
        //void RemoveTogo(int TogoId);

        ///// <summary>
        ///// 获取已经存在与缓存中的点餐商家个数
        ///// </summary>
        //int TogoCacheUsage
        //{
        //    get;
        //}
    }
}
