/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * Function : 区域缓存代理类
 * Created by jijunjian at 2010-8-21 18:53:19.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Web.Caching;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Cache;

/// <summary>
///SectionProxyData 的摘要说明
/// </summary>
public class ProxyData
{
    /// <summary>
    /// 缓存分类
    /// </summary>
    /// <returns></returns>
    public static IList<AdTableInfo> GetWordList()
    {
        AdTable dal = new AdTable();
        IList<AdTableInfo> list = (IList<AdTableInfo>)EasyEatCache.GetCacheService().RetrieveObject("/wordad");
        if (list == null || list.Count == 0)
        {
            list = dal.GetList(3, 1, "tid in (4,5,6)", "tid", 1);
            EasyEatCache.GetCacheService().AddObject("/wordad", list);
        }
        return list;
    }

    /// <summary>
    /// 商家ppt => /shop/shop"+tid+"/ppt
    /// </summary>
    /// <param name="tid"></param>
    /// <returns></returns>
    public static IList<TogoPictureInfo> getTogoPPt(int tid)
    {
        TogoPicture dal = new TogoPicture();
        IList<TogoPictureInfo> list = null;// (IList<TogoPictureInfo>)EasyEatCache.GetCacheService().RetrieveObject("/shop/shop" + tid + "/ppt");
        if (list == null || list.Count == 0)
        {
            string sql = "togoid = " + tid;
            list = dal.GetList(3, 1, sql, "pri", 1);
            EasyEatCache.GetCacheService().AddObject("/shop/shop" + tid + "/ppt", list);
        }
        return list;
    }

    /// <summary>
    /// 商家菜品分类 => /shop/shop"+tid+"/sort
    /// </summary>
    /// <param name="tid"></param>
    /// <returns></returns>
    public static IList<EFoodSortInfo> getTogoSort(int tid)
    {
        EFoodSort dal = new EFoodSort();
        IList<EFoodSortInfo> list = null;// (IList<EFoodSortInfo>)EasyEatCache.GetCacheService().RetrieveObject("/shop/shop" + tid + "/sort");
        if (list == null || list.Count == 0)
        {
            string sql = "togoid = " + tid;
            list = dal.GetListByTogoNum(tid);
            EasyEatCache.GetCacheService().AddObject("/shop/shop" + tid + "/sort", list);
        }
        return list;
    }

    /// <summary>
    /// 商家菜品 => /shop/shop"+tid+"/food
    /// </summary>
    /// <param name="tid"></param>
    /// <returns></returns>
    public static IList<FoodinfoInfo> getTogoFood(int tid)
    {
        Foodinfo dal = new Foodinfo();
        IList<FoodinfoInfo> list = null;// (IList<FoodinfoInfo>)EasyEatCache.GetCacheService().RetrieveObject("/shop/shop" + tid + "/food");
        if (list == null || list.Count == 0)
        {
            string sql = " InUse = 'y' and FPMaster=" + tid + "";
            list = dal.GetList(500, 1, sql, "OrderNum", 1);
            EasyEatCache.GetCacheService().AddObject("/shop/shop" + tid + "/food", list);
        }
        return list;
    }


    /// <summary>
    /// 商家坐标 => /shop/shop"+tid+"/local
    /// </summary>
    /// <param name="tid"></param>
    /// <returns></returns>
    public static ETogoLocalInfo getTogoLocal(int tid)
    {
        ETogoLocal dal = new ETogoLocal();
        ETogoLocalInfo model = (ETogoLocalInfo)EasyEatCache.GetCacheService().RetrieveObject("/shop/shop" + tid + "/local");
        if (model == null || model.DataId == 0)
        {
            model = dal.GetInfoById(tid.ToString());
            EasyEatCache.GetCacheService().AddObject("/shop/shop" + tid + "/local", model);
        }
        return model;
    }

