using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;
/// <summary>
///getCaheData 的摘要说明
/// </summary>
public static class getCaheData
{

    /// <summary>
    /// 获取商家分类
    /// </summary>
    /// <returns></returns>
    public static IList<ShopDataInfo> GetShopData()
    {
        IList<ShopDataInfo> list = (IList<ShopDataInfo>)HttpContext.Current.Cache["shopdata"];
        if (list == null || list.Count == 0)
        {
            list = new ShopData().GetsubList(0);
            HttpContext.Current.Cache["shopdata"] = list;
        }
        return list;
    }
}
