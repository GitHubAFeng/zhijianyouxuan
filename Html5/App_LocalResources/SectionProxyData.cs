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
public class SectionProxyData
{
    /// <summary>
    /// 缓存所有区域
    /// </summary>
    /// <returns></returns>
    public static IList<SectionInfo> GetSectionList()
    {
        ESection dal = new ESection();
        IList<SectionInfo> list = (IList<SectionInfo>)EasyEatCache.GetCacheService().RetrieveObject("/section");
        if (list == null || list.Count == 0)
        {
            list = dal.GetAll();
            EasyEatCache.GetCacheService().AddObject("/section", list);
        }
        return list;
    }

    /// <summary>
    /// 缓存分类(外卖)
    /// </summary>
    /// <returns></returns>
    public static IList<ShopDataInfo> GetSortList()
    {
        ShopData dal = new ShopData();
        IList<ShopDataInfo> list = (IList<ShopDataInfo>)EasyEatCache.GetCacheService().RetrieveObject("/sort");
        if (list == null || list.Count == 0)
        {
            //list = dal.GetsubList(0);
            list = dal.GetList(100, 1, "isDel=0", "Priority", 1);
            EasyEatCache.GetCacheService().AddObject("/sort", list);
        }
        return list;
    }

    /// <summary>
    /// 缓存菜系
    /// </summary>
    /// <returns></returns>
    public static IList<ShopDataInfo> GetStyleList()
    {
        ShopData dal = new ShopData();
        IList<ShopDataInfo> list = (IList<ShopDataInfo>)EasyEatCache.GetCacheService().RetrieveObject("/style");
        if (list == null || list.Count == 0)
        {
            list = dal.GetsubList(12);
            EasyEatCache.GetCacheService().AddObject("/style", list);
        }
        return list;
    }


    /// <summary>
    /// 缓存系统参数
    /// </summary>
    /// <returns></returns>
    public static string GetSetValue(int id)
    {
        string rs = "";
        IList<WebBasicInfo> list = (IList<WebBasicInfo>)EasyEatCache.GetCacheService().RetrieveObject("/syspara");
        if (list == null || list.Count == 0)
        {
            WebBasic dal = new WebBasic();
            list = dal.GetAllData("");
            EasyEatCache.GetCacheService().AddObject("/syspara", list,600);
        }
        foreach (WebBasicInfo i in list)
        {
            if (i.DataId == id)
            {
                rs = i.Value;
            }
        }
        return rs;
    }


    /// <summary>
    /// 缓存建筑物
    /// </summary>
    /// <returns></returns>
    public static IList<BuildingInfo> GetBuildingList()
    {
        EBuilding dal = new EBuilding();
        IList<BuildingInfo> list = (IList<BuildingInfo>)EasyEatCache.GetCacheService().RetrieveObject("/BuildingList");
        if (list == null || list.Count == 0)
        {
            list = dal.GetAll();
            EasyEatCache.GetCacheService().AddObject("/BuildingList", list);
        }
        return list;
    }

    /// <summary>
    /// 清除缓存pp
    /// </summary>
    public static void ClearBuildingListList()
    {
        EasyEatCache.GetCacheService().RemoveObject("/BuildingList");
    }

    /// <summary>
    /// 备注快捷选项
    /// </summary>
    /// <returns></returns>
    public static IList<STemplateInfo> GetSTemplateList()
    {
        STemplate dal = new STemplate();
        IList<STemplateInfo> list = (IList<STemplateInfo>)EasyEatCache.GetCacheService().RetrieveObject("/STemplate");
        if (list == null || list.Count == 0)
        {
            list = dal.GetsubList(0);
            EasyEatCache.GetCacheService().AddObject("/STemplate", list);
        }
        return list;
    }
    /// <summary>
    /// 缓存ppt
    /// </summary>
    /// <returns></returns>
    public static IList<PPTInfo> GetPPTList()
    {
        PPT dal = new PPT();
        IList<PPTInfo> list = (IList<PPTInfo>)EasyEatCache.GetCacheService().RetrieveObject("/PPTList");
        if (list == null || list.Count == 0)
        {
            list = dal.GetList(100, 1, "", "Reve1", 1);
            EasyEatCache.GetCacheService().AddObject("/PPTList", list);
        }
        return list;
    }
}
