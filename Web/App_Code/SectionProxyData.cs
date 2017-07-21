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
    /// 缓存网站城市
    /// </summary>
    /// <returns></returns>
    public static IList<CityInfo> GetCityList()
    {
        City dal = new City();
        IList<CityInfo> list = (IList<CityInfo>)EasyEatCache.GetCacheService().RetrieveObject("/city");
        if (list == null || list.Count == 0)
        {
            list = dal.GetAllCity();
            EasyEatCache.GetCacheService().AddObject("/city", list);
        }
        return list;
    }

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
    /// 缓存分类
    /// </summary>
    /// <returns></returns>
    public static IList<ShopDataInfo> GetSortList()
    {
        ShopData dal = new ShopData();
        IList<ShopDataInfo> list = (IList<ShopDataInfo>)EasyEatCache.GetCacheService().RetrieveObject("/sort");
        if (list == null || list.Count == 0)
        {
            Hangjing.AppLog.AppLog.Info("分类缓存 无");
            list = dal.GetsubList(0);
            EasyEatCache.GetCacheService().AddObject("/sort", list);
        }
        else
        {
            Hangjing.AppLog.AppLog.Info("分类缓存 有");
        }
        return list;
    }

    /// <summary>
    /// 缓存系统参数
    /// </summary>
    /// <returns></returns>
    public static string GetSetValue(int id)
    {
        return CacheHelper.GetSetValue(id);
    }

    /// <summary>
    /// 清除参数缓存
    /// </summary>
    public static void ClearWebSet()
    {
        CacheHelper.UpdateCacheByKey("syspara");
        EasyEatCache.GetCacheService().RemoveObject("/syspara");
    }

    /// <summary>
    /// 缓存所有友情链接
    /// </summary>
    /// <returns></returns>
    public static IList<LinksInfo> GetLinkList()
    {
        Links dal = new Links();
        IList<LinksInfo> list = (IList<LinksInfo>)EasyEatCache.GetCacheService().RetrieveObject("/LinkList");
        if (list == null || list.Count == 0)
        {
            list = dal.GetList(100, 1, "", "Introduce", 1);
            EasyEatCache.GetCacheService().AddObject("/LinkList", list);
        }
        return list;
    }

    /// <summary>
    /// 清除缓存所有友情链接
    /// </summary>
    public static void ClearLinkList()
    {
        EasyEatCache.GetCacheService().RemoveObject("/LinkList");
    }

    /// <summary>
    /// 缓存首页动态
    /// </summary>
    /// <returns></returns>
    public static IList<NewsInfo> GetIndexNewsList()
    {
        News dal = new News();
        IList<NewsInfo> list = (IList<NewsInfo>)EasyEatCache.GetCacheService().RetrieveObject("/IndexNewsList");
        if (list == null || list.Count == 0)
        {
            list = dal.GetList(3, 1, "", "SortNum", 1);
            EasyEatCache.GetCacheService().AddObject("/IndexNewsList", list);
        }
        return list;
    }

    /// <summary>
    /// 清除首页指南缓存
    /// </summary>
    public static void ClearIndexNewsList()
    {
        EasyEatCache.GetCacheService().RemoveObject("/IndexNewsList");
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

    /// <summary>
    /// 清除缓存ppt
    /// </summary>
    public static void ClearPPTList()
    {
        //同时需要情况微信版中的缓存
        CacheHelper.UpdateCacheByKey("/PPTList");
        EasyEatCache.GetCacheService().RemoveObject("/PPTList");
    }

    /// <summary>
    /// 清除缓存首页链接
    /// </summary>
    public static void Clearindexlinklist()
    {
        //同时需要情况微信版中的缓存
        CacheHelper.UpdateCacheByKey("/indexlinklist");
        EasyEatCache.GetCacheService().RemoveObject("/indexlinklist");
    }


    /// <summary>
    /// 缓存支付信息
    /// </summary>
    /// <returns></returns>
    public static IList<acountInfo> GetOnlinePayTypeList()
    {
        return CacheHelper.GetOnlinePayTypeList();
    }

    /// <summary>
    /// 清除支付信息缓存
    /// </summary>
    public static void ClearOnlinePayTypeList()
    {
        CacheHelper.UpdateCacheByKey("OnlinePayTypeList");
        EasyEatCache.GetCacheService().RemoveObject("/OnlinePayTypeList");
    }

    /// <summary>
    /// 获取一个在线支付方式信息
    /// </summary>
    /// <param name="dataid"></param>
    /// <returns></returns>
    public static acountInfo getOnlinePayType(int dataid)
    {
        acountInfo model = null;
        IList<acountInfo> list = GetOnlinePayTypeList();
        foreach (acountInfo item in list)
        {
            if (item.DataID == dataid)
            {
                return item;
                break;
            }
        }
        return model;
    }

    /// <summary>
    /// 缓存用户等级
    /// </summary>
    /// <returns></returns>
    public static IList<VipGradeInfo> GetUserGradeList()
    {
        VipGrade dal = new VipGrade();
        IList<VipGradeInfo> list = (IList<VipGradeInfo>)EasyEatCache.GetCacheService().RetrieveObject("/UserGradeList");
        if (list == null || list.Count == 0)
        {
            list = dal.GetAll();
            EasyEatCache.GetCacheService().AddObject("/UserGradeList", list);
        }
        return list;
    }

    /// <summary>
    /// 清除用户缓存
    /// </summary>
    public static void ClearUserGradeList()
    {
        EasyEatCache.GetCacheService().RemoveObject("/UserGradeList");
    }

    /// <summary>
    /// 缓存此角色的权限信息
    /// /Permissions/RolePermissionsbyrid" + rid;
    /// </summary>
    /// rid 角色编号
    /// <returns></returns>
    public static IList<sys_RolePermissionInfo> GetRolePermissions(string rid)
    {
        if (rid == "")
        {
            return new List<sys_RolePermissionInfo>();
        }
        IList<sys_RolePermissionInfo> list = (IList<sys_RolePermissionInfo>)Hangjing.Cache.EasyEatCache.GetCacheService().RetrieveObject("/Permissions/RolePermissionsbyrid" + rid);
        if (list == null || list.Count == 0)
        {
            sys_RolePermission dal = new sys_RolePermission();
            string sql = "P_RoleID=" + rid;
            list = dal.GetList(1000, 1, sql, "PermissionID", 1);
            Hangjing.Cache.EasyEatCache.GetCacheService().AddObject("/Permissions/RolePermissionsbyrid" + rid, list);
        }
        return list;
    }

    /// <summary>
    /// 缓存群组管理
    /// </summary>
    /// <returns></returns>
    public static IList<DeliverGroupInfo> GetEdelivergroupList()
    {
        DeliverGroup dal = new DeliverGroup();
        IList<DeliverGroupInfo> list = (IList<DeliverGroupInfo>)EasyEatCache.GetCacheService().RetrieveObject("/Edelivergroup");
        if (list == null || list.Count == 0)
        {
            list = dal.GetAll();
            EasyEatCache.GetCacheService().AddObject("/Edelivergroup", list);
        }
        return list;
    }

    /// <summary>
    /// 清除缓存群组管理
    /// </summary>
    public static void ClearEdelivergroupList()
    {
        EasyEatCache.GetCacheService().RemoveObject("/Edelivergroup");
    }

    /// <summary>
    /// 缓存群组管理
    /// </summary>
    /// <returns></returns>
    public static IList<DeliverInfo> GetDeliverList()
    {
        return CacheHelper.GetDeliverList();
    }

    /// <summary>
    /// 缓存热门团购广告
    /// </summary>
    /// <returns></returns>
    public static IList<SortAdInfo> GetWordAdlist()
    {
        SortAd dal = new SortAd();
        IList<SortAdInfo> list = (IList<SortAdInfo>)EasyEatCache.GetCacheService().RetrieveObject("/WordAdlist");
        if (list == null || list.Count == 0)
        {
            list = dal.GetAll();
            EasyEatCache.GetCacheService().AddObject("/WordAdlist", list);
        }
        return list;
    }

    /// <summary>
    /// 清除热门团购广告
    /// </summary>
    public static void ClearWordAdlist()
    {
        EasyEatCache.GetCacheService().RemoveObject("/WordAdlist");
    }


    /// <summary>
    /// 清除商家坐标缓存
    /// </summary>
    public static void ShopLocallisClear()
    {
        CacheHelper.UpdateCacheByKey("ShopLocallist");
        EasyEatCache.GetCacheService().RemoveObject("/ShopLocallist");
    }

    /// <summary>
    /// 清除微信配置缓存
    /// </summary>
    public static void ClearWeiXinAccount()
    {
        CacheHelper.UpdateCacheByKey("WeiXinAccount");
        EasyEatCache.GetCacheService().RemoveObject("/WeiXinAccount");
    }

    /// <summary>
    /// 清除自动调度配置缓存
    /// </summary>
    public static void ClearAutodispatchconfig()
    {
        CacheHelper.UpdateCacheByKey("Autodispatchconfiglist");
        EasyEatCache.GetCacheService().RemoveObject("/Autodispatchconfiglist");
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
            list = dal.GetsubList(0,2);
            EasyEatCache.GetCacheService().AddObject("/STemplate", list);
        }
        return list;
    }

    /// <summary>
    /// 清除热门团购广告
    /// </summary>
    public static void ClearPayModel()
    {
        CacheHelper.UpdateCacheByKey("StateConfig");
        EasyEatCache.GetCacheService().RemoveObject("/StateConfig");
    }


    /// <summary>
    /// 跑腿服务类型
    /// </summary>
    /// <returns></returns>
    public static IList<STemplateInfo> GetExpressServeList()
    {
        STemplate dal = new STemplate();
        IList<STemplateInfo> list = (IList<STemplateInfo>)EasyEatCache.GetCacheService().RetrieveObject("/expressserve");
        if (list == null || list.Count == 0)
        {
            list = dal.GetsubList(0,1);
            EasyEatCache.GetCacheService().AddObject("/expressserve", list);
        }
        return list;
    }
}