    /// <summary>
    /// 商家信息 => /shop/shop"+tid+"/local
    /// </summary>
    /// <param name="tid"></param>
    /// <returns></returns>
    public static PointsInfo getTogoInfo(int tid)
    {
        Points dal = new Points();
        PointsInfo model = null;//(PointsInfo)EasyEatCache.GetCacheService().RetrieveObject("/shop/shop" + tid + "/info");
        if (model == null || model.Unid == 0)
        {
            model = dal.getsearchList(1, 1, "unid=" + tid, "pop")[0];
            EasyEatCache.GetCacheService().AddObject("/shop/shop" + tid + "/info", model);
        }
        return model;
    }

    /// <summary>
    /// 首页商家 => /shop/index"+id+"
    /// </summary>
    /// <param name="tid"></param>
    /// <returns></returns>
    public static IList<PointsInfo> getTogoIndex(int tid)
    {
        Points dal = new Points();
        //IList<PointsInfo> list = (IList<PointsInfo>)EasyEatCache.GetCacheService().RetrieveObject("/shop/index/index" + tid + "");
        IList<PointsInfo> list = null;
        if (list == null || list.Count == 0)
        {
            string sql = "1=1 and IsDelete = 0 and Star = 1 and  id ='1' and menunum = "+tid+" and ptype = 2 and InUse = 'Y'";
            list = dal.getsearchList(1, 8, sql, "togoad.isRecommend desc ,  togoad.RecommendSort desc ,points.Status desc,havenew desc, Grade asc");
            EasyEatCache.GetCacheService().AddObject("/shop/index/index" + tid + "", list);
        }
        return list;
    }

    /// <summary>
    /// 缓存参数
    /// </summary>
    /// <returns></returns>
    public static IList<PointSetInfo> GetPointSet()
    {
        PointSet dal = new PointSet();
        IList<PointSetInfo> list = (IList<PointSetInfo>)EasyEatCache.GetCacheService().RetrieveObject("/PointSet");
        if (list == null || list.Count == 0)
        {
            list = dal.GetList(100, 1, "", "dataid", 1);
            EasyEatCache.GetCacheService().AddObject("/PointSet", list);
        }
        return list;
    }

    /// <summary>
    /// 获取一个参数
    /// </summary>
    /// <returns></returns>
    public static int  GetPointSet(int id)
    {
        int rs = 0;
        IList<PointSetInfo> list = GetPointSet();
        foreach (PointSetInfo item in list)
        {
            if (id == item.DataId)
            {
                rs = Convert.ToInt32(item.KeyValue);
            }
        }
        return rs;
    }

    /// <summary>
    /// 首页热点商家 => /shop/hot
    /// </summary>
    /// <param name="tid"></param>
    /// <returns></returns>
    public static IList<PointsInfo> getTogohot()
    {
        Points dal = new Points();
        IList<PointsInfo> list = (IList<PointsInfo>)EasyEatCache.GetCacheService().RetrieveObject("/shop/hot");
        if (list == null || list.Count == 0 )
        {
            string sql = "1=1 and IsDelete = 0 and Star = 1 and  id ='1' and ptype = 2 and InUse = 'Y'";
            list = dal.getsearchList(1, 15, sql, "togoad.isRecommend desc ,  togoad.RecommendSort desc ,sortnum");
            EasyEatCache.GetCacheService().AddObject("/shop/hot", list);
        }
        return list;
    }

    /// <summary>
    /// 缓存分类
    /// </summary>
    /// <returns></returns>
    public static IList<AdTableInfo> GetAdList()
    {
        AdTable dal = new AdTable();
        IList<AdTableInfo> list = (IList<AdTableInfo>)EasyEatCache.GetCacheService().RetrieveObject("/AdList");
        if (list == null || list.Count == 0)
        {
            list = dal.GetList(15, 1, "tid >= 9 and tid <= 24", "tid", 1);
            EasyEatCache.GetCacheService().AddObject("/AdList", list);
        }
        return list;
    }

}
